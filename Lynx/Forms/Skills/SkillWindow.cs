using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using StudioElevenLib.Tools;
using StudioElevenLib.Level5.Text;
using StudioElevenLib.Level5.Image;
using StudioElevenLib.Level5.Binary;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;
using Lynx.InazumaEleven.Common;
using Lynx.InazumaEleven.Games.GO;
using Microsoft.WindowsAPICodePack.Dialogs;
using OfficeOpenXml;

namespace Lynx.Forms.Skills
{
    public partial class SkillWindow : Form
    {
        private Game GameOpened;

        private Dictionary<string, List<ISkillConfig>> Skills;

        private T2bþ Skilltext;

        private ISkillConfig SelectedSkillConfig;

        private TreeNode RightClickNode;

        Dictionary<int, string> PositionNames = new Dictionary<int, string>
        {
            { 1, "Shoot" },
            { 2, "Dribble" },
            { 3, "Block" },
            { 4, "Save" },
            { 15, "Skill" }
        };

        public SkillWindow(Game game)
        {
            GameOpened = game;
            InitializeComponent();
            InitializeSkillResource();
        }

        private bool SameSkillID(int id, string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return id == crc32;
        }

        private string FindSkillID(int id, int position)
        {
            for (int i = 0; i < 10000; i++)
            {
                string prefix = "";

                switch(position)
                {
                    case 1:
                        prefix = "s";
                        break;
                    case 2:
                        prefix = "o";
                        break;
                    case 3:
                        prefix = "d";
                        break;
                    case 4:
                        prefix = "k";
                        break;
                    case 15:
                        prefix = "p";
                        break;
                }

                string tryWazaID = $"wh{prefix}{i.ToString().PadLeft(4, '0')}";
                string tryAvatarID = $"wk{prefix}{i.ToString().PadLeft(4, '0')}";

                if (SameSkillID(id, tryWazaID))
                {
                    return tryWazaID;
                } else if (SameSkillID(id, tryAvatarID))
                {
                    return tryAvatarID;
                }
            }

            // Not found
            return id.ToString("X8");
        }

        private void FillTreeView(string searchText)
        {
            skillTreeView.Nodes.Clear();

            foreach(KeyValuePair<string, List<ISkillConfig>> skillCategory in Skills)
            {
                TreeNode categoryNode = new TreeNode(skillCategory.Key);

                var nameCounts = new Dictionary<string, int>();
                foreach (var skillConfig in skillCategory.Value.Select((skill, index) => new { skill, index }))
                {
                    string baseName;
                    if (Skilltext.Nouns.TryGetValue(skillConfig.skill.NameHash, out var noun) && noun.Strings.Count > 0)
                    {
                        baseName = noun.Strings[0].Text;
                    }
                    else
                    {
                        baseName = "Name " + skillConfig.index;
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

                    if (searchText == null || baseName.ToLower().Contains(searchText.ToLower()))
                    {
                        TreeNode skillNode = new TreeNode(baseName);
                        skillNode.Tag = skillConfig.skill.SkillHash;
                        categoryNode.Nodes.Add(skillNode);
                    }                    
                }

                skillTreeView.Nodes.Add(categoryNode);
            }

            skillTreeView.ExpandAll();
        }

        public static TreeNode FindNodeByText(TreeNodeCollection nodes, string text)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text == text)
                {
                    return node; // Node found
                }

                // Child node
                TreeNode foundChild = FindNodeByText(node.Nodes, text);
                if (foundChild != null)
                {
                    return foundChild; // Node found in child
                }
            }

