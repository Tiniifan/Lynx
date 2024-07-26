using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx.Forms.Scipts
{
    public partial class NewScriptWindow : Form 
    {
        public string Filename;

        public NewScriptWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            Filename = nameTextBox.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
