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
    public partial class ucMGLossPan2 : UserControl
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
        public string AcdtCaus
        {
            get { return txtAcdtCaus.rtbDoc.Rtf; }
            set { txtAcdtCaus.rtbDoc.Rtf = value; }
        }
        public string S111_LongCnts1
        {
            get { return txtS111_LongCnts1.rtbDoc.Rtf; }
            set { txtS111_LongCnts1.rtbDoc.Rtf = value; }
        }
        public string S111_LongCnts2
        {
            get { return txtS111_LongCnts2.rtbDoc.Rtf; }
            set { txtS111_LongCnts2.rtbDoc.Rtf = value; }
        }
        public string S111_ShrtCnts1
        {
            get { return txtS111_ShrtCnts1.Text; }
            set { txtS111_ShrtCnts1.Text = value; }
        }
        public string S111_ShrtCnts2
        {
            get { return txtS111_ShrtCnts2.Text; }
            set { txtS111_ShrtCnts2.Text = value; }
        }
        public string S111_ShrtCnts3
        {
            get { return txtS111_ShrtCnts3.Text; }
            set { txtS111_ShrtCnts3.Text = value; }
        }

        private bool readOnlyMode = false;
        public ucMGLossPan2()
        {
            InitializeComponent();

            this.dtAcdtDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.txtAcdtTm.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtAcdtAddressName.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtAcdtCaus.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS111_LongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS111_LongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS111_ShrtCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS111_ShrtCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS111_ShrtCnts3.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtAcdtCaus.ContentsResized += Txt1_ContentsResized;
            this.txtS111_LongCnts1.ContentsResized += Txt2_ContentsResized;
            this.txtS111_LongCnts2.ContentsResized += Txt3_ContentsResized;
            this.txtAcdtCaus.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS111_LongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS111_LongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;

            txtAcdtAddressName.SetReadOnly(true);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.dtAcdtDt.SetReadOnly(rdonly);
            txtAcdtTm.SetReadOnly(rdonly);
            //txtAcdtAddressName.SetReadOnly(rdonly);
            txtAcdtCaus.SetReadOnly(rdonly);
            txtS111_LongCnts1.SetReadOnly(rdonly);
            txtS111_LongCnts2.SetReadOnly(rdonly);
            txtS111_ShrtCnts1.SetReadOnly(rdonly);
            txtS111_ShrtCnts2.SetReadOnly(rdonly);
            txtS111_ShrtCnts3.SetReadOnly(rdonly);
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

        private void Txt1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int hgt = txtAcdtCaus.Top + txtAcdtCaus.Height;
            panelEx4.Top = hgt; hgt += panelEx4.Height;
            txtS111_LongCnts1.Top = hgt; hgt += txtS111_LongCnts1.Height;
            panelEx5.Top = hgt; hgt += panelEx5.Height;
            txtS111_LongCnts2.Top = hgt; hgt += txtS111_LongCnts2.Height;
            panelEx6.Top = hgt; hgt += panelEx6.Height;
            panelEx71.Top = hgt; txtS111_ShrtCnts1.Top = hgt;
            panelEx72.Top = hgt; txtS111_ShrtCnts2.Top = hgt;
            panelEx73.Top = hgt; txtS111_ShrtCnts3.Top = hgt;
            hgt += panelEx71.Height;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt2_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int hgt = txtS111_LongCnts1.Top + txtS111_LongCnts1.Height;
            panelEx5.Top = hgt; hgt += panelEx5.Height;
            txtS111_LongCnts2.Top = hgt; hgt += txtS111_LongCnts2.Height;
            panelEx6.Top = hgt; hgt += panelEx6.Height;
            panelEx71.Top = hgt; txtS111_ShrtCnts1.Top = hgt;
            panelEx72.Top = hgt; txtS111_ShrtCnts2.Top = hgt;
            panelEx73.Top = hgt; txtS111_ShrtCnts3.Top = hgt;
            hgt += panelEx71.Height;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Txt3_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int hgt = txtS111_LongCnts2.Top + txtS111_LongCnts2.Height;
            panelEx6.Top = hgt; hgt += panelEx6.Height;
            panelEx71.Top = hgt; txtS111_ShrtCnts1.Top = hgt;
            panelEx72.Top = hgt; txtS111_ShrtCnts2.Top = hgt;
            panelEx73.Top = hgt; txtS111_ShrtCnts3.Top = hgt;
            hgt += panelEx71.Height;
            this.Height = hgt + 1;
            this.panel1.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            dtAcdtDt.ValueObject = null;
            txtAcdtTm.Text = "";
            txtAcdtAddressName.Text = "";
            txtAcdtCaus.rtbDoc.Rtf = "";
            txtS111_LongCnts1.rtbDoc.Rtf = "";
            txtS111_LongCnts2.rtbDoc.Rtf = "";
            txtS111_ShrtCnts1.Text = "";
            txtS111_ShrtCnts2.Text = "";
            txtS111_ShrtCnts3.Text = "";
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
