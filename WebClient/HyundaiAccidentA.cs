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
    public partial class HyundaiAccidentA : UserControl, IComparable
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
                    panelEx15.Visible = false;
                    dtiCureFrDt.Visible = false;
                    dtiCureToDt.Visible = false;
                    panelEx16.Visible = false;
                    iniOutHospDay.Visible = false;
                    iniInHospDay.Visible = false;
                    txtCureCnts.Visible = false;
                    panelEx17.Visible = false;
                    txtTestNmRslt.Visible = false;
                    txtVstHosp.Visible = false;
                    txtBfGivCnts.Visible = false;
                    txtPrvSrc.Visible = false;
                    panelEx18.Visible = false;
                    lblno00_01.Visible = true;
                    lblno00_02.Visible = true;
                    lblno00_04.Visible = true;
                    lblno00_05.Visible = true;
                }
                else
                {
                    txtGubunName.Visible = true;
                    panelEx15.Visible = true;
                    dtiCureFrDt.Visible = true;
                    dtiCureToDt.Visible = true;
                    panelEx16.Visible = true;
                    iniOutHospDay.Visible = true;
                    iniInHospDay.Visible = true;
                    txtCureCnts.Visible = true;
                    panelEx17.Visible = true;
                    txtTestNmRslt.Visible = true;
                    txtVstHosp.Visible = true;
                    txtBfGivCnts.Visible = true;
                    txtPrvSrc.Visible = true;
                    panelEx18.Visible = true;
                    lblno00_01.Visible = false;
                    lblno00_02.Visible = false;
                    lblno00_04.Visible = false;
                    lblno00_05.Visible = false;
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
        public string OutHospDay
        {
            get { return Utils.ConvertToString(iniOutHospDay.ValueObject); }
            set { iniOutHospDay.ValueObject = Utils.ToInt(value); }
        }
        public string InHospDay
        {
            get { return Utils.ConvertToString(iniInHospDay.ValueObject); }
            set { iniInHospDay.ValueObject = Utils.ToInt(value); }
        }
        public string CureCnts
        {
            get { return txtCureCnts.rtbDoc.Rtf; }
            set
            {
                txtCureCnts.rtbDoc.Rtf = Utils.ConvertToRtf(value);
                lblno00_04.Text = txtCureCnts.rtbDoc.Text;
            }
        }
        public string TestNmRslt
        {
            get { return txtTestNmRslt.Text; }
            set { txtTestNmRslt.Text = value; }
        }
        public string VstHosp
        {
            get { return txtVstHosp.Text; }
            set
            {
                txtVstHosp.Text = value;
                lblno00_05.Text = txtVstHosp.Text;
            }
        }
        public string BfGivCnts
        {
            get { return txtBfGivCnts.Text; }
            set { txtBfGivCnts.Text = value; }
        }
        public string PrvSrc
        {
            get { return txtPrvSrc.Text; }
            set { txtPrvSrc.Text = value; }
        }
        private string _gubun = "";

        public bool IsNewRow { get; set; } = false;

        private HyundaiAccident _parentC = null;
        private bool readOnlyMode = false;

        public HyundaiAccidentA(HyundaiAccident p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();
            base.BorderStyle = BorderStyle.None;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.dtiCureFrDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.dtiCureToDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.iniOutHospDay.ValueChanged += new System.EventHandler(this.Ini_Change);
            this.iniInHospDay.ValueChanged += new System.EventHandler(this.Ini_Change);
            this.txtCureCnts.rtbDoc.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtTestNmRslt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtVstHosp.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtBfGivCnts.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtPrvSrc.TextChanged += new System.EventHandler(this.Text_Change);

            this.txtCureCnts.ContentsResized += Txt_ContentsResized;
            this.txtCureCnts.ContentsMouseWheel += Txt_ContentsMouseWheel;

            manager = new ResizeRowManager(this, this.pan_hide_00);
            manager.AddControl(0, txtGubunName, panelEx15, panelEx16, txtCureCnts, panelEx17, txtBfGivCnts, txtPrvSrc, panelEx18);
            manager.LockSize(dtiCureFrDt, dtiCureToDt, iniOutHospDay, iniInHospDay, txtTestNmRslt, txtVstHosp);

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
            int hgt = e.NewRectangle.Height;
            hgt = hgt - txtVstHosp.Top;
            txtVstHosp.MinimumSize = new Size(0, hgt);
            txtVstHosp.Height = hgt;
            if (this.ContentsResized != null) this.ContentsResized(this, e);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            //this.txtGubunName.SetReadOnly(rdonly);
            this.dtiCureFrDt.SetReadOnly(rdonly);
            this.dtiCureToDt.SetReadOnly(rdonly);
            this.iniOutHospDay.SetReadOnly(rdonly);
            this.iniInHospDay.SetReadOnly(rdonly);
            this.txtCureCnts.SetReadOnly(rdonly);
            this.txtTestNmRslt.SetReadOnly(rdonly);
            this.txtVstHosp.SetReadOnly(rdonly);
            this.txtBfGivCnts.SetReadOnly(rdonly);
            this.txtPrvSrc.SetReadOnly(rdonly);
            this.btnDel.Enabled = !rdonly;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Return)
            {
                if (this.ActiveControl == txtVstHosp)
                {
                    SendKeys.Send("^{ENTER}");
                    return true;
                }
                else

                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
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
            HyundaiAccidentA other = obj as HyundaiAccidentA;
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
