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
    public partial class DBLossPan6A : UserControl, IComparable
    {
        public delegate void DBLossPanAResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler DBLossPan6AMouseWheel;
        public event DBLossPanAResizeEventHandler DBLossPan6AResize;
        ResizeRowManager manager = null;

        public string ShrtCnts1
        {
            get { return txtShrtCnts1.rtbDoc.Text; }
            set { txtShrtCnts1.rtbDoc.Text = value; }
        }
        public string ShrtCnts2
        {
            get { return txtShrtCnts2.rtbDoc.Text; }
            set { txtShrtCnts2.rtbDoc.Text = value; }
        }
        public string ShrtCnts3
        {
            get { return txtShrtCnts3.rtbDoc.Text; }
            set { txtShrtCnts3.rtbDoc.Text = value; }
        }
        public string LongCnts1
        {
            get { return txtLongCnts1.rtbDoc.Rtf; }
            set { txtLongCnts1.rtbDoc.Rtf = value; }
        }
        public string LongCnts2
        {
            get { return txtLongCnts2.rtbDoc.Rtf; }
            set { txtLongCnts2.rtbDoc.Rtf = value; }
        }
        public bool IsNewRow { get; set; } = false;

        private DBLossPan6 _parentC = null;
        private bool readOnlyMode = false;

        public DBLossPan6A(DBLossPan6 p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();

            this.txtShrtCnts1.rtbDoc.TextChanged += new System.EventHandler(this.RichText_Change);
            this.txtShrtCnts2.rtbDoc.TextChanged += new System.EventHandler(this.RichText_Change);
            this.txtShrtCnts3.rtbDoc.TextChanged += new System.EventHandler(this.RichText_Change);
            this.txtLongCnts1.rtbDoc.TextChanged += new System.EventHandler(this.RichText_Change);
            this.txtLongCnts2.rtbDoc.TextChanged += new System.EventHandler(this.RichText_Change);
            this.txtShrtCnts1.ContentsResized += Txt_ContentsResized;
            this.txtShrtCnts2.ContentsResized += Txt_ContentsResized;
            this.txtShrtCnts3.ContentsResized += Txt_ContentsResized;
            this.txtLongCnts1.ContentsResized += Txt_ContentsResized;
            this.txtLongCnts2.ContentsResized += Txt_ContentsResized;
            this.txtShrtCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtShrtCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtShrtCnts3.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtLongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtLongCnts2.ContentsMouseWheel += Txt_ContentsMouseWheel;

            manager = new ResizeRowManager(this, this.panel2);
            manager.AddControl(0, panelEx1, txtShrtCnts1, panelEx2, txtShrtCnts2, panelEx3, txtShrtCnts3);
            manager.AddControl(1, panelEx4, txtLongCnts1);
            manager.AddControl(2, panelEx5, txtLongCnts2, panelEx18);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtShrtCnts1.SetReadOnly(rdonly);
            this.txtShrtCnts2.SetReadOnly(rdonly);
            this.txtShrtCnts3.SetReadOnly(rdonly);
            this.txtLongCnts1.SetReadOnly(rdonly);
            this.txtLongCnts2.SetReadOnly(rdonly);
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

        private void Text_Change(object sender, EventArgs e)
        {
            Control txt = (Control)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(txt.Text))
            {
                this.IsNewRow = false;
                _parentC.AddEmptyRow();
            }
        }

        private void RichText_Change(object sender, EventArgs e)
        {
            ExtendedRichTextBox.RichTextBoxPrintCtrl txt = (ExtendedRichTextBox.RichTextBoxPrintCtrl)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(txt.Text))
            {
                this.IsNewRow = false;
                _parentC.AddEmptyRow();
            }
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(dat.Text))
            {
                this.IsNewRow = false;
                _parentC.AddEmptyRow();
            }
        }

        private void PriceChange(object sender, EventArgs e)
        {
            DevComponents.Editors.DoubleInput amt = (DevComponents.Editors.DoubleInput)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(amt.Text))
            {
                this.IsNewRow = false;
                _parentC.AddEmptyRow();
            }
        }

        private void Txt_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.DBLossPan6AMouseWheel != null) this.DBLossPan6AMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            manager.ContentsResized(sender, e);
            if (this.DBLossPan6AResize != null) this.DBLossPan6AResize(this, e);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (readOnlyMode) return;
            ButtonX btn = (ButtonX)sender;
            _parentC.RemoveRow(this);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            DBLossPan6A other = obj as DBLossPan6A;
            if (other != null)
            {
                return 0;
            }
            return 1;
        }
    }
}
