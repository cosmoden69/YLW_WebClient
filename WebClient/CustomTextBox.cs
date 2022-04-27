using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLW_WebClient
{
    public class CustomTextBox : DevComponents.DotNetBar.Controls.TextBoxX
    {
        public void SetReadOnly(bool rdOnly)
        {
            base.ReadOnly = rdOnly;
            if (!rdOnly) base.FocusHighlightEnabled = true;
            else base.FocusHighlightEnabled = false;
        }
    }
}
