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
    public partial class DBLifeDetail2 : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public miDBLifeDetail2Rows Rows = null;

        private bool readOnlyMode = false;

        public DBLifeDetail2()
        {
            InitializeComponent();

            this.Rows = new miDBLifeDetail2Rows(this);
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
            this.Rows = new miDBLifeDetail2Rows(this);
        }

        public DBLifeDetail2A AddEmptyRow()
        {
            DBLifeDetail2A item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            item = this.Rows.Add(true);
            this.Rows.EmptyRow = item;
            item.DBLifeDetail2AMouseWheel += Item_DBLifeDetail2AMouseWheel;
            item.DBLifeDetail2AResize += Item_DBLifeDetail2AResize;
            item.TabIndex = tabindex + 1;
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            this.RefreshControl();
            return item;
        }

        public DBLifeDetail2A AddRow(DataRow drow)
        {
            DBLifeDetail2A item;
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
            item.DBLifeDetail2AMouseWheel += Item_DBLifeDetail2AMouseWheel;
            item.DBLifeDetail2AResize += Item_DBLifeDetail2AResize;
            item.TabIndex = tabindex;
            item.Title1_Text = (!drow.Table.Columns.Contains("RprtHed") ? "" : Utils.ConvertToString(drow["RprtHed"]));           // 항목
            item.Check1 = (!drow.Table.Columns.Contains("RprtRevwRslt") ? "" : Utils.ConvertToString(drow["RprtRevwRslt"]));      // 확인사항
            item.RprtNo = (!drow.Table.Columns.Contains("RprtNo") ? "" : Utils.ConvertToString(drow["RprtNo"]));                 // 순번
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            //this.RefreshControl();
            return item;
        }

        private void Item_DBLifeDetail2AMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void Item_DBLifeDetail2AResize(object sender, ContentsResizedEventArgs e)
        {
            this.RefreshControl();
        }

        public void RemoveRow(DBLifeDetail2A item)
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
        }

        public void SetFocus()
        {
            this.Rows.FirstRow.Focus();
        }
    }

    public class miDBLifeDetail2Rows
    {
        public int Count { get { return _list.Count; } }
        public DBLifeDetail2A FirstRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[0];
            }
        }
        public DBLifeDetail2A LastRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[this._list.Count - 1];
            }
        }

        public DBLifeDetail2A EmptyRow { get; set; } = null;

        private DBLifeDetail2 _view = null;
        private List<DBLifeDetail2A> _list = null;

        public miDBLifeDetail2Rows(DBLifeDetail2 vw)
        {
            this._view = vw;
            this._list = new List<DBLifeDetail2A>();
        }

        public DBLifeDetail2A this[int index]
        {
            get
            {
                if ((index < 0) || (index > _list.Count - 1)) return null;
                return _list[index];
            }
        }

        public DBLifeDetail2A Add(bool newrow = false)
        {
            DBLifeDetail2A var = new DBLifeDetail2A(_view, newrow);
            _list.Add(var);
            return var;
        }

        public DBLifeDetail2A Insert(int idx)
        {
            DBLifeDetail2A var = new DBLifeDetail2A(_view);
            _list.Insert(idx, var);
            return var;
        }

        public void Remove(int idx)
        {
            _list.RemoveAt(idx);
        }

        public void Remove(DBLifeDetail2A item)
        {
            _list.Remove(item);
        }

        public void Sort(IComparer<DBLifeDetail2A> comp)
        {
            _list.Sort(comp);
        }
    }
}
