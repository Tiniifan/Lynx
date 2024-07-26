using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Lynx.Tools;
using Lynx.Level5.Text;
using Lynx.Level5.Image;
using Lynx.Level5.Binary;
using Lynx.Level5.Base64;
using Lynx.Level5.Binary.Logic;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;
using Lynx.InazumaEleven.Common;
using Lynx.InazumaEleven.Games.GO;
using Lynx.Forms.Scipts;

namespace Lynx.Forms.Maps
{
    public partial class MapEditor : Form
    {
        private IGame GameOpened;

        private string MapID;

        private Bitmap MiniMapImage;

        private Dictionary<ICharabase, string> Charabases;

        private Dictionary<INPCBase, List<INPCAppear>> NPCs;

        private Dictionary<ITalkInfo, List<ITalkConfig>> NPCEvents;

        private CfgBin Mapenv;

        private int[] BounderBox = new int[4];

        public MapEditor(string mapID, IGame game)
        {
            MapID = mapID;
            GameOpened = game;
            InitializeComponent();
            InitializeMapResource();
        }

        private void InitializeMapResource()
        {
            // Get charabase dict
            GameSupports.GameFile charaText = GameOpened.Files["chara_text"];
            T2bþ charanames = new T2bþ(charaText.File.Directory.GetFileFromFullPath(charaText.Path));
            Charabases = GetCharabaseDict(GameOpened.GetCharabase(), charanames);

            // Fill head combobox
            headFlatComboBox.Items.AddRange(Charabases.Values.ToArray());

            // Get npcs data
            NPCs = GameOpened.GetNPCs(MapID);
            NPCEvents = GameOpened.GetEvents(MapID);

            // Get mapenv
            Mapenv = GameOpened.GetMapenv(MapID);

            // Get bounder box
            BounderBox = new int[4];
            Entry modelPos = Mapenv.Entries[0].Children.FirstOrDefault(
                x => x.GetName() == "PTREE" && x.Variables.Any(
                    y => y.Type == Level5.Binary.Logic.Type.String && y.Value is OffsetTextPair offsetTextPair && offsetTextPair.Text == "MMModelPos"
                )
            );
            if (modelPos != null)
            {
                BounderBox[0] = Convert.ToInt32(modelPos.Children[0].Variables[0].Value);
                BounderBox[1] = Convert.ToInt32(modelPos.Children[1].Variables[0].Value);
                BounderBox[2] = Convert.ToInt32(modelPos.Children[2].Variables[0].Value);
                BounderBox[3] = Convert.ToInt32(modelPos.Children[3].Variables[0].Value);
            }

            // Fill tree view
            TreeNode root = new TreeNode("Characters");
            foreach (KeyValuePair<INPCBase, List<INPCAppear>> npc in NPCs)
            {
                // Find charabase
                ICharabase charabase = Charabases.Keys.FirstOrDefault(x => x.CharaBaseType == npc.Key.Type && x.ModelNumber == npc.Key.HeadID);

                if (charabase != null)
                {
                    string charaname = Charabases[charabase];

                    TreeNode charaGroupNode = new TreeNode(charaname);
                    charaGroupNode.Tag = npc.Key;

                    for (int i = 0; i < npc.Value.Count; i++)
                    {
                        TreeNode charaNode = new TreeNode($"{charaname}_{i}");
                        charaNode.Tag = "Character";
                        charaGroupNode.Nodes.Add(charaNode);
                    }

                    root.Nodes.Add(charaGroupNode);
                }
            }
            root.ExpandAll();
            npcTreeView.Nodes.Add(root);

            try
            {
                GameSupports.GameFile mapFolder = GameOpened.Files["map"];
                byte[] imageData = mapFolder.File.Directory.GetFileFromFullPath($"{mapFolder.Path}{MapID}/{MapID}.xi");
                MiniMapImage = IMGC.ToBitmap(imageData);
            }
            catch
            {
                MiniMapImage = new Bitmap(256, 256);
                selectedNpcMapPictureBox.Image = MiniMapImage;
            }

            selectedNpcMapPictureBox.Image = DrawNPC(MiniMapImage);
        }

        private Dictionary<ICharabase, string> GetCharabaseDict(ICharabase[] charabases, T2bþ charanames)
        {
            Dictionary<ICharabase, string> output = new Dictionary<ICharabase, string>();

            int index = 0;
            foreach (var charabase in charabases)
            {
                string name = "";

                if (charanames.Nouns.TryGetValue(charabase.NameHash, out var noun) && noun.Strings.Count > 0)
                {
                    name = noun.Strings[0].Text;
                }
                else
                {
                    name = "Name " + index;
                }

                output.Add(charabase, name);
                index++;
            }

            return output;
        }

