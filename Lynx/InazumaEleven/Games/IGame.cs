using System.Collections.Generic;
using Lynx.Level5.Archive.ARC0;
using Lynx.InazumaEleven.Logic;
using Lynx.Level5.Text;
using Lynx.Level5.Binary;

namespace Lynx.InazumaEleven.Games
{
    public interface IGame
    {
        string Name { get; }

        ARC0 Game { get; set; }

        string LanguageCode { get; set; }

        Dictionary<string, GameSupports.GameFile> Files { get; set; }

        T GetEmptyObject<T>() where T : class;

        ICharabase[] GetCharabase();

        void SaveCharaBase(ICharabase[] charabases);

        ICharaparam[] GetCharaparams();

        void SaveCharaparams(ICharaparam[] charaparams);

        ITrainingUD[] GetTrainingUDs();

        void SaveTrainingUD(ITrainingUD[] trainingUDs);

        IAvatar[] GetAvatars(bool emptyAvatar);

        void SaveAvatars(IAvatar[] avatars);

        ISkillConfig[] GetSkillConfigs(bool emptySkillConfig);

        void SaveSkillConfigs(ISkillConfig[] skills);

        ISkillTable[] GetSkillTable();

        void SaveSkillTable(ISkillTable[] skills);

        Dictionary<INPCBase, List<INPCAppear>> GetNPCs(string mapID);

        Dictionary<ITalkInfo, List<ITalkConfig>> GetEvents(string mapID);

        IShopConfig[] GetShop(string shopID);

        void SaveShop(string shopID, IShopConfig[] shop);

        ICommunityInfo[] GetCommunities();

        void SaveCommunities(ICommunityInfo[] communities);

        IItemConfig[] GetItems(string itemType);

        CfgBin GetMapenv(string mapID);

        void SaveTextFile(GameSupports.GameFile fileName, T2bþ fileData);

        void Save();
    }
}
