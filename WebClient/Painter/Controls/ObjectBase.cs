using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public abstract class ObjectBase
    {
        private bool _bDrawTracker;
        private bool _bSelected;
        private bool _bDrawn;
        private Color _PenColor;
        private Color _FillColor;
        private PenStyle _PenStyle;
        private FillStyle _FillStyle;
        private int _PenWidth;
        public MySheet ObjectSheet;

        [NonSerialized]
        public Rectangle WorkingArea = new Rectangle();

        public bool DrawTrackers
        {
            get
            {
                return this._bDrawTracker;
            }
            set
            {
                this._bDrawTracker = value;
            }
        }

        public bool Selected
        {
            get
            {
                return this._bSelected;
            }
            set
            {
                this._bSelected = value;
                if (value == false && !this.Drawn)
                {
                    this.ObjectSheet.DrawObject(this);
                }
            }
        }

        public bool Drawn
        {
            get
            {
                return this._bDrawn;
            }
            set
            {
                this._bDrawn = value;
            }
        }

        public System.Drawing.Color PenColor
        {
            get
            {
                return this._PenColor;
            }
            set
            {
                this._PenColor = value;
            }
        }

        public System.Drawing.Color FillColor
        {
            get
            {
                return this._FillColor;
            }
            set
            {
                this._FillColor = value;
            }
        }

        public PenStyle PenStyle
        {
            get
            {
                return this._PenStyle;
            }
            set
            {
                this._PenStyle = value;
            }
        }

        public FillStyle FillStyle
        {
            get
            {
                return this._FillStyle;
            }
            set
            {
                this._FillStyle = value;
            }
        }

        public int PenWidth
        {
            get
            {
                return this._PenWidth;
            }
            set
            {
                this._PenWidth = value;
            }
        }

        public virtual int HandleCount
        {
            get
            {
                return 0;
            }
        }

        public ObjectBase(MySheet sheet)
        {
            this.ObjectSheet = sheet;
            this.SetProperties();
        }

        public abstract ObjectBase Clone();

        public virtual void Draw(MySheet sheet, Graphics g)
        {
        }

        public virtual void DrawTracker(Graphics g)
        {
            if (this.Selected && this.DrawTrackers)
            {
                SolidBrush brush = new SolidBrush(System.Drawing.Color.White);
                for (int i = 1; i <= this.HandleCount; i++)
                {
                    g.FillEllipse(brush, this.GetHandleRectangle(i));
                    g.DrawEllipse(new Pen(new SolidBrush(System.Drawing.Color.Black)), this.GetHandleRectangle(i));
                }
                brush.Dispose();
            }
        }

        protected void SetObjectFields(ObjectBase objectBase)
        {
            objectBase._bSelected = this._bSelected;
            objectBase._PenColor = this._PenColor;
            objectBase._FillColor = this._FillColor;
            objectBase._PenStyle = this._PenStyle;
            objectBase._FillStyle = this._FillStyle;
            objectBase._PenWidth = this._PenWidth;
        }

        public virtual GraphicsPath GetGraphicPath()
        {
            return null;
        }

        public virtual ObjectPoint GetHandle(int handleNumber)
        {
            return new ObjectPoint(0, 0);
        }

        public virtual Cursor GetHandleCursor(int handleNumber)
        {
            return new MyCursor().ObjectDefaultCursor;
        }

        public virtual Rectangle GetHandleRectangle(int handleNumber)
        {
            ObjectPoint handle = this.GetHandle(handleNumber);
            return new Rectangle(handle.GetPoint(this.ObjectSheet).X - 3, handle.GetPoint(this.ObjectSheet).Y - 3, 6, 6);
        }

        public virtual int HitTest(ObjectPoint point)
        {
            return -1;
        }

        public void SetProperties()
        {
            this._PenColor = ObjectSheet.ToolBar.BorderColor;
            this._FillColor = ObjectSheet.ToolBar.FillColor;
            this._PenStyle = ObjectSheet.ToolBar.PenStyle;
            this._FillStyle = ObjectSheet.ToolBar.FillStyle;
            this._PenWidth = ObjectSheet.ToolBar.LineSize;
        }

        public virtual bool IntersectsWith(Rectangle rectangle)
        {
            return false;
        }

        public virtual void Move(int dx, int dy)
        {
        }

        public virtual void Resize(int dx, int dy)
        {
        }

        public virtual void MoveHandleTo(ObjectPoint point, int handleNumber)
        {
        }

        public virtual void Normalize()
        {
        }

        protected virtual bool PointInObject(ObjectPoint point)
        {
            return false;
        }
    }
}

