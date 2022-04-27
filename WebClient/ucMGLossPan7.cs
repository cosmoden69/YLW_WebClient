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
    public partial class ucMGLossPan7 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S341_LongCnts1
        {
            get { return txtS341_LongCnts1.rtbDoc.Rtf; }
            set { txtS341_LongCnts1.rtbDoc.Rtf = value; }
        }
        public string S341_LongCnts2
        {
            get { return txtS341_LongCnts2.rtbDoc.Rtf; }
            set { txtS341_LongCnts2.rtbDoc.Rtf = value; }
        }
        public string S341_LongCnts3
        {
            get { return txtS341_LongCnts3.rtbDoc.Rtf; }
            set { txtS341_LongCnts3.rtbDoc.Rtf = value; }
        }

        private bool readOnlyMode = false;

        public ucMGLossPan7()
        {
            InitializeComponent();

            this.txtS341_LongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS341_LongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS341_LongCnts3.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS341_LongCnts1.ContentsResized += Txt1_ContentsResized;
            this.txtS341_LongCnts2.ContentsResized += Txt2_ContentsResized;
            this.txtS341_LongCnts3.ContentsResized += Txt3_ContentsResized;
            this.txtS341_LongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS341_LongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS341_LongCnts3.ContentsMouseWheel += Txt_ContentsMouseWheel;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtS341_LongCnts1.SetReadOnly(rdonly);
            this.txtS341_LongCnts2.SetReadOnly(rdonly);
            this.txtS341_LongCnts3.SetReadOnly(rdonly);
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
            int hgt = Math.Max(this.txtS341_LongCnts2.NewHeight, this.txtS341_LongCnts3.NewHeight);
            hgt = Math.Max(e.NewRectangle.Height, hgt);
            this.txtS341_LongCnts1.SetContentsHeight(hgt);
            this.txtS341_LongCnts2.SetContentsHeight(hgt);
            this.txtS341_LongCnts3.SetContentsHeight(hgt);
            this.Height = 22 + hgt;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt2_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int hgt = Math.Max(this.txtS341_LongCnts1.NewHeight, this.txtS341_LongCnts3.NewHeight);
            hgt = Math.Max(e.NewRectangle.Height, hgt);
            this.txtS341_LongCnts1.SetContentsHeight(hgt);
            this.txtS341_LongCnts2.SetContentsHeight(hgt);
            this.txtS341_LongCnts3.SetContentsHeight(hgt);
            this.Height = 22 + hgt;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt3_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int hgt = Math.Max(this.txtS341_LongCnts1.NewHeight, this.txtS341_LongCnts3.NewHeight);
            hgt = Math.Max(e.NewRectangle.Height, hgt);
            this.txtS341_LongCnts1.SetContentsHeight(hgt);
            this.txtS341_LongCnts2.SetContentsHeight(hgt);
            this.txtS341_LongCnts3.SetContentsHeight(hgt);
            this.Height = 22 + hgt;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            txtS341_LongCnts1.rtbDoc.Rtf = "";
            txtS341_LongCnts2.rtbDoc.Rtf = "";
            txtS341_LongCnts3.rtbDoc.Rtf = "";
        }

        public void SetFocus()
        {
            this.txtS341_LongCnts1.Focus();
        }

        private void Text_Change(object sender, EventArgs e)
        {
            System.Windows.Forms.Control txt = (System.Windows.Forms.Control)sender;
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;
        }

        private void PriceChange(object sender, EventArgs e)
        {
            DevComponents.Editors.DoubleInput amt = (DevComponents.Editors.DoubleInput)sender;
        }
    }
}
