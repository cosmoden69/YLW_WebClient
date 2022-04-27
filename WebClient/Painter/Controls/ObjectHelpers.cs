using System;
using System.Drawing;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectHelpers
    {
        public static Rectangle GetNormalizedRectangle(Rectangle r)
        {
            return GetNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }

        public static Rectangle GetNormalizedRectangle(Point p1, Point p2)
        {
            return GetNormalizedRectangle(p1.X, p1.Y, p2.X, p2.Y);
        }

        public static Rectangle GetNormalizedRectangle(int x1, int y1, int x2, int y2)
        {
            if (x2 < x1)
            {
                int num = x2;
                x2 = x1;
                x1 = num;
            }
            if (y2 < y1)
            {
                int num2 = y2;
                y2 = y1;
                y1 = num2;
            }
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }
    }
}

