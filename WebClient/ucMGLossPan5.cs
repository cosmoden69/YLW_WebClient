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
    public partial class ucMGLossPan5 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S221_LongCnts1
        {
            get { return txtS221_LongCnts1.rtbDoc.Rtf; }
            set { txtS221_LongCnts1.rtbDoc.Rtf = value; }
        }
        public string S221_LongCnts2
        {
            get { return txtS221_LongCnts2.rtbDoc.Rtf; }
            set { txtS221_LongCnts2.rtbDoc.Rtf = value; }
        }

        private bool readOnlyMode = false;
        public ucMGLossPan5()
        {
            InitializeComponent();

            this.txtS221_LongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS221_LongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS221_LongCnts1.ContentsResized += Txt1_ContentsResized;
            this.txtS221_LongCnts2.ContentsResized += Txt2_ContentsResized;
            this.txtS221_LongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS221_LongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            txtS221_LongCnts1.SetReadOnly(rdonly);
            txtS221_LongCnts2.SetReadOnly(rdonly);
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
            panelEx1.Height = e.NewRectangle.Height;
            int hgt = panelEx1.Top + panelEx1.Height + 4;
            panelEx2.Top = hgt; txtS221_LongCnts2.Top = hgt; hgt += panelEx2.Height;
            this.Height = hgt + 2;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt2_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            panelEx2.Height = e.NewRectangle.Height;
            int hgt = panelEx2.Top + panelEx2.Height;
            this.Height = hgt + 2;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            txtS221_LongCnts1.rtbDoc.Rtf = "";
            txtS221_LongCnts2.rtbDoc.Rtf = "";
        }

        public void SetFocus()
        {
            //this.dtAcdtDt.Focus();
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
