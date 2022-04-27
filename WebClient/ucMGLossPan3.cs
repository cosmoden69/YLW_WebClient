using System;
using System.IO;
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
    public partial class ucMGLossPan3 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S131_LongCnts1
        {
            get { return txtS131_LongCnts1.rtbDoc.Rtf; }
            set { txtS131_LongCnts1.rtbDoc.Rtf = value; }
        }
        public string S131_LongCnts2
        {
            get { return txtS131_LongCnts2.rtbDoc.Rtf; }
            set { txtS131_LongCnts2.rtbDoc.Rtf = value; }
        }
        public string S131_ShrtCnts1
        {
            get { return txtS131_ShrtCnts1.Text; }
            set { txtS131_ShrtCnts1.Text = value; }
        }
        public string S131_ShrtCnts2
        {
            get { return txtS131_ShrtCnts2.Text; }
            set { txtS131_ShrtCnts2.Text = value; }
        }
        public string IsrdJob
        {
            get { return txtIsrdJob.Text; }
            set { txtIsrdJob.Text = value; }
        }
        public string IsrdJobDmnd
        {
            get { return txtIsrdJobDmnd.Text; }
            set { txtIsrdJobDmnd.Text = value; }
        }
        public string IsrdJobNow
        {
            get { return txtIsrdJobNow.Text; }
            set { txtIsrdJobNow.Text = value; }
        }
        public string S131_LongCnts3
        {
            get { return txtS131_LongCnts3.rtbDoc.Rtf; }
            set { txtS131_LongCnts3.rtbDoc.Rtf = value; }
        }

        private bool readOnlyMode = false;
        public ucMGLossPan3()
        {
            InitializeComponent();

            this.txtS131_LongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS131_LongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS131_ShrtCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS131_ShrtCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdJob.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdJobDmnd.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdJobNow.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS131_LongCnts3.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS131_LongCnts1.ContentsResized += Txt1_ContentsResized;
            this.txtS131_LongCnts2.ContentsResized += Txt2_ContentsResized;
            this.txtS131_LongCnts3.ContentsResized += Txt3_ContentsResized;
            this.txtS131_LongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS131_LongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS131_LongCnts3.ContentsMouseWheel += Txt_ContentsMouseWheel;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            txtS131_LongCnts1.SetReadOnly(rdonly);
            txtS131_LongCnts2.SetReadOnly(rdonly);
            txtS131_ShrtCnts1.SetReadOnly(rdonly);
            txtS131_ShrtCnts2.SetReadOnly(rdonly);
            txtIsrdJob.SetReadOnly(rdonly);
            txtIsrdJobDmnd.SetReadOnly(rdonly);
            txtIsrdJobNow.SetReadOnly(rdonly);
            txtS131_LongCnts3.SetReadOnly(rdonly);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Return)
            {
                SendKeys.Send("{TAB}");
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            this.Focus();
        }

        private void Txt_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void Txt1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int hgt = txtS131_LongCnts1.Top + txtS131_LongCnts1.Height + 4;
            panelEx2.Top = hgt; hgt += panelEx2.Height;
            txtS131_LongCnts2.Top = hgt; hgt += txtS131_LongCnts2.Height + 4;
            panelEx3.Top = hgt; txtS131_ShrtCnts1.Top = hgt; hgt += txtS131_ShrtCnts1.Height + 4;
            panelEx4.Top = hgt; txtS131_ShrtCnts2.Top = hgt; hgt += txtS131_ShrtCnts2.Height + 4;
            panelEx5.Top = hgt; txtIsrdJob.Top = hgt; hgt += txtIsrdJob.Height + 4;
            panelEx6.Top = hgt; txtIsrdJobDmnd.Top = hgt; hgt += txtIsrdJobDmnd.Height + 4;
            panelEx7.Top = hgt; txtIsrdJobNow.Top = hgt; hgt += txtIsrdJobNow.Height + 4;
            panelEx8.Top = hgt; hgt += panelEx8.Height;
            txtS131_LongCnts3.Top = hgt; hgt += txtS131_LongCnts3.Height;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt2_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int hgt = txtS131_LongCnts2.Top + txtS131_LongCnts2.Height + 4;
            panelEx3.Top = hgt; txtS131_ShrtCnts1.Top = hgt; hgt += txtS131_ShrtCnts1.Height + 4;
            panelEx4.Top = hgt; txtS131_ShrtCnts2.Top = hgt; hgt += txtS131_ShrtCnts2.Height + 4;
            panelEx5.Top = hgt; txtIsrdJob.Top = hgt; hgt += txtIsrdJob.Height + 4;
            panelEx6.Top = hgt; txtIsrdJobDmnd.Top = hgt; hgt += txtIsrdJobDmnd.Height + 4;
            panelEx7.Top = hgt; txtIsrdJobNow.Top = hgt; hgt += txtIsrdJobNow.Height + 4;
            panelEx8.Top = hgt; hgt += panelEx8.Height;
            txtS131_LongCnts3.Top = hgt; hgt += txtS131_LongCnts3.Height;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt3_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int hgt = txtS131_LongCnts3.Top + txtS131_LongCnts3.Height;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            txtS131_LongCnts1.rtbDoc.Rtf = "";
            txtS131_LongCnts2.rtbDoc.Rtf = "";
            txtS131_ShrtCnts1.Text = "";
            txtS131_ShrtCnts2.Text = "";
            txtIsrdJob.Text = "";
            txtIsrdJobDmnd.Text = "";
            txtIsrdJobNow.Text = "";
            txtS131_LongCnts3.rtbDoc.Rtf = "";
        }

        public void SetFocus()
        {
            this.txtS131_LongCnts1.Focus();
        }

        private void Text_Change(object sender, EventArgs e)
        {
            Control txt = (Control)sender;
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;
        }
    }
}
