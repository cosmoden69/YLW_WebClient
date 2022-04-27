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
    public partial class Grid42 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string TermsRelayCode
        {
            get
            {
                return Utils.GetComboSelectedValue(cbTermsRelayCode, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbTermsRelayCode, value, "MinorSeq");
            }
        }
        public string TermsRelayCnts
        {
            get { return txtTermsRelayCnts.Text; }
            set { txtTermsRelayCnts.Text = value; }
        }
        public string HandSignCode
        {
            get
            {
                return Utils.GetComboSelectedValue(cbHandSignCode, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbHandSignCode, value, "MinorSeq");
            }
        }
        public string HandSignCnts
        {
            get { return txtHandSignCnts.Text; }
            set { txtHandSignCnts.Text = value; }
        }
        public string ExTermsExplnCode
        {
            get
            {
                return Utils.GetComboSelectedValue(cbExTermsExplnCode, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbExTermsExplnCode, value, "MinorSeq");
            }
        }
        public string ExTermsExplnCnts
        {
            get { return txtExTermsExplnCnts.Text; }
            set { txtExTermsExplnCnts.Text = value; }
        }
        public string ExTermsAplyCode
        {
            get
            {
                return Utils.GetComboSelectedValue(cbExTermsAplyCode, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbExTermsAplyCode, value, "MinorSeq");
            }
        }
        public string ExTermsAplyCnts
        {
            get { return txtExTermsAplyCnts.Text; }
            set { txtExTermsAplyCnts.Text = value; }
        }
        public string ReqDocuAdeqCode
        {
            get
            {
                return Utils.GetComboSelectedValue(cbReqDocuAdeqCode, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbReqDocuAdeqCode, value, "MinorSeq");
            }
        }
        public string ReqDocuAdeqCnts
        {
            get { return txtReqDocuAdeqCnts.Text; }
            set { txtReqDocuAdeqCnts.Text = value; }
        }
        public string ExTermsUndstdLvlCode
        {
            get
            {
                return Utils.GetComboSelectedValue(cbExTermsUndstdLvlCode, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbExTermsUndstdLvlCode, value, "MinorSeq");
            }
        }
        public string ExTermsUndstdLvlCnts
        {
            get { return txtExTermsUndstdLvlCnts.Text; }
            set { txtExTermsUndstdLvlCnts.Text = value; }
        }
        public string WriterDisadvantageYn
        {
            get
            {
                return Utils.GetComboSelectedValue(cbWriterDisadvantageYn, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbWriterDisadvantageYn, value, "MinorSeq");
            }
        }
        public string WriterDisadvantageCnts
        {
            get { return txtWriterDisadvantageCnts.Text; }
            set { txtWriterDisadvantageCnts.Text = value; }
        }
        public string CaseLawAdjReviewYn
        {
            get
            {
                return Utils.GetComboSelectedValue(cbCaseLawAdjReviewYn, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbCaseLawAdjReviewYn, value, "MinorSeq");
            }
        }
        public string CaseLawAdjReviewCnts
        {
            get { return txtCaseLawAdjReviewCnts.Text; }
            set { txtCaseLawAdjReviewCnts.Text = value; }
        }
        public string ReviewPossibleYn
        {
            get
            {
                return Utils.GetComboSelectedValue(cbReviewPossibleYn, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbReviewPossibleYn, value, "MinorSeq");
            }
        }
        public string ReviewPossibleCnts
        {
            get { return txtReviewPossibleCnts.Text; }
            set { txtReviewPossibleCnts.Text = value; }
        }
        public string ExTermsTtlOpinionCode
        {
            get
            {
                return Utils.GetComboSelectedValue(cbExTermsTtlOpinionCode, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbExTermsTtlOpinionCode, value, "MinorSeq");
            }
        }
        public string ExTermsTtlOpinionCnts
        {
            get { return txtExTermsTtlOpinionCnts.Text; }
            set { txtExTermsTtlOpinionCnts.Text = value; }
        }
        public int id { get; set; } = 0;

        private bool readOnlyMode = false;

        public Grid42()
        {
            InitializeComponent();

            this.cbTermsRelayCode.MouseWheel += Combo_MouseWheel;
            this.cbHandSignCode.MouseWheel += Combo_MouseWheel;
            this.cbExTermsExplnCode.MouseWheel += Combo_MouseWheel;
            this.cbExTermsAplyCode.MouseWheel += Combo_MouseWheel;
            this.cbReqDocuAdeqCode.MouseWheel += Combo_MouseWheel;
            this.cbExTermsUndstdLvlCode.MouseWheel += Combo_MouseWheel;
            this.cbWriterDisadvantageYn.MouseWheel += Combo_MouseWheel;
            this.cbCaseLawAdjReviewYn.MouseWheel += Combo_MouseWheel;
            this.cbReviewPossibleYn.MouseWheel += Combo_MouseWheel;
            this.cbExTermsTtlOpinionCode.MouseWheel += Combo_MouseWheel;
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
                Utils.SetCombo(cbTermsRelayCode, ds.Tables["CD_AI00591"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbHandSignCode, ds.Tables["CD_AI00593"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbExTermsExplnCode, ds.Tables["CD_AI00589"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbExTermsAplyCode, ds.Tables["CD_AI00590"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbReqDocuAdeqCode, ds.Tables["CD_AI00594"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbExTermsUndstdLvlCode, ds.Tables["CD_AI00595"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbWriterDisadvantageYn, ds.Tables["YESNO_CD"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbCaseLawAdjReviewYn, ds.Tables["YESNO_CD"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbReviewPossibleYn, ds.Tables["YESNO_CD"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbExTermsTtlOpinionCode, ds.Tables["USERCD_001"].Copy(), "MinorSeq", "MinorName", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.cbTermsRelayCode.SetReadOnly(rdonly);
            this.txtTermsRelayCnts.SetReadOnly(rdonly);
            this.cbHandSignCode.SetReadOnly(rdonly);
            this.txtHandSignCnts.SetReadOnly(rdonly);
            this.cbExTermsExplnCode.SetReadOnly(rdonly);
            this.txtExTermsExplnCnts.SetReadOnly(rdonly);
            this.cbExTermsAplyCode.SetReadOnly(rdonly);
            this.txtExTermsAplyCnts.SetReadOnly(rdonly);
            this.cbReqDocuAdeqCode.SetReadOnly(rdonly);
            this.txtReqDocuAdeqCnts.SetReadOnly(rdonly);
            this.cbExTermsUndstdLvlCode.SetReadOnly(rdonly);
            this.txtExTermsUndstdLvlCnts.SetReadOnly(rdonly);
            this.cbWriterDisadvantageYn.SetReadOnly(rdonly);
            this.txtWriterDisadvantageCnts.SetReadOnly(rdonly);
            this.cbCaseLawAdjReviewYn.SetReadOnly(rdonly);
            this.txtCaseLawAdjReviewCnts.SetReadOnly(rdonly);
            this.cbReviewPossibleYn.SetReadOnly(rdonly);
            this.txtReviewPossibleCnts.SetReadOnly(rdonly);
            this.cbExTermsTtlOpinionCode.SetReadOnly(rdonly);
            this.txtExTermsTtlOpinionCnts.SetReadOnly(rdonly);
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
            this.cbTermsRelayCode.Text = "";
            if (cbTermsRelayCode.Items.Count > 0) cbTermsRelayCode.SelectedIndex = 0;
            this.txtTermsRelayCnts.Text = "";
            this.cbHandSignCode.Text = "";
            if (cbHandSignCode.Items.Count > 0) cbHandSignCode.SelectedIndex = 0;
            this.txtHandSignCnts.Text = "";
            this.cbExTermsExplnCode.Text = "";
            if (cbExTermsExplnCode.Items.Count > 0) cbExTermsExplnCode.SelectedIndex = 0;
            this.txtExTermsExplnCnts.Text = "";
            this.cbExTermsAplyCode.Text = "";
            if (cbExTermsAplyCode.Items.Count > 0) cbExTermsAplyCode.SelectedIndex = 0;
            this.txtExTermsAplyCnts.Text = "";
            this.cbReqDocuAdeqCode.Text = "";
            if (cbReqDocuAdeqCode.Items.Count > 0) cbReqDocuAdeqCode.SelectedIndex = 0;
            this.txtReqDocuAdeqCnts.Text = "";
            this.cbExTermsUndstdLvlCode.Text = "";
            if (cbExTermsUndstdLvlCode.Items.Count > 0) cbExTermsUndstdLvlCode.SelectedIndex = 0;
            this.txtExTermsUndstdLvlCnts.Text = "";
            this.cbWriterDisadvantageYn.Text = "";
            if (cbWriterDisadvantageYn.Items.Count > 0) cbWriterDisadvantageYn.SelectedIndex = 0;
            this.txtWriterDisadvantageCnts.Text = "";
            this.cbCaseLawAdjReviewYn.Text = "";
            if (cbCaseLawAdjReviewYn.Items.Count > 0) cbCaseLawAdjReviewYn.SelectedIndex = 0;
            this.txtCaseLawAdjReviewCnts.Text = "";
            this.cbReviewPossibleYn.Text = "";
            if (cbReviewPossibleYn.Items.Count > 0) cbReviewPossibleYn.SelectedIndex = 0;
            this.txtReviewPossibleCnts.Text = "";
            this.cbExTermsTtlOpinionCode.Text = "";
            if (cbExTermsTtlOpinionCode.Items.Count > 0) cbExTermsTtlOpinionCode.SelectedIndex = 0;
            this.txtExTermsTtlOpinionCnts.Text = "";
            this.id = 0;
        }

        public void SetFocus()
        {
            this.Focus();
            this.txtTermsRelayCnts.Focus();
        }
    }
}
