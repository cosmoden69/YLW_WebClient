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
    public partial class accidentA : UserControl, IComparable
    {
        public event MouseEventHandler ContentsMouseWheel;
        public event EventHandler ContentsResized;

        private BorderStyle border;

        public new BorderStyle BorderStyle
        {
            get { return border; }
            set
            {
                border = value;
                Invalidate();
            }
        }

        public string CureFrDt
        {
            get { return Utils.DateFormat(dti_no00_01.ValueObject, "yyyyMMdd"); }
            set
            {
                dti_no00_01.ValueObject = Utils.ConvertToDateTime(value);
                lblno00_01.Text = Utils.DateFormat(dti_no00_01.ValueObject, "yyyy-MM-dd");
            }
        }
        public string CureSeq
        {
            get { return ini_no00_01.Text; }
            set
            {
                ini_no00_01.Text = value;
                lblno00_02.Text = ini_no00_01.Text;
            }
        }
        public string CureCnts
        {
            get { return txt_no00_01.rtbDoc.Rtf; }
            set
            {
                txt_no00_01.rtbDoc.Rtf = Utils.ConvertToRtf(value);
                lblno00_03.Text = txt_no00_01.rtbDoc.Text;
            }
        }
        public string ObjAgnc
        {
            get { return txt_no00_02.Text; }
            set
            {
                txt_no00_02.Text = value;
                lblno00_04.Text = txt_no00_02.Text;
            }
        }
        public string Gubun
        {
            get { return this._gubun; }
            set
            {
                this._gubun = value;
                if (_gubun == "1")
                {
                    dti_no00_01.Visible = false;
                    ini_no00_01.Visible = false;
                    txt_no00_01.Visible = false;
                    txt_no00_02.Visible = false;
                    panelEx18.Visible = false;
                    lblno00_01.Visible = true;
                    lblno00_02.Visible = true;
                    lblno00_03.Visible = true;
                    lblno00_04.Visible = true;
                    lblno00_05.Visible = true;
                }
                else
                {
                    dti_no00_01.Visible = true;
                    ini_no00_01.Visible = true;
                    txt_no00_01.Visible = true;
                    txt_no00_02.Visible = true;
                    panelEx18.Visible = true;
                    lblno00_01.Visible = false;
                    lblno00_02.Visible = false;
                    lblno00_03.Visible = false;
                    lblno00_04.Visible = false;
                    lblno00_05.Visible = false;
                }
            }
        }
        private string _gubun = "";

        public bool IsNewRow { get; set; } = false;

        private accident _parentC = null;
        private bool readOnlyMode = false;

        public accidentA(accident p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();
            base.BorderStyle = BorderStyle.None;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.txt_no00_01.ContentsResized += Txt_no00_01_ContentsResized;
            this.txt_no00_01.ContentsMouseWheel += Txt_ContentsMouseWheel;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.BorderStyle == BorderStyle.FixedSingle)
            {
                //ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
                Rectangle rect = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
                e.Graphics.DrawRectangle(new Pen(Color.LightGray), rect);
            }
        }

        private void Txt_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void Txt_no00_01_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.pan_hide_00.Height = e.NewRectangle.Height + 2;
            this.Height = this.pan_hide_00.Height + 1;
            if (this.ContentsResized != null) this.ContentsResized(this, e);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.dti_no00_01.SetReadOnly(rdonly);
            this.ini_no00_01.SetReadOnly(rdonly);
            this.txt_no00_01.rtbDoc.ReadOnly = rdonly;
            this.txt_no00_02.SetReadOnly(rdonly);
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
            DevComponents.Editors.IntegerInput amt = (DevComponents.Editors.IntegerInput)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(amt.Text))
            {
                this.IsNewRow = false;
                _parentC.AddEmptyRow();
            }
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
            accidentA other = obj as accidentA;
            if (other != null)
            {
                int CompareResult = this.CureFrDt.CompareTo(other.CureFrDt);
                if (CompareResult == 0)
                {
                    CompareResult = Utils.ToInt(this.CureSeq) - Utils.ToInt(other.CureSeq);
                }
                return CompareResult;
            }
            return 1;
        }
    }
}
