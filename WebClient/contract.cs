﻿using System;
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
    public partial class contract : UserControl
    {
        public miContractRows Rows = null;

        private bool readOnlyMode = false;

        public contract()
        {
            InitializeComponent();

            this.Rows = new miContractRows(this);
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
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

        public void Clear()
        {
            for (int ii = 0; ii < this.Rows.Count; ii++)
            {
                this.Controls.Remove(this.Rows[ii]);
            }
            this.Rows = new miContractRows(this);
        }

        public contractA AddEmptyRow()
        {
            contractA item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            item = this.Rows.Add(true);
            this.Rows.EmptyRow = item;
            item.TabIndex = tabindex + 1;
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            this.RefreshControl();
            return item;
        }

        public contractA AddRow(DataRow drow)
        {
            contractA item;
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
            item.TabIndex = tabindex;
            item.InsurPrdt = (!drow.Table.Columns.Contains("InsurPrdt") ? "" : Utils.ConvertToString(drow["InsurPrdt"]));     // 보험종목
            item.InsurNo = (!drow.Table.Columns.Contains("InsurNo") ? "" : Utils.ConvertToString(drow["InsurNo"]));           // 증권번호
            item.CtrtDt = (!drow.Table.Columns.Contains("CtrtDt") ? "" : Utils.ConvertToString(drow["CtrtDt"]));              // 계약일자
            item.Insurant = (!drow.Table.Columns.Contains("Insurant") ? "" : Utils.ConvertToString(drow["Insurant"]));        // 계약자
            item.Insured = (!drow.Table.Columns.Contains("Insured") ? "" : Utils.ConvertToString(drow["Insured"]));           // 피보험자
            item.Bnfc = (!drow.Table.Columns.Contains("Bnfc") ? "" : Utils.ConvertToString(drow["Bnfc"]));                    // 수익자
            item.IsrdJob = (!drow.Table.Columns.Contains("IsrdJob") ? "" : Utils.ConvertToString(drow["IsrdJob"]));           // 직업
            item.CltrCnts = (!drow.Table.Columns.Contains("CltrCnts") ? "" : Utils.ConvertToString(drow["CltrCnts"]));        // 담보내용
            item.InsurValue = (!drow.Table.Columns.Contains("InsurValue") ? "" : Utils.ConvertToString(drow["InsurValue"]));  // 담보금액
            //추가정보
            item.CtrtExprDt = (!drow.Table.Columns.Contains("CtrtExprDt") ? "" : Utils.ConvertToString(drow["CtrtExprDt"]));
            item.CtrtStts = (!drow.Table.Columns.Contains("CtrtStts") ? "" : Utils.ConvertToString(drow["CtrtStts"]));
            item.CtrtSttsDt = (!drow.Table.Columns.Contains("CtrtSttsDt") ? "" : Utils.ConvertToString(drow["CtrtSttsDt"]));
            item.IsrtRegno1 = (!drow.Table.Columns.Contains("IsrtRegno1") ? "" : Utils.ConvertToString(drow["IsrtRegno1"]));
            item.IsrtRegno2 = (!drow.Table.Columns.Contains("IsrtRegno2") ? "" : Utils.ConvertToString(drow["IsrtRegno2"]));
            item.IsrtTel = (!drow.Table.Columns.Contains("IsrtTel") ? "" : Utils.ConvertToString(drow["IsrtTel"]));
            item.IsrdRegno1 = (!drow.Table.Columns.Contains("IsrdRegno1") ? "" : Utils.ConvertToString(drow["IsrdRegno1"]));
            item.IsrdRegno2 = (!drow.Table.Columns.Contains("IsrdRegno2") ? "" : Utils.ConvertToString(drow["IsrdRegno2"]));
            item.IsrdTel = (!drow.Table.Columns.Contains("IsrdTel") ? "" : Utils.ConvertToString(drow["IsrdTel"]));
            item.IsrdAddressSeq = (!drow.Table.Columns.Contains("IsrdAddressSeq") ? "" : Utils.ConvertToString(drow["IsrdAddressSeq"]));
            item.IsrdAddressName = (!drow.Table.Columns.Contains("IsrdAddressName") ? "" : Utils.ConvertToString(drow["IsrdAddressName"]));
            item.IsrdJobGrad = (!drow.Table.Columns.Contains("IsrdJobGrad") ? "" : Utils.ConvertToString(drow["IsrdJobGrad"]));
            item.IsrdJobDmnd = (!drow.Table.Columns.Contains("IsrdJobDmnd") ? "" : Utils.ConvertToString(drow["IsrdJobDmnd"]));
            item.IsrdJobGradDmnd = (!drow.Table.Columns.Contains("IsrdJobGradDmnd") ? "" : Utils.ConvertToString(drow["IsrdJobGradDmnd"]));
            item.IsrdJobNow = (!drow.Table.Columns.Contains("IsrdJobNow") ? "" : Utils.ConvertToString(drow["IsrdJobNow"]));
            item.IsrdJobGradNow = (!drow.Table.Columns.Contains("IsrdJobGradNow") ? "" : Utils.ConvertToString(drow["IsrdJobGradNow"]));
            //추가정보
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            //this.RefreshControl();
            return item;
        }

        public void RemoveRow(contractA item)
        {
            this.Rows.Remove(item);
            this.Controls.Remove(item);
            if (item.IsNewRow) this.AddEmptyRow();
            this.RefreshControl();
        }

        private void RefreshControl()
        {
            this.SuspendLayout();
            int height = 23;
            int tabindex = 0;
            for (int ii = 0; ii < this.Rows.Count; ii++)
            {
                this.Rows[ii].TabIndex = tabindex++;
                this.Rows[ii].Location = new Point(0, height);
                height += this.Rows[ii].Height - 1;
            }
            this.Height = height + 1;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Sort()
        {
            this.Rows.Sort(new ContractAComp());
            this.RefreshControl();
        }

        public void SetFocus()
        {
            this.Rows.FirstRow.Focus();
        }
    }

    public class miContractRows
    {
        public int Count { get { return _list.Count; } }
        public contractA FirstRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[0];
            }
        }
        public contractA LastRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[this._list.Count - 1];
            }
        }

        public contractA EmptyRow { get; set; } = null;

        private contract _view = null;
        private List<contractA> _list = null;

        public miContractRows(contract vw)
        {
            this._view = vw;
            this._list = new List<contractA>();
        }

        public contractA this[int index]
        {
            get
            {
                if ((index < 0) || (index > _list.Count - 1)) return null;
                return _list[index];
            }
        }

        public contractA Add(bool newrow = false)
        {
            contractA var = new contractA(_view, newrow);
            _list.Add(var);
            return var;
        }

        public contractA Insert(int idx)
        {
            contractA var = new contractA(_view);
            _list.Insert(idx, var);
            return var;
        }

        public void Remove(int idx)
        {
            _list.RemoveAt(idx);
        }

        public void Remove(contractA item)
        {
            _list.Remove(item);
        }

        public void Sort(IComparer<contractA> comp)
        {
            _list.Sort(comp);
        }
    }

    public class ContractAComp : IComparer<contractA>
    {
        // Compares by Height, Length, and Width.
        public int Compare(contractA x, contractA y)
        {
            if (x.IsNewRow) return 1;
            if (y.IsNewRow) return -1;
            int CompareResult = x.CtrtDt.CompareTo(y.CtrtDt);
            if (CompareResult == 0)
            {
                CompareResult = x.InsurNo.CompareTo(y.InsurNo);
            }
            return CompareResult;
        }
    }
}
