using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;

namespace YLW_WebClient
{
    public partial class ucDocxControl2 : DevExpress.XtraEditors.XtraUserControl, IViewerSheet
    {
        string fileName = "";

        public ucDocxControl2()
        {
            InitializeComponent();

            this.Load += UcDocxControl2_Load;
            this.Disposed += UcDocxControl2_Disposed;
        }

        private void UcDocxControl2_Disposed(object sender, EventArgs e)
        {
            try
            {
                System.IO.File.Delete(fileName);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void UcDocxControl2_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Program.G_Create_))
                Directory.CreateDirectory(Program.G_Create_);

            if (!Directory.Exists(Program.G_WD_Path))
                Directory.CreateDirectory(Program.G_WD_Path);
        }

        public void SetReport(Stream stream)
        {
            fileName = Program.G_Create_ + @"\" + Guid.NewGuid().ToString() + ".docx";
            using (FileStream fs = new FileStream(fileName, FileMode.CreateNew))
            {
                stream.CopyTo(fs);
            }

            winWordControl1.LoadDocument(fileName);
        }

        public void PrintPreview(string printername)
        {
        }

        public void Print(string printername)
        {
            winWordControl1.PrintDocument();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            winWordControl1.PrintDocument();
        }
    }
}
