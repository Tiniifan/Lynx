using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using StudioElevenLib.Tools;
using StudioElevenLib.Level5.Text;
using StudioElevenLib.Level5.Image;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;
using Lynx.InazumaEleven.Common;
using OfficeOpenXml;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Lynx.Forms.FightingSpirits
{
    public partial class FightingSpiritWindow : Form
    {
        private Game GameOpened;

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

        private void Save()
        {
            GameOpened.SaveAvatars(Avatars.ToArray());
            GameOpened.SaveAvatarGrowthTable(AvatarTimeGrowths.ToArray());
            GameOpened.SaveTextFile(GameOpened.Files["item_text"], Itemtext);
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

        public FightingSpiritWindow(Game game)
        {
            GameOpened = game;
            InitializeComponent();
            InitializeAvatarResource();
        }

        private void FightingSpiritWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Save();
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
                    facePictureBox.Image = Imager.Open(imageData).Bitmap;
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

        private void ExportAsCfgbinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // Save
                Save();

                // Get files
                (string, byte[]) itemconfig = GameOpened.GetFileNameAndContent("item_config");
                (string, byte[]) itemtext = GameOpened.GetFileNameAndContent("item_text");

                // Export files
                File.WriteAllBytes(Path.Combine(dialog.FileName, itemconfig.Item1), itemconfig.Item2);
                File.WriteAllBytes(Path.Combine(dialog.FileName, itemtext.Item1), itemtext.Item2);

                MessageBox.Show("Data exported!");
            }
        }

        public void ExportToExcel(string filePath)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Skills");

                string[] headers = new string[]
                {
            "Name", "ID", "Position", "Element", "Special Move", "Skill", "Fusion (Target)",
            "Fusion (Material 1)", "Fusion (Material 2)", "Level Growth",
            "FSP (Level I)", "FSP (Level II)", "FSP (Level III)", "FSP (Level IV)", "FSP (Level V)", "FSP (Level Ω)",
            "Attack (Level I)", "Attack (Level II)", "Attack (Level III)", "Attack (Level IV)", "Attack (Level V)", "Attack (Level Ω)"
                };

                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                int row = 2;

                foreach (var avatar in Avatars)
                {
                    if (avatar.AvatarHash == 0) continue;

                    worksheet.Cells[row, 1].Value = AvatarNamesDict.ContainsKey(avatar.AvatarHash)
                        ? AvatarNamesDict[avatar.AvatarHash]
                        : "Unknown";

                    worksheet.Cells[row, 2].Value = FindAvatarID(avatar.AvatarHash);

                    worksheet.Cells[row, 3].Value = positonFlatComboBox.Items[avatar.Position].ToString();
                    worksheet.Cells[row, 4].Value = elementFlatComboBox.Items[avatar.Element].ToString();

                    var skill = SkillConfigs.Find(x => x.SkillHash == avatar.SkillID);
                    var special = SkillConfigs.Find(x => x.SkillHash == avatar.SpecialMoveID);

                    worksheet.Cells[row, 5].Value = special != null && SkillNamesDict.ContainsKey(special.SkillHash)
                        ? SkillNamesDict[special.SkillHash]
                        : "None";

                    worksheet.Cells[row, 6].Value = skill != null && SkillNamesDict.ContainsKey(skill.SkillHash)
                        ? SkillNamesDict[skill.SkillHash]
                        : "None";

                    worksheet.Cells[row, 7].Value = AvatarNamesDict.ContainsKey(avatar.FusionID)
                        ? AvatarNamesDict[avatar.FusionID]
                        : "None";

                    worksheet.Cells[row, 8].Value = AvatarNamesDict.ContainsKey(avatar.Partner1FusionID)
                        ? AvatarNamesDict[avatar.Partner1FusionID]
                        : "None";

                    worksheet.Cells[row, 9].Value = AvatarNamesDict.ContainsKey(avatar.Partner2FusionID)
                        ? AvatarNamesDict[avatar.Partner2FusionID]
                        : "None";

                    worksheet.Cells[row, 10].Value = levelGrowthFlatComboBox.Items[avatar.EvolutionGrow].ToString();

                    for (int i = 0; i < 6; i++)
                    {
                        worksheet.Cells[row, 11 + i].Value = GetFSPAtLevel(avatar, i);
                        worksheet.Cells[row, 17 + i].Value = GetAttackAtLevel(avatar, i);
                    }

                    row++;
                }

                for (int col = 1; col <= headers.Length; col++)
                {
                    worksheet.Column(col).AutoFit();
                }

                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);
            }

            MessageBox.Show("Data exported!");
        }

        private int GetFSPAtLevel(IAvatar avatar, int levelIndex)
        {
            // levelIndex: 0 = level 1, 5 = level Ω
            if (avatar.EvolutionStatGrow > 0 && avatar.EvolutionGrow > 0 &&
                levelIndex >= 0 && levelIndex <= 5)
            {
                var fgTable = AvatarGrowthStats.IEGO[avatar.EvolutionStatGrow][avatar.EvolutionGrow].FG;
                int baseFSP = avatar.FightingSpiritPoint;
                int total = baseFSP;

                for (int i = 0; i < levelIndex; i++)
                {
                    total += fgTable[i];
                }

                return total;
            }

            return avatar.FightingSpiritPoint;
        }

        private int GetAttackAtLevel(IAvatar avatar, int levelIndex)
        {
            if (avatar.EvolutionStatGrow > 0 && avatar.EvolutionGrow > 0 &&
                levelIndex >= 0 && levelIndex <= 5)
            {
                var powerTable = AvatarGrowthStats.IEGO[avatar.EvolutionStatGrow][avatar.EvolutionGrow].Attack;
                int basePower = avatar.Attack;
                int total = basePower;

                for (int i = 0; i < levelIndex; i++)
                {
                    total += powerTable[i];
                }

                return total;
            }

            return avatar.Attack;
        }

        private void ExportAscsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.SaveFileDialogSheet))
            {
                saveFileDialog1.InitialDirectory = Properties.Settings.Default.SaveFileDialogSheet;
            }

            saveFileDialog1.Filter = "XLSX Files(*.xlsx) | *.xlsx";
            saveFileDialog1.Title = "Export your file as sheet";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportToExcel(saveFileDialog1.FileName);

                Properties.Settings.Default.OpenFileDialogSaveEditor = Path.GetDirectoryName(saveFileDialog1.FileName);
                Properties.Settings.Default.Save();
            }
        }
    }
}
