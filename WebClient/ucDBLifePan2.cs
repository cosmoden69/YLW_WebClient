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
    public partial class ucDBLifePan2 : UserControl
    {
        public delegate void DBLfResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler ContentsMouseWheel;
        public event DBLfResizeEventHandler DBLifePan2Resize;

        public string SurvAsgnEmpName
        {
            get { return txtSurvAsgnEmpName.Text; }
            set { txtSurvAsgnEmpName.Text = value; }
        }
        public string InsurChrg
        {
            get { return txtInsurChrg.Text; }
            set { txtInsurChrg.Text = value; }
        }
        public string SurvReqDt
        {
            get { return txtSurvReqDt.Text.Replace("-", ""); }
            set { txtSurvReqDt.Text = Utils.DateFormat(value, "yyyy-MM-dd"); }
        }
        public string AcptDt
        {
            get { return txtAcptDt.Text.Replace("-", ""); }
            set { txtAcptDt.Text = Utils.DateFormat(value, "yyyy-MM-dd"); }
        }
        public string CclsDt
        {
            get { return txtCclsDt.Text.Replace("-", ""); }
            set { txtCclsDt.Text = Utils.DateFormat(value, "yyyy-MM-dd"); }
        }
        public string LasRptSbmsDt
        {
            get { return txtLasRptSbmsDt.Text.Replace("-", ""); }
            set { txtLasRptSbmsDt.Text = Utils.DateFormat(value, "yyyy-MM-dd"); }
        }
        public string PayDt
        {
            get { return txtPayDt.Text.Replace("-", ""); }
            set { txtPayDt.Text = Utils.DateFormat(value, "yyyy-MM-dd"); }
        }
        public string CmplExptFg
        {
            get
            {
                if (chkCmplExptFgY.Checked) return "1";
                else if (chkCmplExptFgN.Checked) return "2";
                else return "";
            }
            set
            {
                if (value == "1") chkCmplExptFgY.Checked = true;
                else if (value == "2") chkCmplExptFgN.Checked = true;
            }
        }

        private bool _bEvent = false;

        private bool readOnlyMode = false;
        public ucDBLifePan2()
        {
            InitializeComponent();

            this.txtSurvAsgnEmpName.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsurChrg.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtSurvReqDt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtAcptDt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtCclsDt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtLasRptSbmsDt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtPayDt.TextChanged += new System.EventHandler(this.Text_Change);
            this.chkCmplExptFgY.Click += ChkCmplExptFgY_Click;
            this.chkCmplExptFgN.Click += ChkCmplExptFgN_Click;

            txtSurvAsgnEmpName.SetReadOnly(true);
            txtInsurChrg.SetReadOnly(true);
            txtSurvReqDt.SetReadOnly(true);
            txtAcptDt.SetReadOnly(true);
            txtCclsDt.SetReadOnly(true);
            txtLasRptSbmsDt.SetReadOnly(true);
            txtPayDt.SetReadOnly(true);

            _bEvent = true;
        }

        private void ChkCmplExptFgY_Click(object sender, EventArgs e)
        {
            if (chkCmplExptFgY.Checked) chkCmplExptFgN.Checked = false;
        }

        private void ChkCmplExptFgN_Click(object sender, EventArgs e)
        {
            if (chkCmplExptFgN.Checked) chkCmplExptFgY.Checked = false;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            //txtSurvAsgnEmpName.SetReadOnly(rdonly);
            //txtInsurChrg.SetReadOnly(rdonly);
            //txtSurvReqDt.SetReadOnly(rdonly);
            //txtAcptDt.SetReadOnly(rdonly);
            //txtCclsDt.SetReadOnly(rdonly);
            //txtLasRptSbmsDt.SetReadOnly(rdonly);
            //txtPayDt.SetReadOnly(rdonly);
            chkCmplExptFgY.Enabled = !rdonly;
            chkCmplExptFgN.Enabled = !rdonly;
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
            if (this.DBLifePan2Resize != null) this.DBLifePan2Resize(this, e);
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

            txtSurvAsgnEmpName.Text = "";
            txtInsurChrg.Text = "";
            txtSurvReqDt.Text = "";
            txtAcptDt.Text = "";
            txtCclsDt.Text = "";
            txtLasRptSbmsDt.Text = "";
            txtPayDt.Text = "";
            chkCmplExptFgY.Checked = false;
            chkCmplExptFgN.Checked = false;

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.txtAcptDt.Focus();
        }
    }
}
