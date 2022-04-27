using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public class ObjectEraser : ObjectLine
    {
        private List<ObjectPoint> _PointList;

        public ObjectEraser(MySheet sheet) : base(sheet)
        {
            this._PointList = new List<ObjectPoint>();
        }

        public ObjectEraser(MySheet sheet, int x1, int y1, int x2, int y2) : base(sheet)
        {
            this._PointList = new List<ObjectPoint>();
            this._PointList.Add(new ObjectPoint(x1, y1));
            this._PointList.Add(new ObjectPoint(x2, y2));
        }

        public void AddPoint(ObjectPoint point)
        {
            this._PointList.Add(point);
        }

        public override ObjectBase Clone()
        {
            ObjectEraser objectBase = new ObjectEraser(base.ObjectSheet);
            foreach (ObjectPoint point in this._PointList)
            {
                objectBase._PointList.Add(point);
            }
            base.SetObjectFields(objectBase);
            return objectBase;
        }

        public override bool IntersectsWith(Rectangle rectangle)
        {
            return true;
        }

        public override void Draw(MySheet sheet, Graphics g)
        {
            try
            { 
                base.Invalidate();
                base.WorkingArea = sheet.WorkingArea;
                g.SetClip(base.WorkingArea);
                int x = 0;
                int y = 0;
                //g.SmoothingMode = SmoothingMode.AntiAlias;
                Pen pen = new Pen(sheet.TransparentColor, 20);
                pen.StartCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;
                pen.EndCap = LineCap.Round;
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
                    g.DrawLine(pen, x, y, num3, num4);
                    x = num3;
                    y = num4;
                }
                g.SmoothingMode = SmoothingMode.Default;
                pen.Dispose();
            }
            catch { }
        }
    }
}

