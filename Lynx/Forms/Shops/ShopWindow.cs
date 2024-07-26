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
using Lynx.Level5.Binary;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;
using Lynx.InazumaEleven.Common;
using Lynx.InazumaEleven.Games.GO;

namespace Lynx.Forms.Shops
{
    public partial class ShopWindow : Form
    {
        private IGame GameOpened;

        private Dictionary<int, string> ItemNames;

        private List<ICommunityInfo> Communities;

        private Dictionary<string, string> ShopsName;

        private Dictionary<string, List<IShopConfig>> Shops;     

        private T2bþ Systemtext;

        private T2bþ EncountText;

        private string SelectedShopID;

        private bool CanEdit;

        private TreeNode RightClickNode;

        public ShopWindow(IGame game)
        {
            GameOpened = game;
            InitializeComponent();
            InitializeShopResource();
        }

        private Dictionary<int, string> GetNames(IItemConfig[] items, T2bþ itemtext)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();

            var nameCounts = new Dictionary<string, int>();
            foreach (var itemconfig in items.Select((item, index) => new { item, index }))
            {
                string baseName;
                if (itemtext.Nouns.TryGetValue(itemconfig.item.NameID, out var noun) && noun.Strings.Count > 0)
                {
                    baseName = noun.Strings[0].Text;
                }
                else
                {
                    baseName = "Name " + itemconfig.index;
                }

                if (nameCounts.ContainsKey(baseName))
                {
                    nameCounts[baseName]++;
                    baseName += $" ({nameCounts[baseName]})";
                }
                else
                {
                    nameCounts[baseName] = 1;
                }

                output.Add(itemconfig.item.ItemID, baseName);
            }

            return output;
        }

        private void InitializeShopResource()
        {
            TreeNode shopsNode = new TreeNode("Shops");
            TreeNode communitiespNode = new TreeNode("Communities");

            GameSupports.GameFile itemTextGameFile = GameOpened.Files["item_text"];
            GameSupports.GameFile systemTextGameFile = GameOpened.Files["system_text"];
            GameSupports.GameFile encountTextGameFile = GameOpened.Files["encount_area_text"];
            T2bþ itemtext = new T2bþ(itemTextGameFile.File.Directory.GetFileFromFullPath(itemTextGameFile.Path));
            Systemtext = new T2bþ(systemTextGameFile.File.Directory.GetFileFromFullPath(systemTextGameFile.Path));
            EncountText = new T2bþ(systemTextGameFile.File.Directory.GetFileFromFullPath(encountTextGameFile.Path));

            // Fill item in datagrid combo box
            IItemConfig[] items = GameOpened.GetItems("all");
            ItemNames = GetNames(items, itemtext);
            ItemNames.Add(0, "No Item");
            ((DataGridViewComboBoxColumn)shopDataGridView.Columns[0]).Items.AddRange(ItemNames.Values.ToArray());

            Communities = GameOpened.GetCommunities().ToList();

            Shops = new Dictionary<string, List<IShopConfig>>();
            string[] shopDirectoriesNames = GameOpened.Game.Directory.GetFolderFromFullPath(GameOpened.Files["shop"].Path)
                .Files.Where(x => x.Key != "community_config.cfg.bin").Select(x => x.Key.Replace("shop_", "").Replace(".cfg.bin", "")).ToArray();
            ShopsName = new Dictionary<string, string>();
            Dictionary<string, int> nameCounter = new Dictionary<string, int>();

            for (int i = 0; i < shopDirectoriesNames.Length; i++)
            {
                // Get shop data
                List<IShopConfig> shopConfig = GameOpened.GetShop(shopDirectoriesNames[i]).ToList();
                Shops.Add(shopDirectoriesNames[i], shopConfig);

                // Obtain map name using crc32 hash
                uint crc32 = Crc32.Compute(Encoding.GetEncoding("Shift-JIS").GetBytes(shopDirectoriesNames[i]));
                int crc32AsInt = (int)crc32;

                ICommunityInfo communityInfo = Communities.FirstOrDefault(x => x.ShopID == crc32AsInt);
                if (communityInfo != null)
                {
                    string communityName = Systemtext.Texts.TryGetValue(communityInfo.NameID, out var noun) && noun.Strings.Count > 0
                                        ? noun.Strings[0].Text
                                        : shopDirectoriesNames[i];

                    // Check if the name has already been added
                    if (nameCounter.ContainsKey(communityName))
                    {
                        // Increment the counter and add the name with the counter in parentheses
                        nameCounter[communityName]++;
                        communityName = $"{communityName} ({nameCounter[communityName]})";
                    }
                    else
                    {
                        // Add the name to the counter dictionary
                        nameCounter.Add(communityName, 1);
                    }

                    communitiespNode.Nodes.Add(communityName);
                    ShopsName.Add(shopDirectoriesNames[i], communityName);
                } 
                else
                {
                    shopsNode.Nodes.Add(shopDirectoriesNames[i]);
                    ShopsName.Add(shopDirectoriesNames[i], shopDirectoriesNames[i]);
                }
            }

            shopTreeView.Nodes.Add(shopsNode);
            shopTreeView.Nodes.Add(communitiespNode);

            shopDataGridView.Focus();
        }

