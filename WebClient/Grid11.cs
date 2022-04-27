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
    public partial class Grid11 : UserControl
    {
        public string AcdtNo
        {
            get { return txtAcdtNo.Text; }
            set { txtAcdtNo.Text = value; }
        }
        public string AcdtExamSerl
        {
            get { return txtAcdtExamSerl.Text; }
            set { txtAcdtExamSerl.Text = value; }
        }
        public string AcdtPsnName
        {
            get { return txtAcdtPsnName.Text; }
            set { txtAcdtPsnName.Text = value; }
        }
        public string Regno
        {
            get { return txtRegno.Text; }
            set { txtRegno.Text = value; }
        }
        public string InsurChrg
        {
            get { return txtInsurChrg.Text; }
            set { txtInsurChrg.Text = value; }
        }
        public string SurvAcptDt
        {
            get { return txtSurvAcptDt.Text.Replace("-", ""); }
            set { txtSurvAcptDt.Text = Utils.DateFormat(value, "yyyy-MM-dd"); }
        }
        public string SurvReqDt
        {
            get { return txtSurvReqDt.Text.Replace("-", ""); }
            set { txtSurvReqDt.Text = Utils.DateFormat(value, "yyyy-MM-dd"); }
        }
        public string DelayRprtDt
        {
            get { return txtDelayRprtDt.Text.Replace("-", ""); }
            set { txtDelayRprtDt.Text = Utils.DateFormat(value, "yyyy-MM-dd"); }
        }
        public string EndDate
        {
            get
            {
                if (dtiEndDate.ValueObject == null || dtiEndDate.ValueObject + "" == "") return "";
                return Utils.DateFormat(dtiEndDate.ValueObject, "yyyyMMdd");
            }
            set { dtiEndDate.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string SolarWDays
        {
            get { return txtSolarWDays.Text; }
            set { txtSolarWDays.Text = value; }
        }
        public string SurvComp
        {
            get { return txtSurvComp.Text; }
            set { txtSurvComp.Text = value; }
        }
        public string SurvAsgnTeamLeadName
        {
            get { return txtSurvAsgnTeamLeadName.Text; }
            set { txtSurvAsgnTeamLeadName.Text = value; }
        }
        public string SurvAsgnTeamLeadOP
        {
            get { return txtSurvAsgnTeamLeadOP.Text.Replace("-", ""); }
            set { txtSurvAsgnTeamLeadOP.Text = Utils.TelNumber(value); }
        }
        public string SurvAsgnEmpName
        {
            get { return txtSurvAsgnEmpName.Text; }
            set { txtSurvAsgnEmpName.Text = value; }
        }
        public string SurvAsgnEmpOP
        {
            get { return txtSurvAsgnEmpOP.Text.Replace("-", ""); }
            set { txtSurvAsgnEmpOP.Text = Utils.TelNumber(value); }
        }
        public int edi_id { get; set; } = 0;
        public int parent_id { get; set; } = 0;
        public string bis_code { get; } = "27";

        private bool readOnlyMode = false;

        public Grid11()
        {
            InitializeComponent();

            this.txtAcdtNo.SetReadOnly(true);
            this.txtAcdtExamSerl.SetReadOnly(true);
            this.txtAcdtPsnName.SetReadOnly(true);
            this.txtRegno.SetReadOnly(true);
            this.txtInsurChrg.SetReadOnly(true);
            this.txtSurvAcptDt.SetReadOnly(true);
            this.txtSurvReqDt.SetReadOnly(true);
            this.txtDelayRprtDt.SetReadOnly(true);
            this.txtSolarWDays.SetReadOnly(true);
            this.txtSurvComp.SetReadOnly(true);
            this.txtSurvAsgnTeamLeadName.SetReadOnly(true);
            this.txtSurvAsgnTeamLeadOP.SetReadOnly(true);
            this.txtSurvAsgnEmpName.SetReadOnly(true);
            this.txtSurvAsgnEmpOP.SetReadOnly(true);
        }

        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            this.Focus();
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.dtiEndDate.SetReadOnly(rdonly);
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

        public void Clear()
        {
            this.txtAcdtNo.Text = "";
            this.txtAcdtExamSerl.Text = "";
            this.txtAcdtPsnName.Text = "";
            this.txtRegno.Text = "";
            this.txtInsurChrg.Text = "";
            this.txtSurvAcptDt.Text = "";
            this.txtSurvReqDt.Text = "";
            this.txtDelayRprtDt.Text = "";
            this.dtiEndDate.ValueObject = null;
            this.txtSolarWDays.Text = "";
            this.txtSurvComp.Text = "";
            this.txtSurvAsgnTeamLeadName.Text = "";
            this.txtSurvAsgnTeamLeadOP.Text = "";
            this.txtSurvAsgnEmpName.Text = "";
            this.txtSurvAsgnEmpOP.Text = "";
            this.edi_id = 0;
            this.parent_id = 0;
        }
    }
}
