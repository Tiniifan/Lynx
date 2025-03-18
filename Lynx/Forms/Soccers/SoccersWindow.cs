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

namespace Lynx.Forms.Soccers
{
    public partial class SoccersWindow : Form
    {
        private IGame GameOpened;

        private T2bþ Itemtext;

        private T2bþ TeamText;

        private List<IItemConfig> ItemsConfigs;

        private List<ISoccerInfo> Soccers;

        private List<ITeamParamInfo> TeamParams;

        private List<IEncountTeamInfo> EncountTeamInfos;

        private List<IStoryTeamInfo> StoryTeamInfos;

        private Dictionary<int, string> BootsNamesDict;

        private Dictionary<int, string> GlovesNamesDict;

        private Dictionary<int, string> BraceletNamesDict;

        private Dictionary<int, string> PendantNamesDict;

        private Dictionary<int, string> FormationNamesDict;

        private Dictionary<int, string> TacticNamesDict;

        private Dictionary<int, string> CoachNamesDict;

        private Dictionary<int, string> ItemsNamesDict;

        private Dictionary<int, string> SoccerNamesDict;

        private Dictionary<int, string> TeamParamNamesDict;

        private Dictionary<int, string> TeamConfigNamesDict;

        private ISoccerInfo SelectedSoccer;

        private ITeamParamInfo SelectedTeamParam;

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

        private Dictionary<int, string> GetNames(IEncountTeamInfo[] encountTeams, IStoryTeamInfo[] storyTeams)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            void ProcessTeams<T>(T[] teams, Func<T, int> getNameID, Func<T, int> getConfigID)
            {
                int index = 0;
                foreach (var team in teams)
                {
                    int nameID = getNameID(team);
                    int configID = getConfigID(team);

                    string name = nameID == 0x00
                        ? " "
                        : TeamText.Nouns.TryGetValue(nameID, out var noun) && noun.Strings.Count > 0
                            ? noun.Strings[0].Text
                            : $"Encount Team {index}";

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
            }

            ProcessTeams(storyTeams, team => team.NameID, team => team.TeamConfigID);
            ProcessTeams(encountTeams, team => team.NameID, team => team.TeamConfigID);           

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

        private void InitializeSoccersResource()
        {
            // Text
            GameSupports.GameFile skillTextGameFile = GameOpened.Files["item_text"];
            Itemtext = new T2bþ(skillTextGameFile.File.Directory.GetFileFromFullPath(skillTextGameFile.Path));
            GameSupports.GameFile teamText = GameOpened.Files["team_text"];
            TeamText = new T2bþ(teamText.File.Directory.GetFileFromFullPath(teamText.Path));

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
            StoryTeamInfos = GameOpened.GetStoryTeams().ToList();
            EncountTeamInfos = GameOpened.GetEncounterTeams().ToList();

            TeamConfigNamesDict = GetNames(EncountTeamInfos.ToArray(), StoryTeamInfos.ToArray());
            configFlatComboBox.Items.AddRange(TeamConfigNamesDict.Values.ToArray());

            TeamParamNamesDict = GetNames(TeamParams.ToArray());
            paramFlatComboBox.Items.AddRange(TeamParamNamesDict.Values.ToArray());

            SoccerNamesDict = GetNames(Soccers.ToArray());
            soccerFlatComboBox.Items.AddRange(SoccerNamesDict.Values.ToArray());
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
            settingsGroupBox.Enabled = true;
            scriptGroupBox.Enabled = true;
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

            levelGroupBox.Enabled = true;
            equipmentsGroupBox.Enabled = true;
            strategyGroupBox.Enabled = true;
            dropsGroupBox.Enabled = true;
        }

        private void paramFlatComboBox_TextChanged(object sender, EventArgs e)
        {
            string textToFind = paramFlatComboBox.Text;
            if (string.IsNullOrEmpty(textToFind)) return;

            // Recherche par "contains" au lieu de "starts with"
            var foundIndex = -1;
            for (int i = 0; i < paramFlatComboBox.Items.Count; i++)
            {
                string currentItem = paramFlatComboBox.Items[i].ToString();
                if (currentItem.IndexOf(textToFind, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    foundIndex = i;
                    break;
                }
            }

            // Si un élément est trouvé, le sélectionner
            if (foundIndex >= 0)
            {
                //paramFlatComboBox.SelectedIndex = foundIndex;
                // Pour positionner le curseur à la fin du texte saisi
                paramFlatComboBox.SelectionStart = textToFind.Length;
                paramFlatComboBox.SelectionLength = paramFlatComboBox.Text.Length - textToFind.Length;
            }
        }
    }
}
