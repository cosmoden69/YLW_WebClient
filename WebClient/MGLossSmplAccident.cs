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
    public partial class MGLossSmplAccident : UserControl
    {
        public event MouseEventHandler ContentsMouseWheel;

        public miMGLossSmplAccidentRows Rows = null;

        public MGLossSmplContract Userno1 { get; set; }

        private bool readOnlyMode = false;

        public MGLossSmplAccident()
        {
            InitializeComponent();

            this.Rows = new miMGLossSmplAccidentRows(this);
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
            this.Rows = new miMGLossSmplAccidentRows(this);
        }

        public MGLossSmplAccidentA AddEmptyRow()
        {
            MGLossSmplAccidentA item;
            int tabindex = (this.Rows.LastRow != null ? this.Rows.LastRow.TabIndex : 0);
            item = this.Rows.Add(true);
            item.TabIndex = tabindex + 1;
            item.SetReadOnlyMode(this.readOnlyMode);
            this.Controls.Add(item);
            this.RefreshControl();
            return item;
        }

        public MGLossSmplAccidentA AddRow(object cureSeq, object gubun, object cureFrDt, object cureCnts, object vstHosp)
        {
            MGLossSmplAccidentA item;
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

        public void RemoveRow(MGLossSmplAccidentA item)
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
            this.Rows.Sort(new MGLossSmplAccidentAComp());
            this.RefreshControl();
        }

        public void SetFocus()
        {
            this.Rows.FirstRow.Focus();
        }
    }

    public class miMGLossSmplAccidentRows
    {
        public int Count { get { return _list.Count; } }
        public MGLossSmplAccidentA FirstRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[0];
            }
        }
        public MGLossSmplAccidentA LastRow
        {
            get
            {
                if (this._list.Count < 1) return null;
                return this._list[this._list.Count - 1];
            }
        }

        private MGLossSmplAccident _view = null;
        private List<MGLossSmplAccidentA> _list = null;

        public miMGLossSmplAccidentRows(MGLossSmplAccident vw)
        {
            this._view = vw;
            this._list = new List<MGLossSmplAccidentA>();
        }

        public MGLossSmplAccidentA this[int index]
        {
            get
            {
                if ((index < 0) || (index > _list.Count - 1)) return null;
                return _list[index];
            }
        }

        public MGLossSmplAccidentA Add(bool newrow = false)
        {
            MGLossSmplAccidentA var = new MGLossSmplAccidentA(_view, newrow);
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

        public MGLossSmplAccidentA Insert(int idx)
        {
            MGLossSmplAccidentA var = new MGLossSmplAccidentA(_view);
            var.ContentsMouseWheel += Var_ContentsMouseWheel;
            var.ContentsResized += Var_ContentsResized;
            _list.Insert(idx, var);
            return var;
        }

        public void Remove(int idx)
        {
            _list.RemoveAt(idx);
        }

        public void Remove(MGLossSmplAccidentA item)
        {
            _list.Remove(item);
        }

        public void Sort(IComparer<MGLossSmplAccidentA> comp)
        {
            _list.Sort(comp);
        }
    }

    public class MGLossSmplAccidentAComp : IComparer<MGLossSmplAccidentA>
    {
        // Compares by Height, Length, and Width.
        public int Compare(MGLossSmplAccidentA x, MGLossSmplAccidentA y)
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
