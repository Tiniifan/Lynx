using System;
using System.Drawing;
using System.Windows.Forms;
using Lynx.InazumaEleven.Common;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;

namespace Lynx.Forms.Characters
{
    /// <summary>
    /// Lists every growth profile and the multiplier it applies to each stat. Modeless, but only
    /// one instance can exist at a time so the user cannot pile copies of it up.
    /// </summary>
    public partial class GrowthProfileWindow : Form
    {
        private static GrowthProfileWindow Opened;

        private readonly IStatCalculator Calculator;

        private GrowthProfileWindow(IStatCalculator calculator)
        {
            Calculator = calculator;

            InitializeComponent();
            BuildGrid();
        }

        /// <summary>
        /// Shows the window, or brings the one already open back to the front.
        /// </summary>
        public static void ShowSingle(IWin32Window owner, IStatCalculator calculator, int currentProfile)
        {
            if (Opened == null || Opened.IsDisposed)
            {
                Opened = new GrowthProfileWindow(calculator);
                Opened.FormClosed += (sender, e) => Opened = null;
                Opened.Show(owner);
            }
            else
            {
                if (Opened.WindowState == FormWindowState.Minimized)
                {
                    Opened.WindowState = FormWindowState.Normal;
                }

                Opened.BringToFront();
                Opened.Activate();
            }

            Opened.Highlight(currentProfile);
        }

        private void BuildGrid()
        {
            profileDataGridView.Columns.Add(NewColumn("Profile", 130));

            foreach (PlayerStats stat in Enum.GetValues(typeof(PlayerStats)))
            {
                profileDataGridView.Columns.Add(NewColumn(EnumHelper.GetEnumName(stat), 62));
            }

            for (int profile = 1; profile <= Calculator.ProfileCount; profile++)
            {
                object[] cells = new object[profileDataGridView.Columns.Count];
                cells[0] = Calculator.GetProfileName(profile);

                foreach (PlayerStats stat in Enum.GetValues(typeof(PlayerStats)))
                {
                    cells[(int)stat + 1] = Calculator.GetProfileMultiplier(stat, profile).ToString("0.0");
                }

                profileDataGridView.Rows.Add(cells);
                profileDataGridView.Rows[profile - 1].Tag = profile;
            }

            // the grid is a reference table, a selected row would only be noise
            profileDataGridView.ClearSelection();
            profileDataGridView.SelectionChanged += (sender, e) => profileDataGridView.ClearSelection();

            // a multiplier that is not 1.0 is the whole point of the table, so make it stand out
            foreach (DataGridViewRow row in profileDataGridView.Rows)
            {
                for (int column = 1; column < profileDataGridView.Columns.Count; column++)
                {
                    float multiplier = float.Parse((string)row.Cells[column].Value,
                                                   System.Globalization.CultureInfo.CurrentCulture);

                    if (multiplier > 1.0f) row.Cells[column].Style.ForeColor = Color.FromArgb(120, 202, 122);
                    else if (multiplier < 1.0f) row.Cells[column].Style.ForeColor = Color.FromArgb(233, 105, 105);
                }
            }
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

        private void Highlight(int currentProfile)
        {
            headerLabel.Text = "Multiplier applied to the growth of each stat. Current profile: "
                               + Calculator.GetProfileName(currentProfile);

            foreach (DataGridViewRow row in profileDataGridView.Rows)
            {
                bool current = row.Tag is int profile && profile == currentProfile;

                row.DefaultCellStyle.BackColor = current
                    ? Color.FromArgb(60, 60, 60)
                    : Color.FromArgb(35, 35, 35);
                row.DefaultCellStyle.Font = new Font(profileDataGridView.Font,
                                                     current ? FontStyle.Bold : FontStyle.Regular);
            }
        }
    }
}
