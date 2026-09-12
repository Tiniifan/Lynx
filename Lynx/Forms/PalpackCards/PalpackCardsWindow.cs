using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using StudioElevenLib.Tools;
using StudioElevenLib.Level5.Text;
using StudioElevenLib.Level5.Text.Logic;
using StudioElevenLib.Level5.Image;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;
using Lynx.InazumaEleven.Common;
using OfficeOpenXml;
using Microsoft.WindowsAPICodePack.Dialogs;
using Player = Lynx.InazumaEleven.Logic.Player;
using static Lynx.InazumaEleven.Games.GO.GOSupport;

namespace Lynx.Forms.PalpackCards
{
    public partial class PalpackCardsWindow : Form
    {
        private const string NicknameToken = "{nickname}";

        private Game GameOpened;

        private List<IItemPalpackCard> PalpackCards;

        private List<IItemPalpackCard> PalpackCardsFiltred;

        private List<IItemConfig> AvailableItems;

        private List<ICharabase> Charabases;

        private List<Player> Players;

        private IItemPalpackCard SelectedPalpackCard;

        private T2bþ Itemtext;

        private T2bþ Charanames;

        private Dictionary<int, string> PalpackNamesDict;

        private Dictionary<int, string> PalpackIdsDict;

        private string[] AvailableItemNames;

        private string[] PlayerNames;

        public PalpackCardsWindow(Game game)
        {
            GameOpened = game;
            InitializeComponent();
            InitializePalpackResources();
        }

        #region Initialization / Resources

        private void InitializePalpackResources()
        {
            // Load text resources
            GameSupports.GameFile itemTextFile = GameOpened.Files["item_text"];
            Itemtext = new T2bþ(itemTextFile.File.Directory.GetFileFromFullPath(itemTextFile.Path));

            GameSupports.GameFile charaTextFile = GameOpened.Files["chara_text"];
            Charanames = new T2bþ(charaTextFile.File.Directory.GetFileFromFullPath(charaTextFile.Path));

            // AvailableItems: every item EXCEPT palpack cards (equipment, consumable, important, uniform, avatar, director...)
            AvailableItems = GameOpened.GetItems("all")
                .Where(x => !(x is IItemPalpackCard))
                .Select(x => (IItemConfig)x)
                .ToList();

            // PalpackCards: only the kizunax (palpack) entries
            PalpackCards = GameOpened.GetItems("kizunax")
                .Select(x => (IItemPalpackCard)x)
                .ToList();

            // Players / Charabases, needed to resolve recruited character info
            Charabases = GameOpened.GetCharabase().ToList();
            Players = GameOpened.GetCharaparams()
                .Select(x => new Player(x, new List<ISkillTable>()))
                .ToList();

            // Build display name caches
            AvailableItemNames = GetItemNames(AvailableItems.ToArray());
            PlayerNames = GetPlayerNames(Players.ToArray());

            SetPalpackNames();
            BuildPalpackIdsDict();

            // Fill the player combobox (used to pick the recruited character on a palpack card)
            playerFlatComboBox.Items.Clear();
            playerFlatComboBox.Items.AddRange(PlayerNames);

            // Fill the 4 condition slots comboboxes
            for (int i = 1; i <= 4; i++)
            {
                ComboBox conditionTypeComboBox = Controls.Find($"conditionTypeFlatComboBox{i}", true).FirstOrDefault() as ComboBox;
                ComboBox conditionObjectComboBox = Controls.Find($"conditionObjectFlatComboBox{i}", true).FirstOrDefault() as ComboBox;
                ComboBox conditionPlayerComboBox = Controls.Find($"conditionPlayerFlatComboBox{i}", true).FirstOrDefault() as ComboBox;

                if (conditionTypeComboBox != null)
                {
                    conditionTypeComboBox.Items.Clear();
                    conditionTypeComboBox.Items.AddRange(EnumHelper.GetValues<PalpackConditionTypes>().Select(s => s.Name).ToArray());
                }

                if (conditionObjectComboBox != null)
                {
                    conditionObjectComboBox.Items.Clear();
                    conditionObjectComboBox.Items.AddRange(AvailableItemNames);
                }

                if (conditionPlayerComboBox != null)
                {
                    conditionPlayerComboBox.Items.Clear();
                    conditionPlayerComboBox.Items.AddRange(PlayerNames);
                }
            }
        }

        private void SetPalpackNames()
        {
            palpackListBox.Items.Clear();

            PalpackNamesDict = GetNames(PalpackCards.ToArray());
            palpackListBox.Items.AddRange(PalpackNamesDict.Where(x => x.Key != 0x0).Select(x => x.Value).ToArray());
        }

        private void BuildPalpackIdsDict()
        {
            // Brute forces the "ikaXXXX" text id for every palpack card once, so searching by id doesn't
            // need to re-hash 10000 candidates per keystroke

            PalpackIdsDict = new Dictionary<int, string>();

            foreach (IItemPalpackCard card in PalpackCards)
            {
                if (!PalpackIdsDict.ContainsKey(card.ItemID))
                {
                    PalpackIdsDict[card.ItemID] = FindPalpackID(card.ItemID);
                }
            }
        }

