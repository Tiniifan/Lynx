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

        private bool SameSkillID(int id, string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return id == crc32;
        }

        private string FindAvatarID()
        {
            for (int i = 0; i < 10000; i++)
            {
                string prefix = "";

                string tryAvatarID = $"ck{prefix}{i.ToString().PadLeft(4, '0')}";

                if (SameSkillID(SelectedAvatar.AvatarHash, tryAvatarID))
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

        private void InitializeAvatarResource()
        {
            GameSupports.GameFile skillTextGameFile = GameOpened.Files["item_text"];
            Itemtext = new T2bþ(skillTextGameFile.File.Directory.GetFileFromFullPath(skillTextGameFile.Path));
            GameSupports.GameFile skillText = GameOpened.Files["skill_text"];
            Skillnames = new T2bþ(skillText.File.Directory.GetFileFromFullPath(skillText.Path));

            positonFlatComboBox.Items.AddRange(EnumHelper.GetValues<PlayerPositions>().Select(s => s.Name).ToArray());
            elementFlatComboBox.Items.AddRange(EnumHelper.GetValues<Elements>().Select(s => s.Name).ToArray());

            SkillConfigs = GameOpened.GetSkillConfigs(true).ToList();
            SkillNamesDict = GetNames(SkillConfigs.ToArray());
            skillFlatComboBox.Items.AddRange(SkillNamesDict.Select(x => x.Value).ToArray());
            specialMoveFlatComboBox.Items.AddRange(skillFlatComboBox.Items.Cast<Object>().ToArray());

            Avatars = GameOpened.GetAvatars(true).ToList();
            AvatarNamesDict = GetNames(Avatars.ToArray());
            avatarListBox.Items.AddRange(AvatarNamesDict.Where(x => x.Key != 0x0).Select(x => x.Value).ToArray());
            targetFlatComboBox.Items.AddRange(AvatarNamesDict.Select(x => x.Value).ToArray());
            material1FlatComboBox.Items.AddRange(targetFlatComboBox.Items.Cast<Object>().ToArray());
            material2FlatComboBox.Items.AddRange(targetFlatComboBox.Items.Cast<Object>().ToArray());
        }

        public FightingSpiritWindow(IGame game)
        {
            GameOpened = game;
            InitializeComponent();
            InitializeAvatarResource();
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
        }

        private void CanBeSoldCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            soldPriceFlatNumericUpDown.Enabled = canBeSoldCheckBox.Checked;
        }

        private void CanBeBoughtCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            boughtPriceFlatNumericUpDown.Enabled = canBeBoughtCheckBox.Checked;
        }
    }
}
