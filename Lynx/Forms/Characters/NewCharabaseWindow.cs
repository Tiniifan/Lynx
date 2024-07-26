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
    public partial class NewCharabaseWindow : Form
    {
        private List<int> UsedHash;

        public int SelectedCharaType;

        public int SelectedCharaNameHash;
        
        public NewCharabaseWindow(List<int> usedHash)
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
                string namePlayer = "cp" + i.ToString().PadLeft(4, '0');
                string nameNPC = "cn" + i.ToString().PadLeft(4, '0');
                string nameNPCOther = "ca" + i.ToString().PadLeft(4, '0');

                if (!SameCharacterHash(namePlayer))
                {
                    playerListBox.Items.Add(namePlayer);
                }
                
                if (!SameCharacterHash(nameNPC))
                {
                    npcListBox.Items.Add(nameNPC);
                }
                
                if (!SameCharacterHash(nameNPCOther))
                {
                    npcOtherListBox.Items.Add(nameNPCOther);
                }
            }

            if (playerListBox.Items.Count > 0)
            {
                playerListBox.SelectedIndex = 0;
            }

            if (npcListBox.Items.Count > 0)
            {
                npcListBox.SelectedIndex = 0;
            }

            if (npcOtherListBox.Items.Count > 0)
            {
                npcOtherListBox.SelectedIndex = 0;
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            SelectedCharaType = charaTypeVSTabControl.SelectedIndex;

            switch(SelectedCharaType)
            {
                case 0:
                    SelectedCharaNameHash = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(playerListBox.SelectedItem.ToString())));
                    break;
                case 1:
                    SelectedCharaNameHash = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(npcListBox.SelectedItem.ToString())));
                    break;
                case 2:
                    SelectedCharaNameHash = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(npcOtherListBox.SelectedItem.ToString())));
                    break;
            }

            Close();
        }
    }
}
