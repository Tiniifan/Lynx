using System;
using System.ComponentModel;

namespace Lynx.InazumaEleven.Common
{
    public enum MovePositions
    {
        [Description("Unposition")]
        Unposition = 0,

        [Description("Save")]
        Save = 1,

        [Description("Shot")]
        Shot = 2,

        [Description("Dribble")]
        Dribble = 3,

        [Description("Block")]
        Block = 4,

        [Description("Skill")]
        Skill = 5,
    }
}
