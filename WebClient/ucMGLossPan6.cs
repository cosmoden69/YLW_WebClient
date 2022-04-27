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
    public partial class ucMGLossPan6 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S331_LongCnts1
        {
            get { return txtS331_LongCnts1.rtbDoc.Rtf; }
            set { txtS331_LongCnts1.rtbDoc.Rtf = value; }
        }
        public string S331_LongCnts2
        {
            get { return txtS331_LongCnts2.rtbDoc.Rtf; }
            set { txtS331_LongCnts2.rtbDoc.Rtf = value; }
        }
        public string S331_LongCnts3
        {
            get { return txtS331_LongCnts3.rtbDoc.Rtf; }
            set { txtS331_LongCnts3.rtbDoc.Rtf = value; }
        }
        public string S332_LongCnts1
        {
            get { return txtS332_LongCnts1.rtbDoc.Rtf; }
            set { txtS332_LongCnts1.rtbDoc.Rtf = value; }
        }
        public string S332_LongCnts2
        {
            get { return txtS332_LongCnts2.rtbDoc.Rtf; }
            set { txtS332_LongCnts2.rtbDoc.Rtf = value; }
        }

        private bool readOnlyMode = false;
        public ucMGLossPan6()
        {
            InitializeComponent();

            this.txtS331_LongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS331_LongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS331_LongCnts3.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS332_LongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS332_LongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS331_LongCnts1.ContentsResized += Txt1_ContentsResized;
            this.txtS331_LongCnts2.ContentsResized += Txt2_ContentsResized;
            this.txtS331_LongCnts3.ContentsResized += Txt3_ContentsResized;
            this.txtS332_LongCnts1.ContentsResized += Txt4_ContentsResized;
            this.txtS332_LongCnts2.ContentsResized += Txt5_ContentsResized;
            this.txtS331_LongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS331_LongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS331_LongCnts3.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS332_LongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS332_LongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            txtS331_LongCnts1.SetReadOnly(rdonly);
            txtS331_LongCnts2.SetReadOnly(rdonly);
            txtS331_LongCnts3.SetReadOnly(rdonly);
            txtS332_LongCnts1.SetReadOnly(rdonly);
            txtS332_LongCnts2.SetReadOnly(rdonly);
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
            panelEx21.Height = e.NewRectangle.Height;
            int hgt = panelEx21.Top + panelEx21.Height - 1;
            panelEx22.Top = hgt - 1; txtS331_LongCnts2.Top = hgt - 1; hgt += panelEx22.Height;
            panelEx23.Top = hgt - 1; txtS331_LongCnts3.Top = hgt - 1; hgt += panelEx23.Height;
            panelEx24.Top = hgt - 1; txtS332_LongCnts1.Top = hgt - 1; hgt += panelEx24.Height;
            panelEx25.Top = hgt - 1; txtS332_LongCnts2.Top = hgt - 1; hgt += panelEx25.Height;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt2_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            panelEx22.Height = e.NewRectangle.Height;
            int hgt = panelEx22.Top + panelEx22.Height - 1;
            panelEx23.Top = hgt - 1; txtS331_LongCnts3.Top = hgt - 1; hgt += panelEx23.Height;
            panelEx24.Top = hgt - 1; txtS332_LongCnts1.Top = hgt - 1; hgt += panelEx24.Height;
            panelEx25.Top = hgt - 1; txtS332_LongCnts2.Top = hgt - 1; hgt += panelEx25.Height;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt3_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            panelEx23.Height = e.NewRectangle.Height;
            int hgt = panelEx23.Top + panelEx23.Height - 1;
            panelEx24.Top = hgt - 1; txtS332_LongCnts1.Top = hgt - 1; hgt += panelEx24.Height;
            panelEx25.Top = hgt - 1; txtS332_LongCnts2.Top = hgt - 1; hgt += panelEx25.Height;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt4_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            panelEx24.Height = e.NewRectangle.Height;
            int hgt = panelEx24.Top + panelEx24.Height - 1;
            panelEx25.Top = hgt - 1; txtS332_LongCnts2.Top = hgt - 1; hgt += panelEx25.Height;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt5_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            panelEx25.Height = e.NewRectangle.Height;
            int hgt = panelEx25.Top + panelEx25.Height - 1;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            txtS331_LongCnts1.rtbDoc.Rtf = "";
            txtS331_LongCnts2.rtbDoc.Rtf = "";
            txtS331_LongCnts3.rtbDoc.Rtf = "";
            txtS332_LongCnts1.rtbDoc.Rtf = "";
            txtS332_LongCnts2.rtbDoc.Rtf = "";
        }

        public void SetFocus()
        {
            this.txtS331_LongCnts1.Focus();
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
