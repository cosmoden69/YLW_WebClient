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
    public partial class ucHeungkukPan2 : UserControl
    {
        public delegate void HenkResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler ContentsMouseWheel;
        public event HenkResizeEventHandler HeungkukPan2Resize;

        public string S101_LongCnts1
        {
            get { return txtS101_LongCnts1.rtbDoc.Rtf; }
            set { txtS101_LongCnts1.rtbDoc.Rtf = value; }
        }
        public string S101_ShrtCnts1
        {
            get { return Utils.DateFormat(dtiS101_ShrtCnts1.ValueObject, "yyyyMMdd"); }
            set { dtiS101_ShrtCnts1.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string S101_ShrtCnts2
        {
            get { return txtS101_ShrtCnts2.Text; }
            set { txtS101_ShrtCnts2.Text = value; }
        }
        public string S101_LongCnts2
        {
            get { return txtS101_LongCnts2.rtbDoc.Rtf; }
            set { txtS101_LongCnts2.rtbDoc.Rtf = value; }
        }
        public string S102_ShrtCnts1
        {
            get { return txtS102_ShrtCnts1.Text; }
            set { txtS102_ShrtCnts1.Text = value; }
        }
        public string S102_ShrtCnts2
        {
            get { return txtS102_ShrtCnts2.Text; }
            set { txtS102_ShrtCnts2.Text = value; }
        }
        public string S102_LongCnts1
        {
            get { return txtS102_LongCnts1.rtbDoc.Rtf; }
            set { txtS102_LongCnts1.rtbDoc.Rtf = value; }
        }
        public string S102_LongCnts2
        {
            get { return txtS102_LongCnts2.Text; }
            set { txtS102_LongCnts2.Text = value; }
        }
        public string S102_ShrtCnts3
        {
            get { return Utils.DateFormat(dtiS102_ShrtCnts3.ValueObject, "yyyyMMdd"); }
            set { dtiS102_ShrtCnts3.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string S133_ShrtCnts1
        {
            get { return Utils.DateFormat(dtiS133_ShrtCnts1.ValueObject, "yyyyMMdd"); }
            set { dtiS133_ShrtCnts1.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string S133_ShrtCnts2
        {
            get { return Utils.DateFormat(dtiS133_ShrtCnts2.ValueObject, "yyyyMMdd"); }
            set { dtiS133_ShrtCnts2.ValueObject = Utils.ConvertToDateTime(value); }
        }

        private bool _bEvent = false;

        private bool readOnlyMode = false;
        public ucHeungkukPan2()
        {
            InitializeComponent();

            this.txtS101_LongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.dtiS101_ShrtCnts1.ValueChanged += new System.EventHandler(this.Date_Change);
            this.txtS101_ShrtCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS101_LongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS102_ShrtCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS102_ShrtCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS102_LongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS102_LongCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.dtiS102_ShrtCnts3.ValueChanged += new System.EventHandler(this.Date_Change);
            this.dtiS133_ShrtCnts1.ValueChanged += new System.EventHandler(this.Date_Change);
            this.dtiS133_ShrtCnts2.ValueChanged += new System.EventHandler(this.Date_Change);

            this.txtS101_LongCnts1.ContentsResized += Txt_ContentsResized;
            this.txtS101_LongCnts2.ContentsResized += Txt_ContentsResized;
            this.txtS102_LongCnts1.ContentsResized += Txt_ContentsResized;

            this.txtS101_LongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.dtiS101_ShrtCnts1.MouseWheel += Txt_ContentsMouseWheel;
            this.txtS101_ShrtCnts2.MouseWheel += Txt_ContentsMouseWheel;
            this.txtS101_LongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS102_ShrtCnts1.MouseWheel += Txt_ContentsMouseWheel;
            this.txtS102_ShrtCnts2.MouseWheel += Txt_ContentsMouseWheel;
            this.txtS102_LongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.dtiS102_ShrtCnts3.MouseWheel += Txt_ContentsMouseWheel;
            this.dtiS133_ShrtCnts1.MouseWheel += Txt_ContentsMouseWheel;
            this.dtiS133_ShrtCnts2.MouseWheel += Txt_ContentsMouseWheel;

            _bEvent = true;
        }

        private void TxtS101_ShrtCnts2_MouseWheel(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtS101_LongCnts1.SetReadOnly(rdonly);
            this.dtiS101_ShrtCnts1.SetReadOnly(rdonly);
            this.txtS101_ShrtCnts2.SetReadOnly(rdonly);
            this.txtS101_LongCnts2.SetReadOnly(rdonly);
            this.txtS102_ShrtCnts1.SetReadOnly(rdonly);
            this.txtS102_ShrtCnts2.SetReadOnly(rdonly);
            this.txtS102_LongCnts1.SetReadOnly(rdonly);
            this.txtS102_LongCnts2.SetReadOnly(rdonly);
            this.dtiS102_ShrtCnts3.SetReadOnly(rdonly);
            this.dtiS133_ShrtCnts1.SetReadOnly(rdonly);
            this.dtiS133_ShrtCnts2.SetReadOnly(rdonly);
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

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            this.SuspendLayout();
            int height = 23;
            Point pos = new Point(0, height - 1);
            this.txtS101_LongCnts1.Top = pos.Y;
            this.pnTitle1.Height = this.txtS101_LongCnts1.Height;
            this.pnTitle1.Top = pos.Y; pos.Y += this.pnTitle1.Height - 1;
            //
            this.dtiS101_ShrtCnts1.Top = pos.Y; this.txtS101_ShrtCnts2.Top = pos.Y;
            this.pnTitle2.Top = pos.Y; pos.Y += this.pnTitle2.Height - 1;
            //
            this.txtS101_LongCnts2.Top = pos.Y;
            this.pnTitle3.Height = this.txtS101_LongCnts2.Height;
            this.pnTitle3.Top = pos.Y; pos.Y += this.pnTitle3.Height - 1;
            //
            this.txtS102_ShrtCnts1.Top = pos.Y; this.txtS102_ShrtCnts2.Top = pos.Y;
            this.pnTitle4.Top = pos.Y; pos.Y += this.pnTitle4.Height - 1;
            //
            this.txtS102_LongCnts1.Top = pos.Y;
            this.pnTitle5.Height = this.txtS102_LongCnts1.Height;
            this.pnTitle5.Top = pos.Y; pos.Y += this.pnTitle5.Height - 1;
            //
            this.txtS102_LongCnts2.Top = pos.Y; this.dtiS102_ShrtCnts3.Top = pos.Y;
            this.pnTitle6.Height = this.txtS102_LongCnts2.Height;
            this.pnTitle6.Top = pos.Y; pos.Y += this.pnTitle6.Height - 1;
            //
            this.dtiS133_ShrtCnts1.Top = pos.Y; this.dtiS133_ShrtCnts2.Top = pos.Y;
            this.pnTitle7.Top = pos.Y; pos.Y += this.pnTitle7.Height - 1;
            this.Height = pos.Y + 1;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
            if (this.HeungkukPan2Resize != null) this.HeungkukPan2Resize(this, e);
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

        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            this.Focus();
        }

        public void Clear()
        {
            _bEvent = false;

            this.txtS101_LongCnts1.rtbDoc.Rtf = "";
            this.dtiS101_ShrtCnts1.ValueObject = null;
            this.txtS101_ShrtCnts2.Text = "";
            this.txtS101_LongCnts2.rtbDoc.Rtf = "";
            this.txtS102_ShrtCnts1.Text = "";
            this.txtS102_ShrtCnts2.Text = "";
            this.txtS102_LongCnts1.rtbDoc.Rtf = "";
            this.txtS102_LongCnts2.Text = "";
            this.dtiS102_ShrtCnts3.ValueObject = null;
            this.dtiS133_ShrtCnts1.ValueObject = null;
            this.dtiS133_ShrtCnts2.ValueObject = null;

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.txtS101_LongCnts1.Focus();
        }
    }
}
