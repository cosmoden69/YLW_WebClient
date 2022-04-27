using System;
using System.Drawing;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public class ObjectPoint
    {
        private int _DeltaX;
        private int _DeltaY;

        public ObjectPoint(Point deltaP) : this(deltaP.X, deltaP.Y)
        {
        }

        public ObjectPoint(int deltaX, int deltaY)
        {
            this._DeltaX = deltaX;
            this._DeltaY = deltaY;
        }

        public Point GetPoint(MySheet sheet)
        {
            Point BasePoint = sheet.GetBasePosition();
            return new Point(BasePoint.X + this.DeltaX, BasePoint.Y + this.DeltaY);
        }

        public override string ToString()
        {
            return ("DX : " + this._DeltaX.ToString() + ", DY : " + this._DeltaY.ToString());
        }

        public int DeltaX
        {
            get
            {
                return this._DeltaX;
            }
            set
            {
                this._DeltaX = value;
            }
        }

        public int DeltaY
        {
            get
            {
                return this._DeltaY;
            }
            set
            {
                this._DeltaY = value;
            }
        }

        public PointF GetScalePos(ObjectPoint point, float rateX, float rateY)
        {
            return new PointF(point.DeltaX * rateX, point.DeltaY * rateY);
        }
    }
}

