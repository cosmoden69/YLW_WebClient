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
    public partial class ucMGLossPan8 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string InsurPrdt
        {
            get { return txtInsurPrdt.Text; }
            set { txtInsurPrdt.Text = value; }
        }
        public string AcdtDt
        {
            get
            {
                if (dtAcdtDt.ValueObject == null || dtAcdtDt.ValueObject + "" == "") return "";
                return Utils.DateFormat(dtAcdtDt.ValueObject, "yyyyMMdd");
            }
            set { dtAcdtDt.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string AcdtTm
        {
            get { return txtAcdtTm.Text; }
            set { txtAcdtTm.Text = value; }
        }
        public string Insured
        {
            get { return txtInsured.Text; }
            set { txtInsured.Text = value; }
        }
        public string InsurChrg
        {
            get { return txtInsurChrg.Text; }
            set { txtInsurChrg.Text = value; }
        }
        public string SurvAsgnEmp
        {
            get { return txtSurvAsgnEmp.Text; }
            set { txtSurvAsgnEmp.Text = value; }
        }
        public string LasRptSbmsDt
        {
            get { return txtLasRptSbmsDt.Text.Replace("-", ""); }
            set { txtLasRptSbmsDt.Text = Utils.DateFormat(value, "yyyy-MM-dd"); }
        }
            
        private bool readOnlyMode = false;
        public ucMGLossPan8()
        {
            InitializeComponent();

            this.txtInsurPrdt.TextChanged += new System.EventHandler(this.Text_Change);
            this.dtAcdtDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.txtAcdtTm.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsured.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsurChrg.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtSurvAsgnEmp.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtLasRptSbmsDt.TextChanged += new System.EventHandler(this.Text_Change);

            txtInsurPrdt.SetReadOnly(true);
            this.dtAcdtDt.SetReadOnly(true);
            txtAcdtTm.SetReadOnly(true);
            txtInsured.SetReadOnly(true);
            txtInsurChrg.SetReadOnly(true);
            txtSurvAsgnEmp.SetReadOnly(true);
            txtLasRptSbmsDt.SetReadOnly(true);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            //txtInsurPrdt.SetReadOnly(rdonly);
            //this.dtAcdtDt.SetReadOnly(rdonly);
            //txtAcdtTm.SetReadOnly(rdonly);
            //txtInsured.SetReadOnly(rdonly);
            //txtInsurChrg.SetReadOnly(rdonly);
            //txtSurvAsgnEmp.SetReadOnly(rdonly);
            //txtLasRptSbmsDt.SetReadOnly(rdonly);
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

        public void Clear()
        {
            txtInsurPrdt.Text = "";
            this.dtAcdtDt.ValueObject = null;
            txtAcdtTm.Text = "";
            txtInsured.Text = "";
            txtInsurChrg.Text = "";
            txtSurvAsgnEmp.Text = "";
            txtLasRptSbmsDt.Text = "";
        }

        public void SetFocus()
        {
            this.txtInsurPrdt.Focus();
        }

        private void Text_Change(object sender, EventArgs e)
        {
            Control txt = (Control)sender;
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;
        }
    }
}
