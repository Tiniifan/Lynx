using System;
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

namespace Lynx.Forms.Characters
{
    public partial class CharaParamWindow : Form
    {
        private IGame GameOpened;

        private List<ICharabase> Charabases;

        private List<Player> Players;

        private List<ITrainingUD> TrainingUDs;

        private List<IAvatar> Avatars;

        private List<ISkillConfig> SkillConfigs;

        private List<Player> PlayersFiltred;

        private Player SelectedPlayer;

        private T2bþ Charanames;

        private T2bþ Itemnames;

        private T2bþ Skillnames;

        private Dictionary<int, string> SkillnamesDict;

        private Dictionary<int, string> FightingSpiritNames;

        public CharaParamWindow(IGame game)
        {
            GameOpened = game;
            InitializeComponent();
        }

        private string[] GetNames(ICharabase[] charabases)
        {
            return charabases
                .Select((charabase, index) =>
                    Charanames.Nouns.TryGetValue(charabase.NameHash, out var noun) && noun.Strings.Count > 0
                        ? noun.Strings[0].Text
                        : "Name " + index)
                .ToArray();
        }

        private string[] GetNames(ICharaparam[] charaparams)
        {
            return charaparams
                .Select((charaparam, index) =>
                {
                    var charabase = Charabases.FirstOrDefault(cb => cb.BaseHash == charaparam.BaseHash);
                    if (charabase != null)
                    {
                        var nameHash = charabase.NameHash;
                if (Charanames.Nouns.TryGetValue(nameHash, out var noun) && noun.Strings.Count > 0)
                        {
                            return noun.Strings[0].Text;
                        }
                    }
                    return "Name " + index;
                })
                .ToArray();
        }

