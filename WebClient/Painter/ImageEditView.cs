using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YLW_WebClient;
using YLW_WebClient.Painter.Controller;
using YLWService;

namespace YLW_WebClient.Painter
{
    public partial class ImageEditView : Form
    {
        private static frmAcdtPictGoods _parentForm = null;

        private static ReportParam _param = null;

        private DataGridViewRow _selectRow = null;

        private static ImageEditView current = null;

        public static ImageEditView Current { get { return current; } }

        bool _bEvent = false;

        public ImageEditView()
        {
            InitializeComponent();

            this.toolBar1.MouseMove += ToolBar1_MouseMove;
            this.panel1.MouseMove += Panel1_MouseMove;
            this.sheet1.SaveButtonClicked += Sheet1_SaveButtonClicked;

            MainController.Instance.ToolBar = toolBar1;
            MainController.Instance.Sheet = sheet1;

            _bEvent = true;
        }

        public static bool ShowPreview(frmAcdtPictGoods pFrm, ReportParam p)
        {
            try
            {
                _parentForm = pFrm;
                _param = p;

                if (current == null || current.IsDisposed)
                {
                    current = new ImageEditView();
                    current.Top = 0;
                    current.Left = 0;  // (Screen.PrimaryScreen.WorkingArea.Width - current.Width) / 2;
                    //current.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    current.Show();
                }
                else if (!current.Visible)
                {
                    current.Visible = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                current.Close();
                return false;
            }
        }

        public bool LoadDocument(DataGridViewRow row, Image img)
        {
            try
            {
                _selectRow = row;

                _bEvent = false;
                SetInit();
                _bEvent = true;

                sheet1.NewPage(img);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return false;
            }
        }

        private void SetInit()
        {
        }

        private void Sheet1_SaveButtonClicked(object sender, ObjectEventArgs e)
        {
            if (_parentForm != null)
            {
                _parentForm.SetChangeImage(_selectRow, (Bitmap)e.SendObject);
            }
        }

        private void ToolBar1_MouseMove(object sender, MouseEventArgs e)
        {
            sheet1.MouseMoveEventcall(e);
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            sheet1.MouseMoveEventcall(e);
        }
    }
}
