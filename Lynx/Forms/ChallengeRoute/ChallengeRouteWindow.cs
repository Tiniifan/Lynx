using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Lynx.Tools;
using Lynx.Level5.Text;
using Lynx.Level5.Image;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;
using Lynx.InazumaEleven.Common;
using Lynx.InazumaEleven.Games.GO;

namespace Lynx.Forms.ChallengeRoute
{
    public partial class ChallengeRouteWindow : Form
    {
        private const int HexSize = 30;
        private const int Columns = 10;

        public ChallengeRouteWindow(IGame game)
        {
            InitializeComponent();
        }

        private void ChallengeRouteWindow_Shown(object sender, EventArgs e)
        {
            //DrawHexagon();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int cellCount = 0;
            int rowOffset = (int)(Math.Sqrt(3) * HexSize / 2);
            int colWidth = (int)(1.5 * HexSize);
            int xOffset = 50;
            int yOffset = 50;
            int extraColumn = Math.Max(0, (Columns / 5) - 1);

            Font font = new Font("Arial", 12, FontStyle.Bold);
            Brush textBrush = Brushes.White;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Pattern qui redémarre toutes les 6 colonnes
            int[] pattern = { 2, 3, 2, 3, 2, 2 };

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

                    g.DrawPolygon(Pens.White, points);

                    // Affichage du numéro de la cellule au centre
                    g.DrawString(cellCount.ToString(), font, textBrush, x, y, format);

                    cellCount++;
                }
            }
        }



    }
}
