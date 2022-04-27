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
    public partial class DBLifeAccident : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public miDBLifeAccidentRows Rows = null;

        public DBLifeContract Userno1 { get; set; }

        private bool readOnlyMode = false;

        public DBLifeAccident()
        {
            InitializeComponent();

            this.Rows = new miDBLifeAccidentRows(this);
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
            this.Rows = new miDBLifeAccidentRows(this);
        }

        public DBLifeAccidentA AddEmptyRow()
        {
            DBLifeAccidentA item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            item = this.Rows.Add(true);
            item.TabIndex = tabindex + 1;
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            this.RefreshControl();
            return item;
        }

        public DBLifeAccidentA AddRow(object cureSeq, object gubun, object cureFrDt, object cureToDt, object cureCnts, object vstHosp)
        {
            DBLifeAccidentA item;
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
            item.CureCnts = Utils.ConvertToString(cureCnts);
            item.VstHosp = Utils.ConvertToString(vstHosp);
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            //this.RefreshControl();
            return item;
        }

        public void RemoveRow(DBLifeAccidentA item)
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
            this.Rows.Sort(new DBLifeAccidentAComp());
            this.RefreshControl();
        }

        public void SetFocus()
        {
            this.Rows.FirstRow.Focus();
        }
    }

    public class miDBLifeAccidentRows
    {
        public int Count { get { return _list.Count; } }
        public DBLifeAccidentA FirstRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[0];
            }
        }
        public DBLifeAccidentA LastRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[this._list.Count - 1];
            }
        }

        private DBLifeAccident _view = null;
        private List<DBLifeAccidentA> _list = null;

        public miDBLifeAccidentRows(DBLifeAccident vw)
        {
            this._view = vw;
            this._list = new List<DBLifeAccidentA>();
        }

        public DBLifeAccidentA this[int index]
        {
            get
            {
                if ((index < 0) || (index > _list.Count - 1)) return null;
                return _list[index];
            }
        }

        public DBLifeAccidentA Add(bool newrow = false)
        {
            DBLifeAccidentA var = new DBLifeAccidentA(_view, newrow);
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

        public DBLifeAccidentA Insert(int idx)
        {
            DBLifeAccidentA var = new DBLifeAccidentA(_view);
            var.ContentsMouseWheel += Var_ContentsMouseWheel;
            var.ContentsResized += Var_ContentsResized;
            _list.Insert(idx, var);
            return var;
        }

        public void Remove(int idx)
        {
            _list.RemoveAt(idx);
        }

        public void Remove(DBLifeAccidentA item)
        {
            _list.Remove(item);
        }

        public void Sort(IComparer<DBLifeAccidentA> comp)
        {
            _list.Sort(comp);
        }
    }

    public class DBLifeAccidentAComp : IComparer<DBLifeAccidentA>
    {
        // Compares by Height, Length, and Width.
        public int Compare(DBLifeAccidentA x, DBLifeAccidentA y)
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
