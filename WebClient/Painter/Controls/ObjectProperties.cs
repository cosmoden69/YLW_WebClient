using System;

namespace YLW_WebClient.Painter.PaintControls
{
    public class ObjectProperties
    {
        private System.Drawing.Color? _Color = null;
        private int? _PenWidth = null;

        public System.Drawing.Color? Color
        {
            get
            {
                return this._Color;
            }
            set
            {
                this._Color = value;
            }
        }

        public int? PenWidth
        {
            get
            {
                return this._PenWidth;
            }
            set
            {
                this._PenWidth = value;
            }
        }
    }
}

