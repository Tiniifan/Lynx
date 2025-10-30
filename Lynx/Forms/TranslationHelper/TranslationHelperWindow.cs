using System;
using System.Drawing;
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
using OfficeOpenXml;
using System.Text.RegularExpressions;
using Lynx.Level5.Base64;
using Lynx.UI;
using Microsoft.WindowsAPICodePack.Dialogs;
using Lynx.InazumaEleven.Games.GO;
using Lynx.Level5.Text.Logic;
using static Lynx.InazumaEleven.Games.GO.GOSupport;

namespace Lynx.Forms.TranslationHelper
{
    public partial class TranslationHelperWindow : Form
    {
        private Game GameOpened;

        private string CibleLanguage;

        private Dictionary<string, T2bþ> Charatexts;

        private Dictionary<string, T2bþ> Systemtexts;

        private Dictionary<string, T2bþ> Troutetexts;

        private Dictionary<string, T2bþ> Itemtexts;

        private Dictionary<string, T2bþ> Skilltexts;

        private Dictionary<string, T2bþ> Teamtexts;

        public TranslationHelperWindow(Game game)
        {
            GameOpened = game;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            InitializeComponent();
            InitializeTextResource();
        }

        private void InitializeTextResource()
        {
            CibleLanguage = GameOpened.LanguageCode;

            string targetLanguage = GameOpened.LanguageCode;

            // Initialize dictionary
            Charatexts = new Dictionary<string, T2bþ>();
            Systemtexts = new Dictionary<string, T2bþ>();
            Troutetexts = new Dictionary<string, T2bþ>();
            Itemtexts = new Dictionary<string, T2bþ>();
            Skilltexts = new Dictionary<string, T2bþ>();
            Teamtexts = new Dictionary<string, T2bþ>();

            foreach (string key in GOSupport.AvailableLanguages.Values)
            {
                GameOpened.LanguageCode = key;
                GameOpened.GetGameFiles();

                GameSupports.GameFile charaTextGameFile = GameOpened.Files["chara_text"];
                Charatexts[key] = new T2bþ(charaTextGameFile.File.Directory.GetFileFromFullPath(charaTextGameFile.Path));

                GameSupports.GameFile systemTextGameFile = GameOpened.Files["system_text"];
                Systemtexts[key] = new T2bþ(systemTextGameFile.File.Directory.GetFileFromFullPath(systemTextGameFile.Path));

                GameSupports.GameFile trouteTextGameFile = GameOpened.Files["troute_text"];
                Troutetexts[key] = new T2bþ(trouteTextGameFile.File.Directory.GetFileFromFullPath(trouteTextGameFile.Path));

                GameSupports.GameFile itemTextGameFile = GameOpened.Files["item_text"];
                Itemtexts[key] = new T2bþ(itemTextGameFile.File.Directory.GetFileFromFullPath(itemTextGameFile.Path));

                GameSupports.GameFile skillTextGameFile = GameOpened.Files["skill_text"];
                Skilltexts[key] = new T2bþ(skillTextGameFile.File.Directory.GetFileFromFullPath(skillTextGameFile.Path));

                GameSupports.GameFile teamTextGameFile = GameOpened.Files["team_text"];
                Teamtexts[key] = new T2bþ(teamTextGameFile.File.Directory.GetFileFromFullPath(teamTextGameFile.Path));
            }

            GameOpened.LanguageCode = CibleLanguage;
            GameOpened.GetGameFiles();
        }

