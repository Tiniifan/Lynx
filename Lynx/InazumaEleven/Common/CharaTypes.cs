using System;
using System.ComponentModel;

namespace Lynx.InazumaEleven.Common
{
    public enum CharaTypes
    {
        [Description("Unknown")]
        Unknown = 0,

        [Description("Player")]
        Player = 1,

        [Description("Unused")]
        Unused = 2,

        [Description("NPC")]
        NPC = 3,

        [Description("NPC Other")]
        NPCOther = 4,
    }
}
