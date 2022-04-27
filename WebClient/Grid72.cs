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
    public partial class Grid72 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string clmRscd
        {
            get
            {
                return Utils.GetComboSelectedValue(cbclmRscd, "MinorSeq");
            }
            set
            {
                Utils.SetComboSelectedValue(cbclmRscd, value, "MinorSeq");
            }
        }
        public string payNotcCtnt
        {
            get { return txtpayNotcCtnt.rtbDoc.Text; }
            set { txtpayNotcCtnt.rtbDoc.Text = value; }
        }
        public string rltRuleCtnt
        {
            get { return txtrltRuleCtnt.rtbDoc.Text; }
            set { txtrltRuleCtnt.rtbDoc.Text = value; }
        }
        public int id { get; set; } = 0;
        public int parent_id { get; set; } = 0;

        private bool readOnlyMode = false;
        private bool _bEvent = false;

        public Grid72()
        {
            InitializeComponent();

            this.txtpayNotcCtnt.MaxInputLength = 3000;
            this.txtrltRuleCtnt.MaxInputLength = 3000;

            this.txtpayNotcCtnt.ContentsResized += txtpayNotcCtnt_ContentsResized;
            this.txtrltRuleCtnt.ContentsResized += txtrltRuleCtnt_ContentsResized;
            this.txtpayNotcCtnt.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtrltRuleCtnt.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.cbclmRscd.MouseWheel += Combo_MouseWheel;

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

                Utils.SetCombo(cbclmRscd, ds.Tables["CD_PA00238"].Copy(), "MinorSeq", "MinorName", true);

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
            this.cbclmRscd.SetReadOnly(rdonly);
            this.txtpayNotcCtnt.rtbDoc.ReadOnly = rdonly;
            this.txtrltRuleCtnt.rtbDoc.ReadOnly = rdonly;
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

        private void txtpayNotcCtnt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.panelEx21.Height = e.NewRectangle.Height + 2;
            this.Height = this.panelEx11.Height + this.panelEx21.Height + this.panelEx31.Height;
            this.panelEx31.Top = this.panelEx11.Height + this.panelEx21.Height;
            this.txtrltRuleCtnt.Top = this.panelEx31.Top + 1;
        }

        private void txtrltRuleCtnt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.panelEx31.Height = e.NewRectangle.Height + 2;
            this.Height = this.panelEx11.Height + this.panelEx21.Height + this.panelEx31.Height;
        }

        public void Clear()
        {
            _bEvent = false;

            this.cbclmRscd.Text = "";
            if (cbclmRscd.Items.Count > 0) cbclmRscd.SelectedIndex = 0;
            this.txtpayNotcCtnt.rtbDoc.Text = "";
            this.txtrltRuleCtnt.rtbDoc.Text = "";
            this.id = 0;
            this.parent_id = 0;

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.Focus();
            this.cbclmRscd.Focus();
        }
    }
}
