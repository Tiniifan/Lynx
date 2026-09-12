using System;
using System.Drawing;
using System.Windows.Forms;
using Lynx.InazumaEleven.Common;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;
using Lynx.UI;

namespace Lynx.Forms.Characters
{
    /// <summary>
    /// Asks for the stats a player should show at level 99, then ranks every growth profile by how
    /// close it gets and lets the user walk through them before applying one.
    /// </summary>
    public partial class GenerateStatsWindow : Form
    {
        private const int TargetLevel = 99;

        private readonly IStatCalculator Calculator;

        private readonly int Element;

        private readonly int[] PreferredGrows;

        private readonly FlatNumericUpDown[] TargetNumericUpDowns;

        private StatProfileMatch[] Matches;

        private int Index;

        /// <summary>The profile the user validated, null when the window was cancelled.</summary>
        public StatProfileMatch SelectedMatch { get; private set; }

        public GenerateStatsWindow(IStatCalculator calculator, int element, int[] currentTargets, int[] preferredGrows)
        {
            Calculator = calculator;
            Element = element;
            PreferredGrows = preferredGrows;

            InitializeComponent();

            TargetNumericUpDowns = BuildTargetRows(currentTargets);
            BuildResultGrid();
            UpdateNavigation();
        }

        /// <summary>
        /// One label and one numeric up down per stat, bounded by what the stat can actually reach
        /// at level 99 with this element, whichever profile and curve it takes to get there.
        /// </summary>
        private FlatNumericUpDown[] BuildTargetRows(int[] currentTargets)
        {
            FlatNumericUpDown[] numericUpDowns = new FlatNumericUpDown[Enum.GetValues(typeof(PlayerStats)).Length];

            foreach (PlayerStats stat in Enum.GetValues(typeof(PlayerStats)))
            {
                int index = (int)stat;
                int top = 24 + index * 28;

                Label label = new Label
                {
                    AutoSize = true,
                    ForeColor = Color.White,
                    Location = new Point(12, top + 4),
                    Text = EnumHelper.GetEnumName(stat),
                };

                Calculator.GetReachableRange(stat, TargetLevel, Element, out int lowest, out int highest);

                FlatNumericUpDown numericUpDown = new FlatNumericUpDown
                {
                    BackColor = Color.FromArgb(35, 35, 35),
                    BorderColor = Color.FromArgb(54, 54, 54),
                    BorderStyle = BorderStyle.FixedSingle,
                    ButtonHighlightColor = Color.FromArgb(88, 88, 88),
                    ForeColor = SystemColors.ControlLightLight,
                    Location = new Point(120, top),
                    Size = new Size(118, 22),
                    Minimum = lowest,
                    Maximum = highest,
                    Value = Math.Min(Math.Max(currentTargets[index], lowest), highest),
                };

                targetGroupBox.Controls.Add(label);
                targetGroupBox.Controls.Add(numericUpDown);
                numericUpDowns[index] = numericUpDown;
            }

            return numericUpDowns;
        }

        private void BuildResultGrid()
        {
            resultDataGridView.Columns.Add(NewColumn("Stat", 150));
            resultDataGridView.Columns.Add(NewColumn("Wanted", 75));
            resultDataGridView.Columns.Add(NewColumn("Result", 75));
            resultDataGridView.Columns.Add(NewColumn("Gap", 75));

            foreach (PlayerStats stat in Enum.GetValues(typeof(PlayerStats)))
            {
                resultDataGridView.Rows.Add(EnumHelper.GetEnumName(stat), string.Empty, string.Empty, string.Empty);
            }

            // the grid only reports, a selected row would only be noise
            resultDataGridView.ClearSelection();
            resultDataGridView.SelectionChanged += (sender, e) => resultDataGridView.ClearSelection();
        }

        private static DataGridViewTextBoxColumn NewColumn(string header, int width)
        {
            return new DataGridViewTextBoxColumn
            {
                HeaderText = header,
                Width = width,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Resizable = DataGridViewTriState.False,
            };
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            int[] targets = new int[TargetNumericUpDowns.Length];

            for (int index = 0; index < targets.Length; index++)
            {
                targets[index] = Convert.ToInt32(TargetNumericUpDowns[index].Value);
            }

            Matches = Calculator.RankProfiles(targets, PreferredGrows, TargetLevel, Element);
            Index = 0;

            ShowMatch();
            UpdateNavigation();
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            if (Matches == null || Matches.Length == 0) return;

            Index = (Index - 1 + Matches.Length) % Matches.Length;
            ShowMatch();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (Matches == null || Matches.Length == 0) return;

            Index = (Index + 1) % Matches.Length;
            ShowMatch();
        }

        private void ShowMatch()
        {
            if (Matches == null || Matches.Length == 0) return;

            StatProfileMatch match = Matches[Index];

            profileLabel.Text = string.Format("{0}   ({1} of {2})",
                                              Calculator.GetProfileName(match.Profile),
                                              Index + 1, Matches.Length);

            foreach (PlayerStats stat in Enum.GetValues(typeof(PlayerStats)))
            {
                int index = (int)stat;
                int gap = match.GetDeviation(index);
                DataGridViewRow row = resultDataGridView.Rows[index];

                row.Cells[1].Value = match.Targets[index].ToString();
                row.Cells[2].Value = match.Values[index].ToString();
                row.Cells[3].Value = gap == 0 ? "-" : (gap > 0 ? "+" + gap : gap.ToString());
                row.Cells[3].Style.ForeColor = gap == 0
                    ? Color.White
                    : (gap > 0 ? Color.FromArgb(120, 202, 122) : Color.FromArgb(233, 105, 105));
            }

            deviationLabel.Text = match.TotalDeviation == 0
                ? "Every stat is matched exactly."
                : "Total gap: " + match.TotalDeviation + " points.";
        }

        private void UpdateNavigation()
        {
            bool searched = Matches != null && Matches.Length > 0;

            previousButton.Enabled = searched;
            nextButton.Enabled = searched;
            applyButton.Enabled = searched;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (Matches == null || Matches.Length == 0) return;

            SelectedMatch = Matches[Index];
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
