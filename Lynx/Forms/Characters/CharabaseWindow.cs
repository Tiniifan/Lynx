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

namespace Lynx.Forms.Characters
{
    public partial class CharabaseWindow : Form
    {
        private IGame GameOpened;

        private List<ICharabase> Charabases;

        private List<ICharabase> CharabasesFiltred;

        private ICharabase SelectedCharabase;

        private T2bþ Charanames;

        private T2bþ CharaKiznaxHint;

        private Dictionary<string, List<int>> Models;

        public CharabaseWindow(IGame game)
        {
            GameOpened = game;
            InitializeComponent();

            // Drawing event for the ComboBox
            skinFlatComboBox.DrawItem += ComboBox1_DrawItem;

            // Enables drawing for ComboBox items
            skinFlatComboBox.DrawMode = DrawMode.OwnerDrawFixed;
        }

        private void ComboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            // Get the ComboBox
            ComboBox comboBox = (ComboBox)sender;

            // Get the default font
            Font font = comboBox.Font;

            // Get the text color
            Color textColor = e.ForeColor;

            // Determine the text color based on the item index
            textColor = EnumHelper.GetColorById<Skins>(e.Index);

            // Draw the ComboBox item with the appropriate text color
            e.DrawBackground();
            e.Graphics.DrawString(comboBox.Items[e.Index].ToString(), font, new SolidBrush(textColor), e.Bounds);
            e.DrawFocusRectangle();
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

        private void FillModel(GameSupports.GameFile gameFile)
        {
            foreach (string fileName in gameFile.File.Directory.GetFolderFromFullPath(gameFile.Path).Files.Keys)
            {
                if (fileName.EndsWith(".xi") || fileName.EndsWith(".xc"))
                {
                    int fileNumber = Convert.ToInt32(fileName
                                    .Replace("ca", "")
                                    .Replace("cn", "")
                                    .Replace("cp", "")
                                    .Replace("a", "")
                                    .Replace("m", "")
                                    .Replace(".xi", "")
                                    .Replace(".xc", "")
                                );

                    if (fileName.StartsWith("ca"))
                    {
                        if (Models["NPCOther"].IndexOf(fileNumber) == -1)
                        {
                            Models["NPCOther"].Add(fileNumber);
                        }
                    }
                    else if (fileName.StartsWith("cn"))
                    {
                        if (Models["NPC"].IndexOf(fileNumber) == -1)
                        {
                            Models["NPC"].Add(fileNumber);
                        }
                    }
                    else if (fileName.StartsWith("cp"))
                    {
                        if (Models["Player"].IndexOf(fileNumber) == -1)
                        {
                            Models["Player"].Add(fileNumber);
                        }
                    }
                }
            }
        }

        private void SetFace(UI.FlatComboBox comboBox, string fileCode)
        {
            GameSupports.GameFile faceInfo = GameOpened.Files["face"];

            VirtualDirectory faceFolder = faceInfo.File.Directory.GetFolderFromFullPath(faceInfo.Path);

            string faceFileName = fileCode + comboBox.SelectedItem.ToString().PadLeft(4, '0') + "a.xi";

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
            } else
            {
                facePictureBox.Image = null;
            }
        }

        private bool SameCharacterHash(string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return SelectedCharabase.BaseHash == crc32;
        }

        private string FindCharacterHash()
        {
            for (int i = 0; i < 10000; i++)
            {
                string namePlayer = "cp" + i.ToString().PadLeft(4, '0');
                string nameNPC = "cn" + i.ToString().PadLeft(4, '0');
                string nameNPCOther = "ca" + i.ToString().PadLeft(4, '0');

                if (SameCharacterHash(namePlayer))
                {
                    return namePlayer;
                } else if (SameCharacterHash(nameNPC))
                {
                    return nameNPC;
                } else if (SameCharacterHash(nameNPCOther))
                {
                    return nameNPCOther;
                }
            }

            // Not found
            return SelectedCharabase.BaseHash.ToString("X8");
        }

