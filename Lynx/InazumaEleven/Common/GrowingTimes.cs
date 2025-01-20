using System;
using System.ComponentModel;

namespace Lynx.InazumaEleven.Common
{
    public enum GrowingTimes
    {
        [Description("No growth time")]
        NoGrowthTime = 0,

        [Description("Fast")]
        Fast = 1,

        [Description("Medium")]
        Medium = 2,

        [Description("Slow")]
        Slow = 3,
    }
}
