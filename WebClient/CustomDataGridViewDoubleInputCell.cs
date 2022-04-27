using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace YLW_WebClient
{
    class CustomDataGridViewDoubleInputCell : DevComponents.DotNetBar.Controls.DataGridViewDoubleInputCell
    {
        public override Type EditType => typeof(CustomDataGridViewDoubleInputEditingControl);
    }

    class CustomDataGridViewDoubleInputEditingControl : DevComponents.DotNetBar.Controls.DataGridViewDoubleInputEditingControl
    {
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
            this.Parent.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down)
            {
                return true;
            }
            else if (keyData == Keys.Up)
            {
                return true;
            }
            else if (keyData == Keys.Left)
            {
                return true;
            }
            else if (keyData == Keys.Right)
            {
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    public class DataGridViewCustomDoubleInputColumn : DevComponents.DotNetBar.Controls.DataGridViewDoubleInputColumn
    {
        public DataGridViewCustomDoubleInputColumn()
        {
            this.CellTemplate = new CustomDataGridViewDoubleInputCell();
        }
    }
}
