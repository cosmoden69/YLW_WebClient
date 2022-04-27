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
    public partial class DBLossAccident : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public miDBLossAccidentRows Rows = null;

        public DBLossContract Userno1 { get; set; }

        private bool readOnlyMode = false;

        public DBLossAccident()
        {
            InitializeComponent();

            this.Rows = new miDBLossAccidentRows(this);
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
            this.Rows = new miDBLossAccidentRows(this);
        }

        public DBLossAccidentA AddEmptyRow()
        {
            DBLossAccidentA item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            item = this.Rows.Add(true);
            item.TabIndex = tabindex + 1;
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            this.RefreshControl();
            return item;
        }

        public DBLossAccidentA AddRow(object cureSeq, object gubun, object cureFrDt, object cureCnts, object vstHosp)
        {
            DBLossAccidentA item;
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
            item.CureCnts = Utils.ConvertToString(cureCnts);
            item.VstHosp = Utils.ConvertToString(vstHosp);
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            //this.RefreshControl();
            return item;
        }

        public void RemoveRow(DBLossAccidentA item)
        {
            this.Rows.Remove(item);
            this.Controls.Remove(item);
            if (item.IsNewRow) this.AddEmptyRow();
            this.RefreshControl();
        }

        public void RefreshControl()
        {
            this.SuspendLayout();
            int height = 22;
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
            this.Rows.Sort(new DBLossAccidentAComp());
            this.RefreshControl();
        }

        public void SetFocus()
        {
            this.Rows.FirstRow.Focus();
        }
    }

    public class miDBLossAccidentRows
    {
        public int Count { get { return _list.Count; } }
        public DBLossAccidentA FirstRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[0];
            }
        }
        public DBLossAccidentA LastRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[this._list.Count - 1];
            }
        }

        private DBLossAccident _view = null;
        private List<DBLossAccidentA> _list = null;

        public miDBLossAccidentRows(DBLossAccident vw)
        {
            this._view = vw;
            this._list = new List<DBLossAccidentA>();
        }

        public DBLossAccidentA this[int index]
        {
            get
            {
                if ((index < 0) || (index > _list.Count - 1)) return null;
                return _list[index];
            }
        }

        public DBLossAccidentA Add(bool newrow = false)
        {
            DBLossAccidentA var = new DBLossAccidentA(_view, newrow);
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

        public DBLossAccidentA Insert(int idx)
        {
            DBLossAccidentA var = new DBLossAccidentA(_view);
            var.ContentsMouseWheel += Var_ContentsMouseWheel;
            var.ContentsResized += Var_ContentsResized;
            _list.Insert(idx, var);
            return var;
        }

        public void Remove(int idx)
        {
            _list.RemoveAt(idx);
        }

        public void Remove(DBLossAccidentA item)
        {
            _list.Remove(item);
        }

        public void Sort(IComparer<DBLossAccidentA> comp)
        {
            _list.Sort(comp);
        }
    }

    public class DBLossAccidentAComp : IComparer<DBLossAccidentA>
    {
        // Compares by Height, Length, and Width.
        public int Compare(DBLossAccidentA x, DBLossAccidentA y)
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
