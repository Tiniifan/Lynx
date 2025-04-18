
namespace Lynx.Forms.Home
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.featuresGroupBox = new System.Windows.Forms.GroupBox();
            this.featuresListBox = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.characterGroupBox = new System.Windows.Forms.GroupBox();
            this.charaparamButton = new System.Windows.Forms.Button();
            this.charabaseButton = new System.Windows.Forms.Button();
            this.movesGroupBox = new System.Windows.Forms.GroupBox();
            this.skillsButton = new System.Windows.Forms.Button();
            this.itemsGroupBox = new System.Windows.Forms.GroupBox();
            this.coachesButton = new System.Windows.Forms.Button();
            this.fightingSpiritsButton = new System.Windows.Forms.Button();
            this.eventGroupBox = new System.Windows.Forms.GroupBox();
            this.scriptButton = new System.Windows.Forms.Button();
            this.mapEditorButton = new System.Windows.Forms.Button();
            this.shopsGroupBox = new System.Windows.Forms.GroupBox();
            this.shopsButton = new System.Windows.Forms.Button();
            this.debugGroupBox = new System.Windows.Forms.GroupBox();
            this.saveEditorButton = new System.Windows.Forms.Button();
            this.matchGroupBox = new System.Windows.Forms.GroupBox();
            this.challengeRouteButton = new System.Windows.Forms.Button();
            this.teamsButton = new System.Windows.Forms.Button();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.featuresGroupBox.SuspendLayout();
            this.characterGroupBox.SuspendLayout();
            this.movesGroupBox.SuspendLayout();
            this.itemsGroupBox.SuspendLayout();
            this.eventGroupBox.SuspendLayout();
            this.shopsGroupBox.SuspendLayout();
            this.debugGroupBox.SuspendLayout();
            this.matchGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(668, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.openFolderToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.openToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openFileToolStripMenuItem.Text = "Open (File)";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // featuresGroupBox
            // 
            this.featuresGroupBox.Controls.Add(this.featuresListBox);
            this.featuresGroupBox.Enabled = false;
            this.featuresGroupBox.ForeColor = System.Drawing.Color.White;
            this.featuresGroupBox.Location = new System.Drawing.Point(12, 27);
            this.featuresGroupBox.Name = "featuresGroupBox";
            this.featuresGroupBox.Size = new System.Drawing.Size(200, 414);
            this.featuresGroupBox.TabIndex = 1;
            this.featuresGroupBox.TabStop = false;
            this.featuresGroupBox.Text = "All Features";
            // 
            // featuresListBox
            // 
            this.featuresListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.featuresListBox.ForeColor = System.Drawing.Color.White;
            this.featuresListBox.FormattingEnabled = true;
            this.featuresListBox.Items.AddRange(new object[] {
            "Charabase",
            "Charaparam",
            "Shops",
            "Skills",
            "Fighting Spirits",
            "Scripts",
            "Map Editor",
            "Save Editor",
            "Challenge Route",
            "Teams",
            "Coaches"});
            this.featuresListBox.Location = new System.Drawing.Point(6, 19);
            this.featuresListBox.Name = "featuresListBox";
            this.featuresListBox.Size = new System.Drawing.Size(188, 381);
            this.featuresListBox.TabIndex = 0;
            this.featuresListBox.SelectedIndexChanged += new System.EventHandler(this.FeaturesListBox_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // characterGroupBox
            // 
            this.characterGroupBox.Controls.Add(this.charaparamButton);
            this.characterGroupBox.Controls.Add(this.charabaseButton);
            this.characterGroupBox.Enabled = false;
            this.characterGroupBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.characterGroupBox.Location = new System.Drawing.Point(218, 27);
            this.characterGroupBox.Name = "characterGroupBox";
            this.characterGroupBox.Size = new System.Drawing.Size(216, 84);
            this.characterGroupBox.TabIndex = 3;
            this.characterGroupBox.TabStop = false;
            this.characterGroupBox.Text = "Character";
            // 
            // charaparamButton
            // 
            this.charaparamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.charaparamButton.Location = new System.Drawing.Point(6, 48);
            this.charaparamButton.Name = "charaparamButton";
            this.charaparamButton.Size = new System.Drawing.Size(200, 23);
            this.charaparamButton.TabIndex = 1;
            this.charaparamButton.Text = "Charaparam";
            this.charaparamButton.UseVisualStyleBackColor = true;
            this.charaparamButton.Click += new System.EventHandler(this.CharaparamButton_Click);
            // 
            // charabaseButton
            // 
            this.charabaseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.charabaseButton.Location = new System.Drawing.Point(6, 19);
            this.charabaseButton.Name = "charabaseButton";
            this.charabaseButton.Size = new System.Drawing.Size(200, 23);
            this.charabaseButton.TabIndex = 0;
            this.charabaseButton.Text = "Charabase";
            this.charabaseButton.UseVisualStyleBackColor = true;
            this.charabaseButton.Click += new System.EventHandler(this.CharabaseButton_Click);
            // 
            // movesGroupBox
            // 
            this.movesGroupBox.Controls.Add(this.skillsButton);
            this.movesGroupBox.Enabled = false;
            this.movesGroupBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.movesGroupBox.Location = new System.Drawing.Point(440, 27);
            this.movesGroupBox.Name = "movesGroupBox";
            this.movesGroupBox.Size = new System.Drawing.Size(216, 84);
            this.movesGroupBox.TabIndex = 4;
            this.movesGroupBox.TabStop = false;
            this.movesGroupBox.Text = "Moves";
            // 
            // skillsButton
            // 
            this.skillsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.skillsButton.Location = new System.Drawing.Point(6, 19);
            this.skillsButton.Name = "skillsButton";
            this.skillsButton.Size = new System.Drawing.Size(200, 23);
            this.skillsButton.TabIndex = 0;
            this.skillsButton.Text = "Skills";
            this.skillsButton.UseVisualStyleBackColor = true;
            this.skillsButton.Click += new System.EventHandler(this.SkillsButton_Click);
            // 
            // itemsGroupBox
            // 
            this.itemsGroupBox.Controls.Add(this.coachesButton);
            this.itemsGroupBox.Controls.Add(this.fightingSpiritsButton);
            this.itemsGroupBox.Enabled = false;
            this.itemsGroupBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.itemsGroupBox.Location = new System.Drawing.Point(218, 207);
            this.itemsGroupBox.Name = "itemsGroupBox";
            this.itemsGroupBox.Size = new System.Drawing.Size(216, 84);
            this.itemsGroupBox.TabIndex = 5;
            this.itemsGroupBox.TabStop = false;
            this.itemsGroupBox.Text = "Items";
            // 
            // coachesButton
            // 
            this.coachesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.coachesButton.Location = new System.Drawing.Point(6, 48);
            this.coachesButton.Name = "coachesButton";
            this.coachesButton.Size = new System.Drawing.Size(200, 23);
            this.coachesButton.TabIndex = 1;
            this.coachesButton.Text = "Coaches";
            this.coachesButton.UseVisualStyleBackColor = true;
            this.coachesButton.Click += new System.EventHandler(this.CoachesButton_Click);
            // 
            // fightingSpiritsButton
            // 
            this.fightingSpiritsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fightingSpiritsButton.Location = new System.Drawing.Point(6, 19);
            this.fightingSpiritsButton.Name = "fightingSpiritsButton";
            this.fightingSpiritsButton.Size = new System.Drawing.Size(200, 23);
            this.fightingSpiritsButton.TabIndex = 0;
            this.fightingSpiritsButton.Text = "Fighting Spirits";
            this.fightingSpiritsButton.UseVisualStyleBackColor = true;
            this.fightingSpiritsButton.Click += new System.EventHandler(this.FightingSpiritsButton_Click);
            // 
            // eventGroupBox
            // 
            this.eventGroupBox.Controls.Add(this.scriptButton);
            this.eventGroupBox.Controls.Add(this.mapEditorButton);
            this.eventGroupBox.Enabled = false;
            this.eventGroupBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.eventGroupBox.Location = new System.Drawing.Point(218, 297);
            this.eventGroupBox.Name = "eventGroupBox";
            this.eventGroupBox.Size = new System.Drawing.Size(216, 84);
            this.eventGroupBox.TabIndex = 6;
            this.eventGroupBox.TabStop = false;
            this.eventGroupBox.Text = "Event";
            // 
            // scriptButton
            // 
            this.scriptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scriptButton.Location = new System.Drawing.Point(6, 48);
            this.scriptButton.Name = "scriptButton";
            this.scriptButton.Size = new System.Drawing.Size(200, 23);
            this.scriptButton.TabIndex = 1;
            this.scriptButton.Text = "Script";
            this.scriptButton.UseVisualStyleBackColor = true;
            this.scriptButton.Click += new System.EventHandler(this.ScriptButton_Click);
            // 
            // mapEditorButton
            // 
            this.mapEditorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mapEditorButton.Location = new System.Drawing.Point(6, 19);
            this.mapEditorButton.Name = "mapEditorButton";
            this.mapEditorButton.Size = new System.Drawing.Size(200, 23);
            this.mapEditorButton.TabIndex = 0;
            this.mapEditorButton.Text = "Map Editor";
            this.mapEditorButton.UseVisualStyleBackColor = true;
            this.mapEditorButton.Click += new System.EventHandler(this.MapEditorButton_Click);
            // 
            // shopsGroupBox
            // 
            this.shopsGroupBox.Controls.Add(this.shopsButton);
            this.shopsGroupBox.Enabled = false;
            this.shopsGroupBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.shopsGroupBox.Location = new System.Drawing.Point(440, 117);
            this.shopsGroupBox.Name = "shopsGroupBox";
            this.shopsGroupBox.Size = new System.Drawing.Size(216, 84);
            this.shopsGroupBox.TabIndex = 7;
            this.shopsGroupBox.TabStop = false;
            this.shopsGroupBox.Text = "Shops";
            // 
            // shopsButton
            // 
            this.shopsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shopsButton.Location = new System.Drawing.Point(6, 19);
            this.shopsButton.Name = "shopsButton";
            this.shopsButton.Size = new System.Drawing.Size(200, 23);
            this.shopsButton.TabIndex = 0;
            this.shopsButton.Text = "Shops";
            this.shopsButton.UseVisualStyleBackColor = true;
            this.shopsButton.Click += new System.EventHandler(this.ShopsButton_Click);
            // 
            // debugGroupBox
            // 
            this.debugGroupBox.Controls.Add(this.saveEditorButton);
            this.debugGroupBox.Enabled = false;
            this.debugGroupBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.debugGroupBox.Location = new System.Drawing.Point(218, 387);
            this.debugGroupBox.Name = "debugGroupBox";
            this.debugGroupBox.Size = new System.Drawing.Size(216, 54);
            this.debugGroupBox.TabIndex = 8;
            this.debugGroupBox.TabStop = false;
            this.debugGroupBox.Text = "Debug";
            // 
            // saveEditorButton
            // 
            this.saveEditorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveEditorButton.Location = new System.Drawing.Point(6, 19);
            this.saveEditorButton.Name = "saveEditorButton";
            this.saveEditorButton.Size = new System.Drawing.Size(200, 23);
            this.saveEditorButton.TabIndex = 0;
            this.saveEditorButton.Text = "Save editor";
            this.saveEditorButton.UseVisualStyleBackColor = true;
            this.saveEditorButton.Click += new System.EventHandler(this.SaveEditorButton_Click);
            // 
            // matchGroupBox
            // 
            this.matchGroupBox.Controls.Add(this.challengeRouteButton);
            this.matchGroupBox.Controls.Add(this.teamsButton);
            this.matchGroupBox.Enabled = false;
            this.matchGroupBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.matchGroupBox.Location = new System.Drawing.Point(218, 117);
            this.matchGroupBox.Name = "matchGroupBox";
            this.matchGroupBox.Size = new System.Drawing.Size(216, 84);
            this.matchGroupBox.TabIndex = 9;
            this.matchGroupBox.TabStop = false;
            this.matchGroupBox.Text = "Match";
            // 
            // challengeRouteButton
            // 
            this.challengeRouteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.challengeRouteButton.Location = new System.Drawing.Point(6, 48);
            this.challengeRouteButton.Name = "challengeRouteButton";
            this.challengeRouteButton.Size = new System.Drawing.Size(200, 23);
            this.challengeRouteButton.TabIndex = 1;
            this.challengeRouteButton.Text = "Challenge Route";
            this.challengeRouteButton.UseVisualStyleBackColor = true;
            this.challengeRouteButton.Click += new System.EventHandler(this.ChallengeRouteButton_Click);
            // 
            // teamsButton
            // 
            this.teamsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.teamsButton.Location = new System.Drawing.Point(6, 19);
            this.teamsButton.Name = "teamsButton";
            this.teamsButton.Size = new System.Drawing.Size(200, 23);
            this.teamsButton.TabIndex = 0;
            this.teamsButton.Text = "Teams";
            this.teamsButton.UseVisualStyleBackColor = true;
            this.teamsButton.Click += new System.EventHandler(this.TeamsButton_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openFolderToolStripMenuItem.Text = "Open (Folder)";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // Home
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(668, 451);
            this.Controls.Add(this.matchGroupBox);
            this.Controls.Add(this.debugGroupBox);
            this.Controls.Add(this.shopsGroupBox);
            this.Controls.Add(this.eventGroupBox);
            this.Controls.Add(this.itemsGroupBox);
            this.Controls.Add(this.movesGroupBox);
            this.Controls.Add(this.characterGroupBox);
            this.Controls.Add(this.featuresGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(684, 490);
            this.MinimumSize = new System.Drawing.Size(684, 490);
            this.Name = "Home";
            this.Text = "Home";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Home_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Home_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.featuresGroupBox.ResumeLayout(false);
            this.characterGroupBox.ResumeLayout(false);
            this.movesGroupBox.ResumeLayout(false);
            this.itemsGroupBox.ResumeLayout(false);
            this.eventGroupBox.ResumeLayout(false);
            this.shopsGroupBox.ResumeLayout(false);
            this.debugGroupBox.ResumeLayout(false);
            this.matchGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.GroupBox featuresGroupBox;
        private System.Windows.Forms.ListBox featuresListBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox characterGroupBox;
        private System.Windows.Forms.Button charaparamButton;
        private System.Windows.Forms.Button charabaseButton;
        private System.Windows.Forms.GroupBox movesGroupBox;
        private System.Windows.Forms.Button skillsButton;
        private System.Windows.Forms.GroupBox itemsGroupBox;
        private System.Windows.Forms.Button fightingSpiritsButton;
        private System.Windows.Forms.GroupBox eventGroupBox;
        private System.Windows.Forms.Button scriptButton;
        private System.Windows.Forms.Button mapEditorButton;
        private System.Windows.Forms.GroupBox shopsGroupBox;
        private System.Windows.Forms.Button shopsButton;
        private System.Windows.Forms.GroupBox debugGroupBox;
        private System.Windows.Forms.Button saveEditorButton;
        private System.Windows.Forms.GroupBox matchGroupBox;
        private System.Windows.Forms.Button challengeRouteButton;
        private System.Windows.Forms.Button teamsButton;
        private System.Windows.Forms.Button coachesButton;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
    }
}