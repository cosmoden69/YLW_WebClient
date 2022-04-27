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
    public partial class meritzPanA : UserControl
    {
        public delegate void MrtzResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler MeritzPanAMouseWheel;
        public event MrtzResizeEventHandler MeritzPanAResize;

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

        public meritzPanA()
        {
            InitializeComponent();

            this.txtLongCnts1.ContentsResized += Txt_ContentsResized;
            this.txtLongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
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
            if (this.MeritzPanAMouseWheel != null) this.MeritzPanAMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.Height = e.NewRectangle.Height;
            this.pan_hide_00.Height = e.NewRectangle.Height;
            this.pnTitle1.Height = e.NewRectangle.Height;
            if (this.MeritzPanAResize != null) this.MeritzPanAResize(this, e);
        }

        public void SetFocus()
        {
            this.txtLongCnts1.Focus();
        }
    }
}
