using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lynx.Level5.Archive.ARC0;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Games.GO;
using Lynx.Forms.Characters;
using Lynx.Forms.Maps;
using Lynx.Forms.Shops;
using Lynx.Forms.Skills;
using Lynx.Forms.Scipts;
using Lynx.Forms.SaveEditor;

namespace Lynx.Forms.Home
{
    public partial class Home : Form
    {
        public IGame GameOpened;

        public Home()
        {
            InitializeComponent();
        }

        private void LoadFile(string filename)
        {
            openFileDialog1.FileName = filename;

            Properties.Settings.Default.OpenFileDialogHome = Path.GetDirectoryName(openFileDialog1.FileName);
            Properties.Settings.Default.Save();

            LanguageWindow languageWindow = new LanguageWindow();
            languageWindow.ShowDialog();

            GameOpened = new GO(openFileDialog1.FileName, languageWindow.SelectedLanguage);

            featuresGroupBox.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
        }

        private void Home_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string dragPath = Path.GetFullPath(files[0]);
                string dragExt = Path.GetExtension(files[0]);

                if (files.Length > 1) return;
                if (dragExt != ".fa") return;

                openFileDialog1.FileName = dragPath;
                LoadFile(openFileDialog1.FileName);
            }
        }

        private void Home_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Supported Files (*.fa)|*.fa";
            openFileDialog1.FileName = null;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.OpenFileDialogHome))
            {
                openFileDialog1.InitialDirectory = Properties.Settings.Default.OpenFileDialogHome;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadFile(openFileDialog1.FileName);
            }
        }

        private void FeaturesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (featuresListBox.SelectedIndex == -1) return;

            switch (featuresListBox.SelectedItem.ToString())
            {
                case "Charabase":
                    CharabaseWindow charabaseWindow = new CharabaseWindow(GameOpened);
                    charabaseWindow.ShowDialog();
                    break;
                case "Charaparam":
                    CharaParamWindow charaparamWindow = new CharaParamWindow(GameOpened);
                    charaparamWindow.ShowDialog();
                    break;
                case "Shops":
                    ShopWindow shopWindow = new ShopWindow(GameOpened);
                    shopWindow.ShowDialog();
                    break;
                case "Skills":
                    SkillWindow skillWindow = new SkillWindow(GameOpened);
                    skillWindow.ShowDialog();
                    break;
                case "Scripts":
                    ScriptSelect scriptSelect = new ScriptSelect(GameOpened);
                    scriptSelect.ShowDialog();
                    break;
                case "Map Editor":
                    MessageBox.Show("Please note that the map editor is not available, you can only view the maps.");
                    MapSelect mapSelectWindow = new MapSelect(GameOpened);
                    mapSelectWindow.ShowDialog();
                    break;
                case "Save Editor":
                    SaveEditorWindow saveEditorWindow = new SaveEditorWindow(GameOpened);
                    saveEditorWindow.ShowDialog();
                    break;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameOpened.Save();
            MessageBox.Show("Saved!");
        }
    }
}
