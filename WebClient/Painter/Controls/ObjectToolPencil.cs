using System;
using System.Drawing;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectToolPencil : ObjectToolCreatorBase
    {
        private int _LastX;
        private int _LastY;
        private Point _MouseDownLocation;
        private ObjectPencil _NewPencil;
        private const int MINDISTANCE = 0x1;

        public ObjectToolPencil(MySheet sheet) : base(sheet)
        {
            base.Cursor = new MyCursor().ObjectDefaultCursor;
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            try
            { 
                int num = e.X;
                int num2 = e.Y;
                this._NewPencil = new ObjectPencil(_sheet, num, num2, num + 1, num2 + 1);
                base.AddNewObject(this._NewPencil);
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
                if ((e.Button == MouseButtons.Left) && (this._NewPencil != null))
                {
                    int deltaX = e.X;
                    int deltaY = e.Y;
                    int num3 = ((e.X - this._LastX) * (e.X - this._LastX)) + ((e.Y - this._LastY) * (e.Y - this._LastY));
                    if ((num3 < MINDISTANCE) || this._MouseDownLocation.Equals(new Point(this._LastX, this._LastY)))
                    {
                        //시작점근처에서 시작점과 연결되야 될때
                        //this._NewPencil.MoveHandleTo(new ObjectPoint(deltaX, deltaY), this._NewPencil.HandleCount);
                        if (this._MouseDownLocation.Equals(new Point(this._LastX, this._LastY)))
                        {
                            this._LastX = e.X;
                            this._LastY = e.Y;
                        }
                    }
                    else
                    {
                        this._NewPencil.AddPoint(new ObjectPoint(deltaX, deltaY));
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
            try
            { 
                this._NewPencil = null;
                //_sheet.ActiveObjectCreator = ObjectCreatorType.Selector;   //도형 위에 마우스 있을때 선택가능 마웃커서 보임
                _sheet.ObjectList[0].DrawTrackers = true;                  //도형 이동가능하게
                //_sheet.ObjectList[0].Selected = false;                     //도형 선택 해제
                base.OnMouseUp(e);
            }
            catch { }
        }
    }
}

