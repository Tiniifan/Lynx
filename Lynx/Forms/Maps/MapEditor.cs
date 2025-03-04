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
using System.Threading;
using DocumentFormat.OpenXml.Wordprocessing;
using static Lynx.InazumaEleven.Games.GO.GOSupport;
using System.Xml.Linq;
using Lynx.Level5.Save.Logic;
using DocumentFormat.OpenXml.Spreadsheet;
using Lynx.Level5.Text.Logic;
using Lynx.Forms.Characters;

namespace Lynx.Forms.Maps
{
    public partial class MapEditor : Form
    {
        private IGame GameOpened;

        private string MapID;

        private Bitmap MiniMapImage;

        private Dictionary<ICharabase, string> Charabases;

        private Dictionary<int, string> Bodies;

        private Dictionary<int, string> Boots;

        private Dictionary<int, string> Gloves;

        private Dictionary<INPCBase, List<INPCAppear>> NPCs;

        INPCBase SelectedNPCBase;

        INPCAppear SelectedNPCAppear;

        ITalkInfo SelectedTalkInfo;

        ITalkConfig SelectedTalkConfig;

        private Dictionary<ITalkInfo, List<ITalkConfig>> NPCEvents;

        private CfgBin Mapenv;

        private T2bþ MapText;

        private int[] BounderBox = new int[4];

        private Thread mapPreviewThread;
        private MapPreview mapPreviewInstance;

        public MapEditor(string mapID, IGame game)
        {
            MapID = mapID;
            GameOpened = game;
            InitializeComponent();

            // Design 
            positionCondLineNumberRTB.RichTextBox.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            positionCondLineNumberRTB.RichTextBox.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            positionCondLineNumberRTB.Strip.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            positionCondLineNumberRTB.Strip.BoxedLineColor = System.Drawing.Color.FromArgb(35, 35, 35);
            positionCondLineNumberRTB.Strip.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            positionCondLineNumberRTB.RichTextBox.AcceptsTab = true;

            eventCondLineNumberRTB.RichTextBox.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            eventCondLineNumberRTB.RichTextBox.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            eventCondLineNumberRTB.Strip.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            eventCondLineNumberRTB.Strip.BoxedLineColor = System.Drawing.Color.FromArgb(35, 35, 35);
            eventCondLineNumberRTB.Strip.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            eventCondLineNumberRTB.RichTextBox.AcceptsTab = true;
        }

        private void MapEditor_Shown(object sender, EventArgs e)
        {        
            OpenMapPreview();
            InitializeMapResource();
        }

        private void MapEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Force to close MapPreview window
            if (mapPreviewThread != null && mapPreviewThread.IsAlive)
            {
                mapPreviewInstance.Invoke((Action)(() =>
                {
                    mapPreviewInstance.Close();
                }));

                mapPreviewThread.Join();
            }

            // Save
            GameOpened.SaveNPCs(NPCs, MapID);
            GameOpened.SaveEvents(NPCEvents, MapID);
            GameOpened.SaveMapText(MapText, NPCEvents, MapID);
        }

        private void OpenMapPreview()
        {
            if (mapPreviewInstance != null && !mapPreviewInstance.IsDisposed)
            {
                // Si l'instance existe déjà, amenez-la au premier plan
                mapPreviewInstance.Invoke((Action)(() =>
                {
                    mapPreviewInstance.BringToFront();
                }));
                return;
            }

            // Créez un nouveau thread pour MapPreview
            mapPreviewThread = new Thread(() =>
            {
                mapPreviewInstance = new MapPreview();

                // Configurez la position de MapPreview
                mapPreviewInstance.StartPosition = FormStartPosition.Manual;
                mapPreviewInstance.Location = new Point(
                    this.Right,
                    this.Top
                );

                // Démarrez la boucle de messages
                Application.Run(mapPreviewInstance);
            });

            mapPreviewThread.SetApartmentState(ApartmentState.STA); // Configurez le thread comme STA
            mapPreviewThread.Start();
        }

        public void ReceiveCursorPosition(Point position)
        {
            if (SelectedNPCAppear != null)
            {
                (float pointX, float pointY) = CalculateOriginalPosition(BounderBox, position.X, position.Y, MiniMapImage.Width, MiniMapImage.Height);

                SelectedNPCAppear.LocationX = Convert.ToSingle(pointX);
                SelectedNPCAppear.LocationY = Convert.ToSingle(pointY);

                UpdateSelectedNpcImage(DrawNPC(MiniMapImage, SelectedNPCAppear));
            }
        }

