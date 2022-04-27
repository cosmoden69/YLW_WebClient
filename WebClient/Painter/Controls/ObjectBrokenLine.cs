using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public class ObjectBrokenLine : ObjectLine
    {
        private List<ObjectPoint> _PointList;
        public bool bClosed = false;

        public ObjectBrokenLine(MySheet sheet) : base(sheet)
        {
        }

        public ObjectBrokenLine(MySheet sheet, int x1, int y1, int x2, int y2) : base(sheet)
        {
            this._PointList = new List<ObjectPoint>();
            this._PointList.Add(new ObjectPoint(x1, y1));
            this._PointList.Add(new ObjectPoint(x2, y2));
        }

        public void AddPoint(ObjectPoint point)
        {
            this._PointList.Add(point);
        }

        public void RemoveAtPoint(int idx)
        {
            this._PointList.RemoveAt(idx);
        }

        public override ObjectBase Clone()
        {
            ObjectBrokenLine objectBase = new ObjectBrokenLine(base.ObjectSheet);
            foreach (ObjectPoint point in this._PointList)
            {
                objectBase._PointList.Add(point);
            }
            base.SetObjectFields(objectBase);
            return objectBase;
        }

        protected override void CreateHitTestObjects()
        {
            try
            { 
                if (base.AreaPath == null)
                {
                    if (this._PointList.Count <= 2)
                    {
                        base.AreaPath = new GraphicsPath();
                        base.AreaPen = new Pen(Color.Black, 7f);
                        base.AreaPath.AddLine(base._Start.GetPoint(base.ObjectSheet).X, base._Start.GetPoint(base.ObjectSheet).Y, base._End.GetPoint(base.ObjectSheet).X, base._End.GetPoint(base.ObjectSheet).Y);
                        base.AreaPath.Widen(base.AreaPen);
                        base.AreaRegion = new Region(this.AreaPath);
                    }
                    else
                    {
                        base.AreaPath = new GraphicsPath();
                        int x = 0;
                        int y = 0;
                        IEnumerator<ObjectPoint> enumerator = this._PointList.GetEnumerator();
                        if (enumerator.MoveNext())
                        {
                            x = enumerator.Current.GetPoint(base.ObjectSheet).X;
                            y = enumerator.Current.GetPoint(base.ObjectSheet).Y;
                        }
                        while (enumerator.MoveNext())
                        {
                            int num3 = enumerator.Current.GetPoint(base.ObjectSheet).X;
                            int num4 = enumerator.Current.GetPoint(base.ObjectSheet).Y;
                            base.AreaPath.AddLine(x, y, num3, num4);
                            x = num3;
                            y = num4;
                        }
                        base.AreaPath.CloseFigure();
                        base.AreaRegion = new Region(base.AreaPath);
                    }
                }
            }
            catch { }
        }

        public override void Draw(MySheet sheet, Graphics g)
        {
            try
            { 
                base.WorkingArea = sheet.WorkingArea;
                g.SetClip(base.WorkingArea);
                GraphicsPath path = new GraphicsPath();
                int x = 0;
                int y = 0;
                //g.SmoothingMode = SmoothingMode.AntiAlias;  //AntiAlias : 색깔채우기가 잘 안됨
                Pen pen = new Pen(base.PenColor, (float)base.PenWidth);
                pen.StartCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;
                pen.EndCap = LineCap.Round;
                if (base.PenStyle == PenStyle.Line) pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                if (base.PenStyle == PenStyle.Dash) pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                IEnumerator<ObjectPoint> enumerator = this._PointList.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    x = enumerator.Current.GetPoint(sheet).X;
                    y = enumerator.Current.GetPoint(sheet).Y;
                }
                while (enumerator.MoveNext())
                {
                    int num3 = enumerator.Current.GetPoint(sheet).X;
                    int num4 = enumerator.Current.GetPoint(sheet).Y;
                    path.AddLine(x, y, num3, num4);
                    x = num3;
                    y = num4;
                }
                if (bClosed)
                {
                    path.CloseFigure();
                    if (base.FillStyle == FillStyle.Fill)
                    {
                        g.FillPolygon(new SolidBrush(base.FillColor), path.PathPoints);
                    }
                }
                g.DrawLines(pen, path.PathPoints);
                path.Dispose();
                g.SmoothingMode = SmoothingMode.Default;
                pen.Dispose();
            }
            catch { }
        }

        public override GraphicsPath GetGraphicPath()
        {
            base.Invalidate();
            CreateHitTestObjects();
            return base.AreaPath;
        }

        public override ObjectPoint GetHandle(int handleNumber)
        {
            if (handleNumber < 1)
            {
                handleNumber = 1;
            }
            if (handleNumber > this._PointList.Count)
            {
                handleNumber = this._PointList.Count;
            }
            return this._PointList[handleNumber - 1];
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            return new MyCursor().ObjectNESWArrowCursor;
        }

        public override bool IntersectsWith(Rectangle rectangle)
        {
            this.Invalidate();
            //if (this._PointList.Count <= 2) return true;
            this.CreateHitTestObjects();
            if (base.AreaRegion == null) return false;
            return base.AreaRegion.IsVisible(rectangle);
        }

        public override void Move(int dx, int dy)
        {
            try
            { 
                Point point = new Point(0x7fffffff, 0x7fffffff);
                Point point2 = new Point(-2147483648, -2147483648);
                Point point3 = new Point();
                for (int i = 0; i < this._PointList.Count; i++)
                {
                    point3 = this._PointList[i].GetPoint(base.ObjectSheet);
                    if (point3.X < point.X)
                    {
                        point.X = point3.X;
                    }
                    if (point3.Y < point.Y)
                    {
                        point.Y = point3.Y;
                    }
                    if (point3.X > point2.X)
                    {
                        point2.X = point3.X;
                    }
                    if (point3.Y > point2.Y)
                    {
                        point2.Y = point3.Y;
                    }
                }
                bool flag = false;
                //if (point.X < this.WorkingArea.Left)
                //{
                //    flag = true;
                //}
                //if (point2.X < this.WorkingArea.Left)
                //{
                //    flag = true;
                //}
                //if (point.Y < this.WorkingArea.Top)
                //{
                //    flag = true;
                //}
                //if (point2.Y < this.WorkingArea.Top)
                //{
                //    flag = true;
                //}
                if ((dx >= 0) && (dy >= 0))
                {
                    flag = false;
                }
                if (!flag)
                {
                    for (int j = 0; j < this._PointList.Count; j++)
                    {
                        ObjectPoint point4 = new ObjectPoint(0, 0);
                        point4.DeltaX = (this._PointList[j].GetPoint(base.ObjectSheet).X + dx);
                        point4.DeltaY = (this._PointList[j].GetPoint(base.ObjectSheet).Y + dy);
                        this._PointList[j] = point4;
                    }
                    base.Invalidate();
                }
            }
            catch { }
        }

        public override void MoveHandleTo(ObjectPoint point, int handleNumber)
        {
            try
            { 
                if (handleNumber < 1)
                {
                    handleNumber = 1;
                }
                if (handleNumber > this._PointList.Count)
                {
                    handleNumber = this._PointList.Count;
                }
                this._PointList[handleNumber - 1] = point;
                base.Invalidate();
            }
            catch { }
        }

        public override int HandleCount
        {
            get
            {
                return this._PointList.Count;
            }
        }
    }
}

