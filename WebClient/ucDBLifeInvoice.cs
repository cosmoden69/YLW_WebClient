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
    public partial class ucDBLifeInvoice : UserControl
    {
        public delegate void DBLfResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler ContentsMouseWheel;
        public event DBLfResizeEventHandler DBLifePanInvoiceResize;

        public string InvcAmtCof
        {
            get { return txtInvcAmtCof.Text.Replace(",", ""); }
            set { txtInvcAmtCof.Text = Utils.AddComma(value); }
        }
        public string InvcAdjFeeCdNm
        {
            get { return txtInvcAdjFeeCdNm.Text; }
            set { txtInvcAdjFeeCdNm.Text = value; }
        }
        public string InvcAdjFee
        {
            get { return txtInvcAdjFee.Text.Replace(",", ""); }
            set { txtInvcAdjFee.Text = Utils.AddComma(value); }
        }
        public string InvcDocuAmt
        {
            get { return txtInvcDocuAmt.Text.Replace(",", ""); }
            set { txtInvcDocuAmt.Text = Utils.AddComma(value); }
        }
        public string InvcCsltReqAmt
        {
            get { return txtInvcCsltReqAmt.Text.Replace(",", ""); }
            set { txtInvcCsltReqAmt.Text = Utils.AddComma(value); }
        }
        public string InvcTrspExps
        {
            get { return txtInvcTrspExps.Text.Replace(",", ""); }
            set { txtInvcTrspExps.Text = Utils.AddComma(value); }
        }
        public string InvcIctvAmt
        {
            get { return txtInvcIctvAmt.Text.Replace(",", ""); }
            set { txtInvcIctvAmt.Text = Utils.AddComma(value); }
        }

        private bool _bEvent = false;

        private bool readOnlyMode = false;
        public ucDBLifeInvoice()
        {
            InitializeComponent();

            this.txtInvcAmtCof.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInvcAdjFeeCdNm.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInvcAdjFee.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInvcDocuAmt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInvcCsltReqAmt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInvcTrspExps.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInvcIctvAmt.TextChanged += new System.EventHandler(this.Text_Change);

            txtInvcAmtCof.SetReadOnly(true);
            txtInvcAdjFeeCdNm.SetReadOnly(true);
            txtInvcAdjFee.SetReadOnly(true);
            txtInvcDocuAmt.SetReadOnly(true);
            txtInvcCsltReqAmt.SetReadOnly(true);
            txtInvcTrspExps.SetReadOnly(true);
            txtInvcIctvAmt.SetReadOnly(true);

            _bEvent = true;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            //txtInvcAmtCof.SetReadOnly(rdonly);
            //txtInvcAdjFeeCdNm.SetReadOnly(rdonly);
            //txtInvcAdjFee.SetReadOnly(rdonly);
            //txtInvcDocuAmt.SetReadOnly(rdonly);
            //txtInvcCsltReqAmt.SetReadOnly(rdonly);
            //txtInvcTrspExps.SetReadOnly(rdonly);
            //txtInvcIctvAmt.SetReadOnly(rdonly);
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

        private void Txt_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            if (this.DBLifePanInvoiceResize != null) this.DBLifePanInvoiceResize(this, e);
        }

        private void Text_Change(object sender, EventArgs e)
        {
            Control txt = (Control)sender;
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;
        }

        private void PriceChange(object sender, EventArgs e)
        {
            DevComponents.Editors.DoubleInput amt = (DevComponents.Editors.DoubleInput)sender;
        }

        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            this.Focus();
        }

        public void Clear()
        {
            _bEvent = false;

            txtInvcAmtCof.Text = "";
            txtInvcAdjFeeCdNm.Text = "";
            txtInvcAdjFee.Text = "";
            txtInvcDocuAmt.Text = "";
            txtInvcCsltReqAmt.Text = "";
            txtInvcTrspExps.Text = "";
            txtInvcIctvAmt.Text = "";

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.txtInvcAmtCof.Focus();
        }
    }
}
