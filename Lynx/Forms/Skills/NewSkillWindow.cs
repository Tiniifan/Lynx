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

namespace Lynx.Forms.Skills
{
    public partial class NewSkillWindow : Form
    {
        private List<int> UsedIDs;

        public int NewID;

        public int Position;

        public int Type;

        public NewSkillWindow(List<int> usedIDs, int position)
        {
            InitializeComponent();
            UsedIDs = usedIDs;

            switch(position)
            {
                case 1:
                    positionRadioButton1.Checked = true;
                    break;
                case 2:
                    positionRadioButton2.Checked = true;
                    break;
                case 3:
                    positionRadioButton3.Checked = true;
                    break;
                case 4:
                    positionRadioButton4.Checked = true;
                    break;
                case 15:
                    positionRadioButton5.Checked = true;
                    break;
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            string prefix = "w";

            if (typeRadioButton1.Checked)
            {
                prefix += "h";
            }
            else if (typeRadioButton2.Checked)
            {
                prefix += "k";
            }

            if (positionRadioButton1.Checked)
            {
                prefix += "s";
            } 
            else if (positionRadioButton2.Checked)
            {
                prefix += "o";
            }
            else if (positionRadioButton3.Checked)
            {
                prefix += "d";
            }
            else if (positionRadioButton4.Checked)
            {
                prefix += "k";
            }
            else if (positionRadioButton5.Checked)
            {
                prefix += "p";
            }

            // Find random unused id
            bool findNewID = false;
            for (int i = 1; i < 10000; i++)
            {
                string newID = $"{prefix}{i.ToString().PadLeft(4, '0')}";
                int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(newID)));

                if (!UsedIDs.Contains(crc32))
                {
                    NewID = crc32;

                    if (positionRadioButton1.Checked)
                    {
                        Position = 1;
                    }
                    else if (positionRadioButton2.Checked)
                    {
                        Position = 2;
                    }
                    else if (positionRadioButton3.Checked)
                    {
                        Position = 3;
                    }
                    else if (positionRadioButton4.Checked)
                    {
                        Position = 4;
                    }
                    else
                    {
                        Position = 15;
                    }

                    if (typeRadioButton1.Checked)
                    {
                        Type = 1;
                    }
                    else
                    {
                        Type = 2;
                    }

                    DialogResult = DialogResult.OK;
                    findNewID = true;
                    break;
                }
            }

            if (findNewID == false)
            {
                MessageBox.Show("The skill limit has been reached, skill addition will be cancelled");
                DialogResult = DialogResult.Cancel;
            }

            Close();
        }
    }
}
