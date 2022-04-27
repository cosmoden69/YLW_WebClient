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
    public partial class ucHyundaiPan3 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string LongCnts1
        {
            get { return txtLongCnts1.rtbDoc.Rtf; }
            set { txtLongCnts1.rtbDoc.Rtf = value; }
        }

        private bool readOnlyMode = false;
        public ucHyundaiPan3()
        {
            InitializeComponent();

            this.txtLongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtLongCnts1.ContentsResized += Txt_ContentsResized;
            this.txtLongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            txtLongCnts1.rtbDoc.ReadOnly = rdonly;
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

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int height = 23;
            this.Height = height + e.NewRectangle.Height;
            this.panel1.Height = height + e.NewRectangle.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            txtLongCnts1.rtbDoc.Rtf = "";
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
