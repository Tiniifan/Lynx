using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Lynx.InazumaEleven.Logic;

namespace Lynx.Forms.PalpackCards
{
    /// <summary>
    /// A player that can receive a palpack card: its charaparam id is a para_cpXXXX one and no card
    /// recruits it yet.
    /// </summary>
    public class PalpackCardCandidate
    {
        public Player Player { get; set; }

        /// <summary>The XXXX of para_cpXXXX, reused to build ikaXXXX, name_ikaXXXX and desc_ikaXXXX.</summary>
        public int Number { get; set; }

        public string Name { get; set; }
    }

    /// <summary>
    /// Lists the players without a palpack card and lets the user pick the one to create a card for.
    /// </summary>
    public partial class NewPalpackCardWindow : Form
    {
        private const int FaceSize = 40;

        private readonly List<PalpackCardCandidate> Candidates;

        private readonly Func<Player, Image> FaceLoader;

        // Faces are decoded from the archive only once a row is displayed, there can be a thousand players
        private readonly Dictionary<int, Image> Faces = new Dictionary<int, Image>();

        /// <summary>The player the user validated, null when the window was cancelled.</summary>
        public PalpackCardCandidate SelectedCandidate { get; private set; }

        public NewPalpackCardWindow(List<PalpackCardCandidate> candidates, Func<Player, Image> faceLoader)
        {
            Candidates = candidates;
            FaceLoader = faceLoader;

            InitializeComponent();
            BuildPlayerGrid();
        }

        private void BuildPlayerGrid()
        {
            playerDataGridView.Columns.Add(new DataGridViewImageColumn
            {
                HeaderText = "Face",
                Width = FaceSize + 8,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Resizable = DataGridViewTriState.False,
                DefaultCellStyle = { NullValue = null },
            });
            playerDataGridView.Columns.Add(NewColumn("Name", 190));
            playerDataGridView.Columns.Add(NewColumn("Para ID", 110));

            playerDataGridView.RowTemplate.Height = FaceSize;

            foreach (PalpackCardCandidate candidate in Candidates)
            {
                playerDataGridView.Rows.Add(null, candidate.Name, "para_cp" + candidate.Number.ToString().PadLeft(4, '0'));
            }

            playerDataGridView.ClearSelection();
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

        private PalpackCardCandidate GetSelectedCandidate()
        {
            if (playerDataGridView.SelectedRows.Count == 0) return null;

            return Candidates[playerDataGridView.SelectedRows[0].Index];
        }

        private void Confirm()
        {
            SelectedCandidate = GetSelectedCandidate();
            if (SelectedCandidate == null) return;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void PlayerDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex != 0 || e.RowIndex < 0 || e.RowIndex >= Candidates.Count) return;

            if (!Faces.TryGetValue(e.RowIndex, out Image face))
            {
                face = FaceLoader(Candidates[e.RowIndex].Player);
                Faces[e.RowIndex] = face;
            }

            e.Value = face;
            e.FormattingApplied = true;
        }

        private void PlayerDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            okButton.Enabled = GetSelectedCandidate() != null;
        }

        private void PlayerDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Confirm();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            SelectedCandidate = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
