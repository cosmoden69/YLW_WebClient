/// This code is written by Matthias Haenel
/// contact: www.intercopmu.de
/// 
/// you can use it free of charge, but please 
/// mention my name ;)
/// 
/// WinWordControl utilizes MS-WinWord2000 and 
/// WinWord-XP
/// 
/// It simulates a form element, with simple tricks.
///

using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace WinWordControl
{
	public class DocumentInstanceException : Exception
	{}
	
	public class ValidDocumentException : Exception
	{}

	public class WordInstanceException : Exception
	{}

	/// <summary>
	/// WinWordControl allows you to load doc-Files to your
	/// own application without any loss, because it uses 
	/// the real WinWord.
	/// </summary>
	public class WinWordControl : System.Windows.Forms.UserControl
	{

		[DllImport("user32.dll")]
		public static extern int FindWindow(string strclassName, string strWindowName);

		[DllImport("user32.dll")]
		static extern int SetParent(int hWndChild, int hWndNewParent);

        [DllImport("user32.dll")]
        static extern bool CloseWindow(int hWnd);

        [DllImport("kernel32", SetLastError = true)]
        static extern bool CloseHandle(IntPtr objectHandle);

        [DllImport("user32.dll", EntryPoint="SetWindowPos")]
		static extern bool SetWindowPos(
			int hWnd,               // handle to window
			int hWndInsertAfter,    // placement-order handle
			int X,                  // horizontal position
			int Y,                  // vertical position
			int cx,                 // width
			int cy,                 // height
			uint uFlags             // window-positioning options
		);
		
		[DllImport("user32.dll", EntryPoint="MoveWindow")]
		static extern bool MoveWindow(
			int Wnd, 
			int X, 
			int Y, 
			int Width, 
			int Height, 
			bool Repaint
		);

		const int SWP_DRAWFRAME = 0x20;
		const int SWP_NOMOVE = 0x2;
		const int SWP_NOSIZE = 0x1;
		const int SWP_NOZORDER = 0x4;

        private string filename = "";

        private dynamic document;
		private dynamic wd = null;

        public int wordWnd				= 0;

		/// <summary>
		/// needed designer variable
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WinWordControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// cleanup Ressources
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// !do not alter this code! It's designer code
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// WinWordControl
			// 
			this.Name = "WinWordControl";
			this.Size = new System.Drawing.Size(440, 336);
			this.Resize += new System.EventHandler(this.OnResize);
		}
		#endregion


		/// <summary>
		/// Close the current Document in the control --> you can 
		/// load a new one with LoadDocument
		/// </summary>
		public void CloseControl()
		{
			try
			{
                if (wd != null)
                {
                    wd.NormalTemplate.Saved = true;
                    object dummy = null;
                    object noSave = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                    if (document != null)
                    {
                        wd.Documents[document.Name].Close(ref noSave, ref dummy, ref dummy);
                        //document.Close(ref noSave, ref dummy, ref dummy);
                        document = null;
                    }
                    if (wd.Documents.Count < 1)
                    {
                        wd.Quit(ref noSave, ref dummy, ref dummy);
                        CloseWindow(wordWnd);
                        //KillProcess("WINWORD");
                    }
                }
                document = null;
                wd = null;
                SetParent(wordWnd, 0);
                wordWnd = 0;
                if (File.Exists(filename)) File.Delete(filename);
            }
            catch (Exception ex)
			{
                MessageBox.Show(ex.Message + "\r\n\r\n워드에 문제가 발생하여 워드프로그램을 종료합니다.\r\n다른 워드창이 있으면 자료를 저장하세요.");
                KillProcess("WINWORD");
            }
        }

		/// <summary>
		/// Loads a document into the control
		/// </summary>
		/// <param name="t_filename">path to the file (every type word can handle)</param>
		public void LoadDocument(string t_filename, bool lck = false)
		{
			filename = t_filename;
            object dummy = null;
            object noSave = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            int restartCount = 0;

            restart:
            if (restartCount > 3)
            {
                throw new Exception("워드에 문제가 지속적으로 발생하여 보고서 프로그램을 종료합니다");
            }

            if (wordWnd != 0)
            {
                SetParent(wordWnd, 0);
                wordWnd = 0;
            }

            if (wd == null)
            {
                try
                {
                    Type wordType = Type.GetTypeFromProgID("Word.Application");
                    wd = Activator.CreateInstance(wordType);
                    //wd = (Microsoft.Office.Interop.Word.ApplicationClass)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                }
                catch
                {
                    wd = new Microsoft.Office.Interop.Word.Application();
                }
            }
            if (wd == null) wd = new Microsoft.Office.Interop.Word.Application();

            if (document != null) 
			{
				try
				{
                    document.Close(ref noSave, ref dummy, ref dummy);
                    document = null;
                }
				catch
                {
                    document = null;
                }
            }

			object fileName = filename;
			object newTemplate = false;
			object docType = 0;
			object readOnly = true;
			object isVisible = true;

            try
            {
                if (wd == null)
                {
                    throw new WordInstanceException();
                }

                if (wd.Documents == null)
                {
                    throw new DocumentInstanceException();
                }

                document = wd.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.Message.StartsWith("Word에서 이 문서를 읽을 수 없습니다. 문서가 손상되었을 수 있습니다."))
                {
                    throw new Exception(ex.Message + "\r\n\r\n워드프로그램을 실행할 수 없습니다");
                }
                MessageBox.Show(ex.Message + "\r\n\r\n워드에 문제가 발생하여 워드프로그램을 종료합니다.\r\n다른 워드창이 있으면 자료를 저장하세요.");
                KillProcess("WINWORD");  //<--- 엑세스가 거부되었습니다 발생
                wd = null;
                restartCount++;
                goto restart;
            }
            if (document == null)
            {
                MessageBox.Show("워드에 문제가 발생하여 워드프로그램을 종료합니다.\r\n다른 워드창이 있으면 자료를 저장하세요.");
                KillProcess("WINWORD");  //<--- 엑세스가 거부되었습니다 발생
                wd = null;
                restartCount++;
                goto restart;
            }

            try
            {
                string cap = wd.Application.Caption;
                wd.Application.Caption = "haesung word";
                wordWnd = User32Helper.GetDesktopWindowHandle(wd.Application.Caption).ToInt32();
                wd.Application.Caption = cap;
                DisableMenu(lck);

                SetParent(wordWnd, this.Handle.ToInt32());

                //wd.ActiveWindow.ActivePane.View.Zoom.PageColumns = 2;
                wd.ActiveWindow.ActivePane.View.Zoom.PageFit = Microsoft.Office.Interop.Word.WdPageFit.wdPageFitBestFit;

                wd.Visible = true;
                wd.Activate();

                SetWindowPos(wordWnd, this.Handle.ToInt32(), 0, 0, this.Bounds.Width + 20, this.Bounds.Height + 20, SWP_NOZORDER | SWP_NOMOVE | SWP_DRAWFRAME);
                MoveWindow(wordWnd, -5, -33, this.Bounds.Width + 10, this.Bounds.Height + 57, true);
            }
            catch
			{
				MessageBox.Show("Error: do not load the document into the control until the parent window is shown!");
			}
			this.Parent.Focus();
		}

        private void DisableMenu(bool lck)
        {
            try
            {
                if (lck)
                {
                    //wd.CommandBars.ExecuteMso("MinimiseRibbon");
                    wd.CommandBars.ExecuteMso("HideRibbon");
                    wd.Application.ActiveDocument.Protect(Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyReading, false, System.String.Empty, false, false);
                }
                else
                {
                    //wd.CommandBars.ExecuteMso("MaximiseRibbon");
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void KillProcess(string pname)
        {
            try
            {
                Process[] plist = Process.GetProcessesByName(pname);
                for (int ii = 0; ii < plist.Length; ii++) plist[ii].Kill();
            }
            catch { }
        }

        public void PrintDocument()
        {
            try
            {
                if (document == null) return;
                var dialogResult = wd.Dialogs[Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint].Show();
                if (dialogResult == 1)
                    document.PrintOut();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveAsFile(string savefile)
        {
            try
            {
                if (document == null) return false;
                document.SaveAs(filename);
                System.IO.File.Copy(filename, savefile, true);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetDocumentText()
        {
            try
            {
                if (document == null) return "";
                return document.WordOpenXML;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void OnResize(object sender, System.EventArgs e)
		{
			MoveWindow(wordWnd, -5, -33, this.Bounds.Width+10,this.Bounds.Height+57, true);
            if (wd != null)
            {
                wd.ActiveWindow.ActivePane.View.Zoom.PageFit = Microsoft.Office.Interop.Word.WdPageFit.wdPageFitBestFit;
            }
        }
    }

    public class DesktopWindow
    {
        public IntPtr Handle { get; set; }
        public string Title { get; set; }
        public bool IsVisible { get; set; }
    }

    public class User32Helper
    {
        public delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowText",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction,
            IntPtr lParam);

        public static List<DesktopWindow> GetDesktopWindows()
        {
            var collection = new List<DesktopWindow>();
            EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                var result = new StringBuilder(255);
                GetWindowText(hWnd, result, result.Capacity + 1);
                string title = result.ToString();

                var isVisible = !string.IsNullOrEmpty(title) && IsWindowVisible(hWnd);

                collection.Add(new DesktopWindow { Handle = hWnd, Title = title, IsVisible = isVisible });

                return true;
            };

            EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);
            return collection;
        }

        public static IntPtr GetDesktopWindowHandle(string cap)
        {
            IntPtr ptr = IntPtr.Zero;
            List<DesktopWindow> lst = GetDesktopWindows();
            foreach (DesktopWindow win in lst)
            {
                if (win.Title.Contains(cap))
                    ptr = win.Handle;
            }
            return ptr;
        }
    }
}
