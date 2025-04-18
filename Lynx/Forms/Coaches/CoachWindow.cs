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
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;
using Lynx.InazumaEleven.Common;
using OfficeOpenXml;
using Microsoft.WindowsAPICodePack.Dialogs;
using static Lynx.InazumaEleven.Games.GO.GOSupport;
using Lynx.Level5.Save.Logic;

namespace Lynx.Forms.Coaches
{
    public partial class CoachWindow : Form
    {
        private Game GameOpened;

        private List<IItemDirector> Coaches;

        private List<IItemDirector> CoachesFiltred;

        private IItemDirector SelectedCoach;

        private Dictionary<int, CoachText> CoachNamesDict;

        private T2bþ Itemtext;

        private T2bþ Systemtext;

        private bool SameSkillID(int id, string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return id == crc32;
        }

        private string FindCoachID(int coachID = 0x0)
        {
            if (coachID == 0x0)
            {
                coachID = SelectedCoach.ItemID;
            }

            for (int i = 0; i < 10000; i++)
            {
                string prefix = "";

                string tryCoachID = $"iim{prefix}{i.ToString().PadLeft(4, '0')}";

                if (SameSkillID(coachID, tryCoachID))
                {
                    return tryCoachID;
                }
            }

            // Not found
            return SelectedCoach.ItemID.ToString("X8");
        }

