using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Level5.Archive.ARC0;
using Lynx.Tools;
using Lynx.Level5.Binary;
using Lynx.InazumaEleven.Logic;
using Lynx.Level5.Text;
using Lynx.Level5.Text.Logic;
using Lynx.InazumaEleven.Games;
using Lynx.Level5.Binary.Logic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using DocumentFormat.OpenXml.Spreadsheet;
using static Lynx.InazumaEleven.Games.GO.GOSupport;
using static Microsoft.IO.RecyclableMemoryStreamManager;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Lynx.InazumaEleven.Games.GO
{
    public class GO : IGame
    {
        public string Name => "Inazuma Eleven Go";

        public ARC0 Game { get; set; }

        public string LanguageCode { get; set; }

        public Dictionary<string, GameSupports.GameFile> Files { get; set; }

        private string FileName { get; set; }

        public GO(string filePath, string languageName)
        {
            FileName = filePath;
            Game = new ARC0(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            LanguageCode = GOSupport.AvailableLanguages[languageName];
            GetGameFiles();
        }

        private void GetGameFiles()
        {
            Files = new Dictionary<string, GameSupports.GameFile>
            {
                 { "encount_area_text", new GameSupports.GameFile(Game, "/data/res/text/encount_area_text_" + LanguageCode + ".cfg.bin") },
                { "system_text", new GameSupports.GameFile(Game, "/data/res/text/system_text_" + LanguageCode + ".cfg.bin") },
                { "chara_text", new GameSupports.GameFile(Game, "/data/res/text/chara_text_" + LanguageCode + ".cfg.bin") },
                { "item_text", new GameSupports.GameFile(Game, "/data/res/text/item_text_" + LanguageCode + ".cfg.bin") },
                { "skill_text", new GameSupports.GameFile(Game, "/data/res/text/skill_text_" + LanguageCode + ".cfg.bin") },
                { "kiznax_hint_text", new GameSupports.GameFile(Game, "/data/res/text/kiznax_hint_text_" + LanguageCode + ".cfg.bin") },
                { "team_text", new GameSupports.GameFile(Game, "/data/res/text/team_text_" + LanguageCode + ".cfg.bin") },
                { "troute_text", new GameSupports.GameFile(Game, "/data/res/text/troute_text_" + LanguageCode + ".cfg.bin") },
                { "face", new GameSupports.GameFile(Game, "/data/bustup/face") },
                { "faceAvatar", new GameSupports.GameFile(Game, "/data/bustup/avatar") },
                { "modelRpgPlayer", new GameSupports.GameFile(Game, "/data/chr/model/rpg/face") },
                { "modelWazaPlayer", new GameSupports.GameFile(Game, "/data/chr/model/waza/face") },
                { "modelRpgNPC", new GameSupports.GameFile(Game, "/data/chr/model/rpg/npc") },
                { "modelWazaNPC", new GameSupports.GameFile(Game, "/data/chr/model/waza/npc") },
                { "map", new GameSupports.GameFile(Game, "/data/map/") },
                { "eventScript", new GameSupports.GameFile(Game, "/data/script/event") },
                { "script", new GameSupports.GameFile(Game, "/data/script/") },
                { "shop", new GameSupports.GameFile(Game, "/data/res/shop") },
                { "modelRPGShoes", new GameSupports.GameFile(Game, "/data/chr/model/rpg/shoes") },
                { "modelRPGBody", new GameSupports.GameFile(Game, "/data/chr/model/rpg/body") },
                { "modelRPGGloves", new GameSupports.GameFile(Game, "/data/chr/model/rpg/glove") },
                { "soccer", new GameSupports.GameFile(Game, "/data/res/soccer/") },
                { "emblem", new GameSupports.GameFile(Game, "/data/emblem/") },
            };
        }

        public T GetEmptyObject<T>() where T : class
        {
            switch (typeof(T))
            {
                case System.Type t when t == typeof(ISkillTable):
                    return new GOSupport.SkillTable() as T;
                case System.Type t when t == typeof(ICharaparam):
                    return new GOSupport.CharaParam() as T;
                case System.Type t when t == typeof(IShopConfig):
                    return new GOSupport.ShopConfig() as T;
                case System.Type t when t == typeof(ICommunityInfo):
                    return new GOSupport.CommunityInfo() as T;
                case System.Type t when t == typeof(ISkillConfig):
                    return new GOSupport.SkillConfig() as T;
                case System.Type t when t == typeof(IRouteConfig):
                    return new GOSupport.RouteConfig() as T;
                case System.Type t when t == typeof(IEncountTeamInfo):
                    return new GOSupport.EncountTeamInfo() as T;
                case System.Type t when t == typeof(IStoryTeamInfo):
                    return new GOSupport.StoryTeamInfo() as T;
                default:
                    return null;
            }
        }

        public ICharabase[] GetCharabase()
        {
            CfgBin charaBaseFile = new CfgBin();
            charaBaseFile.Open(Game.Directory.GetFileFromFullPath("/data/res/character/chara_base.cfg.bin"));

            return charaBaseFile.Entries
                .Where(x => x.GetName() == "CHARA_BASE_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.CharaBase>())
                .ToArray();
        }

        public void SaveCharaBase(ICharabase[] charabases)
        {
            CfgBin charaBaseFile = new CfgBin();
            charaBaseFile.Open(Game.Directory.GetFileFromFullPath("/data/res/character/chara_base.cfg.bin"));

            Entry baseBegin = charaBaseFile.Entries.Where(x => x.GetName() == "CHARA_BASE_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = charabases.Length;

            for (int i = 0; i < charabases.Count(); i++)
            {
                Entry newBaseEntry = new Entry("CHARA_BASE_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass<GOSupport.CharaBase>(charabases[i] as GOSupport.CharaBase);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/character").Files["chara_base.cfg.bin"].ByteContent = charaBaseFile.Save();
        }

        public ICharaparam[] GetCharaparams()
        {
            CfgBin charaBaseFile = new CfgBin();
            charaBaseFile.Open(Game.Directory.GetFileFromFullPath("/data/res/character/chara_param.cfg.bin"));

            return charaBaseFile.Entries
                .Where(x => x.GetName() == "CHARA_PARAM_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.CharaParam>())
                .ToArray();
        }

        public void SaveCharaparams(ICharaparam[] charaparams)
        {
            CfgBin charaparamFile = new CfgBin();
            charaparamFile.Open(Game.Directory.GetFileFromFullPath("/data/res/character/chara_param.cfg.bin"));

            Entry baseBegin = charaparamFile.Entries.Where(x => x.GetName() == "CHARA_PARAM_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = charaparams.Length;

            for (int i = 0; i < charaparams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("CHARA_PARAM_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass<GOSupport.CharaParam>(charaparams[i] as GOSupport.CharaParam);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/character").Files["chara_param.cfg.bin"].ByteContent = charaparamFile.Save();
        }

        public ITrainingUD[] GetTrainingUDs()
        {
            CfgBin charaparamFile = new CfgBin();
            charaparamFile.Open(Game.Directory.GetFileFromFullPath("/data/res/character/chara_param.cfg.bin"));

            return charaparamFile.Entries
                .Where(x => x.GetName() == "TRAINING_UD_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.TrainingUD>())
                .ToArray();
        }

        public void SaveTrainingUD(ITrainingUD[] trainingUDs)
        {
            CfgBin charaparamFile = new CfgBin();
            charaparamFile.Open(Game.Directory.GetFileFromFullPath("/data/res/character/chara_param.cfg.bin"));

            Entry baseBegin = charaparamFile.Entries.Where(x => x.GetName() == "TRAINING_UD_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            for (int i = 0; i < trainingUDs.Count(); i++)
            {
                Entry newBaseEntry = new Entry("TRAINING_UD_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(trainingUDs[i] as GOSupport.TrainingUD);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/character").Files["chara_param.cfg.bin"].ByteContent = charaparamFile.Save();
        }

        public IAvatar[] GetAvatars(bool emptyAvatar)
        {
            CfgBin itemconfigFile = new CfgBin();
            itemconfigFile.Open(Game.Directory.GetFileFromFullPath("/data/res/item/item_config.cfg.bin"));

            List<GOSupport.Avatar> avatars = itemconfigFile.Entries
                .Where(x => x.GetName() == "ITEM_AVATAR_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.Avatar>())
                .ToList();

            if (emptyAvatar)
            {
                avatars.Add(new GOSupport.Avatar());
            }

            return avatars.ToArray();
        }

        public void SaveAvatars(IAvatar[] avatars)
        {
            CfgBin itemconfigFile = new CfgBin();
            itemconfigFile.Open(Game.Directory.GetFileFromFullPath("/data/res/item/item_config.cfg.bin"));

            Entry baseBegin = itemconfigFile.Entries.Where(x => x.GetName() == "ITEM_AVATAR_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = avatars.Length;

            for (int i = 0; i < avatars.Count(); i++)
            {
                Entry newBaseEntry = new Entry("ITEM_AVATAR_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(avatars[i] as GOSupport.Avatar);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/item").Files["item_config.cfg.bin"].ByteContent = itemconfigFile.Save();
        }

        public IAvatarTimeGrowth[] GetAvatarGrowthTable()
        {
            CfgBin itemconfigFile = new CfgBin();
            itemconfigFile.Open(Game.Directory.GetFileFromFullPath("/data/res/item/item_config.cfg.bin"));

            return itemconfigFile.Entries
                .Where(x => x.GetName() == "AVATAR_INDEX_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.AvatarTimeGrowth>())
                .ToArray();
        }

        public void SaveAvatarGrowthTable(IAvatarTimeGrowth[] avatars)
        {
            CfgBin itemconfigFile = new CfgBin();
            itemconfigFile.Open(Game.Directory.GetFileFromFullPath("/data/res/character/item_config.cfg.bin"));

            Entry baseBegin = itemconfigFile.Entries.Where(x => x.GetName() == "AVATAR_INDEX_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = avatars.Length;

            for (int i = 0; i < avatars.Count(); i++)
            {
                Entry newBaseEntry = new Entry("AVATAR_INDEX_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass<GOSupport.AvatarTimeGrowth>(avatars[i] as GOSupport.AvatarTimeGrowth);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/item").Files["item_config.cfg.bin"].ByteContent = itemconfigFile.Save();
        }

        public ISkillConfig[] GetSkillConfigs(bool emptySkillConfig)
        {
            CfgBin skillconfigFile = new CfgBin();
            skillconfigFile.Open(Game.Directory.GetFileFromFullPath("/data/res/skill/skill_config.cfg.bin"));

            List<GOSupport.SkillConfig> skills = skillconfigFile.Entries
                .Where(x => x.GetName() == "SKILL_CONFIG_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.SkillConfig>())
                .ToList();

            if (emptySkillConfig)
            {
                skills.Add(new GOSupport.SkillConfig());
            }

            return skills.ToArray();
        }

        public void SaveSkillConfigs(ISkillConfig[] skills)
        {
            CfgBin skillconfigFile = new CfgBin();
            skillconfigFile.Open(Game.Directory.GetFileFromFullPath("/data/res/skill/skill_config.cfg.bin"));

            Entry baseBegin = skillconfigFile.Entries.Where(x => x.GetName() == "SKILL_CONFIG_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = skills.Length;

            for (int i = 0; i < skills.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SKILL_CONFIG_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(skills[i] as GOSupport.SkillConfig);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/skill").Files["skill_config.cfg.bin"].ByteContent = skillconfigFile.Save();
        }

        public (string, byte[]) ExportSkillConfigs(ISkillConfig[] skills)
        {
            CfgBin skillconfigFile = new CfgBin();
            skillconfigFile.Open(Game.Directory.GetFileFromFullPath("/data/res/skill/skill_config.cfg.bin"));

            Entry baseBegin = skillconfigFile.Entries.Where(x => x.GetName() == "SKILL_CONFIG_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = skills.Length;

            for (int i = 0; i < skills.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SKILL_CONFIG_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(skills[i] as GOSupport.SkillConfig);
                baseBegin.Children.Add(newBaseEntry);
            }

            return ("skill_config.cfg.bin", skillconfigFile.Save());
        }

        public ISkillTable[] GetSkillTable()
        {
            CfgBin skilltableFile = new CfgBin();
            skilltableFile.Open(Game.Directory.GetFileFromFullPath("/data/res/character/skill_table.cfg.bin"));

            return skilltableFile.Entries
                .Where(x => x.GetName() == "SKILL_TABLE_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.SkillTable>())
                .ToArray();
        }

        public void SaveSkillTable(ISkillTable[] skills)
        {
            CfgBin skilltableFile = new CfgBin();
            skilltableFile.Open(Game.Directory.GetFileFromFullPath("/data/res/character/skill_table.cfg.bin"));

            Entry baseBegin = skilltableFile.Entries.Where(x => x.GetName() == "SKILL_TABLE_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = skills.Length;

            for (int i = 0; i < skills.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SKILL_TABLE_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(skills[i] as GOSupport.SkillTable);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/character").Files["skill_table.cfg.bin"].ByteContent = skilltableFile.Save();
        }

        public Dictionary<INPCBase, List<INPCAppear>> GetNPCs(string mapID)
        {
            CfgBin npcFile = new CfgBin();

            if (Game.Directory.IsFullPathExists($"/data/map/{mapID}/{mapID}.npc.bin"))
            {
                npcFile.Open(Game.Directory.GetFileFromFullPath($"/data/map/{mapID}/{mapID}.npc.bin"));

                INPCBase[] npcBases = npcFile.Entries
                    .Where(x => x.GetName() == "NPC_BASE_BEGIN")
                    .SelectMany(x => x.Children)
                    .Select(x => x.ToClass<GOSupport.NPCBase>())
                    .ToArray();

                INPCPreset[] npcPresets = npcFile.Entries
                    .Where(x => x.GetName() == "NPC_PRESET_BEGIN")
                    .SelectMany(x => x.Children)
                    .Select(x => x.ToClass<GOSupport.NPCPreset>())
                    .ToArray();

                INPCAppear[] npcAppears = npcFile.Entries
                    .Where(x => x.GetName() == "NPC_APPEAR_BEGIN")
                    .SelectMany(x => x.Children)
                    .Select(x => x.ToClass<GOSupport.NPCAppear>())
                    .ToArray();

                return npcBases.ToDictionary(
                    npcBase => npcBase,
                    npcBase =>
                    {
                        var npcPreset = npcPresets.FirstOrDefault(x => x.NPCID == npcBase.NPCID);
                        if (npcPreset != null)
                        {
                            return npcAppears
                                .Skip(npcPreset.Index)
                                .Take(npcPreset.Count)
                                .ToList();
                        }
                        return new List<INPCAppear>();
                    }
                );
            } else
            {
                return new Dictionary<INPCBase, List<INPCAppear>>();
            }
        }

        public void SaveNPCs(Dictionary<INPCBase, List<INPCAppear>> npcs, string mapID)
        {
            string folderPath = $"/data/map/{mapID}";
            string fileName = $"{mapID}.npc.bin";
            string filePath = $"{folderPath}/{fileName}";

            CfgBin npcFile = new CfgBin();

            // Set encoding
            Encoding shiftJIS = Encoding.GetEncoding("SHIFT-JIS");
            npcFile.Encoding = shiftJIS;

            // Open the npc file if it exist
            if (Game.Directory.IsFullPathExists(filePath))
            {
                npcFile.Open(Game.Directory.GetFileFromFullPath(filePath));
            }

            // Get NPCBase
            Entry npcBaseBegin = npcFile.Entries.Where(x => x.GetName() == "NPC_BASE_BEGIN").FirstOrDefault();
            if (npcBaseBegin != null)
            {
                // resets items if it already exists
                npcBaseBegin.Children.Clear();
                npcBaseBegin.Variables[0].Value = npcs.Count;
            }
            else
            {
                // adds the entry to the npc file if it doesn't exists
                npcBaseBegin = new Entry("NPC_BASE_BEGIN_0", new List<Variable>() { new Variable(Level5.Binary.Logic.Type.Int, npcs.Count) }, Encoding.UTF8, true);
                npcFile.Entries.Add(npcBaseBegin);
            }

            // Get NPCPreset
            Entry npcPresetBegin = npcFile.Entries.Where(x => x.GetName() == "NPC_PRESET_BEGIN").FirstOrDefault();
            if (npcPresetBegin != null)
            {
                // resets items if it already exists
                npcPresetBegin.Children.Clear();
                npcPresetBegin.Variables[0].Value = npcs.Count;
            }
            else
            {
                // adds the entry to the npc file if it doesn't exists
                npcPresetBegin = new Entry("NPC_PRESET_BEGIN_0", new List<Variable>() { new Variable(Level5.Binary.Logic.Type.Int, npcs.Count) }, Encoding.UTF8, true);
                npcFile.Entries.Add(npcPresetBegin);
            }

            // Get NPCAppear
            Entry npcAppearBegin = npcFile.Entries.Where(x => x.GetName() == "NPC_APPEAR_BEGIN").FirstOrDefault();
            if (npcAppearBegin != null)
            {
                // resets items if it already exists
                npcAppearBegin.Children.Clear();
                npcAppearBegin.Variables[0].Value = npcs.Values.Sum(list => list.Count);
            }
            else
            {
                // adds the entry to the npc file if it doesn't exists
                npcAppearBegin = new Entry("NPC_APPEAR_BEGIN_0", new List<Variable>() { new Variable(Level5.Binary.Logic.Type.Int, npcs.Values.Sum(list => list.Count)) }, Encoding.UTF8, true);
                npcFile.Entries.Add(npcAppearBegin);
            }

            int npcIndex = 0;
            int npcCount = 0;

            // Loop on each npc data
            foreach (KeyValuePair<INPCBase, List<INPCAppear>> npc in npcs)
            {
                // Add new item on NPCBase entry
                Entry newNPCBaseEntry = new Entry("NPC_BASE_" + npcIndex, new List<Variable>(), shiftJIS);
                newNPCBaseEntry.SetVariablesFromClass(npc.Key as GOSupport.NPCBase);
                npcBaseBegin.Children.Add(newNPCBaseEntry);

                // Add an NPCPreset based on NPCID and NPCAppear
                NPCPreset newNPCPreset = new NPCPreset(npc.Key.NPCID, npcCount, npc.Value.Count);
                Entry newNPresetEntry = new Entry("NPC_PRESET_" + npcIndex, new List<Variable>(), shiftJIS);
                newNPresetEntry.SetVariablesFromClass(newNPCPreset as GOSupport.NPCPreset);
                npcPresetBegin.Children.Add(newNPresetEntry);

                // Add all NPCAppear items linked to this NPCBase
                for (int i = 0; i < npc.Value.Count; i++)
                {
                    Entry newNPCAppearEntry = new Entry("NPC_APPEAR_" + npcCount, new List<Variable>(), shiftJIS);
                    newNPCAppearEntry.SetVariablesFromClass(npc.Value[i] as GOSupport.NPCAppear);
                    npcAppearBegin.Children.Add(newNPCAppearEntry);
                    npcCount++;
                }

                npcIndex++;
            }

            // Save the file
            Game.Directory.GetFolderFromFullPath(folderPath).Files[fileName].ByteContent = npcFile.SaveWithStrings();
        }

        public Dictionary<ITalkInfo, List<ITalkConfig>> GetEvents(string mapID)
        {
            CfgBin npcFile = new CfgBin();

            if (Game.Directory.IsFullPathExists($"/data/map/{mapID}/{mapID}.talk.bin"))
            {
                npcFile.Open(Game.Directory.GetFileFromFullPath($"/data/map/{mapID}/{mapID}.talk.bin"));

                ITalkInfo[] talkInfos = npcFile.Entries
                    .Where(x => x.GetName() == "TALK_INFO_BEGIN")
                    .SelectMany(x => x.Children)
                    .Select(x => x.ToClass<GOSupport.TalkInfo>())
                    .ToArray();

                ITalkConfig[] talkConfigs = npcFile.Entries
                    .Where(x => x.GetName() == "TALK_CONFIG_BEGIN")
                    .SelectMany(x => x.Children)
                    .Select(x => x.ToClass<GOSupport.TalkConfig>())
                    .ToArray();

                return talkInfos.ToDictionary(
                    talkInfo => talkInfo,
                    talkInfo => talkConfigs.Skip(talkInfo.TalkOffset).Take(talkInfo.TalkCount).ToList());
            } else
            {
                return new Dictionary<ITalkInfo, List<ITalkConfig>>();
            }
        }

        public void SaveEvents(Dictionary<ITalkInfo, List<ITalkConfig>> events, string mapID)
        {
            string folderPath = $"/data/map/{mapID}";
            string fileName = $"{mapID}.talk.bin";
            string filePath = $"{folderPath}/{fileName}";

            CfgBin npcFile = new CfgBin();

            // Set encoding
            Encoding shiftJIS = Encoding.GetEncoding("SHIFT-JIS");
            npcFile.Encoding = shiftJIS;

            // Open the npc file if it exist
            if (Game.Directory.IsFullPathExists(filePath))
            {
                npcFile.Open(Game.Directory.GetFileFromFullPath(filePath));
            }

            // Get TalkInfo
            Entry talkInfoBegin = npcFile.Entries.Where(x => x.GetName() == "TALK_INFO_BEGIN").FirstOrDefault();
            if (talkInfoBegin != null)
            {
                // resets items if it already exists
                talkInfoBegin.Children.Clear();
                talkInfoBegin.Variables[0].Value = events.Count;
            }
            else
            {
                // adds the entry to the npc file if it doesn't exists
                talkInfoBegin = new Entry("TALK_INFO_BEGIN_0", new List<Variable>() { new Variable(Level5.Binary.Logic.Type.Int, events.Count) }, Encoding.UTF8, true);
                npcFile.Entries.Add(talkInfoBegin);
            }

            // Get TalkConfig
            Entry talkConfigBegin = npcFile.Entries.Where(x => x.GetName() == "TALK_CONFIG_BEGIN").FirstOrDefault();
            if (talkConfigBegin != null)
            {
                // resets items if it already exists
                talkConfigBegin.Children.Clear();
                talkConfigBegin.Variables[0].Value = events.Values.Sum(list => list.Count);
            }
            else
            {
                // adds the entry to the npc file if it doesn't exists
                talkConfigBegin = new Entry("TALK_CONFIG_BEGIN_0", new List<Variable>() { new Variable(Level5.Binary.Logic.Type.Int, events.Values.Sum(list => list.Count)) }, Encoding.UTF8, true);
                npcFile.Entries.Add(talkConfigBegin);
            }

            int eventIndex = 0;
            int eventCount = 0;

            // Loop on each npc data
            foreach (KeyValuePair<ITalkInfo, List<ITalkConfig>> myEvent in events)
            {
                // Add a TalkInfo based on TalkConfig
                TalkInfo newTalkInfo = new TalkInfo(myEvent.Key.TalkID, eventCount, myEvent.Value.Count);
                Entry newTalkInfoEntry = new Entry("TALK_INFO_" + eventIndex, new List<Variable>(), shiftJIS);
                newTalkInfoEntry.SetVariablesFromClass(newTalkInfo as GOSupport.TalkInfo);
                talkInfoBegin.Children.Add(newTalkInfoEntry);

                // Add all TalkConfig items linked to this TalkInfo
                for (int i = 0; i < myEvent.Value.Count; i++)
                {
                    Entry newNPCTalkConfigEntry = new Entry("TALK_CONFIG_" + eventCount, new List<Variable>(), shiftJIS);
                    newNPCTalkConfigEntry.SetVariablesFromClass(myEvent.Value[i] as GOSupport.TalkConfig);
                    talkConfigBegin.Children.Add(newNPCTalkConfigEntry);
                    eventCount++;
                }

                eventIndex++;
            }

            // Save the file
            Game.Directory.GetFolderFromFullPath(folderPath).Files[fileName].ByteContent = npcFile.SaveWithStrings();
        }

        public void SaveMapText(T2bþ fileData, Dictionary<ITalkInfo, List<ITalkConfig>> events, string mapID)
        {
            List<ITalkConfig> allTalkConfigs = events.Values.SelectMany(configList => configList).ToList();

            for (int i = 0; i < fileData.Texts.Count; i++)
            {
                int textID = fileData.Texts.ElementAt(i).Key;

                // Remove unused key
                if (!allTalkConfigs.Any(x => x.TalkValue == textID))
                {
                    fileData.Texts.Remove(textID);
                }
            }

            string folderPath = $"/data/map/{mapID}";
            string fileName = $"{mapID}_{LanguageCode}.cfg.bin";
            string filePath = $"{folderPath}/{fileName}";

            // Save the file
            Game.Directory.GetFolderFromFullPath(folderPath).Files[fileName].ByteContent = fileData.Save(true);
        }

        public IShopConfig[] GetShop(string shopID)
        {
            CfgBin shopFile = new CfgBin();

            shopFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/shop/shop_{shopID}.cfg.bin"));

            return shopFile.Entries
                .Where(x => x.GetName() == "SHOP_CONFIG_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.ShopConfig>())
                .ToArray();
        }

        public void SaveShop(string shopID, IShopConfig[] shop)
        {
            CfgBin shopFile = new CfgBin();
            shopFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/shop/shop_{shopID}.cfg.bin"));

            Entry baseBegin = shopFile.Entries.Where(x => x.GetName() == "SHOP_CONFIG_INFO_BEGIN").FirstOrDefault();

            if (baseBegin != null)
            {
                baseBegin.Children.Clear();
            } else
            {
                baseBegin = new Entry("SHOP_CONFIG_INFO_BEGIN_0", new List<Variable>() { new Variable(Level5.Binary.Logic.Type.Int, shop.Length) }, Encoding.UTF8, true);
                shopFile.Entries.Add(baseBegin);
            }

            baseBegin.Variables[0].Value = shop.Length;

            for (int i = 0; i < shop.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SHOP_CONFIG_INFO_" + i, new List<Variable>(), Encoding.UTF8);

                if (shop[i].Condition.ToString() == "0" || shop[i].Condition.ToString() == "")
                {
                    shop[i].Condition = 0;
                }

                newBaseEntry.SetVariablesFromClass(shop[i] as GOSupport.ShopConfig);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/shop").Files[$"shop_{shopID}.cfg.bin"].ByteContent = shopFile.Save();
        }

        public ICommunityInfo[] GetCommunities()
        {
            CfgBin communityFile = new CfgBin();
            communityFile.Open(Game.Directory.GetFileFromFullPath("/data/res/shop/community_config.cfg.bin"));

            return communityFile.Entries
                .Where(x => x.GetName() == "COMMUNITY_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.CommunityInfo>())
                .ToArray();
        }

        public void SaveCommunities(ICommunityInfo[] communities)
        {
            CfgBin communityFile = new CfgBin();
            communityFile.Open(Game.Directory.GetFileFromFullPath("/data/res/shop/community_config.cfg.bin"));

            Entry baseBegin = communityFile.Entries.Where(x => x.GetName() == "COMMUNITY_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = communities.Length;

            for (int i = 0; i < communities.Count(); i++)
            {
                Entry newBaseEntry = new Entry("COMMUNITY_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(communities[i] as GOSupport.CommunityInfo);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/shop").Files["community_config.cfg.bin"].ByteContent = communityFile.Save();
        }

        public (IRouteConfig[], int) GetRoutes(string filename)
        {
            CfgBin routeFile = new CfgBin();
            routeFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/soccer/{filename}"));

            var routes = routeFile.Entries
                .Where(x => x.GetName() == "ROUTE_CONFIG_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.RouteConfig>())
                .ToArray();

            int nameID = Convert.ToInt32(routeFile.Entries[0].Variables[1].Value);

            return (routes, nameID);
        }

        public void SaveRoutes(string filename, int nameCRC32, IRouteConfig[] routes)
        {
            CfgBin routeFile = new CfgBin();

            // Set encoding
            Encoding shiftJIS = Encoding.GetEncoding("SHIFT-JIS");
            routeFile.Encoding = shiftJIS;

            // Open the route file if it exist
            if (Game.Directory.IsFullPathExists($"/data/res/soccer/{filename}"))
            {
                routeFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/soccer/{filename}"));
            } else
            {
                // Create the entry
                Entry communityHeader = new Entry("ROUTE_CONFIG_BEGIN_0", 
                    new List<Variable>() { 
                        new Variable(Level5.Binary.Logic.Type.Int, routes.Count()),
                        new Variable(Level5.Binary.Logic.Type.Int, nameCRC32)
                    }, shiftJIS, true);
                
                // Insert
                routeFile.Entries.Add(communityHeader);
            }
            
            Entry baseBegin = routeFile.Entries.Where(x => x.GetName() == "ROUTE_CONFIG_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = routes.Length;

            for (int i = 0; i < routes.Count(); i++)
            {
                Entry newBaseEntry = new Entry("ROUTE_CONFIG_" + i, new List<Variable>(), shiftJIS);
                newBaseEntry.SetVariablesFromClass(routes[i] as GOSupport.RouteConfig);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/soccer").Files[filename].ByteContent = routeFile.SaveWithStrings();
            // Console.WriteLine(BitConverter.ToString(Game.Directory.GetFolderFromFullPath("/data/res/soccer").Files[filename].ByteContent).Replace("-", ""));
        }

        public ISoccerInfo[] GetSoccers()
        {
            CfgBin soccerFile = new CfgBin();
            soccerFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/soccer/soccer_config.cfg.bin"));

            return soccerFile.Entries
                .Where(x => x.GetName() == "SOCCER_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.SoccerInfo>())
                .ToArray();
        }

        public void SaveSoccers(ISoccerInfo[] soccers)
        {
            CfgBin soccerFile = new CfgBin();
            soccerFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/soccer/soccer_config.cfg.bin"));

            Entry baseBegin = soccerFile.Entries.Where(x => x.GetName() == "SOCCER_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = soccers.Length;

            for (int i = 0; i < soccers.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SOCCER_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(soccers[i] as GOSupport.SoccerInfo);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/soccer").Files["soccer_config.cfg.bin"].ByteContent = soccerFile.Save();
        }

        public ITeamParamInfo[] GetTeamParams()
        {
            CfgBin teamParamFile = new CfgBin();
            teamParamFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/team/team_param.cfg.bin"));

            return teamParamFile.Entries
                .Where(x => x.GetName() == "TEAM_PARAM_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.TeamParamInfo>())
                .ToArray();
        }

        public void SaveTeamParams(ITeamParamInfo[] teams)
        {
            CfgBin teamParamFile = new CfgBin();
            teamParamFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/team/team_param.cfg.bin"));

            Entry baseBegin = teamParamFile.Entries.Where(x => x.GetName() == "TEAM_PARAM_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = teams.Length;

            for (int i = 0; i < teams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("TEAM_PARAM_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(teams[i] as GOSupport.TeamParamInfo);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/team").Files["team_param.cfg.bin"].ByteContent = teamParamFile.Save();
        }

        public object[] GetTeamConfig()
        {
            CfgBin teamConfigFile = new CfgBin();
            teamConfigFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/team/team_config.cfg.bin"));

            return teamConfigFile.Entries
                .Where(x => x.GetName() == "STORY_TEAM_INFO_BEGIN" || x.GetName() == "ENCOUNT_TEAM_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x =>
                {
                    if (x.Variables.Count == 53)
                        return x.ToClass<GOSupport.StoryTeamInfo>() as object;
                    else if (x.Variables.Count == 20)
                        return x.ToClass<GOSupport.EncountTeamInfo>() as object;
                    else
                        return null;
                })
                .Where(x => x != null)
                .ToArray();
        }

        public IStoryTeamInfo[] GetStoryTeams()
        {
            CfgBin teamConfigFile = new CfgBin();
            teamConfigFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/team/team_config.cfg.bin"));

            return teamConfigFile.Entries
                .Where(x => x.GetName() == "STORY_TEAM_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.StoryTeamInfo>())
                .ToArray();
        }

        public IEncountTeamInfo[] GetEncounterTeams()
        {
            CfgBin teamConfigFile = new CfgBin();
            teamConfigFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/team/team_config.cfg.bin"));

            return teamConfigFile.Entries
                .Where(x => x.GetName() == "ENCOUNT_TEAM_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => x.ToClass<GOSupport.EncountTeamInfo>())
                .ToArray();
        }

        public void SaveTeamConfig(IStoryTeamInfo[] storyTeams, IEncountTeamInfo[] encounterTeams)
        {
            CfgBin teamConfigFile = new CfgBin();
            teamConfigFile.Open(Game.Directory.GetFileFromFullPath($"/data/res/team/team_config.cfg.bin"));

            Entry storyBaseBegin = teamConfigFile.Entries.Where(x => x.GetName() == "STORY_TEAM_INFO_BEGIN").FirstOrDefault();
            storyBaseBegin.Children.Clear();

            storyBaseBegin.Variables[0].Value = storyTeams.Length;

            for (int i = 0; i < storyTeams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("STORY_TEAM_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(storyTeams[i] as GOSupport.StoryTeamInfo);
                storyBaseBegin.Children.Add(newBaseEntry);
            }

            Entry encountBaseBegin = teamConfigFile.Entries.Where(x => x.GetName() == "ENCOUNT_TEAM_INFO_BEGIN").FirstOrDefault();
            encountBaseBegin.Children.Clear();

            encountBaseBegin.Variables[0].Value = encounterTeams.Length;

            for (int i = 0; i < encounterTeams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("ENCOUNT_TEAM_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(encounterTeams[i] as GOSupport.EncountTeamInfo);
                encountBaseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/team").Files["team_config.cfg.bin"].ByteContent = teamConfigFile.Save();
        }

        public IItemConfig[] GetItems(string itemType)
        {
            CfgBin itemconfigFile = new CfgBin();
            itemconfigFile.Open(Game.Directory.GetFileFromFullPath("/data/res/item/item_config.cfg.bin"));

            switch (itemType)
            {
                case "equipment":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_EQUIPMENT_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfig>())
                        .ToArray();
                case "consumable":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_CONSUME_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfig>())
                        .ToArray();
                case "important":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_IMPORTANT_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfig>())
                        .ToArray();
                case "uniform":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_UNIFORM_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfigUniform>())
                        .ToArray();
                case "kizunax":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_KIZUNAX_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfig>())
                        .ToArray();
                case "avatar":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_AVATAR_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfigAvatar>())
                        .ToArray();
                case "director":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_DIRECTOR_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfigDirector>())
                        .ToArray();
                case "all":
                    string[] itemTypesAvatar = { "ITEM_AVATAR_BEGIN" };
                    string[] itemTypesUniform = { "ITEM_UNIFORM_BEGIN" };
                    string[] itemTypesDirector = { "ITEM_DIRECTOR_BEGIN" };
                    string[] itemTypesOther = { "ITEM_EQUIPMENT_BEGIN", "ITEM_CONSUME_BEGIN", "ITEM_IMPORTANT_BEGIN", "ITEM_KIZUNAX_BEGIN"};

                    var avatarItems = itemconfigFile.Entries
                        .Where(x => itemTypesAvatar.Contains(x.GetName()))
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfigAvatar>())
                        .ToList();

                    var directorItems = itemconfigFile.Entries
                        .Where(x => itemTypesDirector.Contains(x.GetName()))
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfigDirector>())
                        .ToList();

                    var uniformItems = itemconfigFile.Entries
                        .Where(x => itemTypesUniform.Contains(x.GetName()))
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfigUniform>())
                        .ToList();

                    var otherItems = itemconfigFile.Entries
                        .Where(x => itemTypesOther.Contains(x.GetName()))
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfig>())
                        .ToList();

                    otherItems.AddRange(avatarItems.Select(x => x.ToItemConfig()));
                    otherItems.AddRange(directorItems.Select(x => x.ToItemConfig()));
                    otherItems.AddRange(uniformItems.Select(x => x.ToItemConfig()));

                    return otherItems.ToArray();
                default:
                    return new GOSupport.ItemConfig[] { };
            }
        }

        public CfgBin GetMapenv(string mapID)
        {
            CfgBin npcFile = new CfgBin();

            if (Game.Directory.IsFullPathExists($"/data/map/{mapID}/{mapID}_mapenv.bin"))
            {
                npcFile.Open(Game.Directory.GetFileFromFullPath($"/data/map/{mapID}/{mapID}_mapenv.bin"));
                return npcFile;
            }
            else
            {
                return null;
            }
        }

        public T2bþ GetMapText(string mapID)
        {
            string filePath = $"/data/map/{mapID}/{mapID}_{LanguageCode}.cfg.bin";

            if (Game.Directory.IsFullPathExists(filePath))
            {
                return new T2bþ(Game.Directory.GetFileFromFullPath(filePath));
            }
            else
            {
                return null;
            }
        }

        public void SaveTextFile(GameSupports.GameFile fileName, T2bþ fileData)
        {
            VirtualDirectory directory = fileName.File.Directory.GetFolderFromFullPath(Path.GetDirectoryName(fileName.Path).Replace("\\", "/"));
            directory.Files[Path.GetFileName(fileName.Path)].ByteContent = fileData.Save(false);
        }

        public void Save()
        {
            string tempPath = @"./temp";

            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            // Save
            Game.Save(tempPath + Path.GetFileName(FileName));

            // Close File
            Game = (ARC0)Game.Close();

            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            File.Move(tempPath + Path.GetFileName(FileName), FileName);
            Game = new ARC0(new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            GetGameFiles();
        }
    }
}
