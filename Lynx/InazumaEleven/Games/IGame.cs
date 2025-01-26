using System.Collections.Generic;
using Lynx.Level5.Archive.ARC0;
using Lynx.InazumaEleven.Logic;
using Lynx.Level5.Text;
using Lynx.Level5.Binary;

namespace Lynx.InazumaEleven.Games
{
    /// <summary>
    /// Represents a game interface with methods for managing game data.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Gets the name of the game.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the ARC0 archive of the game.
        /// </summary>
        ARC0 Game { get; set; }

        /// <summary>
        /// Gets or sets the language code of the game.
        /// </summary>
        string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the files of the game as a dictionary for accessing certain directories.
        /// </summary>
        Dictionary<string, GameSupports.GameFile> Files { get; set; }

        /// <summary>
        /// Gets an empty object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to get.</typeparam>
        /// <returns>An empty object of the specified type.</returns>
        T GetEmptyObject<T>() where T : class;

        /// <summary>
        /// Gets the character base data.
        /// </summary>
        /// <returns>An array of character base data.</returns>
        ICharabase[] GetCharabase();

        /// <summary>
        /// Saves the character base data.
        /// </summary>
        /// <param name="charabases">The character base data to save.</param>
        void SaveCharaBase(ICharabase[] charabases);

        /// <summary>
        /// Gets the character parameters.
        /// </summary>
        /// <returns>An array of character parameters.</returns>
        ICharaparam[] GetCharaparams();

        /// <summary>
        /// Saves the character parameters.
        /// </summary>
        /// <param name="charaparams">The character parameters to save.</param>
        void SaveCharaparams(ICharaparam[] charaparams);

        /// <summary>
        /// Gets the training data.
        /// </summary>
        /// <returns>An array of training data.</returns>
        ITrainingUD[] GetTrainingUDs();

        /// <summary>
        /// Saves the training data.
        /// </summary>
        /// <param name="trainingUDs">The training data to save.</param>
        void SaveTrainingUD(ITrainingUD[] trainingUDs);

        /// <summary>
        /// Gets the avatars.
        /// </summary>
        /// <param name="emptyAvatar">A value indicating whether to get empty avatars.</param>
        /// <returns>An array of avatars.</returns>
        IAvatar[] GetAvatars(bool emptyAvatar);

        /// <summary>
        /// Saves the avatars.
        /// </summary>
        /// <param name="avatars">The avatars to save.</param>
        void SaveAvatars(IAvatar[] avatars);

        /// <summary>
        /// Gets the avatar growth table.
        /// </summary>
        /// <returns>An array of avatar growth data.</returns>
        IAvatarTimeGrowth[] GetAvatarGrowthTable();

        /// <summary>
        /// Saves the avatar growth table.
        /// </summary>
        /// <param name="avatars">The avatar growth data to save.</param>
        void SaveAvatarGrowthTable(IAvatarTimeGrowth[] avatars);

        /// <summary>
        /// Gets the skill configurations.
        /// </summary>
        /// <param name="emptySkillConfig">A value indicating whether to get empty skill configurations.</param>
        /// <returns>An array of skill configurations.</returns>
        ISkillConfig[] GetSkillConfigs(bool emptySkillConfig);

        /// <summary>
        /// Saves the skill configurations.
        /// </summary>
        /// <param name="skills">The skill configurations to save.</param>
        void SaveSkillConfigs(ISkillConfig[] skills);

        /// <summary>
        /// Exports the skill configurations.
        /// </summary>
        /// <param name="skills">The skill configurations to export.</param>
        /// <returns>A tuple containing the file name and byte array representation of the exported skill configurations.</returns>
        (string, byte[]) ExportSkillConfigs(ISkillConfig[] skills);

        /// <summary>
        /// Gets the skill table.
        /// </summary>
        /// <returns>An array of skill table data.</returns>
        ISkillTable[] GetSkillTable();

        /// <summary>
        /// Saves the skill table.
        /// </summary>
        /// <param name="skills">The skill table data to save.</param>
        void SaveSkillTable(ISkillTable[] skills);

        /// <summary>
        /// Gets the NPCs for the specified map ID.
        /// </summary>
        /// <param name="mapID">The map ID to get the NPCs for.</param>
        /// <returns>A dictionary of NPCs and their appearances.</returns>
        Dictionary<INPCBase, List<INPCAppear>> GetNPCs(string mapID);

        /// <summary>
        /// Gets the events for the specified map ID.
        /// </summary>
        /// <param name="mapID">The map ID to get the events for.</param>
        /// <returns>A dictionary of events and their configurations.</returns>
        Dictionary<ITalkInfo, List<ITalkConfig>> GetEvents(string mapID);

        /// <summary>
        /// Gets the shop configurations for the specified shop ID.
        /// </summary>
        /// <param name="shopID">The shop ID to get the configurations for.</param>
        /// <returns>An array of shop configurations.</returns>
        IShopConfig[] GetShop(string shopID);

        /// <summary>
        /// Saves the shop configurations.
        /// </summary>
        /// <param name="shopID">The shop ID to save the configurations for.</param>
        /// <param name="shop">The shop configurations to save.</param>
        void SaveShop(string shopID, IShopConfig[] shop);

        /// <summary>
        /// Gets the community information.
        /// </summary>
        /// <returns>An array of community information.</returns>
        ICommunityInfo[] GetCommunities();

        /// <summary>
        /// Saves the community information.
        /// </summary>
        /// <param name="communities">The community information to save.</param>
        void SaveCommunities(ICommunityInfo[] communities);

        /// <summary>
        /// Gets the item configurations for the specified item type.
        /// </summary>
        /// <param name="itemType">The item type to get the configurations for.</param>
        /// <returns>An array of item configurations.</returns>
        IItemConfig[] GetItems(string itemType);

        /// <summary>
        /// Gets the map environment configuration for the specified map ID.
        /// </summary>
        /// <param name="mapID">The map ID to get the environment configuration for.</param>
        /// <returns>The map environment configuration.</returns>
        CfgBin GetMapenv(string mapID);

        /// <summary>
        /// Saves the text file with the specified file name and data.
        /// </summary>
        /// <param name="fileName">The file name to save the text file as.</param>
        /// <param name="fileData">The data to save in the text file.</param>
        void SaveTextFile(GameSupports.GameFile fileName, T2bþ fileData);

        /// <summary>
        /// Saves the game data.
        /// </summary>
        void Save();
    }
}
