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
    public partial class HeungkukAccidentA : UserControl, IComparable
    {
        public event MouseEventHandler ContentsMouseWheel;
        public event EventHandler ContentsResized;
        ResizeRowManager manager = null;

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

        public string CureSeq { get; set; }
        public string Gubun
        {
            get { return this._gubun; }
            set
            {
                this._gubun = value;
                if (_gubun == "1")
                {
                    txtGubunName.Visible = false;
                    dtiCureFrDt.Visible = false;
                    dtiCureToDt.Visible = false;
                    txtCureCnts.Visible = false;
                    txtVstHosp.Visible = false;
                    panelEx17.Visible = false;
                    panelEx18.Visible = false;
                    lblno00_01.Visible = true;
                    lblno00_02.Visible = true;
                    lblno00_03.Visible = true;
                    lblno00_04.Visible = true;
                }
                else
                {
                    txtGubunName.Visible = true;
                    dtiCureFrDt.Visible = true;
                    dtiCureToDt.Visible = true;
                    txtCureCnts.Visible = true;
                    txtVstHosp.Visible = true;
                    panelEx17.Visible = true;
                    panelEx18.Visible = true;
                    lblno00_01.Visible = false;
                    lblno00_02.Visible = false;
                    lblno00_03.Visible = false;
                    lblno00_04.Visible = false;
                }
            }
        }
        public string GubunName
        {
            get { return txtGubunName.Text; }
            set
            {
                txtGubunName.Text = value;
                lblno00_01.Text = txtGubunName.Text;
            }
        }
        public string CureFrDt
        {
            get { return Utils.DateFormat(dtiCureFrDt.ValueObject, "yyyyMMdd"); }
            set
            {
                dtiCureFrDt.ValueObject = Utils.ConvertToDateTime(value);
                lblno00_02.Text = Utils.DateFormat(dtiCureFrDt.ValueObject, "yyyy-MM-dd");
            }
        }
        public string CureToDt
        {
            get { return Utils.DateFormat(dtiCureToDt.ValueObject, "yyyyMMdd"); }
            set
            {
                dtiCureToDt.ValueObject = Utils.ConvertToDateTime(value);
            }
        }
        public string CureCnts
        {
            get { return txtCureCnts.rtbDoc.Rtf; }
            set
            {
                txtCureCnts.rtbDoc.Rtf = Utils.ConvertToRtf(value);
                lblno00_03.Text = txtCureCnts.rtbDoc.Text;
            }
        }
        public string VstHosp
        {
            get { return txtVstHosp.rtbDoc.Text; }
            set
            {
                txtVstHosp.rtbDoc.Text = value;
                lblno00_04.Text = txtVstHosp.rtbDoc.Text;
            }
        }
        private string _gubun = "";

        public bool IsNewRow { get; set; } = false;

        private HeungkukAccident _parentC = null;
        private bool readOnlyMode = false;

        public HeungkukAccidentA(HeungkukAccident p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();
            base.BorderStyle = BorderStyle.None;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.dtiCureFrDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.dtiCureToDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.txtCureCnts.rtbDoc.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtVstHosp.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtVstHosp.ContentsResized += Txt_ContentsResized;
            this.txtVstHosp.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.txtCureCnts.ContentsResized += Txt_ContentsResized;
            this.txtCureCnts.ContentsMouseWheel += Txt_ContentsMouseWheel;

            manager = new ResizeRowManager(this, this.pan_hide_00);
            manager.AddControl(0, txtGubunName, panelEx17, txtCureCnts, txtVstHosp, panelEx18);
            manager.LockSize(dtiCureFrDt, dtiCureToDt);

            this.txtGubunName.SetReadOnly(true);
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

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            manager.ContentsResized(sender, e);
            if (this.ContentsResized != null) this.ContentsResized(this, e);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            //this.txtGubunName.SetReadOnly(rdonly);
            this.dtiCureFrDt.SetReadOnly(rdonly);
            this.dtiCureToDt.SetReadOnly(rdonly);
            this.txtCureCnts.SetReadOnly(rdonly);
            this.txtVstHosp.SetReadOnly(rdonly);
            this.btnDel.Enabled = !rdonly;
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

        private void Ini_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.IntegerInput ini = (DevComponents.Editors.IntegerInput)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(ini.Text))
            {
                this.IsNewRow = false;
                _parentC.AddEmptyRow();
            }
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;

            if (Gubun == "2")
            {
                string ctrtDt = Utils.DateFormat(_parentC?.Userno1?.Rows[0].CtrtDt, "yyyyMMdd");
                string cureFrDt = Utils.DateFormat(dtiCureFrDt.ValueObject, "yyyyMMdd");
                GubunName = (cureFrDt.CompareTo(ctrtDt) < 0 ? "계약전" : "계약후");
            }

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
            HeungkukAccidentA other = obj as HeungkukAccidentA;
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
