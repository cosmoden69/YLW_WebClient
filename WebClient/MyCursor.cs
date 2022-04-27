using System;
using System.Windows.Forms;

namespace YLW_WebClient
{
    public class MyCursor
    {
        public Cursor ObjectAddCursor
        {
            get
            {
                return new Cursor(base.GetType(), "Resources.ObjectAddCursor.cur");
            }
        }

        public Cursor ObjectAllArrowCursor
        {
            get
            {
                return new Cursor(base.GetType(), "Resources.ObjectAllArrowCursor.cur");
            }
        }

        public Cursor ObjectDefaultCursor
        {
            get
            {
                return new Cursor(base.GetType(), "Resources.ObjectDefaultCursor.cur");
            }
        }

        public Cursor ObjectMoveCursor
        {
            get
            {
                return new Cursor(base.GetType(), "Resources.ObjectMoveCursor.cur");
            }
        }

        public Cursor ObjectNESWArrowCursor
        {
            get
            {
                return new Cursor(base.GetType(), "Resources.ObjectNESWArrowCursor.cur");
            }
        }

        public Cursor ObjectNSArrowCursor
        {
            get
            {
                return new Cursor(base.GetType(), "Resources.ObjectNSArrowCursor.cur");
            }
        }

        public Cursor ObjectNWSEArrowCursor
        {
            get
            {
                return new Cursor(base.GetType(), "Resources.ObjectNWSEArrowCursor.cur");
            }
        }

        public Cursor ObjectRotateCursor
        {
            get
            {
                return new Cursor(base.GetType(), "Resources.ObjectRotateCursor.cur");
            }
        }

        public Cursor ObjectWEArrowCursor
        {
            get
            {
                return new Cursor(base.GetType(), "Resources.ObjectWEArrowCursor.cur");
            }
        }

        public Cursor ObjectEraserCursor
        {
            get
            {
                return new Cursor(base.GetType(), "Resources.ObjectEraserCursor.cur");
            }
        }

        public Cursor ObjectFillCursor
        {
            get
            {
                return new Cursor(base.GetType(), "Resources.ObjectFillCursor.cur");
            }
        }
    }
}

