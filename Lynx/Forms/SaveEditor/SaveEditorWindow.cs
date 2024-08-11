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
            return charaparams
                .Select((charaparam, index) =>
                {
                    var charabase = charabases.FirstOrDefault(cb => cb.BaseHash == charaparam.BaseHash);
                    if (charabase != null)
                    {
                        var nameHash = charabase.NameHash;
                        if (charanames.Nouns.TryGetValue(nameHash, out var noun) && noun.Strings.Count > 0)
                        {
                            return noun.Strings[0].Text;
                        }
                    }
                    return "Name " + index;
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

        private void InitializeSaveResource()
        {
            ICharaparam[] charaparams = GameOpened.GetCharaparams();
            ICharabase[] charabases = GameOpened.GetCharabase();

            GameSupports.GameFile charaText = GameOpened.Files["chara_text"];
            T2bþ charanames = new T2bþ(charaText.File.Directory.GetFileFromFullPath(charaText.Path));
            string[] names = GetNames(charanames, charaparams, charabases);

            Players = charaparams
                .Select((x, index) => new {
                    x.ParamHash,
                    Player = new Level5.Save.Logic.Player(
                        names[index],
                        null,
                        null,
                        null,
                        new List<uint>() { 0, 0, 0, 0 },
                        new List<int>() { x.Kick },
                        x.Freedom
                    )
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

        private void OpenToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.Title = "Open Inazuma Eleven save file";
            openFileDialog1.Filter = "IEGOCS/IEGOGALAXY save file (*.ie*)|*.ie*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
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
            avatarFlatComboBox.SelectedIndex = avatarFlatComboBox.Items.IndexOf(SelectedPlayer.Avatar.Name);
            moveFlatComboBox1.SelectedIndex = moveFlatComboBox1.Items.IndexOf(SelectedPlayer.Moves[0].Name);
            moveFlatComboBox2.SelectedIndex = moveFlatComboBox2.Items.IndexOf(SelectedPlayer.Moves[1].Name);
            moveFlatComboBox3.SelectedIndex = moveFlatComboBox3.Items.IndexOf(SelectedPlayer.Moves[2].Name);
            moveFlatComboBox4.SelectedIndex = moveFlatComboBox4.Items.IndexOf(SelectedPlayer.Moves[3].Name);
            moveFlatComboBox5.SelectedIndex = moveFlatComboBox5.Items.IndexOf(SelectedPlayer.Moves[4].Name);
            moveFlatComboBox6.SelectedIndex = moveFlatComboBox6.Items.IndexOf(SelectedPlayer.Moves[5].Name);

            playerGroupBox.Enabled = true;
        }

        private void PlayerFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!playerFlatComboBox.Focused || playerFlatComboBox.SelectedIndex == -1) return;

            int selectedIndex = reserveListBox.SelectedIndex;

            Level5.Save.Logic.Player newPlayer = Players.FirstOrDefault(x => x.Value.Name == playerFlatComboBox.SelectedItem.ToString()).Value;
            int reserveIndex = Save.Game.Reserve.IndexOf(SelectedPlayer);
            Save.Game.Reserve[reserveIndex] = Save.Game.ChangePlayer(SelectedPlayer, newPlayer, true);

            reserveListBox.Items.Clear();
            reserveListBox.Items.AddRange(Save.Game.Reserve.Select(x => x.Name).ToArray());
            reserveListBox.SelectedIndex = selectedIndex;

            ReserveListBox_SelectedIndexChanged(sender, e);
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
    }
}
