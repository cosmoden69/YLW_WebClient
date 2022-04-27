using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct BasePosition
    {
        private int _X;
        private int _Y;
        public static readonly BasePosition Empty;

        public BasePosition(int x, int y)
        {
            this._X = x;
            this._Y = y;
        }

        static BasePosition()
        {
            Empty = new BasePosition(-1, -1);
        }

        public override int GetHashCode()
        {
            return this._X;
        }

        public Point Position
        {
            get
            {
                return new Point(this._X, this._Y);
            }
            set
            {
                this._X = value.X;
                this._Y = value.Y;
            }
        }
        public bool IsEmpty()
        {
            return this.Equals(Empty);
        }

        public bool Equals(BasePosition position)
        {
            return ((this._X == position._X) && (this._Y == position._Y));
        }

        public override bool Equals(object obj)
        {
            return this.Equals((BasePosition)obj);
        }

        public static bool operator ==(BasePosition left, BasePosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BasePosition left, BasePosition right)
        {
            return !left.Equals(right);
        }
    }
}

