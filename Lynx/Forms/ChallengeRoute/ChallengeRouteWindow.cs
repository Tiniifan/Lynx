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
using Lynx.InazumaEleven.Games.GO;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using static Lynx.InazumaEleven.Games.GO.GOSupport;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using Font = System.Drawing.Font;
using Color = System.Drawing.Color;
using Control = System.Windows.Forms.Control;
using ChallengeRouteClass = Lynx.InazumaEleven.Logic.ChallengeRoute;
using Team = Lynx.InazumaEleven.Logic.Team;
using IGame = Lynx.InazumaEleven.Games.IGame;
using System.Text.RegularExpressions;
using Lynx.Level5.Save.Logic.Competition_Route;
using Lynx.Level5.Base64;
using DocumentFormat.OpenXml.Wordprocessing;
using Lynx.UI;
using System.Reflection;
using Lynx.Forms.Characters;
using Lynx.Level5.Save.Logic;
using Lynx.Level5.Save.Games;

namespace Lynx.Forms.ChallengeRoute
{
    public partial class ChallengeRouteWindow : Form
    {
        private IGame GameOpened;

        private const int HexSize = 25;

        private const int Columns = 10;

        private List<PointF[]> HexPoints = new List<PointF[]>();

        private List<ChallengeRouteClass> ChallengeRoutes;

        ChallengeRouteClass SelectedChallengeRoutes;

        IRouteConfig SelectedCell;

        private List<Team> Teams;

        private List<IItemConfig> ItemsConfigs;

        private Dictionary<int, string> ItemsNamesDict;

        private T2bþ Itemtext;

        private T2bþ TeamText;

        private T2bþ RouteText;

        private TreeNode RightClickNode;

        private int DraggingCell = -1;

        private bool IsDragging = false;

        private Point DraggingOffset;

        private List<string> FilesToDelete;

        public ChallengeRouteWindow(IGame game)
        {
            GameOpened = game;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            InitializeComponent();
            InitializeChallengeRouteResource();

            // Design 
            conditionLineNumberRTB.RichTextBox.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            conditionLineNumberRTB.RichTextBox.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            conditionLineNumberRTB.Strip.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            conditionLineNumberRTB.Strip.BoxedLineColor = System.Drawing.Color.FromArgb(35, 35, 35);
            conditionLineNumberRTB.Strip.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            conditionLineNumberRTB.RichTextBox.AcceptsTab = true;

            conditionLineNumberRTB.RichTextBox.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            conditionLineNumberRTB.RichTextBox.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            conditionLineNumberRTB.Strip.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            conditionLineNumberRTB.Strip.BoxedLineColor = System.Drawing.Color.FromArgb(35, 35, 35);
            conditionLineNumberRTB.Strip.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            conditionLineNumberRTB.RichTextBox.AcceptsTab = true;
        }

        private bool SameSkillID(int id, string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return id == crc32;
        }

        private string FindSoccerID(int soccerID)
        {
            for (int i = 0; i < 10000; i++)
            {
                string prefix = "";

                string trySoccerID = $"btl{prefix}{i.ToString().PadLeft(4, '0')}";

                if (SameSkillID(soccerID, trySoccerID))
                {
                    return trySoccerID;
                }
            }

            // Not found
            return "btl0000";
        }

        private Dictionary<int, string> GetNames(IItemConfig[] items)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var item in items)
            {
                // Determine the item name
                string name = item.NameID == 0x00
                    ? " "
                    : Itemtext.Nouns.TryGetValue(item.NameID, out var noun) && noun.Strings.Count > 0
                        ? noun.Strings[0].Text
                        : $"Item {index}";

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

                // Add the name to the output dictionary with the SkillHash as the key
                output[item.ItemID] = name;
                index++;
            }

