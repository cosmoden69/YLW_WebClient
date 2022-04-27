using System;
using System.Drawing;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectToolBrokenLine : ObjectToolCreatorBase
    {
        private int _LastX;
        private int _LastY;
        private Point _MouseDownLocation;
        private ObjectBrokenLine _NewLine;
        private const int MINDISTANCE = 0xe1;
        private bool _bStartLine = false;

        public ObjectToolBrokenLine(MySheet sheet) : base(sheet)
        {
            base.Cursor = new MyCursor().ObjectDefaultCursor;
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            try
            { 
                if (!_bStartLine)
                {
                    int num = e.X;
                    int num2 = e.Y;
                    this._NewLine = new ObjectBrokenLine(_sheet, num, num2, num + 1, num2 + 1);
                    base.AddNewObject(this._NewLine);
                    this._LastX = e.X;
                    this._LastY = e.Y;
                    this._MouseDownLocation = e.Location;
                    _bStartLine = true;
                }
                else
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        this._NewLine.RemoveAtPoint(this._NewLine.HandleCount - 1);
                        this._NewLine = null;
                        _sheet.ActiveObjectCreator = ObjectCreatorType.Selector;
                        if (_sheet.ObjectList[0] != null) _sheet.ObjectList[0].DrawTrackers = true;
                        _sheet.Refresh();
                        _bStartLine = false;
                    }
                    else
                    {
                        int deltaX = e.X - this._MouseDownLocation.X;
                        int deltaY = e.Y - this._MouseDownLocation.Y;
                        int num3 = (deltaX * deltaX) + (deltaY * deltaY);
                        if (num3 < MINDISTANCE)
                        {
                            //시작점근처에서 시작점과 연결되야 될때
                            this._NewLine.MoveHandleTo(new ObjectPoint(this._MouseDownLocation), this._NewLine.HandleCount);
                            this._NewLine.bClosed = true;
                            this._NewLine = null;
                            _sheet.ActiveObjectCreator = ObjectCreatorType.Selector;
                            if (_sheet.ObjectList[0] != null) _sheet.ObjectList[0].DrawTrackers = true;
                            _sheet.Refresh();
                            _bStartLine = false;
                        }
                        else
                        {
                            this._NewLine.AddPoint(new ObjectPoint(e.X, e.Y));
                            this._LastX = e.X;
                            this._LastY = e.Y;
                        }
                    }
                }
            }
            catch { }
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            try
            { 
                _sheet.Cursor = base.Cursor;
                if ((_NewLine != null) && (_sheet.ActiveObjectCreator != ObjectCreatorType.None))
                {
                    int deltaX = e.X;
                    int deltaY = e.Y;
                    ObjectPoint point = new ObjectPoint(deltaX, deltaY);
                    if (_sheet.ObjectList[0] != null) _sheet.ObjectList[0].MoveHandleTo(point, this._NewLine.HandleCount);
                    _sheet.Refresh();
                }
            }
            catch { }
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        public void CloseTool()
        {
            if (_bStartLine)
            {
                this._NewLine.RemoveAtPoint(this._NewLine.HandleCount - 1);
                this._NewLine = null;
                _sheet.ActiveObjectCreator = ObjectCreatorType.Selector;
                if (_sheet.ObjectList[0] != null) _sheet.ObjectList[0].DrawTrackers = true;
                _sheet.Refresh();
                _bStartLine = false;
            }
        }
    }
}

