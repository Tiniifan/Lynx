using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Lynx.Tools;
using Lynx.Level5.Text;
using Lynx.Level5.Image;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;
using Lynx.InazumaEleven.Common;
using Lynx.InazumaEleven.Games.GO;
using static Lynx.InazumaEleven.Games.GO.GOSupport;
using Lynx.Level5.Save.Logic;
using OfficeOpenXml;

namespace Lynx.Forms.FightingSpirits
{
    public partial class FightingSpiritWindow : Form
    {
        private IGame GameOpened;

        private List<IAvatar> Avatars;

        private List<IAvatar> AvatarsFiltred;

        private IAvatar SelectedAvatar;

        private List<IAvatarTimeGrowth> AvatarTimeGrowths;

        private List<ISkillConfig> SkillConfigs;

        private Dictionary<int, string> SkillNamesDict;

        private Dictionary<int, string> AvatarNamesDict;

        private T2bþ Skillnames;

        private T2bþ Itemtext;

        private bool LevelGrowthIsUpdating = false;

        private bool SameSkillID(int id, string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return id == crc32;
        }

        private string FindAvatarID(int avatarID = 0x0)
        {
            if (avatarID == 0x0)
            {
                avatarID = SelectedAvatar.AvatarHash;
            }

            for (int i = 0; i < 10000; i++)
            {
                string prefix = "";

                string tryAvatarID = $"ck{prefix}{i.ToString().PadLeft(4, '0')}";

                if (SameSkillID(avatarID, tryAvatarID))
                {
                    return tryAvatarID;
                }
            }

            // Not found
            return SelectedAvatar.AvatarHash.ToString("X8");
        }
        private Dictionary<int, string> GetNames(IAvatar[] avatars)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var avatar in avatars)
            {
                // Determine the skill name
                string name = avatar.AvatarHash == 0x00
                    ? " "
                    : Itemtext.Nouns.TryGetValue(avatar.NicknameHash, out var noun) && noun.Strings.Count > 0
                        ? noun.Strings[0].Text
                        : $"Avatar {index}";

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
                output[avatar.AvatarHash] = name;
                index++;
            }

