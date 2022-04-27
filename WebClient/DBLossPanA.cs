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
    public partial class DBLossPanA : UserControl
    {
        public delegate void DBLossResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler DBLossPanAMouseWheel;
        public event DBLossResizeEventHandler DBLossPanAResize;
        ResizeRowManager manager = null;

        public string Title
        {
            get { return pnTitle1.Text; }
            set { pnTitle1.Text = value; }
        }
        public ExtendedRichTextBox.RichTextBoxPrintCtrl rtbDoc
        {
            get { return txtLongCnts1.rtbDoc; }
            set { txtLongCnts1.rtbDoc = value; }
        }
        private bool readOnlyMode = false;

        public DBLossPanA()
        {
            InitializeComponent();

            this.txtLongCnts1.ContentsResized += Txt_ContentsResized;
            this.txtLongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;

            manager = new ResizeRowManager(this, this.pan_hide_00);
            manager.AddControl(0, pnTitle1, txtLongCnts1);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtLongCnts1.rtbDoc.ReadOnly = rdonly;
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
            if (this.DBLossPanAMouseWheel != null) this.DBLossPanAMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            manager.ContentsResized(sender, e);
            if (this.DBLossPanAResize != null) this.DBLossPanAResize(this, e);
        }

        public void SetFocus()
        {
            this.txtLongCnts1.Focus();
        }
    }
}
