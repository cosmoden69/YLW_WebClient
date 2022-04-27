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
    public partial class HeungkukContractA : UserControl
    {
        public delegate void HkCntrAResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler HeungkukContractAMouseWheel;
        public event HkCntrAResizeEventHandler HeungkukContractAResize;

        public miHeungkukContractBRows Rows = null;

        public string InsurPrdt
        {
            get { return txtInsurPrdt.Text; }
            set { txtInsurPrdt.Text = value; }
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
        public string InsurNo
        {
            get { return txtInsurNo.Text; }
            set { txtInsurNo.Text = value; }
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
        public string Bnfc
        {
            get { return txtBnfc.Text; }
            set { txtBnfc.Text = value; }
        }
        public string IsrdJob
        {
            get { return txtIsrdJob.Text; }
            set { txtIsrdJob.Text = value; }
        }
        public string IsrdJobDmnd
        {
            get { return txtIsrdJobDmnd.Text; }
            set { txtIsrdJobDmnd.Text = value; }
        }
        public string CtrtStts { get; set; }
        public string CtrtSttsDt { get; set; }
        public string IsrtRegno1 { get; set; }
        public string IsrtRegno2 { get; set; }
        public string IsrtTel { get; set; }
        public string IsrdRegno1 { get; set; }
        public string IsrdRegno2 { get; set; }
        public string IsrdTel { get; set; }
        public string IsrdAddressSeq { get; set; }
        public string IsrdAddressName { get; set; }
        public string IsrdJobGrad { get; set; }
        public string IsrdJobGradDmnd { get; set; }
        public string IsrdJobNow { get; set; }
        public string IsrdJobGradNow { get; set; }

        public bool IsNewRow { get; set; } = false;
        private HeungkukContract _parentC = null;
        private bool readOnlyMode = false;

        public HeungkukContractA(HeungkukContract p, bool newrow = false)
        {
            this._parentC = p;
            this.IsNewRow = newrow;

            InitializeComponent();

            this.txtInsurPrdt.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsurNo.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsurant.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtInsured.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtBnfc.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdJob.TextChanged += new System.EventHandler(this.Text_Change);
            this.txtIsrdJobDmnd.TextChanged += new System.EventHandler(this.Text_Change);
            this.dtiCtrtDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.dtiCtrtExprDt.ValueChanged += new System.EventHandler(this.Date_Change);
            this.Rows = new miHeungkukContractBRows(this);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.txtInsurPrdt.SetReadOnly(rdonly);
            this.dtiCtrtDt.SetReadOnly(rdonly);
            this.dtiCtrtExprDt.SetReadOnly(rdonly);
            this.txtInsurNo.SetReadOnly(rdonly);
            this.txtInsurant.SetReadOnly(rdonly);
            this.txtInsured.SetReadOnly(rdonly);
            this.txtBnfc.SetReadOnly(rdonly);
            this.txtIsrdJob.SetReadOnly(rdonly);
            this.txtIsrdJobDmnd.SetReadOnly(rdonly);
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
            this.dtiCtrtDt.ValueObject = null;
            this.dtiCtrtExprDt.ValueObject = null;
            this.txtInsurNo.Text = "";
            this.txtInsurant.Text = "";
            this.txtInsured.Text = "";
            this.txtBnfc.Text = "";
            this.txtIsrdJob.Text = "";
            this.txtIsrdJobDmnd.Text = "";
            for (int ii = 0; ii < this.Rows.Count; ii++)
            {
                this.Controls.Remove(this.Rows[ii]);
            }
            this.Rows = new miHeungkukContractBRows(this);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            HeungkukContractA other = obj as HeungkukContractA;
            if (other != null)
            {
                return 0;
            }
            return 1;
        }

        public HeungkukContractB AddEmptyRow()
        {
            HeungkukContractB item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            item = this.Rows.Add(true);
            this.Rows.EmptyRow = item;
            item.HeungkukContractBMouseWheel += Item_HeungkukContractBMouseWheel;
            item.HeungkukContractBResize += Item_HeungkukContractBResize;
            item.TabIndex = tabindex + 1;
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            this.RefreshControl();
            return item;
        }

        public HeungkukContractB AddRow(DataRow drow)
        {
            HeungkukContractB item;
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
            item.HeungkukContractBMouseWheel += Item_HeungkukContractBMouseWheel;
            item.HeungkukContractBResize += Item_HeungkukContractBResize;
            item.TabIndex = tabindex;
            item.CltrCnts = (!drow.Table.Columns.Contains("CltrCnts") ? "" : Utils.ConvertToString(drow["CltrCnts"]));             // 청구담보
            item.InsurRegsAmt = (!drow.Table.Columns.Contains("InsurRegsAmt") ? "" : Utils.ConvertToString(drow["InsurRegsAmt"]));  // 가입금액
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            //this.RefreshControl();
            return item;
        }

        private void Item_HeungkukContractBMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.HeungkukContractAMouseWheel != null) this.HeungkukContractAMouseWheel(this, e);
        }

        private void Item_HeungkukContractBResize(object sender, ContentsResizedEventArgs e)
        {
            this.RefreshControl();
            if (this.HeungkukContractAResize != null) this.HeungkukContractAResize(this, e);
        }

        public void RemoveRow(HeungkukContractB item)
        {
            this.Rows.Remove(item);
            this.Controls.Remove(item);
            if (item.IsNewRow) this.AddEmptyRow();
            this.RefreshControl();
        }

        private void RefreshControl()
        {
            this.SuspendLayout();
            int height = 270;
            int tabindex = 99;
            for (int ii = 0; ii < this.Rows.Count; ii++)
            {
                this.Rows[ii].TabIndex = tabindex++;
                this.Rows[ii].Location = new Point(0, height);
                height += this.Rows[ii].Height - 1;
            }
            this.Height = height + 1;
            this.ResumeLayout(false);
            this.PerformLayout();
            if (this.HeungkukContractAResize != null) this.HeungkukContractAResize(this, new ContentsResizedEventArgs(new Rectangle(0, 0, this.Width, this.Height)));
        }

        public void Sort()
        {
            this.Rows.Sort(new HeungkukContractBComp());
            this.RefreshControl();
        }

        public void SetFocus()
        {
            this.Rows.FirstRow.Focus();
        }
    }

    public class miHeungkukContractBRows
    {
        public int Count { get { return _list.Count; } }
        public HeungkukContractB FirstRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[0];
            }
        }
        public HeungkukContractB LastRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[this._list.Count - 1];
            }
        }

        public HeungkukContractB EmptyRow { get; set; } = null;

        private HeungkukContractA _view = null;
        private List<HeungkukContractB> _list = null;

        public miHeungkukContractBRows(HeungkukContractA vw)
        {
            this._view = vw;
            this._list = new List<HeungkukContractB>();
        }

        public HeungkukContractB this[int index]
        {
            get
            {
                if ((index < 0) || (index > _list.Count - 1)) return null;
                return _list[index];
            }
        }

        public HeungkukContractB Add(bool newrow = false)
        {
            HeungkukContractB var = new HeungkukContractB(_view, newrow);
            _list.Add(var);
            return var;
        }

        public HeungkukContractB Insert(int idx)
        {
            HeungkukContractB var = new HeungkukContractB(_view);
            _list.Insert(idx, var);
            return var;
        }

        public void Remove(int idx)
        {
            _list.RemoveAt(idx);
        }

        public void Remove(HeungkukContractB item)
        {
            _list.Remove(item);
        }

        public void Sort(IComparer<HeungkukContractB> comp)
        {
            _list.Sort(comp);
        }
    }

    public class HeungkukContractBComp : IComparer<HeungkukContractB>
    {
        // Compares by Height, Length, and Width.
        public int Compare(HeungkukContractB x, HeungkukContractB y)
        {
            if (x.IsNewRow) return 1;
            if (y.IsNewRow) return -1;
            return 0;
        }
    }
}