        #endregion

        #region Name / Id helpers

        private bool SameSkillID(int id, string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return id == crc32;
        }

        private int ComputeHash(string name)
        {
            return unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
        }

        private string FindPalpackID(int itemID = 0x0)
        {
            if (itemID == 0x0)
            {
                itemID = SelectedPalpackCard.ItemID;
            }

            for (int i = 0; i < 10000; i++)
            {
                string tryPalpackID = $"ika{i.ToString().PadLeft(4, '0')}";

                if (SameSkillID(itemID, tryPalpackID))
                {
                    return tryPalpackID;
                }
            }

            // Not found
            return itemID.ToString("X8");
        }

        private string GetItemName(IItemConfig item)
        {
            if (item.ItemID == 0x00) return " ";

            return Itemtext.Nouns.TryGetValue(item.NameID, out var noun) && noun.Strings.Count > 0
                ? noun.Strings[0].Text
                : "Item";
        }

        private string[] GetItemNames(IItemConfig[] items)
        {
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();
            List<string> output = new List<string>();

            int index = 0;
            foreach (var item in items)
            {
                string name = item.ItemID == 0x00
                    ? " "
                    : Itemtext.Nouns.TryGetValue(item.NameID, out var noun) && noun.Strings.Count > 0
                        ? noun.Strings[0].Text
                        : $"Item {index}";

                if (nameCounts.ContainsKey(name))
                {
                    nameCounts[name]++;
                    name += $" ({nameCounts[name]})";
                }
                else
                {
                    nameCounts[name] = 1;
                }

                output.Add(name);
                index++;
            }

            return output.ToArray();
        }

        private Dictionary<int, string> GetNames(IItemPalpackCard[] cards)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var card in cards)
            {
                string name = card.ItemID == 0x00
                    ? " "
                    : Itemtext.Nouns.TryGetValue(card.NameID, out var noun) && noun.Strings.Count > 0
                        ? noun.Strings[0].Text
                        : $"Palpack {index}";

                if (nameCounts.ContainsKey(name))
                {
                    nameCounts[name]++;
                    name += $" ({nameCounts[name]})";
                }
                else
                {
                    nameCounts[name] = 1;
                }

                output[card.ItemID] = name;
                index++;
            }

