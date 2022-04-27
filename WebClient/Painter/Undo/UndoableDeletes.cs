using System.Collections;
using System.Collections.Generic;
using System;

namespace YLW_WebClient.Painter.PaintControls
{
    public class UndoableDeletes : IUndoableOperation
    {
        MySheet _fm;
        List<ObjectBase> _do;

        public UndoableDeletes(MySheet fm, List<ObjectBase> d)
        {
            _do = d;
            _fm = fm;
        }

        public void Undo()
        {
            Console.WriteLine("Delete Undo List:" + _do.ToString());
            _fm.UndoStack.AcceptChanges = false;
            _fm.InsertObjects(_do);
            _fm.UndoStack.AcceptChanges = true;
        }

        public void Redo()
        {
            Console.WriteLine("Delete Redo List:" + _do.ToString());
            _fm.UndoStack.AcceptChanges = false;
            _fm.DeleteObjects(_do);
            _fm.UndoStack.AcceptChanges = true;
        }
    }
}