        private void CharabaseWindow_Load(object sender, EventArgs e)
        {
            Charabases = GameOpened.GetCharabase().ToList();

            GameSupports.GameFile charaText = GameOpened.Files["chara_text"];
            Charanames = new T2bþ(charaText.File.Directory.GetFileFromFullPath(charaText.Path));

            // Get model info
            GameSupports.GameFile modelRpgPlayer = GameOpened.Files["modelRpgPlayer"];
            GameSupports.GameFile modelWazaPlayer = GameOpened.Files["modelWazaPlayer"];
            GameSupports.GameFile modelRpgNPC = GameOpened.Files["modelRpgNPC"];
            GameSupports.GameFile modelWazaNPC = GameOpened.Files["modelWazaNPC"];

            // Prepare dictionary
            Models = new Dictionary<string, List<int>>();
            Models["NPC"] = new List<int>();
            Models["NPCOther"] = new List<int>();
            Models["Player"] = new List<int>();

            // get available models
            FillModel(modelRpgPlayer);
            FillModel(modelWazaPlayer);
            FillModel(modelRpgNPC);
            FillModel(modelWazaNPC);

            modelNPCFlatComboBox.Items.AddRange(Models["NPC"].Select(x => x.ToString()).ToArray());
            modelNPCOtherFlatComboBox.Items.AddRange(Models["NPCOther"].Select(x => x.ToString()).ToArray());
            modelPlayerFlatComboBox.Items.AddRange(Models["Player"].Select(x => x.ToString()).ToArray());

            yearFlatComboBox.Items.AddRange(EnumHelper.GetValues<Years>().Select(s => s.Name).ToArray());
            modelTypeflatComboBox.Items.AddRange(EnumHelper.GetValues<CharaTypes>().Select(s => s.Name).ToArray());
            skinFlatComboBox.Items.AddRange(EnumHelper.GetValues<Skins>().Select(s => s.Name).ToArray());
            bodyFlatComboBox.Items.AddRange(EnumHelper.GetValues<Bodies>().Select(s => s.Name).ToArray());
            genderFlatComboBox.Items.AddRange(EnumHelper.GetValues<Genders>().Select(s => s.Name).ToArray());

            if (GameOpened.Files.ContainsKey("kiznax_hint_text"))
            {
                GameSupports.GameFile kiznaxHintText = GameOpened.Files["kiznax_hint_text"];
                CharaKiznaxHint = new T2bþ(kiznaxHintText.File.Directory.GetFileFromFullPath(kiznaxHintText.Path));
            }

            characterListBox.Items.AddRange(GetNames(Charabases.ToArray()).ToArray());
        }

        private void CharabaseWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            GameOpened.SaveCharaBase(Charabases.ToArray());

            GameOpened.SaveTextFile(GameOpened.Files["chara_text"], Charanames);

