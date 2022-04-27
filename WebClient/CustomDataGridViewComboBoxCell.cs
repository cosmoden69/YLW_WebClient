using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YLW_WebClient
{
    class CustomDataGridViewComboBoxCell : DataGridViewComboBoxCell
    {
        public override Type EditType => typeof(CustomDataGridViewComboBoxEditingControl);
    }

    class CustomDataGridViewComboBoxEditingControl : DataGridViewComboBoxEditingControl
    {
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.DroppedDown) return;
            ((HandledMouseEventArgs)e).Handled = true;
            this.Parent.Focus();
        }
    }

    public class DataGridViewCustomComboBoxColumn : DataGridViewComboBoxColumn
    {
        public DataGridViewCustomComboBoxColumn()
        {
            this.CellTemplate = new CustomDataGridViewComboBoxCell();
        }
    }
}
