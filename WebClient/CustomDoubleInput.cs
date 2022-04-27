using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLW_WebClient
{
    class CustomDoubleInput : DevComponents.Editors.DoubleInput
    {
        public void SetReadOnly(bool rdOnly)
        {
            base.IsInputReadOnly = rdOnly;
            if (!rdOnly) base.FocusHighlightEnabled = true;
            else base.FocusHighlightEnabled = false;
        }
    }
}
