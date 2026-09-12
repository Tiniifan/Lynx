using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioElevenLib.Level5.Archive.ARC0;
using StudioElevenLib.Tools;
using StudioElevenLib.Level5.Binary;
using Lynx.InazumaEleven.Logic;
using StudioElevenLib.Level5.Text;
using StudioElevenLib.Level5.Binary.Logic;
using StudioElevenLib.Level5.Binary.Mapper;
using StudioElevenLib.Level5.Binary.Collections;
using static Lynx.InazumaEleven.Games.GO.GOSupport;
using StudioElevenLib.Level5.Archive.XPCK;
using Type = System.Type;
using System.Reflection;

namespace Lynx.InazumaEleven.Games
{
    /// <summary>
    /// Represents a game in the Inazuma Eleven series, providing access to game data and functionality.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        public virtual string Name => "";

        /// <summary>
        /// Gets or sets the language code of the game.
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the ARC0 archive for ie_a fa
        /// </summary>
        public ARC0 IE_A { get; set; }

        /// <summary>
        /// Gets or sets the ARC0 archive for ie_b fa
        /// </summary>
        public ARC0 IE_B { get; set; }

        /// <summary>
        /// Gets or sets the ARC0 archive for the language fa
        /// </summary>
        public ARC0 IE_C { get; set; }

        /// <summary>
        /// Gets or sets the XPCK archive for GDS Pack.
        /// </summary>
        public XPCK GDSPack { get; set; }

