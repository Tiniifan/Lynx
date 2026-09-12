using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudioElevenLib.Tools;

namespace Lynx.Forms.Maps
{
    public partial class NewNPCWindow : Form
    {
        private List<int> UsedHash;

        public int SelectedNPCHash;

        public NewNPCWindow(List<int> usedHash)
        {
            InitializeComponent();
            UsedHash = usedHash;
        }

        private bool SameCharacterHash(string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return UsedHash.IndexOf(crc32) != -1;
        }

        private void NewNPCWindow_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                string namePlayer = $"evc{i.ToString().PadLeft(4, '0')}";

                if (!SameCharacterHash(namePlayer))
                {
                    npcListBox.Items.Add(namePlayer);
                }
            }

            npcListBox.SelectedIndex = 0;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            SelectedNPCHash = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(npcListBox.SelectedItem.ToString())));
            Close();
        }
    }
}
