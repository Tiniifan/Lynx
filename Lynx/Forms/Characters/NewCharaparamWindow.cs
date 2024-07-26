using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lynx.Tools;

namespace Lynx.Forms.Characters
{
    public partial class NewCharaparamWindow : Form
    {
        private List<int> UsedHash;

        public int SelectedCharaNameHash;
        
        public NewCharaparamWindow(List<int> usedHash)
        {
            InitializeComponent();
            UsedHash = usedHash;
        }

        private bool SameCharacterHash(string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return UsedHash.IndexOf(crc32) != -1;
        }

        private void NewCharabaseWindow_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                string namePlayer = $"para_cp{i.ToString().PadLeft(4, '0')}";

                if (!SameCharacterHash(namePlayer))
                {
                    playerListBox.Items.Add(namePlayer);
                }
            }

            playerListBox.SelectedIndex = 0;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            SelectedCharaNameHash = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(playerListBox.SelectedItem.ToString())));
            Close();
        }
    }
}
