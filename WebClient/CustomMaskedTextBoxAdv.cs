using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLW_WebClient
{
    class CustomMaskedTextBoxAdv : DevComponents.DotNetBar.Controls.MaskedTextBoxAdv
    {
        public void SetReadOnly(bool rdOnly)
        {
            base.ReadOnly = rdOnly;
            if (!rdOnly) base.FocusHighlightEnabled = true;
            else base.FocusHighlightEnabled = false;
        }
    }
}
