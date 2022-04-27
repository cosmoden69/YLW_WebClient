﻿using System;
using System.Collections.Generic;

namespace YLW_WebClient.Painter.PaintControls
{
    /// <summary>
    /// Description of UndoQueue.
    /// </summary>
    internal class UndoQueue : IUndoableOperation
    {
        List<IUndoableOperation> undolist = new List<IUndoableOperation>();

        public UndoQueue(UndoStack stack, int numops)
        {
            if (stack == null)
            {
                throw new ArgumentNullException("stack");
            }

            System.Diagnostics.Debug.Assert(numops > 0, "UndoQueue : numops should be > 0");

            for (int i = 0; i < numops; ++i)
            {
                if (stack._UndoStack.Count > 0)
                {
                    undolist.Add(stack._UndoStack.Pop());
                }
            }
        }

        public void Undo()
        {
            for (int i = 0; i < undolist.Count; ++i)
            {
                undolist[i].Undo();
            }
        }

        public void Redo()
        {
            for (int i = undolist.Count - 1; i >= 0; --i)
            {
                ((IUndoableOperation)undolist[i]).Redo();
            }
        }
    }
}