        private Dictionary<int, CoachText> GetNames(IItemDirector[] coaches)
        {
            Dictionary<int, CoachText> output = new Dictionary<int, CoachText>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var coach in coaches)
            {
                // Determine the skill name
                string name = coach.ItemID == 0x00
                    ? " "
                    : Itemtext.Nouns.TryGetValue(coach.NameID, out var noun) && noun.Strings.Count > 0
                        ? noun.Strings[0].Text
                        : $"Coach {index}";

                // If the name already exists in the dictionary, increment the counter and add the suffix
                if (nameCounts.ContainsKey(name))
                {
                    nameCounts[name]++;
                    name += $" ({nameCounts[name]})";
                }
                else
                {
                    nameCounts[name] = 1;
                }

                int statText1Crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes($"Dir-{coach.ItemNumber}-1")));
                int statText2Crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes($"Dir-{coach.ItemNumber}-2")));

                string statText1 = coach.ItemID == 0x00
                    ? " "
                    : Systemtext.Texts.TryGetValue(statText1Crc32, out var text1) && text1.Strings.Count > 0
                        ? text1.Strings[0].Text
                        : $"";

                string statText2 = coach.ItemID == 0x00
                    ? " "
                    : Systemtext.Texts.TryGetValue(statText2Crc32, out var text2) && text2.Strings.Count > 0
                        ? text2.Strings[0].Text
                        : $"";

                // Add the name to the output dictionary with the SkillHash as the key
                output[coach.ItemID] = new CoachText(name, statText1, statText2);
                index++;
            }

            return output;
        }

        private void SetCoachNames()
        {
            if (CoachNamesDict != null)
            {
                CoachNamesDict.Clear();
            }

            coachListBox.Items.Clear();

            CoachNamesDict = GetNames(Coaches.ToArray());
            coachListBox.Items.AddRange(CoachNamesDict.Where(x => x.Key != 0x0).Select(x => x.Value.Name).ToArray());
        }

        private void UpdateCoachText()
        {
            statTextBox1.Text = CoachNamesDict[SelectedCoach.ItemID].StatText1.Replace("\\n", Environment.NewLine);
            statTextBox2.Text = CoachNamesDict[SelectedCoach.ItemID].StatText2.Replace("\\n", Environment.NewLine);
        }

        private void Save()
        {
            // GameOpened.saveco(Avatars.ToArray());
            // GameOpened.SaveAvatarGrowthTable(AvatarTimeGrowths.ToArray());
            // GameOpened.SaveTextFile(GameOpened.Files["item_text"], Itemtext);
        }

        private void InitializeCoachResource()
        {
            GameSupports.GameFile skillTextGameFile = GameOpened.Files["item_text"];
            Itemtext = new T2bþ(skillTextGameFile.File.Directory.GetFileFromFullPath(skillTextGameFile.Path));
            GameSupports.GameFile systemText = GameOpened.Files["system_text"];
            Systemtext = new T2bþ(systemText.File.Directory.GetFileFromFullPath(systemText.Path));

            Coaches = GameOpened.GetItems("director").Select(x => (IItemDirector)x).ToList();
            SetCoachNames();
        }

        public CoachWindow(Game game)
        {
            GameOpened = game;
            InitializeComponent();
            InitializeCoachResource();
        }

        private void CoachWindow_Shown(object sender, EventArgs e)
        {
            coachListBox.Focus();
        }

        private void CoachWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Save();
        }

        private void CoachListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (coachListBox.SelectedIndex == -1) return;

            if (CoachesFiltred != null && CoachesFiltred.Count > 0)
            {
                SelectedCoach = CoachesFiltred[coachListBox.SelectedIndex];
            }
            else
            {
                SelectedCoach = Coaches[coachListBox.SelectedIndex];
            }

            idTextBox.Text = FindCoachID();
            nameTextBox.Text = coachListBox.SelectedItem.ToString();
            numberFlatNumericUpDown.Value = SelectedCoach.ItemNumber;
            xPositionFlatNumericUpDown.Value = SelectedCoach.ItemPositionX;
            yPositionFlatNumericUpDown.Value = SelectedCoach.ItemPositionY;

            // Buff
            buffGroupFlatNumericUpDown1.Value = SelectedCoach.PlayerGroupBuff1;
            buffGroupFlatNumericUpDown2.Value = SelectedCoach.PlayerGroupBuff2;
            buffGroupFlatNumericUpDown3.Value = SelectedCoach.PlayerGroupBuff3;
            buffFpFlatNumericUpDown.Value = SelectedCoach.FPCompatible;
            buffTpFlatNumericUpDown.Value = SelectedCoach.TPCompatible;
            buffKickFlatNumericUpDown.Value = SelectedCoach.KickCompatible;
            buffDribbleFlatNumericUpDown.Value = SelectedCoach.DribbleCompatible;
            buffTechniqueFlatNumericUpDown.Value = SelectedCoach.TechniqueCompatible;
            buffBlockFlatNumericUpDown.Value = SelectedCoach.BlockCompatible;
            buffSpeedFlatNumericUpDown.Value = SelectedCoach.SpeedCompatible;
            buffStaminaFlatNumericUpDown.Value = SelectedCoach.StaminaCompatible;
            buffCatchFlatNumericUpDown.Value = SelectedCoach.CatchCompatible;
            buffLuckFlatNumericUpDown.Value = SelectedCoach.LuckCompatible;

            // Debuff
            debuffGroupFlatNumericUpDown1.Value = SelectedCoach.PlayerGroupDebuff1;
            debuffGroupFlatNumericUpDown2.Value = SelectedCoach.PlayerGroupDebuff2;
            debuffGroupFlatNumericUpDown3.Value = SelectedCoach.PlayerGroupDebuff3;
            debuffFpFlatNumericUpDown.Value = SelectedCoach.FPNotCompatible;
            debuffTpFlatNumericUpDown.Value = SelectedCoach.TPNotCompatible;
            debuffKickFlatNumericUpDown.Value = SelectedCoach.KickNotCompatible;
            debuffDribbleFlatNumericUpDown.Value = SelectedCoach.DribbleNotCompatible;
            debuffTechniqueFlatNumericUpDown.Value = SelectedCoach.TechniqueNotCompatible;
            debuffBlockFlatNumericUpDown.Value = SelectedCoach.BlockNotCompatible;
            debuffSpeedFlatNumericUpDown.Value = SelectedCoach.SpeedNotCompatible;
            debuffStaminaFlatNumericUpDown.Value = SelectedCoach.StaminaNotCompatible;
            debuffCatchFlatNumericUpDown.Value = SelectedCoach.CatchNotCompatible;
            debuffLuckFlatNumericUpDown.Value = SelectedCoach.LuckNotCompatible;

            if (Itemtext.Texts.ContainsKey(SelectedCoach.DescriptionID))
            {
                descriptionTextBox.Text = Itemtext.Texts[SelectedCoach.DescriptionID].Strings[0].Text.Replace("\\n", Environment.NewLine);
            }
            else
            {
                descriptionTextBox.Clear();
            }

            UpdateCoachText();

            coachGroupBox.Enabled = true;
        }

        private void NumberFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // To do
            //GameSupports.GameFile faceInfo = GameOpened.Files["faceAvatar"];

            //VirtualDirectory faceFolder = faceInfo.File.Directory.GetFolderFromFullPath(faceInfo.Path);

            //string faceFileName = "ck" + numberFlatNumericUpDown.Value.ToString().PadLeft(4, '0') + "a.xi";

            //if (faceFolder.Files.ContainsKey(faceFileName))
            //{
            //    try
            //    {
            //        byte[] imageData = faceInfo.File.Directory.GetFileFromFullPath(faceInfo.Path + "/" + faceFileName);
            //        facePictureBox.Image = IMGC.ToBitmap(imageData);
            //    }
            //    catch
            //    {
            //        facePictureBox.Image = null;
            //    }
            //}
            //else
            //{
            //    facePictureBox.Image = null;
            //}

            if (!numberFlatNumericUpDown.Focused) return;
           
            SelectedCoach.ItemNumber = Convert.ToInt32(numberFlatNumericUpDown.Value);

            int selectedIndex = coachListBox.SelectedIndex;
            SetCoachNames();
            coachListBox.SelectedIndex = selectedIndex;
        }

        private void NameTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["item_text"].Path), Itemtext, false, true, SelectedCoach.NameID);
            nyanko.ShowDialog();
            Itemtext = nyanko.T2bþFileOpened;

            // Update current name
            if (nyanko.SelectedHash != 0)
            {
                SelectedCoach.NameID = nyanko.SelectedHash;
            }

            // Update all name
            int selectedIndex = coachListBox.SelectedIndex;
            SetCoachNames();
            coachListBox.SelectedIndex = selectedIndex;
        }

        private void XPositionFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!xPositionFlatNumericUpDown.Focused) return;

            SelectedCoach.ItemPositionX = Convert.ToInt32(xPositionFlatNumericUpDown.Value);
        }

        private void YPositionFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!yPositionFlatNumericUpDown.Focused) return;

            SelectedCoach.ItemPositionY = Convert.ToInt32(yPositionFlatNumericUpDown.Value);
        }

        private void BuffGroupFlatNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (!buffGroupFlatNumericUpDown1.Focused) return;

            SelectedCoach.PlayerGroupBuff1 = Convert.ToInt32(buffGroupFlatNumericUpDown1.Value);
        }

        private void BuffGroupFlatNumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (!buffGroupFlatNumericUpDown2.Focused) return;

            SelectedCoach.PlayerGroupBuff2 = Convert.ToInt32(buffGroupFlatNumericUpDown2.Value);
        }

        private void BuffGroupFlatNumericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (!buffGroupFlatNumericUpDown3.Focused) return;

            SelectedCoach.PlayerGroupBuff3 = Convert.ToInt32(buffGroupFlatNumericUpDown3.Value);
        }

        private void BuffFpFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!buffGroupFlatNumericUpDown3.Focused) return;

            SelectedCoach.PlayerGroupBuff3 = Convert.ToInt32(buffGroupFlatNumericUpDown3.Value);
        }

        private void BuffTpFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!buffTpFlatNumericUpDown.Focused) return;

            SelectedCoach.TPCompatible = Convert.ToInt32(buffTpFlatNumericUpDown.Value);
        }

        private void BuffKickFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!buffKickFlatNumericUpDown.Focused) return;

            SelectedCoach.KickCompatible = Convert.ToInt32(buffKickFlatNumericUpDown.Value);
        }

        private void BuffDribbleFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!buffDribbleFlatNumericUpDown.Focused) return;

            SelectedCoach.DribbleCompatible = Convert.ToInt32(buffDribbleFlatNumericUpDown.Value);
        }

        private void BuffTechniqueFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!buffTechniqueFlatNumericUpDown.Focused) return;

            SelectedCoach.TechniqueCompatible = Convert.ToInt32(buffTechniqueFlatNumericUpDown.Value);
        }

        private void BuffBlockFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!buffBlockFlatNumericUpDown.Focused) return;

            SelectedCoach.BlockCompatible = Convert.ToInt32(buffBlockFlatNumericUpDown.Value);
        }

        private void BuffSpeedFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!buffSpeedFlatNumericUpDown.Focused) return;

            SelectedCoach.SpeedCompatible = Convert.ToInt32(buffSpeedFlatNumericUpDown.Value);
        }

        private void BuffStaminaFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!buffStaminaFlatNumericUpDown.Focused) return;

            SelectedCoach.StaminaCompatible = Convert.ToInt32(buffStaminaFlatNumericUpDown.Value);
        }

        private void BuffCatchFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!buffCatchFlatNumericUpDown.Focused) return;

            SelectedCoach.CatchCompatible = Convert.ToInt32(buffCatchFlatNumericUpDown.Value);
        }

        private void BuffLuckFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!buffLuckFlatNumericUpDown.Focused) return;

            SelectedCoach.LuckCompatible = Convert.ToInt32(buffLuckFlatNumericUpDown.Value);
        }

        private void DebuffGroupFlatNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffGroupFlatNumericUpDown1.Focused) return;

            SelectedCoach.PlayerGroupDebuff1 = Convert.ToInt32(debuffGroupFlatNumericUpDown1.Value);
        }

        private void DebuffGroupFlatNumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffGroupFlatNumericUpDown2.Focused) return;

            SelectedCoach.PlayerGroupDebuff2 = Convert.ToInt32(debuffGroupFlatNumericUpDown2.Value);
        }

        private void DebuffGroupFlatNumericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffGroupFlatNumericUpDown3.Focused) return;

            SelectedCoach.PlayerGroupDebuff3 = Convert.ToInt32(debuffGroupFlatNumericUpDown3.Value);
        }

        private void DebuffFpFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffFpFlatNumericUpDown.Focused) return;

            SelectedCoach.FPNotCompatible = Convert.ToInt32(debuffFpFlatNumericUpDown.Value);
        }

        private void DebuffTpFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffTpFlatNumericUpDown.Focused) return;

            SelectedCoach.TPNotCompatible = Convert.ToInt32(debuffTpFlatNumericUpDown.Value);
        }

        private void DebuffKickFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffKickFlatNumericUpDown.Focused) return;

            SelectedCoach.KickNotCompatible = Convert.ToInt32(debuffKickFlatNumericUpDown.Value);
        }

        private void DebuffDribbleFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffDribbleFlatNumericUpDown.Focused) return;

            SelectedCoach.DribbleNotCompatible = Convert.ToInt32(debuffDribbleFlatNumericUpDown.Value);
        }

        private void DebuffTechniqueFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffTechniqueFlatNumericUpDown.Focused) return;

            SelectedCoach.TechniqueNotCompatible = Convert.ToInt32(debuffTechniqueFlatNumericUpDown.Value);
        }

        private void DebuffBlockFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffBlockFlatNumericUpDown.Focused) return;

            SelectedCoach.BlockNotCompatible = Convert.ToInt32(debuffBlockFlatNumericUpDown.Value);
        }

        private void DebuffSpeedFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffSpeedFlatNumericUpDown.Focused) return;

            SelectedCoach.SpeedNotCompatible = Convert.ToInt32(debuffSpeedFlatNumericUpDown.Value);
        }

        private void DebuffStaminaFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffStaminaFlatNumericUpDown.Focused) return;

            SelectedCoach.StaminaNotCompatible = Convert.ToInt32(debuffStaminaFlatNumericUpDown.Value);
        }

        private void DebuffCatchFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffCatchFlatNumericUpDown.Focused) return;

            SelectedCoach.CatchNotCompatible = Convert.ToInt32(debuffCatchFlatNumericUpDown.Value);
        }

        private void DebuffLuckFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!debuffLuckFlatNumericUpDown.Focused) return;

            SelectedCoach.CatchNotCompatible = Convert.ToInt32(debuffLuckFlatNumericUpDown.Value);
        }

        private void DescriptionTextBox_Click(object sender, EventArgs e)
        {
            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["item_text"].Path), Itemtext, true, false, SelectedCoach.DescriptionID);
            nyanko.ShowDialog();
            Itemtext = nyanko.T2bþFileOpened;

            // Update current name
            if (nyanko.SelectedHash != 0)
            {
                SelectedCoach.DescriptionID = nyanko.SelectedHash;
            }

            if (Itemtext.Texts.ContainsKey(SelectedCoach.DescriptionID))
            {
                descriptionTextBox.Text = Itemtext.Texts[SelectedCoach.DescriptionID].Strings[0].Text.Replace("\\n", Environment.NewLine);
            }
            else
            {
                descriptionTextBox.Clear();
            }
        }

        private void StatTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!statTextBox1.Focused) return;

            CoachNamesDict[SelectedCoach.ItemID].StatText1 = statTextBox1.Text;
        }

        private void StatTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (!statTextBox2.Focused) return;

            CoachNamesDict[SelectedCoach.ItemID].StatText2 = statTextBox2.Text;
        }
    }
}
