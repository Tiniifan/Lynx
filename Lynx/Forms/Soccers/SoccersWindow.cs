using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using Lynx.Tools;
using Lynx.Level5.Text;
using Lynx.Level5.Image;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;
using Lynx.InazumaEleven.Common;
using Lynx.InazumaEleven.Games.GO;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using static Lynx.InazumaEleven.Games.GO.GOSupport;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using Font = System.Drawing.Font;
using Color = System.Drawing.Color;
using Control = System.Windows.Forms.Control;
using ChallengeRouteClass = Lynx.InazumaEleven.Logic.ChallengeRoute;
using Team = Lynx.InazumaEleven.Logic.Team;
using System.Text.RegularExpressions;
using Lynx.Level5.Save.Logic.Competition_Route;
using Lynx.Level5.Base64;
using DocumentFormat.OpenXml.Wordprocessing;
using Lynx.UI;
using System.Reflection;
using Lynx.Forms.Characters;
using Lynx.Level5.Save.Logic;
using System.Runtime.Remoting.Lifetime;
using System.Xml.Linq;
using Lynx.Forms.Skills;

namespace Lynx.Forms.Soccers
{
    public partial class SoccersWindow : Form
    {
        private IGame GameOpened;

        private T2bþ Itemtext;

        private T2bþ TeamText;

        private T2bþ CharaText;

        private List<IItemConfig> ItemsConfigs;

        private List<ISoccerInfo> Soccers;

        private List<ITeamParamInfo> TeamParams;

        private List<object> TeamConfigs;

        private Dictionary<int, string> BootsNamesDict;

        private Dictionary<int, string> GlovesNamesDict;

        private Dictionary<int, string> BraceletNamesDict;

        private Dictionary<int, string> PendantNamesDict;

        private Dictionary<int, string> FormationNamesDict;

        private Dictionary<int, string> TacticNamesDict;

        private Dictionary<int, string> CoachNamesDict;

        private Dictionary<int, string> KitNamesDict;

        private Dictionary<int, string> ItemsNamesDict;

        private Dictionary<int, string> SoccerNamesDict;

        private Dictionary<int, string> TeamParamNamesDict;

        private Dictionary<int, string> TeamConfigNamesDict;

        private Dictionary<int, string> CharaNamesDict;

        private ISoccerInfo SelectedSoccer;

        private ITeamParamInfo SelectedTeamParam;

        private object SelectedTeamConfig;

        public SoccersWindow(IGame game)
        {
            GameOpened = game;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            InitializeComponent();
            InitializeSoccersResource();
        }

        private Dictionary<int, string> GetNames(IItemConfig[] items)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var item in items)
            {
                // Determine the item name
                string name = item.NameID == 0x00
                    ? " "
                    : Itemtext.Nouns.TryGetValue(item.NameID, out var noun) && noun.Strings.Count > 0
                        ? noun.Strings[0].Text
                        : $"Item {index}";

                // If the name already exists in the dictionary, increment the counter and add the suffix
                if (nameCounts.ContainsKey(name))
                {
                    nameCounts[name]++;
                    name += $" ({nameCounts[name]})";
                }
                else
                {
                    nameCounts[name] = 1;
                }

                // Add the name to the output dictionary with the SkillHash as the key
                output[item.ItemID] = name;
                index++;
            }

