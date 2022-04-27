using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YLW_WebClient
{
    class CustomDataGridViewTextBoxCell : DataGridViewTextBoxCell
    {
        public override Type EditType => typeof(CustomDataGridViewTextBoxEditingControl);
    }

    class CustomDataGridViewTextBoxEditingControl : DataGridViewTextBoxEditingControl
    {
        public bool IsInputReadOnly { get; set; } = false;

        public override bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Enter:
                    // Don't let the DataGridView handle the Enter key.
                    return true;
                case Keys.Home:
                case Keys.End:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    return true;
                default:
                    break;
            }

            return base.EditingControlWantsInputKey(keyData, dataGridViewWantsInputKey);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode & Keys.KeyCode)
            {
                case Keys.Enter:
                    if (IsInputReadOnly) return;
                    int oldSelectionStart = this.SelectionStart;
                    string currentText = this.Text;

                    this.Text = String.Format("{0}{1}{2}",
                        currentText.Substring(0, this.SelectionStart),
                        Environment.NewLine,
                        currentText.Substring(this.SelectionStart + this.SelectionLength));
                    this.SelectionStart = oldSelectionStart + Environment.NewLine.Length;
                    this.ScrollToCaret();
                    break;
                case Keys.Home:
                    //this.SelectionStart = 0;
                    //this.ScrollToCaret();
                    break;
                case Keys.End:
                    //this.SelectionStart = this.Text.Length;
                    //this.ScrollToCaret();
                    break;
                case Keys.Back:
                case Keys.Delete:
                    if (IsInputReadOnly) return;
                    break;
                default:
                    break;
            }

            base.OnKeyDown(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (IsInputReadOnly) return;
            DataGridView dgv = this.EditingControlDataGridView;
            string prevText = this.Text;
            DataGridViewCustomColumn mycol = dgv.CurrentCell.OwningColumn as DataGridViewCustomColumn;
            if (mycol != null && mycol.MaxInputLength > 0 && YLWService.Utils.HLen(prevText) > mycol.MaxInputLength)
            {
                MessageBox.Show("입력가능 길이(" + mycol.MaxInputLength.ToString() + ") 초과입니다");
                int pos = this.SelectionStart;
                this.Text = YLWService.Utils.HMid(prevText, 1, mycol.MaxInputLength);
                this.SelectionStart = (pos < this.Text.Length ? pos : this.Text.Length);
                this.ScrollToCaret();
            }
            GridRowHeightResize();
            GridHeightResize();
            base.OnTextChanged(e);
        }

        private void GridRowHeightResize()
        {
            DataGridView dgv = this.EditingControlDataGridView;
            dgv.CurrentCell.OwningRow.Height = GridRowMaxHeight();
        }

        private int GridRowMaxHeight()
        {
            DataGridView dgv = this.EditingControlDataGridView;
            Graphics editControlGraphics = dgv.EditingControl.CreateGraphics();
            int heightForString = 0;
            foreach (DataGridViewCell cel in dgv.CurrentCell.OwningRow.Cells)
            {
                if (!(cel is CustomDataGridViewTextBoxCell)) continue;
                string prevText = cel.EditedFormattedValue.ToString() + "0";
                // Gets the length of the current text value.
                SizeF stringSize = editControlGraphics.MeasureString(prevText, dgv.EditingControl.Font, cel.Size.Width);
                heightForString = Math.Max((int)Math.Ceiling(stringSize.Height), heightForString);
            }
            if (heightForString < 23) heightForString = 23;
            return heightForString;
        }

        private void GridHeightResize()
        {
            DataGridView dgv = this.EditingControlDataGridView;

            int height = dgv.ColumnHeadersHeight;
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                height += dr.Height; // Row height.
            }
            dgv.Height = height + 45;
        }
    }

    public class DataGridViewRolloverCell : DataGridViewTextBoxCell
    {
        public override Type EditType => typeof(CustomDataGridViewTextBoxEditingControl);
    }

    [ToolboxBitmap(typeof(DataGridViewTextBoxColumn), "DataGridViewTextBoxColumn.bmp")]
    public class DataGridViewCustomColumn : DataGridViewColumn
    {
        [DefaultValue(32767)]
        public int MaxInputLength { get; set; }

        public DataGridViewCustomColumn()
        {
            this.CellTemplate = new CustomDataGridViewTextBoxCell();
        }
    }
}
