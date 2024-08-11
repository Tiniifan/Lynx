namespace Lynx.Forms.Maps
{
    partial class MapEditor
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
            this.selectedNpcMapPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewNPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.focusNPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vsTabControl1 = new Lynx.UI.VSTabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.npcTreeView = new System.Windows.Forms.TreeView();
            this.mapVSTabControl = new Lynx.UI.VSTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.appearanceGroupBox = new System.Windows.Forms.GroupBox();
            this.glovesFlatComboBox = new Lynx.UI.FlatComboBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.iconFlatComboBox = new Lynx.UI.FlatComboBox();
            this.headFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.bootsFlatComboBox = new Lynx.UI.FlatComboBox();
            this.uniformFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.appearGroupBox = new System.Windows.Forms.GroupBox();
            this.rotationNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.locationZNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.locationYNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.locationXFlatNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.positionCondTextBox = new System.Windows.Forms.TextBox();
            this.animationGroupBox1 = new System.Windows.Forms.GroupBox();
            this.unkAnimationTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.talkAnimationTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.restAnimationTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.configurationGroupBox = new System.Windows.Forms.GroupBox();
            this.scriptButton = new System.Windows.Forms.Button();
            this.valueFlatNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.eventTypeFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.eventCondTextBox = new System.Windows.Forms.TextBox();
            this.eventListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.selectedNpcMapPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.vsTabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.mapVSTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.appearanceGroupBox.SuspendLayout();
            this.appearGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rotationNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationZNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationYNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationXFlatNumericUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.animationGroupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.configurationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valueFlatNumericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectedNpcMapPictureBox
            // 
            this.selectedNpcMapPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectedNpcMapPictureBox.Location = new System.Drawing.Point(696, 32);
            this.selectedNpcMapPictureBox.Name = "selectedNpcMapPictureBox";
            this.selectedNpcMapPictureBox.Size = new System.Drawing.Size(385, 385);
            this.selectedNpcMapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.selectedNpcMapPictureBox.TabIndex = 7;
            this.selectedNpcMapPictureBox.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1090, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewNPCToolStripMenuItem,
            this.focusNPCToolStripMenuItem});
            this.viewToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // viewNPCToolStripMenuItem
            // 
            this.viewNPCToolStripMenuItem.Name = "viewNPCToolStripMenuItem";
            this.viewNPCToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.viewNPCToolStripMenuItem.Text = "View NPC";
            // 
            // focusNPCToolStripMenuItem
            // 
            this.focusNPCToolStripMenuItem.Name = "focusNPCToolStripMenuItem";
            this.focusNPCToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.focusNPCToolStripMenuItem.Text = "Focus NPC";
            // 
            // vsTabControl1
            // 
            this.vsTabControl1.ActiveIndicator = System.Drawing.Color.White;
            this.vsTabControl1.ActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.vsTabControl1.ActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.vsTabControl1.Background = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.vsTabControl1.BackgroundTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.vsTabControl1.Border = System.Drawing.Color.White;
            this.vsTabControl1.Controls.Add(this.tabPage4);
            this.vsTabControl1.Divider = System.Drawing.Color.White;
            this.vsTabControl1.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
            this.vsTabControl1.InActiveIndicator = System.Drawing.Color.White;
            this.vsTabControl1.InActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.vsTabControl1.InActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.vsTabControl1.Location = new System.Drawing.Point(9, 32);
            this.vsTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.vsTabControl1.Name = "vsTabControl1";
            this.vsTabControl1.Padding = new System.Drawing.Point(0, 0);
            this.vsTabControl1.SelectedIndex = 0;
            this.vsTabControl1.Size = new System.Drawing.Size(684, 385);
            this.vsTabControl1.TabIndex = 51;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tabPage4.Controls.Add(this.npcTreeView);
            this.tabPage4.Controls.Add(this.mapVSTabControl);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(676, 356);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "NPC";
            // 
            // npcTreeView
            // 
            this.npcTreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.npcTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.npcTreeView.ForeColor = System.Drawing.Color.White;
            this.npcTreeView.LineColor = System.Drawing.Color.White;
            this.npcTreeView.Location = new System.Drawing.Point(6, 6);
            this.npcTreeView.Name = "npcTreeView";
            this.npcTreeView.Size = new System.Drawing.Size(245, 343);
            this.npcTreeView.TabIndex = 51;
            this.npcTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.NpcTreeView_AfterSelect);
            // 
            // mapVSTabControl
            // 
            this.mapVSTabControl.ActiveIndicator = System.Drawing.Color.White;
            this.mapVSTabControl.ActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.mapVSTabControl.ActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mapVSTabControl.Background = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.mapVSTabControl.BackgroundTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.mapVSTabControl.Border = System.Drawing.Color.White;
            this.mapVSTabControl.Controls.Add(this.tabPage1);
            this.mapVSTabControl.Controls.Add(this.tabPage2);
            this.mapVSTabControl.Divider = System.Drawing.Color.White;
            this.mapVSTabControl.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
            this.mapVSTabControl.InActiveIndicator = System.Drawing.Color.White;
            this.mapVSTabControl.InActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.mapVSTabControl.InActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mapVSTabControl.Location = new System.Drawing.Point(254, 6);
            this.mapVSTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.mapVSTabControl.Name = "mapVSTabControl";
            this.mapVSTabControl.Padding = new System.Drawing.Point(0, 0);
            this.mapVSTabControl.SelectedIndex = 0;
            this.mapVSTabControl.Size = new System.Drawing.Size(411, 343);
            this.mapVSTabControl.TabIndex = 50;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tabPage1.Controls.Add(this.appearanceGroupBox);
            this.tabPage1.Controls.Add(this.appearGroupBox);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.animationGroupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(403, 314);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Information";
            // 
            // appearanceGroupBox
            // 
            this.appearanceGroupBox.Controls.Add(this.glovesFlatComboBox);
            this.appearanceGroupBox.Controls.Add(this.idTextBox);
            this.appearanceGroupBox.Controls.Add(this.label19);
            this.appearanceGroupBox.Controls.Add(this.label1);
            this.appearanceGroupBox.Controls.Add(this.label4);
            this.appearanceGroupBox.Controls.Add(this.iconFlatComboBox);
            this.appearanceGroupBox.Controls.Add(this.headFlatComboBox);
            this.appearanceGroupBox.Controls.Add(this.label18);
            this.appearanceGroupBox.Controls.Add(this.bootsFlatComboBox);
            this.appearanceGroupBox.Controls.Add(this.uniformFlatComboBox);
            this.appearanceGroupBox.Controls.Add(this.label5);
            this.appearanceGroupBox.Controls.Add(this.label3);
            this.appearanceGroupBox.ForeColor = System.Drawing.Color.White;
            this.appearanceGroupBox.Location = new System.Drawing.Point(6, 6);
            this.appearanceGroupBox.Name = "appearanceGroupBox";
            this.appearanceGroupBox.Size = new System.Drawing.Size(392, 92);
            this.appearanceGroupBox.TabIndex = 265;
            this.appearanceGroupBox.TabStop = false;
            this.appearanceGroupBox.Text = "Appearance";
            // 
            // glovesFlatComboBox
            // 
            this.glovesFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.glovesFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.glovesFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.glovesFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.glovesFlatComboBox.FormattingEnabled = true;
            this.glovesFlatComboBox.Location = new System.Drawing.Point(257, 65);
            this.glovesFlatComboBox.Name = "glovesFlatComboBox";
            this.glovesFlatComboBox.Size = new System.Drawing.Size(129, 21);
            this.glovesFlatComboBox.TabIndex = 262;
            // 
            // idTextBox
            // 
            this.idTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.idTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.idTextBox.ForeColor = System.Drawing.Color.White;
            this.idTextBox.Location = new System.Drawing.Point(58, 19);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(130, 15);
            this.idTextBox.TabIndex = 49;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(204, 68);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 13);
            this.label19.TabIndex = 263;
            this.label19.Text = "Gloves";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Model";
            // 
            // iconFlatComboBox
            // 
            this.iconFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.iconFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.iconFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.iconFlatComboBox.ForeColor = System.Drawing.Color.White;
            this.iconFlatComboBox.FormattingEnabled = true;
            this.iconFlatComboBox.Items.AddRange(new object[] {
            "Point",
            "Palpack Shop",
            "Consumable Shop",
            "Equipment Shop",
            "Special Move Shop",
            "Competition Route",
            "Fighting Spirit",
            "Play coin",
            "None"});
            this.iconFlatComboBox.Location = new System.Drawing.Point(256, 15);
            this.iconFlatComboBox.Name = "iconFlatComboBox";
            this.iconFlatComboBox.Size = new System.Drawing.Size(130, 21);
            this.iconFlatComboBox.TabIndex = 55;
            // 
            // headFlatComboBox
            // 
            this.headFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.headFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.headFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.headFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.headFlatComboBox.FormattingEnabled = true;
            this.headFlatComboBox.Location = new System.Drawing.Point(58, 39);
            this.headFlatComboBox.Name = "headFlatComboBox";
            this.headFlatComboBox.Size = new System.Drawing.Size(130, 21);
            this.headFlatComboBox.Sorted = true;
            this.headFlatComboBox.TabIndex = 37;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(204, 43);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 13);
            this.label18.TabIndex = 260;
            this.label18.Text = "Uniform";
            // 
            // bootsFlatComboBox
            // 
            this.bootsFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.bootsFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.bootsFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.bootsFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.bootsFlatComboBox.FormattingEnabled = true;
            this.bootsFlatComboBox.Location = new System.Drawing.Point(58, 65);
            this.bootsFlatComboBox.Name = "bootsFlatComboBox";
            this.bootsFlatComboBox.Size = new System.Drawing.Size(130, 21);
            this.bootsFlatComboBox.TabIndex = 49;
            // 
            // uniformFlatComboBox
            // 
            this.uniformFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.uniformFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uniformFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.uniformFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.uniformFlatComboBox.FormattingEnabled = true;
            this.uniformFlatComboBox.Location = new System.Drawing.Point(256, 39);
            this.uniformFlatComboBox.Name = "uniformFlatComboBox";
            this.uniformFlatComboBox.Size = new System.Drawing.Size(130, 21);
            this.uniformFlatComboBox.TabIndex = 261;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 50;
            this.label5.Text = "Boots";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(204, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Icon";
            // 
            // appearGroupBox
            // 
            this.appearGroupBox.Controls.Add(this.rotationNumericUpDown);
            this.appearGroupBox.Controls.Add(this.label9);
            this.appearGroupBox.Controls.Add(this.locationZNumericUpDown);
            this.appearGroupBox.Controls.Add(this.label11);
            this.appearGroupBox.Controls.Add(this.locationYNumericUpDown);
            this.appearGroupBox.Controls.Add(this.label10);
            this.appearGroupBox.Controls.Add(this.locationXFlatNumericUpDown);
            this.appearGroupBox.Controls.Add(this.label6);
            this.appearGroupBox.ForeColor = System.Drawing.Color.White;
            this.appearGroupBox.Location = new System.Drawing.Point(6, 104);
            this.appearGroupBox.Name = "appearGroupBox";
            this.appearGroupBox.Size = new System.Drawing.Size(217, 86);
            this.appearGroupBox.TabIndex = 266;
            this.appearGroupBox.TabStop = false;
            this.appearGroupBox.Text = "Position";
            // 
            // rotationNumericUpDown
            // 
            this.rotationNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.rotationNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.rotationNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rotationNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.rotationNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rotationNumericUpDown.Location = new System.Drawing.Point(145, 51);
            this.rotationNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.rotationNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.rotationNumericUpDown.Name = "rotationNumericUpDown";
            this.rotationNumericUpDown.Size = new System.Drawing.Size(66, 22);
            this.rotationNumericUpDown.TabIndex = 270;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(111, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 269;
            this.label9.Text = "Rot.";
            // 
            // locationZNumericUpDown
            // 
            this.locationZNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.locationZNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.locationZNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.locationZNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.locationZNumericUpDown.DecimalPlaces = 2;
            this.locationZNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.locationZNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.locationZNumericUpDown.Location = new System.Drawing.Point(39, 51);
            this.locationZNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.locationZNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.locationZNumericUpDown.Name = "locationZNumericUpDown";
            this.locationZNumericUpDown.Size = new System.Drawing.Size(66, 22);
            this.locationZNumericUpDown.TabIndex = 268;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 267;
            this.label11.Text = "Z";
            // 
            // locationYNumericUpDown
            // 
            this.locationYNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.locationYNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.locationYNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.locationYNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.locationYNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.locationYNumericUpDown.Location = new System.Drawing.Point(145, 13);
            this.locationYNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.locationYNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.locationYNumericUpDown.Name = "locationYNumericUpDown";
            this.locationYNumericUpDown.Size = new System.Drawing.Size(66, 22);
            this.locationYNumericUpDown.TabIndex = 266;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(111, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 13);
            this.label10.TabIndex = 265;
            this.label10.Text = "Y";
            // 
            // locationXFlatNumericUpDown
            // 
            this.locationXFlatNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.locationXFlatNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.locationXFlatNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.locationXFlatNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.locationXFlatNumericUpDown.DecimalPlaces = 2;
            this.locationXFlatNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.locationXFlatNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.locationXFlatNumericUpDown.Location = new System.Drawing.Point(39, 13);
            this.locationXFlatNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.locationXFlatNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.locationXFlatNumericUpDown.Name = "locationXFlatNumericUpDown";
            this.locationXFlatNumericUpDown.Size = new System.Drawing.Size(66, 22);
            this.locationXFlatNumericUpDown.TabIndex = 264;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "X";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.positionCondTextBox);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(6, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 112);
            this.groupBox1.TabIndex = 268;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Condition";
            // 
            // positionCondTextBox
            // 
            this.positionCondTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.positionCondTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.positionCondTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.positionCondTextBox.Location = new System.Drawing.Point(9, 17);
            this.positionCondTextBox.Multiline = true;
            this.positionCondTextBox.Name = "positionCondTextBox";
            this.positionCondTextBox.Size = new System.Drawing.Size(377, 86);
            this.positionCondTextBox.TabIndex = 271;
            // 
            // animationGroupBox1
            // 
            this.animationGroupBox1.Controls.Add(this.unkAnimationTextBox);
            this.animationGroupBox1.Controls.Add(this.label12);
            this.animationGroupBox1.Controls.Add(this.talkAnimationTextBox);
            this.animationGroupBox1.Controls.Add(this.label8);
            this.animationGroupBox1.Controls.Add(this.restAnimationTextBox);
            this.animationGroupBox1.Controls.Add(this.label7);
            this.animationGroupBox1.ForeColor = System.Drawing.Color.White;
            this.animationGroupBox1.Location = new System.Drawing.Point(229, 104);
            this.animationGroupBox1.Name = "animationGroupBox1";
            this.animationGroupBox1.Size = new System.Drawing.Size(169, 86);
            this.animationGroupBox1.TabIndex = 267;
            this.animationGroupBox1.TabStop = false;
            this.animationGroupBox1.Text = "Animation";
            // 
            // unkAnimationTextBox
            // 
            this.unkAnimationTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.unkAnimationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.unkAnimationTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.unkAnimationTextBox.Location = new System.Drawing.Point(43, 60);
            this.unkAnimationTextBox.Name = "unkAnimationTextBox";
            this.unkAnimationTextBox.ReadOnly = true;
            this.unkAnimationTextBox.Size = new System.Drawing.Size(120, 15);
            this.unkAnimationTextBox.TabIndex = 274;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 13);
            this.label12.TabIndex = 273;
            this.label12.Text = "Unk";
            // 
            // talkAnimationTextBox
            // 
            this.talkAnimationTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.talkAnimationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.talkAnimationTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.talkAnimationTextBox.Location = new System.Drawing.Point(43, 39);
            this.talkAnimationTextBox.Name = "talkAnimationTextBox";
            this.talkAnimationTextBox.ReadOnly = true;
            this.talkAnimationTextBox.Size = new System.Drawing.Size(120, 15);
            this.talkAnimationTextBox.TabIndex = 272;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Rest";
            // 
            // restAnimationTextBox
            // 
            this.restAnimationTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.restAnimationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.restAnimationTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.restAnimationTextBox.Location = new System.Drawing.Point(43, 18);
            this.restAnimationTextBox.Name = "restAnimationTextBox";
            this.restAnimationTextBox.ReadOnly = true;
            this.restAnimationTextBox.Size = new System.Drawing.Size(120, 15);
            this.restAnimationTextBox.TabIndex = 271;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 263;
            this.label7.Text = "Talk";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tabPage2.Controls.Add(this.configurationGroupBox);
            this.tabPage2.Controls.Add(this.eventListBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(403, 314);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Event";
            // 
            // configurationGroupBox
            // 
            this.configurationGroupBox.Controls.Add(this.scriptButton);
            this.configurationGroupBox.Controls.Add(this.valueFlatNumericUpDown);
            this.configurationGroupBox.Controls.Add(this.eventTypeFlatComboBox);
            this.configurationGroupBox.Controls.Add(this.label15);
            this.configurationGroupBox.Controls.Add(this.label13);
            this.configurationGroupBox.Controls.Add(this.groupBox2);
            this.configurationGroupBox.Enabled = false;
            this.configurationGroupBox.ForeColor = System.Drawing.Color.White;
            this.configurationGroupBox.Location = new System.Drawing.Point(104, 7);
            this.configurationGroupBox.Name = "configurationGroupBox";
            this.configurationGroupBox.Size = new System.Drawing.Size(293, 301);
            this.configurationGroupBox.TabIndex = 271;
            this.configurationGroupBox.TabStop = false;
            this.configurationGroupBox.Text = "Configuration";
            // 
            // scriptButton
            // 
            this.scriptButton.Enabled = false;
            this.scriptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scriptButton.ForeColor = System.Drawing.Color.White;
            this.scriptButton.Location = new System.Drawing.Point(16, 48);
            this.scriptButton.Name = "scriptButton";
            this.scriptButton.Size = new System.Drawing.Size(261, 23);
            this.scriptButton.TabIndex = 273;
            this.scriptButton.Text = "Script Editor";
            this.scriptButton.UseVisualStyleBackColor = true;
            this.scriptButton.Click += new System.EventHandler(this.ScriptButton_Click);
            // 
            // valueFlatNumericUpDown
            // 
            this.valueFlatNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.valueFlatNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.valueFlatNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueFlatNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.valueFlatNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.valueFlatNumericUpDown.Location = new System.Drawing.Point(197, 16);
            this.valueFlatNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.valueFlatNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.valueFlatNumericUpDown.Name = "valueFlatNumericUpDown";
            this.valueFlatNumericUpDown.Size = new System.Drawing.Size(80, 22);
            this.valueFlatNumericUpDown.TabIndex = 272;
            // 
            // eventTypeFlatComboBox
            // 
            this.eventTypeFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.eventTypeFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.eventTypeFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.eventTypeFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.eventTypeFlatComboBox.FormattingEnabled = true;
            this.eventTypeFlatComboBox.Items.AddRange(new object[] {
            "None (0)",
            "Text (1)",
            "Script (2)",
            "Script (3)",
            "Talk (4)",
            "Talk (5)",
            "Event (6)",
            "Talk (7)",
            "Shop (8)"});
            this.eventTypeFlatComboBox.Location = new System.Drawing.Point(53, 16);
            this.eventTypeFlatComboBox.Name = "eventTypeFlatComboBox";
            this.eventTypeFlatComboBox.Size = new System.Drawing.Size(80, 21);
            this.eventTypeFlatComboBox.TabIndex = 57;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(157, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(36, 13);
            this.label15.TabIndex = 271;
            this.label15.Text = "Value";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(13, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 13);
            this.label13.TabIndex = 58;
            this.label13.Text = "Type";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.eventCondTextBox);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(16, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 124);
            this.groupBox2.TabIndex = 270;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Condition";
            // 
            // eventCondTextBox
            // 
            this.eventCondTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.eventCondTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.eventCondTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.eventCondTextBox.Location = new System.Drawing.Point(6, 18);
            this.eventCondTextBox.Multiline = true;
            this.eventCondTextBox.Name = "eventCondTextBox";
            this.eventCondTextBox.ReadOnly = true;
            this.eventCondTextBox.Size = new System.Drawing.Size(242, 100);
            this.eventCondTextBox.TabIndex = 271;
            // 
            // eventListBox
            // 
            this.eventListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.eventListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eventListBox.ForeColor = System.Drawing.Color.White;
            this.eventListBox.FormattingEnabled = true;
            this.eventListBox.Location = new System.Drawing.Point(6, 7);
            this.eventListBox.Name = "eventListBox";
            this.eventListBox.Size = new System.Drawing.Size(92, 301);
            this.eventListBox.TabIndex = 52;
            this.eventListBox.SelectedIndexChanged += new System.EventHandler(this.EventListBox_SelectedIndexChanged);
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(1090, 427);
            this.Controls.Add(this.vsTabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.selectedNpcMapPictureBox);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MapEditor";
            this.Text = "MapEditor";
            ((System.ComponentModel.ISupportInitialize)(this.selectedNpcMapPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.vsTabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.mapVSTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.appearanceGroupBox.ResumeLayout(false);
            this.appearanceGroupBox.PerformLayout();
            this.appearGroupBox.ResumeLayout(false);
            this.appearGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rotationNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationZNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationYNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationXFlatNumericUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.animationGroupBox1.ResumeLayout(false);
            this.animationGroupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.configurationGroupBox.ResumeLayout(false);
            this.configurationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valueFlatNumericUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox selectedNpcMapPictureBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewNPCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem focusNPCToolStripMenuItem;
        private UI.VSTabControl vsTabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private UI.VSTabControl mapVSTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label19;
        private UI.FlatComboBox headFlatComboBox;
        private UI.FlatComboBox glovesFlatComboBox;
        private UI.FlatComboBox bootsFlatComboBox;
        private UI.FlatComboBox uniformFlatComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label3;
        private UI.FlatComboBox iconFlatComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.GroupBox appearGroupBox;
        private UI.FlatNumericUpDown rotationNumericUpDown;
        private System.Windows.Forms.Label label9;
        private UI.FlatNumericUpDown locationZNumericUpDown;
        private System.Windows.Forms.Label label11;
        private UI.FlatNumericUpDown locationYNumericUpDown;
        private System.Windows.Forms.Label label10;
        private UI.FlatNumericUpDown locationXFlatNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox animationGroupBox1;
        private System.Windows.Forms.TextBox unkAnimationTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox talkAnimationTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox restAnimationTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox positionCondTextBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button scriptButton;
        private UI.FlatNumericUpDown valueFlatNumericUpDown;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox eventCondTextBox;
        private System.Windows.Forms.Label label13;
        private UI.FlatComboBox eventTypeFlatComboBox;
        private System.Windows.Forms.TreeView npcTreeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox appearanceGroupBox;
        private System.Windows.Forms.ListBox eventListBox;
        private System.Windows.Forms.GroupBox configurationGroupBox;
    }
}