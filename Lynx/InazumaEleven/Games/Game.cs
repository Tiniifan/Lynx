using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lynx.Level5.Archive.ARC0;
using Lynx.Tools;
using Lynx.Level5.Binary;
using Lynx.InazumaEleven.Logic;
using Lynx.Level5.Text;
using Lynx.Level5.Binary.Logic;
using static Lynx.InazumaEleven.Games.GO.GOSupport;
using Lynx.Level5.Archive.XPCK;
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
            CfgBin charaBaseFile = new CfgBin();
            charaBaseFile.Open(GetFileContent("chara_base"));

            return charaBaseFile.Entries
                .Where(x => x.GetName() == "CHARA_BASE_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (ICharabase)x.ToClass(TypeCharabase))
                .ToArray();
        }

        /// <summary>
        /// Saves the provided array of ICharabase objects to the "chara_base" file.
        /// </summary>
        /// <param name="charabases">The array of ICharabase objects to save.</param>
        public void SaveCharaBase(ICharabase[] charabases)
        {
            CfgBin charaBaseFile = new CfgBin();
            charaBaseFile.Open(GetFileContent("chara_base"));

            Entry baseBegin = charaBaseFile.Entries.Where(x => x.GetName() == "CHARA_BASE_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = charabases.Length;

            for (int i = 0; i < charabases.Count(); i++)
            {
                Entry newBaseEntry = new Entry("CHARA_BASE_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(charabases[i]);
                baseBegin.Children.Add(newBaseEntry);
            }

            GetFile("chara_base").ByteContent = charaBaseFile.Save();
        }

        /// <summary>
        /// Retrieves an array of ICharaparam objects from the "chara_param" file.
        /// </summary>
        /// <returns>An array of ICharaparam objects.</returns>
        public ICharaparam[] GetCharaparams()
        {
            CfgBin charaparamFile = new CfgBin();
            charaparamFile.Open(GetFileContent("chara_param"));

            return charaparamFile.Entries
                .Where(x => x.GetName() == "CHARA_PARAM_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (ICharaparam)x.ToClass(TypeCharaparam))
                .ToArray();
        }

        /// <summary>
        /// Saves the provided array of ICharaparam objects to the "chara_param" file.
        /// </summary>
        /// <param name="charaparams">The array of ICharaparam objects to save.</param>
        public void SaveCharaparams(ICharaparam[] charaparams)
        {
            CfgBin charaparamFile = new CfgBin();
            charaparamFile.Open(GetFileContent("chara_param"));

            Entry baseBegin = charaparamFile.Entries.Where(x => x.GetName() == "CHARA_PARAM_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = charaparams.Length;

            for (int i = 0; i < charaparams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("CHARA_PARAM_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(charaparams[i]);
                baseBegin.Children.Add(newBaseEntry);
            }

            GetFile("chara_param").ByteContent = charaparamFile.Save();
        }

        /// <summary>
        /// Retrieves an array of ITrainingUD objects from the "chara_param" file.
        /// </summary>
        /// <returns>An array of ITrainingUD objects.</returns>
        public ITrainingUD[] GetTrainingUDs()
        {
            CfgBin charaparamFile = new CfgBin();
            charaparamFile.Open(GetFileContent("chara_param"));

            return charaparamFile.Entries
                .Where(x => x.GetName() == "TRAINING_UD_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (ITrainingUD)x.ToClass(TypeTrainingUID))
                .ToArray();
        }

        /// <summary>
        /// Saves the provided array of ITrainingUD objects to the "chara_param" file.
        /// </summary>
        /// <param name="trainingUDs">The array of ITrainingUD objects to save.</param>
        public void SaveTrainingUD(ITrainingUD[] trainingUDs)
        {
            CfgBin charaparamFile = new CfgBin();
            charaparamFile.Open(GetFileContent("chara_param"));

            Entry baseBegin = charaparamFile.Entries.Where(x => x.GetName() == "TRAINING_UD_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            for (int i = 0; i < trainingUDs.Count(); i++)
            {
                Entry newBaseEntry = new Entry("TRAINING_UD_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(trainingUDs[i]);
                baseBegin.Children.Add(newBaseEntry);
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
            CfgBin itemconfigFile = new CfgBin();
            itemconfigFile.Open(GetFileContent("item_config"));

            List<IAvatar> avatars = itemconfigFile.Entries
                .Where(x => x.GetName() == "ITEM_AVATAR_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (IAvatar)x.ToClass(TypeAvatar))
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
            CfgBin itemconfigFile = new CfgBin();
            itemconfigFile.Open(GetFileContent("item_config"));

            Entry baseBegin = itemconfigFile.Entries.Where(x => x.GetName() == "ITEM_AVATAR_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = avatars.Length;

            for (int i = 0; i < avatars.Count(); i++)
            {
                Entry newBaseEntry = new Entry("ITEM_AVATAR_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(avatars[i]);
                baseBegin.Children.Add(newBaseEntry);
            }

            GetFile("item_config").ByteContent = itemconfigFile.Save();
        }

        /// <summary>
        /// Retrieves an array of IAvatarTimeGrowth objects from the "item_config" file.
        /// </summary>
        /// <returns>An array of IAvatarTimeGrowth objects.</returns>
        public IAvatarTimeGrowth[] GetAvatarGrowthTable()
        {
            CfgBin itemconfigFile = new CfgBin();
            itemconfigFile.Open(GetFileContent("item_config"));

            return itemconfigFile.Entries
                .Where(x => x.GetName() == "AVATAR_INDEX_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (IAvatarTimeGrowth)x.ToClass(TypeAvatarTimeGrowth))
                .ToArray();
        }

        /// <summary>
        /// Saves the provided array of IAvatarTimeGrowth objects to the "item_config" file.
        /// </summary>
        /// <param name="avatars">The array of IAvatarTimeGrowth objects to save.</param>
        public void SaveAvatarGrowthTable(IAvatarTimeGrowth[] avatars)
        {
            CfgBin itemconfigFile = new CfgBin();
            itemconfigFile.Open(GetFileContent("item_config"));

            Entry baseBegin = itemconfigFile.Entries.Where(x => x.GetName() == "AVATAR_INDEX_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = avatars.Length;

            for (int i = 0; i < avatars.Count(); i++)
            {
                Entry newBaseEntry = new Entry("AVATAR_INDEX_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(avatars[i]);
                baseBegin.Children.Add(newBaseEntry);
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
            CfgBin skillconfigFile = new CfgBin();
            skillconfigFile.Open(GetFileContent("skill_config"));

            List<ISkillConfig> skills = skillconfigFile.Entries
                .Where(x => x.GetName() == "SKILL_CONFIG_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (ISkillConfig)x.ToClass(TypeSkillConfig))
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
            CfgBin skillconfigFile = new CfgBin();
            skillconfigFile.Open(GetFileContent("skill_config"));

            Entry baseBegin = skillconfigFile.Entries.Where(x => x.GetName() == "SKILL_CONFIG_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = skills.Length;

            for (int i = 0; i < skills.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SKILL_CONFIG_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(skills[i]);
                baseBegin.Children.Add(newBaseEntry);
            }

            GetFile("skill_config").ByteContent = skillconfigFile.Save();
        }

        /// <summary>
        /// Retrieves an array of ISkillTable objects from the "skill_table" file.
        /// </summary>
        /// <returns>An array of ISkillTable objects.</returns>
        public ISkillTable[] GetSkillTable()
        {
            CfgBin skilltableFile = new CfgBin();
            skilltableFile.Open(GetFileContent("skill_table"));

            return skilltableFile.Entries
                .Where(x => x.GetName() == "SKILL_TABLE_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (ISkillTable)x.ToClass(TypeSkillTable))
                .ToArray();
        }

        /// <summary>
        /// Saves the provided array of ISkillTable objects to the "skill_table" file.
        /// </summary>
        /// <param name="skills">The array of ISkillTable objects to save.</param>
        public void SaveSkillTable(ISkillTable[] skills)
        {
            CfgBin skilltableFile = new CfgBin();
            skilltableFile.Open(GetFileContent("skill_table"));

            Entry baseBegin = skilltableFile.Entries.Where(x => x.GetName() == "SKILL_TABLE_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = skills.Length;

            for (int i = 0; i < skills.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SKILL_TABLE_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(skills[i]);
                baseBegin.Children.Add(newBaseEntry);
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
            CfgBin npcFile = new CfgBin();

            VirtualDirectory mapDirectory = GetDirectory("map");

            if (mapDirectory.IsFullPathExists($"/{mapID}/{mapID}.npc.bin"))
            {
                npcFile.Open(mapDirectory.GetFileFromFullPath($"/{mapID}/{mapID}.npc.bin"));

                INPCBase[] npcBases = npcFile.Entries
                    .Where(x => x.GetName() == "NPC_BASE_BEGIN")
                    .SelectMany(x => x.Children)
                    .Select(x => (INPCBase)x.ToClass(TypeNPCBase))
                    .ToArray();

                INPCPreset[] npcPresets = npcFile.Entries
                    .Where(x => x.GetName() == "NPC_PRESET_BEGIN")
                    .SelectMany(x => x.Children)
                    .Select(x => (INPCPreset)x.ToClass(TypeNPCPreset))
                    .ToArray();

                INPCAppear[] npcAppears = npcFile.Entries
                    .Where(x => x.GetName() == "NPC_APPEAR_BEGIN")
                    .SelectMany(x => x.Children)
                    .Select(x => (INPCAppear)x.ToClass(TypeNPCAppear))
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

            CfgBin npcFile = new CfgBin();

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
                newNPCBaseEntry.SetVariablesFromClass(npc.Key);
                npcBaseBegin.Children.Add(newNPCBaseEntry);

                // Add an NPCPreset based on NPCID and NPCAppear
                NPCPreset newNPCPreset = new NPCPreset(npc.Key.NPCID, npcCount, npc.Value.Count);
                Entry newNPresetEntry = new Entry("NPC_PRESET_" + npcIndex, new List<Variable>(), shiftJIS);
                newNPresetEntry.SetVariablesFromClass(newNPCPreset);
                npcPresetBegin.Children.Add(newNPresetEntry);

                // Add all NPCAppear items linked to this NPCBase
                for (int i = 0; i < npc.Value.Count; i++)
                {
                    Entry newNPCAppearEntry = new Entry("NPC_APPEAR_" + npcCount, new List<Variable>(), shiftJIS);
                    newNPCAppearEntry.SetVariablesFromClass(npc.Value[i]);
                    npcAppearBegin.Children.Add(newNPCAppearEntry);
                    npcCount++;
                }

                npcIndex++;
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
            CfgBin npcFile = new CfgBin();

            VirtualDirectory mapDirectory = GetDirectory("map");

            if (mapDirectory.IsFullPathExists($"/{mapID}/{mapID}.npc.bin"))
            {
                npcFile.Open(mapDirectory.GetFileFromFullPath($"/{mapID}/{mapID}.talk.bin"));

                ITalkInfo[] talkInfos = npcFile.Entries
                    .Where(x => x.GetName() == "TALK_INFO_BEGIN")
                    .SelectMany(x => x.Children)
                    .Select(x => (ITalkInfo)x.ToClass(TypeTalkInfo))
                    .ToArray();

                ITalkConfig[] talkConfigs = npcFile.Entries
                    .Where(x => x.GetName() == "TALK_CONFIG_BEGIN")
                    .SelectMany(x => x.Children)
                    .Select(x => (ITalkConfig)x.ToClass(TypeTalkConfig))
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

            CfgBin npcFile = new CfgBin();

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
                newTalkInfoEntry.SetVariablesFromClass(newTalkInfo);
                talkInfoBegin.Children.Add(newTalkInfoEntry);

                // Add all TalkConfig items linked to this TalkInfo
                for (int i = 0; i < myEvent.Value.Count; i++)
                {
                    Entry newNPCTalkConfigEntry = new Entry("TALK_CONFIG_" + eventCount, new List<Variable>(), shiftJIS);
                    newNPCTalkConfigEntry.SetVariablesFromClass(myEvent.Value[i]);
                    talkConfigBegin.Children.Add(newNPCTalkConfigEntry);
                    eventCount++;
                }

                eventIndex++;
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

            CfgBin shopFile = new CfgBin();
            shopFile.Open(shopDirectory.GetFileFromFullPath($"/shop_{shopID}.cfg.bin"));

            return shopFile.Entries
                .Where(x => x.GetName() == "SHOP_CONFIG_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (IShopConfig)x.ToClass(TypeShopConfig))
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

            CfgBin shopFile = new CfgBin();
            shopFile.Open(shopDirectory.GetFileFromFullPath($"/shop_{shopID}.cfg.bin"));

            Entry baseBegin = shopFile.Entries.Where(x => x.GetName() == "SHOP_CONFIG_INFO_BEGIN").FirstOrDefault();
            int crc32ShopID = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(shopID)));

            if (baseBegin != null)
            {
                baseBegin.Children.Clear();
            }
            else
            {
                baseBegin = new Entry("SHOP_CONFIG_INFO_BEGIN_0", new List<Variable>() {
                    new Variable(Level5.Binary.Logic.Type.Int, crc32ShopID),
                    new Variable(Level5.Binary.Logic.Type.Int, shop.Length)
                }, Encoding.UTF8, true);
                shopFile.Entries.Add(baseBegin);
            }

            baseBegin.Variables[0].Value = crc32ShopID;
            baseBegin.Variables[1].Value = shop.Length;

            for (int i = 0; i < shop.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SHOP_CONFIG_INFO_" + i, new List<Variable>(), Encoding.UTF8);

                if (shop[i].Condition.ToString() == "0" || shop[i].Condition.ToString() == "")
                {
                    shop[i].Condition = 0;
                }

                newBaseEntry.SetVariablesFromClass(shop[i]);
                baseBegin.Children.Add(newBaseEntry);
            }

            shopDirectory.Files[$"shop_{shopID}.cfg.bin"].ByteContent = shopFile.Save();
        }

        /// <summary>
        /// Retrieves information about communities.
        /// </summary>
        /// <returns>An array of ICommunityInfo objects representing the community information.</returns>
        public ICommunityInfo[] GetCommunities()
        {
            CfgBin communityFile = new CfgBin();
            communityFile.Open(GetFileContent("community_config"));

            return communityFile.Entries
                .Where(x => x.GetName() == "COMMUNITY_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (ICommunityInfo)x.ToClass(TypeCommunityInfo))
                .ToArray();
        }

        /// <summary>
        /// Saves information about communities.
        /// </summary>
        /// <param name="communities">An array of ICommunityInfo objects representing the community information.</param>
        public void SaveCommunities(ICommunityInfo[] communities)
        {
            CfgBin communityFile = new CfgBin();
            communityFile.Open(GetFileContent("community_config"));

            Entry baseBegin = communityFile.Entries.Where(x => x.GetName() == "COMMUNITY_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = communities.Length;

            for (int i = 0; i < communities.Count(); i++)
            {
                Entry newBaseEntry = new Entry("COMMUNITY_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(communities[i]);
                baseBegin.Children.Add(newBaseEntry);
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

            CfgBin routeFile = new CfgBin();
            routeFile.Open(soccerDirectory.GetFileFromFullPath(filename));

            var routes = routeFile.Entries
                .Where(x => x.GetName() == "ROUTE_CONFIG_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (IRouteConfig)x.ToClass(TypeRouteConfig))
                .ToArray();

            int nameID = Convert.ToInt32(routeFile.Entries[0].Variables[1].Value);

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

            CfgBin routeFile = new CfgBin();

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
            baseBegin.Variables[1].Value = nameCRC32;

            for (int i = 0; i < routes.Count(); i++)
            {
                Entry newBaseEntry = new Entry("ROUTE_CONFIG_" + i, new List<Variable>(), shiftJIS);
                newBaseEntry.SetVariablesFromClass(routes[i]);
                baseBegin.Children.Add(newBaseEntry);
            }

            soccerDirectory.Files[filename].ByteContent = routeFile.Save();
        }

        /// <summary>
        /// Retrieves information about soccer.
        /// </summary>
        /// <returns>An array of ISoccerInfo objects representing the soccer information.</returns>
        public ISoccerInfo[] GetSoccers()
        {
            CfgBin soccerFile = new CfgBin();
            soccerFile.Open(GetFileContent("soccer_config"));

            return soccerFile.Entries
                .Where(x => x.GetName() == "SOCCER_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (ISoccerInfo)x.ToClass(TypeSoccerInfo))
                .ToArray();
        }

        /// <summary>
        /// Saves information about soccer.
        /// </summary>
        /// <param name="soccers">An array of ISoccerInfo objects representing the soccer information.</param>
        public void SaveSoccers(ISoccerInfo[] soccers)
        {
            CfgBin soccerFile = new CfgBin();
            soccerFile.Open(GetFileContent("soccer_config"));

            Entry baseBegin = soccerFile.Entries.Where(x => x.GetName() == "SOCCER_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = soccers.Length;

            for (int i = 0; i < soccers.Count(); i++)
            {
                Entry newBaseEntry = new Entry("SOCCER_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(soccers[i]);
                baseBegin.Children.Add(newBaseEntry);
            }

            GetFile("soccer_config").ByteContent = soccerFile.Save();
        }

        /// <summary>
        /// Retrieves team parameters.
        /// </summary>
        /// <returns>An array of ITeamParamInfo objects representing the team parameters.</returns>
        public ITeamParamInfo[] GetTeamParams()
        {
            CfgBin teamParamFile = new CfgBin();
            teamParamFile.Open(GetFileContent("team_param"));

            return teamParamFile.Entries
                .Where(x => x.GetName() == "TEAM_PARAM_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x => (ITeamParamInfo)x.ToClass(TypeTeamParamInfo))
                .ToArray();
        }

        /// <summary>
        /// Saves team parameters.
        /// </summary>
        /// <param name="teams">An array of ITeamParamInfo objects representing the team parameters.</param>
        public void SaveTeamParams(ITeamParamInfo[] teams)
        {
            CfgBin teamParamFile = new CfgBin();
            teamParamFile.Open(GetFileContent("team_param"));

            Entry baseBegin = teamParamFile.Entries.Where(x => x.GetName() == "TEAM_PARAM_INFO_BEGIN").FirstOrDefault();
            baseBegin.Children.Clear();

            baseBegin.Variables[0].Value = teams.Length;

            for (int i = 0; i < teams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("TEAM_PARAM_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(teams[i]);
                baseBegin.Children.Add(newBaseEntry);
            }

            GetFile("team_param").ByteContent = teamParamFile.Save();
        }

        /// <summary>
        /// Retrieves the team configuration (story and encounter teams).
        /// </summary>
        /// <returns>An array of objects representing the team configurations (IStoryTeamInfo or IEncountTeamInfo).</returns>
        public object[] GetTeamConfig()
        {
            CfgBin teamConfigFile = new CfgBin();
            teamConfigFile.Open(GetFileContent("team_config"));

            return teamConfigFile.Entries
                .Where(x => x.GetName() == "STORY_TEAM_INFO_BEGIN" || x.GetName() == "ENCOUNT_TEAM_INFO_BEGIN")
                .SelectMany(x => x.Children)
                .Select(x =>
                {
                    if (x.Variables.Count == 53)
                        return (IStoryTeamInfo)x.ToClass(TypeStoryTeamInfo) as object;
                    else if (x.Variables.Count == 20)
                        return (IEncountTeamInfo)x.ToClass(TypeEncountTeamInfo) as object;
                    else
                        return null;
                })
                .Where(x => x != null)
                .ToArray();
        }

        /// <summary>
        /// Saves the team configuration (story and encounter teams).
        /// </summary>
        /// <param name="storyTeams">An array of IStoryTeamInfo objects representing the story teams.</param>
        /// <param name="encounterTeams">An array of IEncountTeamInfo objects representing the encounter teams.</param>
        public void SaveTeamConfig(IStoryTeamInfo[] storyTeams, IEncountTeamInfo[] encounterTeams)
        {
            CfgBin teamConfigFile = new CfgBin();
            teamConfigFile.Open(GetFileContent("team_config"));

            Entry storyBaseBegin = teamConfigFile.Entries.Where(x => x.GetName() == "STORY_TEAM_INFO_BEGIN").FirstOrDefault();
            storyBaseBegin.Children.Clear();

            storyBaseBegin.Variables[0].Value = storyTeams.Length;

            for (int i = 0; i < storyTeams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("STORY_TEAM_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(storyTeams[i]);
                storyBaseBegin.Children.Add(newBaseEntry);
            }

            Entry encountBaseBegin = teamConfigFile.Entries.Where(x => x.GetName() == "ENCOUNT_TEAM_INFO_BEGIN").FirstOrDefault();
            encountBaseBegin.Children.Clear();

            encountBaseBegin.Variables[0].Value = encounterTeams.Length;

            for (int i = 0; i < encounterTeams.Count(); i++)
            {
                Entry newBaseEntry = new Entry("ENCOUNT_TEAM_INFO_" + i, new List<Variable>(), Encoding.UTF8);
                newBaseEntry.SetVariablesFromClass(encounterTeams[i]);
                encountBaseBegin.Children.Add(newBaseEntry);
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
            CfgBin itemconfigFile = new CfgBin();
            itemconfigFile.Open(GetFileContent("item_config"));

            switch (itemType)
            {
                case "equipment":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_EQUIPMENT_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfig))
                        .ToArray();
                case "consumable":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_CONSUME_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfig))
                        .ToArray();
                case "important":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_IMPORTANT_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfig))
                        .ToArray();
                case "uniform":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_UNIFORM_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfigUniform))
                        .ToArray();
                case "kizunax":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_KIZUNAX_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfigPalpackCard))
                        .ToArray();
                case "avatar":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_AVATAR_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfigAvatar))
                        .ToArray();
                case "director":
                    return itemconfigFile.Entries
                        .Where(x => x.GetName() == "ITEM_DIRECTOR_BEGIN")
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfigDirector))
                        .ToArray();
                case "all":
                    string[] itemTypesAvatar = { "ITEM_AVATAR_BEGIN" };
                    string[] itemTypesUniform = { "ITEM_UNIFORM_BEGIN" };
                    string[] itemTypesDirector = { "ITEM_DIRECTOR_BEGIN" };
                    string[] itemTypesPalpack = { "ITEM_KIZUNAX_BEGIN" };
                    string[] itemTypesOther = { "ITEM_EQUIPMENT_BEGIN", "ITEM_CONSUME_BEGIN", "ITEM_IMPORTANT_BEGIN" };

                    var avatarItems = itemconfigFile.Entries
                        .Where(x => itemTypesAvatar.Contains(x.GetName()))
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfigAvatar))
                        .ToList();

                    var directorItems = itemconfigFile.Entries
                        .Where(x => itemTypesDirector.Contains(x.GetName()))
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfigDirector))
                        .ToList();

                    var uniformItems = itemconfigFile.Entries
                        .Where(x => itemTypesUniform.Contains(x.GetName()))
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfigUniform))
                        .ToList();

                    var palpackItems = itemconfigFile.Entries
                        .Where(x => itemTypesPalpack.Contains(x.GetName()))
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfigPalpackCard))
                        .ToList();

                    var otherItems = itemconfigFile.Entries
                        .Where(x => itemTypesOther.Contains(x.GetName()))
                        .SelectMany(x => x.Children)
                        .Select(x => (IItemConfig)x.ToClass(TypeItemConfig))
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
            string prefix;

            if (typeof(T) == TypeItemConfigUniform)
            {
                beginName = "ITEM_UNIFORM_BEGIN";
                prefix = "ITEM_UNIFORM_";
            }
            else if (typeof(T) == TypeItemConfigPalpackCard)
            {
                beginName = "ITEM_KIZUNAX_BEGIN";
                prefix = "ITEM_KIZUNAX_";
            }
            else if (typeof(T) == TypeItemConfigAvatar)
            {
                beginName = "ITEM_AVATAR_BEGIN";
                prefix = "ITEM_AVATAR_";
            }
            else if (typeof(T) == TypeItemConfigDirector)
            {
                beginName = "ITEM_DIRECTOR_BEGIN";
                prefix = "ITEM_DIRECTOR_";
            }
            else
            {
                throw new ArgumentException($"Unsupported item type: {typeof(T).Name}");
            }

            CfgBin itemConfigFile = new CfgBin();
            itemConfigFile.Open(GetFileContent("item_config"));

            Entry baseBegin = itemConfigFile.Entries.FirstOrDefault(x => x.GetName() == beginName);
            if (baseBegin == null)
                throw new InvalidOperationException($"Entry '{beginName}' not found in item_config.");

            baseBegin.Children.Clear();
            baseBegin.Variables[0].Value = items.Length;

            for (int i = 0; i < items.Length; i++)
            {
                Entry newEntry = new Entry(prefix + i, new List<Variable>(), Encoding.UTF8);
                newEntry.SetVariablesFromClass(items[i]);
                baseBegin.Children.Add(newEntry);
            }

            GetFile("item_config").ByteContent = itemConfigFile.Save();
        }

        /// <summary>
        /// Gets the map environment configuration.
        /// </summary>
        /// <param name="mapID">The ID of the map.</param>
        /// <returns>The CfgBin containing the map environment configuration, or null if not found.</returns>
        public CfgBin GetMapenv(string mapID)
        {
            VirtualDirectory mapDirectory = GetDirectory("map");

            CfgBin npcFile = new CfgBin();

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
