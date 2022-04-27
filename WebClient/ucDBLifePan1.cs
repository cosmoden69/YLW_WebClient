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
    public partial class ucDBLifePan1 : UserControl
    {
        public delegate void DBLfResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler ContentsMouseWheel;
        public event DBLfResizeEventHandler DBLifePan1Resize;

        public string AcptDt
        {
            get { return txtAcptDt.Text.Replace("-", ""); }
            set { txtAcptDt.Text = Utils.DateFormat(value, "yyyy-MM-dd"); }
        }
        public string AcptMgmtNo
        {
            get { return txtAcptMgmtNo.Text; }
            set { txtAcptMgmtNo.Text = value; }
        }
        public string InsurPrdt
        {
            get { return txtInsurPrdt.Text; }
            set
            {
                txtInsurPrdt.Text = value;
                //if (txtInsurPrdt.Text != "") txtInsurPrdt.SetReadOnly(true);
                //else txtInsurPrdt.SetReadOnly(false);
            }
        }
        public string InsurNo
        {
            get { return txtInsurNo.Text; }
            set
            {
                txtInsurNo.Text = value;
                //if (txtInsurNo.Text != "") txtInsurNo.SetReadOnly(true);
                //else txtInsurNo.SetReadOnly(false);
            }
        }
        public string InsurNoOld
        {
            get;
            set;
        }
        public string Insured
        {
            get { return txtInsured.Text; }
            set { txtInsured.Text = value; }
        }
        public string IsrdRegno1
        {
            get { return txtIsrdRegno1.Text; }
            set { txtIsrdRegno1.Text = value; }
        }
        public string IsrdRegno2
        {
            get;
            set;
        }
        public string IsrdJob
        {
            get { return txtIsrdJob.Text; }
            set { txtIsrdJob.Text = value; }
        }
        public string IsrdJobDmnd
        {
            get { return txtIsrdJobDmnd.Text; }
            set { txtIsrdJobDmnd.Text = value; }
        }
        public string IsrdTel
        {
            get { return txtIsrdTel.Text; }
            set { txtIsrdTel.Text = Utils.TelNumber(value); }
        }
        public string LongCnts1
        {
            get { return txtLongCnts1.Text; }
            set { txtLongCnts1.Text = value; }
        }
        public string IsrdAddressName
        {
            get { return txtIsrdAddressName.Text; }
            set { txtIsrdAddressName.Text = value; }
        }
        public string IsrdAddressSeq
        {
            get { return Utils.ConvertToString(txtIsrdAddressName.AddressSeq); }
            set { txtIsrdAddressName.AddressSeq = Utils.ToInt(value); }
        }

        private bool _bEvent = false;

        private bool readOnlyMode = false;
        public ucDBLifePan1()
        {
            InitializeComponent();

            this.txtAcptDt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtAcptMgmtNo.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsurPrdt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsurNo.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsured.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdRegno1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdJob.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdJobDmnd.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdTel.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtLongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdAddressName.TextChanged += new System.EventHandler(this.Text_Change);

            txtAcptDt.SetReadOnly(true);
            txtAcptMgmtNo.SetReadOnly(true);

            _bEvent = true;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            //txtAcptDt.SetReadOnly(rdonly);
            //txtAcptMgmtNo.SetReadOnly(rdonly);
            txtInsurPrdt.SetReadOnly(rdonly);
            txtInsurNo.SetReadOnly(rdonly);
            txtInsured.SetReadOnly(rdonly);
            txtIsrdRegno1.SetReadOnly(rdonly);
            txtIsrdJob.SetReadOnly(rdonly);
            txtIsrdJobDmnd.SetReadOnly(rdonly);
            txtIsrdTel.SetReadOnly(rdonly);
            txtLongCnts1.SetReadOnly(rdonly);
            txtIsrdAddressName.SetReadOnly(rdonly);
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
            if (this.DBLifePan1Resize != null) this.DBLifePan1Resize(this, e);
        }

        private void Text_Change(object sender, EventArgs e)
        {
            System.Windows.Forms.Control txt = (System.Windows.Forms.Control)sender;
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

            txtAcptDt.Text = "";
            txtAcptMgmtNo.Text = "";
            txtInsurPrdt.Text = "";
            txtInsurNo.Text = "";
            txtInsured.Text = "";
            txtIsrdRegno1.Text = "";
            txtIsrdJob.Text = "";
            txtIsrdJobDmnd.Text = "";
            txtIsrdTel.Text = "";
            txtLongCnts1.Text = "";
            txtIsrdAddressName.Text = "";

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.txtAcptDt.Focus();
        }
    }
}
