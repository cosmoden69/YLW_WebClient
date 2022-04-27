using System;
using System.Drawing;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectToolBox : ObjectToolCreatorBase
    {
        public ObjectToolBox(MySheet sheet) : base(sheet)
        {
            base.Cursor = new MyCursor().ObjectDefaultCursor;
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            try
            { 
                int num = e.X;
                int num2 = e.Y;
                base.AddNewObject(new ObjectBox(_sheet, num, num2, num + 1, num2 + 1));
            }
            catch { }
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            try
            { 
                base._sheet.Cursor = base.Cursor;
                if ((e.Button == MouseButtons.Left) && (base._sheet.ActiveObjectCreator != ObjectCreatorType.None))
                {
                    int deltaX = e.X;
                    int deltaY = e.Y;
                    ObjectPoint point = new ObjectPoint(deltaX, deltaY);
                    base._sheet.ObjectList[0].MoveHandleTo(point, 5);
                    base._sheet.Refresh();
                }
            }
            catch { }
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            try
            { 
                base._sheet.ActiveObjectCreator = ObjectCreatorType.Selector;
                base._sheet.ObjectList[0].DrawTrackers = true;
                base.OnMouseUp(e);
            }
            catch { }
        }
    }
}

