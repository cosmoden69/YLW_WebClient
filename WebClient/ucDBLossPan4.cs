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
    public partial class ucDBLossPan4 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

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
        public string S5_ShrtCnts1
        {
            get { return pnS54.TextValue; }
            set { pnS54.TextValue = value; }
        }

        private bool readOnlyMode = false;
        public ucDBLossPan4()
        {
            InitializeComponent();

            this.pnS51.DBLossPanAResize += PnS_DBLossPanAResize;
            this.pnS52.DBLossPanAResize += PnS_DBLossPanAResize;
            this.pnS53.DBLossPanAResize += PnS_DBLossPanAResize;
            this.pnS54.DBLossPanBResize += PnS_DBLossPanAResize;

            this.pnS51.DBLossPanAMouseWheel += PnS_DBLossPanAMouseWheel;
            this.pnS52.DBLossPanAMouseWheel += PnS_DBLossPanAMouseWheel;
            this.pnS53.DBLossPanAMouseWheel += PnS_DBLossPanAMouseWheel;
            this.pnS54.DBLossPanBMouseWheel += PnS_DBLossPanAMouseWheel;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.pnS51.SetReadOnlyMode(rdonly);
            this.pnS52.SetReadOnlyMode(rdonly);
            this.pnS53.SetReadOnlyMode(rdonly);
            this.pnS54.SetReadOnlyMode(rdonly);
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

        private void PnS_DBLossPanAMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void PnS_DBLossPanAResize(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int height = 23;
            Point pos = new Point(0, height - 1);
            this.pnS51.Location = pos; pos.Y += this.pnS51.Height - 1;
            this.pnS52.Location = pos; pos.Y += this.pnS52.Height - 1;
            this.pnS53.Location = pos; pos.Y += this.pnS53.Height - 1;
            this.pnS54.Location = pos; pos.Y += this.pnS54.Height - 1;
            this.Height = pos.Y + 1;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            this.pnS51.rtbDoc.Rtf = "";
            this.pnS52.rtbDoc.Rtf = "";
            this.pnS53.rtbDoc.Rtf = "";
            this.pnS54.TextValue = "";
        }

        public void SetFocus()
        {
            this.pnS51.SetFocus();
        }
    }
}
