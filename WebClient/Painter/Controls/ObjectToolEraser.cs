using System;
using System.Drawing;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectToolEraser : ObjectToolCreatorBase
    {
        private int _LastX;
        private int _LastY;
        private Point _MouseDownLocation;
        private ObjectEraser _NewEraser;
        private const int MINDISTANCE = 0x1;

        public ObjectToolEraser(MySheet sheet) : base(sheet)
        {
            base.Cursor = new MyCursor().ObjectEraserCursor;
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            try
            { 
                int num = e.X;
                int num2 = e.Y;
                this._NewEraser = new ObjectEraser(_sheet, num, num2, num + 1, num2 + 1);
                base.AddNewObject(this._NewEraser);
                this._LastX = e.X;
                this._LastY = e.Y;
                this._MouseDownLocation = e.Location;
            }
            catch { }
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            try
            { 
                _sheet.Cursor = base.Cursor;
                if ((e.Button == MouseButtons.Left) && (this._NewEraser != null))
                {
                    int deltaX = e.X;
                    int deltaY = e.Y;
                    int num3 = ((e.X - this._LastX) * (e.X - this._LastX)) + ((e.Y - this._LastY) * (e.Y - this._LastY));
                    if ((num3 < MINDISTANCE) || this._MouseDownLocation.Equals(new Point(this._LastX, this._LastY)))
                    {
                        if (this._MouseDownLocation.Equals(new Point(this._LastX, this._LastY)))
                        {
                            this._LastX = e.X;
                            this._LastY = e.Y;
                        }
                    }
                    else
                    {
                        this._NewEraser.AddPoint(new ObjectPoint(deltaX, deltaY));
                        this._LastX = e.X;
                        this._LastY = e.Y;
                    }
                    _sheet.Refresh();
                }
            }
            catch { }
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            this._NewEraser = null;
            //sheet.ObjectList[0].DrawTrackers = false;
            base.OnMouseUp(e);
        }
    }
}

