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
