using System;
using System.ComponentModel;

namespace Lynx.InazumaEleven.Common
{
    public enum PlayerPositions
    {
        [Description("Unposition")]
        Unposition = 0,

        [Description("Goalkeeper")]
        Goalkeeper = 1,

        [Description("Forward")]
        Forward = 2,

        [Description("Midfielder")]
        Midfielder = 3,

        [Description("Defender")]
        Defender = 4,
    }
}
