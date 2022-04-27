using System;

namespace YLW_WebClient.Painter.PaintControls
{
    public class UndoableDelete : IUndoableOperation
    {
        MySheet _fm;
        ObjectBase _do;

        public UndoableDelete(MySheet fm, ObjectBase d)
        {
            _do = d;
            _fm = fm;
        }

        public void Undo()
        {
            Console.WriteLine("Delete Undo:" + _do.ToString());
            _fm.UndoStack.AcceptChanges = false;
            _fm.InsertObject(_do);
            _fm.UndoStack.AcceptChanges = true;
        }

        public void Redo()
        {
            Console.WriteLine("Delete Redo:" + _do.ToString());
            _fm.UndoStack.AcceptChanges = false;
            _fm.DeleteObject(_do);
            _fm.UndoStack.AcceptChanges = true;
        }
    }
}
