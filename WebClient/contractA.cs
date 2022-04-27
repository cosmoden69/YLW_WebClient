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
    public partial class contractA : UserControl, IComparable
    {
        public string InsurPrdt
        {
            get { return txt_no00_01.Text; }
            set { txt_no00_01.Text = value; }
        }
        public string InsurNo
        {
            get { return txt_no00_02.Text; }
            set { txt_no00_02.Text = value; }
        }
        public string CtrtDt
        {
            get { return Utils.DateFormat(dti_no00_01.ValueObject, "yyyyMMdd"); }
            set { dti_no00_01.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string Insurant
        {
            get { return txt_no00_04.Text; }
            set { txt_no00_04.Text = value; }
        }
        public string Insured
        {
            get { return txt_no00_05.Text; }
            set { txt_no00_05.Text = value; }
        }
        public string Bnfc
        {
            get { return txt_no00_06.Text; }
            set { txt_no00_06.Text = value; }
        }
        public string IsrdJob
        {
            get { return txt_no00_07.Text; }
            set { txt_no00_07.Text = value; }
        }
        public string CltrCnts
        {
            get { return txt_no00_08.Text; }
            set { txt_no00_08.Text = value; }
        }
        public object InsurValue
        {
            get { return ini_no00_01.ValueObject; }
            set { ini_no00_01.ValueObject = Utils.ToDecimal(value); }
        }
        public string CtrtExprDt { get; set; }
        public string CtrtStts { get; set; }
        public string CtrtSttsDt { get; set; }
        public string IsrtRegno1 { get; set; }
        public string IsrtRegno2 { get; set; }
        public string IsrtTel { get; set; }
        public string IsrdRegno1 { get; set; }
        public string IsrdRegno2 { get; set; }
        public string IsrdTel { get; set; }
        public string IsrdAddressSeq { get; set; }
        public string IsrdAddressName { get; set; }
        public string IsrdJobGrad { get; set; }
        public string IsrdJobDmnd { get; set; }
        public string IsrdJobGradDmnd { get; set; }
        public string IsrdJobNow { get; set; }
        public string IsrdJobGradNow { get; set; }

        public bool IsNewRow { get; set; } = false;

        private contract _parentC = null;
        private bool readOnlyMode = false;

        public contractA(contract p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txt_no00_01.SetReadOnly(rdonly);
            this.txt_no00_02.SetReadOnly(rdonly);
            this.dti_no00_01.SetReadOnly(rdonly);
            this.txt_no00_04.SetReadOnly(rdonly);
            this.txt_no00_05.SetReadOnly(rdonly);
            this.txt_no00_06.SetReadOnly(rdonly);
            this.txt_no00_07.SetReadOnly(rdonly);
            this.txt_no00_08.SetReadOnly(rdonly);
            this.ini_no00_01.SetReadOnly(rdonly);
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

        private void Button_Click(object sender, EventArgs e)
        {
            if (readOnlyMode) return;
            ButtonX btn = (ButtonX)sender;
            _parentC.RemoveRow(this);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            contractA other = obj as contractA;
            if (other != null)
            {
                return this.CtrtDt.CompareTo(other.CtrtDt);
            }
            return 1;
        }
    }
}
