using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Models.InazumaEleven.Logic;
using StudioElevenLib.Level5.Archive;
using StudioElevenLib.Level5.Archive.ARC0;
using static Lynx.Models.InazumaEleven.Games.GO.GOSupport;

namespace Lynx.Models.InazumaEleven.Games.GO
{
    public class GO : Game
    {
        public override string Name => "Inazuma Eleven Go";

        private string FileName { get; set; }

        private bool IsExtractedFA { get; set; }

        public GO(string filePath, bool isExtractedFA, string languageName)
        {
            FileName = filePath;
            IsExtractedFA = isExtractedFA;

            if (IsExtractedFA)
            {
                string faFolder1 = Path.Combine(filePath, "ie_a.fa");
                string faFolder2 = Path.Combine(filePath, "ie_a_fa");

                if (Directory.Exists(faFolder1))
                {
                    IE_A = (ARC0)Archiver.CreateArchiveFromDirectory(faFolder1, ArchiveType.ARC0);
                }
                else if (Directory.Exists(faFolder2))
                {
                    IE_A = (ARC0)Archiver.CreateArchiveFromDirectory(faFolder2, ArchiveType.ARC0);
                }
                else
                {
                    throw new DirectoryNotFoundException("No extracted romfs folder found. Expected 'ie_a.fa' or 'ie_a_fa'.");
                }
            }
            else
            {
                IE_A = new ARC0(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            }

            LanguageCode = languageName.ToLower();

            GetGameFiles();
            GetTypes();
        }

        private void GetTypes()
        {
            TypeCharabase = typeof(CharaBase);
            TypeCharaparam = typeof(CharaParam);
            TypeTrainingUID = typeof(TrainingUD);
            TypeAvatar = typeof(Avatar);
            TypeAvatarTimeGrowth = typeof(AvatarTimeGrowth);
            TypeSkillConfig = typeof(SkillConfig);
            TypeSkillTable = typeof(SkillTable);
            TypeNPCPreset = typeof(NPCPreset);
            TypeNPCAppear = typeof(NPCAppear);
            TypeNPCBase = typeof(NPCBase);
            TypeTalkInfo = typeof(TalkInfo);
            TypeTalkConfig = typeof(TalkConfig);
            TypeShopConfig = typeof(ShopConfig);
            TypeCommunityInfo = typeof(CommunityInfo);
            TypeRouteConfig = typeof(RouteConfig);
            TypeEncountTeamInfo = typeof(EncountTeamInfo);
            TypeStoryTeamInfo = typeof(StoryTeamInfo);
            TypeTeamParamInfo = typeof(TeamParamInfo);
            TypeSoccerInfo = typeof(SoccerInfo);
            TypeItemConfig = typeof(ItemConfig);
            TypeItemConfigUniform = typeof(ItemConfigUniform);
            TypeItemConfigAvatar = typeof(ItemConfigAvatar);
            TypeItemConfigDirector = typeof(ItemConfigDirector);
        }

        public override void GetGameFiles()
        {
            Files = new Dictionary<string, GameSupports.GameFile>
            {
                // Utility
                { "chara_base", new GameSupports.GameFile(IE_A, "/data/res/character/chara_base.cfg.bin") },
                { "chara_param", new GameSupports.GameFile(IE_A, "/data/res/character/chara_param.cfg.bin") },
                { "item_config", new GameSupports.GameFile(IE_A, "/data/res/item/item_config.cfg.bin") },
                { "skill_config", new GameSupports.GameFile(IE_A, "/data/res/skill/skill_config.cfg.bin") },
                { "skill_table", new GameSupports.GameFile(IE_A, "/data/res/character/skill_table.cfg.bin") },
                { "community_config", new GameSupports.GameFile(IE_A, "/data/res/shop/community_config.cfg.bin") },
                { "soccer_config", new GameSupports.GameFile(IE_A, "/data/res/soccer/soccer_config.cfg.bin") },
                { "team_param", new GameSupports.GameFile(IE_A, "/data/res/team/team_param.cfg.bin") },
                { "team_config", new GameSupports.GameFile(IE_A, "/data/res/team/team_config.cfg.bin") },

                // Folder
                { "map", new GameSupports.GameFile(IE_A, "/data/map/") },
                { "shop", new GameSupports.GameFile(IE_A, "/data/res/shop") },
                { "soccer", new GameSupports.GameFile(IE_A, "/data/res/soccer/") },
                { "face", new GameSupports.GameFile(IE_A, "/data/bustup/face") },
                { "faceAvatar", new GameSupports.GameFile(IE_A, "/data/bustup/avatar") },
                { "modelRpgPlayer", new GameSupports.GameFile(IE_A, "/data/chr/model/rpg/face") },
                { "modelWazaPlayer", new GameSupports.GameFile(IE_A, "/data/chr/model/waza/face") },
                { "modelRpgNPC", new GameSupports.GameFile(IE_A, "/data/chr/model/rpg/npc") },
                { "modelWazaNPC", new GameSupports.GameFile(IE_A, "/data/chr/model/waza/npc") },
                { "eventScript", new GameSupports.GameFile(IE_A, "/data/script/event") },
                { "script", new GameSupports.GameFile(IE_A, "/data/script/") },
                { "modelRPGShoes", new GameSupports.GameFile(IE_A, "/data/chr/model/rpg/shoes") },
                { "modelRPGBody", new GameSupports.GameFile(IE_A, "/data/chr/model/rpg/body") },
                { "modelRPGGloves", new GameSupports.GameFile(IE_A, "/data/chr/model/rpg/glove") },
                { "emblem", new GameSupports.GameFile(IE_A, "/data/emblem/") },

                // Text
                { "encount_area_text", new GameSupports.GameFile(IE_A, "/data/res/text/encount_area_text_" + LanguageCode + ".cfg.bin") },
                { "system_text", new GameSupports.GameFile(IE_A, "/data/res/text/system_text_" + LanguageCode + ".cfg.bin") },
                { "chara_text", new GameSupports.GameFile(IE_A, "/data/res/text/chara_text_" + LanguageCode + ".cfg.bin") },
                { "item_text", new GameSupports.GameFile(IE_A, "/data/res/text/item_text_" + LanguageCode + ".cfg.bin") },
                { "skill_text", new GameSupports.GameFile(IE_A, "/data/res/text/skill_text_" + LanguageCode + ".cfg.bin") },
                { "kiznax_hint_text", new GameSupports.GameFile(IE_A, "/data/res/text/kiznax_hint_text_" + LanguageCode + ".cfg.bin") },
                { "team_text", new GameSupports.GameFile(IE_A, "/data/res/text/team_text_" + LanguageCode + ".cfg.bin") },
                { "troute_text", new GameSupports.GameFile(IE_A, "/data/res/text/troute_text_" + LanguageCode + ".cfg.bin") },
            };
        }

        public override void Save()
        {
            // Will need to be remake to support extracted fa files

            //string tempPath = @"./temp";

            //if (!Directory.Exists(tempPath))
            //{
            //    Directory.CreateDirectory(tempPath);
            //}

            //// Save
            //IE_A.Save(tempPath + Path.GetFileName(FileName));

            //// Close File
            //IE_A.Close();

            //if (File.Exists(FileName))
            //{
            //    File.Delete(FileName);
            //}

            //File.Move(tempPath + Path.GetFileName(FileName), FileName);
            //IE_A = new ARC0(new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            //GetGameFiles();
        }
    }
}
