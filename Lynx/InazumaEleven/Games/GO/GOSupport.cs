using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Lynx.InazumaEleven.Logic;

namespace Lynx.InazumaEleven.Games.GO
{
    public class GOSupport
    {
        public static Dictionary<string, string> AvailableLanguages = new Dictionary<string, string>()
        {
            { "Français", "fr"},
            { "English", "en"},
            { "Deutsch", "de"},
            { "Español", "es"},
            { "Italiano", "it"},
        };

        public class CharaBase : ICharabase
        {
            public int BaseHash { get; set; }
            public int ModelNumber { get; set; }
            public int NameHash { get; set; }
            public int NicknameHash { get; set; }
            public int CharaBaseType { get; set; }
            public int Unk1 { get; set; }
            public int Unk2 { get; set; }
            public int Body { get; set; }
            public int Skin { get; set; }
            public int Gender { get; set; }
            public int Year { get; set; }
            public int DescriptionHash { get; set; }
        }

        public class CharaParam : ICharaparam
        {
            public int ParamHash { get; set; }
            public int BaseHash { get; set; }
            public int FP { get; set; }
            public int TP { get; set; }
            public int Kick { get; set; }
            public int Dribble { get; set; }
            public int Technique { get; set; }
            public int Block { get; set; }
            public int Speed { get; set; }
            public int Stamina { get; set; }
            public int Catch { get; set; }
            public int Luck { get; set; }
            public int Element { get; set; }
            public int PlayerPosition { get; set; }
            public int DescriptionHash { get; set; }
            public int Freedom { get; set; }
            public int FightingSpiritHash { get; set; }
            public int SpecialMoveCount { get; set; }
            public int SpecialMoveOffset { get; set; }
            public int Unk1 { get; set; }
            public int ExperienceGrow { get; set; }
            public int StatVariance { get; set; }
            public int Unk2 { get; set; }
            public int FPGrow { get; set; }
            public int TPGrow { get; set; }
            public int KickGrow { get; set; }
            public int DribbleGrow { get; set; }
            public int TechniqueGrow { get; set; }
            public int BlockGrow { get; set; }
            public int SpeedGrow { get; set; }
            public int StaminaGrow { get; set; }
            public int CatchGrow { get; set; }
            public int LuckGrow { get; set; }
            public int Unk3 { get; set; }
            public int Unk4 { get; set; }
            public int Unk5 { get; set; }
            public int Unk6 { get; set; }
            public int Unk7 { get; set; }
            public int Unk8 { get; set; }
            public int TrainingUD { get; set; }
            public int Unk9 { get; set; }
            public int FightingSpiritMatchkHash { get; set; }
        }

        public class SkillTable : ISkillTable
        {
            public int SkillIndex { get; set; }
            public int LevelLearned { get; set; }
            public int SkillHash { get; set; }
            public int Level { get; set; }
        }

        public class SkillConfig : ISkillConfig
        {
            public int SkillHash { get; set; }
            public int NameHash { get; set; }
            public int DescriptionHash { get; set; }
            public int Element { get; set; }
            public int EvolutionType { get; set; }
            public int EvolutionGrow { get; set; }
            public int SkillPosition { get; set; }
            public int TPCost { get; set; }
            public int Number { get; set; }
            public int SkillType { get; set; }
            public int EffectType { get; set; }
            public int PartnerNumber { get; set; }
            public int Unk1 { get; set; }
            public int Unk2 { get; set; }
            public int Unk3 { get; set; }
            public int Fault { get; set; }
            public int Power { get; set; }
            public int Technique { get; set; }
            public int BoostActiveOn { get; set; }
            public int SkillBoost { get; set; }
            public int NameWazaID { get; set; }
        }

        public class Avatar : IAvatar
        {
            public int AvatarHash { get; set; }
            public int AvatarNumber { get; set; }
            public int NicknameHash { get; set; }
            public int FullNameHash { get; set; }
            public int DescriptionHash { get; set; }
            public int Unk1 { get; set; }
            public int Unk2 { get; set; }
            public int Unk3 { get; set; }
            public int Unk4 { get; set; }
            public int Unk5 { get; set; }
            public int Unk6 { get; set; }
            public int Unk7 { get; set; }
            public int FightingSpiritPoint { get; set; }
            public int Attack { get; set; }
            public int AttackUp { get; set; }
            public int Unk8 { get; set; }
            public int Unk9 { get; set; }
            public int Unk10 { get; set; }
            public int Unk11 { get; set; }
            public int Unk12 { get; set; }
            public int MoveHash { get; set; }
            public int SkillHash { get; set; }
            public int Unk13 { get; set; }
            public int Unk14 { get; set; }
            public int Unk15 { get; set; }
            public int Unk16 { get; set; }
            public int Unk17 { get; set; }
            public int Unk18 { get; set; }
            public int Unk19 { get; set; }
        }

        public class TrainingUD : ITrainingUD
        {
            public int KickDown { get; set; }
            public int DribbleDown { get; set; }
            public int TechniqueDown { get; set; }
            public int BlockDown { get; set; }
            public int SpeedDown { get; set; }
            public int StaminaDown { get; set; }
            public int CatchDown { get; set; }
            public int LuckDown { get; set; }
        }

        public class NPCBase : INPCBase
        {
            public int NPCID { get; set; }
            public int HeadID { get; set; }
            public int Type { get; set; }
            public int Unk1 { get; set; }
            public int UniformID { get; set; }
            public int BootsID { get; set; }
            public int GlovesID { get; set; }
            public int IconID { get; set; }
        }

        public class NPCPreset : INPCPreset
        {
            public int NPCID { get; set; }
            public int Index { get; set; }
            public int Count { get; set; }
        }

        public class NPCAppear : INPCAppear
        {
            public float LocationX { get; set; }
            public float LocationZ { get; set; }
            public float LocationY { get; set; }
            public int Unk1 { get; set; }
            public int Unk2 { get; set; }
            public float Rotation { get; set; }
            public string StandAnimation { get; set; }
            public int Unk3 { get; set; }
            public string TalkAnimation { get; set; }
            public string UnkAnimation { get; set; }
            public int Unk4 { get; set; }
            public string PhaseAppear { get; set; }
            public int Unk5 { get; set; }
        }

        public class TalkInfo : ITalkInfo
        {
            public int TalkID { get; set; }
            public int TalkOffset { get; set; }
            public int TalkCount { get; set; }
        }

        public class TalkConfig : ITalkConfig
        {
            public int TalkType { get; set; }
            public int TalkValue { get; set; }
            public string PhaseAppear { get; set; }

            public int Unk1 { get; set; }
        }

        public class ShopConfig : IShopConfig
        {
            public int ItemID { get; set; }
            public object Condition { get; set; }
        }

        public class CommunityInfo : ICommunityInfo
        {
            public int ShopID { get; set; }
            public int NameID { get; set; }
            public int AreaID { get; set; }
        }

        public class ItemConfig : IItemConfig
        {
            public int ItemID { get; set; }
            public int NameID { get; set; }
            public int DescriptionID { get; set; }
        }
    }
}
