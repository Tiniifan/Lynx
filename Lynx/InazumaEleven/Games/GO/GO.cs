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
                { "face", new GameSupports.GameFile(Game, "/data/bustup/face") },
                { "modelRpgPlayer", new GameSupports.GameFile(Game, "/data/chr/model/rpg/face") },
                { "modelWazaPlayer", new GameSupports.GameFile(Game, "/data/chr/model/waza/face") },
                { "modelRpgNPC", new GameSupports.GameFile(Game, "/data/chr/model/rpg/npc") },
                { "modelWazaNPC", new GameSupports.GameFile(Game, "/data/chr/model/waza/npc") },
                { "map", new GameSupports.GameFile(Game, "/data/map/") },
                { "eventScript", new GameSupports.GameFile(Game, "/data/script/event") },
                { "script", new GameSupports.GameFile(Game, "/data/script/") },
                { "shop", new GameSupports.GameFile(Game, "/data/res/shop") },
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

            for (int i = 0; i < avatars.Count(); i++)
            {
                Entry newBaseEntry = new Entry("ITEM_AVATAR_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(avatars[i] as GOSupport.Avatar);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/item").Files["item_config.bin"].ByteContent = itemconfigFile.Save();
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

            for (int i = 0; i < skills.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SKILL_CONFIG_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(skills[i] as GOSupport.SkillConfig);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/skill").Files["skill_config.bin"].ByteContent = skillconfigFile.Save();
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
            baseBegin.Children.Clear();

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

            for (int i = 0; i < communities.Count(); i++)
            {
                Entry newBaseEntry = new Entry("COMMUNITY_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(communities[i] as GOSupport.CommunityInfo);
                baseBegin.Children.Add(newBaseEntry);
            }

            Game.Directory.GetFolderFromFullPath("/data/res/shop").Files["community_config.cfg.bin"].ByteContent = communityFile.Save();
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
                        .Select(x => x.ToClass<GOSupport.ItemConfig>())
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
                        .Select(x => x.ToClass<GOSupport.ItemConfig>())
                        .ToArray();
                case "director":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_DIRECTOR_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfig>())
                        .ToArray();
                case "all":
                    string[] itemTypes = { "ITEM_EQUIPMENT_BEGIN", "ITEM_CONSUME_BEGIN", "ITEM_IMPORTANT_BEGIN", "ITEM_UNIFORM_BEGIN", "ITEM_KIZUNAX_BEGIN", "ITEM_AVATAR_BEGIN", "ITEM_DIRECTOR_BEGIN" };
                    return itemconfigFile.Entries
                        .Where(x => itemTypes.Contains(x.GetName()))
                        .SelectMany(x => x.Children)
                        .Select(x => x.ToClass<GOSupport.ItemConfig>())
                        .ToArray();
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
