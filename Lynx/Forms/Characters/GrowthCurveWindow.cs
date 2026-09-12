using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Lynx.InazumaEleven.Common;
using Lynx.InazumaEleven.Logic;

namespace Lynx.Forms.Characters
{
    /// <summary>
    /// Plots a stat from level 1 to 99 for every growth curve available to it, with the curve
    /// currently selected drawn on top.
    /// </summary>
    public partial class GrowthCurveWindow : Form
    {
        private const int FirstLevel = 1;

        private const int LastLevel = 99;

        private static readonly Color[] CurveColors =
        {
            Color.FromArgb(233, 105, 105),
            Color.FromArgb(233, 173, 91),
            Color.FromArgb(214, 214, 106),
            Color.FromArgb(120, 202, 122),
            Color.FromArgb(104, 178, 233),
            Color.FromArgb(183, 132, 226),
        };

        private readonly IStatCalculator Calculator;

        private readonly PlayerStats Stat;

        private readonly string StatName;

        private readonly int BaseStat;

        private readonly int Grow;

        private readonly int Element;

        private readonly int Profile;

        private readonly int[] Curves;

        public GrowthCurveWindow(IStatCalculator calculator, PlayerStats stat, string statName,
                                 int baseStat, int grow, int element, int profile)
        {
            Calculator = calculator;
            Stat = stat;
            StatName = statName;
            BaseStat = baseStat;
            Grow = grow;
            Element = element;
            Profile = profile;
            Curves = calculator.GetCurves(stat);

            InitializeComponent();

            Text = statName + " growth curves";
            headerLabel.Text = string.Format("{0} from level {1} to {2}   -   base stat {3}   -   {4}   -   current curve: {5}",
                                             statName, FirstLevel, LastLevel, baseStat,
                                             calculator.GetProfileName(profile),
                                             calculator.GetCurveName(stat, grow));
        }

        private void ChartPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle plot = new Rectangle(46, 14, chartPanel.Width - 62, chartPanel.Height - 92);
            int[][] series = BuildSeries();
            int highest = Highest(series);

            DrawGrid(graphics, plot, highest);

            for (int index = 0; index < Curves.Length; index++)
            {
                if (Curves[index] == Grow) continue;

                DrawCurve(graphics, plot, series[index], highest, ColorOf(index), 1.0f, 150);
            }

            int selected = Array.IndexOf(Curves, Grow);

            if (selected >= 0)
            {
                DrawCurve(graphics, plot, series[selected], highest, ColorOf(selected), 2.5f, 255);
            }

            DrawLegend(graphics, plot);
        }

        /// <summary>The stat at every level, one array per curve.</summary>
        private int[][] BuildSeries()
        {
            int[][] series = new int[Curves.Length][];

            for (int index = 0; index < Curves.Length; index++)
            {
                series[index] = new int[LastLevel - FirstLevel + 1];

                for (int level = FirstLevel; level <= LastLevel; level++)
                {
                    series[index][level - FirstLevel] =
                        Calculator.GetStat(Stat, BaseStat, Curves[index], level, Element, Profile);
                }
            }

            return series;
        }

        private static int Highest(int[][] series)
        {
            int highest = 1;

            foreach (int[] values in series)
            {
                foreach (int value in values)
                {
                    if (value > highest) highest = value;
                }
            }

            // round up to a readable tick
            int step = Step(highest);
            return ((highest + step - 1) / step) * step;
        }

        private static int Step(int highest)
        {
            if (highest <= 50) return 10;
            if (highest <= 120) return 20;
            if (highest <= 300) return 50;
            return 100;
        }

        private void DrawGrid(Graphics graphics, Rectangle plot, int highest)
        {
            using (Pen axis = new Pen(Color.FromArgb(120, 120, 120)))
            using (Pen grid = new Pen(Color.FromArgb(62, 62, 62)))
            using (Brush text = new SolidBrush(Color.FromArgb(190, 190, 190)))
            using (Font font = new Font(Font.FontFamily, 7.5f))
            {
                StringFormat right = new StringFormat { Alignment = StringAlignment.Far };
                StringFormat centre = new StringFormat { Alignment = StringAlignment.Center };

                int step = Step(highest);

                for (int value = 0; value <= highest; value += step)
                {
                    int y = ValueToY(plot, value, highest);
                    graphics.DrawLine(grid, plot.Left, y, plot.Right, y);
                    graphics.DrawString(value.ToString(), font, text, plot.Left - 6, y - 7, right);
                }

                for (int level = 0; level <= LastLevel; level += 10)
                {
                    if (level < FirstLevel) continue;

                    int x = LevelToX(plot, level);
                    graphics.DrawLine(grid, x, plot.Top, x, plot.Bottom);
                    graphics.DrawString(level.ToString(), font, text, x, plot.Bottom + 4, centre);
                }

                graphics.DrawLine(axis, plot.Left, plot.Top, plot.Left, plot.Bottom);
                graphics.DrawLine(axis, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
            }
        }

        private void DrawCurve(Graphics graphics, Rectangle plot, int[] values, int highest,
                               Color color, float width, int alpha)
        {
            PointF[] points = new PointF[values.Length];

            for (int index = 0; index < values.Length; index++)
            {
                points[index] = new PointF(LevelToX(plot, FirstLevel + index),
                                           ValueToY(plot, values[index], highest));
            }

            using (Pen pen = new Pen(Color.FromArgb(alpha, color), width))
            {
                graphics.DrawLines(pen, points);
            }
        }

        private void DrawLegend(Graphics graphics, Rectangle plot)
        {
            using (Font font = new Font(Font.FontFamily, 8f))
            {
                int x = plot.Left;
                int y = plot.Bottom + 26;

                for (int index = 0; index < Curves.Length; index++)
                {
                    bool selected = Curves[index] == Grow;
                    Color color = ColorOf(index);
                    string name = Calculator.GetCurveName(Stat, Curves[index]);
                    int last = Calculator.GetStat(Stat, BaseStat, Curves[index], LastLevel, Element, Profile);
                    string caption = name + " (" + last + ")";
                    int width = 22 + (int)graphics.MeasureString(caption, font).Width + 16;

                    // wrap rather than run off the panel when a stat offers many curves
                    if (x > plot.Left && x + width > plot.Right)
                    {
                        x = plot.Left;
                        y += 17;
                    }

                    using (Brush swatch = new SolidBrush(selected ? color : Color.FromArgb(150, color)))
                    {
                        graphics.FillRectangle(swatch, x, y + 3, 18, selected ? 5 : 3);
                    }

                    using (Brush text = new SolidBrush(selected ? Color.White : Color.FromArgb(170, 170, 170)))
                    using (Font caption_font = new Font(font, selected ? FontStyle.Bold : FontStyle.Regular))
                    {
                        graphics.DrawString(caption, caption_font, text, x + 22, y - 3);
                    }

                    x += width;
                }
            }
        }

        private Color ColorOf(int index)
        {
            return CurveColors[index % CurveColors.Length];
        }

        private static int LevelToX(Rectangle plot, int level)
        {
            return plot.Left + (int)Math.Round((double)(level - FirstLevel) * plot.Width / (LastLevel - FirstLevel));
        }

        private static int ValueToY(Rectangle plot, int value, int highest)
        {
            return plot.Bottom - (int)Math.Round((double)value * plot.Height / highest);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
