using System;

namespace YLW_WebClient.Painter.PaintControls
{
    public class UndoableInsert : IUndoableOperation
    {
        MySheet _fm;
        ObjectBase _do;

        public UndoableInsert(MySheet fm, ObjectBase d)
        {
            _do = d;
            _fm = fm;
        }

        public void Undo()
        {
            Console.WriteLine("Insert Undo:" + _do.ToString());
            _fm.UndoStack.AcceptChanges = false;
            _fm.DeleteObject(_do);
            _fm.UndoStack.AcceptChanges = true;
        }

        public void Redo()
        {
            Console.WriteLine("Insert Redo:" + _do.ToString());
            _fm.UndoStack.AcceptChanges = false;
            _fm.InsertObject(_do);
            _fm.UndoStack.AcceptChanges = true;
        }
    }
}
