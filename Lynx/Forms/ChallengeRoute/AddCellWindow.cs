using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudioElevenLib.Tools;

namespace Lynx.Forms.ChallengeRoute
{
    public partial class AddCellWindow : Form
    {
        private List<int> UsedCell;

        public int SelectedCell = -1;

        public AddCellWindow(List<int> usedCell)
        {
            InitializeComponent();
            UsedCell = usedCell;
        }

        public static int GetCellNumber(string input)
        {
            Match match = Regex.Match(input, @"\bCell\s+(\d+)\b", RegexOptions.IgnoreCase);
            return match.Success ? int.Parse(match.Groups[1].Value) : -1;
        }

        private void AddCellWindow_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < 24; i++)
            {
                if (!UsedCell.Contains(i))
                {
                    cellListBox.Items.Add($"Cell {i}");
                }
            }

            if (cellListBox.Items.Count > 0)
            {
                cellListBox.SelectedIndex = 0;
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            SelectedCell = GetCellNumber(cellListBox.SelectedItem.ToString());
            Close();
        }
    }
}
