using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace YLW_WebClient.Painter.PaintControls
{
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct DefaultValues
    {
        [StructLayout(LayoutKind.Sequential, Size = 1)]
        public struct Defaults
        {
            public static string HeaderFontName;
            public static float HeaderFontSize;
            public static Font CellFont;
            static Defaults()
            {
                HeaderFontName = "굴림";
                HeaderFontSize = 10f;
                CellFont = new Font("굴림", 9f);
            }
        }

        [StructLayout(LayoutKind.Sequential, Size = 1)]
        public struct MySheetColors
        {
            public static readonly Color SheetBackColor;
            public static readonly Color HeaderBackColor;
            public static readonly Color HeaderBorderColor;
            public static readonly Color HeaderSelectionColor;
            public static readonly Color HeaderSelectionBorderColor;
            public static readonly Color CellBackColor;
            public static readonly Color CellForeColor;
            public static readonly Color CellSelectionBackColor;
            public static readonly Color UnMoveableCellForeColor;
            public static readonly Color UnMoveableCellBackColor;
            static MySheetColors()
            {
                SheetBackColor = Color.Silver;
                HeaderBackColor = Color.FromArgb(0xd6, 0xd3, 0xce);
                HeaderBorderColor = Color.Gray;
                HeaderSelectionColor = Color.FromArgb(0xb5, 190, 0xd6);
                HeaderSelectionBorderColor = Color.FromArgb(8, 0x24, 0x6b);
                CellBackColor = Color.White;
                CellForeColor = Color.Black;
                CellSelectionBackColor = Color.FromArgb(50, Color.Purple);
                UnMoveableCellForeColor = Color.FromArgb(30, Color.Black);
                UnMoveableCellBackColor = Color.FromArgb(3, Color.Red);
            }
        }

        [StructLayout(LayoutKind.Sequential, Size = 1)]
        public struct MySheetValues
        {
            public static int CellBorderWidth;
            public static int CellWidth;
            public static int CellHeight;
            public static int RowHeaderWidth;
            public static int MaxRowCount;
            public static int MaxColumnCount;
            static MySheetValues()
            {
                CellBorderWidth = 1;
                CellWidth = 80;
                CellHeight = 0x12;
                RowHeaderWidth = 0x25;
                MaxRowCount = 0x10001;
                MaxColumnCount = 0x101;
            }
        }
    }
}

