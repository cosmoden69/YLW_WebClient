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
    public partial class HyndContractA : UserControl, IComparable
    {
        public event MouseEventHandler ContentsMouseWheel;

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
        public string Insurant
        {
            get { return txt_no00_03.Text; }
            set { txt_no00_03.Text = value; }
        }
        public string Insured
        {
            get { return txt_no00_04.Text; }
            set { txt_no00_04.Text = value; }
        }
        public string Bnfc
        {
            get { return txt_no00_05.Text; }
            set { txt_no00_05.Text = value; }
        }
        public string CtrtDt
        {
            get { return Utils.DateFormat(dti_no00_01.ValueObject, "yyyyMMdd"); }
            set { dti_no00_01.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string CtrtExprDt
        {
            get { return Utils.DateFormat(dti_no00_02.ValueObject, "yyyyMMdd"); }
            set { dti_no00_02.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string IsrdJob
        {
            get { return txt_no00_06.Text; }
            set { txt_no00_06.Text = value; }
        }
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

        private HyndContract _parentC = null;
        private bool readOnlyMode = false;
        private bool _bEvent = false;

        public HyndContractA(HyndContract p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();

            this.txt_no00_01.TextChanged += new System.EventHandler(this.Text_Change);
            this.txt_no00_02.TextChanged += new System.EventHandler(this.Text_Change);
            this.txt_no00_03.TextChanged += new System.EventHandler(this.Text_Change);
            this.txt_no00_04.TextChanged += new System.EventHandler(this.Text_Change);
            this.txt_no00_05.TextChanged += new System.EventHandler(this.Text_Change);
            this.dti_no00_01.ValueChanged += new System.EventHandler(this.Date_Change);
            this.dti_no00_02.ValueChanged += new System.EventHandler(this.Date_Change);
            this.txt_no00_06.TextChanged += new System.EventHandler(this.Text_Change);
            this.btn_no00_01.Click += new System.EventHandler(this.Button_Click);
            this.Load += HyndContractA_Load;

            _bEvent = true;
        }

        private void HyndContractA_Load(object sender, EventArgs e)
        {
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txt_no00_01.SetReadOnly(rdonly);
            this.txt_no00_02.SetReadOnly(rdonly);
            this.txt_no00_03.SetReadOnly(rdonly);
            this.txt_no00_04.SetReadOnly(rdonly);
            this.txt_no00_05.SetReadOnly(rdonly);
            this.dti_no00_01.IsInputReadOnly = rdonly;
            this.dti_no00_02.IsInputReadOnly = rdonly;
            this.txt_no00_05.SetReadOnly(rdonly);
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
            HyndContractA other = obj as HyndContractA;
            if (other != null)
            {
                return this.CtrtDt.CompareTo(other.CtrtDt);
            }
            return 1;
        }
    }
}
