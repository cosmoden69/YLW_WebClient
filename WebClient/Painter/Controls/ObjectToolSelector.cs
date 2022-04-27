using System;
using System.Drawing;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectToolSelector : ObjectToolCreatorBase
    {
        private bool _bHandleMove;
        private ObjectPoint _StartPoint = new ObjectPoint(0, 0);
        private ObjectPoint _EndPoint = new ObjectPoint(0, 0);
        private ObjectBase _ResizedObject;
        private int _ResizedObjectHandle;
        private ObjectSelectionMode _SelectionMode;

        public ObjectToolSelector(MySheet sheet) : base(sheet)
        {
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            try
            { 
                int deltaX = e.X;
                int deltaY = e.Y;
                ObjectPoint point = new ObjectPoint(deltaX, deltaY);
                this._SelectionMode = ObjectSelectionMode.None;
                foreach (ObjectBase base2 in _sheet.ObjectList.Selection)
                {
                    int num2 = base2.HitTest(point);
                    if (num2 > 0)
                    {
                        this._SelectionMode = ObjectSelectionMode.Size;
                        this._ResizedObject = base2;
                        this._ResizedObjectHandle = num2;
                        //sheet.ObjectList.UnselectAll();
                        base2.Selected = true;
                        _sheet.OnObjectSelected(base2);
                        break;
                    }
                }
                if (this._SelectionMode == ObjectSelectionMode.None)
                {
                    ObjectBase base3 = null;
                    int count = _sheet.ObjectList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (_sheet.ObjectList[i].HitTest(point) == 0)
                        {
                            base3 = _sheet.ObjectList[i];
                            break;
                        }
                    }
                    if (base3 != null)
                    {
                        if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                        {
                            this._SelectionMode = ObjectSelectionMode.MultiSelection;
                            _sheet.Cursor = new MyCursor().ObjectAddCursor;
                            base3.Selected = !base3.Selected;
                            _sheet.ObjectList.MoveSelectionToFront();
                            if (base3.Selected) _sheet.OnObjectSelected(base3);
                        }
                        else
                        {
                            this._SelectionMode = ObjectSelectionMode.Move;
                            if (((Control.ModifierKeys & Keys.Control) == Keys.None) && !base3.Selected)
                            {
                                _sheet.ObjectList.UnselectAll();
                                base3.Selected = true;
                                _sheet.ObjectList.MoveSelectionToFront();
                                _sheet.OnObjectSelected(base3);
                            }
                            _sheet.Cursor = new MyCursor().ObjectMoveCursor;
                        }
                    }
                }
                if (this._SelectionMode == ObjectSelectionMode.None)
                {
                    if ((Control.ModifierKeys & Keys.Control) == Keys.None)
                    {
                        _sheet.ObjectList.UnselectAll();
                    }
                    this._SelectionMode = ObjectSelectionMode.NetSelection;
                }
                this._StartPoint.DeltaX = e.X;
                this._StartPoint.DeltaY = e.Y;
                this._EndPoint.DeltaX = e.X;
                this._EndPoint.DeltaY = e.Y;
                _sheet.Capture = true;
                _sheet.Refresh();
                if (this._SelectionMode == ObjectSelectionMode.NetSelection)
                {
                    ControlPaint.DrawReversibleFrame(_sheet.RectangleToScreen(ObjectHelpers.GetNormalizedRectangle(this._StartPoint.GetPoint(_sheet), this._EndPoint.GetPoint(_sheet))), Color.Black, FrameStyle.Dashed);
                }
                if (e.Button == MouseButtons.Right)
                {
                    bool flag = false;
                    for (int j = 0; j < _sheet.ObjectList.Count; j++)
                    {
                        if (_sheet.ObjectList[j].HitTest(point) >= 0)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        _sheet.MousePointerOnObject = true;
                    }
                    else
                    {
                        _sheet.MousePointerOnObject = false;
                    }
                }
                //            if ((e.Button == MouseButtons.Left) && this._bHandleMove)
                //            {
                //                sheet.Cursor = new MyCursor().ObjectDefaultCursor;
                //                foreach (ObjectBase base4 in sheet.ObjectList.Selection)
                //                {
                //                    base4.DrawTrackers = false;
                //                }
                //            }
            }
            catch { }
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            try
            { 
                int deltaX = e.X;
                int deltaY = e.Y;
                ObjectPoint point = new ObjectPoint(deltaX, deltaY);
                ObjectPoint point3 = new ObjectPoint(this._EndPoint.DeltaX, this._EndPoint.DeltaY);
                if (e.Button != MouseButtons.None)
                {
                    if ((e.Button == MouseButtons.Left) && this._bHandleMove)
                    {
                        _sheet.Cursor = new MyCursor().ObjectDefaultCursor;
                        foreach (ObjectBase base2 in _sheet.ObjectList.Selection)
                        {
                            base2.DrawTrackers = false;
                        }
                    }
                    if (e.Button == MouseButtons.Left)
                    {
                        int dx = e.X - this._EndPoint.GetPoint(_sheet).X;
                        int dy = e.Y - this._EndPoint.GetPoint(_sheet).Y;
                        this._EndPoint.DeltaX = e.X;
                        this._EndPoint.DeltaY = e.Y;
                        if ((this._SelectionMode == ObjectSelectionMode.Size) && (this._ResizedObject != null))
                        {
                            this._ResizedObject.MoveHandleTo(point, this._ResizedObjectHandle);
                            _sheet.Refresh();
                        }
                        if (this._SelectionMode == ObjectSelectionMode.Move)
                        {
                            foreach (ObjectBase base3 in _sheet.ObjectList.Selection)
                            {
                                base3.Move(dx, dy);
                                base3.DrawTrackers = false;
                            }
                            _sheet.Cursor = new MyCursor().ObjectMoveCursor;
                            _sheet.Refresh();
                        }
                        if (this._SelectionMode == ObjectSelectionMode.NetSelection)
                        {
                            ControlPaint.DrawReversibleFrame(_sheet.RectangleToScreen(ObjectHelpers.GetNormalizedRectangle(this._StartPoint.GetPoint(_sheet), point3.GetPoint(_sheet))), Color.Black, FrameStyle.Dashed);
                            ControlPaint.DrawReversibleFrame(_sheet.RectangleToScreen(ObjectHelpers.GetNormalizedRectangle(this._StartPoint.GetPoint(_sheet), point.GetPoint(_sheet))), Color.Black, FrameStyle.Dashed);
                        }
                    }
                }
                else
                {
                    Cursor handleCursor = null;
                    for (int i = 0; i < _sheet.ObjectList.Count; i++)
                    {
                        if (_sheet.ObjectList[i].ObjectSheet == null)
                        {
                            _sheet.ObjectList[i].ObjectSheet = _sheet;
                        }
                        int handleNumber = _sheet.ObjectList[i].HitTest(point);
                        if (handleNumber > 0)
                        {
                            handleCursor = _sheet.ObjectList[i].GetHandleCursor(handleNumber);
                            this._bHandleMove = true;
                            break;
                        }
                        if (handleNumber == 0)
                        {
                            if ((_sheet.ObjectList.SelectionCount > 0) && ((Control.ModifierKeys & Keys.Control) == Keys.Control))
                            {
                                handleCursor = new MyCursor().ObjectAddCursor;
                            }
                            else
                            {
                                handleCursor = new MyCursor().ObjectAllArrowCursor;
                            }
                            this._bHandleMove = false;
                            break;
                        }
                    }
                    if (handleCursor != null)
                    {
                        _sheet.MousePointerOnObject = true;
                    }
                    else
                    {
                        _sheet.MousePointerOnObject = false;
                        handleCursor = new MyCursor().ObjectDefaultCursor;
                    }
                    if ((handleCursor == null) && _sheet.ObjectSelectionMode)
                    {
                        handleCursor = new MyCursor().ObjectDefaultCursor;
                    }
                    if (handleCursor != null)
                    {
                        _sheet.Cursor = handleCursor;
                    }
                }
            }
            catch { }
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            try
            { 
                if (this._SelectionMode == ObjectSelectionMode.NetSelection)
                {
                    ControlPaint.DrawReversibleFrame(_sheet.RectangleToScreen(ObjectHelpers.GetNormalizedRectangle(this._StartPoint.GetPoint(_sheet), this._EndPoint.GetPoint(_sheet))), Color.Black, FrameStyle.Dashed);
                    _sheet.ObjectList.SelectInRectangle(ObjectHelpers.GetNormalizedRectangle(this._StartPoint.GetPoint(_sheet), this._EndPoint.GetPoint(_sheet)));
                    this._SelectionMode = ObjectSelectionMode.None;
                }
                if (this._ResizedObject != null)
                {
                    this._ResizedObject.Normalize();
                    this._ResizedObject = null;
                }
                foreach (ObjectBase base2 in _sheet.ObjectList.Selection)
                {
                    base2.DrawTrackers = true;
                }
                _sheet.Capture = false;
                _sheet.Refresh();
            }
            catch { }
        }
    }
}

