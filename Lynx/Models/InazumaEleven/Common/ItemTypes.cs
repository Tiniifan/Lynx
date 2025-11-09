using System;
using System.ComponentModel;

namespace Lynx.Models.InazumaEleven.Common
{
    public enum ItemTypes
    {
        [Description("Boots")]
        Boots = 1,

        [Description("Gloves")]
        Gloves = 2,

        [Description("Bracelet")]
        Bracelet = 3,

        [Description("Pendant")]
        Pendant = 4,

        [Description("Unused")]
        Unused = 5,

        [Description("Special Move (Block)")]
        SpecialMoveBlock = 6,

        [Description("Special Move (Save)")]
        SpecialMoveSave = 7,

        [Description("Special Move (Dribble)")]
        SpecialMoveDribble = 8,

        [Description("Special Move (Skill)")]
        SpecialMoveSkill = 9,

        [Description("Special Move (Shot)")]
        SpecialMoveShot = 10,

        [Description("Healing Item (FP)")]
        HealingItemGP = 11,

        [Description("Healing Item (TP)")]
        HealingItemTP = 12,

        [Description("")]
        HealingItemFPTP = 13,

        [Description("Key Item")]
        KeyItem = 14,

        [Description("Versus Ticket")]
        VersusTicket = 15,

        [Description("Kit")]
        Kit = 16,

        [Description("Palpack")]
        Palpack = 17,

        [Description("Fighting Spirit")]
        FightingSpirit = 18,

        [Description("Formation (Match)")]
        FormationMatch = 19,

        [Description("Formation (Mini-Battle)")]
        FormationMiniBattle = 20,

        [Description("Coach")]
        Coach = 21,

        [Description("Goal Celebration")]
        GoalCelebration = 22,

        [Description("Tactic")]
        Tactic = 23,

        [Description("Emblem")]
        Emblem = 24,
    }
}