            if (CharaKiznaxHint != null)
            {
                GameOpened.SaveTextFile(GameOpened.Files["kiznax_hint_text"], CharaKiznaxHint);
            }
        }

        private void CharacterListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CharabasesFiltred != null && CharabasesFiltred.Count > 0)
            {
                SelectedCharabase = CharabasesFiltred[characterListBox.SelectedIndex];
            } else
            {
                SelectedCharabase = Charabases[characterListBox.SelectedIndex];
            }

            hashTextBox.Text = FindCharacterHash();
            fullNameTextBox.Text = characterListBox.SelectedItem.ToString();

            if (Charanames.Nouns.ContainsKey(SelectedCharabase.NicknameHash))
            {
                nicknameTextBox.Text = Charanames.Nouns[SelectedCharabase.NicknameHash].Strings[0].Text;
            } else
            {
                nicknameTextBox.Clear();
            }

            if (CharaKiznaxHint != null && CharaKiznaxHint.Texts.ContainsKey(SelectedCharabase.DescriptionHash))
            {
                descriptionTextBox.Text = CharaKiznaxHint.Texts[SelectedCharabase.DescriptionHash].Strings[0].Text;
            }
            else
            {
                descriptionTextBox.Clear();
            }

            if (SelectedCharabase.Year >= yearFlatComboBox.Items.Count)
            {
                SelectedCharabase.Year = 0;
            }

            yearFlatComboBox.SelectedIndex = SelectedCharabase.Year;
            skinFlatComboBox.SelectedIndex = SelectedCharabase.Skin;

            modelTypeflatComboBox.SelectedIndex = SelectedCharabase.CharaBaseType;

            switch (modelTypeflatComboBox.SelectedItem.ToString())
            {
                case "Player":
                    if (modelPlayerFlatComboBox.Items.IndexOf(SelectedCharabase.ModelNumber.ToString()) == -1)
                    {
                        modelPlayerFlatComboBox.SelectedIndex = -1;
                        facePictureBox.Image = null;
                    }
                    else
                    {
                        modelPlayerFlatComboBox.SelectedItem = SelectedCharabase.ModelNumber.ToString();
                        ModelPlayerFlatComboBox_SelectedIndexChanged(sender, e);
                    }

                    break;
                case "NPC":
                    if (modelNPCFlatComboBox.Items.IndexOf(SelectedCharabase.ModelNumber.ToString()) == -1)
                    {
                        modelNPCFlatComboBox.SelectedIndex = -1;
                        facePictureBox.Image = null;
                    } else
                    {
                        modelNPCFlatComboBox.SelectedItem = SelectedCharabase.ModelNumber.ToString();
                        ModelNPCFlatComboBox_SelectedIndexChanged(sender, e);
                    }

                    break;
                case "NPC Other":

                    if (modelNPCOtherFlatComboBox.Items.IndexOf(SelectedCharabase.ModelNumber.ToString()) == -1)
                    {
                        modelNPCOtherFlatComboBox.SelectedIndex = -1;
                        facePictureBox.Image = null;
                    }
                    else
                    {
                        modelNPCOtherFlatComboBox.SelectedItem = SelectedCharabase.ModelNumber.ToString();
                        ModelNPCOtherFlatComboBox_SelectedIndexChanged(sender, e);
                    }

                    break;
                default:
                    facePictureBox.Image = null;
                    break;
            }

            bodyFlatComboBox.SelectedIndex = SelectedCharabase.Body;

            if (SelectedCharabase.Gender >= genderFlatComboBox.Items.Count)
            {
                SelectedCharabase.Gender = 0;
            }
            genderFlatComboBox.SelectedIndex = SelectedCharabase.Gender;

            characterGroupBox.Enabled = true;
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!searchTextBox.Focused || searchTextBox.Enabled == false || searchTextBox.Text == "Search...") return;

            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                characterListBox.Items.Clear();
                characterListBox.Items.AddRange(GetNames(Charabases.ToArray()).ToArray());
                CharabasesFiltred = null;
            }
            else
            {
                CharabasesFiltred = Charabases
                    .Where(x =>
                        Charanames.Nouns.ContainsKey(x.NameHash) &&
                        Charanames.Nouns[x.NameHash].Strings.Any(s => s.Text.ToLower().Contains(searchTextBox.Text.ToLower())) ||
                        Charanames.Nouns.ContainsKey(x.NicknameHash) &&
                        Charanames.Nouns[x.NicknameHash].Strings.Any(s => s.Text.ToLower().Contains(searchTextBox.Text.ToLower())))
                    .ToList();

                string[] names = GetNames(CharabasesFiltred.ToArray());

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
            characterListBox.Items.AddRange(GetNames(Charabases.ToArray()).ToArray());
            CharabasesFiltred = null;
            searchTextBox.Text = "Search...";
            searchTextBox.Enabled = true;
        }

        private void FullNameTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["chara_text"].Path), Charanames, false, true, SelectedCharabase.NameHash);
            nyanko.ShowDialog();
            Charanames = nyanko.T2bþFileOpened;

            // Update current name
            if (nyanko.SelectedHash > 0)
            {
                SelectedCharabase.NameHash = nyanko.SelectedHash;
            }

            // Update all name
            int selectedIndex = characterListBox.SelectedIndex;
            characterListBox.Items.Clear();
            characterListBox.Items.AddRange(GetNames(Charabases.ToArray()));
            characterListBox.SelectedIndex = selectedIndex;
        }

        private void NicknameTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["chara_text"].Path), Charanames, false, true, SelectedCharabase.NicknameHash);
            nyanko.ShowDialog();
            Charanames = nyanko.T2bþFileOpened;

            // Update current nickname
            if (nyanko.SelectedHash > 0)
            {
                SelectedCharabase.NicknameHash = nyanko.SelectedHash;

                if (Charanames.Nouns.ContainsKey(SelectedCharabase.NicknameHash))
                {
                    nicknameTextBox.Text = Charanames.Nouns[SelectedCharabase.NicknameHash].Strings[0].Text;
                }
                else
                {
                    nicknameTextBox.Clear();
                }
            }
        }

        private void YearFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!yearFlatComboBox.Focused || yearFlatComboBox.SelectedIndex == -1) return;

            SelectedCharabase.Year = yearFlatComboBox.SelectedIndex;
        }

        private void ModelPlayerFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modelPlayerFlatComboBox.SelectedIndex != -1)
            {
                SetFace(modelPlayerFlatComboBox, "cp");
            }
            else
            {
                facePictureBox.Image = null;
            }

            if (modelPlayerFlatComboBox.Focused && modelPlayerFlatComboBox.SelectedIndex != -1)
            {
                SelectedCharabase.ModelNumber = Convert.ToInt32(modelPlayerFlatComboBox.SelectedItem);
            }
        }

        private void ModelNPCFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modelNPCFlatComboBox.SelectedIndex != -1)
            {
                SetFace(modelNPCFlatComboBox, "cn");
            }
            else
            {
                facePictureBox.Image = null;
            }

            if (modelNPCFlatComboBox.Focused && modelNPCFlatComboBox.SelectedIndex != -1)
            {
                SelectedCharabase.ModelNumber = Convert.ToInt32(modelNPCFlatComboBox.SelectedItem);
            }
        }

        private void ModelNPCOtherFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modelNPCOtherFlatComboBox.SelectedIndex != -1)
            {
                SetFace(modelNPCOtherFlatComboBox, "ca");
            }
            else
            {
                facePictureBox.Image = null;
            }

            if (modelNPCOtherFlatComboBox.Focused && modelNPCOtherFlatComboBox.SelectedIndex != -1)
            {
                SelectedCharabase.ModelNumber = Convert.ToInt32(modelNPCOtherFlatComboBox.SelectedItem);
            }
        }

        private void ModelTypeflatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modelTypeflatComboBox.SelectedIndex == -1) return;

            switch (modelTypeflatComboBox.SelectedItem.ToString())
            {
                case "Player":
                    modelPlayerFlatComboBox.Visible = true;
                    modelNPCFlatComboBox.Visible = false;
                    modelNPCOtherFlatComboBox.Visible = false;
                    break;
                case "NPC":
                    modelPlayerFlatComboBox.Visible = false;
                    modelNPCFlatComboBox.Visible = true;
                    modelNPCOtherFlatComboBox.Visible = false;
                    break;
                case "NPC Other":
                    modelPlayerFlatComboBox.Visible = false;
                    modelNPCFlatComboBox.Visible = false;
                    modelNPCOtherFlatComboBox.Visible = true;
                    modelNPCOtherFlatComboBox.Enabled = true;
                    break;
                default:
                    modelPlayerFlatComboBox.Visible = false;
                    modelNPCFlatComboBox.Visible = false;
                    modelNPCOtherFlatComboBox.Visible = true;
                    modelNPCOtherFlatComboBox.Enabled = false;
                    break;
            }

            if (modelTypeflatComboBox.Focused)
            {
                int oldModelIndex = SelectedCharabase.ModelNumber;

                if (modelTypeflatComboBox.SelectedItem.ToString() == "Player")
                {
                    if (!modelPlayerFlatComboBox.Items.Contains(oldModelIndex.ToString()))
                    {
                        modelPlayerFlatComboBox.SelectedIndex = 0;
                        SelectedCharabase.ModelNumber = 0;
                    } else
                    {
                        modelPlayerFlatComboBox.SelectedItem = oldModelIndex.ToString();
                    }

                    ModelPlayerFlatComboBox_SelectedIndexChanged(sender, e);
                }
                else if (modelTypeflatComboBox.SelectedItem.ToString() == "NPC")
                {
                    if (!modelNPCFlatComboBox.Items.Contains(oldModelIndex.ToString()))
                    {
                        modelNPCFlatComboBox.SelectedIndex = 0;
                        SelectedCharabase.ModelNumber = 0;
                    }
                    else
                    {
                        modelNPCFlatComboBox.SelectedItem = oldModelIndex.ToString();
                    }

                    ModelNPCFlatComboBox_SelectedIndexChanged(sender, e);
                } else if (modelTypeflatComboBox.SelectedItem.ToString() == "Other") 
                {
                    if (!modelNPCOtherFlatComboBox.Items.Contains(oldModelIndex.ToString()))
                    {
                        modelNPCOtherFlatComboBox.SelectedIndex = 0;
                        SelectedCharabase.ModelNumber = 0;
                    }
                    else
                    {
                        modelNPCOtherFlatComboBox.SelectedItem = oldModelIndex.ToString();
                    }

                    ModelNPCOtherFlatComboBox_SelectedIndexChanged(sender, e);
                } else
                {
                    SelectedCharabase.ModelNumber = 0;
                }
            }
        }

        private void SkinFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skinFlatComboBox.SelectedIndex == -1) return;

            colorPictureBox.BackColor = EnumHelper.GetColorById<Skins>(skinFlatComboBox.SelectedIndex);

            if (skinFlatComboBox.Focused)
            {
                SelectedCharabase.Skin = skinFlatComboBox.SelectedIndex;
            }
        }

        private void BodyFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bodyFlatComboBox.Focused || bodyFlatComboBox.SelectedIndex == -1) return;

            SelectedCharabase.Body = bodyFlatComboBox.SelectedIndex;
        }

        private void GenderFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!genderFlatComboBox.Focused || genderFlatComboBox.SelectedIndex == -1) return;

            SelectedCharabase.Gender = genderFlatComboBox.SelectedIndex;
        }

        private void DescriptionTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["kiznax_hint_text"].Path), CharaKiznaxHint, true, false, SelectedCharabase.DescriptionHash);
            nyanko.ShowDialog();
            CharaKiznaxHint = nyanko.T2bþFileOpened;

            // Update current description
            if (nyanko.SelectedHash > 0)
            {
                SelectedCharabase.DescriptionHash = nyanko.SelectedHash;

                if (CharaKiznaxHint.Texts.ContainsKey(SelectedCharabase.DescriptionHash))
                {
                    descriptionTextBox.Text = CharaKiznaxHint.Texts[SelectedCharabase.DescriptionHash].Strings[0].Text;
                }
                else
                {
                    descriptionTextBox.Clear();
                }
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (characterListBox.SelectedIndex == -1) return;

            DialogResult dialogResult = MessageBox.Show("Do you want to delete " + characterListBox.SelectedItem.ToString() + "?", "Delete character", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                characterGroupBox.Enabled = false;

                Charabases.Remove(SelectedCharabase);
                facePictureBox.Image = null;

                MessageBox.Show(characterListBox.SelectedItem.ToString() + " has been removed!");

                if (CharabasesFiltred != null && CharabasesFiltred.Count > 0)
                {
                    CharabasesFiltred.Remove(SelectedCharabase);

                    string[] names = GetNames(CharabasesFiltred.ToArray());

                    string focusedText = characterListBox.Text;

                    characterListBox.Items.Clear();
                    characterListBox.Items.AddRange(names);

                    if (names.Contains(focusedText))
                    {
                        characterListBox.SelectedIndex = Array.IndexOf(names, focusedText);
                    }
                } else
                {
                    characterListBox.Items.Clear();
                    characterListBox.Items.AddRange(GetNames(Charabases.ToArray()).ToArray());
                }
            }
        }

        private void CharacterListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = characterListBox.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    characterListBox.SelectedIndex = index;
                }
            }
        }

        private void InsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewCharabaseWindow newCharabaseWindow = new NewCharabaseWindow(Charabases.Select(x => x.BaseHash).Distinct().ToList());
            newCharabaseWindow.ShowDialog();

            if (newCharabaseWindow.SelectedCharaNameHash != 0)
            {
                ICharabase newCharabase = null;
                int insertIndex = Charabases.Count;

                if (GameOpened.Name == "Inazuma Eleven Go")
                {
                    newCharabase = new GOSupport.CharaBase();

                    if (newCharabaseWindow.SelectedCharaType == 0)
                    {
                        newCharabase.CharaBaseType = 1;

                        int lastIndex = Charabases.FindLastIndex(x => x.CharaBaseType == 1);
                        if (lastIndex != -1)
                        {
                            insertIndex = lastIndex + 1;
                        }
                    } else if (newCharabaseWindow.SelectedCharaType == 1)
                    {
                        newCharabase.CharaBaseType = 3;

                        int lastIndex = Charabases.FindLastIndex(x => x.CharaBaseType == 3);
                        if (lastIndex != -1)
                        {
                            insertIndex = lastIndex + 1;
                        }
                    } else if (newCharabaseWindow.SelectedCharaType == 2)
                    {
                        newCharabase.CharaBaseType = 4;

                        int lastIndex = Charabases.FindLastIndex(x => x.CharaBaseType == 4);
                        if (lastIndex != -1)
                        {
                            insertIndex = lastIndex + 1;
                        }
                    }
                }

                newCharabase.BaseHash = newCharabaseWindow.SelectedCharaNameHash;

                Charabases.Insert(insertIndex, newCharabase);

                characterListBox.Items.Clear();
                characterListBox.Items.AddRange(GetNames(Charabases.ToArray()).ToArray());
                CharabasesFiltred = null;
                searchTextBox.Text = "Search...";

                characterListBox.SelectedIndex = insertIndex;
            }
        }
    }
}