        public void EndReceiveCursorPosition()
        {
            if (SelectedNPCAppear != null)
            {
                this.BeginInvoke((Action)(() =>
                {
                    locationXFlatNumericUpDown.Value = Convert.ToDecimal(SelectedNPCAppear.LocationX);
                    locationYNumericUpDown.Value = Convert.ToDecimal(SelectedNPCAppear.LocationY);
                }));
            }
        }

        private bool MapPreviewExists()
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is MapPreview)
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateSelectedNpcImage(Image newImage)
        {
            if (MapPreviewExists() && mapPreviewInstance != null)
            {
                mapPreviewInstance.Invoke((Action)(() =>
                {
                    mapPreviewInstance.UpdateNpcMapPictureBox(newImage);
                }));
            }
        }

        private void FillNPCTreeView()
        {
            npcTreeView.Nodes.Clear();

            Dictionary<string, INPCBase> npcSorted = NPCs.Keys.ToDictionary(x => FindNPCHash(x), y => y);
            npcSorted = npcSorted.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);

            TreeNode root = new TreeNode("NPC");
            root.ContextMenuStrip = npcsContextMenuStrip;

            foreach (var myNPC in npcSorted)
            {
                KeyValuePair<INPCBase, List<INPCAppear>> npc = new KeyValuePair<INPCBase, List<INPCAppear>>(myNPC.Value, NPCs[myNPC.Value]);

                // Find charabase
                ICharabase charabase = Charabases.Keys.FirstOrDefault(x => x.CharaBaseType == npc.Key.Type && x.ModelNumber == npc.Key.HeadID);

                string name = npc.Key.HeadID.ToString();

                if (charabase != null)
                {
                    name = Charabases[charabase];
                }

                TreeNode charaGroupNode = new TreeNode(myNPC.Key);
                charaGroupNode.Tag = npc.Key;

                for (int i = 0; i < npc.Value.Count; i++)
                {
                    TreeNode charaNode = new TreeNode($"{name}_{i}");
                    charaNode.Tag = "Character";
                    charaGroupNode.Nodes.Add(charaNode);
                }

                root.Nodes.Add(charaGroupNode);
            }

            root.ExpandAll();
            npcTreeView.Nodes.Add(root);
        }

        private void FillEventTreeView(string conditionText)
        {
            // Clear event
            eventListBox.Items.Clear();

            // Reset selected index
            eventListBox.SelectedIndex = -1;

            if (Events != null && NPCEvents.Any(x => x.Key.TalkID == SelectedNPCBase.NPCID))
            {
                List<ITalkConfig> matchTalkConfigs = new List<ITalkConfig>();

                if (conditionText == "")
                {
                    ITalkInfo talkInfo = NPCEvents.FirstOrDefault(x => x.Key.TalkID == SelectedNPCBase.NPCID).Key;
                    List<ITalkConfig> talkConfigs = NPCEvents[talkInfo];

                    for (int i = 0; i < talkConfigs.Count(); i++)
                    {
                        matchTalkConfigs.Add(talkConfigs[i]);
                    }
                }
                else
                {
                    int locationPhaseStart = GetPhaseNumber(conditionText, true);
                    int locationPhaseEnd = GetPhaseNumber(conditionText, false);

                    ITalkInfo talkInfo = NPCEvents.FirstOrDefault(x => x.Key.TalkID == SelectedNPCBase.NPCID).Key;
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

                configurationGroupBox.Enabled = false;

                eventListBox.Items.AddRange(matchTalkConfigs.Select((x, index) => $"{eventTypeFlatComboBox.Items[x.TalkType]}").ToArray());

                if (eventListBox.Items.Count > 0)
                {
                    eventListBox.SelectedIndex = 0;
                }
                else
                {
                    dialogTextBox.Clear();
                    eventCondLineNumberRTB.RichTextBox.Clear();
                }
            }
        }

        private void InitializeMapResource()
        {
            // Get charabase dict
            GameSupports.GameFile charaText = GameOpened.Files["chara_text"];
            T2bþ charanames = new T2bþ(charaText.File.Directory.GetFileFromFullPath(charaText.Path));
            Charabases = GetCharabaseDict(GameOpened.GetCharabase(), charanames);

            // Add none character
            Charabases.Add(new CharaBase(), "None");

            // Fill head combobox
            headFlatComboBox.Items.AddRange(Charabases.Values.ToArray());

            // Get bodies, boots, gloves dict
            GameSupports.GameFile itemText = GameOpened.Files["item_text"];
            T2bþ itemnames = new T2bþ(itemText.File.Directory.GetFileFromFullPath(itemText.Path));
            IItemConfig[] equipments = GameOpened.GetItems("equipment");
            IItemConfig[] uniforms = GameOpened.GetItems("uniform");
            Bodies = GetUniformDict(uniforms, itemnames, "modelRPGBody");
            Boots = GetEquipmentDict(equipments, itemnames, "modelRPGShoes");
            Gloves = GetEquipmentDict(equipments, itemnames, "modelRPGGloves");

            // Fill bodies, boots, gloves combobox
            uniformFlatComboBox.Items.AddRange(Bodies.Values.ToArray());
            bootsFlatComboBox.Items.AddRange(Boots.Values.ToArray());
            glovesFlatComboBox.Items.AddRange(Gloves.Values.ToArray());

            // Get npcs data
            NPCs = GameOpened.GetNPCs(MapID);
            NPCEvents = GameOpened.GetEvents(MapID);

            // Get mapenv
            Mapenv = GameOpened.GetMapenv(MapID);

            // Get Maptext
            MapText = GameOpened.GetMapText(MapID);

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
            FillNPCTreeView();

            try
            {
                GameSupports.GameFile mapFolder = GameOpened.Files["map"];
                byte[] imageData = mapFolder.File.Directory.GetFileFromFullPath($"{mapFolder.Path}{MapID}/{MapID}.xi");
                MiniMapImage = IMGC.ToBitmap(imageData);
            }
            catch
            {
                MiniMapImage = new Bitmap(256, 256);
            }

            UpdateSelectedNpcImage(DrawNPC(MiniMapImage));
        }

        private Dictionary<ICharabase, string> GetCharabaseDict(ICharabase[] charabases, T2bþ charanames)
        {
            Dictionary<ICharabase, string> output = new Dictionary<ICharabase, string>();

            int index = 0;
            foreach (var charabase in charabases)
            {
                string name = "";
                string type = $" ({EnumHelper.GetEnumName((CharaTypes)charabase.CharaBaseType)})";

                if (charanames.Nouns.TryGetValue(charabase.NameHash, out var noun) && noun.Strings.Count > 0)
                {
                    name = noun.Strings[0].Text;
                }
                else
                {
                    name = "Name " + index;
                }

                output.Add(charabase, name + type);
                index++;
            }

            return output;
        }

        private Dictionary<int, string> GetEquipmentDict(IItemConfig[] items, T2bþ itemnames, string folderName)
        {
            Dictionary<int, string> output = new Dictionary<int, string>
            {
                { 0, " " }
            };

            GameSupports.GameFile gameFile = GameOpened.Files[folderName];

            VirtualDirectory modelFolder = gameFile.File.Directory.GetFolderFromFullPath(gameFile.Path);

            foreach (string fileNameWithExtension in modelFolder.Files.Keys)
            {
                string fileName = Path.GetFileNameWithoutExtension(fileNameWithExtension).Replace('z', 'a');

                int fileNameCrc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(fileName)));

                // output name
                string name = fileName;

                try
                {
                    int itemIndex = Convert.ToInt32(fileName.Substring(3));

                    if (!output.ContainsKey(itemIndex))
                    {
                        // Search if the item exist
                        if (items.Any(x => x.ItemID == fileNameCrc32))
                        {
                            // Get the item
                            IItemConfig itemConfig = items.FirstOrDefault(x => x.ItemID == fileNameCrc32);

                            // Get the text
                            if (itemnames.Nouns.TryGetValue(itemConfig.NameID, out var noun) && noun.Strings.Count > 0)
                            {
                                name = noun.Strings[0].Text;
                            }
                        }

                        output.Add(itemIndex, name);
                    }
                } catch
                {
                    Console.WriteLine("Invalid item");
                }
            }

            return output;
        }

        private Dictionary<int, string> GetUniformDict(IItemConfig[] items, T2bþ itemnames, string folderName)
        {
            Dictionary<int, string> output = new Dictionary<int, string>
            {
                { 0, " " }
            };

            GameSupports.GameFile gameFile = GameOpened.Files[folderName];

            VirtualDirectory modelFolder = gameFile.File.Directory.GetFolderFromFullPath(gameFile.Path);
            string[] files = modelFolder.Files.Keys.ToArray();

            foreach (string fileNameWithExtension in files.Where(x => x.Contains("uza") && x != "uzatest.xc"))
            {
                string fileName = Path.GetFileNameWithoutExtension(fileNameWithExtension);

                string itemID = fileName.Replace("uza", "iiu");
                int itemCRC32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(itemID)));
                int itemIndex = Convert.ToInt32(fileName.Substring(3));

                bool hasGoalkeeper = files.Any(x => x.Contains(fileName.Replace("uza", "uzb")));
                bool hasAwayColours = files.Any(x => x.Contains(fileName.Replace("uza", "uzc")));
                bool hasGoalkeeperAwayColours = files.Any(x => x.Contains(fileName.Replace("uza", "uzd")));

                string bodyName = fileName;

                // Search if the item exist
                if (items.Any(x => x.ItemID == itemCRC32))
                {
                    // Get the item
                    IItemConfig itemConfig = items.FirstOrDefault(x => x.ItemID == itemCRC32);

                    // Get the text
                    if (itemnames.Nouns.TryGetValue(itemConfig.NameID, out var noun) && noun.Strings.Count > 0)
                    {
                        bodyName = noun.Strings[0].Text;
                    }
                }

                int bodyIndex = itemIndex * 1000;

                output.Add(bodyIndex, bodyName + " (Home)");
                bodyIndex += 1;

                if (hasAwayColours)
                {
                    output.Add(bodyIndex, bodyName + " (Away)");
                }

                bodyIndex += 1;

                if (hasGoalkeeper)
                {
                    output.Add(bodyIndex, bodyName + " (GK Home)");
                }

                bodyIndex += 1;

                if (hasGoalkeeperAwayColours)
                {
                    output.Add(bodyIndex, bodyName + " (GK Away)");
                }
            }

            return output;
        }

        public Point CalculatePosition(int[] boundaries, float pointX, float pointY, int mapWidth, int mapHeight)
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

        public (float pointX, float pointY) CalculateOriginalPosition(int[] boundaries, int mapX, int mapY, int mapWidth, int mapHeight)
        {
            int minX = boundaries[0];
            int minY = boundaries[1];
            int maxX = boundaries[2];
            int maxY = boundaries[3];

            int rangeX = maxX - minX;
            int rangeY = maxY - minY;

            double scaleX = (double)rangeX / mapWidth;
            double scaleY = (double)rangeY / mapHeight;

            float pointX = (float)(mapX * scaleX + minX);
            float pointY = (float)(mapY * scaleY + minY);

            return (pointX, pointY);
        }

        public Bitmap DrawNPC(Bitmap map, INPCAppear selectNPC = null)
        {
            Bitmap outputMap = new Bitmap(map);

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

                    Point npcLocation = GetNPCLocation(outputMap, npc.Value[i].LocationX, npc.Value[i].LocationY);
                    outputMap = Draw.DrawImage(outputMap, npcLocation.X, npcLocation.Y, npcIcon);
                }
            }

            return outputMap;
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

        private void SetComboBox(ComboBox comboBox, Dictionary<int, string> dict, int value)
        {
            if (dict.ContainsKey(value))
            {
                int index = comboBox.Items.IndexOf(dict[value]);

                if (index == -1)
                {
                    comboBox.SelectedIndex = comboBox.Items.IndexOf(dict[0]);
                }
                else
                {
                    comboBox.SelectedIndex = index;
                }
            } else
            {
                comboBox.SelectedIndex = comboBox.Items.IndexOf(dict[0]);
            }
        }

        private bool SameNPCHash(string name, INPCBase npcBase)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return npcBase.NPCID == crc32;
        }

        private string FindNPCHash(INPCBase npcBase = null)
        {
            if (npcBase == null)
            {
                npcBase = SelectedNPCBase;
            }

            for (int i = 0; i < 10000; i++)
            {
                string nameNPC = "evc" + i.ToString().PadLeft(4, '0');

                if (SameNPCHash(nameNPC, npcBase))
                {
                    return nameNPC;
                }
            }

            // Not found
            return npcBase.NPCID.ToString("X8");
        }

        private void ReorganizeTabNames()
        {
            int tabIndex = 1;

            foreach (TabPage tabPage in dialogVsTabControl.TabPages)
            {
                if (tabPage == addTabPage)
                    continue;

                tabPage.Text = $"Text {tabIndex}";
                tabIndex++;
            }
        }

        private void SetText(int textNumber)
        {
            if (MapText == null) return;

            if (SelectedTalkConfig != null && SelectedTalkConfig.TalkType == 1)
            {
                // Get Text ID
                TextConfig text = MapText.Texts.FirstOrDefault(x => x.Key == SelectedTalkConfig.TalkValue).Value;

                if (text != null)
                {
                    // Replace "/n" with an actual line break
                    string modifiedText = text.Strings[textNumber].Text.Replace("\\n", "\r\n");
                    dialogTextBox.Text = modifiedText;
                    return;
                }
            }

            dialogTextBox.Clear();
        }

        private int CreateTextEntry()
        {
            if (MapText != null)
            {
                string npcName = npcTreeView.SelectedNode.Text;
                int washaID = 0;

                // Find charabase
                ICharabase charabase = Charabases.Keys.FirstOrDefault(x => x.CharaBaseType == SelectedNPCBase.Type && x.ModelNumber == SelectedNPCBase.HeadID);

                if (charabase != null)
                {
                    washaID = charabase.BaseHash;
                }

                string textID = $"text_" + npcName;
                int textIDCRC32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(textID)));

                if (MapText.Texts.ContainsKey(textIDCRC32))
                {
                    MapText.Texts.Remove(textIDCRC32);
                }

                MapText.Texts.Add(textIDCRC32, new TextConfig(new List<StringLevel5>() { new StringLevel5(0, "")}, washaID));

                Console.WriteLine(MapText.Texts.FirstOrDefault(x => x.Key == textIDCRC32).Value.Strings.Count);

                return textIDCRC32;
            } else
            {
                return 0;
            }
        }

        private void NpcTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (e.Node.Tag.ToString() == "Character")
                {
                    TreeNode characterGroupNode = e.Node.Parent;

                    SelectedNPCBase = characterGroupNode.Tag as INPCBase;
                    SelectedNPCAppear = NPCs[SelectedNPCBase][characterGroupNode.Nodes.IndexOf(e.Node)];

                    // Find charabase
                    ICharabase charabase = Charabases.Keys.FirstOrDefault(x => x.CharaBaseType == SelectedNPCBase.Type && x.ModelNumber == SelectedNPCBase.HeadID);

                    // NPC Config
                    idTextBox.Text = characterGroupNode.Text;
                    iconFlatComboBox.SelectedIndex = SelectedNPCBase.IconID;
                    headFlatComboBox.SelectedIndex = headFlatComboBox.Items.IndexOf(Charabases[charabase]);
                    SetComboBox(uniformFlatComboBox, Bodies, SelectedNPCBase.UniformID);
                    SetComboBox(bootsFlatComboBox, Boots, SelectedNPCBase.BootsID);
                    SetComboBox(glovesFlatComboBox, Gloves, SelectedNPCBase.GlovesID);
                    locationXFlatNumericUpDown.Value = Convert.ToDecimal(SelectedNPCAppear.LocationX);
                    locationYNumericUpDown.Value = Convert.ToDecimal(SelectedNPCAppear.LocationY);
                    locationZNumericUpDown.Value = Convert.ToDecimal(SelectedNPCAppear.LocationZ);
                    rotationNumericUpDown.Value = Convert.ToDecimal(SelectedNPCAppear.Rotation);
                    lookAtPlayerCheckBox.Checked = (SelectedNPCAppear.LookAtThePlayer == 2);
                    restAnimationTextBox.Text = SelectedNPCAppear.StandAnimation;
                    talkAnimationTextBox.Text = SelectedNPCAppear.TalkAnimation;
                    unkAnimationTextBox.Text = SelectedNPCAppear.UnkAnimation;
                    string conditionText = (SelectedNPCAppear.PhaseAppear == "0") ? "" : Condition.ToString(SelectedNPCAppear.PhaseAppear);
                    positionCondLineNumberRTB.RichTextBox.Text = conditionText;

                    // Event
                    FillEventTreeView(conditionText);


                    UpdateSelectedNpcImage(DrawNPC(MiniMapImage, SelectedNPCAppear));

                    mapVSTabControl.Enabled = true;
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

            int locationPhaseStart = 0;
            int locationPhaseEnd = 999999999;

            if (conditionText != "")
            {
                locationPhaseStart = GetPhaseNumber(conditionText, true);
                locationPhaseEnd = GetPhaseNumber(conditionText, false);
            }

            SelectedTalkInfo = NPCEvents.FirstOrDefault(x => x.Key.TalkID == selectedNPCBase.NPCID).Key;
            List<ITalkConfig> talkConfigs = NPCEvents[SelectedTalkInfo];

            for (int i = 0; i < talkConfigs.Count(); i++)
            {
                string talkConditionText = (talkConfigs[i].PhaseAppear == "0") ? "" : Condition.ToString(talkConfigs[i].PhaseAppear);
                int talkPhaseEnd = GetPhaseNumber(talkConditionText, false);

                if (talkConditionText == "")
                {
                    matchTalkConfigs.Add(talkConfigs[i]);
                } else
                {
                    if (talkPhaseEnd != -1
                        && (talkPhaseEnd >= locationPhaseStart && talkPhaseEnd < locationPhaseEnd
                        || talkPhaseEnd == locationPhaseStart && talkPhaseEnd == locationPhaseEnd)
                    )
                    {
                        matchTalkConfigs.Add(talkConfigs[i]);
                    }
                }
            }

            SelectedTalkConfig = matchTalkConfigs[eventListBox.SelectedIndex];
            eventTypeFlatComboBox.SelectedIndex = SelectedTalkConfig.TalkType;
            valueFlatNumericUpDown.Value = SelectedTalkConfig.TalkValue;
            eventCondLineNumberRTB.RichTextBox.Text = (SelectedTalkConfig.PhaseAppear == "0") ? "" : Condition.ToString(SelectedTalkConfig.PhaseAppear);

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

        private void PreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMapPreview();
        }

        private void IconFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iconFlatComboBox.Focused || iconFlatComboBox.SelectedIndex == -1) return;

            SelectedNPCBase.IconID = iconFlatComboBox.SelectedIndex;

            UpdateSelectedNpcImage(DrawNPC(MiniMapImage, SelectedNPCAppear));
        }

        private void HeadFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!headFlatComboBox.Focused || headFlatComboBox.SelectedIndex == -1) return;

            // Find charabase
            ICharabase charabase = Charabases.FirstOrDefault(x => x.Value == headFlatComboBox.SelectedItem.ToString()).Key;

            if (charabase != null)
            {
                SelectedNPCBase.HeadID = charabase.ModelNumber;
                SelectedNPCBase.Type = charabase.CharaBaseType;

                // Reload npc names
                FillNPCTreeView();

                mapVSTabControl.Enabled = false;
            }
        }

        private void UniformFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!uniformFlatComboBox.Focused || uniformFlatComboBox.SelectedIndex == -1) return;

            SelectedNPCBase.UniformID = Bodies.FirstOrDefault(x => x.Value == uniformFlatComboBox.SelectedItem.ToString()).Key;
        }

        private void BootsFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bootsFlatComboBox.Focused || bootsFlatComboBox.SelectedIndex == -1) return;

            SelectedNPCBase.BootsID = Boots.FirstOrDefault(x => x.Value == bootsFlatComboBox.SelectedItem.ToString()).Key;
        }

        private void GlovesFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!glovesFlatComboBox.Focused || glovesFlatComboBox.SelectedIndex == -1) return;

            SelectedNPCBase.GlovesID = Gloves.FirstOrDefault(x => x.Value == glovesFlatComboBox.SelectedItem.ToString()).Key;
        }

        private void LocationXFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!locationXFlatNumericUpDown.Focused) return;

            SelectedNPCAppear.LocationX = Convert.ToSingle(locationXFlatNumericUpDown.Value);

            UpdateSelectedNpcImage(DrawNPC(MiniMapImage, SelectedNPCAppear));
        }

        private void LocationYNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!locationYNumericUpDown.Focused) return;

            SelectedNPCAppear.LocationY = Convert.ToSingle(locationYNumericUpDown.Value);

            UpdateSelectedNpcImage(DrawNPC(MiniMapImage, SelectedNPCAppear));
        }

        private void LocationZNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!locationZNumericUpDown.Focused) return;

            SelectedNPCAppear.LocationZ = Convert.ToSingle(locationZNumericUpDown.Value);
        }

        private void RotationNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!rotationNumericUpDown.Focused) return;

            SelectedNPCAppear.Rotation = Convert.ToSingle(rotationNumericUpDown.Value);
        }

        private void LookAtPlayerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!lookAtPlayerCheckBox.Focused) return;

            if (lookAtPlayerCheckBox.Checked)
            {
                SelectedNPCAppear.LookAtThePlayer = 2;
            } else
            {
                SelectedNPCAppear.LookAtThePlayer = 1;
            }
        }

        private void RestAnimationTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!restAnimationTextBox.Focused) return;

            SelectedNPCAppear.StandAnimation = restAnimationTextBox.Text;
        }

        private void TalkAnimationTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!talkAnimationTextBox.Focused) return;

            SelectedNPCAppear.TalkAnimation = talkAnimationTextBox.Text;
        }

        private void UnkAnimationTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!unkAnimationTextBox.Focused) return;

            SelectedNPCAppear.UnkAnimation = unkAnimationTextBox.Text;
        }

        private void CompileButton_Click(object sender, EventArgs e)
        {
            string condPhaseAppear = "0";

            if (positionCondLineNumberRTB.Text != "" || positionCondLineNumberRTB.Text != " " || positionCondLineNumberRTB.Text != "0")
            {
                try
                {
                    condPhaseAppear = Condition.ToBase64String(positionCondLineNumberRTB.Text);
                    Console.WriteLine(condPhaseAppear);
                }
                catch
                {
                    MessageBox.Show("Failed to compile.");
                    return;
                }
            }

            SelectedNPCAppear.PhaseAppear = condPhaseAppear;
            MessageBox.Show("Compiled!");
        }

        private void DialogTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!dialogTextBox.Focused) return;

            if (MapText != null && SelectedTalkConfig != null)
            {
                int textID = SelectedTalkConfig.TalkValue;
                TextConfig textConfig = MapText.Texts.FirstOrDefault(x => x.Key  == textID).Value;

                if (textConfig != null)
                {
                   if (dialogVsTabControl.SelectedIndex >= textConfig.Strings.Count)
                   {
                        textConfig.Strings.Add(new StringLevel5());
                   }
                }

                textConfig.Strings[dialogVsTabControl.SelectedIndex].Text = dialogTextBox.Text;

                // Reorganise variance
                for (int i = 0; i < textConfig.Strings.Count; i++)
                {
                    textConfig.Strings[i].Variance = i;
                }
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dialogVsTabControl.TabPages.Count <= 2)
            {
                MessageBox.Show("You must have at least two tabs remaining.");
                return;
            }

            if (dialogVsTabControl.SelectedTab != null)
            {
                if (dialogVsTabControl.SelectedTab == addTabPage)
                {
                    MessageBox.Show("Cannot delete the add tab page.");
                    return;
                }

                dialogVsTabControl.TabPages.Remove(dialogVsTabControl.SelectedTab);

                ReorganizeTabNames();

                dialogVsTabControl.SelectedIndex = dialogVsTabControl.TabCount - 2;
            }
        }

        private void DialogVsTabControl_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void DialogVsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dialogVsTabControl.SelectedTab != null)
            {
                if (dialogVsTabControl.SelectedTab == addTabPage)
                {
                    TabPage newTab = new TabPage($"Text {dialogVsTabControl.TabPages.Count}");
                    newTab.BackColor = System.Drawing.Color.FromArgb(54, 54, 54);

                    dialogVsTabControl.TabPages.Add(newTab);

                    // Move AddTabPage to the end
                    if (dialogVsTabControl.TabPages.Contains(addTabPage))
                    {
                        dialogVsTabControl.TabPages.Remove(addTabPage);
                        dialogVsTabControl.TabPages.Add(addTabPage);
                    }

                    dialogVsTabControl.SelectedIndex = dialogVsTabControl.TabCount - 2;
                }
                else
                {
                    if (dialogTextBox.Parent != dialogVsTabControl.SelectedTab)
                    {
                        dialogTextBox.Parent = dialogVsTabControl.SelectedTab;
                        dialogTextBox.Location = new Point(6, 6);
                        SetText(dialogVsTabControl.SelectedIndex);
                    }
                }

                // Draw again
                dialogVsTabControl.Invalidate();
            } 
        }

        private void EventTypeFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventTypeFlatComboBox.Focused)
            {
                SelectedTalkConfig.TalkType = eventTypeFlatComboBox.SelectedIndex;
            }

            if (SelectedTalkConfig.TalkType == 1)
            {
                valueLabel.Visible = false;
                valueFlatNumericUpDown.Visible = false;
                scriptButton.Visible = false;
                dialogVsTabControl.Visible = true;
                eventConditionGroupBox.Size = new Size(391, 158);
                eventConditionGroupBox.Location = new Point(16, 184);
                eventCondLineNumberRTB.Size = new Size(379, 134);
                SetText(0);
            }
            else
            {
                valueLabel.Visible = true;
                valueFlatNumericUpDown.Visible = true;
                scriptButton.Visible = true;
                dialogVsTabControl.Visible = false;
                eventConditionGroupBox.Size = new Size(391, 265);
                eventConditionGroupBox.Location = new Point(16, 77);
                eventCondLineNumberRTB.Size = new Size(379, 241);
                dialogTextBox.Clear();
            }

            if (SelectedTalkConfig.TalkType == 3)
            {
                scriptButton.Enabled = true;
            }
            else
            {
                scriptButton.Enabled = false;
            }
        }

        private void ValueFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!valueFlatNumericUpDown.Focused) return;

            SelectedTalkConfig.TalkValue = Convert.ToInt32(valueFlatNumericUpDown.Value);
        }

        private void eventCompileButton_Click(object sender, EventArgs e)
        {
            string eventPhaseAppear = "0";

            if (eventCondLineNumberRTB.Text != "" && eventCondLineNumberRTB.Text != "0")
            {
                try
                {
                    eventPhaseAppear = Condition.ToBase64String(eventCondLineNumberRTB.Text);
                }
                catch
                {
                    MessageBox.Show("Failed to compile.");
                    return;
                }
            }

            SelectedTalkConfig.PhaseAppear = eventPhaseAppear;
            MessageBox.Show("Compiled!");
        }

        private void AddNewNPCGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewNPCWindow newNPCWindow = new NewNPCWindow(NPCs.Keys.Select(x => x.NPCID).Distinct().ToList());
            newNPCWindow.ShowDialog();

            if (newNPCWindow.SelectedNPCHash != 0)
            {
                int insertIndex = NPCs.Count;

                INPCBase newNPCBase = GameOpened.GetEmptyObject<INPCBase>();
                INPCAppear newNPCAppear = GameOpened.GetEmptyObject<INPCAppear>();

                if (GameOpened.Name == "Inazuma Eleven Go")
                {
                    newNPCBase = new NPCBase();
                    newNPCAppear = new NPCAppear();
                }

                newNPCBase.NPCID = newNPCWindow.SelectedNPCHash;
                newNPCAppear.PhaseAppear = "0";

                NPCs.Add(newNPCBase, new List<INPCAppear>() { newNPCAppear });

                FillNPCTreeView();
            }
        }

        private void AddNewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DeleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedNPCBase == null) return;

            // Get talk info of the npc
            ITalkInfo npcTalk = NPCEvents.FirstOrDefault(x => x.Key.TalkID == SelectedNPCBase.NPCID).Key;
            List<ITalkConfig> npcTalkConfigs = new List<ITalkConfig>();
        
            if (npcTalk != null)
            {
                // Get talk config from the talk info
                npcTalkConfigs = NPCEvents[npcTalk];
            }
            else
            {
                // Create NPC Talk
                npcTalk = GameOpened.GetEmptyObject<ITalkInfo>();

                if (GameOpened.Name == "Inazuma Eleven Go")
                {
                    npcTalk = new TalkInfo();
                }

                npcTalk.TalkID = SelectedNPCBase.NPCID;
            }

            // Insert new TalkConfig
            ITalkConfig newITalkConfig = GameOpened.GetEmptyObject<ITalkConfig>();
            if (GameOpened.Name == "Inazuma Eleven Go")
            {
                newITalkConfig = new TalkConfig();
            }

            // Set property of TalkConfig
            newITalkConfig.TalkType = 1;
            newITalkConfig.PhaseAppear = "0";

            // Create text entry
            int npcTextID = CreateTextEntry();
            if (npcTextID != 0)
            {
                newITalkConfig.TalkValue = npcTextID;
            }

            // Add element
            npcTalkConfigs.Add(newITalkConfig);

            // add new npc event in the dictionary
            if (!NPCEvents.ContainsKey(npcTalk))
            {
                NPCEvents.Add(npcTalk, npcTalkConfigs);
            }

            // Update event list box
            string conditionText = (SelectedNPCAppear.PhaseAppear == "0") ? "" : Condition.ToString(SelectedNPCAppear.PhaseAppear);
            FillEventTreeView(conditionText);
        }

        private void DeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}
