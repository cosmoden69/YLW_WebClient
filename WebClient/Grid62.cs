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
    public partial class Grid62 : UserControl
    {
        public delegate void invsgDcdChangeEventHandler(string defaultFee, string TransFee, string otherFee);
        public event invsgDcdChangeEventHandler InvsgDcdChanged;
        public event MouseEventHandler ContentsMouseWheel;

        public string invgrEno
        {
            get { return txtinvgrEno.Text; }
            set { txtinvgrEno.Text = value; }
        }
        public string invsgDcd
        {
            get
            {
                return Utils.GetP(Utils.GetComboSelectedValue(cbinvsgDcd, "MinorSeq"), "-", 1);
            }
            set { }
        }
        public string invsgDcd2
        {
            get
            {
                return Utils.GetComboSelectedValue(cbinvsgDcd, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbinvsgDcd, value, "MinorSeq");
            }
        }
        public string invsgDifcCd
        {
            get
            {
                return Utils.GetComboSelectedValue(cbinvsgDifcCd, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbinvsgDifcCd, value, "MinorSeq");
            }
        }
        public string accipInsJobCd
        {
            get
            {
                return Utils.GetComboSelectedValue(cbaccipInsJobCd, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbaccipInsJobCd, value, "MinorSeq");
            }
        }
        public int id { get; set; } = 0;

        private bool readOnlyMode = false;
        private bool _bEvent = false;
        private DataTable USERCD_SURVGB2 = null;
        private DataTable USERCD_INSJOBCD = null;

        public Grid62()
        {
            InitializeComponent();

            this.cbinvsgDcd.MouseWheel += Combo_MouseWheel;
            this.cbinvsgDifcCd.MouseWheel += Combo_MouseWheel;
            this.cbaccipInsJobCd.MouseWheel += Combo_MouseWheel;
            this.cbinvsgDcd.SelectedIndexChanged += CbinvsgDcd_SelectedIndexChanged;
            this.cbaccipInsJobCd.DropDownChange += CbaccipInsJobCd_DropDownChange;
            this.txtJobCodeFilter.TextChanged += TxtJobCodeFilter_TextChanged;
            this.txtJobCodeFilter.MouseWheel += TxtJobCodeFilter_MouseWheel;

            _bEvent = true;
        }

        private void Combo_MouseWheel(object sender, MouseEventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo != null && cbo.DroppedDown) return;
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void TxtJobCodeFilter_MouseWheel(object sender, MouseEventArgs e)
        {
            if (cbaccipInsJobCd.DroppedDown)
            {
                ((HandledMouseEventArgs)e).Handled = true;
                return;
            }
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void CbaccipInsJobCd_DropDownChange(object sender, bool Expanded)
        {
            try
            { 
                if (Expanded)
                {
                    string value = Utils.GetComboSelectedValue(cbaccipInsJobCd, "MinorSeq");
                    DataTable dtTmp = USERCD_INSJOBCD.Copy();
                    DataRow[] drows = dtTmp.Select("MinorName LIKE '%" + txtJobCodeFilter.Text + "%'");
                    DataTable dtTmp2 = null;
                    if (drows != null && drows.Length > 0) dtTmp2 = drows?.CopyToDataTable();
                    else dtTmp2 = dtTmp.Clone();
                    Utils.SetCombo(cbaccipInsJobCd, dtTmp2, "MinorSeq", "MinorName", true);
                    Utils.SetComboSelectedValue(cbaccipInsJobCd, value, "MinorSeq");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Init_Set(DataSet ds)
        {
            try
            {
                _bEvent = false;

                USERCD_SURVGB2 = ds.Tables["USERCD_SURVGB2"].Copy();
                USERCD_INSJOBCD = ds.Tables["USERCD_INSJOBCD"].Copy();            
                Utils.SetCombo(cbinvsgDcd, ds.Tables["USERCD_SURVGB2"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbinvsgDifcCd, ds.Tables["CD_PA00206"].Copy(), "MinorSeq", "MinorName", true);
                Utils.SetCombo(cbaccipInsJobCd, ds.Tables["USERCD_INSJOBCD"].Copy(), "MinorSeq", "MinorName", true);

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
            this.txtinvgrEno.SetReadOnly(rdonly);
            this.cbinvsgDcd.SetReadOnly(rdonly);
            this.cbinvsgDifcCd.SetReadOnly(rdonly);
            this.cbaccipInsJobCd.SetReadOnly(rdonly);
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

        private void CbinvsgDcd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bEvent) return;
            string cd = Utils.GetComboSelectedValue(cbinvsgDcd, "MinorSeq");
            DataRow[] drs = USERCD_SURVGB2.Select("MinorSeq = '" + cd + "' ");
            if (drs == null || drs.Length != 1) return;
            string fee = Utils.ConvertToString(drs[0]["value_remark"]);
            string fee1 = Utils.GetP(Utils.GetP(fee, "/", 1), ":", 2).Replace(",", "");  //기본보수
            string fee2 = Utils.GetP(Utils.GetP(fee, "/", 2), ":", 2).Replace(",", "");  //교통비
            string fee3 = Utils.GetP(Utils.GetP(fee, "/", 3), ":", 2).Replace(",", "");  //우수
            if (this.InvsgDcdChanged != null) this.InvsgDcdChanged(fee1.Replace("원", ""), fee2.Replace("원", ""), fee3.Replace("원", ""));
        }

        private void TxtJobCodeFilter_TextChanged(object sender, EventArgs e)
        {
            //DataTable dtTmp = USERCD_INSJOBCD.Copy();
            //Utils.SetCombo(cbaccipInsJobCd, dtTmp.Select("MinorName LIKE '%" + txtJobCodeFilter.Text + "%'")?.CopyToDataTable(), "MinorSeq", "MinorName", true);
            //if (!cbaccipInsJobCd.DroppedDown) cbaccipInsJobCd.DroppedDown = true;
        }

        public void Clear()
        {
            _bEvent = false;

            this.txtinvgrEno.Text = "";
            this.cbinvsgDcd.Text = "";
            if (cbinvsgDcd.Items.Count > 0) cbinvsgDcd.SelectedIndex = 0;
            this.cbinvsgDifcCd.Text = "";
            if (cbinvsgDifcCd.Items.Count > 0) cbinvsgDifcCd.SelectedIndex = 0;
            this.cbaccipInsJobCd.Text = "";
            if (cbaccipInsJobCd.Items.Count > 0) cbaccipInsJobCd.SelectedIndex = 0;
            this.id = 0;

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.Focus();
            this.txtinvgrEno.Focus();
        }
    }
}