            return output;
        }

        private Dictionary<int, string> GetNames(ISkillConfig[] skills)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var skill in skills)
            {
                // Determine the skill name
                string name = skill.SkillHash == 0x00
                    ? " "
                    : Skillnames.Nouns.TryGetValue(skill.NameHash, out var noun) && noun.Strings.Count > 0
                        ? noun.Strings[0].Text
                        : $"Skill {index}";

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
                output[skill.SkillHash] = name;
                index++;
            }

            return output;
        }

        private void FillDataGridView()
        {
            levelGrowthTableDataGridView.Rows.Clear();

            for (int i = 0; i < AvatarTimeGrowths.Count; i++)
            {
                IAvatarTimeGrowth avatarTimeGrowth = AvatarTimeGrowths[i];
                levelGrowthTableDataGridView.Rows.Add(
                    $"{i}", 
                    avatarTimeGrowth.Level1, 
                    avatarTimeGrowth.Level2, 
                    avatarTimeGrowth.Level3, 
                    avatarTimeGrowth.Level4,
                    avatarTimeGrowth.Level5,
                    avatarTimeGrowth.Level6
                );
            }
        }

        private void SetAvatarNames()
        {
            if (AvatarNamesDict != null)
            {
                AvatarNamesDict.Clear();
            }
            
            avatarListBox.Items.Clear();
            targetFlatComboBox.Items.Clear();
            material1FlatComboBox.Items.Clear();
            material2FlatComboBox.Items.Clear();

            AvatarNamesDict = GetNames(Avatars.ToArray());
            avatarListBox.Items.AddRange(AvatarNamesDict.Where(x => x.Key != 0x0).Select(x => x.Value).ToArray());
            targetFlatComboBox.Items.AddRange(AvatarNamesDict.Select(x => x.Value).ToArray());
            material1FlatComboBox.Items.AddRange(targetFlatComboBox.Items.Cast<Object>().ToArray());
            material2FlatComboBox.Items.AddRange(targetFlatComboBox.Items.Cast<Object>().ToArray());
        }

        private void UpdateStat()
        {
            int[] fgTable = new int[] { 0, 0, 0, 0, 0 };
            int[] powerTable = new int[] { 0, 0, 0, 0, 0 };

            if (statGrowthflatComboBox.SelectedIndex > 0 && levelGrowthFlatComboBox.SelectedIndex > 0)
            {
                fgTable = AvatarGrowthStats.IEGO[statGrowthflatComboBox.SelectedIndex][levelGrowthFlatComboBox.SelectedIndex].FG.ToArray();
                powerTable = AvatarGrowthStats.IEGO[statGrowthflatComboBox.SelectedIndex][levelGrowthFlatComboBox.SelectedIndex].Attack.ToArray();
            }

            fg2FlatNumericUpDown.Value = fgFlatNumericUpDown.Value + fgTable[0];
            fg3FlatNumericUpDown.Value = fg2FlatNumericUpDown.Value + fgTable[1];
            fg4FlatNumericUpDown.Value = fg3FlatNumericUpDown.Value + fgTable[2];
            fg5FlatNumericUpDown.Value = fg4FlatNumericUpDown.Value + fgTable[3];
            fg6FlatNumericUpDown.Value = fg5FlatNumericUpDown.Value + fgTable[4];

            power2FlatNumericUpDown.Value = powerFlatNumericUpDown.Value + powerTable[0];
            power3FlatNumericUpDown.Value = power2FlatNumericUpDown.Value + powerTable[1];
            power4FlatNumericUpDown.Value = power3FlatNumericUpDown.Value + powerTable[2];
            power5FlatNumericUpDown.Value = power4FlatNumericUpDown.Value + powerTable[3];
            power6FlatNumericUpDown.Value = power5FlatNumericUpDown.Value + powerTable[4];
        }

        private string GetText(int nameID, bool isNoun)
        {
            if (isNoun)
            {
                if (Skillnames.Nouns.ContainsKey(nameID) && Skillnames.Nouns[nameID].Strings[0].Text != null)
                {
                    return Skillnames.Nouns[nameID].Strings[0].Text;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                if (Skillnames.Texts.ContainsKey(nameID) && Skillnames.Texts[nameID].Strings[0].Text != null)
                {
                    return Skillnames.Texts[nameID].Strings[0].Text.Replace("\\n", Environment.NewLine);
                }
                else
                {
                    return "";
                }
            }
        }

        public void ExportToExcel(string filePath)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                // Create a new sheet
                var worksheet = package.Workbook.Worksheets.Add("Fighting Spirits");

                // Add headers
                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "ID";
                worksheet.Cells[1, 3].Value = "Position";
                worksheet.Cells[1, 4].Value = "Element";
                worksheet.Cells[1, 5].Value = "Special Move";
                worksheet.Cells[1, 6].Value = "Skill";
                worksheet.Cells[1, 7].Value = "Use time Growth";
                worksheet.Cells[1, 8].Value = "FSP (1)";
                worksheet.Cells[1, 9].Value = "FSP (2)";
                worksheet.Cells[1, 10].Value = "FSP (3)";
                worksheet.Cells[1, 11].Value = "FSP (4)";
                worksheet.Cells[1, 12].Value = "FSP (5)";
                worksheet.Cells[1, 13].Value = "FSP (Ω)";
                worksheet.Cells[1, 14].Value = "Attack (1)";
                worksheet.Cells[1, 15].Value = "Attack (2)";
                worksheet.Cells[1, 16].Value = "Attack (3)";
                worksheet.Cells[1, 17].Value = "Attack (4)";
                worksheet.Cells[1, 18].Value = "Attack (5)";
                worksheet.Cells[1, 19].Value = "Attack (Ω)";

                // Fill the rows with data
                int row = 2;
                foreach (IAvatar avatar in Avatars)
                {
                    worksheet.Cells[row, 1].Value = GetText(avatar.NicknameHash, true);
                    worksheet.Cells[row, 2].Value = FindAvatarID(avatar.AvatarHash);
                    worksheet.Cells[row, 3].Value = positonFlatComboBox.Items[avatar.Position].ToString();
                    worksheet.Cells[row, 4].Value = elementFlatComboBox.Items[avatar.Element].ToString();
                    //worksheet.Cells[row, 5].Value = player.Charaparam.FP;
                    //worksheet.Cells[row, 6].Value = player.Charaparam.TP;
                    //worksheet.Cells[row, 7].Value = player.Charaparam.Kick;
                    //worksheet.Cells[row, 8].Value = player.Charaparam.Dribble;
                    //worksheet.Cells[row, 9].Value = player.Charaparam.Technique;
                    //worksheet.Cells[row, 10].Value = player.Charaparam.Block;
                    //worksheet.Cells[row, 11].Value = player.Charaparam.Speed;
                    //worksheet.Cells[row, 12].Value = player.Charaparam.Stamina;
                    //worksheet.Cells[row, 13].Value = player.Charaparam.Catch;
                    //worksheet.Cells[row, 14].Value = player.Charaparam.Luck;
                    //worksheet.Cells[row, 15].Value = player.Charaparam.Freedom;

                    //if (player.Charaparam.FightingSpiritHash != 0x00)
                    //{
                        //worksheet.Cells[row, 16].Value = FightingSpiritNames[player.Charaparam.FightingSpiritHash].ToString();
                    //}
                    //else
                    //{
                        //worksheet.Cells[row, 16].Value = "";
                    //}

                    //for (int i = 0; i < 6; i++)
                    //{
                        //if (i < player.SpecialMoves.Count())
                        //{
                            //var skill = SkillConfigs.Find(x => x.SkillHash == player.SpecialMoves[i].SkillHash);

                            //if (skill != null && SkillnamesDict.ContainsKey(skill.SkillHash))
                            //{
                                //if (skill.SkillHash != 0x00)
                                //{
                                    //worksheet.Cells[row, 17 + i].Value = $"{SkillnamesDict[skill.SkillHash]} ({player.SpecialMoves[i].LevelLearned})";
                                //}
                                //else
                                //{
                                    //worksheet.Cells[row, 17 + i].Value = $"";
                                //}

                            //}
                        //}
                        //else
                        //{
                            //var defaultSkill = SkillConfigs.Find(x => x.SkillHash == 0x00);

                            //if (defaultSkill != null && SkillnamesDict.ContainsKey(defaultSkill.SkillHash))
                            //{
                                //worksheet.Cells[row, 17 + i].Value = $"{moveFlatComboBox1.Items.IndexOf(SkillnamesDict[defaultSkill.SkillHash])}";
                            //}
                        //}
                    //}

                    // Color the row based on the element
                    var range = worksheet.Cells[row, 4, row, 4];  // The entire row, from column 4 to 4

                    switch (avatar.Element)
                    {
                        case 1:
                            range.Style.Font.Color.SetColor(System.Drawing.Color.Blue);  // Blue text
                            break;
                        case 2:
                            range.Style.Font.Color.SetColor(System.Drawing.Color.Green);  // Green text
                            break;
                        case 3:
                            range.Style.Font.Color.SetColor(System.Drawing.Color.Red);  // Red text
                            break;
                        case 4:
                            range.Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(255, 204, 0));  // Dark yellow text
                            break;
                        case 5:
                            range.Style.Font.Color.SetColor(System.Drawing.Color.Purple);  // Purple text
                            break;
                        default:
                            range.Style.Font.Color.SetColor(System.Drawing.Color.Gray);  // Gray text
                            break;
                    }

                    row++;
                }

                for (int col = 1; col <= 22; col++)
                {
                    worksheet.Column(col).AutoFit();
                }

                // Save the Excel file
                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);
            }

            MessageBox.Show($"Saved on {Path.GetFileName(filePath)}");
        }

        private void InitializeAvatarResource()
        {
            GameSupports.GameFile skillTextGameFile = GameOpened.Files["item_text"];
            Itemtext = new T2bþ(skillTextGameFile.File.Directory.GetFileFromFullPath(skillTextGameFile.Path));
            GameSupports.GameFile skillText = GameOpened.Files["skill_text"];
            Skillnames = new T2bþ(skillText.File.Directory.GetFileFromFullPath(skillText.Path));

            positonFlatComboBox.Items.AddRange(EnumHelper.GetValues<PlayerPositions>().Select(s => s.Name).ToArray());
            elementFlatComboBox.Items.AddRange(EnumHelper.GetValues<Elements>().Select(s => s.Name).ToArray());
            levelGrowthFlatComboBox.Items.AddRange(EnumHelper.GetValues<GrowingTimes>().Select(s => s.Name).ToArray());

            SkillConfigs = GameOpened.GetSkillConfigs(true).ToList();
            SkillNamesDict = GetNames(SkillConfigs.ToArray());
            skillFlatComboBox.Items.AddRange(SkillNamesDict.Select(x => x.Value).ToArray());
            specialMoveFlatComboBox.Items.AddRange(skillFlatComboBox.Items.Cast<Object>().ToArray());

            Avatars = GameOpened.GetAvatars(true).ToList();
            SetAvatarNames();

            AvatarTimeGrowths = GameOpened.GetAvatarGrowthTable().ToList();
            FillDataGridView();
        }

        public FightingSpiritWindow(IGame game)
        {
            GameOpened = game;
            InitializeComponent();
            InitializeAvatarResource();
        }

        private void FightingSpiritWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            GameOpened.SaveAvatars(Avatars.ToArray());
            //GameOpened.SaveAvatarGrowthTable(AvatarTimeGrowths.ToArray());
            GameOpened.SaveTextFile(GameOpened.Files["item_text"], Itemtext);
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!searchTextBox.Focused || searchTextBox.Enabled == false || searchTextBox.Text == "Search...") return;

            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                SetAvatarNames();
                AvatarsFiltred = null;
            }
            else
            {
                AvatarsFiltred = Avatars
                    .Where(avatar =>
                    {
                        bool searchByAvatarHash = ("0x" + avatar.AvatarHash.ToString("X8").ToLower()).Contains(searchTextBox.Text.ToLower());
                        bool searchByFullName = Itemtext.Nouns.ContainsKey(avatar.FullNameHash) && Itemtext.Nouns[avatar.FullNameHash].Strings.Any(s => s.Text.ToLower().Contains(searchTextBox.Text.ToLower()));
                        bool searchByNickname = Itemtext.Nouns.ContainsKey(avatar.NicknameHash) && Itemtext.Nouns[avatar.NicknameHash].Strings.Any(s => s.Text.ToLower().Contains(searchTextBox.Text.ToLower()));
                        bool searchBySpecialMove = SkillNamesDict.ContainsKey(avatar.SpecialMoveID) && SkillNamesDict[avatar.SpecialMoveID].ToLower().Contains(searchTextBox.Text.ToLower());
                        bool searchBySkill = SkillNamesDict.ContainsKey(avatar.SkillID) && SkillNamesDict[avatar.SkillID].ToLower().Contains(searchTextBox.Text.ToLower());

                        return searchByAvatarHash || searchByFullName || searchByNickname || searchBySpecialMove || searchBySkill;
                    })
                    .ToList();

                string[] names = GetNames(AvatarsFiltred.ToArray()).Select(x => x.Value).ToArray();

                string focusedText = avatarListBox.Text;

                avatarListBox.Items.Clear();
                avatarListBox.Items.AddRange(names);

                if (names.Contains(focusedText))
                {
                    avatarListBox.SelectedIndex = Array.IndexOf(names, focusedText);
                }
            }
        }

        private void SearchTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (searchTextBox.Text != "Search...") return;

            this.Focus();
            searchTextBox.Enabled = false;
            searchTextBox.Text = "";
            searchTextBox.Enabled = true;
            searchTextBox.Focus();
        }

        private void SearchTextBox_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchTextBox.Text)) return;

            searchTextBox.Enabled = false;
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                SetAvatarNames();
                AvatarsFiltred = null;
            }
            searchTextBox.Text = "Search...";
            searchTextBox.Enabled = true;
        }

        private void AvatarListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AvatarsFiltred != null && AvatarsFiltred.Count > 0)
            {
                SelectedAvatar = AvatarsFiltred[avatarListBox.SelectedIndex];
            }
            else
            {
                SelectedAvatar = Avatars[avatarListBox.SelectedIndex];
            }

            idTextBox.Text = FindAvatarID();
            nameTextBox.Text = avatarListBox.SelectedItem.ToString();
            numberFlatNumericUpDown.Value = SelectedAvatar.AvatarNumber;

            if (Itemtext.Nouns.ContainsKey(SelectedAvatar.FullNameHash))
            {
                fullNameTextBox.Text = Itemtext.Nouns[SelectedAvatar.FullNameHash].Strings[0].Text;
            }
            else
            {
                fullNameTextBox.Clear();
            }

            if (Itemtext.Texts.ContainsKey(SelectedAvatar.DescriptionHash))
            {
                descriptionTextBox.Text = Itemtext.Texts[SelectedAvatar.DescriptionHash].Strings[0].Text;
            }
            else
            {
                descriptionTextBox.Clear();
            }

            canBeSoldCheckBox.Checked = Convert.ToBoolean(SelectedAvatar.CanBeSold);
            soldPriceFlatNumericUpDown.Value = SelectedAvatar.SellingPrice;
            canBeBoughtCheckBox.Checked = Convert.ToBoolean(SelectedAvatar.CanBeBought);
            boughtPriceFlatNumericUpDown.Value = SelectedAvatar.PurchasePrice;
            maxQuantityFlatNumericUpDown.Value = SelectedAvatar.MaxQuantity;
            xPositionFlatNumericUpDown.Value = SelectedAvatar.ItemPosX;
            yPositionFlatNumericUpDown.Value = SelectedAvatar.ItemPosY;

            positonFlatComboBox.SelectedIndex = SelectedAvatar.Position;
            elementFlatComboBox.SelectedIndex = SelectedAvatar.Element;

            var skill = SkillConfigs.Find(x => x.SkillHash == SelectedAvatar.SkillID);
            if (skill != null && SkillNamesDict.ContainsKey(skill.SkillHash))
            {
                skillFlatComboBox.SelectedIndex = skillFlatComboBox.Items.IndexOf(SkillNamesDict[skill.SkillHash]);
            } else
            {
                skillFlatComboBox.SelectedIndex = skillFlatComboBox.Items.IndexOf(SkillNamesDict[0x0]);
            }

            var specialMoveID = SkillConfigs.Find(x => x.SkillHash == SelectedAvatar.SpecialMoveID);
            if (specialMoveID != null && SkillNamesDict.ContainsKey(specialMoveID.SkillHash))
            {
                specialMoveFlatComboBox.SelectedIndex = specialMoveFlatComboBox.Items.IndexOf(SkillNamesDict[specialMoveID.SkillHash]);
            } else
            {
                specialMoveFlatComboBox.SelectedIndex = skillFlatComboBox.Items.IndexOf(SkillNamesDict[0x0]);
            }

            fgFlatNumericUpDown.Value = SelectedAvatar.FightingSpiritPoint;
            powerFlatNumericUpDown.Value = SelectedAvatar.Attack;
            levelGrowthFlatNumericUpDown.Value = SelectedAvatar.EvolutionGrow;

            if (SelectedAvatar.EvolutionStatGrow + 1 > statGrowthflatComboBox.Items.Count)
            {
                statGrowthflatComboBox.SelectedIndex = 0;
            } else
            {
                statGrowthflatComboBox.SelectedIndex = SelectedAvatar.EvolutionStatGrow;
            }

            var target = Avatars.Find(x => x.AvatarHash == SelectedAvatar.FusionID);
            if (target != null && AvatarNamesDict.ContainsKey(target.AvatarHash))
            {
                targetFlatComboBox.SelectedIndex = targetFlatComboBox.Items.IndexOf(AvatarNamesDict[target.AvatarHash]);
            }
            else
            {
                targetFlatComboBox.SelectedIndex = targetFlatComboBox.Items.IndexOf(AvatarNamesDict[0x0]);
            }

            var material1 = Avatars.Find(x => x.AvatarHash == SelectedAvatar.Partner1FusionID);
            if (material1 != null && AvatarNamesDict.ContainsKey(target.AvatarHash))
            {
                material1FlatComboBox.SelectedIndex = material1FlatComboBox.Items.IndexOf(AvatarNamesDict[material1.AvatarHash]);
            }
            else
            {
                material1FlatComboBox.SelectedIndex = material1FlatComboBox.Items.IndexOf(AvatarNamesDict[0x0]);
            }

            var material2 = Avatars.Find(x => x.AvatarHash == SelectedAvatar.Partner2FusionID);
            if (material1 != null && AvatarNamesDict.ContainsKey(target.AvatarHash))
            {
                material2FlatComboBox.SelectedIndex = material2FlatComboBox.Items.IndexOf(AvatarNamesDict[material2.AvatarHash]);
            }
            else
            {
                material2FlatComboBox.SelectedIndex = material2FlatComboBox.Items.IndexOf(AvatarNamesDict[0x0]);
            }

            fightingSpiritGroupBox.Enabled = true;
        }

        private void NumberFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            GameSupports.GameFile faceInfo = GameOpened.Files["faceAvatar"];

            VirtualDirectory faceFolder = faceInfo.File.Directory.GetFolderFromFullPath(faceInfo.Path);

            string faceFileName = "ck" + numberFlatNumericUpDown.Value.ToString().PadLeft(4, '0') + "a.xi";

            if (faceFolder.Files.ContainsKey(faceFileName))
            {
                try
                {
                    byte[] imageData = faceInfo.File.Directory.GetFileFromFullPath(faceInfo.Path + "/" + faceFileName);
                    facePictureBox.Image = IMGC.ToBitmap(imageData);
                }
                catch
                {
                    facePictureBox.Image = null;
                }
            }
            else
            {
                facePictureBox.Image = null;
            }

            if (!numberFlatNumericUpDown.Focused) return;
            SelectedAvatar.AvatarNumber = Convert.ToInt32(numberFlatNumericUpDown.Value);
        }

        private void CanBeSoldCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            soldPriceFlatNumericUpDown.Enabled = canBeSoldCheckBox.Checked;

            if (!canBeSoldCheckBox.Focused) return;
            SelectedAvatar.CanBeSold = Convert.ToInt32(canBeSoldCheckBox.Checked);
        }

        private void CanBeBoughtCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            boughtPriceFlatNumericUpDown.Enabled = canBeBoughtCheckBox.Checked;

            if (!canBeBoughtCheckBox.Focused) return;
            SelectedAvatar.CanBeBought = Convert.ToInt32(canBeBoughtCheckBox.Checked);
        }

        private void LevelGrowthFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (LevelGrowthIsUpdating) return;

            try
            {
                LevelGrowthIsUpdating = true;

                if (levelGrowthFlatComboBox.Items.Count > levelGrowthFlatNumericUpDown.Value)
                {
                    levelGrowthFlatComboBox.SelectedIndex = Convert.ToInt32(levelGrowthFlatNumericUpDown.Value);
                }
                else
                {
                    levelGrowthFlatComboBox.SelectedIndex = -1;
                }
            }
            finally
            {
                LevelGrowthIsUpdating = false;
            }

            if (!levelGrowthFlatNumericUpDown.Focused) return;

            SelectedAvatar.EvolutionGrow = Convert.ToInt32(levelGrowthFlatNumericUpDown.Value);
        }

        private void LevelGrowthFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LevelGrowthIsUpdating) return;

            try
            {
                LevelGrowthIsUpdating = true;

                if (levelGrowthFlatComboBox.SelectedIndex != -1)
                {
                    levelGrowthFlatNumericUpDown.Value = levelGrowthFlatComboBox.SelectedIndex;
                }
            }
            finally
            {
                LevelGrowthIsUpdating = false;
            }

            if (!levelGrowthFlatComboBox.Focused || levelGrowthFlatComboBox.SelectedIndex == -1) return;

            SelectedAvatar.EvolutionGrow = levelGrowthFlatComboBox.SelectedIndex;
        }

        private void StatGrowthflatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStat();

            if (!statGrowthflatComboBox.Focused || statGrowthflatComboBox.SelectedIndex == -1) return;

            SelectedAvatar.EvolutionStatGrow = statGrowthflatComboBox.SelectedIndex;
        }

        private void NameTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["item_text"].Path), Itemtext, false, true, SelectedAvatar.NicknameHash);
            nyanko.ShowDialog();
            Itemtext = nyanko.T2bþFileOpened;

            // Update current name
            if (nyanko.SelectedHash != 0)
            {
                SelectedAvatar.NicknameHash = nyanko.SelectedHash;
            }

            // Update all name
            int selectedIndex = avatarListBox.SelectedIndex;
            SetAvatarNames();
            avatarListBox.SelectedIndex = selectedIndex;
        }

        private void FullNameTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["item_text"].Path), Itemtext, false, true, SelectedAvatar.FullNameHash);
            nyanko.ShowDialog();
            Itemtext = nyanko.T2bþFileOpened;

            // Update current nickname
            if (nyanko.SelectedHash != 0)
            {
                SelectedAvatar.FullNameHash = nyanko.SelectedHash;

                if (Itemtext.Nouns.ContainsKey(SelectedAvatar.FullNameHash))
                {
                    fullNameTextBox.Text = Itemtext.Nouns[SelectedAvatar.FullNameHash].Strings[0].Text;
                }
                else
                {
                    fullNameTextBox.Clear();
                }
            }
        }

        private void SoldPriceFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!soldPriceFlatNumericUpDown.Focused) return;

            SelectedAvatar.SellingPrice = Convert.ToInt32(soldPriceFlatNumericUpDown.Value);
        }

        private void BoughtPriceFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!boughtPriceFlatNumericUpDown.Focused) return;

            SelectedAvatar.PurchasePrice = Convert.ToInt32(boughtPriceFlatNumericUpDown.Value);
        }

        private void MaxQuantityFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!maxQuantityFlatNumericUpDown.Focused) return;

            SelectedAvatar.MaxQuantity = Convert.ToInt32(maxQuantityFlatNumericUpDown.Value);
        }

        private void XPositionFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!xPositionFlatNumericUpDown.Focused) return;

            SelectedAvatar.ItemPosX = Convert.ToInt32(xPositionFlatNumericUpDown.Value);
        }

        private void YPositionFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!yPositionFlatNumericUpDown.Focused) return;

            SelectedAvatar.ItemPosY = Convert.ToInt32(yPositionFlatNumericUpDown.Value);
        }

        private void PositonFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!positonFlatComboBox.Focused || positonFlatComboBox.SelectedIndex == -1) return;

            SelectedAvatar.Position = positonFlatComboBox.SelectedIndex;
        }

        private void ElementFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!elementFlatComboBox.Focused || elementFlatComboBox.SelectedIndex == -1) return;

            SelectedAvatar.Element = elementFlatComboBox.SelectedIndex;
        }

        private void SkillFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!skillFlatComboBox.Focused || skillFlatComboBox.SelectedIndex == -1) return;

            KeyValuePair<int, string>? findMove = SkillNamesDict.FirstOrDefault(x => x.Value == skillFlatComboBox.SelectedItem.ToString());

            if (findMove.HasValue)
            {
                SelectedAvatar.SkillID = findMove.Value.Key;
            }
        }

        private void SpecialMoveFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!specialMoveFlatComboBox.Focused || specialMoveFlatComboBox.SelectedIndex == -1) return;

            KeyValuePair<int, string>? findMove = SkillNamesDict.FirstOrDefault(x => x.Value == specialMoveFlatComboBox.SelectedItem.ToString());

            if (findMove.HasValue)
            {
                SelectedAvatar.SpecialMoveID = findMove.Value.Key;
            }
        }

        private void FgFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!fgFlatNumericUpDown.Focused) return;

            UpdateStat();

            SelectedAvatar.FightingSpiritPoint = Convert.ToInt32(fgFlatNumericUpDown.Value);
        }

        private void PowerFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!powerFlatNumericUpDown.Focused) return;

            UpdateStat();

            SelectedAvatar.Attack = Convert.ToInt32(powerFlatNumericUpDown.Value);
        }

        private void TargetFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!targetFlatComboBox.Focused || targetFlatComboBox.SelectedIndex == -1) return;

            KeyValuePair<int, string>? findAvatar = AvatarNamesDict.FirstOrDefault(x => x.Value == targetFlatComboBox.SelectedItem.ToString());

            if (findAvatar.HasValue)
            {
                SelectedAvatar.FusionID = findAvatar.Value.Key;
            }
        }

        private void Material1FlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!material1FlatComboBox.Focused || material1FlatComboBox.SelectedIndex == -1) return;

            KeyValuePair<int, string>? findAvatar = AvatarNamesDict.FirstOrDefault(x => x.Value == material1FlatComboBox.SelectedItem.ToString());

            if (findAvatar.HasValue)
            {
                SelectedAvatar.Partner1FusionID = findAvatar.Value.Key;
            }
        }

        private void Material2FlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!material2FlatComboBox.Focused || material2FlatComboBox.SelectedIndex == -1) return;

            KeyValuePair<int, string>? findAvatar = AvatarNamesDict.FirstOrDefault(x => x.Value == material2FlatComboBox.SelectedItem.ToString());

            if (findAvatar.HasValue)
            {
                SelectedAvatar.Partner2FusionID = findAvatar.Value.Key;
            }
        }

        private void DescriptionTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["item_text"].Path), Itemtext, true, false, SelectedAvatar.DescriptionHash);
            nyanko.ShowDialog();
            Itemtext = nyanko.T2bþFileOpened;

            // Update current description
            if (nyanko.SelectedHash != 0)
            {
                SelectedAvatar.DescriptionHash = nyanko.SelectedHash;

                if (Itemtext.Texts.ContainsKey(SelectedAvatar.DescriptionHash))
                {
                    descriptionTextBox.Text = Itemtext.Texts[SelectedAvatar.DescriptionHash].Strings[0].Text;
                }
                else
                {
                    descriptionTextBox.Clear();
                }
            }
        }
    }
}
