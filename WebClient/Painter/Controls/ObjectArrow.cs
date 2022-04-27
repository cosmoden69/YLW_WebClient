using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public class ObjectArrow : ObjectBase
    {
        internal ObjectPoint _Start;
        internal ObjectPoint _End;
        [NonSerialized]
        private GraphicsPath HitAreaPath;
        [NonSerialized]
        private Pen HitAreaPen;
        [NonSerialized]
        private Region HitAreaRegion;

        public ObjectArrow(MySheet sheet) : this(sheet, 0, 0, 1, 0)
        {
        }

        public ObjectArrow(MySheet sheet, int x1, int y1, int x2, int y2) : base(sheet)
        {
            this._Start = new ObjectPoint(0, 0);
            this._End = new ObjectPoint(0, 0);
            this._Start.DeltaX = x1;
            this._Start.DeltaY = y1;
            this._End.DeltaX = x2;
            this._End.DeltaY = y2;
        }

        public override ObjectBase Clone()
        {
            ObjectArrow objectBase = new ObjectArrow(base.ObjectSheet);
            objectBase._Start = this._Start;
            objectBase._End = this._End;
            base.SetObjectFields(objectBase);
            return objectBase;
        }

        protected virtual void CreateHitTestObjects()
        {
            try
            {
                if (this.AreaPath == null)
                {
                    this.AreaPath = new GraphicsPath();
                    this.AreaPen = new Pen(Color.Black, 7f);
                    this.AreaPath.AddLine(this._Start.GetPoint(base.ObjectSheet).X, this._Start.GetPoint(base.ObjectSheet).Y, this._End.GetPoint(base.ObjectSheet).X, this._End.GetPoint(base.ObjectSheet).Y);
                    this.AreaPath.Widen(this.AreaPen);
                    this.AreaRegion = new Region(this.AreaPath);
                    this.AreaPath.Dispose();
                }
            }
            catch { }
        }

        public override void Draw(MySheet sheet, Graphics g)
        {
            try
            { 
                //            Console.WriteLine("Draw : " + this._Start.ToString() + " -> " + this._End.ToString());
                base.WorkingArea = sheet.WorkingArea;
                g.SetClip(base.WorkingArea);
                //g.SmoothingMode = SmoothingMode.AntiAlias;
                Pen pen = new Pen(base.PenColor, (float)base.PenWidth);
                AdjustableArrowCap bigArrow = new AdjustableArrowCap(3, 3, true);
                pen.CustomEndCap = bigArrow;
                if (base.PenStyle == PenStyle.Line) pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                if (base.PenStyle == PenStyle.Dash) pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawLine(pen, this._Start.GetPoint(sheet).X, this._Start.GetPoint(sheet).Y, this._End.GetPoint(sheet).X, this._End.GetPoint(sheet).Y);
                g.SmoothingMode = SmoothingMode.Default;
                pen.Dispose();
            }
            catch { }
        }

        public override ObjectPoint GetHandle(int handleNumber)
        {
            if (handleNumber == 1)
            {
                return this._Start;
            }
            return this._End;
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                case 2:
                    return new MyCursor().ObjectNESWArrowCursor;
            }
            return new MyCursor().ObjectDefaultCursor;
        }

        public override int HitTest(ObjectPoint point)
        {
            if (base.Drawn) return -1;
            if (base.Selected)
            {
                for (int i = 1; i <= this.HandleCount; i++)
                {
                    if (this.GetHandleRectangle(i).Contains(point.GetPoint(base.ObjectSheet)))
                    {
                        return i;
                    }
                }
            }
            if (this.PointInObject(point))
            {
                return 0;
            }
            return -1;
        }

        public override bool IntersectsWith(Rectangle rectangle)
        {
            this.Invalidate();
            this.CreateHitTestObjects();
            if (this.AreaRegion == null) return false;
            return this.AreaRegion.IsVisible(rectangle);
        }

        protected void Invalidate()
        {
            if (this.AreaPath != null)
            {
                this.AreaPath.Dispose();
                this.AreaPath = null;
            }
            if (this.AreaPen != null)
            {
                this.AreaPen.Dispose();
                this.AreaPen = null;
            }
            if (this.AreaRegion != null)
            {
                this.AreaRegion.Dispose();
                this.AreaRegion = null;
            }
        }

        public override void Move(int dx, int dy)
        {
            try
            { 
                ObjectPoint point = new ObjectPoint(this._Start.GetPoint(base.ObjectSheet));
                ObjectPoint point2 = new ObjectPoint(this._End.GetPoint(base.ObjectSheet));
                bool flag = false;
                //if (point.GetPoint(base.ObjectSheet).X < this.WorkingArea.Left)
                //{
                //    flag = true;
                //}
                //if (point2.GetPoint(base.ObjectSheet).X < this.WorkingArea.Left)
                //{
                //    flag = true;
                //}
                //if (point.GetPoint(base.ObjectSheet).Y < this.WorkingArea.Top)
                //{
                //    flag = true;
                //}
                //if (point2.GetPoint(base.ObjectSheet).Y < this.WorkingArea.Top)
                //{
                //    flag = true;
                //}
                if ((dx >= 0) && (dy >= 0))
                {
                    flag = false;
                }
                if (!flag)
                {
                    point.DeltaX = (this._Start.GetPoint(base.ObjectSheet).X + dx);
                    point.DeltaY = (this._Start.GetPoint(base.ObjectSheet).Y + dy);
                    point2.DeltaX = (this._End.GetPoint(base.ObjectSheet).X + dx);
                    point2.DeltaY = (this._End.GetPoint(base.ObjectSheet).Y + dy);
                    this._Start = point;
                    this._End = point2;
                    this.Invalidate();
                }
            }
            catch { }
        }

        public override void Resize(int dx, int dy)
        {
            try
            { 
                ObjectPoint point = new ObjectPoint(this._Start.GetPoint(base.ObjectSheet));
                ObjectPoint point2 = new ObjectPoint(this._End.GetPoint(base.ObjectSheet));
                bool flag = false;
                if (point.GetPoint(base.ObjectSheet).X < this.WorkingArea.Left)
                {
                    flag = true;
                }
                if (point2.GetPoint(base.ObjectSheet).X < this.WorkingArea.Left)
                {
                    flag = true;
                }
                if (point.GetPoint(base.ObjectSheet).Y < this.WorkingArea.Top)
                {
                    flag = true;
                }
                if (point2.GetPoint(base.ObjectSheet).Y < this.WorkingArea.Top)
                {
                    flag = true;
                }
                if ((dx >= 0) && (dy >= 0))
                {
                    flag = false;
                }
                if (!flag)
                {
                    point2.DeltaX = (this._End.GetPoint(base.ObjectSheet).X + dx);
                    point2.DeltaY = (this._End.GetPoint(base.ObjectSheet).Y + dy);
                    this._Start = point;
                    this._End = point2;
                    this.Invalidate();
                }
            }
            catch { }
        }

        public override void MoveHandleTo(ObjectPoint point, int handleNumber)
        {
            try
            { 
                if (handleNumber == 1)
                {
                    this._Start = point;
                }
                else
                {
                    this._End = point;
                }
                this.Invalidate();
            }
            catch { }
        }

        protected override bool PointInObject(ObjectPoint point)
        {
            this.CreateHitTestObjects();
            if (this.AreaRegion == null) return false;
            return this.AreaRegion.IsVisible(point.GetPoint(base.ObjectSheet));
        }

        protected GraphicsPath AreaPath
        {
            get
            {
                return this.HitAreaPath;
            }
            set
            {
                this.HitAreaPath = value;
            }
        }

        protected Pen AreaPen
        {
            get
            {
                return this.HitAreaPen;
            }
            set
            {
                this.HitAreaPen = value;
            }
        }

        protected Region AreaRegion
        {
            get
            {
                return this.HitAreaRegion;
            }
            set
            {
                this.HitAreaRegion = value;
            }
        }

        public override int HandleCount
        {
            get
            {
                return 2;
            }
        }
    }
}

