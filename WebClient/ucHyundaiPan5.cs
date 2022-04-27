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
    public partial class ucHyundaiPan5 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string S301_LongCnts1
        {
            get { return pnS301.rtbLongCnts1.Rtf; }
            set { pnS301.rtbLongCnts1.Rtf = value; }
        }
        public string S301_LongCnts2
        {
            get { return pnS301.rtbLongCnts2.Rtf; }
            set { pnS301.rtbLongCnts2.Rtf = value; }
        }

        private bool _bEvent = false;

        private bool readOnlyMode = false;
        public ucHyundaiPan5()
        {
            InitializeComponent();

            this.pnS301.HyundaiPanBResize += PnS_HyundaiPanBResize;

            this.pnS301.HyundaiPanBMouseWheel += PnS_HyundaiPanBMouseWheel;

            _bEvent = true;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.pnS301.SetReadOnlyMode(rdonly);
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
            this.pnS301.Location = pos; pos.Y += this.pnS301.Height - 1;
            this.Height = pos.Y + 1;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            _bEvent = false;

            this.pnS301.Clear();

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.pnS301.SetFocus();
        }
    }
}