            return output;
        }

        private string[] GetPlayerNames(Player[] players)
        {
            return players.Select((player, index) =>
            {
                var charabase = Charabases.FirstOrDefault(cb => cb.BaseHash == player.Charaparam.BaseHash);

                if (charabase != null && Charanames.Nouns.TryGetValue(charabase.NameHash, out var noun) && noun.Strings.Count > 0)
                {
                    return noun.Strings[0].Text;
                }

                return "Player " + index;
            }).ToArray();
        }

        private void SetFace(Player player)
        {
            facePictureBox.Image = GetFaceImage(player);
        }

        private Image GetFaceImage(Player player)
        {
            if (player == null) return null;

            ICharabase charabase = Charabases.FirstOrDefault(cb => cb.BaseHash == player.Charaparam.BaseHash);
            if (charabase == null) return null;

            string fileCode;
            switch (charabase.CharaBaseType)
            {
                case 1: fileCode = "cp"; break;
                case 3: fileCode = "cn"; break;
                case 4: fileCode = "ca"; break;
                default: return null;
            }

            GameSupports.GameFile faceInfo = GameOpened.Files["face"];
            VirtualDirectory faceFolder = faceInfo.File.Directory.GetFolderFromFullPath(faceInfo.Path);

            string faceFileName = fileCode + charabase.ModelNumber.ToString().PadLeft(4, '0') + "a.xi";

            if (!faceFolder.Files.ContainsKey(faceFileName)) return null;

            try
            {
                byte[] imageData = faceInfo.File.Directory.GetFileFromFullPath(faceInfo.Path + "/" + faceFileName);
                return Imager.Open(imageData).Bitmap;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Prerequisites (conditions) helpers

        // The 4 condition slots (PrerequisitesType1-4 / PrerequisitesId1-4) are not indexable as an
        // array on IItemPalpackCard, so we switch on the slot number instead of using reflection.

        private int GetPrerequisitesType(int n)
        {
            switch (n)
            {
                case 1: return SelectedPalpackCard.PrerequisitesType1;
                case 2: return SelectedPalpackCard.PrerequisitesType2;
                case 3: return SelectedPalpackCard.PrerequisitesType3;
                case 4: return SelectedPalpackCard.PrerequisitesType4;
                default: return 0;
            }
        }

        private int GetPrerequisitesId(int n)
        {
            switch (n)
            {
                case 1: return SelectedPalpackCard.PrerequisitesId1;
                case 2: return SelectedPalpackCard.PrerequisitesId2;
                case 3: return SelectedPalpackCard.PrerequisitesId3;
                case 4: return SelectedPalpackCard.PrerequisitesId4;
                default: return 0;
            }
        }

        private void SetPrerequisitesType(int n, int value)
        {
            switch (n)
            {
                case 1: SelectedPalpackCard.PrerequisitesType1 = value; break;
                case 2: SelectedPalpackCard.PrerequisitesType2 = value; break;
                case 3: SelectedPalpackCard.PrerequisitesType3 = value; break;
                case 4: SelectedPalpackCard.PrerequisitesType4 = value; break;
            }
        }

        private void SetPrerequisitesId(int n, int value)
        {
            switch (n)
            {
                case 1: SelectedPalpackCard.PrerequisitesId1 = value; break;
                case 2: SelectedPalpackCard.PrerequisitesId2 = value; break;
                case 3: SelectedPalpackCard.PrerequisitesId3 = value; break;
                case 4: SelectedPalpackCard.PrerequisitesId4 = value; break;
            }
        }

        private int GetPrerequisitesType(IItemPalpackCard card, int n)
        {
            // Reads the PrerequisitesType of slot n for a given card (unlike GetPrerequisitesType, this
            // is not tied to SelectedPalpackCard, so it can be used for any card, e.g. during export).

            switch (n)
            {
                case 1: return card.PrerequisitesType1;
                case 2: return card.PrerequisitesType2;
                case 3: return card.PrerequisitesType3;
                case 4: return card.PrerequisitesType4;
                default: return 0;
            }
        }

        private int GetPrerequisitesId(IItemPalpackCard card, int n)
        {
            // Reads the PrerequisitesId of slot n for a given card (see GetPrerequisitesType(card, n)).

            switch (n)
            {
                case 1: return card.PrerequisitesId1;
                case 2: return card.PrerequisitesId2;
                case 3: return card.PrerequisitesId3;
                case 4: return card.PrerequisitesId4;
                default: return 0;
            }
        }

        private string GetConditionDisplayText(IItemPalpackCard card, int n)
        {
            // Builds a readable "Item: Name" / "Player: Name" / "" text for condition slot n of a card,
            // used in the excel export.

            PalpackConditionTypes type = (PalpackConditionTypes)GetPrerequisitesType(card, n);
            int id = GetPrerequisitesId(card, n);

            switch (type)
            {
                case PalpackConditionTypes.Item:
                    IItemConfig item = AvailableItems.FirstOrDefault(x => x.ItemID == id);
                    return item != null ? $"Item: {GetItemName(item)}" : "";

                case PalpackConditionTypes.Player:
                    Player conditionPlayer = Players.FirstOrDefault(p => p.Charaparam.ParamHash == id);
                    if (conditionPlayer != null)
                    {
                        var charabase = Charabases.FirstOrDefault(cb => cb.BaseHash == conditionPlayer.Charaparam.BaseHash);
                        string playerName = charabase != null && Charanames.Nouns.ContainsKey(charabase.NameHash)
                            ? Charanames.Nouns[charabase.NameHash].Strings[0].Text
                            : "";
                        return $"Player: {playerName}";
                    }
                    return "";

                default:
                    return "";
            }
        }

        private int GetConditionIndex(string controlName, string prefix)
        {
            // Extracts the slot number (1-4) from a control name given its prefix,
            // e.g. GetConditionIndex("conditionTypeFlatComboBox3", "conditionTypeFlatComboBox") -> 3

            string suffix = controlName.Replace(prefix, "");
            return int.TryParse(suffix, out int result) ? result : -1;
        }

        private void ApplyConditionVisibility(int n, PalpackConditionTypes type)
        {
            // Applies the visibility/enabled/text rules described for the condition controls of slot n.

            Label objectLabel = Controls.Find($"objectLabel{n}", true).FirstOrDefault() as Label;
            ComboBox conditionObjectComboBox = Controls.Find($"conditionObjectFlatComboBox{n}", true).FirstOrDefault() as ComboBox;
            ComboBox conditionPlayerComboBox = Controls.Find($"conditionPlayerFlatComboBox{n}", true).FirstOrDefault() as ComboBox;

            switch (type)
            {
                case PalpackConditionTypes.None:
                    if (objectLabel != null) objectLabel.Text = "Object";
                    if (conditionObjectComboBox != null)
                    {
                        conditionObjectComboBox.Visible = true;
                        conditionObjectComboBox.Enabled = false;

                        // Clear the leftover text from a previous Item selection
                        conditionObjectComboBox.SelectedIndex = -1;
                        conditionObjectComboBox.Text = "";
                    }
                    if (conditionPlayerComboBox != null) conditionPlayerComboBox.Visible = false;
                    break;

                case PalpackConditionTypes.Item:
                    if (objectLabel != null) objectLabel.Text = "Item";
                    if (conditionObjectComboBox != null)
                    {
                        conditionObjectComboBox.Visible = true;
                        conditionObjectComboBox.Enabled = true;
                    }
                    if (conditionPlayerComboBox != null) conditionPlayerComboBox.Visible = false;
                    break;

                case PalpackConditionTypes.Player:
                    if (objectLabel != null) objectLabel.Text = "Player";
                    if (conditionPlayerComboBox != null)
                    {
                        conditionPlayerComboBox.Visible = true;
                        conditionPlayerComboBox.Enabled = true;
                    }
                    if (conditionObjectComboBox != null) conditionObjectComboBox.Visible = false;
                    break;
            }
        }

        private void UpdateConditionValueDisplay(int n)
        {
            // Refreshes the display of condition slot n from the currently selected palpack card.

            ComboBox conditionTypeComboBox = Controls.Find($"conditionTypeFlatComboBox{n}", true).FirstOrDefault() as ComboBox;
            ComboBox conditionObjectComboBox = Controls.Find($"conditionObjectFlatComboBox{n}", true).FirstOrDefault() as ComboBox;
            ComboBox conditionPlayerComboBox = Controls.Find($"conditionPlayerFlatComboBox{n}", true).FirstOrDefault() as ComboBox;

            if (conditionTypeComboBox == null) return;

            int type = GetPrerequisitesType(n);
            conditionTypeComboBox.SelectedIndex = type;

            ApplyConditionVisibility(n, (PalpackConditionTypes)type);

            int id = GetPrerequisitesId(n);

            if (type == (int)PalpackConditionTypes.Item && conditionObjectComboBox != null)
            {
                int itemIndex = AvailableItems.FindIndex(item => item.ItemID == id);
                conditionObjectComboBox.SelectedIndex = itemIndex;
            }
            else if (type == (int)PalpackConditionTypes.Player && conditionPlayerComboBox != null)
            {
                int playerIndex = Players.FindIndex(p => p.Charaparam.ParamHash == id);
                conditionPlayerComboBox.SelectedIndex = playerIndex;
            }
        }

        #endregion

        #region Save / Export

        private void Save()
        {
            GameOpened.SaveItems(PalpackCards.Cast<ItemConfigPalpackCard>().ToArray());
            GameOpened.SaveTextFile(GameOpened.Files["item_text"], Itemtext);
        }

        private void ExportAsCfgbinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Save();

                (string, byte[]) itemconfig = GameOpened.GetFileNameAndContent("item_config");
                (string, byte[]) itemtext = GameOpened.GetFileNameAndContent("item_text");

                File.WriteAllBytes(Path.Combine(dialog.FileName, itemconfig.Item1), itemconfig.Item2);
                File.WriteAllBytes(Path.Combine(dialog.FileName, itemtext.Item1), itemtext.Item2);

                MessageBox.Show("Items data exported!");
            }
        }

        private void ExportAscsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.SaveFileDialogSheet))
            {
                saveFileDialog1.InitialDirectory = Properties.Settings.Default.SaveFileDialogSheet;
            }

            saveFileDialog1.Filter = "XLSX Files(*.xlsx) | *.xlsx";
            saveFileDialog1.Title = "Export available items as sheet";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportAvailableItemsToExcel(saveFileDialog1.FileName);

                Properties.Settings.Default.SaveFileDialogSheet = Path.GetDirectoryName(saveFileDialog1.FileName);
                Properties.Settings.Default.Save();
            }
        }

        private void ExportAsCfgbinToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Save();

                (string, byte[]) itemconfig = GameOpened.GetFileNameAndContent("item_config");
                (string, byte[]) itemtext = GameOpened.GetFileNameAndContent("item_text");

                File.WriteAllBytes(Path.Combine(dialog.FileName, itemconfig.Item1), itemconfig.Item2);
                File.WriteAllBytes(Path.Combine(dialog.FileName, itemtext.Item1), itemtext.Item2);

                MessageBox.Show("Palpack cards data exported!");
            }
        }

        private void ExportAscsvToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.SaveFileDialogSheet))
            {
                saveFileDialog1.InitialDirectory = Properties.Settings.Default.SaveFileDialogSheet;
            }

            saveFileDialog1.Filter = "XLSX Files(*.xlsx) | *.xlsx";
            saveFileDialog1.Title = "Export palpack cards as sheet";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportPalpackCardsToExcel(saveFileDialog1.FileName);

                Properties.Settings.Default.SaveFileDialogSheet = Path.GetDirectoryName(saveFileDialog1.FileName);
                Properties.Settings.Default.Save();
            }
        }

        private void ExportAvailableItemsToExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("AvailableItems");

                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Category";

                int row = 2;
                for (int i = 0; i < AvailableItems.Count; i++)
                {
                    worksheet.Cells[row, 1].Value = AvailableItems[i].ItemID.ToString("X8");
                    worksheet.Cells[row, 2].Value = AvailableItemNames[i];
                    worksheet.Cells[row, 3].Value = AvailableItems[i].ItemCategory;
                    row++;
                }

                for (int col = 1; col <= 3; col++)
                {
                    worksheet.Column(col).AutoFit();
                }

                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);
            }

            MessageBox.Show($"Saved on {Path.GetFileName(filePath)}");
        }

        private void ExportPalpackCardsToExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("PalpackCards");

                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Recruted Player";
                worksheet.Cells[1, 4].Value = "Purchase Price";
                worksheet.Cells[1, 5].Value = "Condition 1";
                worksheet.Cells[1, 6].Value = "Condition 2";
                worksheet.Cells[1, 7].Value = "Condition 3";
                worksheet.Cells[1, 8].Value = "Condition 4";

                int row = 2;
                foreach (var card in PalpackCards)
                {
                    worksheet.Cells[row, 1].Value = PalpackIdsDict.ContainsKey(card.ItemID) ? PalpackIdsDict[card.ItemID] : card.ItemID.ToString("X8");
                    worksheet.Cells[row, 2].Value = PalpackNamesDict.ContainsKey(card.ItemID) ? PalpackNamesDict[card.ItemID] : GetItemName(card);

                    var recruitedPlayer = Players.FirstOrDefault(p => p.Charaparam.ParamHash == card.RecrutedCharacterId);
                    if (recruitedPlayer != null)
                    {
                        var charabase = Charabases.FirstOrDefault(cb => cb.BaseHash == recruitedPlayer.Charaparam.BaseHash);
                        worksheet.Cells[row, 3].Value = charabase != null && Charanames.Nouns.ContainsKey(charabase.NameHash)
                            ? Charanames.Nouns[charabase.NameHash].Strings[0].Text
                            : "";
                    }
                    else
                    {
                        worksheet.Cells[row, 3].Value = "";
                    }

                    worksheet.Cells[row, 4].Value = card.PurchasePrice;

                    worksheet.Cells[row, 5].Value = GetConditionDisplayText(card, 1);
                    worksheet.Cells[row, 6].Value = GetConditionDisplayText(card, 2);
                    worksheet.Cells[row, 7].Value = GetConditionDisplayText(card, 3);
                    worksheet.Cells[row, 8].Value = GetConditionDisplayText(card, 4);

                    row++;
                }

                for (int col = 1; col <= 8; col++)
                {
                    worksheet.Column(col).AutoFit();
                }

                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);
            }

            MessageBox.Show($"Saved on {Path.GetFileName(filePath)}");
        }

        #endregion

        #region UI events

        private void NameTextBox_Click(object sender, EventArgs e)
        {
            if (SelectedPalpackCard == null) return;

            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["item_text"].Path), Itemtext, false, true, SelectedPalpackCard.NameID);
            nyanko.ShowDialog();
            Itemtext = nyanko.T2bþFileOpened;

            // Update current name
            if (nyanko.SelectedHash != 0)
            {
                SelectedPalpackCard.NameID = nyanko.SelectedHash;
            }

            // Update all names
            int selectedIndex = palpackListBox.SelectedIndex;
            SetPalpackNames();
            palpackListBox.SelectedIndex = selectedIndex;
        }

        private void SoldPriceFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!soldPriceFlatNumericUpDown.Focused) return;

            SelectedPalpackCard.SellingPrice = Convert.ToInt32(soldPriceFlatNumericUpDown.Value);
        }

        private void BoughtPriceFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!boughtPriceFlatNumericUpDown.Focused) return;

            SelectedPalpackCard.PurchasePrice = Convert.ToInt32(boughtPriceFlatNumericUpDown.Value);
        }

        private void MaxQuantityFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!maxQuantityFlatNumericUpDown.Focused) return;

            SelectedPalpackCard.MaxQuantity = Convert.ToInt32(maxQuantityFlatNumericUpDown.Value);
        }

        private void XPositionFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!xPositionFlatNumericUpDown.Focused) return;

            SelectedPalpackCard.ItemPosX = Convert.ToInt32(xPositionFlatNumericUpDown.Value);
        }

        private void YPositionFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!yPositionFlatNumericUpDown.Focused) return;

            SelectedPalpackCard.ItemPosY = Convert.ToInt32(yPositionFlatNumericUpDown.Value);
        }

        private void DescriptionTextBox_Click(object sender, EventArgs e)
        {
            if (SelectedPalpackCard == null) return;

            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["item_text"].Path), Itemtext, true, false, SelectedPalpackCard.DescriptionID);
            nyanko.ShowDialog();
            Itemtext = nyanko.T2bþFileOpened;

            // Update current description
            if (nyanko.SelectedHash != 0)
            {
                SelectedPalpackCard.DescriptionID = nyanko.SelectedHash;
            }

            if (Itemtext.Texts.ContainsKey(SelectedPalpackCard.DescriptionID))
            {
                descriptionTextBox.Text = Itemtext.Texts[SelectedPalpackCard.DescriptionID].Strings[0].Text.Replace("\\n", Environment.NewLine);
            }
            else
            {
                descriptionTextBox.Clear();
            }
        }

        private void PlayerFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!playerFlatComboBox.Focused || playerFlatComboBox.SelectedIndex == -1 || SelectedPalpackCard == null) return;

            Player recruitedPlayer = Players[playerFlatComboBox.SelectedIndex];
            SelectedPalpackCard.RecrutedCharacterId = recruitedPlayer.Charaparam.ParamHash;

            // Automatically set the player number used to build the item id (ikaXXXX)
            ICharabase charabase = Charabases.FirstOrDefault(cb => cb.BaseHash == recruitedPlayer.Charaparam.BaseHash);
            int playerNumber = charabase != null ? charabase.ModelNumber : 0;

            playerNumFlatNumericUpDown.Value = playerNumber;
            SelectedPalpackCard.CharacterFlag = playerNumber;

            // ApplyPalpackIdFromPlayerNumber(playerNumber);
            SetFace(recruitedPlayer);
        }

        private void PlayerNumFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!playerNumFlatNumericUpDown.Focused || SelectedPalpackCard == null) return;

            SelectedPalpackCard.CharacterFlag = Convert.ToInt32(playerNumFlatNumericUpDown.Value);
            //ApplyPalpackIdFromPlayerNumber(playerNumber);
        }

        private void ConditionTypeFlatComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Shared handler wired to conditionTypeFlatComboBox1, conditionTypeFlatComboBox2,
            // conditionTypeFlatComboBox3 and conditionTypeFlatComboBox4.

            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null || !comboBox.Focused || comboBox.SelectedIndex == -1) return;

            int n = GetConditionIndex(comboBox.Name, "conditionTypeFlatComboBox");
            if (n == -1) return;

            SetPrerequisitesType(n, comboBox.SelectedIndex);
            ApplyConditionVisibility(n, (PalpackConditionTypes)comboBox.SelectedIndex);

            // The previous object/player reference no longer makes sense once the type changed
            SetPrerequisitesId(n, 0);
        }

        private void ConditionObjectFlatComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Shared handler wired to both conditionObjectFlatComboBox1-4 and conditionPlayerFlatComboBox1-4,
            // since both ultimately just set the PrerequisitesId of their slot from a different source list.

            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null || !comboBox.Focused || comboBox.SelectedIndex == -1) return;

            if (comboBox.Name.StartsWith("conditionObjectFlatComboBox"))
            {
                int n = GetConditionIndex(comboBox.Name, "conditionObjectFlatComboBox");
                if (n == -1 || comboBox.SelectedIndex >= AvailableItems.Count) return;

                SetPrerequisitesId(n, AvailableItems[comboBox.SelectedIndex].ItemID);
            }
            else if (comboBox.Name.StartsWith("conditionPlayerFlatComboBox"))
            {
                int n = GetConditionIndex(comboBox.Name, "conditionPlayerFlatComboBox");
                if (n == -1 || comboBox.SelectedIndex >= Players.Count) return;

                SetPrerequisitesId(n, Players[comboBox.SelectedIndex].Charaparam.ParamHash);
            }
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!searchTextBox.Focused) return;

            string search = searchTextBox.Text.ToLower();

            if (string.IsNullOrEmpty(search))
            {
                SetPalpackNames();
                PalpackCardsFiltred = null;
            }
            else
            {
                PalpackCardsFiltred = PalpackCards
                    .Where(card =>
                    {
                        // Search by the "ikaXXXX" palpack id
                        bool searchById = PalpackIdsDict.ContainsKey(card.ItemID) && PalpackIdsDict[card.ItemID].ToLower().Contains(search);

                        // Search by the recruited player's charabase/charaparam id or name
                        bool searchByRecruitedPlayer = false;
                        Player recruitedPlayer = Players.FirstOrDefault(p => p.Charaparam.ParamHash == card.RecrutedCharacterId);

                        if (recruitedPlayer != null)
                        {
                            bool searchByParamHash = ("0x" + recruitedPlayer.Charaparam.ParamHash.ToString("X8").ToLower()).Contains(search);
                            bool searchByBaseHash = ("0x" + recruitedPlayer.Charaparam.BaseHash.ToString("X8").ToLower()).Contains(search);

                            var charabase = Charabases.FirstOrDefault(cb => cb.BaseHash == recruitedPlayer.Charaparam.BaseHash);
                            bool searchByPlayerName = charabase != null
                                && Charanames.Nouns.ContainsKey(charabase.NameHash)
                                && Charanames.Nouns[charabase.NameHash].Strings.Any(s => s.Text.ToLower().Contains(search));

                            searchByRecruitedPlayer = searchByParamHash || searchByBaseHash || searchByPlayerName;
                        }

                        // Search by the palpack card's own name
                        bool searchByName = PalpackNamesDict.ContainsKey(card.ItemID) && PalpackNamesDict[card.ItemID].ToLower().Contains(search);

                        return searchById || searchByRecruitedPlayer || searchByName;
                    })
                    .ToList();

                string[] names = PalpackCardsFiltred
                    .Select(card => PalpackNamesDict.ContainsKey(card.ItemID) ? PalpackNamesDict[card.ItemID] : "")
                    .ToArray();

                palpackListBox.Items.Clear();
                palpackListBox.Items.AddRange(names);
            }
        }

        private void PalpackListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (palpackListBox.SelectedIndex == -1) return;

            if (PalpackCardsFiltred != null && PalpackCardsFiltred.Count > 0)
            {
                SelectedPalpackCard = PalpackCardsFiltred[palpackListBox.SelectedIndex];
            }
            else
            {
                SelectedPalpackCard = PalpackCards[palpackListBox.SelectedIndex];
            }

            // Basic infos
            idTextBox.Text = PalpackIdsDict.ContainsKey(SelectedPalpackCard.ItemID)
                ? PalpackIdsDict[SelectedPalpackCard.ItemID]
                : FindPalpackID(SelectedPalpackCard.ItemID);

            nameTextBox.Text = palpackListBox.SelectedItem.ToString();

            if (Itemtext.Texts.ContainsKey(SelectedPalpackCard.DescriptionID))
            {
                descriptionTextBox.Text = Itemtext.Texts[SelectedPalpackCard.DescriptionID].Strings[0].Text.Replace("\\n", Environment.NewLine);
            }
            else
            {
                descriptionTextBox.Clear();
            }

            // Prices / quantity / position
            soldPriceFlatNumericUpDown.Value = SelectedPalpackCard.SellingPrice;
            boughtPriceFlatNumericUpDown.Value = SelectedPalpackCard.PurchasePrice;
            maxQuantityFlatNumericUpDown.Value = SelectedPalpackCard.MaxQuantity;
            xPositionFlatNumericUpDown.Value = SelectedPalpackCard.ItemPosX;
            yPositionFlatNumericUpDown.Value = SelectedPalpackCard.ItemPosY;
            playerNumFlatNumericUpDown.Value = SelectedPalpackCard.CharacterFlag;

            // Recruited player
            int playerIndex = Players.FindIndex(p => p.Charaparam.ParamHash == SelectedPalpackCard.RecrutedCharacterId);
            playerFlatComboBox.SelectedIndex = playerIndex;

            if (playerIndex != -1)
            {
                var charabase = Charabases.FirstOrDefault(cb => cb.BaseHash == Players[playerIndex].Charaparam.BaseHash);
                SetFace(Players[playerIndex]);
            }
            else
            {
                SetFace(null);
            }

            // Conditions (4 slots)
            for (int i = 1; i <= 4; i++)
            {
                UpdateConditionValueDisplay(i);
            }

            palpackGroupBox.Enabled = true;
        }

        private void InsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPalpackCardWindow newPalpackCardWindow = new NewPalpackCardWindow(GetPlayersWithoutCard(), GetFaceImage);
            if (newPalpackCardWindow.ShowDialog() != DialogResult.OK || newPalpackCardWindow.SelectedCandidate == null) return;

            AddPalpackCard(newPalpackCardWindow.SelectedCandidate);

            PalpackCardsFiltred = null;
            searchTextBox.Text = "Search...";
            SetPalpackNames();

            palpackListBox.SelectedIndex = PalpackCards.Count - 1;
        }

        private void AddPalpackCard(PalpackCardCandidate candidate)
        {
            string number = candidate.Number.ToString().PadLeft(4, '0');

            IItemPalpackCard newCard = NewPalpackCard(candidate);

            // The card name and description are looked up by their own hashes (name_ikaXXXX / desc_ikaXXXX) in item_text
            GetPalpackTextTemplates(out string nameTemplate, out string descriptionTemplate);
            string nickname = GetPlayerNickname(candidate.Player) ?? candidate.Name;

            if (!Itemtext.Nouns.ContainsKey(newCard.NameID))
            {
                Itemtext.Nouns.Add(newCard.NameID, new TextConfig(new List<StringLevel5>() { new StringLevel5(0, nameTemplate.Replace(NicknameToken, nickname)) }));
            }

            if (!Itemtext.Texts.ContainsKey(newCard.DescriptionID))
            {
                Itemtext.Texts.Add(newCard.DescriptionID, new TextConfig(new List<StringLevel5>() { new StringLevel5(0, descriptionTemplate.Replace(NicknameToken, nickname)) }));
            }

            PalpackCards.Add(newCard);
            PalpackIdsDict[newCard.ItemID] = "ika" + number;
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (palpackListBox.SelectedIndex == -1 || SelectedPalpackCard == null) return;

            string cardName = palpackListBox.SelectedItem.ToString();

            DialogResult dialogResult = MessageBox.Show("Do you want to delete " + cardName + "?", "Delete palpack card", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes) return;

            palpackGroupBox.Enabled = false;

            PalpackCards.Remove(SelectedPalpackCard);
            PalpackIdsDict.Remove(SelectedPalpackCard.ItemID);

            if (PalpackCardsFiltred != null)
            {
                PalpackCardsFiltred.Remove(SelectedPalpackCard);
            }

            SelectedPalpackCard = null;
            SetFace(null);

            // Rebuild the names so the list indexes keep matching PalpackCards / PalpackCardsFiltred
            PalpackNamesDict = GetNames(PalpackCards.ToArray());
            palpackListBox.Items.Clear();

            if (PalpackCardsFiltred != null)
            {
                palpackListBox.Items.AddRange(PalpackCardsFiltred.Select(card => PalpackNamesDict.ContainsKey(card.ItemID) ? PalpackNamesDict[card.ItemID] : "").ToArray());
            }
            else
            {
                palpackListBox.Items.AddRange(PalpackNamesDict.Where(x => x.Key != 0x0).Select(x => x.Value).ToArray());
            }

            MessageBox.Show(cardName + " has been removed!");
        }

        private List<PalpackCardCandidate> GetPlayersWithoutCard()
        {
            // A card id is ikaXXXX where XXXX comes from the recruited player's para_cpXXXX, so only those
            // players can get a card, and only when neither the player nor the ikaXXXX id is already used

            Dictionary<int, int> paraNumbers = new Dictionary<int, int>();
            for (int i = 0; i < 10000; i++)
            {
                paraNumbers[ComputeHash($"para_cp{i.ToString().PadLeft(4, '0')}")] = i;
            }

            HashSet<int> recruitedPlayers = new HashSet<int>(PalpackCards.Select(card => card.RecrutedCharacterId));
            HashSet<int> usedItemIds = new HashSet<int>(GameOpened.GetItems("all").Where(x => !(x is IItemPalpackCard)).Select(x => x.ItemID));
            usedItemIds.UnionWith(PalpackCards.Select(card => card.ItemID));

            List<PalpackCardCandidate> candidates = new List<PalpackCardCandidate>();
            HashSet<int> addedPlayers = new HashSet<int>();

            for (int i = 0; i < Players.Count; i++)
            {
                int paramHash = Players[i].Charaparam.ParamHash;

                if (!paraNumbers.TryGetValue(paramHash, out int number)) continue;
                if (recruitedPlayers.Contains(paramHash) || !addedPlayers.Add(paramHash)) continue;
                if (usedItemIds.Contains(ComputeHash($"ika{number.ToString().PadLeft(4, '0')}"))) continue;

                candidates.Add(new PalpackCardCandidate
                {
                    Player = Players[i],
                    Number = number,
                    Name = PlayerNames[i],
                });
            }

            return candidates.OrderBy(x => x.Number).ToList();
        }

        private string GetPlayerNickname(Player player)
        {
            if (player == null) return null;

            ICharabase charabase = Charabases.FirstOrDefault(cb => cb.BaseHash == player.Charaparam.BaseHash);

            return charabase != null && Charanames.Nouns.TryGetValue(charabase.NicknameHash, out var noun) && noun.Strings.Count > 0
                ? noun.Strings[0].Text
                : null;
        }

        private void GetPalpackTextTemplates(out string nameTemplate, out string descriptionTemplate)
        {
            // Generate name and description - Not visible in game
            nameTemplate = NicknameToken + "'s PalPack";
            descriptionTemplate = NicknameToken + "'s PalPack Card";

            foreach (IItemPalpackCard card in PalpackCards)
            {
                if (!Itemtext.Nouns.TryGetValue(card.NameID, out var name) || name.Strings.Count == 0) continue;
                if (!Itemtext.Texts.TryGetValue(card.DescriptionID, out var description) || description.Strings.Count == 0) continue;

                string nickname = GetPlayerNickname(Players.FirstOrDefault(p => p.Charaparam.ParamHash == card.RecrutedCharacterId));
                if (string.IsNullOrEmpty(nickname)) continue;

                string nameText = name.Strings[0].Text;
                string descriptionText = description.Strings[0].Text;

                if (nameText.Contains(nickname) && descriptionText.Contains(nickname))
                {
                    nameTemplate = nameText.Replace(nickname, NicknameToken);
                    descriptionTemplate = descriptionText.Replace(nickname, NicknameToken);
                    return;
                }
            }
        }

        private IItemPalpackCard NewPalpackCard(PalpackCardCandidate candidate)
        {
            string number = candidate.Number.ToString().PadLeft(4, '0');

            // Create default PalpackCard
            return new ItemConfigPalpackCard
            {
                ItemID = ComputeHash("ika" + number),
                NameID = ComputeHash("name_ika" + number),
                DescriptionID = ComputeHash("desc_ika" + number),
                ItemRef = 90,
                ItemCategory = 17,
                MaxQuantity = 1,
                SellingPrice = 100,
                PurchasePrice = 480,
                ItemSubCategory = 2701,
                CharacterFlag = candidate.Number,
                RecrutedCharacterId = candidate.Player.Charaparam.ParamHash,
                Unk5 = 1,
                ItemPosX = 456,
                ItemPosY = 264,
            };
        }

        private void PalpackCardsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Save();
        }

        #endregion
    }
}