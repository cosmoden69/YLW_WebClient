using System;
using System.Drawing;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectToolText : ObjectToolBox
    {
        public ObjectToolText(MySheet sheet) : base(sheet)
        {
            base.Cursor = new MyCursor().ObjectDefaultCursor;
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            try
            { 
                int num = e.X;
                int num2 = e.Y;
                ObjectText objTxt = new ObjectText(_sheet, num, num2, num + 1, num2 + 1);
                base.AddNewObject(objTxt);
                objTxt.SetObject();
            }
            catch { }
        }
    }
}

