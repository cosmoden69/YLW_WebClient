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
    public partial class MGLossSmplContractBT : UserControl
    {
        public object Sum
        {
            get { return iniSum.ValueObject; }
            set { iniSum.ValueObject = Utils.ToDecimal(value); }
        }
        private MGLossSmplContractA _parentC = null;
        private bool readOnlyMode = false;

        public MGLossSmplContractBT(MGLossSmplContractA p)
        {
            this._parentC = p;

            InitializeComponent();

            this.iniSum.SetReadOnly(true);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
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
    }
}
