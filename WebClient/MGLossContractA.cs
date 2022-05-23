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
    public partial class MGLossContractA : UserControl
    {
        public delegate void HkCntrAResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler MGLossContractAMouseWheel;
        public event HkCntrAResizeEventHandler MGLossContractAResize;

        public MGLossContractBH HeaderRow = null;
        public miMGLossContractBRows Rows = null;
        public MGLossContractBT TailRow = null;

        public string InsurPrdt
        {
            get { return txtInsurPrdt.Text; }
            set { txtInsurPrdt.Text = value; }
        }
        public string InsurNo
        {
            get { return txtInsurNo.Text; }
            set { txtInsurNo.Text = value; }
        }
        public string CtrtDt
        {
            get { return Utils.DateFormat(dtiCtrtDt.ValueObject, "yyyyMMdd"); }
            set { dtiCtrtDt.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string CtrtExprDt
        {
            get { return Utils.DateFormat(dtiCtrtExprDt.ValueObject, "yyyyMMdd"); }
            set { dtiCtrtExprDt.ValueObject = Utils.ConvertToDateTime(value); }
        }
        public string Bnfc
        {
            get { return txtBnfc.Text; }
            set { txtBnfc.Text = value; }
        }
        public string Insurant
        {
            get { return txtInsurant.Text; }
            set { txtInsurant.Text = value; }
        }
        public string Insured
        {
            get { return txtInsured.Text; }
            set { txtInsured.Text = value; }
        }
        public string IsrdTel
        {
            get { return txtIsrdTel.Text; }
            set { txtIsrdTel.Text = value; }
        }
        public string IsrdAddressName
        {
            get { return txtIsrdAddressName.Text; }
            set { txtIsrdAddressName.Text = value; }
        }
        public string CtrtStts { get; set; }
        public string CtrtSttsDt { get; set; }
        public string IsrtRegno1 { get; set; }
        public string IsrtRegno2 { get; set; }
        public string IsrtTel { get; set; }
        public string IsrdRegno1 { get; set; }
        public string IsrdRegno2 { get; set; }
        public string IsrdAddressSeq
        {
            get { return Utils.ConvertToString(txtIsrdAddressName.AddressSeq); }
            set { txtIsrdAddressName.AddressSeq = Utils.ToInt(value); }
        }
        public string IsrdJobGrad { get; set; }
        public string IsrdJobDmnd { get; set; }
        public string IsrdJobGradDmnd { get; set; }
        public string IsrdJobNow { get; set; }
        public string IsrdJobGradNow { get; set; }
        public string IsrdJob { get; set; }

        public bool IsNewRow { get; set; } = false;
        private MGLossContract _parentC = null;
        private bool readOnlyMode = false;

        public MGLossContractA(MGLossContract p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();

            this.txtInsurPrdt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsurNo.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtBnfc.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsurant.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsured.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdTel.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdAddressName.TextChanged += new System.EventHandler(this.Text_Change);
            this.dtiCtrtDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.dtiCtrtExprDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.Rows = new miMGLossContractBRows(this);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtInsurPrdt.SetReadOnly(rdonly);
            this.txtInsurNo.SetReadOnly(rdonly);
            this.dtiCtrtDt.SetReadOnly(rdonly);
            this.dtiCtrtExprDt.SetReadOnly(rdonly);
            this.txtBnfc.SetReadOnly(rdonly);
            this.txtInsurant.SetReadOnly(rdonly);
            this.txtInsured.SetReadOnly(rdonly);
            this.txtIsrdTel.SetReadOnly(rdonly);
            this.txtIsrdAddressName.SetReadOnly(rdonly);
            for (int ii = 0; ii < this.Rows.Count; ii++)
            {
                this.Rows[ii].SetReadOnlyMode(rdonly);
            }
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

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;

            if (this.IsNewRow && !string.IsNullOrEmpty(dat.Text))
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

        public void Clear()
        {
            this.txtInsurPrdt.Text = "";
            this.txtInsurNo.Text = "";
            this.dtiCtrtDt.ValueObject = null;
            this.dtiCtrtExprDt.ValueObject = null;
            this.txtBnfc.Text = "";
            this.txtInsurant.Text = "";
            this.txtInsured.Text = "";
            this.txtIsrdTel.Text = "";
            this.txtIsrdAddressName.Text = "";
            for (int ii = 0; ii < this.Rows.Count; ii++)
            {
                this.Controls.Remove(this.Rows[ii]);
            }
            this.Rows = new miMGLossContractBRows(this);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            MGLossContractA other = obj as MGLossContractA;
            if (other != null)
            {
                return 0;
            }
            return 1;
        }

        public MGLossContractBH AddRowHeader()
        {
            HeaderRow = new MGLossContractBH(this);
            this.panel2.Controls.Add(HeaderRow);
            //this.RefreshControl();
            return HeaderRow;
        }

        public MGLossContractBT AddRowTail()
        {
            TailRow = new MGLossContractBT(this);
            this.panel2.Controls.Add(TailRow);
            //this.RefreshControl();
            return TailRow;
        }

        public MGLossContractB AddEmptyRow()
        {
            MGLossContractB item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            item = this.Rows.Add(true);
            this.Rows.EmptyRow = item;
            item.MGLossContractBMouseWheel += Item_MGLossContractBMouseWheel;
            item.MGLossContractBResize += Item_MGLossContractBResize;
            item.MGLossContractBPriceChanged += Item_MGLossContractBPriceChanged;
            item.TabIndex = tabindex + 1;
            item.SetReadOnlyMode(this.readOnlyMode);
            this.panel2.Controls.Add(item);
            this.RefreshControl();
            return item;
        }

        public MGLossContractB AddRow(DataRow drow)
        {
            MGLossContractB item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            if (this.Rows.LastRow != null)
            {
                this.Rows.LastRow.TabIndex = tabindex + 1;
                if (this.Rows.LastRow == this.Rows.EmptyRow)
                    item = this.Rows.Insert(this.Rows.Count - 1);     //빈 Row 앞에 추가
                else
                    item = this.Rows.Add();
            }
            else
            {
                item = this.Rows.Add();
            }
            item.MGLossContractBMouseWheel += Item_MGLossContractBMouseWheel;
            item.MGLossContractBResize += Item_MGLossContractBResize;
            item.MGLossContractBPriceChanged += Item_MGLossContractBPriceChanged;
            item.TabIndex = tabindex;
            item.CltrCnts = (!drow.Table.Columns.Contains("CltrCnts") ? "" : Utils.ConvertToString(drow["CltrCnts"]));             // 청구담보
            item.InsurRegsAmt = (!drow.Table.Columns.Contains("InsurRegsAmt") ? "" : Utils.ConvertToString(drow["InsurRegsAmt"]));  // 가입금액
            item.SetReadOnlyMode(this.readOnlyMode);
            this.panel2.Controls.Add(item);
            //this.RefreshControl();
            return item;
        }

        private void Item_MGLossContractBMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.MGLossContractAMouseWheel != null) this.MGLossContractAMouseWheel(this, e);
        }

        private void Item_MGLossContractBResize(object sender, ContentsResizedEventArgs e)
        {
            this.RefreshControl();
            if (this.MGLossContractAResize != null) this.MGLossContractAResize(this, e);
        }

        private void Item_MGLossContractBPriceChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        public void CalcSum()
        {
            decimal tot = 0;
            for (int ii = 0; ii < this.Rows.Count; ii++)
            {
                tot += Utils.ToDecimal(this.Rows[ii].InsurRegsAmt);
            }
            TailRow.Sum = tot;
        }

        public void RemoveRow(MGLossContractB item)
        {
            this.Rows.Remove(item);
            this.panel2.Controls.Remove(item);
            if (item.IsNewRow) this.AddEmptyRow();
            this.RefreshControl();
        }

        private void RefreshControl()
        {
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            int hgt = 0;
            int tabindex = 0;
            this.HeaderRow.TabIndex = tabindex++;
            this.HeaderRow.TabStop = false;
            this.HeaderRow.Top = 0; hgt += this.HeaderRow.Height - 1;
            for (int ii = 0; ii < this.Rows.Count; ii++)
            {
                this.Rows[ii].TabIndex = tabindex++;
                this.Rows[ii].Location = new Point(0, hgt);
                hgt += this.Rows[ii].Height - 1;
            }
            this.TailRow.TabIndex = tabindex++;
            this.TailRow.TabStop = false;
            this.TailRow.Top = hgt; hgt += this.TailRow.Height - 1;
            this.panel2.Height = hgt + 1;
            this.Height = this.panel1.Height + this.panel2.Height;
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
            if (this.MGLossContractAResize != null) this.MGLossContractAResize(this, new ContentsResizedEventArgs(new Rectangle(0, 0, this.Width, this.Height)));
        }

        public void Sort()
        {
            this.Rows.Sort(new MGLossContractBComp());
            this.RefreshControl();
        }

        public void SetFocus()
        {
            this.Rows.FirstRow.Focus();
        }
    }

    public class miMGLossContractBRows
    {
        public int Count { get { return _list.Count; } }
        public MGLossContractB FirstRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[0];
            }
        }
        public MGLossContractB LastRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[this._list.Count - 1];
            }
        }

        public MGLossContractB EmptyRow { get; set; } = null;

        private MGLossContractA _view = null;
        private List<MGLossContractB> _list = null;

        public miMGLossContractBRows(MGLossContractA vw)
        {
            this._view = vw;
            this._list = new List<MGLossContractB>();
        }

        public MGLossContractB this[int index]
        {
            get
            {
                if ((index < 0) || (index > _list.Count - 1)) return null;
                return _list[index];
            }
        }

        public MGLossContractB Add(bool newrow = false)
        {
            MGLossContractB var = new MGLossContractB(_view, newrow);
            _list.Add(var);
            return var;
        }

        public MGLossContractB Insert(int idx)
        {
            MGLossContractB var = new MGLossContractB(_view);
            _list.Insert(idx, var);
            return var;
        }

        public void Remove(int idx)
        {
            _list.RemoveAt(idx);
        }

        public void Remove(MGLossContractB item)
        {
            _list.Remove(item);
        }

        public void Sort(IComparer<MGLossContractB> comp)
        {
            _list.Sort(comp);
        }
    }

    public class MGLossContractBComp : IComparer<MGLossContractB>
    {
        // Compares by Height, Length, and Width.
        public int Compare(MGLossContractB x, MGLossContractB y)
        {
            if (x.IsNewRow) return 1;
            if (y.IsNewRow) return -1;
            return 0;
        }
    }
}
