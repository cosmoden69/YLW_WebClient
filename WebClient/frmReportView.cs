using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace YLW_WebClient
{
    public partial class frmReportView : Form
    {
        public static event EventHandler eEventHandler;

        private static frmReportView current = null;

        public static Form Current { get { return current; } }

        public frmReportView()
        {
            InitializeComponent();
        }

        //출력물 누적됨 - 탭으로 
        public static bool ShowPreview(System.Windows.Forms.Control obj, string caption, string key)
        {
            try
            {
                if (current == null || current.IsDisposed)
                {
                    current = new frmReportView();
                    current.Top = 0;
                    current.Left = (Screen.PrimaryScreen.WorkingArea.Width - current.Width) / 2;
                    current.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    current.Show();
                }
                else if (!current.Visible)
                {
                    current.Visible = true;
                }
                return ShowPreviewSub(obj, caption, key);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private static bool ShowPreviewSub(System.Windows.Forms.Control obj, string caption, string key)
        {
            if (current == null) return false;
            if (obj == null) return false;

            eEventHandler = null;
            System.Windows.Forms.Control cntl = (System.Windows.Forms.Control)obj;
            cntl.Dock = DockStyle.Fill;
            DevExpress.XtraBars.Docking2010.Views.BaseDocument doc = current.ucSheet1.FindDocument(caption, key);
            if (doc != null)
                current.ucSheet1.ActivateDocument(doc);
            else
                current.ucSheet1.AddDocument(caption, key, cntl);
            return true;
        }
    }
}
