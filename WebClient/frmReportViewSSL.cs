using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using YLWService;

namespace YLW_WebClient
{
    public partial class frmReportViewSSL : Form
    {
        private static string _fileName = "";
        private static string _fileSeq = "";
        private static ReportParam _param = null;

        private static frmReportViewSSL current = null;

        public static frmReportViewSSL Current { get { return current; } }

        public frmReportViewSSL()
        {
            InitializeComponent();

            this.btnPrint.Click += BtnPrint_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnClose.Click += BtnClose_Click;
            this.FormClosing += FrmReportViewSSL_FormClosing;
        }

        private void FrmReportViewSSL_FormClosing(object sender, FormClosingEventArgs e)
        {
            winWordControl1.CloseControl();
            //this.Hide();
            //e.Cancel = true;
        }

        public static bool ShowPreview(ReportParam p)
        {
            try
            {
                _param = p;
                _fileSeq = "";

                if (!Directory.Exists(Program.G_Create_))
                    Directory.CreateDirectory(Program.G_Create_);

                if (!Directory.Exists(Program.G_WD_Path))
                    Directory.CreateDirectory(Program.G_WD_Path);

                if (current == null || current.IsDisposed)
                {
                    current = new frmReportViewSSL();
                    current.Top = 0;
                    current.Left = 774; //(Screen.PrimaryScreen.WorkingArea.Width - current.Width) / 2;
                    current.Height = Screen.PrimaryScreen.WorkingArea.Height;
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

        public bool LoadDocument(string caption, string key, Stream stream)
        {
            try
            {
                _fileName = caption;
                //string file = Program.G_Create_ + Guid.NewGuid().ToString() + ".docx";
                string file = Program.G_Create_ + "조사보고서.docx";
                winWordControl1.CloseControl();
                if (File.Exists(file)) File.Delete(file);
                using (FileStream fs = new FileStream(file, FileMode.CreateNew))
                {
                    stream.CopyTo(fs);
                }
                switch (_param.ReportName)
                {
                    case "RptAdjSLRptEDISSL":     //삼성계약적부
                        btnSave.Visible = true;
                        winWordControl1.LoadDocument(file, true);
                        break;
                    default:
                        btnSave.Visible = true;
                        winWordControl1.LoadDocument(file, false);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return false;
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                winWordControl1.PrintDocument();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "docx";
            dlg.Filter = "docx files(*.docx)|*.docx";
            if (dlg.ShowDialog() != DialogResult.OK) return;
            string uploadfile = dlg.FileName;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!winWordControl1.SaveAsFile(uploadfile))
                {
                    MessageBox.Show("보고서 다운로드용 파일 생성 실패");
                    return;
                }
                System.Diagnostics.Process.Start(Path.GetDirectoryName(uploadfile));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
