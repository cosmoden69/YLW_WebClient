using System;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public abstract class ObjectToolBase
    {
        protected ObjectToolBase()
        {
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
        }
    }
}

