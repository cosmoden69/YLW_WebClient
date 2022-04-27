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
    public partial class ucHyundaiPan2 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S131_LongCnts1
        {
            get { return pnS131.rtbLongCnts1.Rtf; }
            set { pnS131.rtbLongCnts1.Rtf = value; }
        }
        public string S131_ShrtCnts1
        {
            get { return pnS131.ShrtCnts1; }
            set { pnS131.ShrtCnts1 = value; }
        }
        public string S131_LongCnts2
        {
            get { return pnS131.rtbLongCnts2.Rtf; }
            set { pnS131.rtbLongCnts2.Rtf = value; }
        }
        public string S131_ShrtCnts2
        {
            get { return pnS131.ShrtCnts2; }
            set { pnS131.ShrtCnts2 = value; }
        }
        public string S131_Amt1
        {
            get { return pnS131.Amt1; }
            set { pnS131.Amt1 = value; }
        }
        public string S132_LongCnts1
        {
            get { return pnS132.rtbLongCnts1.Rtf; }
            set { pnS132.rtbLongCnts1.Rtf = value; }
        }
        public string S132_ShrtCnts1
        {
            get { return pnS132.ShrtCnts1; }
            set { pnS132.ShrtCnts1 = value; }
        }
        public string S132_LongCnts2
        {
            get { return pnS132.rtbLongCnts2.Rtf; }
            set { pnS132.rtbLongCnts2.Rtf = value; }
        }
        public string S132_ShrtCnts2
        {
            get { return pnS132.ShrtCnts2; }
            set { pnS132.ShrtCnts2 = value; }
        }
        public string S132_Amt1
        {
            get { return pnS132.Amt1; }
            set { pnS132.Amt1 = value; }
        }
        public string S133_LongCnts1
        {
            get { return pnS133.rtbLongCnts1.Rtf; }
            set { pnS133.rtbLongCnts1.Rtf = value; }
        }
        public string S133_ShrtCnts1
        {
            get { return pnS133.ShrtCnts1; }
            set { pnS133.ShrtCnts1 = value; }
        }
        public string S133_LongCnts2
        {
            get { return pnS133.rtbLongCnts2.Rtf; }
            set { pnS133.rtbLongCnts2.Rtf = value; }
        }
        public string S133_ShrtCnts2
        {
            get { return pnS133.ShrtCnts2; }
            set { pnS133.ShrtCnts2 = value; }
        }
        public string S133_Amt1
        {
            get { return pnS133.Amt1; }
            set { pnS133.Amt1 = value; }
        }

        private bool _bEvent = false;

        private bool readOnlyMode = false;
        public ucHyundaiPan2()
        {
            InitializeComponent();

            this.pnS131.HyundaiPanAResize += PnS_HyundaiPanAResize;
            this.pnS132.HyundaiPanAResize += PnS_HyundaiPanAResize;
            this.pnS133.HyundaiPanAResize += PnS_HyundaiPanAResize;

            this.pnS131.HyundaiPanAMouseWheel += PnS_HyundaiPanAMouseWheel;
            this.pnS132.HyundaiPanAMouseWheel += PnS_HyundaiPanAMouseWheel;
            this.pnS133.HyundaiPanAMouseWheel += PnS_HyundaiPanAMouseWheel;

            _bEvent = true;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.pnS131.SetReadOnlyMode(rdonly);
            this.pnS132.SetReadOnlyMode(rdonly);
            this.pnS133.SetReadOnlyMode(rdonly);
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

        private void PnS_HyundaiPanAMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void PnS_HyundaiPanAResize(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int height = 45;
            Point pos = new Point(0, height - 1);
            this.pnS131.Location = pos; pos.Y += this.pnS131.Height - 1;
            this.pnS132.Location = pos; pos.Y += this.pnS132.Height - 1;
            this.pnS133.Location = pos; pos.Y += this.pnS133.Height - 1;
            this.Height = pos.Y + 1;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            _bEvent = false;

            this.pnS131.Clear();
            this.pnS132.Clear();
            this.pnS133.Clear();

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.pnS131.SetFocus();
        }
    }
}
