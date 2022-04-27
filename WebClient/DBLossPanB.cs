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
    public partial class DBLossPanB : UserControl
    {
        public delegate void DBLossResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler DBLossPanBMouseWheel;
        public event DBLossResizeEventHandler DBLossPanBResize;
        ResizeRowManager manager = null;

        public string Title
        {
            get { return pnTitle1.Text; }
            set { pnTitle1.Text = value; }
        }
        public string TextValue
        {
            get { return txtShrtCnts1.rtbDoc.Text; }
            set { txtShrtCnts1.rtbDoc.Text = value; }
        }
        private bool readOnlyMode = false;

        public DBLossPanB()
        {
            InitializeComponent();

            this.txtShrtCnts1.ContentsResized += Txt_ContentsResized;
            this.txtShrtCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;

            manager = new ResizeRowManager(this, this.pan_hide_00);
            manager.AddControl(0, pnTitle1, txtShrtCnts1);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtShrtCnts1.SetReadOnly(rdonly);
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
            if (this.DBLossPanBMouseWheel != null) this.DBLossPanBMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            manager.ContentsResized(sender, e);
            if (this.DBLossPanBResize != null) this.DBLossPanBResize(this, e);
        }

        public void SetFocus()
        {
            this.txtShrtCnts1.Focus();
        }
    }
}
