﻿using System;
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
    public partial class ucMeritzPan2 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public string AcdtDt
        {
            get
            {
                if (dtiAcdtDt.ValueObject == null || dtiAcdtDt.ValueObject + "" == "") return "";
                return Utils.DateFormat(dtiAcdtDt.ValueObject, "yyyyMMdd");
            }
            set { dtiAcdtDt.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string ShrtCnts1
        {
            get { return txtShrtCnts1.Text; }
            set { txtShrtCnts1.Text = value; }
        }
        public string LongCnts1
        {
            get { return txtLongCnts1.rtbDoc.Rtf; }
            set { txtLongCnts1.rtbDoc.Rtf = value; }
        }
        public string IsrdJobGrad
        {
            get { return txtIsrdJobGrad.Text; }
            set { txtIsrdJobGrad.Text = value; }
        }
        public string IsrdJobGradDmnd
        {
            get { return txtIsrdJobGradDmnd.Text; }
            set { txtIsrdJobGradDmnd.Text = value; }
        }

        private bool readOnlyMode = false;
        public ucMeritzPan2()
        {
            InitializeComponent();

            this.txtShrtCnts1.Multiline = true;
            this.txtIsrdJobGrad.Multiline = true;
            this.txtIsrdJobGradDmnd.Multiline = true;

            this.txtLongCnts1.ContentsResized += Txt_ContentsResized;
            this.txtLongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.dtiAcdtDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.txtShrtCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtLongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdJobGrad.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdJobGradDmnd.TextChanged += new System.EventHandler(this.Text_Change);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            dtiAcdtDt.SetReadOnly(rdonly);
            txtShrtCnts1.SetReadOnly(rdonly);
            txtLongCnts1.rtbDoc.ReadOnly = rdonly;
            txtIsrdJobGrad.SetReadOnly(rdonly);
            txtIsrdJobGradDmnd.SetReadOnly(rdonly);
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
            int height = e.NewRectangle.Height;
            //this.dtiAcdtDt.MinimumSize = new Size(0, height);
            //this.dtiAcdtDt.Height = height;
            this.panelEx11.Height = height;
            this.txtShrtCnts1.Height = height;
            this.txtIsrdJobGrad.Height = height;
            this.txtIsrdJobGradDmnd.Height = height;
            this.Height = 23 + height;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Clear()
        {
            dtiAcdtDt.ValueObject = null;
            txtShrtCnts1.Text = "";
            txtLongCnts1.rtbDoc.Rtf = "";
            txtIsrdJobGrad.Text = "";
            txtIsrdJobGradDmnd.Text = "";
        }

        public void SetFocus()
        {
            this.dtiAcdtDt.Focus();
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
