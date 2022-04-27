using System;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    public abstract class ObjectToolCreatorBase : ObjectToolBase
    {
        private System.Windows.Forms.Cursor _Cursor;
        internal MySheet _sheet;

        protected ObjectToolCreatorBase(MySheet sheet)
        {
            _sheet = sheet;
        }

        protected void AddNewObject(ObjectBase obj)
        {
            _sheet.ObjectList.UnselectAll();
            obj.Selected = true;
            _sheet.InsertObject(obj);
            _sheet.Capture = true;
            _sheet.Refresh();
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            _sheet.Capture = false;
            _sheet.Refresh();
        }

        protected System.Windows.Forms.Cursor Cursor
        {
            get
            {
                return this._Cursor;
            }
            set
            {
                this._Cursor = value;
            }
        }
    }
}

