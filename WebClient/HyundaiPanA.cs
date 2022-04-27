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
    public partial class HyundaiPanA : UserControl
    {
        public delegate void MrtzResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler HyundaiPanAMouseWheel;
        public event MrtzResizeEventHandler HyundaiPanAResize;
        ResizeRowManager manager = null;

        public string Title
        {
            get { return pnTitle1.Text; }
            set { pnTitle1.Text = value; }
        }
        public ExtendedRichTextBox.RichTextBoxPrintCtrl rtbLongCnts1
        {
            get { return txtLongCnts1.rtbDoc; }
            set { txtLongCnts1.rtbDoc = value; }
        }
        public string ShrtCnts1
        {
            get { return txtShrtCnts1.rtbDoc.Text; }
            set { txtShrtCnts1.rtbDoc.Text = value; }
        }
        public ExtendedRichTextBox.RichTextBoxPrintCtrl rtbLongCnts2
        {
            get { return txtLongCnts2.rtbDoc; }
            set { txtLongCnts2.rtbDoc = value; }
        }
        public string ShrtCnts2
        {
            get { return txtShrtCnts2.rtbDoc.Text; }
            set { txtShrtCnts2.rtbDoc.Text = value; }
        }
        public string Amt1
        {
            get { return txtAmt1.Text; }
            set { txtAmt1.Text = value; }
        }
        private bool readOnlyMode = false;

        public HyundaiPanA()
        {
            InitializeComponent();

            this.txtShrtCnts1.ContentsResized += Txt_ContentsResized;
            this.txtShrtCnts2.ContentsResized += Txt_ContentsResized;
            this.txtLongCnts1.ContentsResized += Txt_ContentsResized;
            this.txtLongCnts2.ContentsResized += Txt_ContentsResized;
            this.txtShrtCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtShrtCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtLongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtLongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtLongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtLongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtShrtCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtShrtCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtAmt1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtAmt1.KeyPress += TxtAmt1_KeyPress;

            this.txtShrtCnts1.MaxInputLength = 100;
            this.txtShrtCnts2.MaxInputLength = 100;

            manager = new ResizeRowManager(this, this.pan_hide_00);
            manager.AddControl(0, pnTitle1, txtLongCnts1, txtShrtCnts1, txtLongCnts2, txtShrtCnts2, txtAmt1);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtLongCnts1.rtbDoc.ReadOnly = rdonly;
            this.txtShrtCnts1.SetReadOnly(rdonly);
            this.txtLongCnts2.rtbDoc.ReadOnly = rdonly;
            this.txtShrtCnts2.SetReadOnly(rdonly);
            this.txtAmt1.SetReadOnly(rdonly);
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
            if (this.HyundaiPanAMouseWheel != null) this.HyundaiPanAMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            manager.ContentsResized(sender, e);
            if (this.HyundaiPanAResize != null) this.HyundaiPanAResize(this, e);
        }

        private void Text_Change(object sender, EventArgs e)
        {
            System.Windows.Forms.Control txt = (System.Windows.Forms.Control)sender;

        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;
        }

        private void PriceChange(object sender, EventArgs e)
        {
            DevComponents.Editors.DoubleInput amt = (DevComponents.Editors.DoubleInput)sender;
        }

        private void TxtAmt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        public void SetFocus()
        {
            this.txtLongCnts1.Focus();
        }

        public void Clear()
        {
            txtLongCnts1.rtbDoc.Rtf = "";
            txtShrtCnts1.rtbDoc.Text = "";
            txtLongCnts2.rtbDoc.Rtf = "";
            txtShrtCnts2.rtbDoc.Text = "";
            txtAmt1.Text = "";
        }
    }
}