        private void MakeCommunityTreeNode()
        {
            TreeNode communityNode = shopTreeView.Nodes[1];
            communityNode.Nodes.Clear();

            Dictionary<string, int> nameCounter = new Dictionary<string, int>();
            for (int i = 0; i < ShopsName.Count; i++)
            {
                var shop = ShopsName.ElementAt(i);

                // Get crc32 hash
                uint crc32 = Crc32.Compute(Encoding.GetEncoding("Shift-JIS").GetBytes(shop.Key));
                int crc32AsInt = (int)crc32;

                ICommunityInfo communityInfo = Communities.FirstOrDefault(x => x.ShopID == crc32AsInt);
                if (communityInfo != null)
                {
                    string communityName = Systemtext.Texts.TryGetValue(communityInfo.NameID, out var noun) && noun.Strings.Count > 0
                                        ? noun.Strings[0].Text
                                        : shop.Key;

                    if (nameCounter.ContainsKey(communityName))
                    {
                        nameCounter[communityName]++;
                        communityName = $"{communityName} ({nameCounter[communityName]})";
                    }
                    else
                    {
                        nameCounter.Add(communityName, 1);
                    }

                    communityNode.Nodes.Add(communityName);
                    ShopsName[shop.Key] = communityName;
                }
            }
        }

        private void FillTreeView(string searchText)
        {
            Dictionary<string, string> matchShop = ShopsName;

            if (searchText == null)
            {
                matchShop = ShopsName;
            } else
            {
                matchShop = ShopsName
                .Where(x => x.Value.ToLower().Contains(searchText.ToLower()))
                .ToDictionary(x => x.Key, y => y.Value);
            }

            TreeNode shopsNode = new TreeNode("Shops");
            TreeNode communitiesNode = new TreeNode("Communities");

            shopTreeView.Nodes.Clear();

            // Shops
            foreach (KeyValuePair<string, string> shop in matchShop)
            {
                // Get crc32 hash
                uint crc32 = Crc32.Compute(Encoding.GetEncoding("Shift-JIS").GetBytes(shop.Key));
                int crc32AsInt = (int)crc32;

                ICommunityInfo communityInfo = Communities.FirstOrDefault(x => x.ShopID == crc32AsInt);
                if (communityInfo != null)
                {
                    communitiesNode.Nodes.Add(shop.Value);
                }
                else
                {
                    shopsNode.Nodes.Add(shop.Value);
                    
                }
            }

            shopTreeView.Nodes.Add(shopsNode);
            shopTreeView.Nodes.Add(communitiesNode);
        }

