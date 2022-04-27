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
    public partial class ucMeritzPan5 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S8_LongCnts1
        {
            get { return pnS81.rtbDoc.Rtf; }
            set { pnS81.rtbDoc.Rtf = value; }
        }
        public string S8_LongCnts2
        {
            get { return pnS82.rtbDoc.Rtf; }
            set { pnS82.rtbDoc.Rtf = value; }
        }
        public string S8_LongCnts3
        {
            get { return pnS83.rtbDoc.Rtf; }
            set { pnS83.rtbDoc.Rtf = value; }
        }
        public string S9_LongCnts1
        {
            get { return pnS91.rtbDoc.Rtf; }
            set { pnS91.rtbDoc.Rtf = value; }
        }
        public string S9_LongCnts2
        {
            get { return pnS92.rtbDoc.Rtf; }
            set { pnS92.rtbDoc.Rtf = value; }
        }

        private bool readOnlyMode = false;
        public ucMeritzPan5()
        {
            InitializeComponent();

            this.pnS81.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS82.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS83.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS91.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS92.MeritzPanAResize += PnS_MeritzPanAResize;

            this.pnS81.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS82.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS83.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS91.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS92.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.pnS81.SetReadOnlyMode(rdonly);
            this.pnS82.SetReadOnlyMode(rdonly);
            this.pnS83.SetReadOnlyMode(rdonly);
            this.pnS91.SetReadOnlyMode(rdonly);
            this.pnS92.SetReadOnlyMode(rdonly);
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

        private void PnS_MeritzPanAMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void PnS_MeritzPanAResize(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int height = 23;
            Point pos = new Point(0, height - 1);
            this.pnS81.Location = pos; pos.Y += this.pnS81.Height - 1;
            this.pnS82.Location = pos; pos.Y += this.pnS82.Height - 1;
            this.pnS83.Location = pos; pos.Y += this.pnS83.Height - 1;
            this.pnS91.Location = pos; pos.Y += this.pnS91.Height - 1;
            this.pnS92.Location = pos; pos.Y += this.pnS92.Height - 1;
            this.Height = pos.Y + 1;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            this.pnS81.rtbDoc.Rtf = "";
            this.pnS82.rtbDoc.Rtf = "";
            this.pnS83.rtbDoc.Rtf = "";
            this.pnS91.rtbDoc.Rtf = "";
            this.pnS92.rtbDoc.Rtf = "";
        }

        public void SetFocus()
        {
            this.pnS81.SetFocus();
        }
    }
}
