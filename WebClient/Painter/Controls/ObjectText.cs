using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public class ObjectText : ObjectBox
    {
        private string rtfString = "";

        public ObjectText(MySheet sheet) : this(sheet, 0, 0, 10, 10)
        {
        }

        public ObjectText(MySheet sheet, int x1, int y1, int x2, int y2) : base(sheet)
        {
            this._Start.DeltaX = x1;
            this._Start.DeltaY = y1;
            this._End.DeltaX = x2;
            this._End.DeltaY = y2;
        }

        public override ObjectBase Clone()
        {
            ObjectText objectBase = new ObjectText(base.ObjectSheet);
            objectBase._Start = this._Start;
            objectBase._End = this._End;
            objectBase.rtfString = this.rtfString;
            base.SetObjectFields(objectBase);
            return objectBase;
        }

        public override void Draw(MySheet sheet, Graphics g)
        {
            try
            { 
                base.WorkingArea = sheet.WorkingArea;
                g.SetClip(base.WorkingArea);

                if (base.Selected)
                {
                    Pen pen = new Pen(base.PenColor, 1);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawRectangle(pen, ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End)));
                }
                else
                {
                    Rectangle rect = ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End));
                    Size newsize = new Size(Math.Abs(this._End.DeltaX - this._Start.DeltaX) - 30, Math.Abs(this._End.DeltaY - this._Start.DeltaY));

                    ////배경이미지가 있을 경우 품질이 저하됨
                    //System.Windows.Forms.RichTextBox rtf = new RichTextBox();
                    //rtf.Rtf = this.rtfString;
                    //Image img = RichTextBoxPrinter.Print(rtf, newsize.Width, newsize.Height);
                    //g.DrawImage(img, this._Start.DeltaX + 4, this._Start.DeltaY + 3);
                    ////배경이미지가 있을 경우 품질이 저하됨

                    //이미지품질저하로 pictureBox Paint 이벤트로 변환
                    //g.SmoothingMode = SmoothingMode.AntiAlias;
                    //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    ////g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    ////g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //g.DrawRtfText(rtfString, rect);
                    //이미지품질저하로 pictureBox Paint 이벤트로 변환 - 아래 코딩
                    rect.Offset(4, 3);
                    Image img = ObjectSheet.GetRtfImage(rtfString, rect);
                    g.DrawImage(img, 0, 0);
                }
            }
            catch { }
        }

        public override void Move(int dx, int dy)
        {
            try
            { 
                base.Move(dx, dy);
                ObjectSheet.MyText.Location = new Point(this._Start.DeltaX + 2, this._Start.DeltaY + 2);
                ObjectSheet.MyText.ObjectMoved();
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
                if (point2.DeltaX - point.DeltaX - 7 + dx < ObjectSheet.MyText.ClientRectangle.Width)
                {
                    flag = true;
                }
                if (point2.DeltaY - point.DeltaY - 7 + dy < ObjectSheet.MyText.ClientRectangle.Height)
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
                ObjectSheet.MyText.Location = new Point(this._Start.DeltaX + 2, this._Start.DeltaY + 2);
                ObjectSheet.MyText.ObjectMoved();
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
                        if (left > right - 7 - ObjectSheet.MyText.ClientRectangle.Width) left = right - 7 - ObjectSheet.MyText.ClientRectangle.Width;
                        if (top > bottom - 7 - ObjectSheet.MyText.ClientRectangle.Height) top = bottom - 7 - ObjectSheet.MyText.ClientRectangle.Height;
                        break;

                    case 2:
                        top = point.GetPoint(base.ObjectSheet).Y;
                        if (top > bottom - 7 - ObjectSheet.MyText.ClientRectangle.Height) top = bottom - 7 - ObjectSheet.MyText.ClientRectangle.Height;
                        break;

                    case 3:
                        right = point.GetPoint(base.ObjectSheet).X;
                        top = point.GetPoint(base.ObjectSheet).Y;
                        if (right < left + 7 + ObjectSheet.MyText.ClientRectangle.Width) right = left + 7 + ObjectSheet.MyText.ClientRectangle.Width;
                        if (top > bottom - 7 - ObjectSheet.MyText.ClientRectangle.Height) top = bottom - 7 - ObjectSheet.MyText.ClientRectangle.Height;
                        break;

                    case 4:
                        right = point.GetPoint(base.ObjectSheet).X;
                        if (right < left + 7 + ObjectSheet.MyText.ClientRectangle.Width) right = left + 7 + ObjectSheet.MyText.ClientRectangle.Width;
                        break;

                    case 5:
                        right = point.GetPoint(base.ObjectSheet).X;
                        bottom = point.GetPoint(base.ObjectSheet).Y;
                        if (right < left + 7 + ObjectSheet.MyText.ClientRectangle.Width) right = left + 7 + ObjectSheet.MyText.ClientRectangle.Width;
                        if (bottom < top + 7 + ObjectSheet.MyText.ClientRectangle.Height) bottom = top + 7 + ObjectSheet.MyText.ClientRectangle.Height;
                        break;

                    case 6:
                        bottom = point.GetPoint(base.ObjectSheet).Y;
                        if (bottom < top + 7 + ObjectSheet.MyText.ClientRectangle.Height) bottom = top + 7 + ObjectSheet.MyText.ClientRectangle.Height;
                        break;

                    case 7:
                        left = point.GetPoint(base.ObjectSheet).X;
                        bottom = point.GetPoint(base.ObjectSheet).Y;
                        if (left > right - 7 - ObjectSheet.MyText.ClientRectangle.Width) left = right - 7 - ObjectSheet.MyText.ClientRectangle.Width;
                        if (bottom < top + 7 + ObjectSheet.MyText.ClientRectangle.Height) bottom = top + 7 + ObjectSheet.MyText.ClientRectangle.Height;
                        break;

                    case 8:
                        left = point.GetPoint(base.ObjectSheet).X;
                        if (left > right - 7 - ObjectSheet.MyText.ClientRectangle.Width) left = right - 7 - ObjectSheet.MyText.ClientRectangle.Width;
                        break;
                }
                this._Start.DeltaX = left;
                this._Start.DeltaY = top;
                this._End.DeltaX = right;
                this._End.DeltaY = bottom;
                ObjectSheet.MyText.Location = new Point(this._Start.DeltaX + 2, this._Start.DeltaY + 2);
                ObjectSheet.MyText.ObjectMoved();
            }
            catch { }
        }

        public void SetObject()
        {
            ObjectSheet.MyText.SetObject(this, this._Start.GetPoint(base.ObjectSheet));
        }


        public void SetRtf(string rtf)
        {
            this.rtfString = rtf;
        }

        public void SetObjectSize(Size size)
        {
            //if (size.Width > this._End.DeltaX - this._Start.DeltaX - 7) this._End.DeltaX = this._Start.DeltaX + size.Width + 7;
            //if (size.Height > this._End.DeltaY - this._Start.DeltaY - 7) this._End.DeltaY = this._Start.DeltaY + size.Height + 7;
            this._End.DeltaX = this._Start.DeltaX + size.Width + 7;
            this._End.DeltaY = this._Start.DeltaY + size.Height + 7;
            this.ObjectSheet.Refresh();
        }
    }
}