        private void ResetDatagridViewIndex()
        {
            int index = 0;
            foreach (DataGridViewRow row in shopDataGridView.Rows)
            {
                row.Cells[2].Value = index;
                index++;
            }

        }
        private void ShopTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                TreeNode parentNode = e.Node.Parent;

                // clear
                shopDataGridView.Rows.Clear();

                // Get shop 
                SelectedShopID = ShopsName.FirstOrDefault(x => x.Value == e.Node.Text).Key;
                if (SelectedShopID != null)
                {
                    List<IShopConfig> myShop = Shops[SelectedShopID];

                    // Display shop info
                    fileTextBox.Text = SelectedShopID;

                    if (parentNode.Text == "Shops")
                    {
                        label1.Enabled = false;
                        label2.Enabled = false;
                        nameTextBox.Text = "";
                        nameTextBox.Enabled = false;
                        areaTextBox.Text = "";
                        areaTextBox.Enabled = false;
                    } else
                    {
                        // Obtain map name using crc32 hash
                        uint crc32 = Crc32.Compute(Encoding.GetEncoding("Shift-JIS").GetBytes(SelectedShopID));
                        int crc32AsInt = (int)crc32;

                        // Get community
                        ICommunityInfo community = Communities.FirstOrDefault(x => x.ShopID == crc32AsInt);

                        if (community != null)
                        {
                            string mapName = Systemtext.Texts.TryGetValue(community.NameID, out var noun) && noun.Strings.Count > 0
                                        ? noun.Strings[0].Text
                                        : "Unamed";

                            string areaName = EncountText.Nouns.TryGetValue(community.AreaID, out var noun2) && noun.Strings.Count > 0
                                        ? noun2.Strings[0].Text
                                        : "Unamed";

                            label1.Enabled = true;
                            label2.Enabled = true;
                            nameTextBox.Text = mapName;
                            nameTextBox.Enabled = true;
                            areaTextBox.Text = areaName;
                            areaTextBox.Enabled = true;
                        } else
                        {
                            label1.Enabled = false;
                            label2.Enabled = false;
                            nameTextBox.Text = "";
                            nameTextBox.Enabled = false;
                            areaTextBox.Text = "";
                            areaTextBox.Enabled = false;
                        }
                    }

                    // Display shop content
                    int rowIndex = 0;
                    foreach (IShopConfig item in myShop) {

                        DataGridViewComboBoxCell comboBox = (shopDataGridView.Rows[0].Cells[0] as DataGridViewComboBoxCell);
                        shopDataGridView.Rows.Add(comboBox.Items[comboBox.Items.IndexOf(ItemNames[item.ItemID])], item.Condition, rowIndex);
                        rowIndex++;
                    }
                }

