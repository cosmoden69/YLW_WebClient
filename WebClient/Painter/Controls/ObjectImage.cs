using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public class ObjectImage : ObjectBox
    {
        Image _baseImage;

        public ObjectImage(MySheet sheet) : this(sheet, 0, 0, 10, 10)
        {
        }

        public ObjectImage(MySheet sheet, int x1, int y1, int x2, int y2) : base(sheet)
        {
            this._Start.DeltaX = x1;
            this._Start.DeltaY = y1;
            this._End.DeltaX = x2;
            this._End.DeltaY = y2;
        }

        public override ObjectBase Clone()
        {
            ObjectImage objectBase = new ObjectImage(base.ObjectSheet);
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
                if (_baseImage != null)
                {
                    Size newsize = new Size(Math.Abs(this._End.DeltaX - this._Start.DeltaX), Math.Abs(this._End.DeltaY - this._Start.DeltaY));
                    //Image img = resizeImage(_baseImage, newsize);
                    Image img = new Bitmap(_baseImage, newsize);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.DrawImage(img, this._Start.DeltaX, this._Start.DeltaY);
                }
                if (base.Selected)
                {
                    Pen pen = new Pen(base.PenColor, 1);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawRectangle(pen, ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End)));
                    pen.Dispose();
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
                if (point2.DeltaX - point.DeltaX + dx < 30)
                {
                    flag = true;
                }
                if (point2.DeltaY - point.DeltaY + dy < 30)
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
                        if (left > right - 30) left = right - 30;
                        if (top > bottom - 30) top = bottom - 30;
                        break;

                    case 2:
                        top = point.GetPoint(base.ObjectSheet).Y;
                        if (top > bottom - 30) top = bottom - 30;
                        break;

                    case 3:
                        right = point.GetPoint(base.ObjectSheet).X;
                        top = point.GetPoint(base.ObjectSheet).Y;
                        if (right < left + 30) right = left + 30;
                        if (top > bottom - 30) top = bottom - 30;
                        break;

                    case 4:
                        right = point.GetPoint(base.ObjectSheet).X;
                        if (right < left + 30) right = left + 30;
                        break;

                    case 5:
                        right = point.GetPoint(base.ObjectSheet).X;
                        bottom = point.GetPoint(base.ObjectSheet).Y;
                        if (right < left + 30) right = left + 30;
                        if (bottom < top + 30) bottom = top + 30;
                        break;

                    case 6:
                        bottom = point.GetPoint(base.ObjectSheet).Y;
                        if (bottom < top + 30) bottom = top + 30;
                        break;

                    case 7:
                        left = point.GetPoint(base.ObjectSheet).X;
                        bottom = point.GetPoint(base.ObjectSheet).Y;
                        if (left > right - 30) left = right - 30;
                        if (bottom < top + 30) bottom = top + 30;
                        break;

                    case 8:
                        left = point.GetPoint(base.ObjectSheet).X;
                        if (left > right - 30) left = right - 30;
                        break;
                }
                this._Start.DeltaX = left;
                this._Start.DeltaY = top;
                this._End.DeltaX = right;
                this._End.DeltaY = bottom;
            }
            catch { }
        }

        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            try
            {
                //Get the image current width  
                int sourceWidth = imgToResize.Width;
                //Get the image current height  
                int sourceHeight = imgToResize.Height;
                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;
                //Calulate  width with new desired size  
                nPercentW = ((float)size.Width / (float)sourceWidth);
                //Calculate height with new desired size  
                nPercentH = ((float)size.Height / (float)sourceHeight);
                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;
                //New Width  
                int destWidth = (int)(sourceWidth * nPercent);
                //New Height  
                int destHeight = (int)(sourceHeight * nPercent);
                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((System.Drawing.Image)b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // Draw image with new width and height  
                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                g.Dispose();
                return (System.Drawing.Image)b;
            }
            catch
            {
                return null;
            }
        }

        public void SetImage(Image img)
        {
            this._baseImage = img;
        }
    }
}

