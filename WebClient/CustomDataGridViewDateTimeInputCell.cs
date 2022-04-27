using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace YLW_WebClient
{
    class CustomDataGridViewDateTimeInputCell : DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputCell
    {
        public override Type EditType => typeof(CustomDataGridViewDateTimeInputEditingControl);
    }

    class CustomDataGridViewDateTimeInputEditingControl : DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputEditingControl
    {
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
            this.Parent.Focus();
        }
    }

    public class DataGridViewCustomDateTimeInputColumn : DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn
    {
        public DataGridViewCustomDateTimeInputColumn()
        {
            this.CellTemplate = new CustomDataGridViewDateTimeInputCell();
        }
    }
}
