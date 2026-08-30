using System;
using System.ComponentModel;

namespace Lynx.InazumaEleven.Common
{
    public enum PalpackConditionTypes
    {
        [Description("None")]
        None = 0,

        [Description("Item")]
        Item = 1,

        [Description("Player")]
        Player = 2,
    }
}
