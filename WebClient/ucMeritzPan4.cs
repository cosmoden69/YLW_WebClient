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
    public partial class ucMeritzPan4 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S6_LongCnts1
        {
            get { return pnS61.rtbDoc.Rtf; }
            set { pnS61.rtbDoc.Rtf = value; }
        }
        public string S6_LongCnts2
        {
            get { return pnS62.rtbDoc.Rtf; }
            set { pnS62.rtbDoc.Rtf = value; }
        }
        public string S6_LongCnts3
        {
            get { return pnS63.rtbDoc.Rtf; }
            set { pnS63.rtbDoc.Rtf = value; }
        }
        public string S7_LongCnts1
        {
            get { return pnS71.rtbDoc.Rtf; }
            set { pnS71.rtbDoc.Rtf = value; }
        }
        public string S7_LongCnts2
        {
            get { return pnS72.rtbDoc.Rtf; }
            set { pnS72.rtbDoc.Rtf = value; }
        }

        private bool readOnlyMode = false;
        public ucMeritzPan4()
        {
            InitializeComponent();

            this.pnS61.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS62.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS63.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS71.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS72.MeritzPanAResize += PnS_MeritzPanAResize;

            this.pnS61.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS62.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS63.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS71.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS72.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.pnS61.SetReadOnlyMode(rdonly);
            this.pnS62.SetReadOnlyMode(rdonly);
            this.pnS63.SetReadOnlyMode(rdonly);
            this.pnS71.SetReadOnlyMode(rdonly);
            this.pnS72.SetReadOnlyMode(rdonly);
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
            this.pnS61.Location = pos; pos.Y += this.pnS61.Height - 1;
            this.pnS62.Location = pos; pos.Y += this.pnS62.Height - 1;
            this.pnS63.Location = pos; pos.Y += this.pnS63.Height - 1;
            this.pnS71.Location = pos; pos.Y += this.pnS71.Height - 1;
            this.pnS72.Location = pos; pos.Y += this.pnS72.Height - 1;
            this.Height = pos.Y + 1;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            this.pnS61.rtbDoc.Rtf = "";
            this.pnS62.rtbDoc.Rtf = "";
            this.pnS63.rtbDoc.Rtf = "";
            this.pnS71.rtbDoc.Rtf = "";
            this.pnS72.rtbDoc.Rtf = "";
        }

        public void SetFocus()
        {
            this.pnS61.SetFocus();
        }
    }
}
