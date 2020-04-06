using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vectty.ZXClasses
{
    public enum ZXColor
    {
        Black,
        Blue,
        Red,
        Magenta,
        Green,
        Cyan,
        Yellow,
        White
        
    }
    public class ZXAttribute
    {
        public ZXColor Paper { get; set; } = ZXColor.White;
        public ZXColor Ink { get; set; } = ZXColor.Black;
        public bool Bright { get; set; }
        public bool Flash { get; set; }
        public Color RGBPaper { get { return ZXColorToRGB(Paper, Bright); } }
        public Color RGBInk { get { return ZXColorToRGB(Ink, Bright); } }
        public static ZXAttribute FromByte(byte Value)
        {
            return new ZXAttribute 
            { 
                Paper = (ZXColor)((Value >> 3) & 7), 
                Ink = (ZXColor)(Value & 7), 
                Bright = (Value & 64) != 0, 
                Flash = (Value & 128) != 0 
            };

        }
        public void LoadFromByte(byte Value)
        {
            Paper = (ZXColor)((Value >> 3) & 7);
            Ink = (ZXColor)(Value & 7);
            Bright = (Value & 64) != 0;
            Flash = (Value & 128) != 0;
        }
        private static Color ZXColorToRGB(ZXColor ColorToConvert, bool Bright)
        {
            if (!Bright)
            {
                switch (ColorToConvert)
                {
                    case ZXColor.Black:

                        return Color.Black;

                    case ZXColor.Blue:

                        return Color.FromArgb(0, 0, 192);

                    case ZXColor.Red:

                        return Color.FromArgb(192, 0, 0);

                    case ZXColor.Magenta:

                        return Color.FromArgb(192, 0, 192);

                    case ZXColor.Green:

                        return Color.FromArgb(0, 192, 0);

                    case ZXColor.Cyan:

                        return Color.FromArgb(0, 192, 192);

                    case ZXColor.Yellow:

                        return Color.FromArgb(192, 192, 0);

                    case ZXColor.White:

                        return Color.FromArgb(192, 192, 192);

                }
            }
            else
            {
                switch (ColorToConvert)
                {
                    case ZXColor.Black:

                        return Color.Black;

                    case ZXColor.Blue:

                        return Color.FromArgb(0, 0, 255);

                    case ZXColor.Red:

                        return Color.FromArgb(255, 0, 0);

                    case ZXColor.Magenta:

                        return Color.FromArgb(255, 0, 255);

                    case ZXColor.Green:

                        return Color.FromArgb(0, 255, 0);

                    case ZXColor.Cyan:

                        return Color.FromArgb(0, 255, 255);

                    case ZXColor.Yellow:

                        return Color.FromArgb(255, 255, 0);

                    case ZXColor.White:

                        return Color.FromArgb(255, 255, 255);

                }
            }

            return Color.Black;
        }

        public byte ToByte()
        {
            return (byte)(((byte)Paper << 3) | (byte)Ink | ((Bright ? 1 : 0) << 6) | ((Flash ? 1 : 0) << 7));
        }

    }

    public class ZXChar
    {
        public Rectangle Area { get; private set; }
        public Rectangle GridArea(int Scale) { return new Rectangle(Area.X  * Scale + 1, Area.Y * Scale + 1, Area.Width * Scale - 1, Area.Height * Scale - 1); }
        public Rectangle DoubleArea(int Scale) { return new Rectangle(Area.X * Scale, Area.Y * Scale, Area.Width * Scale, Area.Height * Scale); }

        public int X { get; private set; }
        public int Y { get; private set; }
        public ZXAttribute Attribute { get; private set; }

        public ZXChar(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            Area = new Rectangle(X * 8, Y * 8, 8, 8);
            Attribute = new ZXAttribute();
        }

        public ZXChar Clone()
        {
            ZXChar clon = new ZXChar(X, Y);
            clon.Attribute.Bright = Attribute.Bright;
            clon.Attribute.Ink = Attribute.Ink;
            clon.Attribute.Paper = Attribute.Paper;
            clon.Attribute.Flash = Attribute.Flash;
            return clon;
        }
    }
}
