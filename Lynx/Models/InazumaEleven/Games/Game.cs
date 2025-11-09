using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Models.InazumaEleven.Logic;
using StudioElevenLib.Level5.Archive.ARC0;
using StudioElevenLib.Level5.Archive.XPCK;
using StudioElevenLib.Level5.Binary.Logic;
using StudioElevenLib.Level5.Binary;
using StudioElevenLib.Tools;
using System.IO;
using System.Windows.Forms;
using StudioElevenLib.Level5.Binary.Collections;
using System.Collections;
using StudioElevenLib.Level5.Text;

namespace Lynx.Models.InazumaEleven.Games
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

            CfgTreeNode charaBaseNode = charaBaseFile.Entries.FindByName("CHARA_BASE_INFO_BEGIN");

            if (charaBaseNode != null)
            {
                var charabases = charaBaseNode.FlattenEntryToClassList(TypeCharabase, "CHARA_BASE_INFO");
                return charabases.Cast<ICharabase>().ToArray();
            }
            else
            {
                return Array.Empty<ICharabase>();
            }
        }

        /// <summary>
        /// Saves the provided array of ICharabase objects to the "chara_base" file.
        /// </summary>
        /// <param name="charabases">The array of ICharabase objects to save.</param>
        public void SaveCharaBase(ICharabase[] charabases)
        {
            CfgBin<CfgTreeNode> charaBaseFile = new CfgBin<CfgTreeNode>();
            charaBaseFile.Open(GetFileContent("chara_base"));

            if (charaBaseFile.Entries.Exists("CHARA_BASE_INFO_BEGIN"))
            {
                charaBaseFile.Entries.Delete("CHARA_BASE_INFO_BEGIN");
            }

            var charabaseList = new List<ICharabase>(charabases);

            charaBaseFile.Entries.AddBoundedEntryFromClassList(
                charabaseList,
                TypeCharabase,
                "CHARA_BASE_INFO_BEGIN",
                "CHARA_BASE_INFO"
            );

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

            CfgTreeNode charaparamNode = charaparamFile.Entries.FindByName("CHARA_PARAM_INFO_BEGIN");

            if (charaparamNode != null)
            {
                var charaparams = charaparamNode.FlattenEntryToClassList(TypeCharaparam, "CHARA_PARAM_INFO");
                return charaparams.Cast<ICharaparam>().ToArray();
            }
            else
            {
                return Array.Empty<ICharaparam>();
            }
        }

        /// <summary>
        /// Saves the provided array of ICharaparam objects to the "chara_param" file.
        /// </summary>
        /// <param name="charaparams">The array of ICharaparam objects to save.</param>
        public void SaveCharaparams(ICharaparam[] charaparams)
        {
            CfgBin<CfgTreeNode> charaparamFile = new CfgBin<CfgTreeNode>();
            charaparamFile.Open(GetFileContent("chara_param"));

            if (charaparamFile.Entries.Exists("CHARA_PARAM_INFO_BEGIN"))
            {
                charaparamFile.Entries.Delete("CHARA_PARAM_INFO_BEGIN");
            }

            var charaparamList = new List<ICharaparam>(charaparams);

            charaparamFile.Entries.AddBoundedEntryFromClassList(
                charaparamList,
                TypeCharaparam,
                "CHARA_PARAM_INFO_BEGIN",
                "CHARA_PARAM_INFO"
            );

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

            CfgTreeNode trainingUdNode = charaparamFile.Entries.FindByName("TRAINING_UD_BEGIN");

            if (trainingUdNode != null)
            {
                var trainingUds = trainingUdNode.FlattenEntryToClassList(TypeTrainingUID, "TRAINING_UD_INFO");
                return trainingUds.Cast<ITrainingUD>().ToArray();
            }
            else
            {
                return Array.Empty<ITrainingUD>();
            }
        }

        /// <summary>
        /// Saves the provided array of ITrainingUD objects to the "chara_param" file.
        /// </summary>
        /// <param name="trainingUDs">The array of ITrainingUD objects to save.</param>
        public void SaveTrainingUD(ITrainingUD[] trainingUDs)
        {
            CfgBin<CfgTreeNode> charaparamFile = new CfgBin<CfgTreeNode>();
            charaparamFile.Open(GetFileContent("chara_param"));

            if (charaparamFile.Entries.Exists("TRAINING_UD_BEGIN"))
            {
                charaparamFile.Entries.Delete("TRAINING_UD_BEGIN");
            }

            var trainingUdList = new List<ITrainingUD>(trainingUDs);

            charaparamFile.Entries.AddBoundedEntryFromClassList(
                trainingUdList,
                TypeTrainingUID,
                "TRAINING_UD_BEGIN",
                "TRAINING_UD_INFO"
            );

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

            CfgTreeNode avatarNode = itemconfigFile.Entries.FindByName("ITEM_AVATAR_BEGIN");

            List<IAvatar> avatarsList = new List<IAvatar>();

            if (avatarNode != null)
            {
                var avatars = avatarNode.FlattenEntryToClassList(TypeAvatar, "ITEM_AVATAR");
                avatarsList.AddRange(avatars.Cast<IAvatar>().ToArray());
            }

            if (emptyAvatar)
            {
                avatarsList.Add(GetEmptyObject<IAvatar>());
            }

            return avatarsList.ToArray();
        }

        /// <summary>
        /// Saves the provided array of IAvatar objects to the "item_config" file.
        /// </summary>
        /// <param name="avatars">The array of IAvatar objects to save.</param>
        public void SaveAvatars(IAvatar[] avatars)
        {
            CfgBin<CfgTreeNode> itemconfigFile = new CfgBin<CfgTreeNode>();
            itemconfigFile.Open(GetFileContent("item_config"));

            if (itemconfigFile.Entries.Exists("ITEM_AVATAR_BEGIN"))
            {
                itemconfigFile.Entries.Delete("ITEM_AVATAR_BEGIN");
            }

            var avatarList = new List<IAvatar>(avatars);

            itemconfigFile.Entries.AddBoundedEntryFromClassList(
                avatarList,
                TypeAvatar,
                "ITEM_AVATAR_BEGIN",
                "ITEM_AVATAR"
            );

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

            CfgTreeNode avatarGrowthNode = itemconfigFile.Entries.FindByName("AVATAR_INDEX_BEGIN");

            if (avatarGrowthNode != null)
            {
                var avatarGrowths = avatarGrowthNode.FlattenEntryToClassList(TypeAvatarTimeGrowth, "AVATAR_INDEX");
                return avatarGrowths.Cast<IAvatarTimeGrowth>().ToArray();
            }
            else
            {
                return Array.Empty<IAvatarTimeGrowth>();
            }
        }

        /// <summary>
        /// Saves the provided array of IAvatarTimeGrowth objects to the "item_config" file.
        /// </summary>
        /// <param name="avatars">The array of IAvatarTimeGrowth objects to save.</param>
        public void SaveAvatarGrowthTable(IAvatarTimeGrowth[] avatars)
        {
            CfgBin<CfgTreeNode> itemconfigFile = new CfgBin<CfgTreeNode>();
            itemconfigFile.Open(GetFileContent("item_config"));

            if (itemconfigFile.Entries.Exists("AVATAR_INDEX_BEGIN"))
            {
                itemconfigFile.Entries.Delete("AVATAR_INDEX_BEGIN");
            }

            var avatarGrowthList = new List<IAvatarTimeGrowth>(avatars);

            itemconfigFile.Entries.AddBoundedEntryFromClassList(
                avatarGrowthList,
                TypeAvatarTimeGrowth,
                "AVATAR_INDEX_BEGIN",
                "AVATAR_INDEX"
            );

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

            CfgTreeNode avatarNode = skillconfigFile.Entries.FindByName("SKILL_CONFIG_INFO_BEGIN");

            List<ISkillConfig> skillList = new List<ISkillConfig>();

            if (avatarNode != null)
            {
                var skills = avatarNode.FlattenEntryToClassList(TypeSkillConfig, "SKILL_CONFIG_INFO");
                skillList.AddRange(skills.Cast<ISkillConfig>().ToArray());
            }

            if (emptySkillConfig)
            {
                skillList.Add(GetEmptyObject<ISkillConfig>());
            }

            return skillList.ToArray();
        }

        /// <summary>
        /// Saves the provided array of ISkillConfig objects to the "skill_config" file.
        /// </summary>
        /// <param name="skills">The array of ISkillConfig objects to save.</param>
        public void SaveSkillConfigs(ISkillConfig[] skills)
        {
            CfgBin<CfgTreeNode> skillconfigFile = new CfgBin<CfgTreeNode>();
            skillconfigFile.Open(GetFileContent("skill_config"));

            if (skillconfigFile.Entries.Exists("SKILL_CONFIG_INFO_BEGIN"))
            {
                skillconfigFile.Entries.Delete("SKILL_CONFIG_INFO_BEGIN");
            }

            var skillList = new List<ISkillConfig>(skills);

            skillconfigFile.Entries.AddBoundedEntryFromClassList(
                skillList,
                TypeSkillConfig,
                "SKILL_CONFIG_INFO_BEGIN",
                "SKILL_CONFIG_INFO"
            );

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

            CfgTreeNode skilltableNode = skilltableFile.Entries.FindByName("SKILL_TABLE_BEGIN");

            if (skilltableNode != null)
            {
                var skilltables = skilltableNode.FlattenEntryToClassList(TypeSkillTable, "SKILL_TABLE");
                return skilltables.Cast<ISkillTable>().ToArray();
            }
            else
            {
                return Array.Empty<ISkillTable>();
            }
        }

        /// <summary>
        /// Saves the provided array of ISkillTable objects to the "skill_table" file.
        /// </summary>
        /// <param name="skills">The array of ISkillTable objects to save.</param>
        public void SaveSkillTable(ISkillTable[] skills)
        {
            CfgBin<CfgTreeNode> skilltableFile = new CfgBin<CfgTreeNode>();
            skilltableFile.Open(GetFileContent("skill_table"));

            if (skilltableFile.Entries.Exists("SKILL_TABLE_BEGIN"))
            {
                skilltableFile.Entries.Delete("SKILL_TABLE_BEGIN");
            }

            var skilltableList = new List<ISkillTable>(skills);

            skilltableFile.Entries.AddBoundedEntryFromClassList(
                skilltableList,
                TypeSkillTable,
                "SKILL_TABLE_BEGIN",
                "SKILL_TABLE"
            );

            GetFile("skill_table").ByteContent = skilltableFile.Save();
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

            CfgTreeNode shopNode = shopFile.Entries.FindByName("SHOP_CONFIG_INFO_BEGIN");

            if (shopNode != null)
            {
                var shops = shopNode.FlattenEntryToClassList(TypeShopConfig, "SHOP_CONFIG_INFO");
                return shops.Cast<IShopConfig>().ToArray();
            }
            else
            {
                return Array.Empty<IShopConfig>();
            }
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

            if (shopFile.Entries.Exists("SHOP_CONFIG_INFO_BEGIN"))
            {
                shopFile.Entries.Delete("SHOP_CONFIG_INFO_BEGIN");
            }

            var shopItemList = new List<IShopConfig>(shop);

            // Prevent null condition
            foreach (var x in shopItemList)
            {
                if (string.IsNullOrEmpty(x.Condition?.ToString()) || x.Condition.ToString() == "0")
                {
                    x.Condition = 0;
                }
            }

            int shopItemCount = shopItemList.Count();
            int crc32ShopID = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(shopID)));

            shopFile.Entries.AddBoundedEntryFromClassList(
                shopItemList,
                TypeShopConfig,
                "SHOP_CONFIG_INFO_BEGIN",
                "SHOP_CONFIG_INFO",
                new List<Variable>() { new Variable(CfgValueType.Int, crc32ShopID), new Variable(CfgValueType.Int, shopItemCount) }
            );

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

            CfgTreeNode communityNode = communityFile.Entries.FindByName("COMMUNITY_INFO_BEGIN");

            if (communityNode != null)
            {
                var communities = communityNode.FlattenEntryToClassList(TypeCommunityInfo, "COMMUNITY_INFO");
                return communities.Cast<ICommunityInfo>().ToArray();
            }
            else
            {
                return Array.Empty<ICommunityInfo>();
            }
        }

        /// <summary>
        /// Saves information about communities.
        /// </summary>
        /// <param name="communities">An array of ICommunityInfo objects representing the community information.</param>
        public void SaveCommunities(ICommunityInfo[] communities)
        {
            CfgBin<CfgTreeNode> communityFile = new CfgBin<CfgTreeNode>();
            communityFile.Open(GetFileContent("community_config"));

            if (communityFile.Entries.Exists("COMMUNITY_INFO_BEGIN"))
            {
                communityFile.Entries.Delete("COMMUNITY_INFO_BEGIN");
            }

            var communityList = new List<ICommunityInfo>(communities);

            communityFile.Entries.AddBoundedEntryFromClassList(
                communityList,
                TypeCommunityInfo,
                "COMMUNITY_INFO_BEGIN",
                "COMMUNITY_INFO"
            );

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

            CfgTreeNode routeNode = routeFile.Entries.FindByName("ROUTE_CONFIG_BEGIN");

            IRouteConfig[] routes;
            int nameID = 0;

            if (routeNode != null)
            {
                routes = routeNode.FlattenEntryToClassList(TypeRouteConfig, "ROUTE_CONFIG")
                                  .Cast<IRouteConfig>()
                                  .ToArray();
                nameID = Convert.ToInt32(routeNode.Item.Variables[1].Value);
            }
            else
            {
                routes = Array.Empty<IRouteConfig>();
            }

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
            VirtualDirectory soccerDirectory = GetDirectory("shop");

            CfgBin<CfgTreeNode> routeFile = new CfgBin<CfgTreeNode>();
            routeFile.Open(soccerDirectory.GetFileFromFullPath(filename));

            if (routeFile.Entries.Exists("SHOP_CONFIG_INFO_BEGIN"))
            {
                routeFile.Entries.Delete("SHOP_CONFIG_INFO_BEGIN");
            }

            var cellList = new List<IRouteConfig>(routes);

            // Prevent null condition
            foreach (var x in cellList)
            {
                if (string.IsNullOrEmpty(x.Condition?.ToString()) || x.Condition.ToString() == "0")
                {
                    x.Condition = 0;
                }
            }

            int cellCount = cellList.Count();

            routeFile.Entries.AddBoundedEntryFromClassList(
                cellList,
                TypeRouteConfig,
                "ROUTE_CONFIG_BEGIN",
                "ROUTE_CONFIG",
                new List<Variable>() { new Variable(CfgValueType.Int, cellCount), new Variable(CfgValueType.Int, nameCRC32) }
            );

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

            CfgTreeNode soccerNode = soccerFile.Entries.FindByName("SOCCER_INFO_BEGIN");

            if (soccerNode != null)
            {
                var soccers = soccerNode.FlattenEntryToClassList(TypeSoccerInfo, "SOCCER_INFO");
                return soccers.Cast<ISoccerInfo>().ToArray();
            }
            else
            {
                return Array.Empty<ISoccerInfo>();
            }
        }

        /// <summary>
        /// Saves information about soccer.
        /// </summary>
        /// <param name="soccers">An array of ISoccerInfo objects representing the soccer information.</param>
        public void SaveSoccers(ISoccerInfo[] soccers)
        {
            CfgBin<CfgTreeNode> soccerFile = new CfgBin<CfgTreeNode>();
            soccerFile.Open(GetFileContent("soccer_config"));

            if (soccerFile.Entries.Exists("SOCCER_INFO_BEGIN"))
            {
                soccerFile.Entries.Delete("SOCCER_INFO_BEGIN");
            }

            var soccerList = new List<ISoccerInfo>(soccers);

            soccerFile.Entries.AddBoundedEntryFromClassList(
                soccerList,
                TypeSoccerInfo,
                "SOCCER_INFO_BEGIN",
                "SOCCER_INFO"
            );

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

            CfgTreeNode teamParamNode = teamParamFile.Entries.FindByName("TEAM_PARAM_INFO_BEGIN");

            if (teamParamNode != null)
            {
                var teamParams = teamParamNode.FlattenEntryToClassList(TypeTeamParamInfo, "TEAM_PARAM_INFO");
                return teamParams.Cast<ITeamParamInfo>().ToArray();
            }
            else
            {
                return Array.Empty<ITeamParamInfo>();
            }
        }

        /// <summary>
        /// Saves team parameters.
        /// </summary>
        /// <param name="teams">An array of ITeamParamInfo objects representing the team parameters.</param>
        public void SaveTeamParams(ITeamParamInfo[] teams)
        {
            CfgBin<CfgTreeNode> teamParamFile = new CfgBin<CfgTreeNode>();
            teamParamFile.Open(GetFileContent("team_param"));

            if (teamParamFile.Entries.Exists("TEAM_PARAM_INFO_BEGIN"))
            {
                teamParamFile.Entries.Delete("TEAM_PARAM_INFO_BEGIN");
            }

            var teamParamList = new List<ITeamParamInfo>(teams);

            teamParamFile.Entries.AddBoundedEntryFromClassList(
                teamParamList,
                TypeTeamParamInfo,
                "TEAM_PARAM_INFO_BEGIN",
                "TEAM_PARAM_INFO"
            );

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

            List<object> teamList = new List<object>();

            CfgTreeNode storyNode = teamConfigFile.Entries.FindByName("STORY_TEAM_INFO_BEGIN");
            CfgTreeNode encountNode = teamConfigFile.Entries.FindByName("ENCOUNT_TEAM_INFO_BEGIN");

            if (storyNode != null)
            {
                var storyTeams = storyNode.FlattenEntryToClassList(TypeStoryTeamInfo, "STORY_TEAM_INFO");
                teamList.AddRange(storyTeams.Cast<IStoryTeamInfo>());
            }

            if (encountNode != null)
            {
                var encountTeams = encountNode.FlattenEntryToClassList(TypeEncountTeamInfo, "ENCOUNT_TEAM_INFO");
                teamList.AddRange(encountTeams.Cast<IEncountTeamInfo>());
            }

            return teamList.ToArray();
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

            // Story team
            if (teamConfigFile.Entries.Exists("STORY_TEAM_INFO_BEGIN"))
            {
                teamConfigFile.Entries.Delete("STORY_TEAM_INFO_BEGIN");
            }

            var storyList = new List<IStoryTeamInfo>(storyTeams);

            teamConfigFile.Entries.AddBoundedEntryFromClassList(
                storyList,
                TypeStoryTeamInfo,
                "STORY_TEAM_INFO_BEGIN",
                "STORY_TEAM_INFO"
            );

            // Encount team
            if (teamConfigFile.Entries.Exists("ENCOUNT_TEAM_INFO_BEGIN"))
            {
                teamConfigFile.Entries.Delete("ENCOUNT_TEAM_INFO_BEGIN");
            }

            var encountList = new List<IEncountTeamInfo>(encounterTeams);

            teamConfigFile.Entries.AddBoundedEntryFromClassList(
                encountList,
                TypeEncountTeamInfo,
                "ENCOUNT_TEAM_INFO_BEGIN",
                "ENCOUNT_TEAM_INFO"
            );

            GetFile("team_config").ByteContent = teamConfigFile.Save();
        }

        /// <summary>
        /// Retrieves items based on the item type.
        /// </summary>
        /// <param name="itemType">The type of item to retrieve (equipment, consumable, important, uniform, kizunax, avatar, director, or all).</param>
        /// <returns>An array of IItemConfig objects representing the items.</returns>
        public IItemConfig[] GetItems(string itemType)
        {
            CfgBin<CfgTreeNode> itemConfigFile = new CfgBin<CfgTreeNode>();
            itemConfigFile.Open(GetFileContent("item_config"));

            List<IItemConfig> items = new List<IItemConfig>();

            void AddItems(string beginName, Type type, string entryName)
            {
                CfgTreeNode node = itemConfigFile.Entries.FindByName(beginName);
                if (node != null)
                {
                    var list = node.FlattenEntryToClassList(type, entryName)
                                   .Cast<IItemConfig>()
                                   .ToArray();
                    items.AddRange(list);
                }
            }

            switch (itemType)
            {
                case "equipment":
                    AddItems("ITEM_EQUIPMENT_BEGIN", TypeItemConfig, "ITEM_EQUIPMENT");
                    break;

                case "consumable":
                    AddItems("ITEM_CONSUME_BEGIN", TypeItemConfig, "ITEM_CONSUME");
                    break;

                case "important":
                    AddItems("ITEM_IMPORTANT_BEGIN", TypeItemConfig, "ITEM_IMPORTANT");
                    break;

                case "uniform":
                    AddItems("ITEM_UNIFORM_BEGIN", TypeItemConfigUniform, "ITEM_UNIFORM");
                    break;

                case "kizunax":
                    AddItems("ITEM_KIZUNAX_BEGIN", TypeItemConfig, "ITEM_KIZUNAX");
                    break;

                case "avatar":
                    AddItems("ITEM_AVATAR_BEGIN", TypeItemConfigAvatar, "ITEM_AVATAR");
                    break;

                case "director":
                    AddItems("ITEM_DIRECTOR_BEGIN", TypeItemConfigDirector, "ITEM_DIRECTOR");
                    break;

                case "all":
                    AddItems("ITEM_EQUIPMENT_BEGIN", TypeItemConfig, "ITEM_EQUIPMENT");
                    AddItems("ITEM_CONSUME_BEGIN", TypeItemConfig, "ITEM_CONSUME");
                    AddItems("ITEM_IMPORTANT_BEGIN", TypeItemConfig, "ITEM_IMPORTANT");
                    AddItems("ITEM_KIZUNAX_BEGIN", TypeItemConfig, "ITEM_KIZUNAX");
                    AddItems("ITEM_UNIFORM_BEGIN", TypeItemConfigUniform, "ITEM_UNIFORM");
                    AddItems("ITEM_AVATAR_BEGIN", TypeItemConfigAvatar, "ITEM_AVATAR");
                    AddItems("ITEM_DIRECTOR_BEGIN", TypeItemConfigDirector, "ITEM_DIRECTOR");
                    break;

                default:
                    return Array.Empty<IItemConfig>();
            }

            return items.ToArray();
        }

        public void SaveCoaches(IItemDirector[] coaches)
        {
            CfgBin<CfgTreeNode> itemConfigFile = new CfgBin<CfgTreeNode>();
            itemConfigFile.Open(GetFileContent("item_config"));

            if (itemConfigFile.Entries.Exists("ITEM_DIRECTOR_BEGIN"))
            {
                itemConfigFile.Entries.Delete("ITEM_DIRECTOR_BEGIN");
            }

            var coachList = new List<IItemDirector>(coaches);

            itemConfigFile.Entries.AddBoundedEntryFromClassList(
                coachList,
                TypeItemConfigDirector,
                "ITEM_DIRECTOR_BEGIN",
                "ITEM_DIRECTOR"
            );

            GetFile("item_config").ByteContent = itemConfigFile.Save();
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
