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
    public partial class HyundaiAccident : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public miHyundaiAccidentRows Rows = null;

        public HyndContract Userno1 { get; set; }

        private bool readOnlyMode = false;

        public HyundaiAccident()
        {
            InitializeComponent();

            this.Rows = new miHyundaiAccidentRows(this);
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
            this.Rows = new miHyundaiAccidentRows(this);
        }

        public HyundaiAccidentA AddEmptyRow()
        {
            HyundaiAccidentA item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            item = this.Rows.Add(true);
            item.TabIndex = tabindex + 1;
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            this.RefreshControl();
            return item;
        }

        public HyundaiAccidentA AddRow(object cureSeq, object gubun, object cureFrDt, object cureToDt, object outHospDay, object inHospDay, object cureCnts, object testNmRslt, object vstHosp, object bfGivCnts, object prvSrc)
        {
            HyundaiAccidentA item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            if (this.Rows.LastRow != null)
            {
                this.Rows.LastRow.TabIndex = tabindex + 1;
                item = this.Rows.Insert(this.Rows.Count - 1);     //빈 Row 앞에 추가
            }
            else
            {
                item = this.Rows.Add();
            }
            item.TabIndex = tabindex;
            item.CureSeq = Utils.ConvertToString(cureSeq);
            item.GubunName = Utils.ConvertToString(gubun);
            item.CureFrDt = Utils.ConvertToString(cureFrDt);
            item.CureToDt = Utils.ConvertToString(cureToDt);
            item.OutHospDay = Utils.ConvertToString(outHospDay);
            item.InHospDay = Utils.ConvertToString(inHospDay);
            item.CureCnts = Utils.ConvertToString(cureCnts);
            item.TestNmRslt = Utils.ConvertToString(testNmRslt);
            item.VstHosp = Utils.ConvertToString(vstHosp);
            item.BfGivCnts = Utils.ConvertToString(bfGivCnts);
            item.PrvSrc = Utils.ConvertToString(prvSrc);
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            //this.RefreshControl();
            return item;
        }

        public void RemoveRow(HyundaiAccidentA item)
        {
            this.Rows.Remove(item);
            this.Controls.Remove(item);
            if (item.IsNewRow) this.AddEmptyRow();
            this.RefreshControl();
        }

        public void RefreshControl()
        {
            this.SuspendLayout();
            int height = 45;
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

        public void ScrollMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(sender, e);
        }

        public void Sort()
        {
            this.Rows.Sort(new HyundaiAccidentAComp());
            this.RefreshControl();
        }

        public void SetFocus()
        {
            this.Rows.FirstRow.Focus();
        }
    }

    public class miHyundaiAccidentRows
    {
        public int Count { get { return _list.Count; } }
        public HyundaiAccidentA FirstRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[0];
            }
        }
        public HyundaiAccidentA LastRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[this._list.Count - 1];
            }
        }

        private HyundaiAccident _view = null;
        private List<HyundaiAccidentA> _list = null;

        public miHyundaiAccidentRows(HyundaiAccident vw)
        {
            this._view = vw;
            this._list = new List<HyundaiAccidentA>();
        }

        public HyundaiAccidentA this[int index]
        {
            get
            {
                if ((index < 0) || (index > _list.Count - 1)) return null;
                return _list[index];
            }
        }

        public HyundaiAccidentA Add(bool newrow = false)
        {
            HyundaiAccidentA var = new HyundaiAccidentA(_view, newrow);
            var.ContentsMouseWheel += Var_ContentsMouseWheel;
            var.ContentsResized += Var_ContentsResized;
            _list.Add(var);
            return var;
        }

        private void Var_ContentsResized(object sender, EventArgs e)
        {
            this._view.RefreshControl();
        }

        private void Var_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            this._view.ScrollMouseWheel(sender, e);
        }

        public HyundaiAccidentA Insert(int idx)
        {
            HyundaiAccidentA var = new HyundaiAccidentA(_view);
            var.ContentsMouseWheel += Var_ContentsMouseWheel;
            var.ContentsResized += Var_ContentsResized;
            _list.Insert(idx, var);
            return var;
        }

        public void Remove(int idx)
        {
            _list.RemoveAt(idx);
        }

        public void Remove(HyundaiAccidentA item)
        {
            _list.Remove(item);
        }

        public void Sort(IComparer<HyundaiAccidentA> comp)
        {
            _list.Sort(comp);
        }
    }

    public class HyundaiAccidentAComp : IComparer<HyundaiAccidentA>
    {
        // Compares by Height, Length, and Width.
        public int Compare(HyundaiAccidentA x, HyundaiAccidentA y)
        {
            if (x.IsNewRow) return 1;
            if (y.IsNewRow) return -1;
            int CompareResult = x.CureFrDt.CompareTo(y.CureFrDt);
            if (CompareResult == 0)
            {
                CompareResult = x.Gubun.CompareTo(y.Gubun);
                if (CompareResult == 0)
                {
                    CompareResult = Utils.ToInt(x.CureSeq) - Utils.ToInt(y.CureSeq);
                }
            }
            return CompareResult;
        }
    }
}