            return output;
        }

        private void GetNames(Team[] teams)
        {
            int index = 0;
            foreach (var team in teams)
            {
                // Determine the team name
                string name = team.NameID == 0x00
                    ? " "
                    : TeamText.Nouns.TryGetValue(team.NameID, out var noun) && noun.Strings.Count > 0
                        ? noun.Strings[0].Text
                        : $"Team {index}";


                team.Name = $"{name} ({FindSoccerID(team.ID)})";
                index++;
            }
        }

        private void DrawCell(int cellnum, Graphics g, Font font, Brush textBrush, StringFormat format, int col, int row, int xOffset, int yOffset, int rowOffset, int colWidth, int HexSize, List<(int, int)> cellPositions)
        {
            IRouteConfig cell = SelectedChallengeRoutes.Cells.FirstOrDefault(route => route.CellNum == cellnum);

            int x = col * colWidth + 10 + xOffset;
            int y = row * 2 * rowOffset + ((col % 2 == 0) ? rowOffset : 0) + 10 + yOffset;

            if (IsDragging && cellnum == DraggingCell)
            {
                x = DraggingOffset.X;
                y = DraggingOffset.Y;
            }

            // Calcul des points de l'hexagone
            PointF[] points = new PointF[6];
            for (int i = 0; i < 6; i++)
            {
                double angle = Math.PI / 3 * i;
                points[i] = new PointF(
                    x + HexSize * (float)Math.Cos(angle),
                    y + HexSize * (float)Math.Sin(angle)
                );
            }

            // Add the position (x, y) to the list
            cellPositions.Add((x, y));

            // add the point
            HexPoints.Add(points);

            if (cell != null)
            {
                // Draw hex border
                if (SelectedCell != null && cellnum == SelectedCell.CellNum)
                {
                    g.DrawPolygon(Pens.Red, points);
                } else
                {
                    g.DrawPolygon(Pens.White, points);
                }


                if (cell.CellType == 0x01)
                {
                    g.DrawString("Start", font, Brushes.Red, x, y, format); // Texte en blanc
                }
                else if (cell.CellType == 0x02)
                {
                    if (Teams.Any(myTeam => myTeam.ID == cell.ContentID))
                    {
                        Team team = Teams.FirstOrDefault(myTeam => myTeam.ID == cell.ContentID);

                        GameSupports.GameFile emblemInfo = GameOpened.Files["emblem"];
                        VirtualDirectory emblemFolder = emblemInfo.File.Directory.GetFolderFromFullPath(emblemInfo.Path);
                        string emblemFileName = "emb" + team.Emblem.ToString().PadLeft(4, '0') + ".xi";
                        Bitmap emblemPicture = null;

                        if (emblemFolder.Files.ContainsKey(emblemFileName))
                        {
                            try
                            {
                                byte[] imageData = emblemInfo.File.Directory.GetFileFromFullPath(emblemInfo.Path + "/" + emblemFileName);
                                emblemPicture = IMGC.ToBitmap(imageData);
                            }
                            catch
                            {
                                emblemPicture = null;
                            }
                        }

                        // Si l'emblème existe, dessiner l'emblème
                        if (emblemPicture != null)
                        {
                            g.DrawImage(emblemPicture, x - HexSize / 2, y - HexSize / 2, HexSize, HexSize);
                            
                        }
                        else
                        {
                            // Sinon, dessiner l'hexagone en vert avec le texte "Team"
                            g.DrawString("Team", font, Brushes.Green, x, y, format);  // Texte en blanc
                        }
                    }
                }
                else if (cell.CellType == 0x03)
                {
                    // Dessiner l'hexagone en bleu avec le texte "Chest"
                    g.DrawString("Chest", font, Brushes.Blue, x, y, format);  // Texte en blanc
                }
                else if (cell.CellType == 0x04)
                {
                    // Dessiner l'hexagone en jaune avec le texte "Chest"
                    g.DrawString("Chest", font, Brushes.Yellow, x, y, format);  // Texte en blanc
                }
                else
                {
                    // Dessiner l'hexagone de base sans background, juste le texte en blanc
                    g.DrawString(cellnum.ToString(), font, Brushes.White, x, y, format);  // Texte en blanc
                }
            }
            else
            {
                // Dessiner l'hexagone de base sans background, juste le texte en blanc
                g.DrawPolygon(Pens.White, points);  // Contour blanc
                g.DrawString(cellnum.ToString(), font, Brushes.White, x, y, format);  // Texte en blanc
            }
        }

        private void FillCellFlatComboBox()
        {
            // Clear existing items
            cellFlatComboBox.Items.Clear();
            cellLinkFlatComboBox1.Items.Clear();
            cellLinkFlatComboBox2.Items.Clear();
            cellLinkFlatComboBox3.Items.Clear();

            // Trier les cellules par CellNum croissant
            var sortedCells = SelectedChallengeRoutes.Cells.OrderBy(cell => cell.CellNum);

            // Iterate over sorted challenge route cells
            foreach (var cell in sortedCells)
            {
                string cellname = $"Cell {cell.CellNum} - ";

                switch (cell.CellType)
                {
                    case 1:
                        cellname += "Start";
                        break;
                    case 2:
                        var team = Teams.FirstOrDefault(x => x.ID == cell.ContentID);
                        cellname += team != null
                            ? $"VS {team.Name}"
                            : "VS Unknown Team";
                        break;
                    case 3:
                    case 4:
                        cellname += ItemsNamesDict.ContainsKey(cell.ContentID)
                            ? $"Get {ItemsNamesDict[cell.ContentID]}"
                            : "VS Unknown Item";
                        break;
                    default:
                        cellname += "None";
                        break;
                }

                cellFlatComboBox.Items.Add(cellname);
            }

            cellLinkFlatComboBox1.Items.AddRange(cellFlatComboBox.Items.Cast<Object>().ToArray());
            cellLinkFlatComboBox2.Items.AddRange(cellFlatComboBox.Items.Cast<Object>().ToArray());
            cellLinkFlatComboBox3.Items.AddRange(cellFlatComboBox.Items.Cast<Object>().ToArray());
        }

        public static int GetCellNumber(string input)
        {
            Match match = Regex.Match(input, @"\bCell\s+(\d+)\b", RegexOptions.IgnoreCase);
            return match.Success ? int.Parse(match.Groups[1].Value) : -1;
        }

        private bool SelectCellByNumber(ComboBox comboBox, int cellnum)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString().StartsWith($"Cell {cellnum} -"))
                {
                    comboBox.SelectedIndex = i;
                    return true;
                }
            }

            comboBox.SelectedIndex = -1;
            comboBox.Text = "";

            return false;
        }

        private int GetCellUnderCursor(PointF point)
        {
            for (int hexIndex = 0; hexIndex < HexPoints.Count; hexIndex++)
            {
                if (hexIndex != DraggingCell)
                {
                    PointF[] polygon = HexPoints[hexIndex];
                    int j = polygon.Length - 1;
                    bool inside = false;

                    for (int i = 0; i < polygon.Length; i++)
                    {
                        if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                            (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) /
                            (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                        {
                            inside = !inside;
                        }
                        j = i;
                    }

                    if (inside)
                    {
                        return hexIndex;
                    }
                }
            }
            return -1; // Aucune cellule trouvée
        }

        private void SwapCells(int oldCellNum, int newCellNum)
        {
            if (SelectedCell == null) return;

            // Check if the new cell number is already assigned to another cell
            IRouteConfig conflictRoute = SelectedChallengeRoutes.Cells.FirstOrDefault(x => x.CellNum == newCellNum);

            if (conflictRoute != null)
            {
                // Swap cell numbers to avoid conflicts
                conflictRoute.CellNum = oldCellNum;
            }

            // Assign the new cell number
            SelectedCell.CellNum = newCellNum;

            // Refresh the UI to reflect the changes
            RouteListBox_SelectedIndexChanged(routeListBox, EventArgs.Empty);
            SelectCellByNumber(cellFlatComboBox, newCellNum);
        }

        private void DeleteSelectedCell()
        {
            if (SelectedChallengeRoutes != null & SelectedCell != null)
            {
                if (cellFlatComboBox.SelectedItem != null)
                {
                    string selectedCell = cellFlatComboBox.SelectedItem.ToString();
                    DialogResult result = MessageBox.Show(
                        $"Are you sure you want to delete {selectedCell}?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        SelectedChallengeRoutes.Cells.RemoveAll(x => x.CellNum == SelectedCell.CellNum);

                        SelectedCell = null;
                        informationGroupBox.Enabled = false;
                        linkGroupBox.Enabled = false;
                        conditionGroupBox.Enabled = false;
                        deleteButton.Enabled = false;

                        previewPictureBox.Invalidate();

                        MessageBox.Show($"{selectedCell} removed!");

                        // Refresh the UI to reflect the changes
                        RouteListBox_SelectedIndexChanged(routeListBox, EventArgs.Empty);
                    }
                }
            }
        }

        private void AddCell(int cellNum)
        {
            // The cell does not exist, so prompt the user to create it
            DialogResult result = MessageBox.Show(
                $"Cell {cellNum} is not assigned, do you want to create this cell?",
                "Cell Not Found",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                IRouteConfig newCell = GameOpened.GetEmptyObject<IRouteConfig>();

                newCell.CellNum = cellNum;
                newCell.CellLink1 = -1;
                newCell.CellLink2 = -1;
                newCell.CellLink3 = -1;
                newCell.PhaseAppear = "0";
                newCell.Map = "";
                newCell.MatchTextLock = null;

                SelectedChallengeRoutes.Cells.Add(newCell);

                // Refresh the UI to reflect the changes
                RouteListBox_SelectedIndexChanged(routeListBox, EventArgs.Empty);
                SelectCellByNumber(cellFlatComboBox, cellNum);
            }
        }

        private void ChangeCellLink(FlatComboBox comboBox, int linkNum)
        {
            // Ensure the control is focused, a valid index is selected, and a cell is available
            if (!comboBox.Focused || comboBox.SelectedIndex == -1 || SelectedChallengeRoutes == null || SelectedCell == null) return;

            int cellNum = GetCellNumber(comboBox.SelectedItem.ToString());

            switch (linkNum)
            {
                case 1:
                    SelectedCell.CellLink1 = cellNum;
                    break;
                case 2:
                    SelectedCell.CellLink2 = cellNum;
                    break;
                case 3:
                    SelectedCell.CellLink3 = cellNum;
                    break;
            }

            // Refresh the UI to reflect the changes
            RouteListBox_SelectedIndexChanged(routeListBox, EventArgs.Empty);
            SelectCellByNumber(cellFlatComboBox, SelectedCell.CellNum);
        }

        private void UnlinkCell(int linkNum)
        {
            // Ensure that the cell is available
            if (SelectedChallengeRoutes == null || SelectedCell == null) return;

            switch (linkNum)
            {
                case 1:
                    SelectedCell.CellLink1 = -1;
                    break;
                case 2:
                    SelectedCell.CellLink2 = -1;
                    break;
                case 3:
                    SelectedCell.CellLink3 = -1;
                    break;
            }

            // Refresh the UI to reflect the changes
            RouteListBox_SelectedIndexChanged(routeListBox, EventArgs.Empty);
            SelectCellByNumber(cellFlatComboBox, SelectedCell.CellNum);
        }

        private void ResetCombobox(FlatComboBox comboBox)
        {
            comboBox.SelectedIndex = -1;
            comboBox.Text = "";
        }

        private void ResetCellPanel()
        {
            cellNumberFlatNumericUpDown.Value = 0;
            cellFlagFlatNumericUpDown.Value = 0;
            ResetCombobox(cellTypeFlatComboBox);
            ResetCombobox(cellContentFlatComboBox);
            ResetCombobox(cellLinkFlatComboBox1);
            ResetCombobox(cellLinkFlatComboBox2);
            ResetCombobox(cellLinkFlatComboBox3);
            ResetCombobox(matchFlatComboBox);
            textLockTextBox.Text = "";
            mapTextBox.Text = "";
            conditionLineNumberRTB.RichTextBox.Text = "";
        }

        private void FillRouteListBox()
        {
            int index = 0;

            routeListBox.Items.Clear();
            
            foreach(ChallengeRouteClass challengeRoutes in ChallengeRoutes)
            {
                string name = challengeRoutes.NameID == 0x00
                    ? $"Route {index}"
                    : RouteText.Nouns.TryGetValue(challengeRoutes.NameID, out var noun) && noun.Strings.Count > 0
                        ? noun.Strings[0].Text
                        : $"Route {index}";

                routeListBox.Items.Add(name);

                index++;
            }
        }

        private string GenerateFileName()
        {
            // Récupérer tous les numéros existants
            HashSet<int> existingNumbers = new HashSet<int>();

            foreach (var challenge in ChallengeRoutes)
            {
                if (challenge.Filename.StartsWith("scr_br_") && challenge.Filename.EndsWith(".cfg.bin"))
                {
                    string numberPart = challenge.Filename.Replace("scr_br_", "").Replace(".cfg.bin", "").Replace("_d", "").Replace("_s", "");

                    if (int.TryParse(numberPart, out int num))
                    {
                        existingNumbers.Add(num);
                    }
                }
            }

            // Trouver le premier numéro manquant entre 1 et 1000
            for (int i = 1; i <= 1000; i++)
            {
                if (!existingNumbers.Contains(i))
                {
                    return $"scr_br_{i:D4}.cfg.bin"; // Format en 4 chiffres (ex: 0001)
                }
            }

            throw new Exception("No available filename found in range 1-1000.");
        }

        private void InitializeChallengeRouteResource()
        {
            FilesToDelete = new List<string>();

            GameSupports.GameFile skillTextGameFile = GameOpened.Files["item_text"];
            Itemtext = new T2bþ(skillTextGameFile.File.Directory.GetFileFromFullPath(skillTextGameFile.Path));
            GameSupports.GameFile teamText = GameOpened.Files["team_text"];
            TeamText = new T2bþ(teamText.File.Directory.GetFileFromFullPath(teamText.Path));
            GameSupports.GameFile trouteText = GameOpened.Files["troute_text"];
            RouteText = new T2bþ(trouteText.File.Directory.GetFileFromFullPath(trouteText.Path));

            matchFlatComboBox.Items.AddRange(MatchRestrictions.IEGO.ToArray());

            ItemsConfigs = GameOpened.GetItems("all").ToList();
            ItemsNamesDict = GetNames(ItemsConfigs.ToArray());

            // load team
            ISoccerInfo[] soccers = GameOpened.GetSoccers();
            ITeamParamInfo[] teamParams = GameOpened.GetTeamParams();
            IStoryTeamInfo[] storyTeams = GameOpened.GetStoryTeams();
            IEncountTeamInfo[] encountTeams = GameOpened.GetEncounterTeams();
            Teams = soccers.Select(soccer =>
            {
                // Chercher TeamParam correspondant au Soccer
                var teamParam = teamParams.FirstOrDefault(tp => tp.TeamParamID == soccer.TeamParamID);

                if (teamParam != null)
                {
                    // Chercher TeamConfigID dans StoryTeams
                    var storyTeam = storyTeams.FirstOrDefault(st => st.TeamConfigID == teamParam.TeamConfigID);
                    if (storyTeam != null)
                    {
                        return new Team(soccer.SoccerID, storyTeam.Emblem2, storyTeam.NameID, teamParam.Level);
                    }

                    // Chercher TeamConfigID dans EncountTeams
                    var encountTeam = encountTeams.FirstOrDefault(et => et.TeamConfigID == teamParam.TeamConfigID);
                    if (encountTeam != null)
                    {
                        return new Team(soccer.SoccerID, encountTeam.Emblem2, encountTeam.NameID, teamParam.Level);
                    }
                }

                // Si aucune correspondance trouvée, Emblem = -1
                return new Team(soccer.SoccerID, -1, 0, 0);

            }).ToList();
            GetNames(Teams.ToArray());

            // initialise data
            routeListBox.Items.Clear();
            ChallengeRoutes = new List<ChallengeRouteClass>();

            // Get all routes files
            GameSupports.GameFile soccerFolder = GameOpened.Files["soccer"];
            string[] files = GameOpened.Game.Directory.GetFolderFromFullPath(soccerFolder.Path).Files.Keys.ToArray();

            foreach (string file in files)
            {
                if (file.StartsWith("scr_br"))
                {
                    (IRouteConfig[], int) route = GameOpened.GetRoutes(file);

                    foreach (IRouteConfig cell in route.Item1)
                    {
                        if (cell.Map == null)
                        {
                            cell.Map = "";
                        }
                    }

                    ChallengeRoutes.Add(new ChallengeRouteClass(file, route.Item2, route.Item1.ToList()));
                }
            }

            FillRouteListBox();
        }

        private void ChallengeRouteWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Remove files
            VirtualDirectory directory = GameOpened.Game.Directory.GetFolderFromFullPath($"/data/res/soccer/");
            foreach (string file in FilesToDelete)
            {
                if (directory.Files.ContainsKey(file))
                {
                    directory.Files.Remove(file);
                }
            }

            foreach(ChallengeRouteClass challengeRoute in ChallengeRoutes)
            {
                GameOpened.SaveRoutes(challengeRoute.Filename, challengeRoute.NameID, challengeRoute.Cells.ToArray());
            }

            if (RouteText != null)
            {
                GameOpened.SaveTextFile(GameOpened.Files["troute_text"], RouteText);
            }
        }

        private void PreviewPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (routeListBox.SelectedIndex == -1)
                return;

            HexPoints.Clear();

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int cellCount = 0;
            int rowOffset = (int)(Math.Sqrt(3) * HexSize / 2);
            int colWidth = (int)(1.5 * HexSize);
            int xOffset = 25;
            int yOffset = 45;
            int extraColumn = Math.Max(0, (Columns / 5) - 1);

            Font font = new Font("Arial", 9, FontStyle.Bold);
            Brush textBrush = Brushes.White;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Pattern qui redémarre toutes les 6 colonnes
            int[] pattern = { 2, 3, 2, 3, 2, 2 };

            List<(int, int)> cellPositions = new List<(int, int)>();

            for (int col = 0; col < Columns + extraColumn; col++)
            {
                int numCells = pattern[col % 6]; // Répétition du cycle toutes les 6 colonnes

                // Vérifier si on est dans le dernier cas de pattern (i == 5) avant de dessiner les cellules
                if ((col % 6) == 5) // Dernière case du pattern
                {
                    // Placer 3 cellules non numérotées avant de dessiner les cellules normales
                    for (int sepRow = 0; sepRow < 3; sepRow++)
                    {
                        int x = col * colWidth + 10 + xOffset;
                        int y = sepRow * 2 * rowOffset + ((col % 2 == 0) ? rowOffset : 0) + 10 + yOffset;

                        // Calcul des points de l'hexagone pour les cellules non numérotées
                        PointF[] points = new PointF[6];
                        for (int i = 0; i < 6; i++)
                        {
                            double angle = Math.PI / 3 * i;
                            points[i] = new PointF(
                                x + HexSize * (float)Math.Cos(angle),
                                y + HexSize * (float)Math.Sin(angle)
                            );
                        }

                        g.DrawPolygon(Pens.White, points);
                    }

                    col += 1;
                }

                // Dessiner les cellules selon le pattern
                for (int row = 0; row < numCells; row++)
                {
                    DrawCell(cellCount, g, font, textBrush, format, col, row, xOffset, yOffset, rowOffset, colWidth, HexSize, cellPositions);
                    cellCount++;
                }
            }

            // Draw lines between cells based on the links in SelectedChallengeRoutes
            foreach (var route in SelectedChallengeRoutes.Cells)
            {
                

                (int, int) cellPosition = cellPositions[route.CellNum];

                List<int> links = new List<int> { route.CellLink1, route.CellLink2, route.CellLink3 };

                foreach (var link in links)
                {
                    if (link != -1)
                    {
                        (int, int) linkCellPositions = cellPositions[link];

                        // Draw selected connexion
                        if (SelectedCell != null && SelectedCell == route)
                        {
                            Pen customPen = new Pen(Color.Red, 3);
                            g.DrawLine(customPen, cellPosition.Item1, cellPosition.Item2, linkCellPositions.Item1, linkCellPositions.Item2);
                        }
                        else
                        {
                            Pen customPen = new Pen(Color.White, 3);
                            g.DrawLine(customPen, cellPosition.Item1, cellPosition.Item2, linkCellPositions.Item1, linkCellPositions.Item2);
                        }           
                    }
                }
            }
        }

        private void PreviewPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (SelectedCell != null)
                {
                    DraggingCell = SelectedCell.CellNum;
                    IsDragging = true;
                }
            } else if (e.Button == MouseButtons.Left)
            {
                PointF clickPoint = previewPictureBox.PointToClient(Control.MousePosition);

                for (int hexIndex = 0; hexIndex < HexPoints.Count; hexIndex++)
                {
                    PointF[] polygon = HexPoints[hexIndex];
                    int j = polygon.Length - 1;
                    bool inside = false;

                    for (int i = 0; i < polygon.Length; i++)
                    {
                        if (((polygon[i].Y > clickPoint.Y) != (polygon[j].Y > clickPoint.Y)) &&
                            (clickPoint.X < (polygon[j].X - polygon[i].X) * (clickPoint.Y - polygon[i].Y) /
                            (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                        {
                            inside = !inside;
                        }
                        j = i;
                    }

                    if (inside)
                    {
                        if (SelectCellByNumber(cellFlatComboBox, hexIndex) == false)
                        {
                            AddCell(hexIndex);
                        }

                        return;
                    }
                }
            }
        }

        private void PreviewPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging && e.Button == MouseButtons.Right)
            {
                DraggingOffset = e.Location;
                previewPictureBox.Invalidate();
            }
        }

        private void PreviewPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsDragging)
            {
                IsDragging = false;

                previewPictureBox.Invalidate();

                int newCellNum = GetCellUnderCursor(e.Location);

                if (newCellNum != -1 && newCellNum != DraggingCell)
                {
                    SwapCells(DraggingCell, newCellNum);
                }

                DraggingCell = -1;
            }
        }

        private void PreviewPictureBox_MouseEnter(object sender, EventArgs e)
        {
            previewPictureBox.Focus();
        }

        private void RouteListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (routeListBox.SelectedIndex == -1) return;

            informationGroupBox.Enabled = false;
            linkGroupBox.Enabled = false;
            restrictionGroupBox.Enabled = false;
            conditionGroupBox.Enabled = false;

            SelectedChallengeRoutes = ChallengeRoutes[routeListBox.SelectedIndex];
            previewPictureBox.Invalidate();

            nameTextBox.Text = routeListBox.SelectedItem.ToString();
            filenameTextBox.Text = SelectedChallengeRoutes.Filename;

            // fill element in cell flat combobox
            FillCellFlatComboBox();

            if (cellFlatComboBox.Items.Count > 0)
            {
                cellFlatComboBox.SelectedIndex = 0;
            }

            challengeRouteGroupBox.Enabled = true;
        }

        private void CellFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cellFlatComboBox.SelectedIndex == -1) return;

            ResetCellPanel();

            int cellNumber = GetCellNumber(cellFlatComboBox.SelectedItem.ToString());

            if (cellNumber != -1)
            {
                SelectedCell = SelectedChallengeRoutes.Cells.FirstOrDefault(x => x.CellNum == cellNumber);

                if (SelectedCell != null)
                {
                    cellNumberFlatNumericUpDown.Value = SelectedCell.CellNum;
                    cellFlagFlatNumericUpDown.Value = SelectedCell.Flag;
                    cellTypeFlatComboBox.SelectedIndex = SelectedCell.CellType;
                    matchFlatComboBox.SelectedIndex = SelectedCell.MatchRestriction;
                    textLockTextBox.Text = SelectedCell.MatchTextLock;
                    mapTextBox.Text = SelectedCell.Map;

                    // Clear cell content
                    cellContentFlatComboBox.Items.Clear();
                    cellContentFlatComboBox.Enabled = true;

                    if (SelectedCell.CellType == 2)
                    {
                        // team
                        cellContentFlatComboBox.Items.AddRange(Teams.ToArray());

                        var team = Teams.FirstOrDefault(x => x.ID == SelectedCell.ContentID);

                        if (team != null)
                        {
                            cellContentFlatComboBox.SelectedItem = team;
                        } else
                        {

                        }
                    } 
                    else if (SelectedCell.CellType == 3 || SelectedCell.CellType == 4)
                    {
                        // item
                        cellContentFlatComboBox.Items.AddRange(ItemsNamesDict.Values.ToArray());

                        var item = ItemsConfigs.FirstOrDefault(x => x.ItemID == SelectedCell.ContentID);

                        if (item != null)
                        {
                            cellContentFlatComboBox.SelectedIndex = ItemsConfigs.IndexOf(item);
                        }
                        else
                        {

                        }
                    } 
                    else
                    {
                        SelectedCell.ContentID = 0x0;
                        cellContentFlatComboBox.SelectedIndex = -1;
                        cellContentFlatComboBox.Text = "";
                        cellContentFlatComboBox.Enabled = false;
                    }

                    // links
                    SelectCellByNumber(cellLinkFlatComboBox1, SelectedCell.CellLink1);
                    SelectCellByNumber(cellLinkFlatComboBox2, SelectedCell.CellLink2);
                    SelectCellByNumber(cellLinkFlatComboBox3, SelectedCell.CellLink3);

                    string conditionText = (SelectedCell.PhaseAppear == "0") ? "" : Condition.ToString(SelectedCell.PhaseAppear);
                    conditionLineNumberRTB.RichTextBox.Text = conditionText;

                    informationGroupBox.Enabled = true;
                    linkGroupBox.Enabled = true;
                    restrictionGroupBox.Enabled = true;
                    conditionGroupBox.Enabled = true;
                    conditionGroupBox.Enabled = true;

                    previewPictureBox.Invalidate();
                }
            }
        }

        private void CellNumberFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Ensure the control is focused and a cell is selected before proceeding
            if (!cellNumberFlatNumericUpDown.Focused || SelectedCell == null) return;

            // Store the old cell number before changing it
            int oldCellNum = SelectedCell.CellNum;
            int newCellNum = Convert.ToInt32(cellNumberFlatNumericUpDown.Value);

            SwapCells(oldCellNum, newCellNum);
        }

        private void CellFlagFlatNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Ensure the control is focused and a cell is selected before proceeding
            if (!cellFlagFlatNumericUpDown.Focused || SelectedCell == null) return;

            // Update the flag value of the selected cell based on the numeric input
            SelectedCell.Flag = Convert.ToInt32(cellFlagFlatNumericUpDown.Value);
        }

        private void CellTypeFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ensure the control is focused, a valid index is selected, and a cell is available
            if (!cellTypeFlatComboBox.Focused || cellTypeFlatComboBox.SelectedIndex == -1 || SelectedCell == null) return;

            // Update the selected cell's type based on the chosen index
            SelectedCell.CellType = cellTypeFlatComboBox.SelectedIndex;

            // Refresh the UI to reflect the changes
            RouteListBox_SelectedIndexChanged(routeListBox, EventArgs.Empty);
            SelectCellByNumber(cellFlatComboBox, SelectedCell.CellNum);
        }

        private void CellContentFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ensure the control is focused, a valid index is selected, and a cell is available
            if (!cellContentFlatComboBox.Focused || cellContentFlatComboBox.SelectedIndex == -1 || SelectedCell == null) return;

            if (SelectedCell.CellType == 2)
            {
                // Assign the selected team ID to the cell's content
                SelectedCell.ContentID = (cellContentFlatComboBox.SelectedItem as Team).ID;
            }
            else if (SelectedCell.CellType == 3 || SelectedCell.CellType == 4)
            {
                // Assign the corresponding item ID if the selected item exists in the dictionary
                if (ItemsNamesDict.Any(x => x.Value == cellContentFlatComboBox.SelectedItem.ToString()))
                {
                    SelectedCell.ContentID = ItemsNamesDict.FirstOrDefault(x => x.Value == cellContentFlatComboBox.SelectedItem.ToString()).Key;
                }
            }

            // Refresh the UI to reflect the changes
            RouteListBox_SelectedIndexChanged(routeListBox, EventArgs.Empty);
            SelectCellByNumber(cellFlatComboBox, SelectedCell.CellNum);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddCellWindow addCellWindow = new AddCellWindow(SelectedChallengeRoutes.Cells.Select(x => x.CellNum).Distinct().ToList());
            addCellWindow.ShowDialog();

            if (addCellWindow.SelectedCell != -1)
            {
                AddCell(addCellWindow.SelectedCell);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteSelectedCell();
        }

        private void ChallengeRouteWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Control focusedControl = this.ActiveControl;

                if (focusedControl is TextBox ||
                    focusedControl is Button ||
                    focusedControl is ComboBox ||
                    focusedControl is NumericUpDown || 
                    focusedControl is LineNumberRTB ||
                    focusedControl is RichTextBox)
                {
                    return;
                }

                DeleteSelectedCell();
            }
        }

        private void AddAllCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedChallengeRoutes != null)
            {
                int cellIndex = -1; 

                for (int i = 0; i < 24; i++)
                {
                    if (!SelectedChallengeRoutes.Cells.Any(x => x.CellNum == i))
                    {
                        IRouteConfig newCell = GameOpened.GetEmptyObject<IRouteConfig>();

                        newCell.CellNum = i;
                        newCell.CellLink1 = -1;
                        newCell.CellLink2 = -1;
                        newCell.CellLink3 = -1;
                        newCell.PhaseAppear = "0";
                        newCell.Map = "";
                        newCell.MatchTextLock = null;

                        SelectedChallengeRoutes.Cells.Add(newCell);

                        cellIndex = i;
                    }
                }

                if (cellIndex != -1)
                {
                    // Refresh the UI to reflect the changes
                    RouteListBox_SelectedIndexChanged(routeListBox, EventArgs.Empty);
                    SelectCellByNumber(cellFlatComboBox, cellIndex);
                }
            }
        }

        private void RemoveAllCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedChallengeRoutes != null)
            {
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete all cells?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    SelectedChallengeRoutes.Cells.Clear();

                    SelectedCell = null;
                    informationGroupBox.Enabled = false;
                    linkGroupBox.Enabled = false;
                    conditionGroupBox.Enabled = false;
                    deleteButton.Enabled = false;

                    previewPictureBox.Invalidate();

                    MessageBox.Show($"All cells was removed!");

                    // Refresh the UI to reflect the changes
                    RouteListBox_SelectedIndexChanged(routeListBox, EventArgs.Empty);
                }
            }
        }

        private void TextLockTextBox_TextChanged(object sender, EventArgs e)
        {
            // Ensure the control is focused, a valid index is selected, and a cell is available
            if (!textLockTextBox.Focused || cellContentFlatComboBox.SelectedIndex == -1 || SelectedCell == null) return;

            if (textLockTextBox.Text == "")
            {
                SelectedCell.MatchTextLock = null;
            } else
            {
                SelectedCell.MatchTextLock = textLockTextBox.Text;
            }        
        }

        private void MapTextBox_TextChanged(object sender, EventArgs e)
        {
            // Ensure the control is focused, a valid index is selected, and a cell is available
            if (!mapTextBox.Focused || cellContentFlatComboBox.SelectedIndex == -1 || SelectedCell == null) return;

            SelectedCell.Map = mapTextBox.Text;
        }

        private void CellLinkFlatComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCellLink(sender as FlatComboBox, 1);
        }

        private void CellLinkFlatComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCellLink(sender as FlatComboBox, 2);
        }

        private void cellLinkFlatComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCellLink(sender as FlatComboBox, 3);
        }

        private void UnlinButton1_Click(object sender, EventArgs e)
        {
            UnlinkCell(1);
        }

        private void UnlinkButton2_Click(object sender, EventArgs e)
        {
            UnlinkCell(2);
        }

        private void UnlinkButton3_Click(object sender, EventArgs e)
        {
            UnlinkCell(3);
        }

        private void MatchFlatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ensure the control is focused, a valid index is selected, and a cell is available
            if (!matchFlatComboBox.Focused || matchFlatComboBox.SelectedIndex == -1 || SelectedCell == null) return;

            SelectedCell.MatchRestriction = matchFlatComboBox.SelectedIndex;
        }

        private void NameTextBox_Click(object sender, EventArgs e)
        {
            if (SelectedChallengeRoutes == null) return;

            Nyanko.Nyanko nyanko = new Nyanko.Nyanko(Path.GetFileName(GameOpened.Files["troute_text"].Path), RouteText, false, true, SelectedChallengeRoutes.NameID);
            nyanko.ShowDialog();
            RouteText = nyanko.T2bþFileOpened;

            // Update current name
            if (nyanko.SelectedHash != 0)
            {
                SelectedChallengeRoutes.NameID = nyanko.SelectedHash;
            }

            int selectedIndex = routeListBox.SelectedIndex;

            // Reload
            FillRouteListBox();

            routeListBox.SelectedIndex = selectedIndex;
        }

        private void FilenameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!filenameTextBox.Focused && SelectedChallengeRoutes == null) return;

            // Vérifie si le champ est vide ou si "scr_br" a été retiré
            if (string.IsNullOrWhiteSpace(filenameTextBox.Text) || !filenameTextBox.Text.Contains("scr_br"))
            {
                filenameTextBox.Text = GenerateFileName();
            }

            SelectedChallengeRoutes.Filename = filenameTextBox.Text;
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = GenerateFileName();

            if (FilesToDelete.Contains(filename))
            {
                FilesToDelete.Remove(filename);
            }

            ChallengeRoutes.Add(new ChallengeRouteClass(filename, 0, new List<IRouteConfig>()));
            FillRouteListBox();
            routeListBox.SelectedIndex = routeListBox.Items.Count - 1;
            cellFlatComboBox.SelectedIndex = -1;
            cellFlatComboBox.Text = "";
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (routeListBox.SelectedIndex == -1) return;

            string routeName = routeListBox.SelectedItem.ToString();

            DialogResult dialogResult = MessageBox.Show("Do you want to delete " + routeName + "?", "Delete Route", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                challengeRouteGroupBox.Enabled = false;

                FilesToDelete.Add(SelectedChallengeRoutes.Filename);
                ChallengeRoutes.Remove(SelectedChallengeRoutes);

                SelectedChallengeRoutes = null;
                routeListBox.SelectedIndex = -1;
                previewPictureBox.Image = null;

                MessageBox.Show(routeName + " has been removed!");

                // Reload
                FillRouteListBox();
            }
        }
    }
}
