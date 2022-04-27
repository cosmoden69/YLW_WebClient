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
    public partial class HeungkukContractB : UserControl, IComparable
    {
        public delegate void HkCntrBResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler HeungkukContractBMouseWheel;
        public event HkCntrBResizeEventHandler HeungkukContractBResize;

        public string CltrCnts
        {
            get { return txtCltrCnts.Text; }
            set { txtCltrCnts.Text = value; }
        }
        public object InsurRegsAmt
        {
            get { return iniInsurRegsAmt.ValueObject; }
            set { iniInsurRegsAmt.ValueObject = Utils.ToDecimal(value); }
        }
        public bool IsNewRow { get; set; } = false;

        private HeungkukContractA _parentC = null;
        private bool readOnlyMode = false;

        public HeungkukContractB(HeungkukContractA p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();

            this.txtCltrCnts.TextChanged += new System.EventHandler(this.Text_Change);
            this.iniInsurRegsAmt.TextChanged += new System.EventHandler(this.PriceChange);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtCltrCnts.SetReadOnly(rdonly);
            this.iniInsurRegsAmt.SetReadOnly(rdonly);
            this.btn_no00_01.Enabled = !rdonly;
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

        private void Text_Change(object sender, EventArgs e)
        {
            Control txt = (Control)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(txt.Text))
            {
                this.IsNewRow = false;
                _parentC.AddEmptyRow();
            }
        }

        private void RichText_Change(object sender, EventArgs e)
        {
            ExtendedRichTextBox.RichTextBoxPrintCtrl txt = (ExtendedRichTextBox.RichTextBoxPrintCtrl)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(txt.Text))
            {
                this.IsNewRow = false;
                _parentC.AddEmptyRow();
            }
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(dat.Text))
            {
                this.IsNewRow = false;
                _parentC.AddEmptyRow();
            }
        }

        private void PriceChange(object sender, EventArgs e)
        {
            DevComponents.Editors.DoubleInput amt = (DevComponents.Editors.DoubleInput)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(amt.Text))
            {
                this.IsNewRow = false;
                _parentC.AddEmptyRow();
            }
        }

        private void Txt_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.HeungkukContractBMouseWheel != null) this.HeungkukContractBMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            if (this.HeungkukContractBResize != null) this.HeungkukContractBResize(this, e);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (readOnlyMode) return;
            ButtonX btn = (ButtonX)sender;
            _parentC.RemoveRow(this);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            HeungkukContractB other = obj as HeungkukContractB;
            if (other != null)
            {
                return 0;
            }
            return 1;
        }
    }
}
