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
    public partial class ucDBLossPan1 : UserControl
    {
        public string Insured
        {
            get { return txtInsured.Text; }
            set { txtInsured.Text = value; }
        }
        public string AcdtNo
        {
            get { return txtAcdtNo.Text; }
            set { txtAcdtNo.Text = value; }
        }
        public string SurvAsgnEmpName
        {
            get { return txtSurvAsgnEmpName.Text; }
            set { txtSurvAsgnEmpName.Text = value; }
        }
        public string IsrdRegno
        {
            get { return txtIsrdRegno.Text; }
            set { txtIsrdRegno.Text = value; }
        }
        public string InsurChrg
        {
            get { return txtInsurChrg.Text; }
            set { txtInsurChrg.Text = value; }
        }

        private bool readOnlyMode = false;
        public ucDBLossPan1()
        {
            InitializeComponent();

            this.txtInsured.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtAcdtNo.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtSurvAsgnEmpName.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdRegno.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsurChrg.TextChanged += new System.EventHandler(this.Text_Change);

            txtInsured.SetReadOnly(true);
            txtAcdtNo.SetReadOnly(true);
            txtSurvAsgnEmpName.SetReadOnly(true);
            txtIsrdRegno.SetReadOnly(true);
            txtInsurChrg.SetReadOnly(true);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            //txtInsured.SetReadOnly(rdonly);
            //txtAcdtNo.SetReadOnly(rdonly);
            //txtSurvAsgnEmpName.SetReadOnly(rdonly);
            //txtIsrdRegno.SetReadOnly(rdonly);
            //txtInsurChrg.SetReadOnly(rdonly);
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
            txtInsured.Text = "";
            txtAcdtNo.Text = "";
            txtSurvAsgnEmpName.Text = "";
            txtIsrdRegno.Text = "";
            txtInsurChrg.Text = "";
        }

        public void SetFocus()
        {
            this.txtInsured.Focus();
        }

        private void Text_Change(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;
        }
    }
}
