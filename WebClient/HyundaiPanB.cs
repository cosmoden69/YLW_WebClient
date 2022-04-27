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
    public partial class HyundaiPanB : UserControl
    {
        public delegate void HyndResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler HyundaiPanBMouseWheel;
        public event HyndResizeEventHandler HyundaiPanBResize;

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
        public ExtendedRichTextBox.RichTextBoxPrintCtrl rtbLongCnts2
        {
            get { return txtLongCnts2.rtbDoc; }
            set { txtLongCnts2.rtbDoc = value; }
        }
        private bool readOnlyMode = false;

        public HyundaiPanB()
        {
            InitializeComponent();

            this.txtLongCnts1.ContentsResized += Txt1_ContentsResized;
            this.txtLongCnts2.ContentsResized += Txt2_ContentsResized;
            this.txtLongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtLongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtLongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtLongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtLongCnts1.rtbDoc.ReadOnly = rdonly;
            this.txtLongCnts2.rtbDoc.ReadOnly = rdonly;
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
            if (this.HyundaiPanBMouseWheel != null) this.HyundaiPanBMouseWheel(this, e);
        }

        private void Txt1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            int hgt = Math.Max(this.txtLongCnts2.NewHeight, e.NewRectangle.Height);
            this.Height = hgt;
            this.pan_hide_00.Height = hgt;
            this.pnTitle1.Height = hgt;
            this.txtLongCnts1.SetContentsHeight(hgt);
            this.txtLongCnts2.SetContentsHeight(hgt);
            if (this.HyundaiPanBResize != null) this.HyundaiPanBResize(this, e);
        }

        private void Txt2_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            int hgt = Math.Max(this.txtLongCnts1.NewHeight, e.NewRectangle.Height);
            this.Height = hgt;
            this.pan_hide_00.Height = hgt;
            this.pnTitle1.Height = hgt;
            this.txtLongCnts1.SetContentsHeight(hgt);
            this.txtLongCnts2.SetContentsHeight(hgt);
            if (this.HyundaiPanBResize != null) this.HyundaiPanBResize(this, e);
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
            txtLongCnts2.rtbDoc.Rtf = "";
        }
    }
}
