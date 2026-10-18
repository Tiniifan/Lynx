using System;

namespace Lynx.InazumaEleven.Logic
{
    /// <summary>
    /// The ten stats of a player, in the exact order the game reads them
    /// (CHARA_PARAM_INFO field order, and index order of the growth tables).
    /// Display names live in Common/PlayerStats.json.
    /// </summary>
    public enum PlayerStats
    {
        FP = 0,
        TP = 1,
        Kick = 2,
        Dribble = 3,
        Technique = 4,
        Block = 5,
        Speed = 6,
        Stamina = 7,
        Catch = 8,
        Luck = 9,
    }
}
