using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YLW_WebClient
{
    public partial class ucFlowLayoutPanel : FlowLayoutPanel
    {
        public ucFlowLayoutPanel()
        {
            this.DoubleBuffered = true;
        }

        protected override bool DoubleBuffered
        {
            get
            {
                return base.DoubleBuffered;
            }
            set
            {
                base.DoubleBuffered = true;
            }
        }
    }
}
