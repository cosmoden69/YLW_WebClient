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
    public partial class MGLossContractB : UserControl, IComparable
    {
        public delegate void MGCntrBResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler MGLossContractBMouseWheel;
        public event MGCntrBResizeEventHandler MGLossContractBResize;
        public event EventHandler MGLossContractBPriceChanged;

        public string CltrCnts
        {
            get { return txtCltrCnts.rtbDoc.Rtf; }
            set { txtCltrCnts.rtbDoc.Rtf = value; }
        }
        public object InsurRegsAmt
        {
            get { return iniInsurRegsAmt.ValueObject; }
            set { iniInsurRegsAmt.ValueObject = Utils.ToDecimal(value); }
        }
        public bool IsNewRow { get; set; } = false;

        private MGLossContractA _parentC = null;
        private bool readOnlyMode = false;

        public MGLossContractB(MGLossContractA p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();

            this.txtCltrCnts.rtbDoc.TextChanged += new System.EventHandler(this.RichText_Change);
            this.iniInsurRegsAmt.TextChanged += new System.EventHandler(this.PriceChange);
            this.txtCltrCnts.ContentsResized += Txt_ContentsResized;
            this.txtCltrCnts.ContentsMouseWheel += Txt_ContentsMouseWheel;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtCltrCnts.rtbDoc.ReadOnly = rdonly;
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
            TextBox txt = (TextBox)sender;

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
            if (this.MGLossContractBPriceChanged != null) this.MGLossContractBPriceChanged(this, e);
        }

        private void Txt_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.MGLossContractBMouseWheel != null) this.MGLossContractBMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int hgt = e.NewRectangle.Height + 2;
            this.txtCltrCnts.SetContentsHeight(hgt);
            this.iniInsurRegsAmt.MinimumSize = new Size(0, hgt);
            this.iniInsurRegsAmt.Height = hgt;
            this.panelEx18.Height = hgt;
            this.Height = hgt;
            this.pan_hide_00.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
            if (this.MGLossContractBResize != null) this.MGLossContractBResize(this, e);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (readOnlyMode) return;
            ButtonX btn = (ButtonX)sender;
            _parentC.RemoveRow(this);
            if (this.MGLossContractBPriceChanged != null) this.MGLossContractBPriceChanged(this, e);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            MGLossContractB other = obj as MGLossContractB;
            if (other != null)
            {
                return 0;
            }
            return 1;
        }
    }
}
