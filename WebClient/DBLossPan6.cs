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
    public partial class DBLossPan6 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public miDBLossPan6Rows Rows = null;

        private bool readOnlyMode = false;
        private DataTable gdt = null;

        public DBLossPan6()
        {
            InitializeComponent();

            this.Rows = new miDBLossPan6Rows(this);
        }

        public void Init_Set(DataTable pdt)
        {
            gdt = pdt;
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
            this.Rows = new miDBLossPan6Rows(this);
        }

        public DBLossPan6A AddEmptyRow()
        {
            DBLossPan6A item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            item = this.Rows.Add(true);
            this.Rows.EmptyRow = item;
            item.DBLossPan6AMouseWheel += Item_DBLossPan6AMouseWheel;
            item.DBLossPan6AResize += Item_DBLossPan6AResize;
            item.TabIndex = tabindex + 1;
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            this.RefreshControl();
            return item;
        }

        public DBLossPan6A AddRow(DataRow drow)
        {
            DBLossPan6A item;
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
            item.DBLossPan6AMouseWheel += Item_DBLossPan6AMouseWheel;
            item.DBLossPan6AResize += Item_DBLossPan6AResize;
            item.TabIndex = tabindex;
            item.ShrtCnts1 = (!drow.Table.Columns.Contains("ShrtCnts1") ? "" : Utils.ConvertToString(drow["ShrtCnts1"]));            // 조사쟁점 대(L1)
            item.ShrtCnts2 = (!drow.Table.Columns.Contains("ShrtCnts2") ? "" : Utils.ConvertToString(drow["ShrtCnts2"]));            // 조사쟁점 대(L2)
            item.ShrtCnts3 = (!drow.Table.Columns.Contains("ShrtCnts3") ? "" : Utils.ConvertToString(drow["ShrtCnts3"]));            // 조사자 확인
            item.LongCnts1 = (!drow.Table.Columns.Contains("LongCnts1") ? "" : Utils.ConvertToString(drow["LongCnts1"]));            // 조사쟁점 대(L1)
            item.LongCnts2 = (!drow.Table.Columns.Contains("LongCnts2") ? "" : Utils.ConvertToString(drow["LongCnts2"]));            // 조사내용
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            //this.RefreshControl();
            return item;
        }

        private void Item_DBLossPan6AMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void Item_DBLossPan6AResize(object sender, ContentsResizedEventArgs e)
        {
            this.RefreshControl();
        }

        public void RemoveRow(DBLossPan6A item)
        {
            this.Rows.Remove(item);
            this.Controls.Remove(item);
            if (item.IsNewRow) this.AddEmptyRow();
            this.RefreshControl();
        }

        private void RefreshControl()
        {
            this.SuspendLayout();
            int height = 10;
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
            this.RefreshControl();
        }

        public void SetFocus()
        {
            this.Rows.FirstRow.Focus();
        }
    }

    public class miDBLossPan6Rows
    {
        public int Count { get { return _list.Count; } }
        public DBLossPan6A FirstRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[0];
            }
        }
        public DBLossPan6A LastRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[this._list.Count - 1];
            }
        }

        public DBLossPan6A EmptyRow { get; set; } = null;

        private DBLossPan6 _view = null;
        private List<DBLossPan6A> _list = null;

        public miDBLossPan6Rows(DBLossPan6 vw)
        {
            this._view = vw;
            this._list = new List<DBLossPan6A>();
        }

        public DBLossPan6A this[int index]
        {
            get
            {
                if ((index < 0) || (index > _list.Count - 1)) return null;
                return _list[index];
            }
        }

        public DBLossPan6A Add(bool newrow = false)
        {
            DBLossPan6A var = new DBLossPan6A(_view, newrow);
            _list.Add(var);
            return var;
        }

        public DBLossPan6A Insert(int idx)
        {
            DBLossPan6A var = new DBLossPan6A(_view);
            _list.Insert(idx, var);
            return var;
        }

        public void Remove(int idx)
        {
            _list.RemoveAt(idx);
        }

        public void Remove(DBLossPan6A item)
        {
            _list.Remove(item);
        }

        public void Sort(IComparer<DBLossPan6A> comp)
        {
            _list.Sort(comp);
        }
    }
}
