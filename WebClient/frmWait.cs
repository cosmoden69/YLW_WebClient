using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

using YLWService;

namespace YLW_WebClient
{
    public partial class frmWait : Form
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        /// 
        /// 윈도우를 맨 앞으로 가져오기 위한 Win32 API 메서드
        /// 
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        #region Static Function

        private static frmWait current = null;
        public static frmWait Current { get { return current; } }

        /// 
        /// 스플래쉬 닫을 때 true로 세팅하는 값
        /// 
        private static bool isCloseCall = false;

        /// 
        /// 스플래쉬 띄우기
        /// 
        public static void SplashShow()
        {
            isCloseCall = false;
            Thread thread = new Thread(new ThreadStart(ThreadShowWait));
            thread.Start();
        }

        /// 
        /// 스플래쉬 닫기
        /// 
        /// 스플래쉬를 닫은 후 맨 앞으로 가져올 폼
        public static void SplashClose(Form formFront)
        {
            //Thread의 loop 를 멈춘다.
            isCloseCall = true;

            //주어진 폼을 맨 앞으로
            if (formFront != null)
            {
                SetForegroundWindow(formFront.Handle);
                formFront.BringToFront();
            }
        }

        private static void ThreadShowWait()
        {
            if (current == null || current.IsDisposed)
            {
                current = new frmWait();
                current.Top = (Screen.PrimaryScreen.WorkingArea.Height - current.Height) / 2;
                current.Left = (Screen.PrimaryScreen.WorkingArea.Width - current.Width) / 2;
                current.Show();
            }
            else if (!current.Visible)
            {
                current.Visible = true;
            }

            SetWindowPos(current.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

            //닫기 명령이 올 때 가지 0.01 초 단위로 루프
            while (!isCloseCall)
            {
                Application.DoEvents();
                Thread.Sleep(10);
            }

            //닫는다.
            if (current != null)
            {
                current.CloseForce();
            }
        }
        #endregion Static Function

        #region SplashWnd Member, Function, Event

        /// 
        /// 값이 true 이면 창이 닫히지 않음.
        /// 

        private bool cannotClose = true;

        /// 
        /// 생성자
        /// 
        public frmWait()
        {
            InitializeComponent();

            //투명도는 줘도 되고 안 줘도 되고
            this.Opacity = 1f;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (cannotClose)
            {
                e.Cancel = true;
                return;
            }

            base.OnClosing(e);
        }

        public void CloseForce()
        {
            //OnClose 에서 닫힐 수 있도록 세팅
            cannotClose = false;
            current.Close();
            current = null;
        }

        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                return cp;
            }
        }
        #endregion SplashWnd Member, Function, Event
    }
}
