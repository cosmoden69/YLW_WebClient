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
        YLW_WebClient.ISmplInputer smplIputer = null;

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
                smplIputer = GetInputer(this.DeptGrpCd);
                smplIputer.SetReadOnlyMode(true);
                smplIputer.LoadDocument(p);
                smplIputer.Reload(yds);

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

        private ISmplInputer GetInputer(string dptgrp)
        {
            Control inputer = null;
            if (dptgrp == "2")
            {
                inputer = new YLW_WebClient.CAA.ucHyundaiInputer();
            }
            else if (dptgrp == "3")
            {
                inputer = new YLW_WebClient.CAA.ucHeungkukInputer();
            }
            else if (dptgrp == "4")
            {
                inputer = new YLW_WebClient.CAA.ucMeritzInputer();
            }
            else if (dptgrp == "5")
            {
                inputer = new YLW_WebClient.CAA.ucDBLifeInputer();
            }
            else if (dptgrp == "6")
            {
                inputer = new YLW_WebClient.CAA.ucDBLossInputer();
            }
            else if (dptgrp == "7")
            {
                inputer = new YLW_WebClient.CAA.ucMGLossSmplInputer();
            }
            else if (dptgrp == "8")
            {
                inputer = new YLW_WebClient.CAA.ucMGLossInputer();
            }
            else
            {
                inputer = new YLW_WebClient.CAA.ucInputer();
            }
            inputer.BackColor = System.Drawing.Color.White;
            inputer.Dock = System.Windows.Forms.DockStyle.Fill;
            inputer.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            inputer.Location = new System.Drawing.Point(5, 47);
            inputer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            inputer.Name = "ucInputer1";
            inputer.Size = new System.Drawing.Size(753, 827);
            panel1.Controls.Add(inputer);
            return inputer as ISmplInputer;
        }
    }
}