        private Dictionary<int, string> GetNames(IAvatar[] avatars)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var avatar in avatars)
            {
                // Determine the avatar name
                string name = avatar.AvatarHash == 0x00
                    ? " "
                    : Itemnames.Nouns.TryGetValue(avatar.NicknameHash, out var noun) && noun.Strings.Count > 0
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

                // Add the name to the output dictionary with the AvatarHash as the key
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

        private void SetFace(int modelNumber, string fileCode)
        {
            GameSupports.GameFile faceInfo = GameOpened.Files["face"];

            VirtualDirectory faceFolder = faceInfo.File.Directory.GetFolderFromFullPath(faceInfo.Path);

            string faceFileName = fileCode + modelNumber.ToString().PadLeft(4, '0') + "a.xi";

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

        private bool SameCharacterHash(string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return SelectedPlayer.Charaparam.ParamHash == crc32;
        }

        private string FindCharacterHash()
        {
            for (int i = 0; i < 10000; i++)
            {
                string namePlayer = "para_cp" + i.ToString().PadLeft(4, '0');
                string nameNPC = "para_cn" + i.ToString().PadLeft(4, '0');
                string nameNPCOther = "para_ca" + i.ToString().PadLeft(4, '0');

                if (SameCharacterHash(namePlayer))
                {
                    return namePlayer;
                }
                else if (SameCharacterHash(nameNPC))
                {
                    return nameNPC;
                }
                else if (SameCharacterHash(nameNPCOther))
                {
                    return nameNPCOther;
                }
            }

            // Not found
            return SelectedPlayer.Charaparam.ParamHash.ToString("X8");
        }

        private void SetSpecialMove(ComboBox specialMoveComboBox)
        {
            if (!specialMoveComboBox.Focused || specialMoveComboBox.SelectedIndex == -1) return;

            int specialMoveIndex = Convert.ToInt32(specialMoveComboBox.Name.Replace("moveFlatComboBox", "")) - 1;
            KeyValuePair<int, string>? findMove = SkillnamesDict.FirstOrDefault(x => x.Value == specialMoveComboBox.SelectedItem.ToString());

            if (findMove.HasValue)
            {
                SelectedPlayer.SpecialMoves[specialMoveIndex].SkillHash = findMove.Value.Key;
            }
        }

        private void SetSpecialMoveLearn(NumericUpDown specialMoveNumericUpDown)
        {
            if (!specialMoveNumericUpDown.Focused) return;

            int specialMoveIndex = Convert.ToInt32(specialMoveNumericUpDown.Name.Replace("moveLearnFlatNumericUpDown", "")) - 1;
            SelectedPlayer.SpecialMoves[specialMoveIndex].LevelLearned = Convert.ToInt32(specialMoveNumericUpDown.Value);
        }

        private void SetSpecialMoveLevel(NumericUpDown specialMoveNumericUpDown)
        {
            if (!specialMoveNumericUpDown.Focused) return;

            int specialMoveIndex = Convert.ToInt32(specialMoveNumericUpDown.Name.Replace("moveLevelFlatNumericUpDown6", "")) - 1;
            SelectedPlayer.SpecialMoves[specialMoveIndex].Level = Convert.ToInt32(specialMoveNumericUpDown.Value);
        }

        private void CharaParamWindow_Load(object sender, EventArgs e)
        {
            Charabases = GameOpened.GetCharabase().ToList();
            TrainingUDs = GameOpened.GetTrainingUDs().ToList();
            Avatars = GameOpened.GetAvatars(true).ToList();
            SkillConfigs = GameOpened.GetSkillConfigs(true).ToList();
            ISkillTable[] skilltables = GameOpened.GetSkillTable();
            Players = GameOpened.GetCharaparams()
            .Select(x =>
            {
                var specialMoves = skilltables.Skip(x.SpecialMoveOffset).Take(x.SpecialMoveCount).ToList();

                // Ensure the specialMoves list has exactly 6 items
                while (specialMoves.Count < 6)
                {
                    specialMoves.Add(GameOpened.GetEmptyObject<ISkillTable>());
                }

                return new Player(x, specialMoves);
            })
            .ToList();

            GameSupports.GameFile charaText = GameOpened.Files["chara_text"];
            Charanames = new T2bþ(charaText.File.Directory.GetFileFromFullPath(charaText.Path));
            GameSupports.GameFile itemText = GameOpened.Files["item_text"];
            Itemnames = new T2bþ(itemText.File.Directory.GetFileFromFullPath(itemText.Path));
            GameSupports.GameFile skillText = GameOpened.Files["skill_text"];
            Skillnames = new T2bþ(skillText.File.Directory.GetFileFromFullPath(skillText.Path));

            positionFlatComboBox.Items.AddRange(EnumHelper.GetValues<PlayerPositions>().Select(s => s.Name).ToArray());
            elementFlatComboBox.Items.AddRange(EnumHelper.GetValues<Elements>().Select(s => s.Name).ToArray());
            experienceFlatComboBox.Items.AddRange(EnumHelper.GetValues<ExperienceCurve>().Select(s => s.Name).ToArray());

            fpGrowFlatComboBox.Items.AddRange(EnumHelper.GetValues<GrowStats>().Select(s => s.Name).ToArray());
            tpGrowFlatComboBox.Items.AddRange(EnumHelper.GetValues<GrowStats>().Select(s => s.Name).ToArray());
            kickGrowFlatComboBox.Items.AddRange(EnumHelper.GetValues<GrowStats>().Select(s => s.Name).ToArray());
            dribbleGrowFlatComboBox.Items.AddRange(EnumHelper.GetValues<GrowStats>().Select(s => s.Name).ToArray());
            techniqueGrowFlatComboBox.Items.AddRange(EnumHelper.GetValues<GrowStats>().Select(s => s.Name).ToArray());
            blockGrowFlatComboBox.Items.AddRange(EnumHelper.GetValues<GrowStats>().Select(s => s.Name).ToArray());
            speedGrowFlatComboBox.Items.AddRange(EnumHelper.GetValues<GrowStats>().Select(s => s.Name).ToArray());
            staminaGrowFlatComboBox.Items.AddRange(EnumHelper.GetValues<GrowStats>().Select(s => s.Name).ToArray());
            catchGrowFlatComboBox.Items.AddRange(EnumHelper.GetValues<GrowStats>().Select(s => s.Name).ToArray());
            luckGrowFlatComboBox.Items.AddRange(EnumHelper.GetValues<GrowStats>().Select(s => s.Name).ToArray());

            //genderFlatComboBox.Items.AddRange(EnumHelper.GetValues<Genders>().Select(s => s.Name).ToArray());

            baseFlatComboBox.Items.AddRange(GetNames(Charabases.ToArray()).ToArray());
            characterListBox.Items.AddRange(GetNames(Players.Select(x => x.Charaparam).ToArray()).ToArray());
            trainingFlatComboBox.Items.AddRange(TrainingUDs.Select((x, i) => $"Training {i+1}").ToArray());
            FightingSpiritNames = GetNames(Avatars.ToArray());
            avatarFlatComboBox.Items.AddRange(FightingSpiritNames.Select(x => x.Value).ToArray());
            avatarMatchFlatComboBox.Items.AddRange(avatarFlatComboBox.Items.Cast<Object>().ToArray());
            SkillnamesDict = GetNames(SkillConfigs.ToArray());
            moveFlatComboBox1.Items.AddRange(SkillnamesDict.Select(x => x.Value).ToArray());
            moveFlatComboBox2.Items.AddRange(moveFlatComboBox1.Items.Cast<Object>().ToArray());
            moveFlatComboBox3.Items.AddRange(moveFlatComboBox1.Items.Cast<Object>().ToArray());
            moveFlatComboBox4.Items.AddRange(moveFlatComboBox1.Items.Cast<Object>().ToArray());
            moveFlatComboBox5.Items.AddRange(moveFlatComboBox1.Items.Cast<Object>().ToArray());
            moveFlatComboBox6.Items.AddRange(moveFlatComboBox1.Items.Cast<Object>().ToArray());
        }

        private void CharaParamWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            int skillOffset = 0;
            List<ISkillTable> skillTables = new List<ISkillTable>();

            foreach (Player player in Players)
            {
                // Fix player special move offset start
                player.Charaparam.SpecialMoveOffset = skillOffset;

                // Fix moveset
                ISkillTable[] myPlayerSkills = player.SpecialMoves.Where(x => x.SkillHash != 0x00).ToArray();
                foreach (ISkillTable myPlayerSkill in myPlayerSkills)
                {
                    myPlayerSkill.SkillIndex = skillOffset;
                    skillOffset++;
                }

                // Fix plater special move count
                player.Charaparam.SpecialMoveCount = myPlayerSkills.Length;

                // Add new moveset on skilltables
                skillTables.AddRange(myPlayerSkills);
            }

            GameOpened.SaveCharaparams(Players.Select(x => x.Charaparam).ToArray());
            GameOpened.SaveSkillTable(skillTables.ToArray());
            GameOpened.SaveTextFile(GameOpened.Files["chara_text"], Charanames);
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!searchTextBox.Focused || searchTextBox.Enabled == false || searchTextBox.Text == "Search...") return;

            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                characterListBox.Items.Clear();
                characterListBox.Items.AddRange(GetNames(Players.Select(x => x.Charaparam).ToArray()).ToArray());
                PlayersFiltred = null;
            }
            else
            {
                PlayersFiltred = Players
                    .Where(player =>
                    {
                        var searchCharabase = Charabases.FirstOrDefault(x => x.BaseHash == player.Charaparam.BaseHash);

                        bool searchByBaseHash = ("0x" + player.Charaparam.BaseHash.ToString("X8").ToLower()).Contains(searchTextBox.Text.ToLower());
                        bool searchByParamHash = ("0x" + player.Charaparam.ParamHash.ToString("X8").ToLower()).Contains(searchTextBox.Text.ToLower());
                        bool searchByName = searchCharabase != null && Charanames.Nouns.ContainsKey(searchCharabase.NameHash) && Charanames.Nouns[searchCharabase.NameHash].Strings.Any(s => s.Text.ToLower().Contains(searchTextBox.Text.ToLower()));
                        bool searchByNickname = searchCharabase != null && Charanames.Nouns.ContainsKey(searchCharabase.NicknameHash) && Charanames.Nouns[searchCharabase.NicknameHash].Strings.Any(s => s.Text.ToLower().Contains(searchTextBox.Text.ToLower()));

                        return searchByBaseHash || searchByParamHash || searchByName || searchByNickname;
                    })
                    .ToList();

                string[] names = GetNames(PlayersFiltred.Select(x => x.Charaparam).ToArray());

                string focusedText = characterListBox.Text;

                characterListBox.Items.Clear();
                characterListBox.Items.AddRange(names);

                if (names.Contains(focusedText))
                {
                    characterListBox.SelectedIndex = Array.IndexOf(names, focusedText);
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
            characterListBox.Items.Clear();
            characterListBox.Items.AddRange(GetNames(Players.Select(x => x.Charaparam).ToArray()).ToArray());
            PlayersFiltred = null;
            searchTextBox.Text = "Search...";
            searchTextBox.Enabled = true;
        }

        private void CharacterListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (characterListBox.SelectedIndex == -1) return;

            if (PlayersFiltred != null && PlayersFiltred.Count > 0)
            {
                SelectedPlayer = PlayersFiltred[characterListBox.SelectedIndex];
            }
            else
            {
                SelectedPlayer = Players[characterListBox.SelectedIndex];
            }

            hashTextBox.Text = FindCharacterHash();
            baseFlatComboBox.SelectedIndex = Charabases.FindIndex(x => x.BaseHash == SelectedPlayer.Charaparam.BaseHash);

            positionFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.PlayerPosition;
            elementFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.Element;
            trainingFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.TrainingUD;
            experienceFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.ExperienceGrow;

            fpFlatNumericUpDown.Value = SelectedPlayer.Charaparam.FP;
            fpGrowFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.FPGrow;
            tpFlatNumericUpDown.Value = SelectedPlayer.Charaparam.TP;
            tpGrowFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.TPGrow;
            kickFlatNumericUpDown.Value = SelectedPlayer.Charaparam.Kick;
            kickGrowFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.KickGrow;
            dribbleFlatNumericUpDown.Value = SelectedPlayer.Charaparam.Dribble;
            dribbleGrowFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.DribbleGrow;
            techniqueFlatNumericUpDown.Value = SelectedPlayer.Charaparam.Technique;
            techniqueGrowFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.TechniqueGrow;
            blockFlatNumericUpDown.Value = SelectedPlayer.Charaparam.Block;
            blockGrowFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.BlockGrow;
            speedFlatNumericUpDown.Value = SelectedPlayer.Charaparam.Speed;
            speedGrowFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.SpeedGrow;
            staminaFlatNumericUpDown.Value = SelectedPlayer.Charaparam.Stamina;
            staminaGrowFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.StaminaGrow;
            catchFlatNumericUpDown.Value = SelectedPlayer.Charaparam.Catch;
            catchGrowFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.CatchGrow;
            luckFlatNumericUpDown.Value = SelectedPlayer.Charaparam.Luck;
            luckGrowFlatComboBox.SelectedIndex = SelectedPlayer.Charaparam.LuckGrow;
            freedomFlatNumericUpDown.Value = SelectedPlayer.Charaparam.Freedom;
            varianceFlatNumericUpDown.Value = SelectedPlayer.Charaparam.StatVariance;

            if (FightingSpiritNames.ContainsKey(SelectedPlayer.Charaparam.FightingSpiritHash))
            {
                avatarFlatComboBox.SelectedIndex = avatarFlatComboBox.Items.IndexOf(FightingSpiritNames[SelectedPlayer.Charaparam.FightingSpiritHash]);
            } else
            {
                avatarFlatComboBox.SelectedIndex = avatarFlatComboBox.Items.IndexOf(FightingSpiritNames[0x00]);
            }

            if (FightingSpiritNames.ContainsKey(SelectedPlayer.Charaparam.FightingSpiritHash))
            {
                avatarMatchFlatComboBox.SelectedIndex = avatarMatchFlatComboBox.Items.IndexOf(FightingSpiritNames[SelectedPlayer.Charaparam.FightingSpiritMatchkHash]);
                avatarMatchFlatComboBox.SelectedIndex = avatarMatchFlatComboBox.Items.IndexOf(FightingSpiritNames[0x00]);
            }

            if (Charanames != null && Charanames.Texts.ContainsKey(SelectedPlayer.Charaparam.DescriptionHash))
            {
                descriptionTextBox.Text = Charanames.Texts[SelectedPlayer.Charaparam.DescriptionHash].Strings[0].Text;
            }
            else
            {
                descriptionTextBox.Clear();
            }

            for (int i = 0; i < 6; i++)
            {
                // Get controls using reflection
                ComboBox moveFlatComboBox = Controls.Find($"moveFlatComboBox{i+1}", true).FirstOrDefault() as ComboBox;
                NumericUpDown moveLearnFlatNumericUpDown = Controls.Find($"moveLearnFlatNumericUpDown{i+1}", true).FirstOrDefault() as NumericUpDown;
                NumericUpDown moveLevelFlatNumericUpDown = Controls.Find($"moveLevelFlatNumericUpDown{i+1}", true).FirstOrDefault() as NumericUpDown;

                if (moveFlatComboBox != null && moveLearnFlatNumericUpDown != null && moveLevelFlatNumericUpDown != null)
                {
                    if (i < SelectedPlayer.SpecialMoves.Count())
                    {
                        var skill = SkillConfigs.Find(x => x.SkillHash == SelectedPlayer.SpecialMoves[i].SkillHash);
                        
                        if (skill != null && SkillnamesDict.ContainsKey(skill.SkillHash))
                        {
                            moveFlatComboBox.SelectedIndex = moveFlatComboBox.Items.IndexOf(SkillnamesDict[skill.SkillHash]);
                            moveLearnFlatNumericUpDown.Value = SelectedPlayer.SpecialMoves[i].LevelLearned;
                            moveLevelFlatNumericUpDown.Value = SelectedPlayer.SpecialMoves[i].Level;
                        }
                    }
                    else
                    {
                        var defaultSkill = SkillConfigs.Find(x => x.SkillHash == 0x00);

                        if (defaultSkill != null && SkillnamesDict.ContainsKey(defaultSkill.SkillHash))
                        {
                            moveFlatComboBox.SelectedIndex = moveFlatComboBox.Items.IndexOf(SkillnamesDict[defaultSkill.SkillHash]);
                            moveLearnFlatNumericUpDown.Value = 0;
                            moveLevelFlatNumericUpDown.Value = 0;
                        }
                    }
                }
            }
            

            characterGroupBox.Enabled = true;
        }

