using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace YLW_WebClient
{
    class CustomDataGridViewMaskedTextBoxCell : DevComponents.DotNetBar.Controls.DataGridViewMaskedTextBoxAdvCell
    {
        public override Type EditType => typeof(CustomDataGridViewMaskedTextBoxEditingControl);
    }

    class CustomDataGridViewMaskedTextBoxEditingControl : DevComponents.DotNetBar.Controls.DataGridViewMaskedTextBoxAdvEditingControl
    {
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.SelectionStart = 0;
            this.SelectionLength = this.Text.Length;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                if (this.SelectionStart <= 0)
                {
                    this.SelectionStart = 0;
                    this.SelectionLength = 1;
                }
                else
                {
                    this.SelectionStart = this.SelectionStart - 1;
                    this.SelectionLength = 1;
                }
                return true;
            }
            else if (keyData == Keys.Right)
            {
                if (this.SelectionStart >= this.Text.Length)
                {
                    this.SelectionStart = this.Text.Length;
                    this.SelectionLength = 0;
                }
                else
                {
                    this.SelectionStart = this.SelectionStart + 1;
                    this.SelectionLength = 1;
                }
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    public class DataGridViewCustomMaskedTextBoxColumn : DevComponents.DotNetBar.Controls.DataGridViewMaskedTextBoxAdvColumn
    {
        public DataGridViewCustomMaskedTextBoxColumn()
        {
            this.CellTemplate = new CustomDataGridViewMaskedTextBoxCell();
        }
    }
}