                shopDataGridView.Enabled = true;
                infoGroupBox.Enabled = true;
                CanEdit = true;
            }
        }

        private void ShopTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RightClickNode = shopTreeView.GetNodeAt(e.X, e.Y);

                if (RightClickNode != null)
                {
                    if (RightClickNode.Parent == null)
                    {
                        contextMenuStrip2.Items[1].Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip2.Items[1].Enabled = true;
                    }

                    contextMenuStrip2.Show(shopTreeView, e.Location);
                }
            }
        }

        private void FileTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void NameTextBox_Click(object sender, EventArgs e)
        {
            // Get crc32
            uint crc32 = Crc32.Compute(Encoding.GetEncoding("Shift-JIS").GetBytes(SelectedShopID));
            int crc32AsInt = (int)crc32;

            // Get community
            ICommunityInfo community = Communities.FirstOrDefault(x => x.ShopID == crc32AsInt);
            if (community != null)
            {
                Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["system_text"].Path), Systemtext, true, false, community.NameID);
                nyanko.ShowDialog();
                Systemtext = nyanko.T2bþFileOpened;

                // Update current name
                if (nyanko.SelectedHash > 0)
                {
                    community.NameID = nyanko.SelectedHash;
                }

                // Update name
                string communityName = Systemtext.Texts.TryGetValue(community.NameID, out var noun) && noun.Strings.Count > 0
                                        ? noun.Strings[0].Text
                                        : SelectedShopID;

                int selectedIndex = shopTreeView.Nodes[1].Nodes.IndexOf(shopTreeView.SelectedNode);
                MakeCommunityTreeNode();
                shopTreeView.SelectedNode = shopTreeView.Nodes[1].Nodes[selectedIndex];
            }
        }

        private void AreaTextBox_Click(object sender, EventArgs e)
        {
            // Get crc32
            uint crc32 = Crc32.Compute(Encoding.GetEncoding("Shift-JIS").GetBytes(SelectedShopID));
            int crc32AsInt = (int)crc32;

            // Get community
            ICommunityInfo community = Communities.FirstOrDefault(x => x.ShopID == crc32AsInt);
            if (community != null)
            {
                Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["encount_area_text"].Path), EncountText, true, true, community.AreaID);
                nyanko.ShowDialog();
                EncountText = nyanko.T2bþFileOpened;

                // Update current name
                if (nyanko.SelectedHash > 0)
                {
                    community.AreaID = nyanko.SelectedHash;
                }

                // Update name
                string areaName = EncountText.Nouns.TryGetValue(community.AreaID, out var noun) && noun.Strings.Count > 0
                                        ? noun.Strings[0].Text
                                        : "Unamed";

                areaTextBox.Text = areaName;
            }
        }

        private void ShopDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (CanEdit)
            {
                if (shopDataGridView.IsCurrentCellDirty)
                {
                    shopDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void ShopDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!CanEdit) return;

            int cellIndex = shopDataGridView.CurrentCell.ColumnIndex;
            int rowIndex = shopDataGridView.CurrentCell.RowIndex;

            // Add Row
            if (rowIndex + 2 == shopDataGridView.RowCount)
            {
                if (cellIndex == 0)
                {
                    shopDataGridView.Rows[rowIndex].Cells[1].Value = 0;
                } else
                {
                    DataGridViewComboBoxCell comboBox = (shopDataGridView.Rows[0].Cells[0] as DataGridViewComboBoxCell);
                    shopDataGridView.Rows[rowIndex].Cells[0].Value = comboBox.Items[comboBox.Items.IndexOf("No Item")];
                }

                Shops[SelectedShopID].Add(GameOpened.GetEmptyObject<IShopConfig>());
            }

            ResetDatagridViewIndex();

            int shopIndex = Convert.ToInt32(shopDataGridView.Rows[rowIndex].Cells[2].Value.ToString());

            if (cellIndex == 0)
            {
                string itemName = shopDataGridView.Rows[rowIndex].Cells[0].Value.ToString();
                int itemConfig = ItemNames.FirstOrDefault(x => x.Value == itemName).Key;

                if (itemConfig != 0)
                {
                    Shops[SelectedShopID][shopIndex].ItemID = itemConfig;
                }
            }
            else if (cellIndex == 1)
            {
                var newText = shopDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString() == "0" 
                      ? "" : shopDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                Shops[SelectedShopID][shopIndex].Condition = newText;
            }
        }

        private void DeleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = shopDataGridView.CurrentCell.RowIndex;

            if (!shopDataGridView.Rows[rowIndex].IsNewRow)
            {
                CanEdit = false;
                int shopIndex = Convert.ToInt32(shopDataGridView.Rows[rowIndex].Cells[2].Value.ToString());
                Shops[SelectedShopID].RemoveAt(shopIndex);
                shopDataGridView.Rows.RemoveAt(rowIndex);
                ResetDatagridViewIndex();
                CanEdit = true;
            }
        }

        private void ShopDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                shopDataGridView.Rows[e.RowIndex].Selected = true;
                shopDataGridView.CurrentCell = shopDataGridView.Rows[e.RowIndex].Cells[1];
                contextMenuStrip1.Show(shopDataGridView, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void ShopWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach(KeyValuePair<string, List<IShopConfig>> shop in Shops)
            {
                GameOpened.SaveShop(shop.Key, shop.Value.ToArray());
            }

            GameOpened.SaveCommunities(Communities.ToArray());
            GameOpened.SaveTextFile(GameOpened.Files["system_text"], Systemtext);
            GameOpened.SaveTextFile(GameOpened.Files["encount_area_text"], EncountText);
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RightClickNode == null) return;

            int typeIndex = 0;

            TreeNode parentNode = RightClickNode.Parent;
            if (parentNode != null)
            {
                typeIndex = Convert.ToInt32(parentNode.Text == "Communities");
            } else
            {
                typeIndex = Convert.ToInt32(RightClickNode.Text == "Communities");
            }

            string[] fileNames = GameOpened.Game.Directory.GetFolderFromFullPath(GameOpened.Files["shop"].Path).Files.Keys.ToArray();
            int[] shopNums = fileNames
                .Where(x => x.StartsWith("shop_shp"))
                .Select(x => Convert.ToInt32(x.Replace("shop_shp", "").Replace(".cfg.bin", "")))
                .ToArray();
            int missingNumber = Enumerable.Range(1, int.MaxValue).Except(shopNums).First();

            string shopName = $"shp{ missingNumber.ToString().PadLeft(4, '0')}";
            string newFileName = $"shop_{shopName}.cfg.bin";

            // Create empty shop
            ShopsName.Add(shopName, shopName);
            Shops.Add(shopName, new List<IShopConfig>());

            // Create shop file
            VirtualDirectory shopDirectory = GameOpened.Game.Directory.GetFolderFromFullPath(GameOpened.Files["shop"].Path);
            CfgBin newShopCfgBin = new CfgBin();
            shopDirectory.AddFile(newFileName, new SubMemoryStream(newShopCfgBin.Save()));
            
            // Add Community
            if (typeIndex == 1)
            {
                Communities.Add(GameOpened.GetEmptyObject<ICommunityInfo>());
            }

            // Update tree view
            TreeNode newNode = new TreeNode(shopName);
            shopTreeView.Nodes[typeIndex].Nodes.Add(newNode);
            shopTreeView.SelectedNode = newNode;
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RightClickNode == null) return;

            TreeNode parentNode = RightClickNode.Parent;
            if (parentNode != null)
            {
                int typeIndex = Convert.ToInt32(parentNode.Name == "Communities");
                int nodeIndex = shopTreeView.Nodes[typeIndex].Nodes.IndexOf(RightClickNode);
                string shopID = ShopsName.FirstOrDefault(x => x.Value == RightClickNode.Text).Key;

                // Get crc32 hash
                uint crc32 = Crc32.Compute(Encoding.GetEncoding("Shift-JIS").GetBytes(shopID));
                int crc32AsInt = (int)crc32;

                ShopsName.Remove(shopID);
                Shops.Remove(shopID);

                // Remove file from archive opened
                VirtualDirectory shopFolder = GameOpened.Game.Directory.GetFolderFromFullPath(GameOpened.Files["shop"].Path);
                shopFolder.Files.Remove($"shop_{shopID}.cfg.bin");

                // Update datagridview
                infoGroupBox.Enabled = false;
                shopDataGridView.Enabled = false;
                shopTreeView.Nodes[typeIndex].Nodes.RemoveAt(nodeIndex);

                if (typeIndex == 1)
                {
                    ICommunityInfo community = Communities.FirstOrDefault(x => x.ShopID == crc32AsInt);

                    if (community != null)
                    {
                        Communities.Remove(community);
                    }                 
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!searchTextBox.Focused || searchTextBox.Enabled == false || searchTextBox.Text == "Search...") return;

            FillTreeView(searchTextBox.Text);
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
            FillTreeView(null);
            searchTextBox.Text = "Search...";
            searchTextBox.Enabled = true;
        }
    }
}
