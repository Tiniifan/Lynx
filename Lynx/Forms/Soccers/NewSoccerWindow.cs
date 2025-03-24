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

namespace Lynx.Forms.Soccers
{
    public partial class NewSoccerWindow : Form
    {
        private List<int> UsedHash;

        public string SelectedType;

        public int SelectedHash;

        public NewSoccerWindow(List<int> usedHash)
        {
            InitializeComponent();
            UsedHash = usedHash;
        }

        private bool SameCharacterHash(string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return UsedHash.IndexOf(crc32) != -1;
        }

        private void NewSoccerWindow_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                if (SelectedType == "config")
                {
                    string storyConfig = "ts" + i.ToString().PadLeft(4, '0');
                    string encounterConfig = "te" + i.ToString().PadLeft(4, '0');

                    if (!SameCharacterHash(storyConfig))
                    {
                        storyConfigListBox.Items.Add(storyConfig);
                    }

                    if (!SameCharacterHash(encounterConfig))
                    {
                        encounterConfigListBox.Items.Add(encounterConfig);
                    }
                }
                else if (SelectedType == "param")
                {
                    string storyParam = "para_ts" + i.ToString().PadLeft(4, '0');
                    string encounterParam = "para_te" + i.ToString().PadLeft(4, '0');

                    if (!SameCharacterHash(storyParam))
                    {
                        storyParamListBox.Items.Add(storyParam);
                    }

                    if (!SameCharacterHash(encounterParam))
                    {
                        encounterParamListBox.Items.Add(encounterParam);
                    }
                }
                else if (SelectedType == "soccer")
                {
                    string soccer = "btl" + i.ToString().PadLeft(4, '0');

                    if (!SameCharacterHash(soccer))
                    {
                        soccerListBox.Items.Add(soccer);
                    }
                }
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (SelectedType == "config")
            {
                switch (configVsTabControl.SelectedIndex)
                {
                    case 0:
                        SelectedHash = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(storyConfigListBox.SelectedItem.ToString())));
                        break;
                    case 1:
                        SelectedHash = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(encounterConfigListBox.SelectedItem.ToString())));
                        break;
                }
            } else if (SelectedType == "param")
            {
                switch (paramVsTabControl.SelectedIndex)
                {
                    case 0:
                        SelectedHash = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(storyParamListBox.SelectedItem.ToString())));
                        break;
                    case 1:
                        SelectedHash = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(encounterParamListBox.SelectedItem.ToString())));
                        break;
                }
            } else if (SelectedType == "soccer")
            {
                SelectedHash = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(soccerListBox.SelectedItem.ToString())));
            }

            Close();
        }
    }
}
