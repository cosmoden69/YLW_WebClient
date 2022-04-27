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
    public partial class DBLifeDetail2A : UserControl, IComparable
    {
        public delegate void DBLfCntrAResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler DBLifeDetail2AMouseWheel;
        public event DBLfCntrAResizeEventHandler DBLifeDetail2AResize;

        public string Title1_Text
        {
            get { return panTitle1.Text; }
            set { panTitle1.Text = value; }
        }
        public string Check1Y_Text
        {
            get { return chk1Y.Text; }
            set { chk1Y.Text = value; }
        }
        public string Check1N_Text
        {
            get { return chk1N.Text; }
            set { chk1N.Text = value; }
        }
        public string Check1
        {
            get
            {
                if (chk1Y.Checked) return "1";
                else if (chk1N.Checked) return "2";
                else return "";
            }
            set
            {
                if (value == "1") chk1Y.Checked = true;
                else if (value == "2") chk1N.Checked = true;
            }
        }
        public string RprtNo { get; set; }
        public bool IsNewRow { get; set; } = false;

        private DBLifeDetail2 _parentC = null;
        private bool readOnlyMode = false;
        private bool _bEvent = false;

        public DBLifeDetail2A(DBLifeDetail2 p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();

            this.Load += DBLifeDetail2A_Load;

            _bEvent = true;
        }

        private void DBLifeDetail2A_Load(object sender, EventArgs e)
        {
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            chk1Y.Enabled = !rdonly;
            chk1N.Enabled = !rdonly;
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
            if (this.DBLifeDetail2AMouseWheel != null) this.DBLifeDetail2AMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            if (this.DBLifeDetail2AResize != null) this.DBLifeDetail2AResize(this, e);
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
            DBLifeDetail2A other = obj as DBLifeDetail2A;
            if (other != null)
            {
                return this.RprtNo.CompareTo(other.RprtNo);
            }
            return 1;
        }
    }
}