        public static Point CalculatePosition(int[] boundaries, float pointX, float pointY, int mapWidth, int mapHeight)
        {
            int minX = boundaries[0];
            int minY = boundaries[1];
            int maxX = boundaries[2];
            int maxY = boundaries[3];

            int rangeX = maxX - minX;
            int rangeY = maxY - minY;

            double scaleX = (double)mapWidth / rangeX;
            double scaleY = (double)mapHeight / rangeY;

            int mapX = (int)((pointX - minX) * scaleX);
            int mapY = (int)((pointY - minY) * scaleY);

            return new Point(mapX, mapY);
        }

        public Bitmap DrawNPC(Bitmap map, INPCAppear selectNPC = null)
        {
            foreach (KeyValuePair<INPCBase, List<INPCAppear>> npc in NPCs)
            {
                for (int i = 0; i < npc.Value.Count(); i++)
                {
                    Image npcIcon = null;

                    if (selectNPC == npc.Value[i])
                    {
                        npcIcon = Image.FromStream(new ResourceReader("npc_icon_" + npc.Key.IconID + "_selected.png").GetResourceStream());
                    } else
                    {
                        npcIcon = Image.FromStream(new ResourceReader("npc_icon_" + npc.Key.IconID + ".png").GetResourceStream());
                    }

                    Point npcLocation = GetNPCLocation(map, npc.Value[i].LocationX, npc.Value[i].LocationY);
                    map = Draw.DrawImage(map, npcLocation.X, npcLocation.Y, npcIcon);
                }
            }

            return map;
        }

        private Point GetNPCLocation(Bitmap map, float pointX, float pointY)
        {
            if (BounderBox != null)
            {
                return CalculatePosition(BounderBox, pointX, pointY, map.Width, map.Height);
            }
            else
            {
                return new Point(Convert.ToInt32(pointX), Convert.ToInt32(pointY));
            }
        }

        private int GetPhaseNumber(string phaseText, bool firstOccurrence)
        {
            // Define the regular expression pattern to match the number after "if (phase >= "
            string pattern = @"if \(phase >= (\d+)\)";

            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(phaseText);

            if (matches.Count > 0)
            {
                Match selectedMatch = firstOccurrence ? matches[0] : matches[matches.Count - 1];
                return Convert.ToInt32(selectedMatch.Groups[1].Value);
            }
            else
            {
                return -1;
            }
        }

