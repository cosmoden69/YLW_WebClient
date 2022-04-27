using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public class ObjectCircle : ObjectBox
    {
        public ObjectCircle(MySheet sheet) : this(sheet, 0, 0, 10, 10)
        {
        }

        public ObjectCircle(MySheet sheet, int x1, int y1, int x2, int y2) : base(sheet)
        {
            this._Start.DeltaX = x1;
            this._Start.DeltaY = y1;
            this._End.DeltaX = x2;
            this._End.DeltaY = y2;
        }

        public override ObjectBase Clone()
        {
            ObjectCircle objectBase = new ObjectCircle(base.ObjectSheet);
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
                    g.FillEllipse(new SolidBrush(base.FillColor), ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End)));
                }
                Pen pen = new Pen(base.PenColor, (float)base.PenWidth);
                if (base.PenStyle == PenStyle.Line) pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                if (base.PenStyle == PenStyle.Dash) pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                if (base.PenStyle != PenStyle.None)
                { 
                    g.DrawEllipse(pen, ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End)));
                }
                pen.Dispose();
            }
            catch { }
        }

        public override GraphicsPath GetGraphicPath()
        {
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse(ObjectHelpers.GetNormalizedRectangle(this.GetObjectRectangle(this._Start, this._End)));
            return myPath;
        }
    }
}

