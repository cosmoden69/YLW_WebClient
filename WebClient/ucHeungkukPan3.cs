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
    public partial class ucHeungkukPan3 : UserControl
    {
        public delegate void HenkResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler ContentsMouseWheel;
        public event HenkResizeEventHandler HeungkukPan3Resize;

        public string S201_ShrtCnts1
        {
            get { return txtS201_ShrtCnts1.Text; }
            set { txtS201_ShrtCnts1.Text = value; }
        }
        public string S201_LongCnts1
        {
            get { return txtS201_LongCnts1.rtbDoc.Rtf; }
            set { txtS201_LongCnts1.rtbDoc.Rtf = value; }
        }
        public string S201_LongCnts2
        {
            get { return txtS201_LongCnts2.rtbDoc.Rtf; }
            set { txtS201_LongCnts2.rtbDoc.Rtf = value; }
        }

        private bool _bEvent = false;

        private bool readOnlyMode = false;
        public ucHeungkukPan3()
        {
            InitializeComponent();

            this.txtS201_ShrtCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS201_LongCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtS201_LongCnts2.TextChanged += new System.EventHandler(this.Text_Change);

            this.txtS201_LongCnts1.ContentsResized += Txt_ContentsResized;
            this.txtS201_LongCnts2.ContentsResized += Txt_ContentsResized;

            this.txtS201_ShrtCnts1.MouseWheel += Txt_ContentsMouseWheel;
            this.txtS201_LongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtS201_LongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;

            _bEvent = true;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtS201_ShrtCnts1.SetReadOnly(rdonly);
            this.txtS201_LongCnts1.rtbDoc.ReadOnly = rdonly;
            this.txtS201_LongCnts2.rtbDoc.ReadOnly = rdonly;
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
            this.txtS201_ShrtCnts1.Top = pos.Y;
            this.pnTitle1.Top = pos.Y; pos.Y += this.pnTitle1.Height - 1;
            //
            this.txtS201_LongCnts1.Top = pos.Y;
            this.pnTitle2.Height = this.txtS201_LongCnts1.Height;
            this.pnTitle2.Top = pos.Y; pos.Y += this.pnTitle2.Height - 1;
            //
            this.txtS201_LongCnts2.Top = pos.Y;
            this.pnTitle3.Height = this.txtS201_LongCnts2.Height;
            this.pnTitle3.Top = pos.Y; pos.Y += this.pnTitle3.Height - 1;
            this.Height = pos.Y + 1;
            this.panel2.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
            if (this.HeungkukPan3Resize != null) this.HeungkukPan3Resize(this, e);
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

            this.txtS201_ShrtCnts1.Text = "";
            this.txtS201_LongCnts1.rtbDoc.Rtf = "";
            this.txtS201_LongCnts2.rtbDoc.Rtf = "";

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.txtS201_ShrtCnts1.Focus();
        }
    }
}
