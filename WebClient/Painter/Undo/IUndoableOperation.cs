using System;

namespace YLW_WebClient.Painter.PaintControls
{
    /// <summary>
    /// Description of IUndoableOperation.
    /// </summary>
    public interface IUndoableOperation
	{
		void Undo();
		void Redo();
	}
}
