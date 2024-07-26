using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx.Forms.Home
{
    public partial class LanguageWindow : Form
    {
        public string SelectedLanguage { get; private set; }

        public LanguageWindow()
        {
            InitializeComponent();
        }

        private void LanguagesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedLanguage = languagesListBox.SelectedItem.ToString();
            Close();
        }
    }
}
