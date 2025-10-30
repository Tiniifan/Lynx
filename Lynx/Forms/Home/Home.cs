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
using Lynx.Forms.FightingSpirits;
using Lynx.Forms.ChallengeRoute;
using Lynx.Forms.Soccers;
using Lynx.Forms.Coaches;
using Lynx.Forms.TranslationHelper;

namespace Lynx.Forms.Home
{
    public partial class Home : Form
    {
        public Game GameOpened;

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
            characterGroupBox.Enabled = true;
            movesGroupBox.Enabled = true;
            itemsGroupBox.Enabled = true;
            shopsGroupBox.Enabled = true;
            eventGroupBox.Enabled = true;
            debugGroupBox.Enabled = true;
            matchGroupBox.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            mapEditorButton.Enabled = false;
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

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FeaturesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (featuresListBox.SelectedIndex == -1) return;

            switch (featuresListBox.SelectedItem.ToString())
            {
                case "Charabase":
                    CharabaseButton_Click(sender, e);
                    break;
                case "Charaparam":
                    CharaparamButton_Click(sender, e);
                    break;
                case "Shops":
                    ShopsButton_Click(sender, e);
                    break;
                case "Fighting Spirits":
                    FightingSpiritsButton_Click(sender, e);
                    break;
                case "Skills":
                    SkillsButton_Click(sender, e);
                    break;
                case "Scripts":
                    ScriptButton_Click(sender, e);
                    break;
                case "Map Editor":
                    MapEditorButton_Click(sender, e);
                    break;
                case "Save Editor":
                    SaveEditorButton_Click(sender, e);
                    break;
                case "Challenge Route":
                    ChallengeRouteButton_Click(sender, e);
                    break;
                case "Teams":
                    TeamsButton_Click(sender, e);
                    break;
                case "Coaches":
                    CoachesButton_Click(sender, e);
                    break;
                case "Translation Helper":
                    TranslationHelperButton_Click(sender, e);
                    break;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameOpened.Save();
            MessageBox.Show("Saved!");
        }

        private void CharabaseButton_Click(object sender, EventArgs e)
        {
            CharabaseWindow charabaseWindow = new CharabaseWindow(GameOpened);
            charabaseWindow.ShowDialog();
        }

        private void CharaparamButton_Click(object sender, EventArgs e)
        {
            CharaParamWindow charaparamWindow = new CharaParamWindow(GameOpened);
            charaparamWindow.ShowDialog();
        }

        private void SkillsButton_Click(object sender, EventArgs e)
        {
            SkillWindow skillWindow = new SkillWindow(GameOpened);
            skillWindow.ShowDialog();
        }

        private void FightingSpiritsButton_Click(object sender, EventArgs e)
        {
            FightingSpiritWindow fightingSpiritWindow = new FightingSpiritWindow(GameOpened);
            fightingSpiritWindow.ShowDialog();
        }

        private void ShopsButton_Click(object sender, EventArgs e)
        {
            ShopWindow shopWindow = new ShopWindow(GameOpened);
            shopWindow.ShowDialog();
        }

        private void MapEditorButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Not yet available");
            MapSelect mapSelectWindow = new MapSelect(GameOpened);
            mapSelectWindow.ShowDialog();
        }

        private void ScriptButton_Click(object sender, EventArgs e)
        {
            ScriptSelect scriptSelect = new ScriptSelect(GameOpened);
            scriptSelect.ShowDialog();
        }

        private void SaveEditorButton_Click(object sender, EventArgs e)
        {
            SaveEditorWindow saveEditorWindow = new SaveEditorWindow(GameOpened);
            saveEditorWindow.ShowDialog();
        }

        private void TeamsButton_Click(object sender, EventArgs e)
        {
            SoccersWindow soccerWindow = new SoccersWindow(GameOpened);
            soccerWindow.ShowDialog();
        }

        private void ChallengeRouteButton_Click(object sender, EventArgs e)
        {
            ChallengeRouteWindow challengeRouteWindow = new ChallengeRouteWindow(GameOpened);
            challengeRouteWindow.ShowDialog();
        }

        private void CoachesButton_Click(object sender, EventArgs e)
        {
            CoachWindow coachWindow = new CoachWindow(GameOpened);
            coachWindow.ShowDialog();
        }

        private void TranslationHelperButton_Click(object sender, EventArgs e)
        {
            TranslationHelperWindow translationHelperWindow = new TranslationHelperWindow(GameOpened);
            translationHelperWindow.ShowDialog();
        }
    }
}
