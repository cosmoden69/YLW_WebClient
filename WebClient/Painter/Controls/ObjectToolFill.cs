using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectToolFill : ObjectToolCreatorBase
    {
        private int _LastX;
        private int _LastY;
        private Point _MouseDownLocation;
        private ObjectFill _NewFill;

        public ObjectToolFill(MySheet sheet) : base(sheet)
        {
            base.Cursor = new MyCursor().ObjectFillCursor;
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            try
            { 
                int num = e.X;
                int num2 = e.Y;
                this._NewFill = new ObjectFill(_sheet, num, num2);
                base.AddNewObject(this._NewFill);
                this._NewFill.doFloodFill(num, num2);
                this._LastX = e.X;
                this._LastY = e.Y;
                this._MouseDownLocation = e.Location;
            }
            catch { }
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            _sheet.Cursor = base.Cursor;
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }
    }
}