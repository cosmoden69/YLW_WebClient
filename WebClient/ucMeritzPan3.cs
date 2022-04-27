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
    public partial class ucMeritzPan3 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S3_LongCnts1
        {
            get { return pnS31.rtbDoc.Rtf; }
            set { pnS31.rtbDoc.Rtf = value; }
        }
        public string S3_LongCnts2
        {
            get { return pnS32.rtbDoc.Rtf; }
            set { pnS32.rtbDoc.Rtf = value; }
        }
        public string S3_LongCnts3
        {
            get { return pnS33.rtbDoc.Rtf; }
            set { pnS33.rtbDoc.Rtf = value; }
        }
        public string S4_LongCnts1
        {
            get { return pnS41.rtbDoc.Rtf; }
            set { pnS41.rtbDoc.Rtf = value; }
        }
        public string S4_LongCnts2
        {
            get { return pnS42.rtbDoc.Rtf; }
            set { pnS42.rtbDoc.Rtf = value; }
        }
        public string S4_LongCnts3
        {
            get { return pnS43.rtbDoc.Rtf; }
            set { pnS43.rtbDoc.Rtf = value; }
        }
        public string S5_LongCnts1
        {
            get { return pnS51.rtbDoc.Rtf; }
            set { pnS51.rtbDoc.Rtf = value; }
        }
        public string S5_LongCnts2
        {
            get { return pnS52.rtbDoc.Rtf; }
            set { pnS52.rtbDoc.Rtf = value; }
        }
        public string S5_LongCnts3
        {
            get { return pnS53.rtbDoc.Rtf; }
            set { pnS53.rtbDoc.Rtf = value; }
        }

        private bool _bEvent = false;

        private bool readOnlyMode = false;
        public ucMeritzPan3()
        {
            InitializeComponent();

            this.pnS31.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS32.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS33.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS41.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS42.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS43.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS51.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS52.MeritzPanAResize += PnS_MeritzPanAResize;
            this.pnS53.MeritzPanAResize += PnS_MeritzPanAResize;

            this.pnS31.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS32.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS33.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS41.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS42.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS43.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS51.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS52.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;
            this.pnS53.MeritzPanAMouseWheel += PnS_MeritzPanAMouseWheel;

            _bEvent = true;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.pnS31.SetReadOnlyMode(rdonly);
            this.pnS32.SetReadOnlyMode(rdonly);
            this.pnS33.SetReadOnlyMode(rdonly);
            this.pnS41.SetReadOnlyMode(rdonly);
            this.pnS42.SetReadOnlyMode(rdonly);
            this.pnS43.SetReadOnlyMode(rdonly);
            this.pnS51.SetReadOnlyMode(rdonly);
            this.pnS52.SetReadOnlyMode(rdonly);
            this.pnS53.SetReadOnlyMode(rdonly);
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
            this.pnS31.Location = pos; pos.Y += this.pnS31.Height - 1;
            this.pnS32.Location = pos; pos.Y += this.pnS32.Height - 1;
            this.pnS33.Location = pos; pos.Y += this.pnS33.Height - 1;
            this.pnS41.Location = pos; pos.Y += this.pnS41.Height - 1;
            this.pnS42.Location = pos; pos.Y += this.pnS42.Height - 1;
            this.pnS43.Location = pos; pos.Y += this.pnS43.Height - 1;
            this.pnS51.Location = pos; pos.Y += this.pnS51.Height - 1;
            this.pnS52.Location = pos; pos.Y += this.pnS52.Height - 1;
            this.pnS53.Location = pos; pos.Y += this.pnS53.Height - 1;
            this.Height = pos.Y + 1;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            _bEvent = false;

            this.pnS31.rtbDoc.Rtf = "";
            this.pnS32.rtbDoc.Rtf = "";
            this.pnS33.rtbDoc.Rtf = "";
            this.pnS41.rtbDoc.Rtf = "";
            this.pnS42.rtbDoc.Rtf = "";
            this.pnS43.rtbDoc.Rtf = "";
            this.pnS51.rtbDoc.Rtf = "";
            this.pnS52.rtbDoc.Rtf = "";
            this.pnS53.rtbDoc.Rtf = "";

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.pnS31.SetFocus();
        }
    }
}
