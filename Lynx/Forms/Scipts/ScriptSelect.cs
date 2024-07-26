using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using Lynx.Tools;
using Lynx.Level5.Text;
using Lynx.Level5.Image;
using Lynx.Level5.Binary;
using Lynx.InazumaEleven.Games;
using Lynx.InazumaEleven.Logic;
using Lynx.InazumaEleven.Common;
using Lynx.InazumaEleven.Games.GO;

namespace Lynx.Forms.Scipts
{
    public partial class ScriptSelect : Form
    {
        private IGame GameOpened;

        private Dictionary<string, List<string>> Scripts;

        private TreeNode RightClickNode;

        public ScriptSelect(IGame game)
        {
            GameOpened = game;
            InitializeComponent();
            InitializeScriptResource();
        }

        private void InitializeScriptResource()
        {
            GetScripts(null);
        }

        private void GetScripts(string searchText)
        {
            scriptTreeView.Nodes.Clear();
            Scripts = new Dictionary<string, List<string>>();

            VirtualDirectory scriptDirectory = GameOpened.Game.Directory.GetFolderFromFullPath(GameOpened.Files["script"].Path);
            foreach(VirtualDirectory subDirectory in scriptDirectory.Folders)
            {
                TreeNode directoryNode = new TreeNode(subDirectory.Name);

                foreach(string fileName in subDirectory.Files.Keys)
                {
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        if (fileName.ToLower().Contains(searchText.ToLower()))
                        {
                            directoryNode.Nodes.Add(fileName);
                        }
                    } else
                    {
                        directoryNode.Nodes.Add(fileName);
                    }
                }

                scriptTreeView.Nodes.Add(directoryNode);
            }
        }

        private void ScriptTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                TreeNode parentNode = e.Node.Parent;

                string scriptText = "";
                string scriptFileNameWithExtension = e.Node.Text;
                string scriptFileNameWithoutExtension = e.Node.Text.Replace(".nutb", "");
                string scriptFullPath = $"/data/script/{parentNode.Text}/{scriptFileNameWithExtension}";

                if (GameOpened.Game.Directory.IsFullPathExists(scriptFullPath))
                {
                    byte[] scriptData = GameOpened.Game.Directory.GetFileFromFullPath(scriptFullPath);
                    File.WriteAllBytes($"./temp/{scriptFileNameWithoutExtension}.nutb", scriptData);

                    ProcessStartInfo processStartInfo = new ProcessStartInfo
                    {
                        FileName = "./NutDecompiler/NutDecompiler.exe",
                        Arguments = $"./temp/{scriptFileNameWithoutExtension}.nutb",
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
                    }

                    scriptText = File.ReadAllText($"./temp/{scriptFileNameWithoutExtension}.nut");

                    using (var scriptEditor = new ScriptEditor($"{scriptFileNameWithoutExtension}.nut", scriptText))
                    {
                        if (scriptEditor.ShowDialog() == DialogResult.OK)
                        {
                            if (scriptEditor.Output != null)
                            {
                                GameOpened.Game.Directory.GetFolderFromFullPath($"/data/script/{parentNode.Text}").Files[scriptFileNameWithExtension].ByteContent = scriptEditor.Output;
                            }
                        }
                    }
                }
            }
        }

        private void ScriptTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RightClickNode = scriptTreeView.GetNodeAt(e.X, e.Y);

                if (RightClickNode != null)
                {
                    if (RightClickNode.Parent == null)
                    {
                        contextMenuStrip1.Items[1].Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip1.Items[1].Enabled = true;
                    }

                    contextMenuStrip1.Show(scriptTreeView, e.Location);
                }
            }
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RightClickNode == null) return;

            string scriptType = "";

            TreeNode parentNode = RightClickNode.Parent;
            if (parentNode != null)
            {
                scriptType = parentNode.Text;
                RightClickNode = RightClickNode.Parent;
            } else
            {
                scriptType = RightClickNode.Text;
            }

            using (var newScriptWindow = new NewScriptWindow())
            {
                if (newScriptWindow.ShowDialog() == DialogResult.OK)
                {
                    string newFilename = $"{newScriptWindow.Filename}.nutb";

                    if (GameOpened.Game.Directory.GetFolderFromFullPath($"/data/script/{scriptType}").Files.ContainsKey(newFilename))
                    {
                        MessageBox.Show("The file already exists");
                    } else
                    {
                        GameOpened.Game.Directory.GetFolderFromFullPath($"/data/script/{scriptType}").AddFile(newFilename, new SubMemoryStream(new byte[] { }));

                        using (var scriptEditor = new ScriptEditor($"{newScriptWindow.Filename}.nut", ""))
                        {
                            // Force to create empty script file
                            File.WriteAllText($"./temp/{newScriptWindow.Filename}.nut", "");
                            scriptEditor.SaveToolStripMenuItem_Click(sender, e);

                            if (scriptEditor.ShowDialog() == DialogResult.OK)
                            {
                                if (scriptEditor.Output != null)
                                {
                                    GameOpened.Game.Directory.GetFolderFromFullPath($"/data/script/{scriptType}").Files[$"{newScriptWindow.Filename}.nutb"].ByteContent = scriptEditor.Output;
                                }

                                RightClickNode.Nodes.Add($"{newScriptWindow.Filename}.nutb");
                            }
                        }
                    }
                }
            }

            RightClickNode = null;
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RightClickNode == null) return;

            TreeNode parentNode = RightClickNode.Parent;
            if (parentNode != null)
            {
                string scriptFileNameWithExtension = RightClickNode.Text;
                GameOpened.Game.Directory.GetFolderFromFullPath($"/data/script/{parentNode.Text}").Files.Remove(scriptFileNameWithExtension);
                parentNode.Nodes.Remove(RightClickNode);
                RightClickNode = null;
            }
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!searchTextBox.Focused || searchTextBox.Enabled == false || searchTextBox.Text == "Search...") return;

            GetScripts(searchTextBox.Text);
        }

        private void SearchTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (searchTextBox.Text != "Search...") return;

            this.Focus();
            searchTextBox.Enabled = false;
            searchTextBox.Text = "";
            searchTextBox.Enabled = true;
            searchTextBox.Focus();
        }

        private void SearchTextBox_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchTextBox.Text)) return;

            searchTextBox.Enabled = false;
            GetScripts(searchTextBox.Text);
            searchTextBox.Text = "Search...";
            searchTextBox.Enabled = true;
        }
    }
}
