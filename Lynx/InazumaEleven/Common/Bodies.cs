using System;
using System.ComponentModel;

namespace Lynx.InazumaEleven.Common
{
    public enum Bodies
    {
        [Description("No Body")]
        NoBody = 0,

        [Description("Normal (Collar)")]
        NormalCollar = 1,

        [Description("Fat")]
        Fat = 2,

        [Description("Bulk")]
        Bulk = 3,

        [Description("Normal")]
        Normal = 4,

        [Description("Short")]
        Short = 5,

        [Description("Tall")]
        Tall = 6,

        [Description("Girl")]
        Girl = 7,

        [Description("Short Fat")]
        ShortFat = 8,
    }
}
