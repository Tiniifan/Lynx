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
                Systemtexts[key] = new T2bþ(charaTextGameFile.File.Directory.GetFileFromFullPath(systemTextGameFile.Path));

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
    }
}
