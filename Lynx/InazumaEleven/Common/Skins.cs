using System;
using System.Drawing;
using System.ComponentModel;

namespace Lynx.InazumaEleven.Common
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ColorAttribute : Attribute
    {
        public Color Color { get; }

        public ColorAttribute(int red, int green, int blue)
        {
            Color = Color.FromArgb(red, green, blue);
        }
    }

    public enum Skins
    {
        [Color(0, 0, 0)]
        [Description("Unused")]
        Color0 = 0,

        [Color(241, 182, 123)]
        [Description("Color 1")]
        Color1 = 1,

        [Color(255, 201, 136)]
        [Description("Color 2")]
        Color2 = 2,

        [Color(248, 200, 154)]
        [Description("Color 3")]
        Color3 = 3,

        [Color(255, 255, 117)]
        [Description("Color 4")]
        Color4 = 4,

        [Color(218, 198, 168)]
        [Description("Color 5")]
        Color5 = 5,

        [Color(201, 149, 107)]
        [Description("Color 6")]
        Color6 = 6,

        [Color(228, 163, 99)]
        [Description("Color 7")]
        Color7 = 7,

        [Color(186, 141, 108)]
        [Description("Color 8")]
        Color8 = 8,

        [Color(136, 180, 165)]
        [Description("Color 9")]
        Color9 = 9,

        [Color(199, 116, 84)]
        [Description("Color 10")]
        Color10 = 10,

        [Color(187, 134, 82)]
        [Description("Color 11")]
        Color11 = 11,

        [Color(247, 229, 203)]
        [Description("Color 12")]
        Color12 = 12,

        [Color(240, 207, 176)]
        [Description("Color 13")]
        Color13 = 13,

        [Color(172, 136, 100)]
        [Description("Color 14")]
        Color14 = 14,
    }
}