            return null; // Node not found
        }

        private void SetText(TextBox textbox, int nameID, bool isNoun)
        {
            if (isNoun)
            {
                if (Skilltext.Nouns.ContainsKey(nameID) && Skilltext.Nouns[nameID].Strings[0].Text != null)
                {
                    textbox.Text = Skilltext.Nouns[nameID].Strings[0].Text;
                }
                else
                {
                    textbox.Clear();
                }
            }
            else
            {
                if (Skilltext.Texts.ContainsKey(nameID) && Skilltext.Texts[nameID].Strings[0].Text != null)
                {
                    textbox.Text = Skilltext.Texts[nameID].Strings[0].Text.Replace("\\n", Environment.NewLine);
                }
                else
                {
                    textbox.Clear();
                }
            }
        }

        private string GetText(int nameID, bool isNoun)
        {
            if (isNoun)
            {
                if (Skilltext.Nouns.ContainsKey(nameID) && Skilltext.Nouns[nameID].Strings[0].Text != null)
                {
                    return Skilltext.Nouns[nameID].Strings[0].Text;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                if (Skilltext.Texts.ContainsKey(nameID) && Skilltext.Texts[nameID].Strings[0].Text != null)
                {
                   return Skilltext.Texts[nameID].Strings[0].Text.Replace("\\n", Environment.NewLine);
                }
                else
                {
                    return "";
                }
            }
        }

        private void InitializeSkillResource()
        {
            GameSupports.GameFile skillTextGameFile = GameOpened.Files["skill_text"];
            Skilltext = new T2bþ(skillTextGameFile.File.Directory.GetFileFromFullPath(skillTextGameFile.Path));

            Skills = GameOpened.GetSkillConfigs(false)
                .GroupBy(skill => skill.SkillPosition)
                .ToDictionary(group => PositionNames[group.Key], group => group.ToList());

            FillTreeView(null);
        }

        public void ExportToExcel(string filePath)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Skills");

                // Add headers
                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "ID";
                worksheet.Cells[1, 3].Value = "Type";
                worksheet.Cells[1, 4].Value = "Position";
                worksheet.Cells[1, 5].Value = "Element";
                worksheet.Cells[1, 6].Value = "Effect";
                worksheet.Cells[1, 7].Value = "Partner";
                worksheet.Cells[1, 8].Value = "TP";
                worksheet.Cells[1, 9].Value = "Power";
                worksheet.Cells[1, 10].Value = "Fault (%)";
                worksheet.Cells[1, 11].Value = "Technique";
                worksheet.Cells[1, 12].Value = "Evolution Type";
                worksheet.Cells[1, 13].Value = "Evolution Grow";
                worksheet.Cells[1, 14].Value = "Category"; // Nom de la catégorie d'origine

                int row = 2;

                foreach (var entry in Skills)
                {
                    // Sort skills by Power (ascending order)
                    var sortedSkills = entry.Value.OrderBy(skill => skill.Power).ToList();

                    foreach (var skill in sortedSkills)
                    {
                        worksheet.Cells[row, 1].Value = GetText(skill.NameHash, true);
                        worksheet.Cells[row, 2].Value = FindSkillID(skill.SkillHash, skill.SkillPosition);
                        worksheet.Cells[row, 3].Value = skill.SkillType == 1 ? "Move" : "Fighting Spirit";
                        worksheet.Cells[row, 4].Value = positonFlatComboBox.Items[skill.SkillPosition == 15 ? 4 : skill.SkillPosition - 1].ToString();

                        if (skill.SkillPosition == 15)
                        {
                            worksheet.Cells[row, 5].Value = "";

                            if (skill.Element - 1 != -1)
                            {
                                worksheet.Cells[row, 6].Value = skillEffectFlatComboBox.Items[skill.Element - 1].ToString();
                            }
                            else
                            {
                                worksheet.Cells[row, 6].Value = "";
                            }
                        }
                        else
                        {
                            worksheet.Cells[row, 5].Value = elementFlatComboBox.Items[skill.Element - 1].ToString();

                            string effect = effectFlatComboBox.Items[skill.EffectType].ToString();
                            worksheet.Cells[row, 6].Value = effect != "No Effect" ? effect : "";
                        }

                        var range = worksheet.Cells[row, 5, row, 5];

                        switch (skill.Element)
                        {
                            case 1:
                                range.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                break;
                            case 2:
                                range.Style.Font.Color.SetColor(System.Drawing.Color.Green);
                                break;
                            case 3:
                                range.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                break;
                            case 4:
                                range.Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(255, 204, 0));
                                break;
                            case 5:
                                range.Style.Font.Color.SetColor(System.Drawing.Color.Purple);
                                break;
                            default:
                                range.Style.Font.Color.SetColor(System.Drawing.Color.Gray);
                                break;
                        }

                        worksheet.Cells[row, 7].Value = skill.PartnerNumber;
                        worksheet.Cells[row, 8].Value = skill.TPCost;
                        worksheet.Cells[row, 9].Value = skill.Power;
                        worksheet.Cells[row, 10].Value = skill.Fault;
                        worksheet.Cells[row, 11].Value = skill.Technique;

                        string evolution = typeFlatComboBox.Items[skill.EvolutionType].ToString();
                        worksheet.Cells[row, 12].Value = evolution != "No evolution" ? evolution : "";

                        string grow = growFlatComboBox.Items[skill.EvolutionGrow].ToString();
                        worksheet.Cells[row, 13].Value = grow != "No Grow" ? grow : "";

                        worksheet.Cells[row, 14].Value = entry.Key; // Ajoute la clé du dictionnaire (ex: nom de la catégorie)

                        row++;
                    }
                }

                for (int col = 1; col <= 14; col++)
                {
                    worksheet.Column(col).AutoFit();
                }

                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);
            }

            MessageBox.Show($"Saved on {Path.GetFileName(filePath)}");
        }

        private void Save()
        {
            GameOpened.SaveSkillConfigs(Skills.SelectMany(pair => pair.Value).ToArray());
            GameOpened.SaveTextFile(GameOpened.Files["skill_text"], Skilltext);
        }

        private void SkillWindow_Shown(object sender, EventArgs e)
        {
            skillTreeView.Focus();
        }

        private void SkillWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Save();
        }

        private void SkillTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                TreeNode category = e.Node.Parent;

                // Get Skill 
                SelectedSkillConfig = Skills[category.Text].FirstOrDefault(x => x.SkillHash == Convert.ToInt32(e.Node.Tag));

                if (SelectedSkillConfig == null) return;

                SetText(nameTextBox, SelectedSkillConfig.NameHash, true);
                SetText(nameWazaTextBox, SelectedSkillConfig.NameWazaID, true);
                idTextBox.Text = FindSkillID(SelectedSkillConfig.SkillHash, SelectedSkillConfig.SkillPosition);
                numberFlatNumericUpDown.Value = SelectedSkillConfig.Number;

                if (SelectedSkillConfig.SkillType == 1)
                {
                    moveTypeRadioButton1.Checked = true;
                    moveTypeRadioButton2.Checked = false;
                } else
                {
                    moveTypeRadioButton1.Checked = false;
                    moveTypeRadioButton2.Checked = true;
                }

                positonFlatComboBox.SelectedIndex = SelectedSkillConfig.SkillPosition == 15 ? 4 : SelectedSkillConfig.SkillPosition - 1;

                // Is Skill?
                if (SelectedSkillConfig.SkillPosition == 15)
                {
                    skillEffectFlatComboBox.Visible = true;
                    elementFlatComboBox.Visible = false;
                    label6.Text = "Effect";
                    label2.Text = "Value";
                    effectFlatNumericUpDown1.Visible = true;
                    effectFlatComboBox.Visible = false;
                    label12.Text = "Buff";
                    boostActiveRadioButton1.Visible = true;
                    boostActiveRadioButton2.Visible = true;
                    partnerFlatNumericUpDown.Visible = false;
                    skillEffectFlatComboBox.SelectedIndex = SelectedSkillConfig.Element - 1;
                    effectFlatNumericUpDown1.Value = SelectedSkillConfig.SkillBoost;
                    boostActiveRadioButton2.Checked = SelectedSkillConfig.BoostActiveOn == 1;
                    boostActiveRadioButton1.Checked = SelectedSkillConfig.BoostActiveOn != 1;
                    boostPanel.Visible = true;
                } else
                {
                    skillEffectFlatComboBox.Visible = false;
                    elementFlatComboBox.Visible = true;
                    label6.Text = "Element";
                    effectFlatNumericUpDown1.Visible = false;
                    effectFlatComboBox.Visible = true;
                    label12.Text = "Partner";
                    boostActiveRadioButton1.Visible = false;
                    boostActiveRadioButton2.Visible = false;
                    partnerFlatNumericUpDown.Visible = true;
                    effectFlatComboBox.SelectedIndex = SelectedSkillConfig.EffectType;
                    elementFlatComboBox.SelectedIndex = SelectedSkillConfig.Element - 1;
                    boostPanel.Visible = false;
                }

                partnerFlatNumericUpDown.Value = SelectedSkillConfig.PartnerNumber;
                tpCostFlatNumericUpDown.Value = SelectedSkillConfig.TPCost;
                powerFlatNumericUpDown.Value = SelectedSkillConfig.Power;
                faultRateFlatNumericUpDown.Value = SelectedSkillConfig.Fault;
                techniqueFlatNumericUpDown.Value = SelectedSkillConfig.Technique;
                typeFlatComboBox.SelectedIndex = SelectedSkillConfig.EvolutionType;
                growFlatComboBox.SelectedIndex = SelectedSkillConfig.EvolutionGrow;
                SetText(descriptionTextBox, SelectedSkillConfig.DescriptionHash, false);

                skillGroupBox.Enabled = true;
            }
        }

        private void SkillTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RightClickNode = skillTreeView.GetNodeAt(e.X, e.Y);

                if (RightClickNode != null)
                {
                    if (RightClickNode.Parent == null)
                    {
                        contextMenuStrip1.Items[1].Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip1.Items[1].Enabled = true;
                    }

                    contextMenuStrip1.Show(skillTreeView, e.Location);
                }
            }
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RightClickNode == null) return;

            string nodePosition = "";
            if (RightClickNode.Parent != null)
            {
                nodePosition = RightClickNode.Parent.Text;
            } else
            {
                nodePosition = RightClickNode.Text;
            }

            int position = PositionNames.FirstOrDefault(x => x.Value == nodePosition).Key;
            NewSkillWindow newSkillWindow = new NewSkillWindow(Skills.SelectMany(pair => pair.Value).Select(x => x.SkillHash).Distinct().ToList(), position);
            newSkillWindow.ShowDialog();

            if (newSkillWindow.NewID != 0)
            {
                ISkillConfig newSkillConfig = GameOpened.GetEmptyObject<ISkillConfig>();

                newSkillConfig.SkillHash = newSkillWindow.NewID;
                newSkillConfig.SkillPosition = newSkillWindow.Position;
                newSkillConfig.SkillType = newSkillWindow.Type;

                Skills[PositionNames[newSkillConfig.SkillPosition]].Add(newSkillConfig);

                searchTextBox.Enabled = false;
                FillTreeView(null);
                searchTextBox.Text = "Search...";
                searchTextBox.Enabled = true;

                TreeNode positionNode = FindNodeByText(skillTreeView.Nodes, PositionNames[newSkillConfig.SkillPosition]);
                skillTreeView.SelectedNode = positionNode.Nodes[positionNode.Nodes.Count-1];
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RightClickNode == null) return;

            TreeNode parentNode = RightClickNode.Parent;
            if (parentNode != null)
            {
                string typeName = parentNode.Name;
                TreeNode positionNode = FindNodeByText(skillTreeView.Nodes, typeName);
                int nodeIndex = positionNode.Nodes.IndexOf(RightClickNode);;

                // Remove
                Skills[typeName].RemoveAt(nodeIndex);
                positionNode.Nodes.RemoveAt(nodeIndex);

                // Update form
                searchTextBox.Enabled = false;
                FillTreeView(null);
                searchTextBox.Text = "Search...";
                searchTextBox.Enabled = true;
            }
        }

        private void NameTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["skill_text"].Path), Skilltext, false, true, SelectedSkillConfig.NameHash);
            nyanko.ShowDialog();
            Skilltext = nyanko.T2bþFileOpened;

            // Update current nickname
            if (nyanko.SelectedHash != 0)
            {
                SelectedSkillConfig.NameHash = nyanko.SelectedHash;

                if (Skilltext.Nouns.ContainsKey(SelectedSkillConfig.NameHash))
                {
                    nameTextBox.Text = Skilltext.Nouns[SelectedSkillConfig.NameHash].Strings[0].Text;
                }
                else
                {
                    nameTextBox.Clear();
                }
            }
        }

        private void NameWazaTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["skill_text"].Path), Skilltext, false, true, SelectedSkillConfig.NameWazaID);
            nyanko.ShowDialog();
            Skilltext = nyanko.T2bþFileOpened;

            // Update current nickname
            if (nyanko.SelectedHash != 0)
            {
                SelectedSkillConfig.NameWazaID = nyanko.SelectedHash;

                if (Skilltext.Nouns.ContainsKey(SelectedSkillConfig.NameWazaID))
                {
                    nameWazaTextBox.Text = Skilltext.Nouns[SelectedSkillConfig.NameWazaID].Strings[0].Text;
                }
                else
                {
                    nameWazaTextBox.Clear();
                }
            }
        }

        private void NumberFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!numberFlatNumericUpDown.Focused) return;

            SelectedSkillConfig.Number = Convert.ToInt32(numberFlatNumericUpDown.Value);
        }

        private void MoveTypeRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!moveTypeRadioButton1.Focused) return;

            SelectedSkillConfig.SkillType = 1;
        }

        private void MoveTypeRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!moveTypeRadioButton2.Focused) return;

            SelectedSkillConfig.SkillType = 2;
        }

        private void PositonFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!positonFlatComboBox.Focused || positonFlatComboBox.SelectedIndex == -1) return;

            SelectedSkillConfig.SkillPosition = positonFlatComboBox.SelectedIndex + 1;
        }

        private void SkillEffectFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!skillEffectFlatComboBox.Focused || skillEffectFlatComboBox.SelectedIndex == -1) return;

            SelectedSkillConfig.Element = skillEffectFlatComboBox.SelectedIndex + 1;
        }

        private void ElementFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!elementFlatComboBox.Focused || elementFlatComboBox.SelectedIndex == -1) return;

            SelectedSkillConfig.Element = elementFlatComboBox.SelectedIndex + 1;
        }

        private void EffectFlatNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (!effectFlatNumericUpDown1.Focused) return;

            SelectedSkillConfig.SkillBoost = Convert.ToInt32(effectFlatNumericUpDown1.Value);
        }

        private void EffectFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!effectFlatComboBox.Focused || effectFlatComboBox.SelectedIndex == -1) return;

            SelectedSkillConfig.EffectType = effectFlatComboBox.SelectedIndex;
        }

        private void BoostActiveRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boostActiveRadioButton1.Focused) return;

            SelectedSkillConfig.BoostActiveOn = 0;
        }

        private void BoostActiveRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boostActiveRadioButton2.Focused) return;

            SelectedSkillConfig.BoostActiveOn = 1;
        }

        private void PartnerFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!partnerFlatNumericUpDown.Focused) return;

            SelectedSkillConfig.PartnerNumber = Convert.ToInt32(partnerFlatNumericUpDown.Value);
        }

        private void TpCostFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!tpCostFlatNumericUpDown.Focused) return;

            SelectedSkillConfig.TPCost = Convert.ToInt32(tpCostFlatNumericUpDown.Value);
        }

        private void PowerFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!powerFlatNumericUpDown.Focused) return;

            SelectedSkillConfig.Power = Convert.ToInt32(powerFlatNumericUpDown.Value);
        }

        private void FaultRateFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!faultRateFlatNumericUpDown.Focused) return;

            SelectedSkillConfig.Fault = Convert.ToInt32(faultRateFlatNumericUpDown.Value);
        }

        private void TechniqueFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!techniqueFlatNumericUpDown.Focused) return;

            SelectedSkillConfig.Technique = Convert.ToInt32(techniqueFlatNumericUpDown.Value);
        }

        private void TypeFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!typeFlatComboBox.Focused || typeFlatComboBox.SelectedIndex == -1) return;

            SelectedSkillConfig.EvolutionType = typeFlatComboBox.SelectedIndex;
        }

        private void GrowFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!growFlatComboBox.Focused || growFlatComboBox.SelectedIndex == -1) return;

            SelectedSkillConfig.EvolutionGrow = growFlatComboBox.SelectedIndex;
        }

        private void DescriptionTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["skill_text"].Path), Skilltext, true, false, SelectedSkillConfig.DescriptionHash);
            nyanko.ShowDialog();

            Skilltext = nyanko.T2bþFileOpened;

            // Update current nickname
            if (nyanko.SelectedHash != 0)
            {
                SelectedSkillConfig.DescriptionHash = nyanko.SelectedHash;                

                if (Skilltext.Texts.ContainsKey(SelectedSkillConfig.DescriptionHash))
                {
                    descriptionTextBox.Text = Skilltext.Texts[SelectedSkillConfig.DescriptionHash].Strings[0].Text;
                }
                else
                {
                    descriptionTextBox.Clear();
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
            if (!string.IsNullOrEmpty(searchTextBox.Text))
                FillTreeView(null);
            searchTextBox.Text = "Search...";
            searchTextBox.Enabled = true;
        }

        private void ExportAsCfgbinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // Save
                Save();

                // Get files
                (string, byte[]) skillconfig = GameOpened.GetFileNameAndContent("skill_config");
                (string, byte[]) skilltext = GameOpened.GetFileNameAndContent("skill_text");

                // Export files
                File.WriteAllBytes(Path.Combine(dialog.FileName, skillconfig.Item1), skillconfig.Item2);
                File.WriteAllBytes(Path.Combine(dialog.FileName, skilltext.Item1), skilltext.Item2);

                MessageBox.Show("Data exported!");
            }
        }

        private void ExportAscsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.SaveFileDialogSheet))
            {
                saveFileDialog1.InitialDirectory = Properties.Settings.Default.SaveFileDialogSheet;
            }

            saveFileDialog1.Filter = "XLSX Files(*.xlsx) | *.xlsx";
            saveFileDialog1.Title = "Export your file as sheet";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportToExcel(saveFileDialog1.FileName);

                Properties.Settings.Default.OpenFileDialogSaveEditor = Path.GetDirectoryName(saveFileDialog1.FileName);
                Properties.Settings.Default.Save();
            }
        }
    }
}
