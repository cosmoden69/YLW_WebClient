using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public class ObjectBox : ObjectBase
    {
        internal ObjectPoint _Start;
        internal ObjectPoint _End;

        public ObjectBox(MySheet sheet) : this(sheet, 0, 0, 10, 10)
        {
        }

        public ObjectBox(MySheet sheet, int x1, int y1, int x2, int y2) : base(sheet)
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
            ObjectBox objectBase = new ObjectBox(base.ObjectSheet);
            objectBase._Start = this._Start;
            objectBase._End = this._End;
            base.SetObjectFields(objectBase);
            return objectBase;
        }

        public override void Draw(MySheet sheet, Graphics g)
        {
            try
            { 
                base.WorkingArea = sheet.WorkingArea;
                g.SetClip(base.WorkingArea);
                if (base.FillStyle == FillStyle.Fill)
                {
                    g.FillRectangle(new SolidBrush(base.FillColor), ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End)));
                }
                Pen pen = new Pen(base.PenColor, (float)base.PenWidth);
                if (base.PenStyle == PenStyle.Line) pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                if (base.PenStyle == PenStyle.Dash) pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                if (base.PenStyle != PenStyle.None)
                {
                    g.DrawRectangle(pen, ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End)));
                }
                pen.Dispose();
            }
            catch { }
        }

        public override GraphicsPath GetGraphicPath()
        {
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddRectangle(ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End)));
            return myPath;
        }

        public override ObjectPoint GetHandle(int handleNumber)
        {
            Rectangle objectRectangle = this.GetObjectRectangle(this._Start, this._End);
            int num3 = objectRectangle.X + (objectRectangle.Width / 2);
            int num4 = objectRectangle.Y + (objectRectangle.Height / 2);
            int x = objectRectangle.X;
            int y = objectRectangle.Y;
            switch (handleNumber)
            {
                case 1:
                    x = objectRectangle.X;
                    y = objectRectangle.Y;
                    break;

                case 2:
                    x = num3;
                    y = objectRectangle.Y;
                    break;

                case 3:
                    x = objectRectangle.Right;
                    y = objectRectangle.Y;
                    break;

                case 4:
                    x = objectRectangle.Right;
                    y = num4;
                    break;

                case 5:
                    x = objectRectangle.Right;
                    y = objectRectangle.Bottom;
                    break;

                case 6:
                    x = num3;
                    y = objectRectangle.Bottom;
                    break;

                case 7:
                    x = objectRectangle.X;
                    y = objectRectangle.Bottom;
                    break;

                case 8:
                    x = objectRectangle.X;
                    y = num4;
                    break;
            }
            ObjectPoint point = new ObjectPoint(x, y);
            return point;
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                    return new MyCursor().ObjectNWSEArrowCursor;

                case 2:
                    return new MyCursor().ObjectNSArrowCursor;

                case 3:
                    return new MyCursor().ObjectNESWArrowCursor;

                case 4:
                    return new MyCursor().ObjectWEArrowCursor;

                case 5:
                    return new MyCursor().ObjectNWSEArrowCursor;

                case 6:
                    return new MyCursor().ObjectNSArrowCursor;

                case 7:
                    return new MyCursor().ObjectNESWArrowCursor;

                case 8:
                    return new MyCursor().ObjectWEArrowCursor;
            }
            return new MyCursor().ObjectDefaultCursor;
        }

        public Rectangle GetObjectRectangle(ObjectPoint startPoint, ObjectPoint endPoint)
        {
            Point location = startPoint.GetPoint(base.ObjectSheet);
            Point point = endPoint.GetPoint(base.ObjectSheet);
            return new Rectangle(location, new Size(point.X - location.X, point.Y - location.Y));
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
            return ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End)).IntersectsWith(rectangle);
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
                if (point2.GetPoint(base.ObjectSheet).X + dx <= point.GetPoint(base.ObjectSheet).X)
                {
                    flag = true;
                }
                if (point2.GetPoint(base.ObjectSheet).Y + dy <= point.GetPoint(base.ObjectSheet).Y)
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
                }
            }
            catch { }
        }

        public override void MoveHandleTo(ObjectPoint point, int handleNumber)
        {
            try
            { 
                int left = this.GetObjectRectangle(this._Start, this._End).Left;
                int top = this.GetObjectRectangle(this._Start, this._End).Top;
                int right = this.GetObjectRectangle(this._Start, this._End).Right;
                int bottom = this.GetObjectRectangle(this._Start, this._End).Bottom;
                switch (handleNumber)
                {
                    case 1:
                        left = point.GetPoint(base.ObjectSheet).X;
                        top = point.GetPoint(base.ObjectSheet).Y;
                        break;

                    case 2:
                        top = point.GetPoint(base.ObjectSheet).Y;
                        break;

                    case 3:
                        right = point.GetPoint(base.ObjectSheet).X;
                        top = point.GetPoint(base.ObjectSheet).Y;
                        break;

                    case 4:
                        right = point.GetPoint(base.ObjectSheet).X;
                        break;

                    case 5:
                        right = point.GetPoint(base.ObjectSheet).X;
                        bottom = point.GetPoint(base.ObjectSheet).Y;
                        break;

                    case 6:
                        bottom = point.GetPoint(base.ObjectSheet).Y;
                        break;

                    case 7:
                        left = point.GetPoint(base.ObjectSheet).X;
                        bottom = point.GetPoint(base.ObjectSheet).Y;
                        break;

                    case 8:
                        left = point.GetPoint(base.ObjectSheet).X;
                        break;
                }
                this._Start.DeltaX = left;
                this._Start.DeltaY = top;
                this._End.DeltaX = right;
                this._End.DeltaY = bottom;
            }
            catch { }
        }

        public override void Normalize()
        {
            Rectangle normalizedRectangle = ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End));
            this._Start.DeltaX = normalizedRectangle.Left;
            this._Start.DeltaY = normalizedRectangle.Top;
            this._End.DeltaX = normalizedRectangle.Right;
            this._End.DeltaY = normalizedRectangle.Bottom;
        }

        protected override bool PointInObject(ObjectPoint point)
        {
            return ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End)).Contains(point.GetPoint(base.ObjectSheet));
        }

        public override int HandleCount
        {
            get
            {
                return 8;
            }
        }
    }
}

