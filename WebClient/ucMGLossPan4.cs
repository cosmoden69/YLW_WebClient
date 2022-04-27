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
    public partial class ucMGLossPan4 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S211_LongCnts1
        {
            get { return txtS211_LongCnts1.rtbDoc.Rtf; }
            set { txtS211_LongCnts1.rtbDoc.Rtf = value; }
        }
        public string S211_LongCnts2
        {
            get { return txtS211_LongCnts2.rtbDoc.Rtf; }
            set { txtS211_LongCnts2.rtbDoc.Rtf = value; }
        }
        public string S211_LongCnts3
        {
            get { return txtS211_LongCnts3.rtbDoc.Rtf; }
            set { txtS211_LongCnts3.rtbDoc.Rtf = value; }
        }
        public string S212_LongCnts1
        {
            get { return txtS212_LongCnts1.rtbDoc.Rtf; }
            set { txtS212_LongCnts1.rtbDoc.Rtf = value; }
        }
        public string S212_LongCnts2
        {
            get { return txtS212_LongCnts2.rtbDoc.Rtf; }
            set { txtS212_LongCnts2.rtbDoc.Rtf = value; }
        }
        public string S212_LongCnts3
        {
            get { return txtS212_LongCnts3.rtbDoc.Rtf; }
            set { txtS212_LongCnts3.rtbDoc.Rtf = value; }
        }

        private bool readOnlyMode = false;
        public ucMGLossPan4()
        {
            InitializeComponent();

            this.txtS211_LongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS211_LongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS211_LongCnts3.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS212_LongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS212_LongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS212_LongCnts3.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS211_LongCnts1.ContentsResized += Txt0_ContentsResized;
            this.txtS211_LongCnts2.ContentsResized += Txt1_ContentsResized;
            this.txtS211_LongCnts3.ContentsResized += Txt2_ContentsResized;
            this.txtS212_LongCnts1.ContentsResized += Txt3_ContentsResized;
            this.txtS212_LongCnts2.ContentsResized += Txt4_ContentsResized;
            this.txtS212_LongCnts3.ContentsResized += Txt5_ContentsResized;
            this.txtS211_LongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS211_LongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS211_LongCnts3.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS212_LongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS212_LongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS212_LongCnts3.ContentsMouseWheel += Txt_ContentsMouseWheel;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            txtS211_LongCnts1.SetReadOnly(rdonly);
            txtS211_LongCnts2.SetReadOnly(rdonly);
            txtS211_LongCnts3.SetReadOnly(rdonly);
            txtS212_LongCnts1.SetReadOnly(rdonly);
            txtS212_LongCnts2.SetReadOnly(rdonly);
            txtS212_LongCnts3.SetReadOnly(rdonly);
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

        private void Txt0_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            panelEx1.Height = e.NewRectangle.Height;
            int hgt = 0 + panelEx1.Height;
            panelEx21.Top = hgt - 1; txtS211_LongCnts2.Top = hgt - 1; hgt += panelEx21.Height;
            panelEx22.Top = hgt - 1; txtS211_LongCnts3.Top = hgt - 1; hgt += panelEx22.Height;
            panelEx23.Top = hgt - 1; txtS212_LongCnts1.Top = hgt - 1; hgt += panelEx23.Height;
            panelEx24.Top = hgt - 1; txtS212_LongCnts2.Top = hgt - 1; hgt += panelEx24.Height;
            panelEx25.Top = hgt - 1; txtS212_LongCnts3.Top = hgt - 1; hgt += panelEx25.Height;
            panelEx2.Top = panelEx1.Top + panelEx1.Height - 1; panelEx2.Height = panelEx21.Height + panelEx22.Height + panelEx23.Height + panelEx24.Height + panelEx25.Height;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            panelEx21.Height = e.NewRectangle.Height;
            int hgt = panelEx21.Top + panelEx21.Height;
            panelEx22.Top = hgt - 1; txtS211_LongCnts3.Top = hgt - 1; hgt += panelEx22.Height;
            panelEx23.Top = hgt - 1; txtS212_LongCnts1.Top = hgt - 1; hgt += panelEx23.Height;
            panelEx24.Top = hgt - 1; txtS212_LongCnts2.Top = hgt - 1; hgt += panelEx24.Height;
            panelEx25.Top = hgt - 1; txtS212_LongCnts3.Top = hgt - 1; hgt += panelEx25.Height;
            panelEx2.Top = panelEx1.Top + panelEx1.Height - 1; panelEx2.Height = panelEx21.Height + panelEx22.Height + panelEx23.Height + panelEx24.Height + panelEx25.Height - 4;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt2_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            panelEx22.Height = e.NewRectangle.Height;
            int hgt = panelEx22.Top + panelEx22.Height;
            panelEx23.Top = hgt - 1; txtS212_LongCnts1.Top = hgt - 1; hgt += panelEx23.Height;
            panelEx24.Top = hgt - 1; txtS212_LongCnts2.Top = hgt - 1; hgt += panelEx24.Height;
            panelEx25.Top = hgt - 1; txtS212_LongCnts3.Top = hgt - 1; hgt += panelEx25.Height;
            panelEx2.Top = panelEx1.Top + panelEx1.Height - 1; panelEx2.Height = panelEx21.Height + panelEx22.Height + panelEx23.Height + panelEx24.Height + panelEx25.Height - 4;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt3_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            panelEx23.Height = e.NewRectangle.Height;
            int hgt = panelEx23.Top + panelEx23.Height;
            panelEx24.Top = hgt - 1; txtS212_LongCnts2.Top = hgt - 1; hgt += panelEx24.Height;
            panelEx25.Top = hgt - 1; txtS212_LongCnts3.Top = hgt - 1; hgt += panelEx25.Height;
            panelEx2.Top = panelEx1.Top + panelEx1.Height - 1; panelEx2.Height = panelEx21.Height + panelEx22.Height + panelEx23.Height + panelEx24.Height + panelEx25.Height - 4;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt4_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            panelEx24.Height = e.NewRectangle.Height;
            int hgt = panelEx24.Top + panelEx24.Height;
            panelEx25.Top = hgt - 1; txtS212_LongCnts3.Top = hgt - 1; hgt += panelEx25.Height;
            panelEx2.Top = panelEx1.Top + panelEx1.Height - 1; panelEx2.Height = panelEx21.Height + panelEx22.Height + panelEx23.Height + panelEx24.Height + panelEx25.Height - 4;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt5_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            panelEx25.Height = e.NewRectangle.Height;
            int hgt = panelEx25.Top + panelEx25.Height;
            panelEx2.Top = panelEx1.Top + panelEx1.Height - 1; panelEx2.Height = panelEx21.Height + panelEx22.Height + panelEx23.Height + panelEx24.Height + panelEx25.Height - 4;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            txtS211_LongCnts1.rtbDoc.Rtf = "";
            txtS211_LongCnts2.rtbDoc.Rtf = "";
            txtS211_LongCnts3.rtbDoc.Rtf = "";
            txtS212_LongCnts1.rtbDoc.Rtf = "";
            txtS212_LongCnts2.rtbDoc.Rtf = "";
            txtS212_LongCnts3.rtbDoc.Rtf = "";
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
