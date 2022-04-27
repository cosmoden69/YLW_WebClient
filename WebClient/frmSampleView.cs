using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using DevComponents.DotNetBar;
using System.Collections.Generic;

using YLWService;

namespace YLW_WebClient.CAA
{
    public partial class frmSampleView : Form
    {
        string DeptGrpCd = "";
        string RprtSmplSeq = "";
        string RprtSmplNm = "";

        public frmSampleView(string grpcd, string smplseq, string smplnm)
        {
            this.DeptGrpCd = grpcd;
            this.RprtSmplSeq = smplseq;
            this.RprtSmplNm = smplnm;

            InitializeComponent();

            this.Load += FrmSampleView_Load;
        }

        public bool LoadDocument(ReportParam p, DataSet yds)
        {
            try
            {
                this.ucInputer1.SetReadOnlyMode(true);
                this.ucInputer1.LoadDocument(p);
                this.ucInputer1.Reload(yds);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return false;
            }
        }

        private void FrmSampleView_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 774;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.txtRprtSmplNm.Text = this.RprtSmplNm;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Program.G_Create_))
                Directory.CreateDirectory(Program.G_Create_);

            if (!Directory.Exists(Program.G_WD_Path))
                Directory.CreateDirectory(Program.G_WD_Path);
            
            ScreenDetect();            
        }

        private void ScreenDetect()
        {
            Location = new Point(Screen.PrimaryScreen.Bounds.Left, 0);
            Size = new Size(Width, Screen.PrimaryScreen.WorkingArea.Height);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}