using System.Collections;
using System.Collections.Generic;
using System;

namespace YLW_WebClient.Painter.PaintControls
{
    public class UndoableInserts : IUndoableOperation
    {
        MySheet _fm;
        List<ObjectBase> _do;

        public UndoableInserts(MySheet fm, List<ObjectBase> d)
        {
            _do = d;
            _fm = fm;
        }

        public void Undo()
        {
            Console.WriteLine("Insert Undo List:" + _do.ToString());
            _fm.UndoStack.AcceptChanges = false;
            _fm.DeleteObjects(_do);
            _fm.UndoStack.AcceptChanges = true;
        }

        public void Redo()
        {
            Console.WriteLine("Insert Redo List:" + _do.ToString());
            _fm.UndoStack.AcceptChanges = false;
            _fm.InsertObjects(_do);
            _fm.UndoStack.AcceptChanges = true;
        }
    }
}