        private void NpcTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (e.Node.Tag.ToString() == "Character")
                {
                    TreeNode characterGroupNode = e.Node.Parent;

                    INPCBase selectedNPCBase = characterGroupNode.Tag as INPCBase;
                    INPCAppear selectedNPCAppear = NPCs[selectedNPCBase][characterGroupNode.Nodes.IndexOf(e.Node)];

                    // Find charabase
                    ICharabase charabase = Charabases.Keys.FirstOrDefault(x => x.CharaBaseType == selectedNPCBase.Type && x.ModelNumber == selectedNPCBase.HeadID);

                    // NPC Config
                    idTextBox.Text = selectedNPCBase.NPCID.ToString("X8");
                    iconFlatComboBox.SelectedIndex = selectedNPCBase.IconID;
                    headFlatComboBox.SelectedIndex = headFlatComboBox.Items.IndexOf(Charabases[charabase]);
                    locationXFlatNumericUpDown.Value = Convert.ToDecimal(selectedNPCAppear.LocationX);
                    locationYNumericUpDown.Value = Convert.ToDecimal(selectedNPCAppear.LocationY);
                    locationZNumericUpDown.Value = Convert.ToDecimal(selectedNPCAppear.LocationZ);
                    rotationNumericUpDown.Value = Convert.ToDecimal(selectedNPCAppear.Rotation);
                    restAnimationTextBox.Text = selectedNPCAppear.StandAnimation;
                    talkAnimationTextBox.Text = selectedNPCAppear.TalkAnimation;
                    unkAnimationTextBox.Text = selectedNPCAppear.UnkAnimation;
                    string conditionText = (selectedNPCAppear.PhaseAppear == "0") ? "" : Condition.ToString(selectedNPCAppear.PhaseAppear);
                    positionCondTextBox.Text = conditionText;

                    // Event
                    if (Events != null && NPCEvents.Any(x => x.Key.TalkID == selectedNPCBase.NPCID))
                    {
                        eventListBox.Items.Clear();
                        List<ITalkConfig> matchTalkConfigs = new List<ITalkConfig>();

                        if (conditionText == "")
                        {
                            ITalkInfo talkInfo = NPCEvents.FirstOrDefault(x => x.Key.TalkID == selectedNPCBase.NPCID).Key;
                            List<ITalkConfig> talkConfigs = NPCEvents[talkInfo];

                            for (int i = 0; i < talkConfigs.Count(); i++)
                            {
                                matchTalkConfigs.Add(talkConfigs[i]);
                            }
                        } else
                        {
                            int locationPhaseStart = GetPhaseNumber(conditionText, true);
                            int locationPhaseEnd = GetPhaseNumber(conditionText, false);

                            ITalkInfo talkInfo = NPCEvents.FirstOrDefault(x => x.Key.TalkID == selectedNPCBase.NPCID).Key;
                            List<ITalkConfig> talkConfigs = NPCEvents[talkInfo];

                            for (int i = 0; i < talkConfigs.Count(); i++)
                            {
                                string talkConditionText = (talkConfigs[i].PhaseAppear == "0") ? "" : Condition.ToString(talkConfigs[i].PhaseAppear);
                                int talkPhaseEnd = GetPhaseNumber(talkConditionText, false);

                                if (talkPhaseEnd != -1
                                    && (talkPhaseEnd >= locationPhaseStart && talkPhaseEnd < locationPhaseEnd
                                    || talkPhaseEnd == locationPhaseStart && talkPhaseEnd == locationPhaseEnd)
                                )
                                {
                                    matchTalkConfigs.Add(talkConfigs[i]);
                                }
                            }
                        }

                        eventListBox.Items.AddRange(matchTalkConfigs.Select((x, index) => $"{eventTypeFlatComboBox.Items[x.TalkType]}").ToArray());
                    }

                    selectedNpcMapPictureBox.Image = DrawNPC(MiniMapImage, selectedNPCAppear);
                }
            }
        }

        private void EventListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventListBox.SelectedIndex == -1) return;

            TreeNode characterGroupNode = npcTreeView.SelectedNode.Parent;

            INPCBase selectedNPCBase = characterGroupNode.Tag as INPCBase;
            INPCAppear selectedNPCAppear = NPCs[selectedNPCBase][characterGroupNode.Nodes.IndexOf(npcTreeView.SelectedNode)];

            List<ITalkConfig> matchTalkConfigs = new List<ITalkConfig>();
            string conditionText = (selectedNPCAppear.PhaseAppear == "0") ? "" : Condition.ToString(selectedNPCAppear.PhaseAppear);
            int locationPhaseStart = GetPhaseNumber(conditionText, true);
            int locationPhaseEnd = GetPhaseNumber(conditionText, false);

            ITalkInfo talkInfo = NPCEvents.FirstOrDefault(x => x.Key.TalkID == selectedNPCBase.NPCID).Key;
            List<ITalkConfig> talkConfigs = NPCEvents[talkInfo];

            for (int i = 0; i < talkConfigs.Count(); i++)
            {
                string talkConditionText = (talkConfigs[i].PhaseAppear == "0") ? "" : Condition.ToString(talkConfigs[i].PhaseAppear);
                int talkPhaseEnd = GetPhaseNumber(talkConditionText, false);

                if (talkPhaseEnd != -1
                    && (talkPhaseEnd >= locationPhaseStart && talkPhaseEnd < locationPhaseEnd
                    || talkPhaseEnd == locationPhaseStart && talkPhaseEnd == locationPhaseEnd)
                )
                {
                    matchTalkConfigs.Add(talkConfigs[i]);
                }
            }

            ITalkConfig selectedTalkConfig = matchTalkConfigs[eventListBox.SelectedIndex];
            eventTypeFlatComboBox.SelectedIndex = selectedTalkConfig.TalkType;
            valueFlatNumericUpDown.Value = selectedTalkConfig.TalkValue;
            eventCondTextBox.Text = conditionText;

            if (selectedTalkConfig.TalkType == 3)
            {
                scriptButton.Enabled = true;
            } else
            {
                scriptButton.Enabled = false;
            }

            configurationGroupBox.Enabled = true;
        }

        private void ScriptButton_Click(object sender, EventArgs e)
        {
            GameSupports.GameFile eventScript = GameOpened.Files["eventScript"];

            string scriptText = "";
            string scriptFileName = $"ev{valueFlatNumericUpDown.Value}.nut";
            string scriptFullPath = $"{eventScript.Path}/ev{valueFlatNumericUpDown.Value}.nutb";

            if (GameOpened.Game.Directory.IsFullPathExists(scriptFullPath))
            {
                byte[] scriptData = GameOpened.Game.Directory.GetFileFromFullPath(scriptFullPath);
                File.WriteAllBytes($"./temp/ev{valueFlatNumericUpDown.Value}.nutb", scriptData);

                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "./NutDecompiler/NutDecompiler.exe",
                    Arguments = $"./temp/ev{valueFlatNumericUpDown.Value}.nutb",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    process.Start();

                    string result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                }

                scriptText = File.ReadAllText($"./temp/{scriptFileName}");

                using (var scriptEditor = new ScriptEditor(scriptFileName, scriptText))
                {
                    if (scriptEditor.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
            }
        }
    }
}
