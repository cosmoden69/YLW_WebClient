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
    public partial class ucHyundaiPan4 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S201_LongCnts1
        {
            get { return pnS201.rtbLongCnts1.Rtf; }
            set { pnS201.rtbLongCnts1.Rtf = value; }
        }
        public string S201_LongCnts2
        {
            get { return pnS201.rtbLongCnts2.Rtf; }
            set { pnS201.rtbLongCnts2.Rtf = value; }
        }
        public string S202_LongCnts1
        {
            get { return pnS202.rtbLongCnts1.Rtf; }
            set { pnS202.rtbLongCnts1.Rtf = value; }
        }
        public string S202_LongCnts2
        {
            get { return pnS202.rtbLongCnts2.Rtf; }
            set { pnS202.rtbLongCnts2.Rtf = value; }
        }
        public string S203_LongCnts1
        {
            get { return pnS203.rtbLongCnts1.Rtf; }
            set { pnS203.rtbLongCnts1.Rtf = value; }
        }
        public string S203_LongCnts2
        {
            get { return pnS203.rtbLongCnts2.Rtf; }
            set { pnS203.rtbLongCnts2.Rtf = value; }
        }
        public string S204_LongCnts1
        {
            get { return pnS204.rtbLongCnts1.Rtf; }
            set { pnS204.rtbLongCnts1.Rtf = value; }
        }
        public string S204_LongCnts2
        {
            get { return pnS204.rtbLongCnts2.Rtf; }
            set { pnS204.rtbLongCnts2.Rtf = value; }
        }
        public string S205_LongCnts1
        {
            get { return pnS205.rtbLongCnts1.Rtf; }
            set { pnS205.rtbLongCnts1.Rtf = value; }
        }
        public string S205_LongCnts2
        {
            get { return pnS205.rtbLongCnts2.Rtf; }
            set { pnS205.rtbLongCnts2.Rtf = value; }
        }
        public string S206_LongCnts1
        {
            get { return pnS206.rtbLongCnts1.Rtf; }
            set { pnS206.rtbLongCnts1.Rtf = value; }
        }
        public string S206_LongCnts2
        {
            get { return pnS206.rtbLongCnts2.Rtf; }
            set { pnS206.rtbLongCnts2.Rtf = value; }
        }
        public string S207_LongCnts1
        {
            get { return pnS207.rtbLongCnts1.Rtf; }
            set { pnS207.rtbLongCnts1.Rtf = value; }
        }
        public string S207_LongCnts2
        {
            get { return pnS207.rtbLongCnts2.Rtf; }
            set { pnS207.rtbLongCnts2.Rtf = value; }
        }

        private bool _bEvent = false;

        private bool readOnlyMode = false;
        public ucHyundaiPan4()
        {
            InitializeComponent();

            this.pnS201.HyundaiPanBResize += PnS_HyundaiPanBResize;
            this.pnS202.HyundaiPanBResize += PnS_HyundaiPanBResize;
            this.pnS203.HyundaiPanBResize += PnS_HyundaiPanBResize;
            this.pnS204.HyundaiPanBResize += PnS_HyundaiPanBResize;
            this.pnS205.HyundaiPanBResize += PnS_HyundaiPanBResize;
            this.pnS206.HyundaiPanBResize += PnS_HyundaiPanBResize;
            this.pnS207.HyundaiPanBResize += PnS_HyundaiPanBResize;

            this.pnS201.HyundaiPanBMouseWheel += PnS_HyundaiPanBMouseWheel;
            this.pnS202.HyundaiPanBMouseWheel += PnS_HyundaiPanBMouseWheel;
            this.pnS203.HyundaiPanBMouseWheel += PnS_HyundaiPanBMouseWheel;
            this.pnS204.HyundaiPanBMouseWheel += PnS_HyundaiPanBMouseWheel;
            this.pnS205.HyundaiPanBMouseWheel += PnS_HyundaiPanBMouseWheel;
            this.pnS206.HyundaiPanBMouseWheel += PnS_HyundaiPanBMouseWheel;
            this.pnS207.HyundaiPanBMouseWheel += PnS_HyundaiPanBMouseWheel;

            _bEvent = true;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.pnS201.SetReadOnlyMode(rdonly);
            this.pnS202.SetReadOnlyMode(rdonly);
            this.pnS203.SetReadOnlyMode(rdonly);
            this.pnS204.SetReadOnlyMode(rdonly);
            this.pnS205.SetReadOnlyMode(rdonly);
            this.pnS206.SetReadOnlyMode(rdonly);
            this.pnS207.SetReadOnlyMode(rdonly);
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

        private void PnS_HyundaiPanBMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void PnS_HyundaiPanBResize(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int height = 23;
            Point pos = new Point(0, height - 1);
            this.pnS201.Location = pos; pos.Y += this.pnS201.Height - 1;
            this.pnS202.Location = pos; pos.Y += this.pnS202.Height - 1;
            this.pnS203.Location = pos; pos.Y += this.pnS203.Height - 1;
            this.pnS204.Location = pos; pos.Y += this.pnS204.Height - 1;
            this.pnS205.Location = pos; pos.Y += this.pnS205.Height - 1;
            this.pnS206.Location = pos; pos.Y += this.pnS206.Height - 1;
            this.pnS207.Location = pos; pos.Y += this.pnS207.Height - 1;
            this.Height = pos.Y + 1;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            _bEvent = false;

            this.pnS201.Clear();
            this.pnS202.Clear();
            this.pnS203.Clear();
            this.pnS204.Clear();
            this.pnS205.Clear();
            this.pnS206.Clear();
            this.pnS207.Clear();

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.pnS201.SetFocus();
        }
    }
}
