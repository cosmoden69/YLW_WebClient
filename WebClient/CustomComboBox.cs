using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLW_WebClient
{
    class CustomComboBox : DevComponents.DotNetBar.Controls.ComboBoxEx
    {
        public void SetReadOnly(bool rdOnly)
        {
            base.Enabled = !rdOnly;
            if (!rdOnly) base.FocusHighlightEnabled = true;
            else base.FocusHighlightEnabled = false;
        }
    }
}
