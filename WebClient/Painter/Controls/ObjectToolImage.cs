using System;
using System.Drawing;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectToolImage : ObjectToolBox
    {
        public ObjectToolImage(MySheet sheet) : base(sheet)
        {
            base.Cursor = new MyCursor().ObjectDefaultCursor;
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            try
            { 
                int num = e.X;
                int num2 = e.Y;
                base.AddNewObject(new ObjectImage(_sheet, num, num2, num + 1, num2 + 1));
            }
            catch { }
        }
    }
}

