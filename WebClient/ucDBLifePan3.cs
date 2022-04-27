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
    public partial class ucDBLifePan3 : UserControl
    {
        public delegate void DBLfResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler ContentsMouseWheel;
        public event DBLfResizeEventHandler DBLifePan3Resize;

        public string SurvAsgnEmpName
        {
            get { return txtSurvAsgnEmpName.Text; }
            set { txtSurvAsgnEmpName.Text = value; }
        }
        public string SurvAsgnEmpHP
        {
            get { return txtSurvAsgnEmpHP.Text.Replace("-", ""); }
            set { txtSurvAsgnEmpHP.Text = Utils.TelNumber(value); }
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
        public string ChrgAdjuster
        {
            get { return txtChrgAdjuster.Text; }
            set { txtChrgAdjuster.Text = value; }
        }
        public string SurvAsgnTeamName
        {
            get { return txtSurvAsgnTeamName.Text; }
            set { txtSurvAsgnTeamName.Text = value; }
        }
        public string LeadAdjuster
        {
            get { return txtLeadAdjuster.Text; }
            set { txtLeadAdjuster.Text = value; }
        }
        public string SurvAsgnEmpRank
        {
            get { return txtSurvAsgnEmpRank.Text; }
            set { txtSurvAsgnEmpRank.Text = value; }
        }

        private bool _bEvent = false;

        private bool readOnlyMode = false;
        public ucDBLifePan3()
        {
            InitializeComponent();

            this.txtSurvAsgnEmpName.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtSurvAsgnEmpHP.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtSurvAsgnTeamLeadName.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtSurvAsgnTeamLeadOP.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtChrgAdjuster.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtSurvAsgnTeamName.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtLeadAdjuster.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtSurvAsgnEmpRank.TextChanged += new System.EventHandler(this.Text_Change);

            txtSurvAsgnEmpName.SetReadOnly(true);
            txtSurvAsgnEmpHP.SetReadOnly(true);
            txtSurvAsgnTeamLeadName.SetReadOnly(true);
            txtSurvAsgnTeamLeadOP.SetReadOnly(true);
            txtChrgAdjuster.SetReadOnly(true);
            txtSurvAsgnTeamName.SetReadOnly(true);
            txtLeadAdjuster.SetReadOnly(true);
            txtSurvAsgnEmpRank.SetReadOnly(true);

            _bEvent = true;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            //txtSurvAsgnEmpName.SetReadOnly(rdonly);
            //txtSurvAsgnEmpHP.SetReadOnly(rdonly);
            //txtSurvAsgnTeamLeadName.SetReadOnly(rdonly);
            //txtSurvAsgnTeamLeadOP.SetReadOnly(rdonly);
            //txtChrgAdjuster.SetReadOnly(rdonly);
            //txtSurvAsgnTeamName.SetReadOnly(rdonly);
            //txtLeadAdjuster.SetReadOnly(rdonly);
            //txtSurvAsgnEmpRank.SetReadOnly(rdonly);
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
            if (this.DBLifePan3Resize != null) this.DBLifePan3Resize(this, e);
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
            txtSurvAsgnEmpHP.Text = "";
            txtSurvAsgnTeamLeadName.Text = "";
            txtSurvAsgnTeamLeadOP.Text = "";
            txtChrgAdjuster.Text = "";
            txtSurvAsgnTeamName.Text = "";
            txtLeadAdjuster.Text = "";
            txtSurvAsgnEmpRank.Text = "";

        _bEvent = true;
        }

        public void SetFocus()
        {
            this.txtSurvAsgnEmpName.Focus();
        }
    }
}
