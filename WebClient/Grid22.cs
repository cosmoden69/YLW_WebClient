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
    public partial class Grid22 : UserControl
    {
        public delegate void AcdtSurvDtlCodeChangeEventHandler(string defaultFee, string TransFee);
        public event AcdtSurvDtlCodeChangeEventHandler AcdtSurvDtlCodeChanged;
        public event MouseEventHandler ContentsMouseWheel;

        public string OurAcdtSurvDtlCode
        {
            get
            {
                return Utils.GetComboSelectedValue(cbOurAcdtSurvDtlCode, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbOurAcdtSurvDtlCode, value, "MinorSeq");
            }
        }
        public string CompAcdtSurvDtlCode
        {
            get
            {
                return Utils.GetP(Utils.GetComboSelectedValue(cbCompAcdtSurvDtlCode, "MinorSeq"), "-", 1);
            }
            set { }
        }
        public string CompAcdtSurvDtlCode2
        {
            get
            {
                return Utils.GetComboSelectedValue(cbCompAcdtSurvDtlCode, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbCompAcdtSurvDtlCode, value, "MinorSeq");
            }
        }
        public string AccuseYn
        {
            get
            {
                return Utils.GetComboSelectedValue(cbAccuseYn, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbAccuseYn, value, "MinorSeq");
            }
        }
        public string SurvDtlChgRsn
        {
            get { return txtSurvDtlChgRsn.rtbDoc.Text; }
            set { txtSurvDtlChgRsn.rtbDoc.Text = value; }
        }
        public int id { get; set; } = 0;

        private bool readOnlyMode = false;
        private bool _bEvent = false;
        private DataTable USERCD_006 = null;

        public Grid22()
        {
            InitializeComponent();

            this.txtSurvDtlChgRsn.MaxInputLength = 200;

            this.txtSurvDtlChgRsn.ContentsResized += TxtSurvDtlChgRsn_ContentsResized;
            this.txtSurvDtlChgRsn.ContentsMouseWheel += TxtSurvDtlChgRsn_ContentsMouseWheel;
            this.cbOurAcdtSurvDtlCode.MouseWheel += Combo_MouseWheel;
            this.cbCompAcdtSurvDtlCode.MouseWheel += Combo_MouseWheel;
            this.cbAccuseYn.MouseWheel += Combo_MouseWheel;
            this.cbCompAcdtSurvDtlCode.SelectedIndexChanged += CbCompAcdtSurvDtlCode_SelectedIndexChanged;

            this.cbOurAcdtSurvDtlCode.Enabled = false;

            _bEvent = true;
        }

        private void Combo_MouseWheel(object sender, MouseEventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo != null && cbo.DroppedDown) return;
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
            ((HandledMouseEventArgs)e).Handled = true;
        }

        public void Init_Set(DataSet ds)
        {
            try
            {
                _bEvent = false;

                USERCD_006 = ds.Tables["USERCD_006"].Copy();
                Utils.SetCombo(cbOurAcdtSurvDtlCode, ds.Tables["CD_AI00558"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbCompAcdtSurvDtlCode, ds.Tables["USERCD_006"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbAccuseYn, ds.Tables["YESNO_CD"].Copy(), "MinorSeq", "MinorName", true);

                _bEvent = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            //this.cbOurAcdtSurvDtlCode.SetReadOnly(rdonly);
            this.cbCompAcdtSurvDtlCode.SetReadOnly(rdonly);
            this.cbAccuseYn.SetReadOnly(rdonly);
            this.txtSurvDtlChgRsn.rtbDoc.ReadOnly = rdonly;
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

        private void TxtSurvDtlChgRsn_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void CbCompAcdtSurvDtlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bEvent) return;
            string cd = Utils.GetComboSelectedValue(cbCompAcdtSurvDtlCode, "MinorSeq");
            DataRow[] drs = USERCD_006.Select("MinorSeq = '" + cd + "' ");
            if (drs == null || drs.Length != 1) return;
            string fee = Utils.ConvertToString(drs[0]["value_remark"]);
            string fee1 = Utils.GetP(Utils.GetP(fee, "/", 1), ":", 2).Replace(",", "");  //기본보수
            string fee2 = Utils.GetP(Utils.GetP(fee, "/", 2), ":", 2).Replace(",", "");  //교통비
            if (this.AcdtSurvDtlCodeChanged != null) this.AcdtSurvDtlCodeChanged(fee1.Replace("원", ""), fee2.Replace("원", ""));
        }

        private void TxtSurvDtlChgRsn_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.Height = 72 + e.NewRectangle.Height;
            this.panelEx41.Height = e.NewRectangle.Height + 2;
        }

        public void Clear()
        {
            _bEvent = false;

            this.cbOurAcdtSurvDtlCode.Text = "";
            if (cbOurAcdtSurvDtlCode.Items.Count > 0) cbOurAcdtSurvDtlCode.SelectedIndex = 0;
            this.cbCompAcdtSurvDtlCode.Text = "";
            if (cbCompAcdtSurvDtlCode.Items.Count > 0) cbCompAcdtSurvDtlCode.SelectedIndex = 0;
            this.cbAccuseYn.Text = "";
            if (cbAccuseYn.Items.Count > 0) cbAccuseYn.SelectedIndex = 0;
            this.txtSurvDtlChgRsn.rtbDoc.Text = "";
            this.id = 0;

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.Focus();
            this.cbCompAcdtSurvDtlCode.Focus();
        }
    }
}
