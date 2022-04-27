using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using YLWService;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public class ObjectFill : ObjectBase
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern bool SetPixel(IntPtr hdc, int x, int y, uint crColor);

        private List<Point> _PointList;

        public ObjectFill(MySheet sheet) : this(sheet, 0, 0)
        {
        }

        public ObjectFill(MySheet sheet, int x1, int y1) : base(sheet)
        {
            this._PointList = new List<Point>();
        }

        public void AddPoint(ObjectPoint point)
        {
            this._PointList.Add(point.GetPoint(ObjectSheet));
        }

        public override ObjectBase Clone()
        {
            ObjectFill objectBase = new ObjectFill(base.ObjectSheet);
            foreach (Point point in this._PointList)
            {
                objectBase._PointList.Add(point);
            }
            base.SetObjectFields(objectBase);
            return objectBase;
        }

        public override void Draw(MySheet sheet, Graphics g)
        {
            try
            { 
                base.WorkingArea = sheet.WorkingArea;
                g.SetClip(base.WorkingArea);
                int x = 0;
                int y = 0;
                g.SmoothingMode = SmoothingMode.Default;
                IntPtr hdc = g.GetHdc();
                // NOTE : GDI colors are BGR, not ARGB
                uint fillColor = (uint)((base.FillColor.B << 16) | (base.FillColor.G << 8) | (base.FillColor.R));
                IEnumerator<Point> enumerator = this._PointList.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    x = enumerator.Current.X;
                    y = enumerator.Current.Y;

                    SetPixel(hdc, x, y, fillColor);
                }
                g.ReleaseHdc();
            }
            catch { }
        }

        public override bool IntersectsWith(Rectangle rectangle)
        {
            return true;
        }

        //public void doFloodFill(int x1, int y1)
        //{
        //    try
        //    {
        //        Bitmap img = this.ObjectSheet.GetResultImage();

        //        Point pickPoint = new Point(x1, y1);
        //        Color pickColor = img.GetPixel(x1, y1);

        //        //같은색이라도(ARGB가 같아도 이름등이 다른 경우) 단순 비교는 안됨
        //        if (pickColor.ToArgb().CompareTo(base.FillColor.ToArgb()) == 0) return;

        //        Stack<Point> pixels = new Stack<Point>();
        //        pixels.Push(new Point(x1, y1));

        //        while (pixels.Count > 0)
        //        {
        //            Point i = pixels.Pop();
        //            if (i.X < img.Width && i.X > 0 && i.Y < img.Height && i.Y > 0)
        //            {
        //                if (pickColor.ToArgb().CompareTo(img.GetPixel(i.X, i.Y).ToArgb()) == 0)
        //                {
        //                    img.SetPixel(i.X, i.Y, base.FillColor);
        //                    this._PointList.Add(new Point(i.X, i.Y));
        //                    pixels.Push(new Point(i.X - 1, i.Y));
        //                    pixels.Push(new Point(i.X + 1, i.Y));
        //                    pixels.Push(new Point(i.X, i.Y - 1));
        //                    pixels.Push(new Point(i.X, i.Y + 1));
        //                }
        //            }
        //        }
        //        pixels.Clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        public void doFloodFill(int x1, int y1)
        {
            try
            {
                Bitmap img = this.ObjectSheet.GetResultImage();

                Bitmap32Helper helper = new Bitmap32Helper(img);
                helper.LockBitmap();
                helper.PointList = this._PointList;
                helper.FloodFill(x1, y1, base.FillColor);
                helper.UnlockBitmap();
            }
            catch { }
        }
    }
}