        private void ImportTranslationButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                openFileDialog.Title = "Import Translation from Excel";

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    using (ExcelPackage package = new ExcelPackage(new FileInfo(openFileDialog.FileName)))
                    {
                        // Import each sheet
                        ImportSheetToDictionary(package, "Charatexts", Charatexts);
                        ImportSheetToDictionary(package, "Systemtexts", Systemtexts);
                        ImportSheetToDictionary(package, "Troutetexts", Troutetexts);
                        ImportSheetToDictionary(package, "Itemtexts", Itemtexts);
                        ImportSheetToDictionary(package, "Skilltexts", Skilltexts);
                        ImportSheetToDictionary(package, "Teamtexts", Teamtexts);

                        MessageBox.Show("Import completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during import: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ImportSheetToDictionary(ExcelPackage package, string sheetName, Dictionary<string, T2bþ> dict)
        {
            var sheet = package.Workbook.Worksheets[sheetName];
            if (sheet == null)
            {
                MessageBox.Show($"Sheet '{sheetName}' not found in the Excel file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Read header to get language columns
            var languages = new List<string>();
            var languageColumns = new Dictionary<string, int>();

            // Start from column 3 (after ID and Type)
            int colIndex = 3; 
            while (sheet.Cells[1, colIndex].Value != null)
            {
                string lang = sheet.Cells[1, colIndex].Value.ToString();
                languages.Add(lang);
                languageColumns[lang] = colIndex;
                colIndex++;
            }

            // Process each row
            int rowCount = sheet.Dimension?.Rows ?? 0;
            for (int row = 2; row <= rowCount; row++)
            {
                var idCell = sheet.Cells[row, 1].Value;
                var typeCell = sheet.Cells[row, 2].Value;

                if (idCell == null || typeCell == null)
                    continue;

                // Parse the CRC key
                if (!int.TryParse(idCell.ToString(), System.Globalization.NumberStyles.HexNumber, null, out int crcKey))
                    continue;

                string entryType = typeCell.ToString();

                // Import text for each language
                foreach (var lang in languages)
                {
                    if (lang == CibleLanguage)
                        continue;

                    if (!dict.ContainsKey(lang))
                        continue;

                    var textCell = sheet.Cells[row, languageColumns[lang]].Value;

                    // Skip if text is null, empty, or whitesp
                    if (textCell == null || string.IsNullOrWhiteSpace(textCell.ToString()))
                        continue;

                    string textValue = textCell.ToString();

                    // Verify that the key does not exist before importing
                    bool keyExists = false;
                    if (entryType == "Text")
                    {
                        keyExists = dict[lang].Texts.ContainsKey(crcKey);
                    }
                    else if (entryType == "Noun")
                    {
                        keyExists = dict[lang].Nouns.ContainsKey(crcKey);
                    }

                    if (keyExists)
                    {
                        var result = MessageBox.Show(
                            $"Key {crcKey:X8} already exists in {lang} for {sheetName} ({entryType}). Do you want to overwrite it?",
                            "Key Already Exists",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Cancel)
                            return;
                        else if (result == DialogResult.No)
                            continue;
                    }

                    // Import the text
                    ImportTextEntry(dict[lang], crcKey, entryType, textValue);
                }
            }
        }

        private void ImportTextEntry(T2bþ t2b, int crcKey, string entryType, string textValue)
        {
            try
            {
                if (entryType == "Text")
                {
                    // Create or update text entry
                    if (!t2b.Texts.ContainsKey(crcKey))
                    {
                        t2b.Texts[crcKey] = new TextConfig();
                    }

                    // Ensure the Strings list is initialized and has at least one entry
                    if (t2b.Texts[crcKey].Strings == null)
                    {
                        t2b.Texts[crcKey].Strings = new List<StringLevel5>();
                    }

                    if (t2b.Texts[crcKey].Strings.Count == 0)
                    {
                        t2b.Texts[crcKey].Strings.Add(new StringLevel5());
                    }

                    t2b.Texts[crcKey].Strings[0].Text = textValue;
                }
                else if (entryType == "Noun")
                {
                    // Create or update noun entry
                    if (!t2b.Nouns.ContainsKey(crcKey))
                    {
                        t2b.Nouns[crcKey] = new TextConfig();
                    }

                    // Ensure the Strings list is initialized and has at least one entry
                    if (t2b.Nouns[crcKey].Strings == null)
                    {
                        t2b.Nouns[crcKey].Strings = new List<StringLevel5>();
                    }

                    if (t2b.Nouns[crcKey].Strings.Count == 0)
                    {
                        t2b.Nouns[crcKey].Strings.Add(new StringLevel5());
                    }

                    t2b.Nouns[crcKey].Strings[0].Text = textValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing entry {crcKey:X8}: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Save()
        {
            foreach (string key in GOSupport.AvailableLanguages.Values)
            {
                GameOpened.LanguageCode = key;
                GameOpened.GetGameFiles();

                GameOpened.SaveTextFile(GameOpened.Files["chara_text"], Charatexts[key]);
                GameOpened.SaveTextFile(GameOpened.Files["system_text"], Systemtexts[key]);
                GameOpened.SaveTextFile(GameOpened.Files["troute_text"], Troutetexts[key]);
                GameOpened.SaveTextFile(GameOpened.Files["item_text"], Itemtexts[key]);
                GameOpened.SaveTextFile(GameOpened.Files["skill_text"], Skilltexts[key]);
                GameOpened.SaveTextFile(GameOpened.Files["team_text"], Teamtexts[key]);
            }

            // Restaure default game language files
            GameOpened.LanguageCode = CibleLanguage;
            GameOpened.GetGameFiles();
        }

        private void ExportTranslationButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Export Translation to Excel";

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                using (ExcelPackage package = new ExcelPackage())
                {
                    ExportDictionaryToSheet(package, Charatexts, "Charatexts");
                    ExportDictionaryToSheet(package, Systemtexts, "Systemtexts");
                    ExportDictionaryToSheet(package, Troutetexts, "Troutetexts");
                    ExportDictionaryToSheet(package, Itemtexts, "Itemtexts");
                    ExportDictionaryToSheet(package, Skilltexts, "Skilltexts");
                    ExportDictionaryToSheet(package, Teamtexts, "Teamtexts");

                    package.SaveAs(new FileInfo(saveFileDialog.FileName));
                    MessageBox.Show("Export completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ExportDictionaryToSheet(ExcelPackage package, Dictionary<string, T2bþ> dict, string sheetName)
        {
            var sheet = package.Workbook.Worksheets.Add(sheetName);

            // Header
            sheet.Cells[1, 1].Value = "ID";
            sheet.Cells[1, 2].Value = "Type";

            var langs = dict.Keys.ToList();
            var otherLangs = dict.Keys.Where(lang => lang != CibleLanguage);

            for (int i = 0; i < langs.Count; i++)
            {
                sheet.Cells[1, i + 3].Value = langs[i];
            }

            int row = 2;

            // Export Texts
            if (dict.ContainsKey(CibleLanguage) && dict[CibleLanguage].Texts != null && dict[CibleLanguage].Texts.Count > 0)
            {
                var textKeys = dict[CibleLanguage].Texts.Keys;
                foreach (int crcKey in textKeys)
                {
                    bool missingInSomeLang = otherLangs.Any(lang =>
                        !dict.ContainsKey(lang) ||
                        !dict[lang].Texts.ContainsKey(crcKey) ||
                        dict[lang].Texts[crcKey].Strings.Count == 0);

                    if (!missingInSomeLang)
                        continue;

                    sheet.Cells[row, 1].Value = crcKey.ToString("X8");
                    sheet.Cells[row, 2].Value = "Text";

                    for (int l = 0; l < langs.Count; l++)
                    {
                        var lang = langs[l];

                        if (dict.ContainsKey(lang))
                        {
                            if (dict[lang].Texts.ContainsKey(crcKey))
                            {
                                sheet.Cells[row, l + 3].Value = dict[lang].Texts[crcKey].Strings[0].Text;
                            }
                            else
                            {
                                sheet.Cells[row, l + 3].Value = "";
                            }
                        }
                    }

                    row++;
                }
            }

            // Export Nouns
            if (dict.ContainsKey(CibleLanguage) && dict[CibleLanguage].Nouns != null && dict[CibleLanguage].Nouns.Count > 0)
            {
                var nounKeys = dict[CibleLanguage].Nouns.Keys;

                foreach (int crcKey in nounKeys)
                {
                    bool missingInSomeLang = otherLangs.Any(lang =>
                        !dict.ContainsKey(lang) ||
                        !dict[lang].Nouns.ContainsKey(crcKey) ||
                        dict[lang].Nouns[crcKey].Strings.Count == 0);

                    if (!missingInSomeLang)
                        continue;

                    sheet.Cells[row, 1].Value = crcKey.ToString("X8");
                    sheet.Cells[row, 2].Value = "Noun";

                    for (int l = 0; l < langs.Count; l++)
                    {
                        var lang = langs[l];

                        if (dict.ContainsKey(lang))
                        {
                            if (dict[lang].Nouns.ContainsKey(crcKey))
                            {
                                sheet.Cells[row, l + 3].Value = dict[lang].Nouns[crcKey].Strings[0].Text;
                            } else
                            {
                                sheet.Cells[row, l + 3].Value = "";
                            }
                        }
                    }

                    row++;
                }
            }
        }

        private void ButtonExportCfgBin_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // Save
                Save();

                foreach (string key in GOSupport.AvailableLanguages.Values)
                {
                    GameOpened.LanguageCode = key;
                    GameOpened.GetGameFiles();

                    // Get files
                    (string, byte[]) charatext = GameOpened.GetFileNameAndContent("chara_text");
                    (string, byte[]) systemtext = GameOpened.GetFileNameAndContent("system_text");
                    (string, byte[]) troutetext = GameOpened.GetFileNameAndContent("troute_text");
                    (string, byte[]) itemtext = GameOpened.GetFileNameAndContent("item_text");
                    (string, byte[]) skilltext = GameOpened.GetFileNameAndContent("skill_text");
                    (string, byte[]) teamtext = GameOpened.GetFileNameAndContent("team_text");

                    // Export files
                    File.WriteAllBytes(Path.Combine(dialog.FileName, charatext.Item1), charatext.Item2);
                    File.WriteAllBytes(Path.Combine(dialog.FileName, systemtext.Item1), systemtext.Item2);
                    File.WriteAllBytes(Path.Combine(dialog.FileName, troutetext.Item1), troutetext.Item2);
                    File.WriteAllBytes(Path.Combine(dialog.FileName, itemtext.Item1), itemtext.Item2);
                    File.WriteAllBytes(Path.Combine(dialog.FileName, skilltext.Item1), skilltext.Item2);
                    File.WriteAllBytes(Path.Combine(dialog.FileName, teamtext.Item1), teamtext.Item2);
                }

                // Restaure default game language files
                GameOpened.LanguageCode = CibleLanguage;
                GameOpened.GetGameFiles();

                MessageBox.Show("Data exported!");
            }
        }

        private void TranslationHelperWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Save();
        }
    }
}
