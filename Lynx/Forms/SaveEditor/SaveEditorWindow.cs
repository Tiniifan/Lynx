using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using Lynx.Tools;
using Lynx.Level5.Text;
using Lynx.InazumaEleven.Games;
using Lynx.Level5.Save;
using Lynx.Level5.Save.Games;
using Lynx.Level5.Save.Saves;
using Lynx.Level5.Save.Logic;
using Lynx.InazumaEleven.Logic;
using Lynx.Level5.Save.Saves.IE;

namespace Lynx.Forms.SaveEditor
{
    public partial class SaveEditorWindow : Form
    {
        private ISave Save;

        private Level5.Save.Logic.Player SelectedPlayer;

        private Dictionary<int, Level5.Save.Logic.Player> Players;

        private Dictionary<int, Level5.Save.Logic.Avatar> Avatars;

        private Dictionary<int, Level5.Save.Logic.Move> Moves;

        private InazumaEleven.Games.IGame GameOpened;

        public SaveEditorWindow(InazumaEleven.Games.IGame game)
        {
            GameOpened = game;
            InitializeComponent();
            InitializeSaveResource();
        }

        private string[] GetNames(T2bþ charanames, ICharaparam[] charaparams, ICharabase[] charabases)
        {
            var nameCounts = new Dictionary<string, int>();

            return charaparams
                .Select((charaparam, index) =>
                {
                    string name = "";
                    var charabase = charabases.FirstOrDefault(cb => cb.BaseHash == charaparam.BaseHash);

                    if (charabase != null)
                    {
                        var nameHash = charabase.NameHash;

                        if (charanames.Nouns.TryGetValue(nameHash, out var noun) && noun.Strings.Count > 0)
                        {
                            name = noun.Strings[0].Text;
                        } else
                        {
                            name = "Name " + index;
                        }
                    } else
                    {
                        name = "Name " + index;
                    }

                    if (nameCounts.ContainsKey(name))
                    {
                        nameCounts[name]++;
                        name += $" ({nameCounts[name]})";
                    }
                    else
                    {
                        nameCounts[name] = 1;
                    }

                    return name;
                })
                .ToArray();
        }

        private string[] GetNames(T2bþ skilltext, ISkillConfig[] skillConfigs)
        {
            return skillConfigs
                .Select((ISkillConfig, index) =>
                {
                    var nameHash = ISkillConfig.NameHash;

                    if (ISkillConfig.SkillHash == 0x0)
                    {
                        return " ";
                    } else if (skilltext.Nouns.TryGetValue(nameHash, out var noun) && noun.Strings.Count > 0)
                    {
                        return noun.Strings[0].Text;
                    }

                    return "Name " + index;
                })
                .ToArray();
        }

        private string[] GetNames(T2bþ itemText, IAvatar[] avatars)
        {
            return avatars
                .Select((ISkillConfig, index) =>
                {
                    var nameHash = ISkillConfig.NicknameHash;

                    if (ISkillConfig.SkillHash == 0x0)
                    {
                        return " ";
                    }
                    else if (itemText.Nouns.TryGetValue(nameHash, out var noun) && noun.Strings.Count > 0)
                    {
                        return noun.Strings[0].Text;
                    }

                    return "Name " + index;
                })
                .ToArray();
        }

        private Element GetElement(int element)
        {
            switch (element)
            {
                case 1:
                    return Element.Wind();
                case 2:
                    return Element.Wood();
                case 3:
                    return Element.Fire();
                case 4:
                    return Element.Earth();
                case 5:
                    return Element.Void();
                default:
                    return new Element("Elementless");
            }
        }

        private Position GetPosition(int position)
        {
            switch (position)
            {
                case 1:
                    return Position.Goalkeeper();
                case 2:
                    return Position.Forward();
                case 3:
                    return Position.Midfielder();
                case 4:
                    return Position.Defender();
                default:
                    return new Position("Positionless");
            }
        }

        private Gender GetGender(int gender)
        {
            switch (gender)
            {
                case 1:
                    return Gender.Boy();
                case 2:
                    return Gender.Girl();
                default:
                    return new Gender("Unknown");
            }
        }

