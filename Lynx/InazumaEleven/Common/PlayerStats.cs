using System;
using System.ComponentModel;

namespace Lynx.InazumaEleven.Common
{
    /// <summary>
    /// The ten stats of a player, in the exact order the game reads them
    /// (CHARA_PARAM_INFO field order, and index order of the growth tables).
    /// </summary>
    public enum PlayerStats
    {
        [Description("FP")]
        FP = 0,

        [Description("TP")]
        TP = 1,

        [Description("Kick")]
        Kick = 2,

        [Description("Dribble")]
        Dribble = 3,

        [Description("Technique")]
        Technique = 4,

        [Description("Block")]
        Block = 5,

        [Description("Speed")]
        Speed = 6,

        [Description("Stamina")]
        Stamina = 7,

        [Description("Catch")]
        Catch = 8,

        [Description("Luck")]
        Luck = 9,
    }
}