        /// <summary>
        /// Gets or sets the path to the RomFS.
        /// </summary>
        public string RomFSPath { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of game files, where the key is the filename and the value is the GameFile object.
        /// </summary>       
        public Dictionary<string, GameSupports.GameFile> Files { get; set; }

        /// <summary>
        /// Gets or sets the type for Charabase.
        /// </summary>        
        public Type TypeCharabase { get; set; }

        /// <summary>
        /// Gets or sets the type for Charaparam.
        /// </summary>
        public Type TypeCharaparam { get; set; }

        /// <summary>
        /// Gets or sets the type for Training UID.
        /// </summary>
        public Type TypeTrainingUID { get; set; }

        /// <summary>
        /// Gets or sets the type for Avatar.
        /// </summary>
        public Type TypeAvatar { get; set; }

        /// <summary>
        /// Gets or sets the type for Avatar Time Growth.
        /// </summary>
        public Type TypeAvatarTimeGrowth { get; set; }

        /// <summary>
        /// Gets or sets the type for Skill Config.
        /// </summary>
        public Type TypeSkillConfig { get; set; }

        /// <summary>
        /// Gets or sets the type for Skill Table.
        /// </summary>
        public Type TypeSkillTable { get; set; }

        /// <summary>
        /// Gets or sets the type for NPC Preset.
        /// </summary>
        public Type TypeNPCPreset { get; set; }

        /// <summary>
        /// Gets or sets the type for NPC Appear.
        /// </summary>
        public Type TypeNPCAppear { get; set; }

        /// <summary>
        /// Gets or sets the type for NPC Base.
        /// </summary>
        public Type TypeNPCBase { get; set; }

        /// <summary>
        /// Gets or sets the type for Talk Info.
        /// </summary>
        public Type TypeTalkInfo { get; set; }

        /// <summary>
        /// Gets or sets the type for Talk Config.
        /// </summary>
        public Type TypeTalkConfig { get; set; }

        /// <summary>
        /// Gets or sets the type for Shop Config.
        /// </summary>
        public Type TypeShopConfig { get; set; }

        /// <summary>
        /// Gets or sets the type for Community Info.
        /// </summary>
        public Type TypeCommunityInfo { get; set; }

        /// <summary>
        /// Gets or sets the type for Route Config.
        /// </summary>
        public Type TypeRouteConfig { get; set; }

        /// <summary>
        /// Gets or sets the type for Encount Team Info.
        /// </summary>
        public Type TypeEncountTeamInfo { get; set; }

        /// <summary>
        /// Gets or sets the type for Story Team Info.
        /// </summary>
        public Type TypeStoryTeamInfo { get; set; }

        /// <summary>
        /// Gets or sets the type for Team Param Info.
        /// </summary>
        public Type TypeTeamParamInfo { get; set; }

        /// <summary>
        /// Gets or sets the type for Soccer Info.
        /// </summary>
        public Type TypeSoccerInfo { get; set; }

        /// <summary>
        /// Gets or sets the type for Item Config.
        /// </summary>
        public Type TypeItemConfig { get; set; }

        /// <summary>
        /// Gets or sets the type for Item Config Uniform.
        /// </summary>
        public Type TypeItemConfigUniform { get; set; }

        /// <summary>
        /// Gets or sets the type for Item Config Avatar.
        /// </summary>
        public Type TypeItemConfigAvatar { get; set; }

        /// <summary>
        /// Gets or sets the type for Item Config Director.
        /// </summary>
        public Type TypeItemConfigDirector { get; set; }

        /// <summary>
        /// Gets or sets the type for Item Palpack Card.
        /// </summary>
        public Type TypeItemConfigPalpackCard { get; set; }

        /// <summary>
        /// Creates an empty object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <returns>An empty object of the specified type, or null if the type is not supported or if an error occurs during creation.</returns>
        public T GetEmptyObject<T>() where T : class
        {
            Type targetType = null;

            if (typeof(T) == typeof(ISkillTable))
            {
                targetType = TypeSkillTable;
            }
            else if (typeof(T) == typeof(IAvatar))
            {
                targetType = TypeAvatar;
            }
            else if (typeof(T) == typeof(ICharaparam))
            {
                targetType = TypeCharaparam;
            }
            else if (typeof(T) == typeof(IShopConfig))
            {
                targetType = TypeShopConfig;
            }
            else if (typeof(T) == typeof(ICommunityInfo))
            {
                targetType = TypeCommunityInfo;
            }
            else if (typeof(T) == typeof(ISkillConfig))
            {
                targetType = TypeSkillConfig;
            }
            else if (typeof(T) == typeof(IRouteConfig))
            {
                targetType = TypeRouteConfig;
            }
            else if (typeof(T) == typeof(IEncountTeamInfo))
            {
                targetType = TypeEncountTeamInfo;
            }
            else if (typeof(T) == typeof(IStoryTeamInfo))
            {
                targetType = TypeStoryTeamInfo;
            }
            else if (typeof(T) == typeof(ITeamParamInfo))
            {
                targetType = TypeTeamParamInfo;
            }
            else if (typeof(T) == typeof(ISoccerInfo))
            {
                targetType = TypeSoccerInfo;
            }
            else
            {
                return null;
            }

            if (targetType != null)
            {
                try
                {
                    return Activator.CreateInstance(targetType) as T;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la création d'une instance de {targetType}: {ex.Message}");
                    return null; // Gérer l'erreur de création
                }
            }

            return null;
        }

        public virtual void GetGameFiles()
        {

        }

        /// <summary>
        /// Retrieves a file from the game's file system as a SubMemoryStream.
        /// </summary>
        /// <param name="filename">The name of the file to retrieve.</param>
        /// <returns>A SubMemoryStream representing the file content, or null if the file is not found or an error occurs.</returns>
        public SubMemoryStream GetFile(string filename)
        {
            if (Files.ContainsKey(filename))
            {
                GameSupports.GameFile gameFile = Files[filename];

                if (gameFile.File is ARC0 arc0)
                {
                    return arc0.Directory.GetSubMemoryStreamFromFullPath(gameFile.Path);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a directory from the game's file system.
        /// </summary>
        /// <param name="directoryPath">The path to the directory.</param>
        /// <returns>The VirtualDirectory object representing the directory, or null if the directory is not found.</returns>
        public VirtualDirectory GetDirectory(string directoryPath)
        {
            if (Files.ContainsKey(directoryPath))
            {
                GameSupports.GameFile gameFile = Files[directoryPath];

                if (gameFile.File is ARC0 arc0)
                {
                    if (arc0.Directory.IsFolderExists(gameFile.Path))
                    {
                        return arc0.Directory.GetFolderFromFullPath(gameFile.Path);
                    }
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the content of a file as a byte array.
        /// </summary>
        /// <param name="filename">The name of the file to retrieve.</param>
        /// <returns>A byte array containing the file content, or null if the file is not found or an error occurs.</returns>
        public byte[] GetFileContent(string filename)
        {
            if (Files.ContainsKey(filename))
            {
                GameSupports.GameFile gameFile = Files[filename];

                if (gameFile.File is ARC0 arc0)
                {
                    return arc0.Directory.GetFileFromFullPath(gameFile.Path);
                } else
                {
                    return null;
                }
            } else
            {
                return null;
            }
        }

        public (string, byte[]) GetFileNameAndContent(string shortname)
        {
            if (Files.ContainsKey(shortname))
            {
                GameSupports.GameFile gameFile = Files[shortname];
                string filename = Path.GetFileName(gameFile.Path);

                if (gameFile.File is ARC0 arc0)
                {
                    return (filename, arc0.Directory.GetFileFromFullPath(gameFile.Path));
                }
                else
                {
                    return ("", null);
                }
            }
            else
            {
                return ("", null);
            }
        }

        /// <summary>
        /// Retrieves an array of ICharabase objects from the "chara_base" file.
        /// </summary>
        /// <returns>An array of ICharabase objects.</returns>
        public ICharabase[] GetCharabase()
        {
            CfgBin<CfgTreeNode> charaBaseFile = new CfgBin<CfgTreeNode>();
            charaBaseFile.Open(GetFileContent("chara_base"));

            return charaBaseFile.Entries
                .FlattenEntryToClassList(TypeCharabase, "CHARA_BASE_INFO")
                .Cast<ICharabase>()
                .ToArray();
        }

        /// <summary>
        /// Saves the provided array of ICharabase objects to the "chara_base" file.
        /// </summary>
        /// <param name="charabases">The array of ICharabase objects to save.</param>
        public void SaveCharaBase(ICharabase[] charabases)
        {
            CfgBin<CfgTreeNode> charaBaseFile = new CfgBin<CfgTreeNode>();
            charaBaseFile.Open(GetFileContent("chara_base"));

            CfgTreeNode baseBegin = charaBaseFile.Entries.FindByName("CHARA_BASE_INFO_BEGIN");
            baseBegin.Children.Clear();

            baseBegin.Item.Variables[0].Value = charabases.Length;

            for (int i = 0; i < charabases.Count(); i++)
            {
                Entry newBaseEntry = new Entry("CHARA_BASE_INFO", CfgBinMapper.GetVariablesFromInstance(charabases[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            GetFile("chara_base").ByteContent = charaBaseFile.Save();
        }

        /// <summary>
        /// Retrieves an array of ICharaparam objects from the "chara_param" file.
        /// </summary>
        /// <returns>An array of ICharaparam objects.</returns>
        public ICharaparam[] GetCharaparams()
        {
            CfgBin<CfgTreeNode> charaparamFile = new CfgBin<CfgTreeNode>();
            charaparamFile.Open(GetFileContent("chara_param"));

            return charaparamFile.Entries
                .FlattenEntryToClassList(TypeCharaparam, "CHARA_PARAM_INFO")
                .Cast<ICharaparam>()
                .ToArray();
        }

        /// <summary>
        /// Saves the provided array of ICharaparam objects to the "chara_param" file.
        /// </summary>
        /// <param name="charaparams">The array of ICharaparam objects to save.</param>
        public void SaveCharaparams(ICharaparam[] charaparams)
        {
            CfgBin<CfgTreeNode> charaparamFile = new CfgBin<CfgTreeNode>();
            charaparamFile.Open(GetFileContent("chara_param"));

            CfgTreeNode baseBegin = charaparamFile.Entries.FindByName("CHARA_PARAM_INFO_BEGIN");
            baseBegin.Children.Clear();

            baseBegin.Item.Variables[0].Value = charaparams.Length;

            for (int i = 0; i < charaparams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("CHARA_PARAM_INFO", CfgBinMapper.GetVariablesFromInstance(charaparams[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            GetFile("chara_param").ByteContent = charaparamFile.Save();
        }

        /// <summary>
        /// Retrieves an array of ITrainingUD objects from the "chara_param" file.
        /// </summary>
        /// <returns>An array of ITrainingUD objects.</returns>
        public ITrainingUD[] GetTrainingUDs()
        {
            CfgBin<CfgTreeNode> charaparamFile = new CfgBin<CfgTreeNode>();
            charaparamFile.Open(GetFileContent("chara_param"));

            return charaparamFile.Entries
                .FlattenEntryToClassList(TypeTrainingUID, "TRAINING_UD")
                .Cast<ITrainingUD>()
                .ToArray();
        }

        /// <summary>
        /// Saves the provided array of ITrainingUD objects to the "chara_param" file.
        /// </summary>
        /// <param name="trainingUDs">The array of ITrainingUD objects to save.</param>
        public void SaveTrainingUD(ITrainingUD[] trainingUDs)
        {
            CfgBin<CfgTreeNode> charaparamFile = new CfgBin<CfgTreeNode>();
            charaparamFile.Open(GetFileContent("chara_param"));

            CfgTreeNode baseBegin = charaparamFile.Entries.FindByName("TRAINING_UD_BEGIN");
            baseBegin.Children.Clear();

            for (int i = 0; i < trainingUDs.Count(); i++)
            {
                Entry newBaseEntry = new Entry("TRAINING_UD", CfgBinMapper.GetVariablesFromInstance(trainingUDs[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            GetFile("chara_param").ByteContent = charaparamFile.Save();
        }

        /// <summary>
        /// Retrieves an array of IAvatar objects from the "item_config" file.
        /// </summary>
        /// <param name="emptyAvatar">A boolean indicating whether to add an empty avatar to the returned array.</param>
        /// <returns>An array of IAvatar objects.</returns>
        public IAvatar[] GetAvatars(bool emptyAvatar)
        {
            CfgBin<CfgTreeNode> itemconfigFile = new CfgBin<CfgTreeNode>();
            itemconfigFile.Open(GetFileContent("item_config"));

            List<IAvatar> avatars = itemconfigFile.Entries
                .FlattenEntryToClassList(TypeAvatar, "ITEM_AVATAR")
                .Cast<IAvatar>()
                .ToList();

            if (emptyAvatar)
            {
                avatars.Add(GetEmptyObject<IAvatar>());
            }

            return avatars.ToArray();
        }

        /// <summary>
        /// Saves the provided array of IAvatar objects to the "item_config" file.
        /// </summary>
        /// <param name="avatars">The array of IAvatar objects to save.</param>
        public void SaveAvatars(IAvatar[] avatars)
        {
            CfgBin<CfgTreeNode> itemconfigFile = new CfgBin<CfgTreeNode>();
            itemconfigFile.Open(GetFileContent("item_config"));

            CfgTreeNode baseBegin = itemconfigFile.Entries.FindByName("ITEM_AVATAR_BEGIN");
            baseBegin.Children.Clear();

            baseBegin.Item.Variables[0].Value = avatars.Length;

            for (int i = 0; i < avatars.Count(); i++)
            {
                Entry newBaseEntry = new Entry("ITEM_AVATAR", CfgBinMapper.GetVariablesFromInstance(avatars[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            GetFile("item_config").ByteContent = itemconfigFile.Save();
        }

        /// <summary>
        /// Retrieves an array of IAvatarTimeGrowth objects from the "item_config" file.
        /// </summary>
        /// <returns>An array of IAvatarTimeGrowth objects.</returns>
        public IAvatarTimeGrowth[] GetAvatarGrowthTable()
        {
            CfgBin<CfgTreeNode> itemconfigFile = new CfgBin<CfgTreeNode>();
            itemconfigFile.Open(GetFileContent("item_config"));

            return itemconfigFile.Entries
                .FlattenEntryToClassList(TypeAvatarTimeGrowth, "AVATAR_INDEX")
                .Cast<IAvatarTimeGrowth>()
                .ToArray();
        }

        /// <summary>
        /// Saves the provided array of IAvatarTimeGrowth objects to the "item_config" file.
        /// </summary>
        /// <param name="avatars">The array of IAvatarTimeGrowth objects to save.</param>
        public void SaveAvatarGrowthTable(IAvatarTimeGrowth[] avatars)
        {
            CfgBin<CfgTreeNode> itemconfigFile = new CfgBin<CfgTreeNode>();
            itemconfigFile.Open(GetFileContent("item_config"));

            CfgTreeNode baseBegin = itemconfigFile.Entries.FindByName("AVATAR_INDEX_BEGIN");
            baseBegin.Children.Clear();

            baseBegin.Item.Variables[0].Value = avatars.Length;

            for (int i = 0; i < avatars.Count(); i++)
            {
                Entry newBaseEntry = new Entry("AVATAR_INDEX", CfgBinMapper.GetVariablesFromInstance(avatars[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            GetFile("item_config").ByteContent = itemconfigFile.Save();
        }

        /// <summary>
        /// Retrieves an array of ISkillConfig objects from the "skill_config" file.
        /// </summary>
        /// <param name="emptySkillConfig">A boolean indicating whether to add an empty skill config to the returned array.</param>
        /// <returns>An array of ISkillConfig objects.</returns>
        public ISkillConfig[] GetSkillConfigs(bool emptySkillConfig)
        {
            CfgBin<CfgTreeNode> skillconfigFile = new CfgBin<CfgTreeNode>();
            skillconfigFile.Open(GetFileContent("skill_config"));

            List<ISkillConfig> skills = skillconfigFile.Entries
                .FlattenEntryToClassList(TypeSkillConfig, "SKILL_CONFIG_INFO")
                .Cast<ISkillConfig>()
                .ToList();

            if (emptySkillConfig)
            {
                skills.Add(GetEmptyObject<ISkillConfig>());
            }

            return skills.ToArray();
        }

        /// <summary>
        /// Saves the provided array of ISkillConfig objects to the "skill_config" file.
        /// </summary>
        /// <param name="skills">The array of ISkillConfig objects to save.</param>
        public void SaveSkillConfigs(ISkillConfig[] skills)
        {
            CfgBin<CfgTreeNode> skillconfigFile = new CfgBin<CfgTreeNode>();
            skillconfigFile.Open(GetFileContent("skill_config"));

            CfgTreeNode baseBegin = skillconfigFile.Entries.FindByName("SKILL_CONFIG_INFO_BEGIN");
            baseBegin.Children.Clear();

            baseBegin.Item.Variables[0].Value = skills.Length;

            for (int i = 0; i < skills.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SKILL_CONFIG_INFO", CfgBinMapper.GetVariablesFromInstance(skills[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            GetFile("skill_config").ByteContent = skillconfigFile.Save();
        }

        /// <summary>
        /// Retrieves an array of ISkillTable objects from the "skill_table" file.
        /// </summary>
        /// <returns>An array of ISkillTable objects.</returns>
        public ISkillTable[] GetSkillTable()
        {
            CfgBin<CfgTreeNode> skilltableFile = new CfgBin<CfgTreeNode>();
            skilltableFile.Open(GetFileContent("skill_table"));

            return skilltableFile.Entries
                .FlattenEntryToClassList(TypeSkillTable, "SKILL_TABLE")
                .Cast<ISkillTable>()
                .ToArray();
        }

        /// <summary>
        /// Saves the provided array of ISkillTable objects to the "skill_table" file.
        /// </summary>
        /// <param name="skills">The array of ISkillTable objects to save.</param>
        public void SaveSkillTable(ISkillTable[] skills)
        {
            CfgBin<CfgTreeNode> skilltableFile = new CfgBin<CfgTreeNode>();
            skilltableFile.Open(GetFileContent("skill_table"));

            CfgTreeNode baseBegin = skilltableFile.Entries.FindByName("SKILL_TABLE_BEGIN");
            baseBegin.Children.Clear();

            baseBegin.Item.Variables[0].Value = skills.Length;

            for (int i = 0; i < skills.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SKILL_TABLE", CfgBinMapper.GetVariablesFromInstance(skills[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            GetFile("skill_table").ByteContent = skilltableFile.Save();
        }

        /// <summary>
        /// Retrieves a dictionary of NPCs and their associated appearances for a given map ID.
        /// </summary>
        /// <param name="mapID">The ID of the map.</param>
        /// <returns>A dictionary where the key is an INPCBase object and the value is a list of INPCAppear objects.</returns>
        public Dictionary<INPCBase, List<INPCAppear>> GetNPCs(string mapID)
        {
            CfgBin<CfgTreeNode> npcFile = new CfgBin<CfgTreeNode>();

            VirtualDirectory mapDirectory = GetDirectory("map");

            if (mapDirectory.IsFullPathExists($"/{mapID}/{mapID}.npc.bin"))
            {
                npcFile.Open(mapDirectory.GetFileFromFullPath($"/{mapID}/{mapID}.npc.bin"));

                INPCBase[] npcBases = npcFile.Entries
                    .FlattenEntryToClassList(TypeNPCBase, "NPC_BASE")
                    .Cast<INPCBase>()
                    .ToArray();

                INPCPreset[] npcPresets = npcFile.Entries
                    .FlattenEntryToClassList(TypeNPCPreset, "NPC_PRESET")
                    .Cast<INPCPreset>()
                    .ToArray();

                INPCAppear[] npcAppears = npcFile.Entries
                    .FlattenEntryToClassList(TypeNPCAppear, "NPC_APPEAR")
                    .Cast<INPCAppear>()
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
            }
            else
            {
                return new Dictionary<INPCBase, List<INPCAppear>>();
            }
        }

        /// <summary>
        /// Saves the provided NPC data to the map's NPC file.
        /// </summary>
        /// <param name="npcs">A dictionary where the key is an INPCBase object and the value is a list of INPCAppear objects.</param>
        /// <param name="mapID">The ID of the map.</param>
        public void SaveNPCs(Dictionary<INPCBase, List<INPCAppear>> npcs, string mapID)
        {
            string fileName = $"{mapID}.npc.bin";
            string filePath = $"{mapID}/{fileName}";

            CfgBin<CfgTreeNode> npcFile = new CfgBin<CfgTreeNode>();

            // Set encoding
            Encoding shiftJIS = Encoding.GetEncoding("SHIFT-JIS");
            npcFile.Encoding = shiftJIS;

            VirtualDirectory mapDirectory = GetDirectory("map");

            // Open the npc file if it exist
            if (mapDirectory.IsFullPathExists(filePath))
            {
                npcFile.Open(mapDirectory.GetFileFromFullPath(filePath));
            }

            // Get NPCBase
            CfgTreeNode npcBaseBegin = npcFile.Entries.FindByName("NPC_BASE_BEGIN");
            if (npcBaseBegin != null)
            {
                // resets items if it already exists
                npcBaseBegin.Children.Clear();
                npcBaseBegin.Item.Variables[0].Value = npcs.Count;
            }
            else
            {
                // adds the entry to the npc file if it doesn't exists
                npcBaseBegin = new CfgTreeNode(new Entry("NPC_BASE_BEGIN", new List<Variable>() { new Variable(StudioElevenLib.Level5.Binary.Logic.CfgValueType.Int, npcs.Count) }));
                npcFile.Entries.AddChild(npcBaseBegin);
                npcFile.Entries.AddChild(new CfgTreeNode(new Entry("NPC_BASE_END")));
            }

            // Get NPCPreset
            CfgTreeNode npcPresetBegin = npcFile.Entries.FindByName("NPC_PRESET_BEGIN");
            if (npcPresetBegin != null)
            {
                // resets items if it already exists
                npcPresetBegin.Children.Clear();
                npcPresetBegin.Item.Variables[0].Value = npcs.Count;
            }
            else
            {
                // adds the entry to the npc file if it doesn't exists
                npcPresetBegin = new CfgTreeNode(new Entry("NPC_PRESET_BEGIN", new List<Variable>() { new Variable(StudioElevenLib.Level5.Binary.Logic.CfgValueType.Int, npcs.Count) }));
                npcFile.Entries.AddChild(npcPresetBegin);
                npcFile.Entries.AddChild(new CfgTreeNode(new Entry("NPC_PRESET_END")));
            }

            // Get NPCAppear
            CfgTreeNode npcAppearBegin = npcFile.Entries.FindByName("NPC_APPEAR_BEGIN");
            if (npcAppearBegin != null)
            {
                // resets items if it already exists
                npcAppearBegin.Children.Clear();
                npcAppearBegin.Item.Variables[0].Value = npcs.Values.Sum(list => list.Count);
            }
            else
            {
                // adds the entry to the npc file if it doesn't exists
                npcAppearBegin = new CfgTreeNode(new Entry("NPC_APPEAR_BEGIN", new List<Variable>() { new Variable(StudioElevenLib.Level5.Binary.Logic.CfgValueType.Int, npcs.Values.Sum(list => list.Count)) }));
                npcFile.Entries.AddChild(npcAppearBegin);
                npcFile.Entries.AddChild(new CfgTreeNode(new Entry("NPC_APPEAR_END")));
            }

            int npcCount = 0;

            // Loop on each npc data
            foreach (KeyValuePair<INPCBase, List<INPCAppear>> npc in npcs)
            {
                // Add new item on NPCBase entry
                Entry newNPCBaseEntry = new Entry("NPC_BASE", CfgBinMapper.GetVariablesFromInstance(npc.Key));
                npcBaseBegin.AddChild(new CfgTreeNode(newNPCBaseEntry));

                // Add an NPCPreset based on NPCID and NPCAppear
                NPCPreset newNPCPreset = new NPCPreset(npc.Key.NPCID, npcCount, npc.Value.Count);
                Entry newNPresetEntry = new Entry("NPC_PRESET", CfgBinMapper.GetVariablesFromInstance(newNPCPreset));
                npcPresetBegin.AddChild(new CfgTreeNode(newNPresetEntry));

                // Add all NPCAppear items linked to this NPCBase
                for (int i = 0; i < npc.Value.Count; i++)
                {
                    Entry newNPCAppearEntry = new Entry("NPC_APPEAR", CfgBinMapper.GetVariablesFromInstance(npc.Value[i]));
                    npcAppearBegin.AddChild(new CfgTreeNode(newNPCAppearEntry));
                    npcCount++;
                }
            }

            // Save the file
            mapDirectory.GetFolderFromFullPath(mapID).Files[fileName].ByteContent = npcFile.Save();
        }

        /// <summary>
        /// Retrieves a dictionary of talk information and their associated configurations for a given map ID.
        /// </summary>
        /// <param name="mapID">The ID of the map.</param>
        /// <returns>A dictionary where the key is an ITalkInfo object and the value is a list of ITalkConfig objects.</returns>
        public Dictionary<ITalkInfo, List<ITalkConfig>> GetEvents(string mapID)
        {
            CfgBin<CfgTreeNode> npcFile = new CfgBin<CfgTreeNode>();

            VirtualDirectory mapDirectory = GetDirectory("map");

            if (mapDirectory.IsFullPathExists($"/{mapID}/{mapID}.npc.bin"))
            {
                npcFile.Open(mapDirectory.GetFileFromFullPath($"/{mapID}/{mapID}.talk.bin"));

                ITalkInfo[] talkInfos = npcFile.Entries
                    .FlattenEntryToClassList(TypeTalkInfo, "TALK_INFO")
                    .Cast<ITalkInfo>()
                    .ToArray();

                ITalkConfig[] talkConfigs = npcFile.Entries
                    .FlattenEntryToClassList(TypeTalkConfig, "TALK_CONFIG")
                    .Cast<ITalkConfig>()
                    .ToArray();

                return talkInfos.ToDictionary(
                    talkInfo => talkInfo,
                    talkInfo => talkConfigs.Skip(talkInfo.TalkOffset).Take(talkInfo.TalkCount).ToList());
            }
            else
            {
                return new Dictionary<ITalkInfo, List<ITalkConfig>>();
            }
        }

        /// <summary>
        /// Saves the provided event data to the map's talk file.
        /// </summary>
        /// <param name="events">A dictionary where the key is an ITalkInfo object and the value is a list of ITalkConfig objects.</param>
        /// <param name="mapID">The ID of the map.</param>
        public void SaveEvents(Dictionary<ITalkInfo, List<ITalkConfig>> events, string mapID)
        {
            string fileName = $"{mapID}.talk.bin";
            string filePath = $"{mapID}/{fileName}";

            CfgBin<CfgTreeNode> npcFile = new CfgBin<CfgTreeNode>();

            // Set encoding
            Encoding shiftJIS = Encoding.GetEncoding("SHIFT-JIS");
            npcFile.Encoding = shiftJIS;

            VirtualDirectory mapDirectory = GetDirectory("map");

            // Open the npc file if it exist
            if (mapDirectory.IsFullPathExists(filePath))
            {
                npcFile.Open(mapDirectory.GetFileFromFullPath(filePath));
            }

            // Get TalkInfo
            CfgTreeNode talkInfoBegin = npcFile.Entries.FindByName("TALK_INFO_BEGIN");
            if (talkInfoBegin != null)
            {
                // resets items if it already exists
                talkInfoBegin.Children.Clear();
                talkInfoBegin.Item.Variables[0].Value = events.Count;
            }
            else
            {
                // adds the entry to the npc file if it doesn't exists
                talkInfoBegin = new CfgTreeNode(new Entry("TALK_INFO_BEGIN", new List<Variable>() { new Variable(StudioElevenLib.Level5.Binary.Logic.CfgValueType.Int, events.Count) }));
                npcFile.Entries.AddChild(talkInfoBegin);
                npcFile.Entries.AddChild(new CfgTreeNode(new Entry("TALK_INFO_END")));
            }

            // Get TalkConfig
            CfgTreeNode talkConfigBegin = npcFile.Entries.FindByName("TALK_CONFIG_BEGIN");
            if (talkConfigBegin != null)
            {
                // resets items if it already exists
                talkConfigBegin.Children.Clear();
                talkConfigBegin.Item.Variables[0].Value = events.Values.Sum(list => list.Count);
            }
            else
            {
                // adds the entry to the npc file if it doesn't exists
                talkConfigBegin = new CfgTreeNode(new Entry("TALK_CONFIG_BEGIN", new List<Variable>() { new Variable(StudioElevenLib.Level5.Binary.Logic.CfgValueType.Int, events.Values.Sum(list => list.Count)) }));
                npcFile.Entries.AddChild(talkConfigBegin);
                npcFile.Entries.AddChild(new CfgTreeNode(new Entry("TALK_CONFIG_END")));
            }

            int eventCount = 0;

            // Loop on each npc data
            foreach (KeyValuePair<ITalkInfo, List<ITalkConfig>> myEvent in events)
            {
                // Add a TalkInfo based on TalkConfig
                TalkInfo newTalkInfo = new TalkInfo(myEvent.Key.TalkID, eventCount, myEvent.Value.Count);
                Entry newTalkInfoEntry = new Entry("TALK_INFO", CfgBinMapper.GetVariablesFromInstance(newTalkInfo));
                talkInfoBegin.AddChild(new CfgTreeNode(newTalkInfoEntry));

                // Add all TalkConfig items linked to this TalkInfo
                for (int i = 0; i < myEvent.Value.Count; i++)
                {
                    Entry newNPCTalkConfigEntry = new Entry("TALK_CONFIG", CfgBinMapper.GetVariablesFromInstance(myEvent.Value[i]));
                    talkConfigBegin.AddChild(new CfgTreeNode(newNPCTalkConfigEntry));
                    eventCount++;
                }
            }

            // Save the file
            mapDirectory.GetFolderFromFullPath(mapID).Files[fileName].ByteContent = npcFile.Save();
        }

        /// <summary>
        /// Saves the map text data to a file.
        /// </summary>
        /// <param name="fileData">The text file data.</param>
        /// <param name="events">A dictionary of events associated with talk configurations.</param>
        /// <param name="mapID">The map ID.</param>
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

            VirtualDirectory mapDirectory = GetDirectory("map");

            string fileName = $"{mapID}_{LanguageCode}.cfg.bin";

            // Save the file
            mapDirectory.GetFolderFromFullPath(mapID).Files[fileName].ByteContent = fileData.Save();
        }

        /// <summary>
        /// Retrieves the configuration of a shop from a file.
        /// </summary>
        /// <param name="shopID">The shop ID.</param>
        /// <returns>An array of IShopConfig objects representing the shop's configuration.</returns>
        public IShopConfig[] GetShop(string shopID)
        {
            VirtualDirectory shopDirectory = GetDirectory("shop");

            CfgBin<CfgTreeNode> shopFile = new CfgBin<CfgTreeNode>();
            shopFile.Open(shopDirectory.GetFileFromFullPath($"/shop_{shopID}.cfg.bin"));

            return shopFile.Entries
                .FlattenEntryToClassList(TypeShopConfig, "SHOP_CONFIG_INFO")
                .Cast<IShopConfig>()
                .ToArray();
        }

        /// <summary>
        /// Saves the configuration of a shop to a file.
        /// </summary>
        /// <param name="shopID">The shop ID.</param>
        /// <param name="shop">An array of IShopConfig objects representing the shop's configuration.</param>
        public void SaveShop(string shopID, IShopConfig[] shop)
        {
            VirtualDirectory shopDirectory = GetDirectory("shop");

            CfgBin<CfgTreeNode> shopFile = new CfgBin<CfgTreeNode>();
            shopFile.Open(shopDirectory.GetFileFromFullPath($"/shop_{shopID}.cfg.bin"));

            CfgTreeNode baseBegin = shopFile.Entries.FindByName("SHOP_CONFIG_INFO_BEGIN");
            int crc32ShopID = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(shopID)));

            if (baseBegin != null)
            {
                baseBegin.Children.Clear();
            }
            else
            {
                baseBegin = new CfgTreeNode(new Entry("SHOP_CONFIG_INFO_BEGIN", new List<Variable>() {
                    new Variable(StudioElevenLib.Level5.Binary.Logic.CfgValueType.Int, crc32ShopID),
                    new Variable(StudioElevenLib.Level5.Binary.Logic.CfgValueType.Int, shop.Length)
                }));
                shopFile.Entries.AddChild(baseBegin);
                shopFile.Entries.AddChild(new CfgTreeNode(new Entry("SHOP_CONFIG_INFO_END")));
            }

            baseBegin.Item.Variables[0].Value = crc32ShopID;
            baseBegin.Item.Variables[1].Value = shop.Length;

            for (int i = 0; i < shop.Count(); i++)
            {
                if (shop[i].Condition.ToString() == "0" || shop[i].Condition.ToString() == "")
                {
                    shop[i].Condition = 0;
                }

                Entry newBaseEntry = new Entry("SHOP_CONFIG_INFO", CfgBinMapper.GetVariablesFromInstance(shop[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            shopDirectory.Files[$"shop_{shopID}.cfg.bin"].ByteContent = shopFile.Save();
        }

        /// <summary>
        /// Retrieves information about communities.
        /// </summary>
        /// <returns>An array of ICommunityInfo objects representing the community information.</returns>
        public ICommunityInfo[] GetCommunities()
        {
            CfgBin<CfgTreeNode> communityFile = new CfgBin<CfgTreeNode>();
            communityFile.Open(GetFileContent("community_config"));

            return communityFile.Entries
                .FlattenEntryToClassList(TypeCommunityInfo, "COMMUNITY_INFO")
                .Cast<ICommunityInfo>()
                .ToArray();
        }

        /// <summary>
        /// Saves information about communities.
        /// </summary>
        /// <param name="communities">An array of ICommunityInfo objects representing the community information.</param>
        public void SaveCommunities(ICommunityInfo[] communities)
        {
            CfgBin<CfgTreeNode> communityFile = new CfgBin<CfgTreeNode>();
            communityFile.Open(GetFileContent("community_config"));

            CfgTreeNode baseBegin = communityFile.Entries.FindByName("COMMUNITY_INFO_BEGIN");
            baseBegin.Children.Clear();

            baseBegin.Item.Variables[0].Value = communities.Length;

            for (int i = 0; i < communities.Count(); i++)
            {
                Entry newBaseEntry = new Entry("COMMUNITY_INFO", CfgBinMapper.GetVariablesFromInstance(communities[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            GetFile("community_config").ByteContent = communityFile.Save();
        }

        /// <summary>
        /// Retrieves routes and the name ID from a file.
        /// </summary>
        /// <param name="filename">The name of the route file.</param>
        /// <returns>A tuple containing an array of IRouteConfig objects and the name ID.</returns>
        public (IRouteConfig[], int) GetRoutes(string filename)
        {
            VirtualDirectory soccerDirectory = GetDirectory("soccer");

            CfgBin<CfgTreeNode> routeFile = new CfgBin<CfgTreeNode>();
            routeFile.Open(soccerDirectory.GetFileFromFullPath(filename));

            var routes = routeFile.Entries
                .FlattenEntryToClassList(TypeRouteConfig, "ROUTE_CONFIG")
                .Cast<IRouteConfig>()
                .ToArray();

            int nameID = Convert.ToInt32(routeFile.Entries.Children[0].Item.Variables[1].Value);

            return (routes, nameID);
        }

        /// <summary>
        /// Saves routes to a file.
        /// </summary>
        /// <param name="filename">The name of the route file.</param>
        /// <param name="nameCRC32">The CRC32 of the name.</param>
        /// <param name="routes">An array of IRouteConfig objects representing the routes.</param>
        public void SaveRoutes(string filename, int nameCRC32, IRouteConfig[] routes)
        {
            VirtualDirectory soccerDirectory = GetDirectory("soccer");

            CfgBin<CfgTreeNode> routeFile = new CfgBin<CfgTreeNode>();

            // Set encoding
            Encoding shiftJIS = Encoding.GetEncoding("SHIFT-JIS");
            routeFile.Encoding = shiftJIS;

            // Open the route file if it exist
            if (soccerDirectory.IsFullPathExists(filename))
            {
                routeFile.Open(soccerDirectory.GetFileFromFullPath(filename));
            }
            else
            {
                // Create the entry
                CfgTreeNode communityHeader = new CfgTreeNode(new Entry("ROUTE_CONFIG_BEGIN",
                    new List<Variable>() {
                        new Variable(StudioElevenLib.Level5.Binary.Logic.CfgValueType.Int, routes.Count()),
                        new Variable(StudioElevenLib.Level5.Binary.Logic.CfgValueType.Int, nameCRC32)
                    }));

                // Insert
                routeFile.Entries.AddChild(communityHeader);
                routeFile.Entries.AddChild(new CfgTreeNode(new Entry("ROUTE_CONFIG_END")));
            }

            CfgTreeNode baseBegin = routeFile.Entries.FindByName("ROUTE_CONFIG_BEGIN");
            baseBegin.Children.Clear();

            baseBegin.Item.Variables[0].Value = routes.Length;
            baseBegin.Item.Variables[1].Value = nameCRC32;

            for (int i = 0; i < routes.Count(); i++)
            {
                Entry newBaseEntry = new Entry("ROUTE_CONFIG", CfgBinMapper.GetVariablesFromInstance(routes[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            soccerDirectory.Files[filename].ByteContent = routeFile.Save();
        }

        /// <summary>
        /// Retrieves information about soccer.
        /// </summary>
        /// <returns>An array of ISoccerInfo objects representing the soccer information.</returns>
        public ISoccerInfo[] GetSoccers()
        {
            CfgBin<CfgTreeNode> soccerFile = new CfgBin<CfgTreeNode>();
            soccerFile.Open(GetFileContent("soccer_config"));

            return soccerFile.Entries
                .FlattenEntryToClassList(TypeSoccerInfo, "SOCCER_INFO")
                .Cast<ISoccerInfo>()
                .ToArray();
        }

        /// <summary>
        /// Saves information about soccer.
        /// </summary>
        /// <param name="soccers">An array of ISoccerInfo objects representing the soccer information.</param>
        public void SaveSoccers(ISoccerInfo[] soccers)
        {
            CfgBin<CfgTreeNode> soccerFile = new CfgBin<CfgTreeNode>();
            soccerFile.Open(GetFileContent("soccer_config"));

            CfgTreeNode baseBegin = soccerFile.Entries.FindByName("SOCCER_INFO_BEGIN");
            baseBegin.Children.Clear();

            baseBegin.Item.Variables[0].Value = soccers.Length;

            for (int i = 0; i < soccers.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SOCCER_INFO", CfgBinMapper.GetVariablesFromInstance(soccers[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            GetFile("soccer_config").ByteContent = soccerFile.Save();
        }

        /// <summary>
        /// Retrieves team parameters.
        /// </summary>
        /// <returns>An array of ITeamParamInfo objects representing the team parameters.</returns>
        public ITeamParamInfo[] GetTeamParams()
        {
            CfgBin<CfgTreeNode> teamParamFile = new CfgBin<CfgTreeNode>();
            teamParamFile.Open(GetFileContent("team_param"));

            return teamParamFile.Entries
                .FlattenEntryToClassList(TypeTeamParamInfo, "TEAM_PARAM_INFO")
                .Cast<ITeamParamInfo>()
                .ToArray();
        }

        /// <summary>
        /// Saves team parameters.
        /// </summary>
        /// <param name="teams">An array of ITeamParamInfo objects representing the team parameters.</param>
        public void SaveTeamParams(ITeamParamInfo[] teams)
        {
            CfgBin<CfgTreeNode> teamParamFile = new CfgBin<CfgTreeNode>();
            teamParamFile.Open(GetFileContent("team_param"));

            CfgTreeNode baseBegin = teamParamFile.Entries.FindByName("TEAM_PARAM_INFO_BEGIN");
            baseBegin.Children.Clear();

            baseBegin.Item.Variables[0].Value = teams.Length;

            for (int i = 0; i < teams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("TEAM_PARAM_INFO", CfgBinMapper.GetVariablesFromInstance(teams[i]));
                baseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            GetFile("team_param").ByteContent = teamParamFile.Save();
        }

        /// <summary>
        /// Retrieves the team configuration (story and encounter teams).
        /// </summary>
        /// <returns>An array of objects representing the team configurations (IStoryTeamInfo or IEncountTeamInfo).</returns>
        public object[] GetTeamConfig()
        {
            CfgBin<CfgTreeNode> teamConfigFile = new CfgBin<CfgTreeNode>();
            teamConfigFile.Open(GetFileContent("team_config"));

            return teamConfigFile.Entries
                .FlattenEntryToClassList(TypeStoryTeamInfo, "STORY_TEAM_INFO")
                .Cast<object>()
                .Concat(teamConfigFile.Entries
                    .FlattenEntryToClassList(TypeEncountTeamInfo, "ENCOUNT_TEAM_INFO")
                    .Cast<object>())
                .ToArray();
        }

        /// <summary>
        /// Saves the team configuration (story and encounter teams).
        /// </summary>
        /// <param name="storyTeams">An array of IStoryTeamInfo objects representing the story teams.</param>
        /// <param name="encounterTeams">An array of IEncountTeamInfo objects representing the encounter teams.</param>
        public void SaveTeamConfig(IStoryTeamInfo[] storyTeams, IEncountTeamInfo[] encounterTeams)
        {
            CfgBin<CfgTreeNode> teamConfigFile = new CfgBin<CfgTreeNode>();
            teamConfigFile.Open(GetFileContent("team_config"));

            CfgTreeNode storyBaseBegin = teamConfigFile.Entries.FindByName("STORY_TEAM_INFO_BEGIN");
            storyBaseBegin.Children.Clear();

            storyBaseBegin.Item.Variables[0].Value = storyTeams.Length;

            for (int i = 0; i < storyTeams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("STORY_TEAM_INFO", CfgBinMapper.GetVariablesFromInstance(storyTeams[i]));
                storyBaseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            CfgTreeNode encountBaseBegin = teamConfigFile.Entries.FindByName("ENCOUNT_TEAM_INFO_BEGIN");
            encountBaseBegin.Children.Clear();

            encountBaseBegin.Item.Variables[0].Value = encounterTeams.Length;

            for (int i = 0; i < encounterTeams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("ENCOUNT_TEAM_INFO", CfgBinMapper.GetVariablesFromInstance(encounterTeams[i]));
                encountBaseBegin.AddChild(new CfgTreeNode(newBaseEntry));
            }

            GetFile("team_config").ByteContent = teamConfigFile.Save();
        }

        /// <summary>
        /// Retrieves items based on the item type.
        /// </summary>
        /// <param name="itemType">The type of item to retrieve (equipment, consumable, important, uniform, kizunax, avatar, director, or all).</param>
        /// <returns>An array of IItemConfig objects representing the items.</returns>
        public IItemConfig[] GetItems(string itemType)
        {
            CfgBin<CfgTreeNode> itemconfigFile = new CfgBin<CfgTreeNode>();
            itemconfigFile.Open(GetFileContent("item_config"));

            switch (itemType)
            {
                case "equipment":
                    return itemconfigFile.Entries
                        .FlattenEntryToClassList(TypeItemConfig, "ITEM_EQUIPMENT")
                        .Cast<IItemConfig>()
                        .ToArray();
                case "consumable":
                    return itemconfigFile.Entries
                        .FlattenEntryToClassList(TypeItemConfig, "ITEM_CONSUME")
                        .Cast<IItemConfig>()
                        .ToArray();
                case "important":
                    return itemconfigFile.Entries
                        .FlattenEntryToClassList(TypeItemConfig, "ITEM_IMPORTANT")
                        .Cast<IItemConfig>()
                        .ToArray();
                case "uniform":
                    return itemconfigFile.Entries
                        .FlattenEntryToClassList(TypeItemConfigUniform, "ITEM_UNIFORM")
                        .Cast<IItemConfig>()
                        .ToArray();
                case "kizunax":
                    return itemconfigFile.Entries
                        .FlattenEntryToClassList(TypeItemConfigPalpackCard, "ITEM_KIZUNAX")
                        .Cast<IItemConfig>()
                        .ToArray();
                case "avatar":
                    return itemconfigFile.Entries
                        .FlattenEntryToClassList(TypeItemConfigAvatar, "ITEM_AVATAR")
                        .Cast<IItemConfig>()
                        .ToArray();
                case "director":
                    return itemconfigFile.Entries
                        .FlattenEntryToClassList(TypeItemConfigDirector, "ITEM_DIRECTOR")
                        .Cast<IItemConfig>()
                        .ToArray();
                case "all":
                    string[] itemTypesOther = { "ITEM_EQUIPMENT", "ITEM_CONSUME", "ITEM_IMPORTANT" };

                    var avatarItems = itemconfigFile.Entries
                        .FlattenEntryToClassList(TypeItemConfigAvatar, "ITEM_AVATAR")
                        .Cast<IItemConfig>()
                        .ToList();

                    var directorItems = itemconfigFile.Entries
                        .FlattenEntryToClassList(TypeItemConfigDirector, "ITEM_DIRECTOR")
                        .Cast<IItemConfig>()
                        .ToList();

                    var uniformItems = itemconfigFile.Entries
                        .FlattenEntryToClassList(TypeItemConfigUniform, "ITEM_UNIFORM")
                        .Cast<IItemConfig>()
                        .ToList();

                    var palpackItems = itemconfigFile.Entries
                        .FlattenEntryToClassList(TypeItemConfigPalpackCard, "ITEM_KIZUNAX")
                        .Cast<IItemConfig>()
                        .ToList();

                    var otherItems = itemTypesOther
                        .SelectMany(name => itemconfigFile.Entries
                            .FlattenEntryToClassList(TypeItemConfig, name)
                            .Cast<IItemConfig>())
                        .ToList();

                    otherItems.AddRange(avatarItems.Select(x => (IItemConfig)x));
                    otherItems.AddRange(directorItems.Select(x => (IItemConfig)x));
                    otherItems.AddRange(uniformItems.Select(x => (IItemConfig)x));
                    otherItems.AddRange(palpackItems.Select(x => (IItemConfig)x));

                    return otherItems.ToArray();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Saves items of a given type back into item_config, replacing the existing entries
        /// under the matching begin-entry. The entry name is inferred from T.
        /// </summary>
        /// <typeparam name="T">The concrete item config type, must be a reference type implementing IItemConfig.</typeparam>
        /// <param name="items">The items to write.</param>
        public void SaveItems<T>(T[] items) where T : class, IItemConfig
        {
            string beginName;
            string childName;

            if (typeof(T) == TypeItemConfigUniform)
            {
                beginName = "ITEM_UNIFORM_BEGIN";
                childName = "ITEM_UNIFORM";
            }
            else if (typeof(T) == TypeItemConfigPalpackCard)
            {
                beginName = "ITEM_KIZUNAX_BEGIN";
                childName = "ITEM_KIZUNAX";
            }
            else if (typeof(T) == TypeItemConfigAvatar)
            {
                beginName = "ITEM_AVATAR_BEGIN";
                childName = "ITEM_AVATAR";
            }
            else if (typeof(T) == TypeItemConfigDirector)
            {
                beginName = "ITEM_DIRECTOR_BEGIN";
                childName = "ITEM_DIRECTOR";
            }
            else
            {
                throw new ArgumentException($"Unsupported item type: {typeof(T).Name}");
            }

            CfgBin<CfgTreeNode> itemConfigFile = new CfgBin<CfgTreeNode>();
            itemConfigFile.Open(GetFileContent("item_config"));

            CfgTreeNode baseBegin = itemConfigFile.Entries.FindByName(beginName);
            if (baseBegin == null)
                throw new InvalidOperationException($"Entry '{beginName}' not found in item_config.");

            baseBegin.Children.Clear();
            baseBegin.Item.Variables[0].Value = items.Length;

            for (int i = 0; i < items.Length; i++)
            {
                Entry newEntry = new Entry(childName, CfgBinMapper.GetVariablesFromInstance(items[i]));
                baseBegin.AddChild(new CfgTreeNode(newEntry));
            }

            GetFile("item_config").ByteContent = itemConfigFile.Save();
        }

        /// <summary>
        /// Gets the map environment configuration.
        /// </summary>
        /// <param name="mapID">The ID of the map.</param>
        /// <returns>The CfgBin containing the map environment configuration, or null if not found.</returns>
        public CfgBin<CfgTreeNode> GetMapenv(string mapID)
        {
            VirtualDirectory mapDirectory = GetDirectory("map");

            CfgBin<CfgTreeNode> npcFile = new CfgBin<CfgTreeNode>();

            if (mapDirectory.IsFullPathExists($"/{mapID}/{mapID}_mapenv.bin"))
            {
                npcFile.Open(mapDirectory.GetFileFromFullPath($"/{mapID}/{mapID}_mapenv.bin"));
                return npcFile;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the map text configuration.
        /// </summary>
        /// <param name="mapID">The ID of the map.</param>
        /// <returns>The T2bþ object containing the map text configuration, or null if not found.</returns>
        public T2bþ GetMapText(string mapID)
        {
            VirtualDirectory mapDirectory = GetDirectory("map");

            string filePath = $"/{mapID}/{mapID}_{LanguageCode}.cfg.bin";

            if (mapDirectory.IsFullPathExists(filePath))
            {
                return new T2bþ(mapDirectory.GetFileFromFullPath(filePath));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Saves a text file.
        /// </summary>
        /// <param name="fileName">The GameSupports.GameFile object representing the file to save.</param>
        /// <param name="fileData">The T2bþ object containing the text data to save.</param>
        public void SaveTextFile(GameSupports.GameFile fileName, T2bþ fileData)
        {
            VirtualDirectory directory = fileName.File.Directory.GetFolderFromFullPath(Path.GetDirectoryName(fileName.Path).Replace("\\", "/"));
            directory.Files[Path.GetFileName(fileName.Path)].ByteContent = fileData.Save();
        }

        /// <summary>
        /// Saves changes (implementation not provided).
        /// </summary>
        public virtual void Save()
        {

        }
    }
}
