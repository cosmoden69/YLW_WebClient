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
    public partial class DBLifeDetail1A : UserControl, IComparable
    {
        public delegate void DBLfCntrAResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler DBLifeDetail1AMouseWheel;
        public event DBLfCntrAResizeEventHandler DBLifeDetail1AResize;

        public string ShrtCnts1
        {
            get { return txtShrtCnts1.Text; }
            set { txtShrtCnts1.Text = value; }
        }
        public object Amt1
        {
            get { return iniAmt1.ValueObject; }
            set { iniAmt1.ValueObject = value; }
        }
        public string ShrtCnts2
        {
            get { return txtShrtCnts2.Text; }
            set { txtShrtCnts2.Text = value; }
        }
        public string ShrtCnts3
        {
            get { return Utils.DateFormat(dtiShrtCnts3.ValueObject, "yyyyMMdd"); }
            set { dtiShrtCnts3.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string LongCnts1
        {
            get { return txtLongCnts1.rtbDoc.Rtf; }
            set { txtLongCnts1.rtbDoc.Rtf = value; }
        }
        public bool IsNewRow { get; set; } = false;

        private DBLifeDetail1 _parentC = null;
        private bool readOnlyMode = false;
        private bool _bEvent = false;

        public DBLifeDetail1A(DBLifeDetail1 p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();

            this.txtShrtCnts1.TextChanged += new System.EventHandler(this.Text_Change);
            this.iniAmt1.TextChanged += new System.EventHandler(this.PriceChange);
            this.txtShrtCnts2.TextChanged += new System.EventHandler(this.Text_Change);
            this.dtiShrtCnts3.ValueChanged += new System.EventHandler(this.Date_Change);
            this.txtLongCnts1.TextChanged += new System.EventHandler(this.RichText_Change);
            this.txtLongCnts1.ContentsResized += Txt_ContentsResized;
            this.txtLongCnts1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.btn_no00_01.Click += new System.EventHandler(this.Button_Click);
            this.Load += DBLifeDetail1A_Load;

            _bEvent = true;
        }

        private void DBLifeDetail1A_Load(object sender, EventArgs e)
        {
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtShrtCnts1.SetReadOnly(rdonly);
            this.iniAmt1.SetReadOnly(rdonly);
            this.txtShrtCnts2.SetReadOnly(rdonly);
            this.dtiShrtCnts3.SetReadOnly(rdonly);
            this.txtLongCnts1.SetReadOnly(rdonly);
            this.btn_no00_01.Enabled = !rdonly;
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
            TextBox txt = (TextBox)sender;

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

        private void RichText_Change(object sender, EventArgs e)
        {
            ExtendedRichTextBox.RichTextBoxPrintCtrl txt = (ExtendedRichTextBox.RichTextBoxPrintCtrl)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(txt.Text))
            {
                this.IsNewRow = false;
                _parentC.AddEmptyRow();
            }
        }

        private void Txt_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.DBLifeDetail1AMouseWheel != null) this.DBLifeDetail1AMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.SuspendLayout();
            int hgt = e.NewRectangle.Height + 2;
            this.txtShrtCnts1.MinimumSize = new Size(0, hgt);
            this.txtShrtCnts1.Height = hgt;
            this.iniAmt1.MinimumSize = new Size(0, hgt);
            this.iniAmt1.Height = hgt;
            this.txtShrtCnts2.Height = hgt;
            this.dtiShrtCnts3.MinimumSize = new Size(0, hgt);
            this.dtiShrtCnts3.Height = hgt;
            this.txtLongCnts1.SetContentsHeight(hgt);
            this.panelEx18.Height = hgt;
            this.btn_no00_01.Height = hgt - 4;
            this.Height = hgt;
            this.pan_hide_00.Height = this.Height;
            this.ResumeLayout(false);
            this.PerformLayout();
            if (this.DBLifeDetail1AResize != null) this.DBLifeDetail1AResize(this, e);
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
            DBLifeDetail1A other = obj as DBLifeDetail1A;
            if (other != null)
            {
                return this.ShrtCnts3.CompareTo(other.ShrtCnts3);
            }
            return 1;
        }
    }
}
