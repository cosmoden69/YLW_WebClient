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
    public partial class ucDBLossPan3 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S3_LongCnts1
        {
            get { return txtLongCnts1.rtbDoc.Rtf; }
            set { txtLongCnts1.rtbDoc.Rtf = value; }
        }
        public string S3_ShrtCnts1
        {
            get { return txtShrtCnts1.Text; }
            set { txtShrtCnts1.Text = value; }
        }
        public string S3_LongCnts2
        {
            get { return txtLongCnts2.rtbDoc.Rtf; }
            set { txtLongCnts2.rtbDoc.Rtf = value; }
        }

        private bool readOnlyMode = false;
        public ucDBLossPan3()
        {
            InitializeComponent();

            this.txtShrtCnts1.Multiline = true;

            this.txtLongCnts1.ContentsResized += Txt1_ContentsResized;
            this.txtLongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtLongCnts2.ContentsResized += Txt2_ContentsResized;
            this.txtLongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtLongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtShrtCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtLongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            txtLongCnts1.rtbDoc.ReadOnly = rdonly;
            txtShrtCnts1.SetReadOnly(rdonly);
            txtLongCnts2.rtbDoc.ReadOnly = rdonly;
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
            this.panelEx22.Top = txtLongCnts1.Top + e.NewRectangle.Height + 3;
            this.txtShrtCnts1.Top = this.panelEx22.Top;
            this.panelEx23.Top = this.txtShrtCnts1.Top + this.txtShrtCnts1.Height + 3;
            this.txtLongCnts2.Top = this.panelEx23.Top;
            this.Height = this.txtLongCnts2.Top + this.txtLongCnts2.Height + 3;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt2_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            this.Height = this.txtLongCnts2.Top + e.NewRectangle.Height + 5;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            txtLongCnts1.rtbDoc.Rtf = "";
            txtShrtCnts1.Text = "";
            txtLongCnts2.rtbDoc.Rtf = "";
        }

        public void SetFocus()
        {
            this.txtLongCnts1.Focus();
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
