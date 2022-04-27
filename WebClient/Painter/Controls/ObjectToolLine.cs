using System;
using System.Drawing;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectToolLine : ObjectToolCreatorBase
    {
        public ObjectToolLine(MySheet sheet) : base(sheet)
        {
            base.Cursor = new MyCursor().ObjectDefaultCursor;
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            try
            { 
                int num = e.X;
                int num2 = e.Y;
                base.AddNewObject(new ObjectLine(_sheet, num, num2, num + 1, num2 + 1));
            }
            catch { }
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            try
            { 
                _sheet.Cursor = base.Cursor;
                if ((e.Button == MouseButtons.Left) && (_sheet.ActiveObjectCreator != ObjectCreatorType.None))
                {
                    int deltaX = e.X;
                    int deltaY = e.Y;
                    ObjectPoint point = new ObjectPoint(deltaX, deltaY);
                    _sheet.ObjectList[0].MoveHandleTo(point, 2);
                    _sheet.Refresh();
                }
            }
            catch { }
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            try
            { 
                _sheet.ActiveObjectCreator = ObjectCreatorType.Selector;
                _sheet.ObjectList[0].DrawTrackers = true;
                base.OnMouseUp(e);
            }
            catch { }
        }
    }
}

