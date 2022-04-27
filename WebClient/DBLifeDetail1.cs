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
    public partial class DBLifeDetail1 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public miDBLifeDetail1Rows Rows = null;

        private bool readOnlyMode = false;

        public DBLifeDetail1()
        {
            InitializeComponent();

            this.Rows = new miDBLifeDetail1Rows(this);
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
            this.Rows = new miDBLifeDetail1Rows(this);
        }

        public DBLifeDetail1A AddEmptyRow()
        {
            DBLifeDetail1A item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            item = this.Rows.Add(true);
            this.Rows.EmptyRow = item;
            item.DBLifeDetail1AMouseWheel += Item_DBLifeDetail1AMouseWheel;
            item.DBLifeDetail1AResize += Item_DBLifeDetail1AResize;
            item.TabIndex = tabindex + 1;
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            this.RefreshControl();
            return item;
        }

        public DBLifeDetail1A AddRow(DataRow drow)
        {
            DBLifeDetail1A item;
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
            item.DBLifeDetail1AMouseWheel += Item_DBLifeDetail1AMouseWheel;
            item.DBLifeDetail1AResize += Item_DBLifeDetail1AResize;
            item.TabIndex = tabindex;
            item.ShrtCnts1 = (!drow.Table.Columns.Contains("ShrtCnts1") ? "" : Utils.ConvertToString(drow["ShrtCnts1"]));         // 항목
            item.Amt1 = (!drow.Table.Columns.Contains("Amt1") ? "" : Utils.ConvertToString(drow["Amt1"]));                      // 비용
            item.ShrtCnts2 = (!drow.Table.Columns.Contains("ShrtCnts2") ? "" : Utils.ConvertToString(drow["ShrtCnts2"]));         // 방문처
            item.ShrtCnts3 = (!drow.Table.Columns.Contains("ShrtCnts3") ? "" : Utils.ConvertToString(drow["ShrtCnts3"]));         // 확인일자
            item.LongCnts1 = (!drow.Table.Columns.Contains("LongCnts1") ? "" : Utils.ConvertToString(drow["LongCnts1"]));         // 첨부자료 및 내용
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            //this.RefreshControl();
            return item;
        }

        private void Item_DBLifeDetail1AMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void Item_DBLifeDetail1AResize(object sender, ContentsResizedEventArgs e)
        {
            this.RefreshControl();
        }

        public void RemoveRow(DBLifeDetail1A item)
        {
            this.Rows.Remove(item);
            this.Controls.Remove(item);
            if (item.IsNewRow) this.AddEmptyRow();
            this.RefreshControl();
        }

        private void RefreshControl()
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

        public void Sort()
        {
            this.Rows.Sort(new DBLifeDetail1AComp());
            this.RefreshControl();
        }

        public void SetFocus()
        {
            this.Rows.FirstRow.Focus();
        }
    }

    public class miDBLifeDetail1Rows
    {
        public int Count { get { return _list.Count; } }
        public DBLifeDetail1A FirstRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[0];
            }
        }
        public DBLifeDetail1A LastRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[this._list.Count - 1];
            }
        }

        public DBLifeDetail1A EmptyRow { get; set; } = null;

        private DBLifeDetail1 _view = null;
        private List<DBLifeDetail1A> _list = null;

        public miDBLifeDetail1Rows(DBLifeDetail1 vw)
        {
            this._view = vw;
            this._list = new List<DBLifeDetail1A>();
        }

        public DBLifeDetail1A this[int index]
        {
            get
            {
                if ((index < 0) || (index > _list.Count - 1)) return null;
                return _list[index];
            }
        }

        public DBLifeDetail1A Add(bool newrow = false)
        {
            DBLifeDetail1A var = new DBLifeDetail1A(_view, newrow);
            _list.Add(var);
            return var;
        }

        public DBLifeDetail1A Insert(int idx)
        {
            DBLifeDetail1A var = new DBLifeDetail1A(_view);
            _list.Insert(idx, var);
            return var;
        }

        public void Remove(int idx)
        {
            _list.RemoveAt(idx);
        }

        public void Remove(DBLifeDetail1A item)
        {
            _list.Remove(item);
        }

        public void Sort(IComparer<DBLifeDetail1A> comp)
        {
            _list.Sort(comp);
        }
    }

    public class DBLifeDetail1AComp : IComparer<DBLifeDetail1A>
    {
        // Compares by Height, Length, and Width.
        public int Compare(DBLifeDetail1A x, DBLifeDetail1A y)
        {
            if (x.IsNewRow) return 1;
            if (y.IsNewRow) return -1;
            int CompareResult = x.ShrtCnts3.CompareTo(y.ShrtCnts3);
            if (CompareResult == 0)
            {
                CompareResult = x.ShrtCnts1.CompareTo(y.ShrtCnts1);
            }
            return CompareResult;
        }
    }
}
