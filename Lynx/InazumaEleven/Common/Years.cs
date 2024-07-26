using System;
using System.ComponentModel;

namespace Lynx.InazumaEleven.Common
{
    public enum Years
    {
        [Description("Unknown")]
        Unknown = 0,

        [Description("First Year")]
        FirstYear = 1,

        [Description("Second Year")]
        SecondYear = 2,

        [Description("Third Year")]
        ThirdYear = 3,

        [Description("Adult")]
        Adult = 4,
    }
}
