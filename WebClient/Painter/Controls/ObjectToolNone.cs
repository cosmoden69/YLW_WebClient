using System;
using System.Drawing;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectToolNone : ObjectToolCreatorBase
    {
        public ObjectToolNone(MySheet sheet) : base(sheet)
        {
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            try
            { 
                int deltaX = e.X;
                int deltaY = e.Y;
                ObjectPoint point = new ObjectPoint(deltaX, deltaY);
                if (e.Button == MouseButtons.None)
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
                            break;
                        }
                        if (handleNumber == 0)
                        {
                            if ((_sheet.ObjectList.SelectionCount > 0) && (_sheet._bKeyControl || _sheet._bKeyShift))
                            {
                                handleCursor = new MyCursor().ObjectAddCursor;
                            }
                            else
                            {
                                handleCursor = new MyCursor().ObjectAllArrowCursor;
                            }
                            break;
                        }
                    }
                    if ((handleCursor == null) && _sheet.ObjectSelectionMode)
                    {
                        handleCursor = Cursors.Default;
                    }
                    if (handleCursor != null)
                    {
                        _sheet.Cursor = handleCursor;
                        _sheet.MousePointerOnObject = true;
                    }
                    else
                    {
                        _sheet.MousePointerOnObject = false;
                    }
                }
            }
            catch { }
        }
    }
}

