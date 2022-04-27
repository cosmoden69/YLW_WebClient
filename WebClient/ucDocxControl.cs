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
    public partial class ucDocxControl : DevExpress.XtraEditors.XtraUserControl, IViewerSheet
    {
        public ucDocxControl()
        {
            InitializeComponent();
        }

        public void SetReport(Stream stream, DevExpress.XtraRichEdit.DocumentFormat fmt)
        {
            richEditControl1.LoadDocument(stream, fmt);
        }

        public void PrintPreview(string printername)
        {
        }

        public void Print(string printername)
        {
            System.Drawing.Printing.PrinterSettings prt = new System.Drawing.Printing.PrinterSettings();
            prt.PrinterName = printername;
            richEditControl1.Print(prt);
        }
    }
}