        private void BaseFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (baseFlatComboBox.SelectedIndex != -1)
            {
                if (baseFlatComboBox.Focused)
                {
                    ICharabase newCharabase = Charabases[baseFlatComboBox.SelectedIndex];
                    SelectedPlayer.Charaparam.BaseHash = newCharabase.BaseHash;

                    int previousIndex = characterListBox.SelectedIndex;
                    characterListBox.SelectedIndex = -1;
                    characterListBox.Items.RemoveAt(previousIndex);
                    characterListBox.Items.Insert(previousIndex, baseFlatComboBox.SelectedItem);
                    characterListBox.SelectedIndex = previousIndex;
                }

                int modelNumber = Charabases.Find(x => x.BaseHash == SelectedPlayer.Charaparam.BaseHash).ModelNumber;
                int characterType = Charabases.Find(x => x.BaseHash == SelectedPlayer.Charaparam.BaseHash).CharaBaseType;

                if (characterType == 1)
                {
                    SetFace(modelNumber, "cp");
                } else if (characterType == 3)
                {
                    SetFace(modelNumber, "cn");
                }
                else if (characterType == 4)
                {
                    SetFace(modelNumber, "ca");
                } else
                {
                    facePictureBox.Image = null;
                }
            }
            else
            {
                facePictureBox.Image = null;
            }
        }

        private void PositionFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!positionFlatComboBox.Focused || positionFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.PlayerPosition = positionFlatComboBox.SelectedIndex;
        }

        private void ElementFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!elementFlatComboBox.Focused || elementFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.Element = elementFlatComboBox.SelectedIndex;
        }

        private void TrainingFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!trainingFlatComboBox.Focused || trainingFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.TrainingUD = trainingFlatComboBox.SelectedIndex;
        }

        private void ExperienceFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!experienceFlatComboBox.Focused || experienceFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.ExperienceGrow = experienceFlatComboBox.SelectedIndex;
        }

        private void FpFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!fpFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.FP = Convert.ToInt32(fpFlatNumericUpDown.Value);
        }

        private void TpFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!tpFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.TP = Convert.ToInt32(tpFlatNumericUpDown.Value);
        }

        private void KickFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!kickFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.Kick = Convert.ToInt32(kickFlatNumericUpDown.Value);
        }

        private void DribbleFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!dribbleFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.Dribble = Convert.ToInt32(dribbleFlatNumericUpDown.Value);
        }

        private void TechniqueFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!techniqueFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.Technique = Convert.ToInt32(techniqueFlatNumericUpDown.Value);
        }

        private void BlockFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!blockFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.Block = Convert.ToInt32(blockFlatNumericUpDown.Value);
        }

        private void SpeedFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!speedFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.Speed = Convert.ToInt32(speedFlatNumericUpDown.Value);
        }

        private void StaminaFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!staminaFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.Stamina = Convert.ToInt32(staminaFlatNumericUpDown.Value);
        }

        private void CatchFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!catchFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.Catch = Convert.ToInt32(catchFlatNumericUpDown.Value);
        }

        private void LuckFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!luckFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.Luck = Convert.ToInt32(luckFlatNumericUpDown.Value);
        }

        private void FreedomFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!freedomFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.Freedom = Convert.ToInt32(freedomFlatNumericUpDown.Value);
        }

        private void FpGrowFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!fpGrowFlatComboBox.Focused || fpGrowFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.FPGrow = fpGrowFlatComboBox.SelectedIndex;
        }

        private void TpGrowFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!tpGrowFlatComboBox.Focused || tpGrowFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.TPGrow = tpGrowFlatComboBox.SelectedIndex;
        }

        private void KickGrowFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!kickGrowFlatComboBox.Focused || kickGrowFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.KickGrow = kickGrowFlatComboBox.SelectedIndex;
        }

        private void DribbleGrowFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!dribbleGrowFlatComboBox.Focused || dribbleGrowFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.DribbleGrow = dribbleGrowFlatComboBox.SelectedIndex;
        }

        private void TechniqueGrowFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!techniqueGrowFlatComboBox.Focused || techniqueGrowFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.TechniqueGrow = techniqueGrowFlatComboBox.SelectedIndex;
        }

        private void BlockGrowFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!blockGrowFlatComboBox.Focused || blockGrowFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.BlockGrow = blockGrowFlatComboBox.SelectedIndex;
        }

        private void SpeedGrowFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!speedGrowFlatComboBox.Focused || speedGrowFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.SpeedGrow = speedGrowFlatComboBox.SelectedIndex;
        }

        private void StaminaGrowFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!staminaGrowFlatComboBox.Focused || staminaGrowFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.StaminaGrow = staminaGrowFlatComboBox.SelectedIndex;
        }

        private void CatchGrowFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!catchGrowFlatComboBox.Focused || catchGrowFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.CatchGrow = catchGrowFlatComboBox.SelectedIndex;
        }

        private void LuckGrowFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!luckGrowFlatComboBox.Focused || luckGrowFlatComboBox.SelectedIndex == -1) return;

            SelectedPlayer.Charaparam.LuckGrow = luckGrowFlatComboBox.SelectedIndex;
        }

        private void VarianceFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!varianceFlatNumericUpDown.Focused) return;

            SelectedPlayer.Charaparam.StatVariance = Convert.ToInt32(varianceFlatNumericUpDown.Value);
        }

        private void AvatarFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!avatarFlatComboBox.Focused || avatarFlatComboBox.SelectedIndex == -1) return;

            KeyValuePair<int, string>? findAvatar = FightingSpiritNames.FirstOrDefault(x => x.Value == avatarFlatComboBox.SelectedItem.ToString());
            if (findAvatar.HasValue)
            {
                SelectedPlayer.Charaparam.FightingSpiritHash = findAvatar.Value.Key;
            }
        }

        private void AvatarMatchFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!avatarMatchFlatComboBox.Focused || avatarMatchFlatComboBox.SelectedIndex == -1) return;

            KeyValuePair<int, string>? findAvatar = FightingSpiritNames.FirstOrDefault(x => x.Value == avatarMatchFlatComboBox.SelectedItem.ToString());
            if (findAvatar.HasValue)
            {
                SelectedPlayer.Charaparam.FightingSpiritMatchkHash = findAvatar.Value.Key;
            }
        }

        private void MoveFlatComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSpecialMove(moveFlatComboBox1);
        }

        private void MoveFlatComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSpecialMove(moveFlatComboBox2);
        }

        private void MoveFlatComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSpecialMove(moveFlatComboBox3);
        }

        private void MoveFlatComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSpecialMove(moveFlatComboBox4);
        }

        private void MoveFlatComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSpecialMove(moveFlatComboBox5);
        }

        private void MoveFlatComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSpecialMove(moveFlatComboBox6);
        }

        private void MoveLearnFlatNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLearn(moveLearnFlatNumericUpDown1);
        }

        private void MoveLearnFlatNumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLearn(moveLearnFlatNumericUpDown2);
        }

        private void MoveLearnFlatNumericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLearn(moveLearnFlatNumericUpDown3);
        }

        private void MoveLearnFlatNumericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLearn(moveLearnFlatNumericUpDown4);
        }

        private void MoveLearnFlatNumericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLearn(moveLearnFlatNumericUpDown5);
        }

        private void MoveLearnFlatNumericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLearn(moveLearnFlatNumericUpDown6);
        }

        private void MoveLevelFlatNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLevel(moveLearnFlatNumericUpDown1);
        }

        private void MoveLevelFlatNumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLevel(moveLearnFlatNumericUpDown2);
        }

        private void MoveLevelFlatNumericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLevel(moveLearnFlatNumericUpDown3);
        }

        private void MoveLevelFlatNumericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLevel(moveLearnFlatNumericUpDown4);
        }

        private void MoveLevelFlatNumericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLevel(moveLearnFlatNumericUpDown5);
        }

        private void MoveLevelFlatNumericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            SetSpecialMoveLevel(moveLearnFlatNumericUpDown6);
        }

        private void DescriptionTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["chara_text"].Path), Charanames, true, false, SelectedPlayer.Charaparam.DescriptionHash);
            nyanko.ShowDialog();
            Charanames = nyanko.T2bþFileOpened;

            // Update current description
            if (nyanko.SelectedHash > 0)
            {
                SelectedPlayer.Charaparam.DescriptionHash = nyanko.SelectedHash;

                if (Charanames.Texts.ContainsKey(SelectedPlayer.Charaparam.DescriptionHash))
                {
                    descriptionTextBox.Text = Charanames.Texts[SelectedPlayer.Charaparam.DescriptionHash].Strings[0].Text;
                }
                else
                {
                    descriptionTextBox.Clear();
                }
            }
        }

        private void InsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewCharaparamWindow newCharaparamWindow = new NewCharaparamWindow(Players.Select(x => x.Charaparam.ParamHash).Distinct().ToList());
            newCharaparamWindow.ShowDialog();

            if (newCharaparamWindow.SelectedCharaNameHash != 0)
            {
                int insertIndex = Players.Count;

                ICharaparam newCharaparam = GameOpened.GetEmptyObject<ICharaparam>();
                List<ISkillTable> skillTables = Enumerable.Range(0, 6)
                                        .Select(_ => GameOpened.GetEmptyObject<ISkillTable>())
                                        .ToList();

                newCharaparam.ParamHash = newCharaparamWindow.SelectedCharaNameHash;
                Players.Insert(insertIndex, new Player(newCharaparam, skillTables));

                characterListBox.Items.Clear();
                characterListBox.Items.AddRange(GetNames(Players.Select(x => x.Charaparam).ToArray()).ToArray());
                PlayersFiltred = null;
                searchTextBox.Text = "Search...";

                characterListBox.SelectedIndex = insertIndex;
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (characterListBox.SelectedIndex == -1) return;

            DialogResult dialogResult = MessageBox.Show("Do you want to delete " + characterListBox.SelectedItem.ToString() + "?", "Delete character", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                characterGroupBox.Enabled = false;

                Players.Remove(SelectedPlayer);
                facePictureBox.Image = null;

                MessageBox.Show(characterListBox.SelectedItem.ToString() + " has been removed!");

                if (PlayersFiltred != null && PlayersFiltred.Count > 0)
                {
                    PlayersFiltred.Remove(SelectedPlayer);

                    string[] names = GetNames(Players.Select(x => x.Charaparam).ToArray());

                    string focusedText = characterListBox.Text;

                    characterListBox.Items.Clear();
                    characterListBox.Items.AddRange(names);

                    if (names.Contains(focusedText))
                    {
                        characterListBox.SelectedIndex = Array.IndexOf(names, focusedText);
                    }
                }
                else
                {
                    characterListBox.Items.Clear();
                    characterListBox.Items.AddRange(GetNames(Charabases.ToArray()).ToArray());
                }
            }
        }
    }
}
