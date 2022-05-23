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
    public partial class MGLossSmplContractA : UserControl
    {
        public delegate void HkCntrAResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler MGLossSmplContractAMouseWheel;
        public event HkCntrAResizeEventHandler MGLossSmplContractAResize;

        public MGLossSmplContractBH HeaderRow = null;
        public miMGLossSmplContractBRows Rows = null;
        public MGLossSmplContractBT TailRow = null;

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
        public string IsrdJob
        {
            get { return txtIsrdJob.Text; }
            set { txtIsrdJob.Text = value; }
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
        public string Bnfc { get; set; }

        public bool IsNewRow { get; set; } = false;
        private MGLossSmplContract _parentC = null;
        private bool readOnlyMode = false;

        public MGLossSmplContractA(MGLossSmplContract p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();

            this.txtInsurPrdt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsurNo.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdJob.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsurant.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsured.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdTel.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdAddressName.TextChanged += new System.EventHandler(this.Text_Change);
            this.dtiCtrtDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.dtiCtrtExprDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.Rows = new miMGLossSmplContractBRows(this);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtInsurPrdt.SetReadOnly(rdonly);
            this.txtInsurNo.SetReadOnly(rdonly);
            this.dtiCtrtDt.SetReadOnly(rdonly);
            this.dtiCtrtExprDt.SetReadOnly(rdonly);
            this.txtIsrdJob.SetReadOnly(rdonly);
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
            this.txtIsrdJob.Text = "";
            this.txtInsurant.Text = "";
            this.txtInsured.Text = "";
            this.txtIsrdTel.Text = "";
            this.txtIsrdAddressName.Text = "";
            for (int ii = 0; ii < this.Rows.Count; ii++)
            {
                this.Controls.Remove(this.Rows[ii]);
            }
            this.Rows = new miMGLossSmplContractBRows(this);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            MGLossSmplContractA other = obj as MGLossSmplContractA;
            if (other != null)
            {
                return 0;
            }
            return 1;
        }

        public MGLossSmplContractBH AddRowHeader()
        {
            HeaderRow = new MGLossSmplContractBH(this);
            this.panel2.Controls.Add(HeaderRow);
            //this.RefreshControl();
            return HeaderRow;
        }

        public MGLossSmplContractBT AddRowTail()
        {
            TailRow = new MGLossSmplContractBT(this);
            this.panel2.Controls.Add(TailRow);
            //this.RefreshControl();
            return TailRow;
        }

        public MGLossSmplContractB AddEmptyRow()
        {
            MGLossSmplContractB item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            item = this.Rows.Add(true);
            this.Rows.EmptyRow = item;
            item.MGLossSmplContractBMouseWheel += Item_MGLossSmplContractBMouseWheel;
            item.MGLossSmplContractBResize += Item_MGLossSmplContractBResize;
            item.MGLossSmplContractBPriceChanged += Item_MGLossSmplContractBPriceChanged;
            item.TabIndex = tabindex + 1;
            item.SetReadOnlyMode(this.readOnlyMode);
            this.panel2.Controls.Add(item);
            this.RefreshControl();
            return item;
        }

        public MGLossSmplContractB AddRow(DataRow drow)
        {
            MGLossSmplContractB item;
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
            item.MGLossSmplContractBMouseWheel += Item_MGLossSmplContractBMouseWheel;
            item.MGLossSmplContractBResize += Item_MGLossSmplContractBResize;
            item.MGLossSmplContractBPriceChanged += Item_MGLossSmplContractBPriceChanged;
            item.TabIndex = tabindex;
            item.CltrCnts = (!drow.Table.Columns.Contains("CltrCnts") ? "" : Utils.ConvertToString(drow["CltrCnts"]));             // 청구담보
            item.InsurRegsAmt = (!drow.Table.Columns.Contains("InsurRegsAmt") ? "" : Utils.ConvertToString(drow["InsurRegsAmt"]));  // 가입금액
            item.SetReadOnlyMode(this.readOnlyMode);
            this.panel2.Controls.Add(item);
            //this.RefreshControl();
            return item;
        }

        private void Item_MGLossSmplContractBMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.MGLossSmplContractAMouseWheel != null) this.MGLossSmplContractAMouseWheel(this, e);
        }

        private void Item_MGLossSmplContractBResize(object sender, ContentsResizedEventArgs e)
        {
            this.RefreshControl();
            if (this.MGLossSmplContractAResize != null) this.MGLossSmplContractAResize(this, e);
        }

        private void Item_MGLossSmplContractBPriceChanged(object sender, EventArgs e)
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

        public void RemoveRow(MGLossSmplContractB item)
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
            if (this.MGLossSmplContractAResize != null) this.MGLossSmplContractAResize(this, new ContentsResizedEventArgs(new Rectangle(0, 0, this.Width, this.Height)));
        }

        public void Sort()
        {
            this.Rows.Sort(new MGLossSmplContractBComp());
            this.RefreshControl();
        }

        public void SetFocus()
        {
            this.Rows.FirstRow.Focus();
        }
    }

    public class miMGLossSmplContractBRows
    {
        public int Count { get { return _list.Count; } }
        public MGLossSmplContractB FirstRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[0];
            }
        }
        public MGLossSmplContractB LastRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[this._list.Count - 1];
            }
        }

        public MGLossSmplContractB EmptyRow { get; set; } = null;

        private MGLossSmplContractA _view = null;
        private List<MGLossSmplContractB> _list = null;

        public miMGLossSmplContractBRows(MGLossSmplContractA vw)
        {
            this._view = vw;
            this._list = new List<MGLossSmplContractB>();
        }

        public MGLossSmplContractB this[int index]
        {
            get
            {
                if ((index < 0) || (index > _list.Count - 1)) return null;
                return _list[index];
            }
        }

        public MGLossSmplContractB Add(bool newrow = false)
        {
            MGLossSmplContractB var = new MGLossSmplContractB(_view, newrow);
            _list.Add(var);
            return var;
        }

        public MGLossSmplContractB Insert(int idx)
        {
            MGLossSmplContractB var = new MGLossSmplContractB(_view);
            _list.Insert(idx, var);
            return var;
        }

        public void Remove(int idx)
        {
            _list.RemoveAt(idx);
        }

        public void Remove(MGLossSmplContractB item)
        {
            _list.Remove(item);
        }

        public void Sort(IComparer<MGLossSmplContractB> comp)
        {
            _list.Sort(comp);
        }
    }

    public class MGLossSmplContractBComp : IComparer<MGLossSmplContractB>
    {
        // Compares by Height, Length, and Width.
        public int Compare(MGLossSmplContractB x, MGLossSmplContractB y)
        {
            if (x.IsNewRow) return 1;
            if (y.IsNewRow) return -1;
            return 0;
        }
    }
}
