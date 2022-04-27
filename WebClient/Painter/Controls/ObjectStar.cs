using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using YLWService;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public class ObjectStar : ObjectBox
    {
        private ObjectPoint[] _PointList;
        [NonSerialized]
        private GraphicsPath HitAreaPath;

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

        public ObjectStar(MySheet sheet) : this(sheet, 0, 0, 10, 10)
        {
        }

        public ObjectStar(MySheet sheet, int x1, int y1, int x2, int y2) : base(sheet)
        {
            this._Start.DeltaX = x1;
            this._Start.DeltaY = y1;
            this._End.DeltaX = x2;
            this._End.DeltaY = y2;
            this._PointList = new ObjectPoint[] 
            {
                new ObjectPoint(7, 43),
                new ObjectPoint(25, 44),
                new ObjectPoint(20, 26),
                new ObjectPoint(36, 33),
                new ObjectPoint(31, 13),
                new ObjectPoint(49, 29),
                new ObjectPoint(56, 9),
                new ObjectPoint(61, 23),
                new ObjectPoint(76, 11),
                new ObjectPoint(75, 22),
                new ObjectPoint(91, 20),
                new ObjectPoint(76, 33),
                new ObjectPoint(90, 42),
                new ObjectPoint(76, 43),
                new ObjectPoint(82, 64),
                new ObjectPoint(61, 57),
                new ObjectPoint(64, 81),
                new ObjectPoint(50, 64),
                new ObjectPoint(50, 87),
                new ObjectPoint(40, 68),
                new ObjectPoint(33, 88),
                new ObjectPoint(30, 67),
                new ObjectPoint(10, 78),
                new ObjectPoint(23, 56) }
            ;
        }

        public override ObjectBase Clone()
        {
            ObjectStar objectBase = new ObjectStar(base.ObjectSheet);
            objectBase._Start = this._Start;
            objectBase._End = this._End;
            base.SetObjectFields(objectBase);
            return objectBase;
        }

        protected void CreateHitTestObjects()
        {
            try
            { 
                if (this.AreaPath == null)
                {
                    this.AreaPath = new GraphicsPath();
                    float rateX = Utils.ToFloat((this._End.DeltaX - this._Start.DeltaX) / 100.0);
                    float rateY = Utils.ToFloat((this._End.DeltaY - this._Start.DeltaY) / 100.0);
                    PointF[] points = new PointF[_PointList.Length];
                    for (int i = 0; i < _PointList.Length; i++)
                    {
                        points[i] = _PointList[i].GetScalePos(_PointList[i], rateX, rateY);
                        points[i].X += this._Start.DeltaX;
                        points[i].Y += this._Start.DeltaY;
                    }
                    this.AreaPath.AddPolygon(points);
                    this.AreaPath.CloseFigure();
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
                Pen pen = new Pen(base.PenColor, (float)base.PenWidth);
                pen.StartCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;
                pen.EndCap = LineCap.Round;

                float rateX = Utils.ToFloat((this._End.DeltaX - this._Start.DeltaX) / 100.0);
                float rateY = Utils.ToFloat((this._End.DeltaY - this._Start.DeltaY) / 100.0);
                PointF[] points = new PointF[_PointList.Length];
                for (int i = 0; i < _PointList.Length; i++)
                {
                    points[i] = _PointList[i].GetScalePos(_PointList[i], rateX, rateY);
                    points[i].X += this._Start.DeltaX;
                    points[i].Y += this._Start.DeltaY;
                }

                if (base.FillStyle == FillStyle.Fill)
                {
                    g.FillPolygon(new SolidBrush(base.FillColor), points);
                }
                if (base.PenStyle == PenStyle.Line) pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                if (base.PenStyle == PenStyle.Dash) pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                if (base.PenStyle != PenStyle.None)
                {
                    g.DrawPolygon(pen, points);
                }

                //if (base.Selected)
                //{
                //    Pen penbox = new Pen(base.PenColor, 1);
                //    penbox.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                //    g.DrawRectangle(penbox, ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End)));
                //}
                g.SmoothingMode = SmoothingMode.Default;
                pen.Dispose();
            }
            catch { }
        }

        public override GraphicsPath GetGraphicPath()
        {
            this.Invalidate();
            CreateHitTestObjects();
            return this.AreaPath;
        }

        protected void Invalidate()
        {
            if (this.AreaPath != null)
            {
                this.AreaPath.Dispose();
                this.AreaPath = null;
            }
        }
    }
}

