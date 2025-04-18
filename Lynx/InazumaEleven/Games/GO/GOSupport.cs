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
            public int PlayerGroup { get; set; }
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
            public int CanBeSold { get; set; }
            public int CanBeBought { get; set; }
            public int SellingPrice { get; set; }
            public int PurchasePrice { get; set; }
            public int Unk3 { get; set; }
            public int FightingSpiritPoint { get; set; }
            public int Attack { get; set; }
            public int EvolutionStatGrow { get; set; }
            public int Position { get; set; }
            public int Unk4 { get; set; }
            public int Element { get; set; }
            public int Unk5 { get; set; }
            public int MaxQuantity { get; set; }
            public int SpecialMoveID { get; set; }
            public int SkillID { get; set; }
            public int EvolutionGrow { get; set; }
            public int ItemPosX { get; set; }
            public int ItemPosY { get; set; }
            public int FusionID { get; set; }
            public int Partner1FusionID { get; set; }
            public int Partner2FusionID { get; set; }
            public int Unk6 { get; set; }
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

            public NPCPreset()
            {

            }

            public NPCPreset(int nPCID, int index, int count)
            {
                NPCID = nPCID;
                Index = index;
                Count = count;
            }
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
            public int LookAtThePlayer { get; set; }
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

            public TalkInfo()
            {

            }

            public TalkInfo(int talkID, int talkOffset, int talkCount)
            {
                TalkID = talkID;
                TalkOffset = talkOffset;
                TalkCount = talkCount;
            }
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
            public int Unk3 { get; set; }
            public int ItemCategory { get; set; }
        }

        public class ItemConfigAvatar : IItemConfig
        {
            public int ItemID { get; set; }
            public int ItemNumber { get; set; }
            public int NameID { get; set; }
            public int FullNameID { get; set; }
            public int DescriptionID { get; set; }
            public int Unk5 { get; set; }
            public int ItemCategory { get; set; }

            // Méthode pour convertir explicitement en ItemConfig
            public ItemConfig ToItemConfig()
            {
                return new ItemConfig
                {
                    ItemID = this.ItemID,
                    NameID = this.NameID,
                    DescriptionID = this.DescriptionID,
                    ItemCategory = this.ItemCategory
                };
            }
        }

        public class ItemConfigDirector : IItemDirector
        {
            public int ItemID { get; set; }
            public int ItemNumber { get; set; }
            public int NameID { get; set; }
            public int DescriptionID { get; set; }
            public int Unk4 { get; set; }
            public int ItemCategory { get; set; }
            public int Unk6 { get; set; }
            public int Unk7 { get; set; }
            public int Unk8 { get; set; }
            public int PlayerGroupBuff1 { get; set; }
            public int PlayerGroupBuff2 { get; set; }
            public int PlayerGroupBuff3 { get; set; }
            public int PlayerGroupDebuff1 { get; set; }
            public int PlayerGroupDebuff2 { get; set; }
            public int PlayerGroupDebuff3 { get; set; }
            public int FPCompatible { get; set; }
            public int TPCompatible { get; set; }
            public int KickCompatible { get; set; }
            public int DribbleCompatible { get; set; }
            public int TechniqueCompatible { get; set; }
            public int BlockCompatible { get; set; }
            public int SpeedCompatible { get; set; }
            public int StaminaCompatible { get; set; }
            public int CatchCompatible { get; set; }
            public int LuckCompatible { get; set; }
            public int FPNotCompatible { get; set; }
            public int TPNotCompatible { get; set; }
            public int KickNotCompatible { get; set; }
            public int DribbleNotCompatible { get; set; }
            public int TechniqueNotCompatible { get; set; }
            public int BlockNotCompatible { get; set; }
            public int SpeedNotCompatible { get; set; }
            public int StaminaNotCompatible { get; set; }
            public int CatchNotCompatible { get; set; }
            public int LuckNotCompatible { get; set; }
            public int ItemPositionX { get; set; }
            public int ItemPositionY { get; set; }
            public int Unk37 { get; set; }
        }

        public class ItemConfigUniform : IItemConfig
        {
            public int ItemID { get; set; }
            public int NameID { get; set; }
            public int DescriptionID { get; set; }
            public int ItemNumber { get; set; }
            public int Unk4 { get; set; }
            public int ItemCategory { get; set; }

            // Méthode pour convertir explicitement en ItemConfig
            public ItemConfig ToItemConfig()
            {
                return new ItemConfig
                {
                    ItemID = this.ItemID,
                    NameID = this.NameID,
                    DescriptionID = this.DescriptionID,
                    ItemCategory = this.ItemCategory
                };
            }
        }

        public class AvatarTimeGrowth : IAvatarTimeGrowth
        {
            public int Level1 { get; set; }
            public int Level2 { get; set; }
            public int Level3 { get; set; }
            public int Level4 { get; set; }
            public int Level5 { get; set; }
            public int Level6 { get; set; }
            public int Level7 { get; set; }
            public int Level8 { get; set; }
            public int Level9 { get; set; }
            public int Level10 { get; set; }
        }

        public class SkillUseTimeGrowth : ISkillUseTimeGrowth
        {
            public int Level0 { get; set; }
            public int Level1 { get; set; }
            public int Level2 { get; set; }
            public int Level3 { get; set; }
            public int Level4 { get; set; }
        }

        public class RouteConfig : IRouteConfig
        {
            public int Flag { get; set; }
            public int CellType { get; set; }
            public int ContentID { get; set; }
            public int MatchRestriction { get; set; }
            public int CellNum { get; set; }
            public int CellLink1 { get; set; }
            public int CellLink2 { get; set; }
            public int CellLink3 { get; set; }
            public string PhaseAppear { get; set; }
            public string Map { get; set; }
            public int Unk10 { get; set; }
            public string MatchTextLock { get; set; }
        }

        public class SoccerInfo : ISoccerInfo
        {
            public int SoccerID { get; set; }
            public int TeamParamID { get; set; }
            public int SoccerMode { get; set; }
            public int Unk3 { get; set; }
            public int Unk4 { get; set; }
            public string Sound { get; set; }
            public int VictoryCondition { get; set; }
            public int Script { get; set; }
            public int Time { get; set; }
            public int NextScript { get; set; }
        }

        public class TeamParamInfo : ITeamParamInfo
        {
            public int TeamParamID { get; set; }
            public int TeamConfigID { get; set; }
            public int Friendship { get; set; }
            public int Prestige { get; set; }
            public int VictoryPoints { get; set; }
            public int BootsID { get; set; }
            public int GlovesID { get; set; }
            public int BraceletID { get; set; }
            public int PendantID { get; set; }
            public int DropID1 { get; set; }
            public int DropID2 { get; set; }
            public int DropID3 { get; set; }
            public int DropID4 { get; set; }
            public int DropID5 { get; set; }
            public int Uniform { get; set; }
            public int DropRate1 { get; set; }
            public int DropRate2 { get; set; }
            public int DropRate3 { get; set; }
            public int DropRate4 { get; set; }
            public int DropRate5 { get; set; }
            public int FormationID { get; set; }
            public int Level { get; set; }
            public int DropID6 { get; set; }
            public int DropRate6 { get; set; }
            public int NicePlayBonus { get; set; }
            public int CoachID { get; set; }
            public int TacticID { get; set; }
            public int AILevel { get; set; }
        }

        public class StoryTeamInfo : IStoryTeamInfo
        {
            public int TeamConfigID { get; set; }
            public int NameID { get; set; }
            public int Player1 { get; set; }
            public int NumberPlayer1 { get; set; }
            public int Player2 { get; set; }
            public int NumberPlayer2 { get; set; }
            public int Player3 { get; set; }
            public int NumberPlayer3 { get; set; }
            public int Player4 { get; set; }
            public int NumberPlayer4 { get; set; }
            public int Player5 { get; set; }
            public int NumberPlayer5 { get; set; }
            public int Player6 { get; set; }
            public int NumberPlayer6 { get; set; }
            public int Player7 { get; set; }
            public int NumberPlayer7 { get; set; }
            public int Player8 { get; set; }
            public int NumberPlayer8 { get; set; }
            public int Player9 { get; set; }
            public int NumberPlayer9 { get; set; }
            public int Player10 { get; set; }
            public int NumberPlayer10 { get; set; }
            public int Player11 { get; set; }
            public int NumberPlayer11 { get; set; }
            public int Player12 { get; set; }
            public int NumberPlayer12 { get; set; }
            public int Player13 { get; set; }
            public int NumberPlayer13 { get; set; }
            public int Player14 { get; set; }
            public int NumberPlayer14 { get; set; }
            public int Player15 { get; set; }
            public int NumberPlayer15 { get; set; }
            public int Player16 { get; set; }
            public int NumberPlayer16 { get; set; }
            public int Emblem1 { get; set; }
            public int Emblem2 { get; set; }
            public int Emblem3 { get; set; }
            public int DifferenceLevelPlayer1 { get; set; }
            public int DifferenceLevelPlayer2 { get; set; }
            public int DifferenceLevelPlayer3 { get; set; }
            public int DifferenceLevelPlayer4 { get; set; }
            public int DifferenceLevelPlayer5 { get; set; }
            public int DifferenceLevelPlayer6 { get; set; }
            public int DifferenceLevelPlayer7 { get; set; }
            public int DifferenceLevelPlayer8 { get; set; }
            public int DifferenceLevelPlayer9 { get; set; }
            public int DifferenceLevelPlayer10 { get; set; }
            public int DifferenceLevelPlayer11 { get; set; }
            public int DifferenceLevelPlayer12 { get; set; }
            public int DifferenceLevelPlayer13 { get; set; }
            public int DifferenceLevelPlayer14 { get; set; }
            public int DifferenceLevelPlayer15 { get; set; }
            public int DifferenceLevelPlayer16 { get; set; }
        }

        public class EncountTeamInfo : IEncountTeamInfo
        {
            public int TeamConfigID { get; set; }
            public int NameID { get; set; }
            public int Player1 { get; set; }
            public int NumberPlayer1 { get; set; }
            public int Player2 { get; set; }
            public int NumberPlayer2 { get; set; }
            public int Player3 { get; set; }
            public int NumberPlayer3 { get; set; }
            public int Player4 { get; set; }
            public int NumberPlayer4 { get; set; }
            public int Player5 { get; set; }
            public int NumberPlayer5 { get; set; }
            public int Emblem1 { get; set; }
            public int Emblem2 { get; set; }
            public int Emblem3 { get; set; }
            public int DifferenceLevelPlayer1 { get; set; }
            public int DifferenceLevelPlayer2 { get; set; }
            public int DifferenceLevelPlayer3 { get; set; }
            public int DifferenceLevelPlayer4 { get; set; }
            public int DifferenceLevelPlayer5 { get; set; }
        }
    }
}
