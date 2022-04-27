using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace YLW_WebClient
{
    class CustomDateTimeInput : DevComponents.Editors.DateTimeAdv.DateTimeInput
    {
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        public void SetReadOnly(bool rdOnly)
        {
            base.IsInputReadOnly = rdOnly;
            if (!rdOnly) base.FocusHighlightEnabled = true;
            else base.FocusHighlightEnabled = false;
        }
    }
}
