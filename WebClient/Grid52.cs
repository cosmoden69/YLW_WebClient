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
    public partial class Grid52 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string AcdtSurvDtlCode
        {
            get
            {
                return Utils.GetComboSelectedValue(cbAcdtSurvDtlCode, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbAcdtSurvDtlCode, value, "MinorSeq");
            }
        }
        public object AcdtSurvVstCnt
        {
            get { return iniAcdtSurvVstCnt.ValueObject; }
            set { iniAcdtSurvVstCnt.ValueObject = Utils.ToDecimal(value); }
        }
        public object AcdtSurvPassDay
        {
            get { return iniAcdtSurvPassDay.ValueObject; }
            set { iniAcdtSurvPassDay.ValueObject = Utils.ToDecimal(value); }
        }
        public object DefaultCost
        {
            get { return iniDefaultCost.ValueObject; }
            set { iniDefaultCost.ValueObject = Utils.ToDecimal(value); }
        }
        public object TransCost
        {
            get { return iniTransCost.ValueObject; }
            set { iniTransCost.ValueObject = Utils.ToDecimal(value); }
        }
        public object DocuCost
        {
            get { return iniDocuCost.ValueObject; }
            set { iniDocuCost.ValueObject = Utils.ToDecimal(value); }
        }
        public object AdviceCost
        {
            get { return iniAdviceCost.ValueObject; }
            set { iniAdviceCost.ValueObject = Utils.ToDecimal(value); }
        }
        public object OtherCost
        {
            get { return iniOtherCost.ValueObject; }
            set { iniOtherCost.ValueObject = Utils.ToDecimal(value); }
        }
        public int id { get; set; } = 0;

        private bool readOnlyMode = false;

        public Grid52()
        {
            InitializeComponent();

            this.cbAcdtSurvDtlCode.MouseWheel += Combo_MouseWheel;
            this.iniAcdtSurvVstCnt.Enter += Txt_Enter;
            this.iniAcdtSurvPassDay.Enter += Txt_Enter;
            this.iniDefaultCost.Enter += Txt_Enter;
            this.iniTransCost.Enter += Txt_Enter;
            this.iniDocuCost.Enter += Txt_Enter;
            this.iniAdviceCost.Enter += Txt_Enter;
            this.iniOtherCost.Enter += Txt_Enter;
        }

        private void Txt_Enter(object sender, EventArgs e)
        {
            this.Focus();
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
                Utils.SetCombo(cbAcdtSurvDtlCode, ds.Tables["CD_AI00558"].Copy(), "MinorSeq", "MinorName", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.cbAcdtSurvDtlCode.SetReadOnly(rdonly);
            this.iniAcdtSurvVstCnt.SetReadOnly(rdonly);
            this.iniAcdtSurvPassDay.SetReadOnly(rdonly);
            this.iniDefaultCost.SetReadOnly(rdonly);
            this.iniTransCost.SetReadOnly(rdonly);
            this.iniDocuCost.SetReadOnly(rdonly);
            this.iniAdviceCost.SetReadOnly(rdonly);
            this.iniOtherCost.SetReadOnly(rdonly);
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
            this.cbAcdtSurvDtlCode.Text = "";
            if (cbAcdtSurvDtlCode.Items.Count > 0) cbAcdtSurvDtlCode.SelectedIndex = 0;
            this.iniAcdtSurvVstCnt.ValueObject = null;
            this.iniAcdtSurvPassDay.ValueObject = null;
            this.iniDefaultCost.ValueObject = null;
            this.iniTransCost.ValueObject = null;
            this.iniDocuCost.ValueObject = null;
            this.iniAdviceCost.ValueObject = null;
            this.iniOtherCost.ValueObject = null;
            this.id = 0;
        }
    }
}