            return output;
        }

        private Dictionary<int, string> GetNames(ITeamParamInfo[] teamParams)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var team in teamParams)
            {
                // Determine the team name
                string name = "";

                if (TeamConfigNamesDict.ContainsKey(team.TeamConfigID))
                {
                    name += TeamConfigNamesDict[team.TeamConfigID];
                }

                name += $" ({FindParamID(team.TeamParamID)})";

                // If the name already exists in the dictionary, increment the counter and add the suffix
                if (nameCounts.ContainsKey(name))
                {
                    nameCounts[name]++;
                    name += $" ({nameCounts[name]})";
                }
                else
                {
                    nameCounts[name] = 1;
                }

                // Add the name to the output dictionary with the SkillHash as the key
                output[team.TeamParamID] = name;
                index++;
            }

            return output;
        }

        private Dictionary<int, string> GetNames(ISoccerInfo[] soccers)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var soccer in soccers)
            {
                // Determine the team name
                string name = "";

                if (TeamParamNamesDict.ContainsKey(soccer.TeamParamID))
                {
                    name += TeamParamNamesDict[soccer.TeamParamID];
                }

                name += $" ({FindSoccerID(soccer.SoccerID)})";

                // If the name already exists in the dictionary, increment the counter and add the suffix
                if (nameCounts.ContainsKey(name))
                {
                    nameCounts[name]++;
                    name += $" ({nameCounts[name]})";
                }
                else
                {
                    nameCounts[name] = 1;
                }

                // Add the name to the output dictionary with the SkillHash as the key
                output[soccer.SoccerID] = name;
                index++;
            }

            return output;
        }

        private Dictionary<int, string> GetNames(ICharaparam[] charaparams, ICharabase[] charabases)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var charaparam in charaparams)
            {
                string name = $"Player {index}";

                // Get the charabase
                ICharabase charabase = charabases.FirstOrDefault(x => x.BaseHash == charaparam.BaseHash);

                if (charabase != null)
                {
                    // Determine the item name
                    name = charabase.BaseHash == 0x00
                        ? " "
                        : CharaText.Nouns.TryGetValue(charabase.NameHash, out var noun) && noun.Strings.Count > 0
                            ? noun.Strings[0].Text
                            : $"Player {index}";
                }

                // If the name already exists in the dictionary, increment the counter and add the suffix
                if (nameCounts.ContainsKey(name))
                {
                    nameCounts[name]++;
                    name += $" ({nameCounts[name]})";
                }
                else
                {
                    nameCounts[name] = 1;
                }

                // Add the name to the output dictionary with the SkillHash as the key
                output[charaparam.ParamHash] = name;
                index++;
            }

            return output;
        }

        private Dictionary<int, string> GetNames(object[] teams)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var team in teams)
            {
                int nameID;
                int configID;

                if (team is IStoryTeamInfo storyTeam)
                {
                    nameID = storyTeam.NameID;
                    configID = storyTeam.TeamConfigID;
                }
                else if (team is IEncountTeamInfo encountTeam)
                {
                    nameID = encountTeam.NameID;
                    configID = encountTeam.TeamConfigID;
                }
                else
                {
                    continue;
                }

                string name = nameID == 0x00
                    ? $"Team {index}"
                    : TeamText.Nouns.TryGetValue(nameID, out var noun) && noun.Strings.Count > 0
                        ? noun.Strings[0].Text
                        : $"Team {index}";

                name += $" ({FindConfigID(configID)})";

                if (nameCounts.ContainsKey(name))
                {
                    nameCounts[name]++;
                    name += $" ({nameCounts[name]})";
                }
                else
                {
                    nameCounts[name] = 1;
                }

                output[configID] = name;
                index++;
            }

            return output;
        }

        private Dictionary<int, string> GetKitNames(IItemConfig[] items)
        {
            GameSupports.GameFile modelRpgBody = GameOpened.Files["modelRPGBody"];
            VirtualDirectory modelRpgBodyFolder = modelRpgBody.File.Directory.GetFolderFromFullPath(modelRpgBody.Path);

            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            foreach (string fileName in modelRpgBodyFolder.Files.Keys)
            {
                if (fileName.StartsWith("uza"))
                {
                    try
                    {
                        string kitID = fileName.Replace("uza", "").Replace("uzc", "").Replace(".xc", "");
                        int kitNumber = Convert.ToInt32(kitID);
                        kitID = "iiu" + kitID;
                        int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(kitID)));

                        string name = kitID;

                        IItemConfig kitConfig = items.FirstOrDefault(x => x.ItemID == crc32);

                        if (kitConfig != null)
                        {
                            // Determine the item name
                            name = kitConfig.NameID == 0x00
                                ? " "
                                : Itemtext.Nouns.TryGetValue(kitConfig.NameID, out var noun) && noun.Strings.Count > 0
                                    ? noun.Strings[0].Text
                                    : $"Item {kitID}";
                        }

                        // If the name already exists in the dictionary, increment the counter and add the suffix
                        if (nameCounts.ContainsKey(name))
                        {
                            nameCounts[name]++;
                            name += $" ({nameCounts[name]})";
                        }
                        else
                        {
                            nameCounts[name] = 1;
                        }

                        // Add the name to the output dictionary with the SkillHash as the key
                        output[kitNumber * 1000] = name;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file '{fileName}': {ex.Message}");
                    }
                }
            }

            return output;
        }

        private bool SameID(int id, string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return id == crc32;
        }

        private string FindSoccerID(int soccerID = 0x0)
        {
            for (int i = 0; i < 10000; i++)
            {
                string prefix = "";

                string trySoccerID = $"btl{prefix}{i.ToString().PadLeft(4, '0')}";

                if (SameID(soccerID, trySoccerID))
                {
                    return trySoccerID;
                }
            }

            // Not found
            return soccerID.ToString("X8");
        }

        private string FindParamID(int paramID = 0x0)
        {
            for (int i = 0; i < 10000; i++)
            {
                string prefix = "";

                string tryParamID = $"para_ts{prefix}{i.ToString().PadLeft(4, '0')}";
                string tryParamID2 = $"para_te{prefix}{i.ToString().PadLeft(4, '0')}";

                if (SameID(paramID, tryParamID))
                {
                    return tryParamID;
                } else if (SameID(paramID, tryParamID2))
                {
                    return tryParamID2;
                }
            }

            // Not found
            return paramID.ToString("X8");
        }

        private string FindConfigID(int configID = 0x0)
        {
            for (int i = 0; i < 10000; i++)
            {
                string prefix = "";

                string tryConfigID = $"ts{prefix}{i.ToString().PadLeft(4, '0')}";
                string tryConfigID2 = $"te{prefix}{i.ToString().PadLeft(4, '0')}";

                if (SameID(configID, tryConfigID))
                {
                    return tryConfigID;
                }
                else if (SameID(configID, tryConfigID2))
                {
                    return tryConfigID2;
                }
            }

            // Not found
            return configID.ToString("X8");
        }

        private void SetPlayerComboBox(FlatComboBox comboBox, int playerID)
        {
            if (CharaNamesDict.ContainsKey(playerID))
            {
                comboBox.SelectedIndex = comboBox.Items.IndexOf(CharaNamesDict[playerID]);
            } else
            {
                comboBox.SelectedIndex = -1;
                comboBox.SelectedText = "";
            }        
        }

        private void SetFace(FlatNumericUpDown numericUpDown)
        {
            GameSupports.GameFile faceInfo = GameOpened.Files["emblem"];

            VirtualDirectory faceFolder = faceInfo.File.Directory.GetFolderFromFullPath(faceInfo.Path);

            string faceFileName = "emb" + numericUpDown.Value.ToString().PadLeft(4, '0') + ".xi";

            if (faceFolder.Files.ContainsKey(faceFileName))
            {
                try
                {
                    byte[] imageData = faceInfo.File.Directory.GetFileFromFullPath(faceInfo.Path + "/" + faceFileName);
                    emblemPictureBox.Image = IMGC.ToBitmap(imageData);
                }
                catch
                {
                    emblemPictureBox.Image = null;
                }
            }
            else
            {
                emblemPictureBox.Image = null;
            }
        }

        private void ResetComboBox(FlatComboBox comboBox, bool disable)
        {
            comboBox.SelectedIndex = -1;
            comboBox.Text = "";

            if (disable)
            {
                comboBox.Enabled = false;
            }
        }

        private void ResetNumericUpDown(FlatNumericUpDown numericUpDown, bool disable)
        {
            numericUpDown.Value = 0;

            if (disable)
            {
                numericUpDown.Enabled = false;
            }
        }

        private void FillCombobox()
        {
            int configFlatComboBoxIndex = configFlatComboBox.SelectedIndex;
            int configFromParamFlatComboBoxIndex = configFromParamFlatComboBox.SelectedIndex;
            int paramFlatComboBoxIndex = paramFlatComboBox.SelectedIndex;
            int paramFromSoccerFlatComboBoxIndex = paramFromSoccerFlatComboBox.SelectedIndex;
            int soccerFlatComboBoxIndex = soccerFlatComboBox.SelectedIndex;

            configFlatComboBox.Items.Clear();
            configFromParamFlatComboBox.Items.Clear();
            paramFlatComboBox.Items.Clear();
            paramFromSoccerFlatComboBox.Items.Clear();
            soccerFlatComboBox.Items.Clear();

            TeamConfigNamesDict = GetNames(TeamConfigs.ToArray());
            configFlatComboBox.Items.AddRange(TeamConfigNamesDict.Values.ToArray());
            configFromParamFlatComboBox.Items.AddRange(configFlatComboBox.Items.Cast<object>().ToArray());

            TeamParamNamesDict = GetNames(TeamParams.ToArray());
            paramFlatComboBox.Items.AddRange(TeamParamNamesDict.Values.ToArray());
            paramFromSoccerFlatComboBox.Items.AddRange(paramFlatComboBox.Items.Cast<object>().ToArray());

            SoccerNamesDict = GetNames(Soccers.ToArray());
            soccerFlatComboBox.Items.AddRange(SoccerNamesDict.Values.ToArray());

            configFlatComboBox.SelectedIndex = configFlatComboBoxIndex;
            configFromParamFlatComboBox.SelectedIndex = configFromParamFlatComboBoxIndex;
            paramFlatComboBox.SelectedIndex = configFlatComboBoxIndex;
            paramFromSoccerFlatComboBox.SelectedIndex = paramFromSoccerFlatComboBoxIndex;
            soccerFlatComboBox.SelectedIndex = soccerFlatComboBoxIndex;
        }

        private void InitializeSoccersResource()
        {
            // Text
            GameSupports.GameFile skillTextGameFile = GameOpened.Files["item_text"];
            Itemtext = new T2bþ(skillTextGameFile.File.Directory.GetFileFromFullPath(skillTextGameFile.Path));
            GameSupports.GameFile teamText = GameOpened.Files["team_text"];
            TeamText = new T2bþ(teamText.File.Directory.GetFileFromFullPath(teamText.Path));
            GameSupports.GameFile charaText = GameOpened.Files["chara_text"];
            CharaText = new T2bþ(charaText.File.Directory.GetFileFromFullPath(charaText.Path));

            // Players
            ICharabase[] charabases = GameOpened.GetCharabase();
            ICharaparam[] charaparams = GameOpened.GetCharaparams();
            CharaNamesDict = GetNames(charaparams, charabases);
            CharaNamesDict.Add(0, "");
            playerFlatComboBox1.Items.AddRange(CharaNamesDict.Values.ToArray());
            playerFlatComboBox2.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox3.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox4.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox5.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox6.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox7.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox8.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox9.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox10.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox11.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox12.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox13.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox14.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox15.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());
            playerFlatComboBox16.Items.AddRange(playerFlatComboBox1.Items.Cast<Object>().ToArray());

            // Equipments
            ItemsConfigs = GameOpened.GetItems("all").ToList();
            IItemConfig[] boots = ItemsConfigs.Where(x => x.ItemCategory == (int)ItemTypes.Boots).ToArray();
            IItemConfig[] gloves = ItemsConfigs.Where(x => x.ItemCategory == (int)ItemTypes.Gloves).ToArray();
            IItemConfig[] bracelet = ItemsConfigs.Where(x => x.ItemCategory == (int)ItemTypes.Bracelet).ToArray();
            IItemConfig[] pendeant = ItemsConfigs.Where(x => x.ItemCategory == (int)ItemTypes.Pendant).ToArray();
            BootsNamesDict = GetNames(boots);
            BootsNamesDict.Add(0, "");
            GlovesNamesDict = GetNames(gloves);
            GlovesNamesDict.Add(0, "");
            BraceletNamesDict = GetNames(bracelet);
            BraceletNamesDict.Add(0, "");
            PendantNamesDict = GetNames(pendeant);
            PendantNamesDict.Add(0, "");
            bootsFlatComboBox.Items.AddRange(BootsNamesDict.Values.ToArray());
            glovesFlatComboBox.Items.AddRange(GlovesNamesDict.Values.ToArray());
            braceletFlatComboBox.Items.AddRange(BraceletNamesDict.Values.ToArray());
            pendantFlatComboBox.Items.AddRange(PendantNamesDict.Values.ToArray());

            // coach
            IItemConfig[] coaches = ItemsConfigs.Where(x => x.ItemCategory == (int)ItemTypes.Coach).ToArray();
            CoachNamesDict = GetNames(coaches);
            CoachNamesDict.Add(0, "");
            coachFlatComboBox.Items.AddRange(CoachNamesDict.Values.ToArray());

            // formation
            IItemConfig[] formations = ItemsConfigs.Where(x => x.ItemCategory == (int)ItemTypes.FormationMatch || x.ItemCategory == (int)ItemTypes.FormationMiniBattle).ToArray();
            FormationNamesDict = GetNames(formations);
            FormationNamesDict.Add(0, "");
            formationFlatComboBox.Items.AddRange(FormationNamesDict.Values.ToArray());

            // tactic
            IItemConfig[] tactics = ItemsConfigs.Where(x => x.ItemCategory == (int)ItemTypes.Tactic).ToArray();
            TacticNamesDict = GetNames(tactics);
            TacticNamesDict.Add(0, "");
            tacticFlatComboBox.Items.AddRange(TacticNamesDict.Values.ToArray());

            // Kits
            IItemConfig[] kits = ItemsConfigs.Where(x => x.ItemCategory == (int)ItemTypes.Kit).ToArray();
            KitNamesDict = GetKitNames(kits);
            KitNamesDict.Add(0, "");
            kitFlatComboBox.Items.AddRange(KitNamesDict.Values.ToArray());

            // All items
            ItemsNamesDict = GetNames(ItemsConfigs.ToArray());
            ItemsNamesDict.Add(0, "");
            dropFlatComboBox1.Items.AddRange(ItemsNamesDict.Values.ToArray());
            dropFlatComboBox2.Items.AddRange(dropFlatComboBox1.Items.Cast<Object>().ToArray());
            dropFlatComboBox3.Items.AddRange(dropFlatComboBox1.Items.Cast<Object>().ToArray());
            dropFlatComboBox4.Items.AddRange(dropFlatComboBox1.Items.Cast<Object>().ToArray());
            dropFlatComboBox5.Items.AddRange(dropFlatComboBox1.Items.Cast<Object>().ToArray());
            dropFlatComboBox6.Items.AddRange(dropFlatComboBox1.Items.Cast<Object>().ToArray());

            Soccers = GameOpened.GetSoccers().ToList();
            TeamParams = GameOpened.GetTeamParams().ToList();
            TeamConfigs = GameOpened.GetTeamConfig().ToList();

            FillCombobox();
        }

        private void SoccersWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            IStoryTeamInfo[] storyTeams = TeamConfigs.Where(x => x is IStoryTeamInfo).Cast<IStoryTeamInfo>().ToArray();
            IEncountTeamInfo[] encountTeams = TeamConfigs
                .Where(x => x is IEncountTeamInfo && !(x is IStoryTeamInfo))
                .Cast<IEncountTeamInfo>()
                .ToArray();

            GameOpened.SaveSoccers(Soccers.ToArray());
            GameOpened.SaveTeamParams(TeamParams.ToArray());
            GameOpened.SaveTeamConfig(storyTeams, encountTeams);

            if (TeamText != null)
            {
                GameOpened.SaveTextFile(GameOpened.Files["team_text"], TeamText);
            }
        }

        private void SoccerFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (soccerFlatComboBox.SelectedIndex == -1) return;

            SelectedSoccer = Soccers[soccerFlatComboBox.SelectedIndex];

            typeFlatComboBox.SelectedIndex = SelectedSoccer.SoccerMode;
            soundTextBox.Text = SelectedSoccer.Sound;
            victoryConditionFlatComboBox.SelectedIndex = SelectedSoccer.VictoryCondition;
            timeFlatNumericUpDown.Value = SelectedSoccer.Time;
            matchScriptFlatNumericUpDown.Value = SelectedSoccer.Script;
            nextScriptFlatNumericUpDown.Value = SelectedSoccer.NextScript;

            ITeamParamInfo paramInfo = TeamParams.FirstOrDefault(x => x.TeamParamID == SelectedSoccer.TeamParamID);
            if (paramInfo != null)
            {
                paramFromSoccerFlatComboBox.SelectedIndex = TeamParams.IndexOf(paramInfo);
            } else
            {
                paramFromSoccerFlatComboBox.SelectedIndex = -1;
            }          

            paramLabel.Enabled = true;
            paramFromSoccerFlatComboBox.Enabled = true;
            settingsGroupBox.Enabled = true;
            scriptGroupBox.Enabled = true;
        }

        private void ParamFromSoccerFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (paramFromSoccerFlatComboBox.SelectedIndex == -1) return;

            paramFlatComboBox.SelectedIndex = paramFromSoccerFlatComboBox.SelectedIndex;

            if (!paramFromSoccerFlatComboBox.Focused || SelectedSoccer == null) return;

            SelectedSoccer.TeamParamID = TeamParams[paramFromSoccerFlatComboBox.SelectedIndex].TeamParamID;
        }

        private void ParamFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (paramFlatComboBox.SelectedIndex == -1) return;

            SelectedTeamParam = TeamParams[paramFlatComboBox.SelectedIndex];

            teamLevelFlatNumericUpDown.Value = SelectedTeamParam.Level;
            aiFlatNumericUpDown.Value = SelectedTeamParam.AILevel;
            bootsFlatComboBox.SelectedIndex = bootsFlatComboBox.Items.IndexOf(BootsNamesDict[SelectedTeamParam.BootsID]);
            glovesFlatComboBox.SelectedIndex = glovesFlatComboBox.Items.IndexOf(GlovesNamesDict[SelectedTeamParam.GlovesID]);
            braceletFlatComboBox.SelectedIndex = braceletFlatComboBox.Items.IndexOf(BraceletNamesDict[SelectedTeamParam.BraceletID]);
            pendantFlatComboBox.SelectedIndex = pendantFlatComboBox.Items.IndexOf(PendantNamesDict[SelectedTeamParam.PendantID]);
            coachFlatComboBox.SelectedIndex = coachFlatComboBox.Items.IndexOf(CoachNamesDict[SelectedTeamParam.CoachID]);
            tacticFlatComboBox.SelectedIndex = tacticFlatComboBox.Items.IndexOf(TacticNamesDict[SelectedTeamParam.TacticID]);
            formationFlatComboBox.SelectedIndex = formationFlatComboBox.Items.IndexOf(FormationNamesDict[SelectedTeamParam.FormationID]);

            // kit
            if (KitNamesDict.ContainsKey(SelectedTeamParam.Uniform))
            {
                kitFlatComboBox.SelectedIndex = kitFlatComboBox.Items.IndexOf(KitNamesDict[SelectedTeamParam.Uniform]);
                awayColoursCheckBox.Checked = false;
            } else
            {
                // Try away colours
                int kitNumber = SelectedTeamParam.Uniform - 2;

                if (KitNamesDict.ContainsKey(kitNumber))
                {
                    kitFlatComboBox.SelectedIndex = kitFlatComboBox.Items.IndexOf(KitNamesDict[kitNumber]);
                    awayColoursCheckBox.Checked = true;
                } else
                {
                    kitFlatComboBox.SelectedIndex = kitFlatComboBox.Items.IndexOf(KitNamesDict[0x0]);
                    awayColoursCheckBox.Checked = false;
                }
            }

            prestigeFlatNumericUpDown.Value = SelectedTeamParam.Prestige;
            friendshipFlatNumericUpDown.Value = SelectedTeamParam.Friendship;
            victoryPointFlatNumericUpDown.Value = SelectedTeamParam.VictoryPoints;
            nicePlayFlatNumericUpDown.Value = SelectedTeamParam.NicePlayBonus;

            // drops
            dropFlatComboBox1.SelectedIndex = dropFlatComboBox1.Items.IndexOf(ItemsNamesDict[SelectedTeamParam.DropID1]);
            droptRateFlatNumericUpDown1.Value = SelectedTeamParam.DropRate1;
            dropFlatComboBox2.SelectedIndex = dropFlatComboBox2.Items.IndexOf(ItemsNamesDict[SelectedTeamParam.DropID2]);
            droptRateFlatNumericUpDown2.Value = SelectedTeamParam.DropRate2;
            dropFlatComboBox3.SelectedIndex = dropFlatComboBox3.Items.IndexOf(ItemsNamesDict[SelectedTeamParam.DropID3]);
            droptRateFlatNumericUpDown3.Value = SelectedTeamParam.DropRate3;
            dropFlatComboBox4.SelectedIndex = dropFlatComboBox4.Items.IndexOf(ItemsNamesDict[SelectedTeamParam.DropID4]);
            droptRateFlatNumericUpDown4.Value = SelectedTeamParam.DropRate4;
            dropFlatComboBox5.SelectedIndex = dropFlatComboBox5.Items.IndexOf(ItemsNamesDict[SelectedTeamParam.DropID5]);
            droptRateFlatNumericUpDown5.Value = SelectedTeamParam.DropRate5;
            dropFlatComboBox6.SelectedIndex = dropFlatComboBox6.Items.IndexOf(ItemsNamesDict[SelectedTeamParam.DropID6]);
            droptRateFlatNumericUpDown6.Value = SelectedTeamParam.DropRate6;

            // Team config
            var teamConfig = TeamConfigs.FirstOrDefault(x =>
                (x is IEncountTeamInfo encountTeam && encountTeam.TeamConfigID == SelectedTeamParam.TeamConfigID) ||
                (x is IStoryTeamInfo storyTeam && storyTeam.TeamConfigID == SelectedTeamParam.TeamConfigID));

            if (teamConfig != null)
            {
                configFromParamFlatComboBox.SelectedIndex = TeamConfigs.IndexOf(teamConfig);
            }
            else
            {
                paramFromSoccerFlatComboBox.SelectedIndex = -1;
            }

            configLabel.Enabled = true;
            configFromParamFlatComboBox.Enabled = true;
            levelGroupBox.Enabled = true;
            equipmentsGroupBox.Enabled = true;
            strategyGroupBox.Enabled = true;
            dropsGroupBox.Enabled = true;
        }

        private void ConfigFromParamFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (configFromParamFlatComboBox.SelectedIndex == -1) return;

            configFlatComboBox.SelectedIndex = configFromParamFlatComboBox.SelectedIndex;

            if (!configFromParamFlatComboBox.Focused || SelectedTeamConfig == null) return;

            var teamConfig = TeamConfigs[configFromParamFlatComboBox.SelectedIndex];

            if (teamConfig is IEncountTeamInfo encountTeam)
            {
                SelectedTeamParam.TeamConfigID = encountTeam.TeamConfigID;
            }
            else if (teamConfig is IStoryTeamInfo storyTeam)
            {
                SelectedTeamParam.TeamConfigID = storyTeam.TeamConfigID;
            }
        }

        private void ConfigFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (configFlatComboBox.SelectedIndex == -1) return;

            SelectedTeamConfig = TeamConfigs[configFlatComboBox.SelectedIndex];

            var interfaces = SelectedTeamConfig.GetType().GetInterfaces();
            if (interfaces.Contains(typeof(IStoryTeamInfo)) || interfaces.Contains(typeof(IEncountTeamInfo)))
            {
                IEncountTeamInfo encountTeam = SelectedTeamConfig as IEncountTeamInfo;

                if (TeamText.Nouns.ContainsKey(encountTeam.NameID))
                {
                    teamNameTextBox.Text = TeamText.Nouns[encountTeam.NameID].Strings[0].Text;
                }
                else
                {
                    teamNameTextBox.Clear();
                }

                emblemFlatNumericUpDown1.Value = encountTeam.Emblem1;
                emblemFlatNumericUpDown2.Value = encountTeam.Emblem2;
                emblemFlatNumericUpDown3.Value = encountTeam.Emblem3;
                SetPlayerComboBox(playerFlatComboBox1, encountTeam.Player1);
                SetPlayerComboBox(playerFlatComboBox2, encountTeam.Player2);
                SetPlayerComboBox(playerFlatComboBox3, encountTeam.Player3);
                SetPlayerComboBox(playerFlatComboBox4, encountTeam.Player4);
                SetPlayerComboBox(playerFlatComboBox5, encountTeam.Player5);
                addLevelFlatNumericUpDown1.Value = encountTeam.DifferenceLevelPlayer1;
                addLevelFlatNumericUpDown2.Value = encountTeam.DifferenceLevelPlayer2;
                addLevelFlatNumericUpDown3.Value = encountTeam.DifferenceLevelPlayer3;
                addLevelFlatNumericUpDown4.Value = encountTeam.DifferenceLevelPlayer4;
                addLevelFlatNumericUpDown5.Value = encountTeam.DifferenceLevelPlayer5;
                kitNumberFlatNumericUpDown1.Value = encountTeam.NumberPlayer1;
                kitNumberFlatNumericUpDown2.Value = encountTeam.NumberPlayer2;
                kitNumberFlatNumericUpDown3.Value = encountTeam.NumberPlayer3;
                kitNumberFlatNumericUpDown4.Value = encountTeam.NumberPlayer4;
                kitNumberFlatNumericUpDown5.Value = encountTeam.NumberPlayer5;
                ResetComboBox(playerFlatComboBox6, true);
                ResetComboBox(playerFlatComboBox7, true);
                ResetComboBox(playerFlatComboBox8, true);
                ResetComboBox(playerFlatComboBox9, true);
                ResetComboBox(playerFlatComboBox10, true);
                ResetComboBox(playerFlatComboBox11, true);
                ResetComboBox(playerFlatComboBox12, true);
                ResetComboBox(playerFlatComboBox13, true);
                ResetComboBox(playerFlatComboBox14, true);
                ResetComboBox(playerFlatComboBox15, true);
                ResetComboBox(playerFlatComboBox16, true);
                ResetNumericUpDown(addLevelFlatNumericUpDown6, true);
                ResetNumericUpDown(addLevelFlatNumericUpDown7, true);
                ResetNumericUpDown(addLevelFlatNumericUpDown8, true);
                ResetNumericUpDown(addLevelFlatNumericUpDown9, true);
                ResetNumericUpDown(addLevelFlatNumericUpDown10, true);
                ResetNumericUpDown(addLevelFlatNumericUpDown11, true);
                ResetNumericUpDown(addLevelFlatNumericUpDown12, true);
                ResetNumericUpDown(addLevelFlatNumericUpDown13, true);
                ResetNumericUpDown(addLevelFlatNumericUpDown14, true);
                ResetNumericUpDown(addLevelFlatNumericUpDown15, true);
                ResetNumericUpDown(addLevelFlatNumericUpDown16, true);
                ResetNumericUpDown(kitNumberFlatNumericUpDown6, true);
                ResetNumericUpDown(kitNumberFlatNumericUpDown7, true);
                ResetNumericUpDown(kitNumberFlatNumericUpDown8, true);
                ResetNumericUpDown(kitNumberFlatNumericUpDown9, true);
                ResetNumericUpDown(kitNumberFlatNumericUpDown10, true);
                ResetNumericUpDown(kitNumberFlatNumericUpDown11, true);
                ResetNumericUpDown(kitNumberFlatNumericUpDown12, true);
                ResetNumericUpDown(kitNumberFlatNumericUpDown13, true);
                ResetNumericUpDown(kitNumberFlatNumericUpDown14, true);
                ResetNumericUpDown(kitNumberFlatNumericUpDown15, true);
                ResetNumericUpDown(kitNumberFlatNumericUpDown16, true);
                teamTypeFlatComboBox.SelectedIndex = 0;

                if (SelectedTeamConfig is IStoryTeamInfo storyTeam)
                {
                    SetPlayerComboBox(playerFlatComboBox6, storyTeam.Player6);
                    SetPlayerComboBox(playerFlatComboBox7, storyTeam.Player7);
                    SetPlayerComboBox(playerFlatComboBox8, storyTeam.Player8);
                    SetPlayerComboBox(playerFlatComboBox9, storyTeam.Player9);
                    SetPlayerComboBox(playerFlatComboBox10, storyTeam.Player10);
                    SetPlayerComboBox(playerFlatComboBox11, storyTeam.Player11);
                    SetPlayerComboBox(playerFlatComboBox12, storyTeam.Player12);
                    SetPlayerComboBox(playerFlatComboBox13, storyTeam.Player13);
                    SetPlayerComboBox(playerFlatComboBox14, storyTeam.Player14);
                    SetPlayerComboBox(playerFlatComboBox15, storyTeam.Player15);
                    SetPlayerComboBox(playerFlatComboBox16, storyTeam.Player16);
                    addLevelFlatNumericUpDown6.Value = storyTeam.DifferenceLevelPlayer6;
                    addLevelFlatNumericUpDown7.Value = storyTeam.DifferenceLevelPlayer7;
                    addLevelFlatNumericUpDown8.Value = storyTeam.DifferenceLevelPlayer8;
                    addLevelFlatNumericUpDown9.Value = storyTeam.DifferenceLevelPlayer9;
                    addLevelFlatNumericUpDown10.Value = storyTeam.DifferenceLevelPlayer10;
                    addLevelFlatNumericUpDown11.Value = storyTeam.DifferenceLevelPlayer11;
                    addLevelFlatNumericUpDown12.Value = storyTeam.DifferenceLevelPlayer12;
                    addLevelFlatNumericUpDown13.Value = storyTeam.DifferenceLevelPlayer13;
                    addLevelFlatNumericUpDown14.Value = storyTeam.DifferenceLevelPlayer14;
                    addLevelFlatNumericUpDown15.Value = storyTeam.DifferenceLevelPlayer15;
                    addLevelFlatNumericUpDown16.Value = storyTeam.DifferenceLevelPlayer16;
                    kitNumberFlatNumericUpDown6.Value = storyTeam.NumberPlayer6;
                    kitNumberFlatNumericUpDown7.Value = storyTeam.NumberPlayer7;
                    kitNumberFlatNumericUpDown8.Value = storyTeam.NumberPlayer8;
                    kitNumberFlatNumericUpDown9.Value = storyTeam.NumberPlayer9;
                    kitNumberFlatNumericUpDown10.Value = storyTeam.NumberPlayer10;
                    kitNumberFlatNumericUpDown11.Value = storyTeam.NumberPlayer11;
                    kitNumberFlatNumericUpDown12.Value = storyTeam.NumberPlayer12;
                    kitNumberFlatNumericUpDown13.Value = storyTeam.NumberPlayer13;
                    kitNumberFlatNumericUpDown14.Value = storyTeam.NumberPlayer14;
                    kitNumberFlatNumericUpDown15.Value = storyTeam.NumberPlayer15;
                    kitNumberFlatNumericUpDown16.Value = storyTeam.NumberPlayer16;
                    teamTypeFlatComboBox.SelectedIndex = 1;

                    // Enable
                    playerFlatComboBox6.Enabled = true;
                    playerFlatComboBox7.Enabled = true;
                    playerFlatComboBox8.Enabled = true;
                    playerFlatComboBox9.Enabled = true;
                    playerFlatComboBox10.Enabled = true;
                    playerFlatComboBox11.Enabled = true;
                    playerFlatComboBox12.Enabled = true;
                    playerFlatComboBox13.Enabled = true;
                    playerFlatComboBox14.Enabled = true;
                    playerFlatComboBox15.Enabled = true;
                    playerFlatComboBox16.Enabled = true;
                    addLevelFlatNumericUpDown6.Enabled = true;
                    addLevelFlatNumericUpDown7.Enabled = true;
                    addLevelFlatNumericUpDown8.Enabled = true;
                    addLevelFlatNumericUpDown9.Enabled = true;
                    addLevelFlatNumericUpDown10.Enabled = true;
                    addLevelFlatNumericUpDown11.Enabled = true;
                    addLevelFlatNumericUpDown12.Enabled = true;
                    addLevelFlatNumericUpDown13.Enabled = true;
                    addLevelFlatNumericUpDown14.Enabled = true;
                    addLevelFlatNumericUpDown15.Enabled = true;
                    addLevelFlatNumericUpDown16.Enabled = true;
                    kitNumberFlatNumericUpDown6.Enabled = true;
                    kitNumberFlatNumericUpDown7.Enabled = true;
                    kitNumberFlatNumericUpDown8.Enabled = true;
                    kitNumberFlatNumericUpDown9.Enabled = true;
                    kitNumberFlatNumericUpDown10.Enabled = true;
                    kitNumberFlatNumericUpDown11.Enabled = true;
                    kitNumberFlatNumericUpDown12.Enabled = true;
                    kitNumberFlatNumericUpDown13.Enabled = true;
                    kitNumberFlatNumericUpDown14.Enabled = true;
                    kitNumberFlatNumericUpDown15.Enabled = true;
                    kitNumberFlatNumericUpDown16.Enabled = true;
                }

                teamNameLabel.Enabled = true;
                teamNameTextBox.Enabled = true;
                emblemGroupBox.Enabled = true;
                playersGroupBox.Enabled = true;
            }
        }

        private void TypeFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeFlatComboBox.SelectedIndex == -1 || !typeFlatComboBox.Focused || SelectedSoccer == null) return;

            SelectedSoccer.SoccerMode = typeFlatComboBox.SelectedIndex;
        }

        private void VictoryConditionFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (victoryConditionFlatComboBox.SelectedIndex == -1 || !victoryConditionFlatComboBox.Focused || SelectedSoccer == null) return;

            SelectedSoccer.VictoryCondition = victoryConditionFlatComboBox.SelectedIndex;
        }

        private void SoundTextBox_TextChanged(object sender, EventArgs e)
        {
            if (soundTextBox.Text != null || !soundTextBox.Focused || SelectedSoccer == null) return;

            SelectedSoccer.Sound = soundTextBox.Text;
        }

        private void TimeFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!timeFlatNumericUpDown.Focused || SelectedSoccer == null) return;

            SelectedSoccer.Time = Convert.ToInt32(timeFlatNumericUpDown.Value);
        }

        private void MatchScriptFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!matchScriptFlatNumericUpDown.Focused || SelectedSoccer == null) return;

            SelectedSoccer.Script = Convert.ToInt32(matchScriptFlatNumericUpDown.Value);
        }

        private void NextScriptFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!nextScriptFlatNumericUpDown.Focused || SelectedSoccer == null) return;

            SelectedSoccer.NextScript = Convert.ToInt32(nextScriptFlatNumericUpDown.Value);
        }

        private void TeamLevelFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!teamLevelFlatNumericUpDown.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.Level = Convert.ToInt32(teamLevelFlatNumericUpDown.Value);
        }

        private void AiFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!aiFlatNumericUpDown.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.AILevel = Convert.ToInt32(aiFlatNumericUpDown.Value);
        }

        private void BootsFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bootsFlatComboBox.SelectedIndex == -1 || !bootsFlatComboBox.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.BootsID = BootsNamesDict.ElementAt(bootsFlatComboBox.SelectedIndex).Key;
        }

        private void BraceletFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (braceletFlatComboBox.SelectedIndex == -1 || !braceletFlatComboBox.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.BraceletID = BraceletNamesDict.ElementAt(braceletFlatComboBox.SelectedIndex).Key;
        }

        private void GlovesFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (glovesFlatComboBox.SelectedIndex == -1 || !glovesFlatComboBox.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.GlovesID = GlovesNamesDict.ElementAt(glovesFlatComboBox.SelectedIndex).Key;
        }

        private void PendantFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pendantFlatComboBox.SelectedIndex == -1 || !pendantFlatComboBox.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.PendantID = PendantNamesDict.ElementAt(pendantFlatComboBox.SelectedIndex).Key;
        }

        private void CoachFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (coachFlatComboBox.SelectedIndex == -1 || !coachFlatComboBox.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.CoachID = CoachNamesDict.ElementAt(coachFlatComboBox.SelectedIndex).Key;
        }

        private void KitFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (kitFlatComboBox.SelectedIndex == -1 || !kitFlatComboBox.Focused || SelectedTeamParam == null) return;

            int kitNum = KitNamesDict.ElementAt(kitFlatComboBox.SelectedIndex).Key;

            if (awayColoursCheckBox.Checked)
            {
                kitNum += 2;
            }

            SelectedTeamParam.Uniform = kitNum;
        }

        private void AwayColoursCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!awayColoursCheckBox.Focused || SelectedTeamParam == null) return;

            int kitNum = KitNamesDict.ElementAt(kitFlatComboBox.SelectedIndex).Key;

            if (awayColoursCheckBox.Checked)
            {
                kitNum += 2;
            }

            SelectedTeamParam.Uniform = kitNum;
        }

        private void TacticFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tacticFlatComboBox.SelectedIndex == -1 || !tacticFlatComboBox.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.TacticID = TacticNamesDict.ElementAt(tacticFlatComboBox.SelectedIndex).Key;
        }

        private void FormationFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formationFlatComboBox.SelectedIndex == -1 || !formationFlatComboBox.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.FormationID = FormationNamesDict.ElementAt(formationFlatComboBox.SelectedIndex).Key;
        }

        private void FormationButton_Click(object sender, EventArgs e)
        {
            // To do
        }

        private void PrestigeFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!prestigeFlatNumericUpDown.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.Prestige = Convert.ToInt32(prestigeFlatNumericUpDown.Value);
        }

        private void VictoryPointFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!victoryPointFlatNumericUpDown.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.VictoryPoints = Convert.ToInt32(victoryPointFlatNumericUpDown.Value);
        }

        private void FriendshipFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!friendshipFlatNumericUpDown.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.Friendship = Convert.ToInt32(friendshipFlatNumericUpDown.Value);
        }

        private void NicePlayFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!nicePlayFlatNumericUpDown.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.NicePlayBonus = Convert.ToInt32(nicePlayFlatNumericUpDown.Value);
        }

        private void DropFlatComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropFlatComboBox1.SelectedIndex == -1 || !dropFlatComboBox1.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropID1 = ItemsNamesDict.ElementAt(dropFlatComboBox1.SelectedIndex).Key;
        }

        private void DroptRateFlatNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (!droptRateFlatNumericUpDown1.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropRate1 = Convert.ToInt32(droptRateFlatNumericUpDown1.Value);
        }

        private void DropFlatComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropFlatComboBox2.SelectedIndex == -1 || !dropFlatComboBox2.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropID2 = ItemsNamesDict.ElementAt(dropFlatComboBox2.SelectedIndex).Key;
        }

        private void DroptRateFlatNumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (!droptRateFlatNumericUpDown2.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropRate2 = Convert.ToInt32(droptRateFlatNumericUpDown2.Value);
        }

        private void DropFlatComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropFlatComboBox3.SelectedIndex == -1 || !dropFlatComboBox3.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropID3 = ItemsNamesDict.ElementAt(dropFlatComboBox3.SelectedIndex).Key;
        }

        private void DroptRateFlatNumericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (!droptRateFlatNumericUpDown3.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropRate3 = Convert.ToInt32(droptRateFlatNumericUpDown3.Value);
        }

        private void DropFlatComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropFlatComboBox4.SelectedIndex == -1 || !dropFlatComboBox4.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropID4 = ItemsNamesDict.ElementAt(dropFlatComboBox4.SelectedIndex).Key;
        }

        private void DroptRateFlatNumericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (!droptRateFlatNumericUpDown4.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropRate4 = Convert.ToInt32(droptRateFlatNumericUpDown4.Value);
        }

        private void DropFlatComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropFlatComboBox5.SelectedIndex == -1 || !dropFlatComboBox5.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropID5 = ItemsNamesDict.ElementAt(dropFlatComboBox5.SelectedIndex).Key;
        }

        private void DroptRateFlatNumericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (!droptRateFlatNumericUpDown5.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropRate5 = Convert.ToInt32(droptRateFlatNumericUpDown5.Value);
        }

        private void DropFlatComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropFlatComboBox6.SelectedIndex == -1 || !dropFlatComboBox6.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropID6 = ItemsNamesDict.ElementAt(dropFlatComboBox6.SelectedIndex).Key;
        }

        private void DroptRateFlatNumericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (!droptRateFlatNumericUpDown6.Focused || SelectedTeamParam == null) return;

            SelectedTeamParam.DropRate6 = Convert.ToInt32(droptRateFlatNumericUpDown6.Value);
        }

        private void TeamTypeFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (teamTypeFlatComboBox.SelectedIndex == -1 || !teamTypeFlatComboBox.Focused || SelectedTeamConfig == null) return;

            if (teamTypeFlatComboBox.SelectedIndex == 0)
            {
                if (SelectedTeamConfig is IStoryTeamInfo storyTeam)
                {
                    IEncountTeamInfo newEncounTeam = GameOpened.GetEmptyObject<IEncountTeamInfo>();

                    newEncounTeam.TeamConfigID = storyTeam.TeamConfigID;
                    newEncounTeam.NameID = storyTeam.NameID;
                    newEncounTeam.Player1 = storyTeam.Player1;
                    newEncounTeam.NumberPlayer1 = storyTeam.NumberPlayer1;
                    newEncounTeam.Player2 = storyTeam.Player2;
                    newEncounTeam.NumberPlayer2 = storyTeam.NumberPlayer2;
                    newEncounTeam.Player3 = storyTeam.Player3;
                    newEncounTeam.NumberPlayer3 = storyTeam.NumberPlayer3;
                    newEncounTeam.Player4 = storyTeam.Player4;
                    newEncounTeam.NumberPlayer4 = storyTeam.NumberPlayer4;
                    newEncounTeam.Player5 = storyTeam.Player5;
                    newEncounTeam.NumberPlayer5 = storyTeam.NumberPlayer5;
                    newEncounTeam.Emblem1 = storyTeam.Emblem1;
                    newEncounTeam.Emblem2 = storyTeam.Emblem2;
                    newEncounTeam.Emblem3 = storyTeam.Emblem3;
                    newEncounTeam.DifferenceLevelPlayer1 = storyTeam.DifferenceLevelPlayer1;
                    newEncounTeam.DifferenceLevelPlayer2 = storyTeam.DifferenceLevelPlayer2;
                    newEncounTeam.DifferenceLevelPlayer3 = storyTeam.DifferenceLevelPlayer3;
                    newEncounTeam.DifferenceLevelPlayer4 = storyTeam.DifferenceLevelPlayer4;
                    newEncounTeam.DifferenceLevelPlayer5 = storyTeam.DifferenceLevelPlayer5;

                    SelectedTeamConfig = newEncounTeam;
                    TeamConfigs[configFlatComboBox.SelectedIndex] = newEncounTeam;                    
                }
            }
            else if (teamTypeFlatComboBox.SelectedIndex == 1)
            {
                if (SelectedTeamConfig is IEncountTeamInfo encountTeam)
                {
                    IStoryTeamInfo newStoryTeam = GameOpened.GetEmptyObject<IStoryTeamInfo>();

                    newStoryTeam.TeamConfigID = encountTeam.TeamConfigID;
                    newStoryTeam.NameID = encountTeam.NameID;
                    newStoryTeam.Player1 = encountTeam.Player1;
                    newStoryTeam.NumberPlayer1 = encountTeam.NumberPlayer1;
                    newStoryTeam.Player2 = encountTeam.Player2;
                    newStoryTeam.NumberPlayer2 = encountTeam.NumberPlayer2;
                    newStoryTeam.Player3 = encountTeam.Player3;
                    newStoryTeam.NumberPlayer3 = encountTeam.NumberPlayer3;
                    newStoryTeam.Player4 = encountTeam.Player4;
                    newStoryTeam.NumberPlayer4 = encountTeam.NumberPlayer4;
                    newStoryTeam.Player5 = encountTeam.Player5;
                    newStoryTeam.NumberPlayer5 = encountTeam.NumberPlayer5;
                    newStoryTeam.Player5 = encountTeam.Player5;
                    newStoryTeam.NumberPlayer6 = 6;
                    newStoryTeam.Player6 = 0;
                    newStoryTeam.NumberPlayer7 = 7;
                    newStoryTeam.Player7 = 0;
                    newStoryTeam.NumberPlayer8 = 8;
                    newStoryTeam.Player8 = 0;
                    newStoryTeam.NumberPlayer9 = 9;
                    newStoryTeam.Player9 = 0;
                    newStoryTeam.NumberPlayer10 = 10;
                    newStoryTeam.Player10 = 0;
                    newStoryTeam.NumberPlayer11 = 11;
                    newStoryTeam.Player11 = 0;
                    newStoryTeam.NumberPlayer12 = 12;
                    newStoryTeam.Player12 = 0;
                    newStoryTeam.NumberPlayer13 = 13;
                    newStoryTeam.Player13 = 0;
                    newStoryTeam.NumberPlayer14 = 14;
                    newStoryTeam.Player14 = 0;
                    newStoryTeam.NumberPlayer15 = 15;
                    newStoryTeam.Player15 = 0;
                    newStoryTeam.NumberPlayer16 = 16;
                    newStoryTeam.Player16 = 0;
                    newStoryTeam.Emblem1 = encountTeam.Emblem1;
                    newStoryTeam.Emblem2 = encountTeam.Emblem2;
                    newStoryTeam.Emblem3 = encountTeam.Emblem3;
                    newStoryTeam.DifferenceLevelPlayer1 = encountTeam.DifferenceLevelPlayer1;
                    newStoryTeam.DifferenceLevelPlayer2 = encountTeam.DifferenceLevelPlayer2;
                    newStoryTeam.DifferenceLevelPlayer3 = encountTeam.DifferenceLevelPlayer3;
                    newStoryTeam.DifferenceLevelPlayer4 = encountTeam.DifferenceLevelPlayer4;
                    newStoryTeam.DifferenceLevelPlayer5 = encountTeam.DifferenceLevelPlayer5;
                    newStoryTeam.DifferenceLevelPlayer6 = 0;
                    newStoryTeam.DifferenceLevelPlayer7 = 0;
                    newStoryTeam.DifferenceLevelPlayer8 = 0;
                    newStoryTeam.DifferenceLevelPlayer9 = 0;
                    newStoryTeam.DifferenceLevelPlayer10 = 0;
                    newStoryTeam.DifferenceLevelPlayer11 = 0;
                    newStoryTeam.DifferenceLevelPlayer12 = 0;
                    newStoryTeam.DifferenceLevelPlayer13 = 0;
                    newStoryTeam.DifferenceLevelPlayer14 = 0;
                    newStoryTeam.DifferenceLevelPlayer15 = 0;
                    newStoryTeam.DifferenceLevelPlayer16 = 0;

                    SelectedTeamConfig = newStoryTeam;
                    TeamConfigs[configFlatComboBox.SelectedIndex] = newStoryTeam;
                }
            }

            // Reload
            configFlatComboBox.Focus();
            ConfigFlatComboBox_SelectedIndexChanged(sender, e);
        }

        private void TeamNameTextBox_Click(object sender, EventArgs e)
        {
            if (SelectedTeamConfig == null) return;

            int teamNameHash = 0;

            if (SelectedTeamConfig is IEncountTeamInfo encountTeam)
            {
                teamNameHash = encountTeam.NameID;
            }
            else if (SelectedTeamConfig is IStoryTeamInfo storyTeam)
            {
                teamNameHash = storyTeam.NameID;
            }

            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["team_text"].Path), TeamText, false, true, teamNameHash);
            nyanko.ShowDialog();
            TeamText = nyanko.T2bþFileOpened;

            // Update current name
            if (nyanko.SelectedHash != 0)
            {
                teamNameHash = nyanko.SelectedHash;
            }

            if (SelectedTeamConfig is IEncountTeamInfo encountTeam2)
            {
                encountTeam2.NameID = teamNameHash;
            }
            else if (SelectedTeamConfig is IStoryTeamInfo storyTeam2)
            {
                storyTeam2.NameID = teamNameHash;
            }

            // Get previous index
            int configFlatComboBoxSelectedIndex = configFlatComboBox.SelectedIndex;
            int configFromFlatComboBoxSelectedIndex = configFromParamFlatComboBox.SelectedIndex;
            int paramFlatComboBoxSelectedIndex = paramFlatComboBox.SelectedIndex;
            int paramFromFlatComboBoxSelectedIndex = paramFromSoccerFlatComboBox.SelectedIndex;
            int soccerFlatComboBoxSelectedIndex = soccerFlatComboBox.SelectedIndex;

            // Clear
            configFlatComboBox.Items.Clear();
            configFromParamFlatComboBox.Items.Clear();
            paramFlatComboBox.Items.Clear();
            paramFromSoccerFlatComboBox.Items.Clear();
            soccerFlatComboBox.Items.Clear();

            // Update all name
            TeamConfigNamesDict = GetNames(TeamConfigs.ToArray());
            configFlatComboBox.Items.AddRange(TeamConfigNamesDict.Values.ToArray());
            configFromParamFlatComboBox.Items.AddRange(configFlatComboBox.Items.Cast<Object>().ToArray());
            TeamParamNamesDict = GetNames(TeamParams.ToArray());
            paramFlatComboBox.Items.AddRange(TeamParamNamesDict.Values.ToArray());
            paramFromSoccerFlatComboBox.Items.AddRange(paramFlatComboBox.Items.Cast<Object>().ToArray());
            SoccerNamesDict = GetNames(Soccers.ToArray());
            soccerFlatComboBox.Items.AddRange(SoccerNamesDict.Values.ToArray());

            // Select
            configFlatComboBox.SelectedIndex = configFlatComboBoxSelectedIndex;
            configFromParamFlatComboBox.SelectedIndex = configFromFlatComboBoxSelectedIndex;
            paramFlatComboBox.SelectedIndex = paramFlatComboBoxSelectedIndex;
            paramFromSoccerFlatComboBox.SelectedIndex = paramFromFlatComboBoxSelectedIndex;
            soccerFlatComboBox.SelectedIndex = soccerFlatComboBoxSelectedIndex;
        }

        private void EmblemFlatNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            SetFace(emblemFlatNumericUpDown1);

            if (emblemFlatNumericUpDown1.Focused == false || SelectedTeamConfig == null) return;

            if (SelectedTeamConfig is IEncountTeamInfo encountTeam)
            {
                encountTeam.Emblem1 = Convert.ToInt32(emblemFlatNumericUpDown1.Value);
            } else if (SelectedTeamConfig is IStoryTeamInfo storyTeam) 
            {
                storyTeam.Emblem1 = Convert.ToInt32(emblemFlatNumericUpDown1.Value);
            }             
        }

        private void EmblemFlatNumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (emblemFlatNumericUpDown2.Focused == false || SelectedTeamConfig == null) return;

            if (SelectedTeamConfig is IEncountTeamInfo encountTeam)
            {
                encountTeam.Emblem2 = Convert.ToInt32(emblemFlatNumericUpDown2.Value);
            }
            else if (SelectedTeamConfig is IStoryTeamInfo storyTeam)
            {
                storyTeam.Emblem2 = Convert.ToInt32(emblemFlatNumericUpDown2.Value);
            }
        }

        private void EmblemFlatNumericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (emblemFlatNumericUpDown3.Focused == false || SelectedTeamConfig == null) return;

            if (SelectedTeamConfig is IEncountTeamInfo encountTeam)
            {
                encountTeam.Emblem3 = Convert.ToInt32(emblemFlatNumericUpDown3.Value);
            }
            else if (SelectedTeamConfig is IStoryTeamInfo storyTeam)
            {
                storyTeam.Emblem3 = Convert.ToInt32(emblemFlatNumericUpDown3.Value);
            }
        }

        private void PlayerFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedIndex == -1 || !comboBox.Focused || SelectedTeamConfig == null) return;

                var selectedKey = CharaNamesDict.ElementAt(comboBox.SelectedIndex).Key;

                // Dynamically extract the player number from the ComboBox name
                // Example: "playerFlatComboBox1" → Extracts "1"
                string comboBoxName = comboBox.Name;
                if (!int.TryParse(new string(comboBoxName.Where(char.IsDigit).ToArray()), out int playerNumber)) return;

                string propertyName = $"Player{playerNumber}";

                // Use reflection to get the property from the SelectedTeamConfig type
                var teamType = SelectedTeamConfig.GetType();
                var property = teamType.GetProperty(propertyName);

                // If the property exists and is writable, assign the selected key to it
                if (property != null && property.CanWrite)
                {
                    property.SetValue(SelectedTeamConfig, selectedKey);
                }
            }
        }

        private void KitNumberFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                if (!numericUpDown.Focused || SelectedTeamConfig == null) return;

                int kitNumber = Convert.ToInt32(numericUpDown.Value);

                // Dynamically extract the player number from the NumericUpDown name
                // Example: "kitNumberFlatNumericUpDown1" → Extracts "1"
                string numericUpDownName = numericUpDown.Name;
                if (!int.TryParse(new string(numericUpDownName.Where(char.IsDigit).ToArray()), out int playerNumber)) return;

                string propertyName = $"NumberPlayer{playerNumber}";

                // Use reflection to get the property from the SelectedTeamConfig type
                var teamType = SelectedTeamConfig.GetType();
                var property = teamType.GetProperty(propertyName);

                // If the property exists and is writable, assign the numeric value to it
                if (property != null && property.CanWrite)
                {
                    property.SetValue(SelectedTeamConfig, kitNumber);
                }
            }
        }

        private void AddLevelFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                if (!numericUpDown.Focused || SelectedTeamConfig == null) return;

                int differenceLevel = Convert.ToInt32(numericUpDown.Value);

                // Dynamically extract the player number from the NumericUpDown name
                // Example: "addLevelFlatNumericUpDown1" → Extracts "1"
                string numericUpDownName = numericUpDown.Name;
                if (!int.TryParse(new string(numericUpDownName.Where(char.IsDigit).ToArray()), out int playerNumber)) return;

                string propertyName = $"DifferenceLevelPlayer{playerNumber}";

                // Use reflection to get the property from the SelectedTeamConfig type
                var teamType = SelectedTeamConfig.GetType();
                var property = teamType.GetProperty(propertyName);

                // If the property exists and is writable, assign the numeric value to it
                if (property != null && property.CanWrite)
                {
                    property.SetValue(SelectedTeamConfig, differenceLevel);
                }
            }
        }

        private void DeletePlayerButton_Click(object sender, EventArgs e)
        {
            if (SelectedTeamConfig == null) return;

            playerFlatComboBox1.SelectedIndex = playerFlatComboBox1.Items.Count - 1;

            // Dynamically determine the player number from the ComboBox name (assuming "playerFlatComboBox1")
            string comboBoxName = playerFlatComboBox1.Name;
            if (!int.TryParse(new string(comboBoxName.Where(char.IsDigit).ToArray()), out int playerNumber)) return;

            string propertyName = $"Player{playerNumber}";

            // Use reflection to get the property from the SelectedTeamConfig type
            var teamType = SelectedTeamConfig.GetType();
            var property = teamType.GetProperty(propertyName);

            // If the property exists and is writable, set the selected player to the new value
            if (property != null && property.CanWrite)
            {
                var selectedKey = CharaNamesDict.ElementAt(playerFlatComboBox1.SelectedIndex).Key;
                property.SetValue(SelectedTeamConfig, selectedKey);
            }
        }

        private void NewConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSoccerWindow newSoccerWindow = new NewSoccerWindow(
                TeamConfigs
                    .OfType<IStoryTeamInfo>()
                    .Concat(TeamConfigs.OfType<IEncountTeamInfo>())
                    .Select(x => x.TeamConfigID)
                    .Distinct()
                    .ToList(),
                "config"
            );

            newSoccerWindow.ShowDialog();

            if (newSoccerWindow.SelectedHash != 0)
            {
                if (newSoccerWindow.IsStoryTeam)
                {
                    IStoryTeamInfo newStoryTeamInfo = GameOpened.GetEmptyObject<IStoryTeamInfo>();
                    newStoryTeamInfo.TeamConfigID = newSoccerWindow.SelectedHash;
                    TeamConfigs.Add(newStoryTeamInfo);
                    FillCombobox();
                } else
                {
                    IEncountTeamInfo newEncounterTeam = GameOpened.GetEmptyObject<IEncountTeamInfo>();
                    newEncounterTeam.TeamConfigID = newSoccerWindow.SelectedHash;
                    TeamConfigs.Add(newEncounterTeam);
                    FillCombobox();
                }

                configFlatComboBox.SelectedIndex = configFlatComboBox.Items.Count - 1;
            }
        }

        private void NewParamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSoccerWindow newSoccerWindow = new NewSoccerWindow(
                TeamParams
                    .Select(x => x.TeamParamID)
                    .Distinct()
                    .ToList(),
                "param"
            );

            newSoccerWindow.ShowDialog();

            if (newSoccerWindow.SelectedHash != 0)
            {
                ITeamParamInfo newTeamParam = GameOpened.GetEmptyObject<ITeamParamInfo>();
                newTeamParam.TeamParamID = newSoccerWindow.SelectedHash;
                TeamParams.Add(newTeamParam);
                FillCombobox();

                paramFlatComboBox.SelectedIndex = paramFlatComboBox.Items.Count - 1;
            }
        }

        private void NewSoccerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSoccerWindow newSoccerWindow = new NewSoccerWindow(
                Soccers
                    .Select(x => x.SoccerID)
                    .Distinct()
                    .ToList(),
                "soccer"
            );

            newSoccerWindow.ShowDialog();

            if (newSoccerWindow.SelectedHash != 0)
            {
                ISoccerInfo newSoccer = GameOpened.GetEmptyObject<ISoccerInfo>();
                newSoccer.SoccerID = newSoccerWindow.SelectedHash;
                Soccers.Add(newSoccer);
                FillCombobox();

                soccerFlatComboBox.SelectedIndex = soccerFlatComboBox.Items.Count - 1;
            }
        }
    }
}
