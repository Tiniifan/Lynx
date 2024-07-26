using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lynx.Forms.Scipts
{
    public partial class ScriptEditor : Form
    {
        public string Filename;

        public byte[] Output;

        public ScriptEditor(string filename, string scriptText)
        {
            InitializeComponent();

            // Design 
            scriptLineNumberRTB.RichTextBox.BackColor = Color.FromArgb(35, 35, 35);
            scriptLineNumberRTB.RichTextBox.ForeColor = Color.FromArgb(255, 255, 255);
            scriptLineNumberRTB.Strip.BackColor = Color.FromArgb(35, 35, 35);
            scriptLineNumberRTB.Strip.BoxedLineColor = Color.FromArgb(35, 35, 35);
            scriptLineNumberRTB.Strip.ForeColor = Color.FromArgb(255, 255, 255);

            Filename = filename;
            scriptLineNumberRTB.Enabled = false;
            Text = $"ScriptEditor - {filename}";
            scriptLineNumberRTB.RichTextBox.Text = scriptText;
            scriptLineNumberRTB.Enabled = true;
            this.Focus();
        }

        public void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText($"./temp/{Filename}", scriptLineNumberRTB.RichTextBox.Text);

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "./SquirrelCompiler/SquirrelCompiler.exe",
                Arguments = $"-o ./temp/out.cnut -c ./temp/{Filename}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();

                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (result == null || string.IsNullOrWhiteSpace(result))
                {
                    MessageBox.Show("Compilation successful!");
                    Output = File.ReadAllBytes($"./temp/out.cnut");
                }
                else
                {
                    string errorMessage = result.Replace($"./temp/{Filename}", "");
                    MessageBox.Show(errorMessage, "Compiler Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ScriptEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
