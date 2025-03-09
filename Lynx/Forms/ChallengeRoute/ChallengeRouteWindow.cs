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

namespace Lynx.Forms.ChallengeRoute
{
    public partial class ChallengeRouteWindow : Form
    {
        private IGame GameOpened;

        private const int HexSize = 25;

        private const int Columns = 10;

        private Dictionary<string, List<IRouteConfig>> ChallengeRoutes;

        private Dictionary<string, int> ChallengeRoutesNamesDict;

        List<IRouteConfig> SelectedChallengeRoutes;

        private List<Team> Teams;

        private Dictionary<int, Team> TeamsNameDict;

        private List<IItemConfig> ItemsConfigs;

        private Dictionary<int, string> ItemsNamesDict;

        private T2bþ Itemtext;

        private T2bþ TeamText;

        private T2bþ RouteText;

        private TreeNode RightClickNode;

        public ChallengeRouteWindow(IGame game)
        {
            GameOpened = game;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            InitializeComponent();
            InitializeChallengeRouteResource();
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
                output[item.NameID] = name;
                index++;
            }

            return output;
        }

        private Dictionary<int, Team> GetNames(Team[] teams)
        {
            Dictionary<int, Team> output = new Dictionary<int, Team>();
            Dictionary<string, int> nameCounts = new Dictionary<string, int>();

            int index = 0;
            foreach (var team in teams)
            {
                // Determine the team name
                string name = team.NameID == 0x00
                    ? " "
                    : TeamText.Nouns.TryGetValue(team.NameID, out var noun) && noun.Strings.Count > 0
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

                team.Name = name;
                output[team.ID] = team;
                index++;
            }

            return output;
        }

        private void InitializeChallengeRouteResource()
        {
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
                        return new Team(soccer.SoccerID, storyTeam.Emblem2, storyTeam.NameID);
                    }

                    // Chercher TeamConfigID dans EncountTeams
                    var encountTeam = encountTeams.FirstOrDefault(et => et.TeamConfigID == teamParam.TeamConfigID);
                    if (encountTeam != null)
                    {
                        return new Team(soccer.SoccerID, encountTeam.Emblem2, encountTeam.NameID);
                    }
                }

                // Si aucune correspondance trouvée, Emblem = -1
                return new Team(soccer.SoccerID, -1, 0);

            }).ToList();
            TeamsNameDict = GetNames(Teams.ToArray());

            // initialise data
            routeListBox.Items.Clear();
            ChallengeRoutes = new Dictionary<string, List<IRouteConfig>>();
            ChallengeRoutesNamesDict = new Dictionary<string, int>();

            // Get all routes files
            GameSupports.GameFile soccerFolder = GameOpened.Files["soccer"];
            string[] files = GameOpened.Game.Directory.GetFolderFromFullPath(soccerFolder.Path).Files.Keys.ToArray();

            int index = 0;
            foreach(string file in files)
            {
                if (file.StartsWith("scr_br"))
                {
                    (IRouteConfig[], int) route = GameOpened.GetRoutes(file);

                    // Determine the route name
                    string name = route.Item2 == 0x00
                        ? " "
                        : RouteText.Nouns.TryGetValue(route.Item2, out var noun) && noun.Strings.Count > 0
                            ? noun.Strings[0].Text
                            : $"Route {index}";

                    index++;

                    ChallengeRoutes[file] = route.Item1.ToList();
                    ChallengeRoutesNamesDict[file] = route.Item2;

                    routeListBox.Items.Add(name);
                }
            }
        }

        private void DrawCell(int cellnum, Graphics g, Font font, Brush textBrush, StringFormat format, int col, int row, int xOffset, int yOffset, int rowOffset, int colWidth, int HexSize, List<(int, int)> cellPositions)
        {
            IRouteConfig cell = SelectedChallengeRoutes.FirstOrDefault(route => route.CellNum == cellnum);

            int x = col * colWidth + 10 + xOffset;
            int y = row * 2 * rowOffset + ((col % 2 == 0) ? rowOffset : 0) + 10 + yOffset;

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

            if (cell != null)
            {
                if (cell.CellType == 0x01)
                {
                    // Dessiner l'hexagone en rouge avec le texte "Start"
                    g.DrawPolygon(Pens.White, points);   // Contour blanc
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
                            g.DrawPolygon(Pens.White, points);  // Contour blanc
                        }
                        else
                        {
                            // Sinon, dessiner l'hexagone en vert avec le texte "Team"
                            g.DrawPolygon(Pens.White, points);  // Contour blanc
                            g.DrawString("Team", font, Brushes.Green, x, y, format);  // Texte en blanc
                        }
                    }
                }
                else if (cell.CellType == 0x03)
                {
                    // Dessiner l'hexagone en bleu avec le texte "Chest"
                    g.DrawPolygon(Pens.White, points);  // Contour blanc
                    g.DrawString("Chest", font, Brushes.Blue, x, y, format);  // Texte en blanc
                }
                else if (cell.CellType == 0x04)
                {
                    // Dessiner l'hexagone en jaune avec le texte "Chest"
                    g.DrawPolygon(Pens.White, points);  // Contour blanc
                    g.DrawString("Chest", font, Brushes.Yellow, x, y, format);  // Texte en blanc
                }
                else
                {
                    // Dessiner l'hexagone de base sans background, juste le texte en blanc
                    g.DrawPolygon(Pens.White, points);  // Contour blanc
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

        private void previewPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (routeListBox.SelectedIndex == -1)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int cellCount = 0;
            int rowOffset = (int)(Math.Sqrt(3) * HexSize / 2);
            int colWidth = (int)(1.5 * HexSize);
            int xOffset = 50;
            int yOffset = 50;
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
            foreach (var route in SelectedChallengeRoutes)
            {
                (int, int) cellPosition = cellPositions[route.CellNum];

                List<int> links = new List<int> { route.CellLink1, route.CellLink2, route.CellLink3 };

                foreach (var link in links)
                {
                    if (link != -1)
                    {
                        (int, int) linkCellPositions = cellPositions[link];
                        Pen customPen = new Pen(Color.White, 3);
                        g.DrawLine(customPen, cellPosition.Item1, cellPosition.Item2, linkCellPositions.Item1, linkCellPositions.Item2);
                    }
                }
            }
        }

        private void routeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (routeListBox.SelectedIndex != -1)
            {
                SelectedChallengeRoutes = ChallengeRoutes.ElementAt(routeListBox.SelectedIndex).Value;
                previewPictureBox.Invalidate();
            }
        }
    }
}