        private void InitializeSaveResource()
        {
            ICharaparam[] charaparams = GameOpened.GetCharaparams();
            ICharabase[] charabases = GameOpened.GetCharabase();
            ISkillTable[] skilltables = GameOpened.GetSkillTable();

            GameSupports.GameFile charaText = GameOpened.Files["chara_text"];
            T2bþ charanames = new T2bþ(charaText.File.Directory.GetFileFromFullPath(charaText.Path));
            string[] names = GetNames(charanames, charaparams.GroupBy(x => x.ParamHash).Select(g => g.First()).ToArray(), charabases);

            Players = charaparams
                .GroupBy(x => x.ParamHash)
                .Select(group => group.First())
                .Select((x, index) =>
                {
                    var charabase = charabases.FirstOrDefault(y => y.BaseHash == x.BaseHash);

                    return new
                    {
                        x.ParamHash,
                        Player = new Level5.Save.Logic.Player(
                            names[index],
                            GetPosition(x.PlayerPosition),
                            GetElement(x.Element),
                            GetGender(charabase != null ? charabase.Gender : 0),
                            skilltables.Skip(x.SpecialMoveOffset).Take(x.SpecialMoveCount).Select( y => y.SkillHash).ToList(),
                            new List<int>() { x.Kick },
                            x.Freedom
                        )
                    };
                })
                .ToDictionary(pair => pair.ParamHash, pair => pair.Player);

            playerFlatComboBox.Items.AddRange(names);

            ISkillConfig[] skillConfigs = GameOpened.GetSkillConfigs(true);

            GameSupports.GameFile skillTextGameFile = GameOpened.Files["skill_text"];
            T2bþ skilltext = new T2bþ(skillTextGameFile.File.Directory.GetFileFromFullPath(skillTextGameFile.Path));
            string[] skillnames = GetNames(skilltext, skillConfigs);

            Moves = skillConfigs
                .Select((x, index) => new {
                    x.SkillHash,
                    Move = new Level5.Save.Logic.Move(
                        skillnames[index],
                        null, 
                        null, 
                        x.Power, 
                        x.TPCost, 
                        x.Technique, 
                        0, 
                        0, 
                        null, 
                        null
                    )
                })
            .ToDictionary(pair => pair.SkillHash, pair => pair.Move);

            moveFlatComboBox1.Items.AddRange(skillnames);
            moveFlatComboBox2.Items.AddRange(moveFlatComboBox1.Items.Cast<Object>().ToArray());
            moveFlatComboBox3.Items.AddRange(moveFlatComboBox1.Items.Cast<Object>().ToArray());
            moveFlatComboBox4.Items.AddRange(moveFlatComboBox1.Items.Cast<Object>().ToArray());
            moveFlatComboBox5.Items.AddRange(moveFlatComboBox1.Items.Cast<Object>().ToArray());
            moveFlatComboBox6.Items.AddRange(moveFlatComboBox1.Items.Cast<Object>().ToArray());

            IAvatar[] avatars = GameOpened.GetAvatars(true);

            GameSupports.GameFile itemText = GameOpened.Files["item_text"];
            T2bþ itemnames = new T2bþ(itemText.File.Directory.GetFileFromFullPath(itemText.Path));
            string[] avatarnames = GetNames(itemnames, avatars);

            Avatars = avatars
                .Select((x, index) => new {
                    x.AvatarHash,
                    Avatar = new Level5.Save.Logic.Avatar(
                        avatarnames[index],
                        true
                    )
                })
            .ToDictionary(pair => pair.AvatarHash, pair => pair.Avatar);

            avatarFlatComboBox.Items.AddRange(avatarnames);
        }

