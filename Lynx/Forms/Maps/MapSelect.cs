using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using Lynx.Tools;
using Lynx.Level5.Text;
using Lynx.InazumaEleven.Games;

namespace Lynx.Forms.Maps
{
    public partial class MapSelect : Form
    {
        private IGame GameOpened;

        private Dictionary<string, string> MapName;

        public MapSelect(IGame game)
        {
            GameOpened = game;
            InitializeComponent();
            InitializeMapResource();
        }

        private void InitializeMapResource()
        {
            GameSupports.GameFile systemTextGameFile = GameOpened.Files["system_text"];
            T2bþ systemText = new T2bþ(systemTextGameFile.File.Directory.GetFileFromFullPath(systemTextGameFile.Path));
            string[] mapDirectoriesNames = GameOpened.Game.Directory.GetFolderFromFullPath(GameOpened.Files["map"].Path).Folders.Select(x => x.Name).ToArray();

            MapName = new Dictionary<string, string>();
            Dictionary<string, int> nameCounter = new Dictionary<string, int>();

            for (int i = 0; i < mapDirectoriesNames.Length; i++)
            {
                // Obtain map name using crc32 hash
                uint crc32 = Crc32.Compute(Encoding.GetEncoding("Shift-JIS").GetBytes(mapDirectoriesNames[i]));
                int crc32AsInt = (int)crc32;

                string mapName = systemText.Nouns.TryGetValue(crc32AsInt, out var noun) && noun.Strings.Count > 0
                    ? noun.Strings[0].Text
                    : mapDirectoriesNames[i];

                // Check if the name has already been added
                if (nameCounter.ContainsKey(mapName))
                {
                    // Increment the counter and add the name with the counter in parentheses
                    nameCounter[mapName]++;
                    mapName = $"{mapName} ({nameCounter[mapName]})";
                }
                else
                {
                    // Add the name to the counter dictionary
                    nameCounter.Add(mapName, 1);
                }

                MapName.Add(mapDirectoriesNames[i], mapName);
            }

            mapListBox.Items.AddRange(MapName.Values.ToArray());
        }

        private void FillListBox(string searchText)
        {
            Dictionary<string, string> mapName = MapName;

            if (searchText == null)
            {
                mapName = MapName;
            }
            else
            {
                mapName = MapName
                .Where(x => x.Value.ToLower().Contains(searchText.ToLower()))
                .ToDictionary(x => x.Key, y => y.Value);
            }

            mapListBox.Items.Clear();

            // Shops
            foreach (KeyValuePair<string, string> map in mapName)
            {
                mapListBox.Items.Add(map.Value);
            }
        }

        private void MapSelect_Shown(object sender, System.EventArgs e)
        {
            mapListBox.Focus();
        }

        private void SearchTextBox_TextChanged(object sender, System.EventArgs e)
        {
            if (!searchTextBox.Focused || searchTextBox.Enabled == false || searchTextBox.Text == "Search...") return;

            FillListBox(searchTextBox.Text);
        }

        private void SearchTextBox_MouseEnter(object sender, System.EventArgs e)
        {
            if (searchTextBox.Text != "Search...") return;

            this.Focus();
            searchTextBox.Enabled = false;
            searchTextBox.Text = "";
            searchTextBox.Enabled = true;
            searchTextBox.Focus();
        }

        private void SearchTextBox_MouseLeave(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchTextBox.Text)) return;

            searchTextBox.Enabled = false;
            FillListBox(null);
            searchTextBox.Text = "Search...";
            searchTextBox.Enabled = true;
        }

        private void MapListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string mapName = mapListBox.SelectedItem.ToString();
            string mapID = MapName.FirstOrDefault(x => x.Value == mapName).Key;

            if (mapID != null)
            {
                MapEditor mapEditorWindow = new MapEditor(mapID, GameOpened);
                mapEditorWindow.Text = $"MapEditor - {mapName}";
                mapEditorWindow.ShowDialog();
            } else
            {
                MessageBox.Show("Can't open map");
            }
        }   
    }
}
