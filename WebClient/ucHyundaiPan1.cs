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
    public partial class ucHyundaiPan1 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string AcdtDt
        {
            get
            {
                if (dtAcdtDt.ValueObject == null || dtAcdtDt.ValueObject + "" == "") return "";
                return Utils.DateFormat(dtAcdtDt.ValueObject, "yyyyMMdd");
            }
            set { dtAcdtDt.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string AcdtTm
        {
            get { return txtAcdtTm.Text; }
            set { txtAcdtTm.Text = value; }
        }
        public string AcdtAddressName
        {
            get { return txtAcdtAddressName.Text; }
            set { txtAcdtAddressName.Text = value; }
        }
        public string AcdtCnts
        {
            get { return txtAcdtCnts.rtbDoc.Rtf; }
            set { txtAcdtCnts.rtbDoc.Rtf = value; }
        }

        private bool readOnlyMode = false;
        public ucHyundaiPan1()
        {
            InitializeComponent();

            this.dtAcdtDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.txtAcdtTm.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtAcdtAddressName.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtAcdtCnts.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtAcdtCnts.ContentsResized += Txt_ContentsResized;
            this.txtAcdtCnts.ContentsMouseWheel += Txt_ContentsMouseWheel;

            txtAcdtAddressName.SetReadOnly(true);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.dtAcdtDt.SetReadOnly(rdonly);
            txtAcdtTm.SetReadOnly(rdonly);
            //txtAcdtAddressName.SetReadOnly(rdonly);
            txtAcdtCnts.SetReadOnly(rdonly);
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

        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            this.Focus();
        }

        private void Txt_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int height = 46;
            this.Height = height + e.NewRectangle.Height;
            this.panel1.Height = height + e.NewRectangle.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            dtAcdtDt.ValueObject = null;
            txtAcdtTm.Text = "";
            txtAcdtAddressName.Text = "";
            txtAcdtCnts.rtbDoc.Rtf = "";
        }

        public void SetFocus()
        {
            this.dtAcdtDt.Focus();
        }

        private void Text_Change(object sender, EventArgs e)
        {
            Control txt = (Control)sender;
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;
        }
    }
}