        private void LoadFile(string filePath)
        {
            MemoryStream memoryStream = null;

            try
            {
                switch (Path.GetExtension(filePath))
                {
                    case ".ie":
                        Save = new IE();
                        break;
                    case ".ie4":
                        Save = new IE();
                        break;
                    default:
                        MessageBox.Show("Format isn't supported");
                        return;
                }

                memoryStream = new MemoryStream(File.ReadAllBytes(filePath));
                Save.Open(new BinaryDataReader(memoryStream), Players, Moves, Avatars);

                reserveListBox.Items.Clear();
                reserveListBox.Items.AddRange(Save.Game.Reserve.Select(x => x.Name).ToArray());
                saveToolStripMenuItem.Enabled = true;
            }
            finally
            {
                memoryStream?.Dispose();
            }
        }

        private void SaveEditorWindow_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string dragPath = Path.GetFullPath(files[0]);
                string dragExt = Path.GetExtension(files[0]);

                if (files.Length > 1) return;
                if (dragExt != ".ie" & dragExt != ".ie4") return;

                openFileDialog1.FileName = dragPath;
                Properties.Settings.Default.OpenFileDialogSaveEditor = Path.GetDirectoryName(openFileDialog1.FileName);
                Properties.Settings.Default.Save();
                LoadFile(openFileDialog1.FileName);
            }
        }

        private void SaveEditorWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void OpenToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.Title = "Open Inazuma Eleven save file";
            openFileDialog1.Filter = "IEGOCS/IEGOGALAXY save file (*.ie*)|*.ie*";
            openFileDialog1.FileName = null;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.OpenFileDialogSaveEditor))
            {
                openFileDialog1.InitialDirectory = Properties.Settings.Default.OpenFileDialogSaveEditor;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.OpenFileDialogSaveEditor = Path.GetDirectoryName(openFileDialog1.FileName);
                Properties.Settings.Default.Save();

                LoadFile(openFileDialog1.FileName);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Save == null) return;

            Save.Save(openFileDialog1);
        }

        private void ReserveListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reserveListBox.SelectedIndex == -1) return;

            SelectedPlayer = Save.Game.Reserve[reserveListBox.SelectedIndex];

            playerFlatComboBox.SelectedIndex = playerFlatComboBox.Items.IndexOf(SelectedPlayer.Name);
            isKeyPlayerFlatCheckBox.Checked = SelectedPlayer.IsKeyPlayer;

            levelFlatNumericUpDown.Value = SelectedPlayer.Level;
            fpFlatNumericUpDown.Value = SelectedPlayer.FP;
            tpFlatNumericUpDown.Value = SelectedPlayer.TP;
            freedomFlatNumericUpDown.Value = SelectedPlayer.Freedom;

            positionTextBox.Text = SelectedPlayer.Position.Name;
            elementTextBox.Text = SelectedPlayer.Element.Name;
            genderTextBox.Text = SelectedPlayer.Gender.Name;

            avatarFlatComboBox.SelectedIndex = avatarFlatComboBox.Items.IndexOf(SelectedPlayer.Avatar.Name);
            moveFlatComboBox1.SelectedIndex = moveFlatComboBox1.Items.IndexOf(SelectedPlayer.Moves[0].Name);
            moveFlatComboBox2.SelectedIndex = moveFlatComboBox2.Items.IndexOf(SelectedPlayer.Moves[1].Name);
            moveFlatComboBox3.SelectedIndex = moveFlatComboBox3.Items.IndexOf(SelectedPlayer.Moves[2].Name);
            moveFlatComboBox4.SelectedIndex = moveFlatComboBox4.Items.IndexOf(SelectedPlayer.Moves[3].Name);
            moveFlatComboBox5.SelectedIndex = moveFlatComboBox5.Items.IndexOf(SelectedPlayer.Moves[4].Name);
            moveFlatComboBox6.SelectedIndex = moveFlatComboBox6.Items.IndexOf(SelectedPlayer.Moves[5].Name);

            avatarLevelFlatNumericUpDown.Value = SelectedPlayer.Avatar.Level;
            moveLevelFlatNumericUpDown1.Value = SelectedPlayer.Moves[0].Level;
            moveLevelFlatNumericUpDown2.Value = SelectedPlayer.Moves[1].Level;
            moveLevelFlatNumericUpDown3.Value = SelectedPlayer.Moves[2].Level;
            moveLevelFlatNumericUpDown4.Value = SelectedPlayer.Moves[3].Level;
            moveLevelFlatNumericUpDown5.Value = SelectedPlayer.Moves[4].Level;
            moveLevelFlatNumericUpDown6.Value = SelectedPlayer.Moves[5].Level;

            unlockAvatarFlatCheckBox.Checked = SelectedPlayer.Invoke;
            unlockFlatCheckBox1.Checked = SelectedPlayer.Moves[0].Unlock;
            unlockFlatCheckBox2.Checked = SelectedPlayer.Moves[1].Unlock;
            unlockFlatCheckBox3.Checked = SelectedPlayer.Moves[2].Unlock;
            unlockFlatCheckBox4.Checked = SelectedPlayer.Moves[3].Unlock;
            unlockFlatCheckBox5.Checked = SelectedPlayer.Moves[4].Unlock;
            unlockFlatCheckBox6.Checked = SelectedPlayer.Moves[5].Unlock;

            playerGroupBox.Enabled = true;
        }

        private void PlayerFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!playerFlatComboBox.Focused || playerFlatComboBox.SelectedIndex == -1) return;

            int selectedIndex = reserveListBox.SelectedIndex;
            int reserveIndex = Save.Game.Reserve.IndexOf(SelectedPlayer);

            KeyValuePair<int, Level5.Save.Logic.Player> newPlayer = Players.FirstOrDefault(x => x.Value.Name == playerFlatComboBox.SelectedItem.ToString());
            newPlayer.Value.ID = newPlayer.Key;
            Save.Game.Reserve[reserveIndex] = Save.Game.ChangePlayer(SelectedPlayer, newPlayer.Value, true);

            reserveListBox.Items.Clear();
            reserveListBox.Items.AddRange(Save.Game.Reserve.Select(x => x.Name).ToArray());
            reserveListBox.SelectedIndex = selectedIndex;

            ReserveListBox_SelectedIndexChanged(sender, e);
        }

        private void IsKeyPlayerFlatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isKeyPlayerFlatCheckBox.Focused) return;

            SelectedPlayer.IsKeyPlayer = isKeyPlayerFlatCheckBox.Checked;
        }

        private void LevelFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!levelFlatNumericUpDown.Focused) return;

            SelectedPlayer.Level = Convert.ToInt32(levelFlatNumericUpDown.Value);
        }

        private void FpFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!fpFlatNumericUpDown.Focused) return;

            SelectedPlayer.FP = Convert.ToInt32(fpFlatNumericUpDown.Value);
        }

        private void TpFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!tpFlatNumericUpDown.Focused) return;

            SelectedPlayer.TP = Convert.ToInt32(tpFlatNumericUpDown.Value);
        }

        private void AvatarFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!avatarFlatComboBox.Focused || avatarFlatComboBox.SelectedIndex == -1) return;

            Avatar avatar = Avatars.FirstOrDefault(x => x.Value.Name == avatarFlatComboBox.SelectedItem.ToString()).Value;
            SelectedPlayer.Avatar = new Avatar(avatar, 1, 1);
        }

        private void MoveFlatComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!moveFlatComboBox1.Focused || moveFlatComboBox1.SelectedIndex == -1) return;

            Move move = Moves.FirstOrDefault(x => x.Value.Name == moveFlatComboBox1.SelectedItem.ToString()).Value;
            SelectedPlayer.Moves[0] = new Move(move, 1, true, 1);
        }

        private void MoveFlatComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!moveFlatComboBox2.Focused || moveFlatComboBox2.SelectedIndex == -1) return;

            Move move = Moves.FirstOrDefault(x => x.Value.Name == moveFlatComboBox2.SelectedItem.ToString()).Value;
            SelectedPlayer.Moves[1] = new Move(move, 1, true, 1);
        }

        private void MoveFlatComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!moveFlatComboBox3.Focused || moveFlatComboBox3.SelectedIndex == -1) return;

            Move move = Moves.FirstOrDefault(x => x.Value.Name == moveFlatComboBox3.SelectedItem.ToString()).Value;
            SelectedPlayer.Moves[2] = new Move(move, 1, true, 1);
        }

        private void MoveFlatComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!moveFlatComboBox4.Focused || moveFlatComboBox4.SelectedIndex == -1) return;

            Move move = Moves.FirstOrDefault(x => x.Value.Name == moveFlatComboBox4.SelectedItem.ToString()).Value;
            SelectedPlayer.Moves[3] = new Move(move, 1, true, 1);
        }

        private void MoveFlatComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!moveFlatComboBox5.Focused || moveFlatComboBox5.SelectedIndex == -1) return;

            Move move = Moves.FirstOrDefault(x => x.Value.Name == moveFlatComboBox5.SelectedItem.ToString()).Value;
            SelectedPlayer.Moves[4] = new Move(move, 1, true, 1);
        }

        private void MoveFlatComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!moveFlatComboBox6.Focused || moveFlatComboBox6.SelectedIndex == -1) return;

            Move move = Moves.FirstOrDefault(x => x.Value.Name == moveFlatComboBox6.SelectedItem.ToString()).Value;
            SelectedPlayer.Moves[5] = new Move(move, 1, true, 1);
        }

        private void AvatarLevelFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!avatarLevelFlatNumericUpDown.Focused) return;

            SelectedPlayer.Avatar.Level = Convert.ToInt32(avatarLevelFlatNumericUpDown.Value);
        }

        private void MoveLevelFlatNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (!moveLevelFlatNumericUpDown1.Focused) return;

            SelectedPlayer.Moves[0].Level = Convert.ToInt32(moveLevelFlatNumericUpDown1.Value);
        }

        private void MoveLevelFlatNumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (!moveLevelFlatNumericUpDown2.Focused) return;

            SelectedPlayer.Moves[1].Level = Convert.ToInt32(moveLevelFlatNumericUpDown2.Value);
        }

        private void MoveLevelFlatNumericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (!moveLevelFlatNumericUpDown3.Focused) return;

            SelectedPlayer.Moves[2].Level = Convert.ToInt32(moveLevelFlatNumericUpDown3.Value);
        }

        private void MoveLevelFlatNumericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (!moveLevelFlatNumericUpDown4.Focused) return;

            SelectedPlayer.Moves[3].Level = Convert.ToInt32(moveLevelFlatNumericUpDown4.Value);
        }

        private void MoveLevelFlatNumericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (!moveLevelFlatNumericUpDown5.Focused) return;

            SelectedPlayer.Moves[4].Level = Convert.ToInt32(moveLevelFlatNumericUpDown5.Value);
        }

        private void MoveLevelFlatNumericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (!moveLevelFlatNumericUpDown6.Focused) return;

            SelectedPlayer.Moves[5].Level = Convert.ToInt32(moveLevelFlatNumericUpDown6.Value);
        }

        private void UnlockAvatarFlatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!unlockAvatarFlatCheckBox.Focused) return;

            SelectedPlayer.Invoke = unlockAvatarFlatCheckBox.Checked;
        }

        private void UnlockFlatCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!unlockFlatCheckBox1.Focused) return;

            SelectedPlayer.Moves[0].Unlock = unlockFlatCheckBox1.Checked;
        }

        private void UnlockFlatCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!unlockFlatCheckBox2.Focused) return;

            SelectedPlayer.Moves[1].Unlock = unlockFlatCheckBox2.Checked;
        }

        private void UnlockFlatCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!unlockFlatCheckBox3.Focused) return;

            SelectedPlayer.Moves[2].Unlock = unlockFlatCheckBox3.Checked;
        }

        private void UnlockFlatCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (!unlockFlatCheckBox4.Focused) return;

            SelectedPlayer.Moves[3].Unlock = unlockFlatCheckBox4.Checked;
        }

        private void UnlockFlatCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (!unlockFlatCheckBox5.Focused) return;

            SelectedPlayer.Moves[4].Unlock = unlockFlatCheckBox5.Checked;
        }

        private void UnlockFlatCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (!unlockFlatCheckBox6.Focused) return;

            SelectedPlayer.Moves[5].Unlock = unlockFlatCheckBox6.Checked;
        }
    }
}
