namespace Lynx.Forms.ChallengeRoute
{
    partial class ChallengeRouteWindow
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
            this.components = new System.ComponentModel.Container();
            this.previewPictureBox = new System.Windows.Forms.PictureBox();
            this.challengeRouteGroupBox = new System.Windows.Forms.GroupBox();
            this.filenameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.itemGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.cellFlatComboBox = new Lynx.UI.FlatComboBox();
            this.conditionGroupBox = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.conditionLineNumberRTB = new Lynx.UI.LineNumberRTB();
            this.restrictionGroupBox = new System.Windows.Forms.GroupBox();
            this.mapTextBox = new System.Windows.Forms.TextBox();
            this.textLockTextBox = new System.Windows.Forms.TextBox();
            this.matchFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.linkGroupBox = new System.Windows.Forms.GroupBox();
            this.unlinkButton2 = new System.Windows.Forms.Button();
            this.unlinkButton3 = new System.Windows.Forms.Button();
            this.unlinButton1 = new System.Windows.Forms.Button();
            this.cellLinkFlatComboBox2 = new Lynx.UI.FlatComboBox();
            this.cellLinkFlatComboBox1 = new Lynx.UI.FlatComboBox();
            this.cellLinkFlatComboBox3 = new Lynx.UI.FlatComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.informationGroupBox = new System.Windows.Forms.GroupBox();
            this.cellContentFlatComboBox = new Lynx.UI.FlatComboBox();
            this.cellTypeFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cellNumberFlatNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.cellFlagFlatNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.routeListBox = new System.Windows.Forms.ListBox();
            this.routeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsCfgbinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAscsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAllCellsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllCellsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            this.challengeRouteGroupBox.SuspendLayout();
            this.itemGroupBox.SuspendLayout();
            this.conditionGroupBox.SuspendLayout();
            this.restrictionGroupBox.SuspendLayout();
            this.linkGroupBox.SuspendLayout();
            this.informationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cellNumberFlatNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellFlagFlatNumericUpDown)).BeginInit();
            this.routeContextMenuStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // previewPictureBox
            // 
            this.previewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPictureBox.Location = new System.Drawing.Point(12, 404);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.Size = new System.Drawing.Size(445, 205);
            this.previewPictureBox.TabIndex = 0;
            this.previewPictureBox.TabStop = false;
            this.previewPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PreviewPictureBox_Paint);
            this.previewPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PreviewPictureBox_MouseDown);
            this.previewPictureBox.MouseEnter += new System.EventHandler(this.PreviewPictureBox_MouseEnter);
            this.previewPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PreviewPictureBox_MouseMove);
            this.previewPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PreviewPictureBox_MouseUp);
            // 
            // challengeRouteGroupBox
            // 
            this.challengeRouteGroupBox.Controls.Add(this.filenameTextBox);
            this.challengeRouteGroupBox.Controls.Add(this.label3);
            this.challengeRouteGroupBox.Controls.Add(this.nameTextBox);
            this.challengeRouteGroupBox.Controls.Add(this.label1);
            this.challengeRouteGroupBox.Controls.Add(this.itemGroupBox);
            this.challengeRouteGroupBox.Enabled = false;
            this.challengeRouteGroupBox.ForeColor = System.Drawing.Color.White;
            this.challengeRouteGroupBox.Location = new System.Drawing.Point(463, 24);
            this.challengeRouteGroupBox.Name = "challengeRouteGroupBox";
            this.challengeRouteGroupBox.Size = new System.Drawing.Size(668, 585);
            this.challengeRouteGroupBox.TabIndex = 277;
            this.challengeRouteGroupBox.TabStop = false;
            this.challengeRouteGroupBox.Text = "Challenge Route";
            // 
            // filenameTextBox
            // 
            this.filenameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.filenameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.filenameTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.filenameTextBox.Location = new System.Drawing.Point(411, 25);
            this.filenameTextBox.Name = "filenameTextBox";
            this.filenameTextBox.Size = new System.Drawing.Size(220, 13);
            this.filenameTextBox.TabIndex = 303;
            this.filenameTextBox.TextChanged += new System.EventHandler(this.FilenameTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(343, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 302;
            this.label3.Text = "Filename";
            // 
            // nameTextBox
            // 
            this.nameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nameTextBox.Location = new System.Drawing.Point(106, 25);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(217, 13);
            this.nameTextBox.TabIndex = 301;
            this.nameTextBox.Click += new System.EventHandler(this.NameTextBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(38, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 284;
            this.label1.Text = "Name";
            // 
            // itemGroupBox
            // 
            this.itemGroupBox.Controls.Add(this.label4);
            this.itemGroupBox.Controls.Add(this.deleteButton);
            this.itemGroupBox.Controls.Add(this.addButton);
            this.itemGroupBox.Controls.Add(this.cellFlatComboBox);
            this.itemGroupBox.Controls.Add(this.conditionGroupBox);
            this.itemGroupBox.Controls.Add(this.restrictionGroupBox);
            this.itemGroupBox.Controls.Add(this.linkGroupBox);
            this.itemGroupBox.Controls.Add(this.informationGroupBox);
            this.itemGroupBox.ForeColor = System.Drawing.Color.White;
            this.itemGroupBox.Location = new System.Drawing.Point(15, 45);
            this.itemGroupBox.Name = "itemGroupBox";
            this.itemGroupBox.Size = new System.Drawing.Size(640, 528);
            this.itemGroupBox.TabIndex = 272;
            this.itemGroupBox.TabStop = false;
            this.itemGroupBox.Text = "Cell";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(23, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 303;
            this.label4.Text = "Selected";
            // 
            // deleteButton
            // 
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(483, 17);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(130, 23);
            this.deleteButton.TabIndex = 302;
            this.deleteButton.Text = "Delete Cell";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // addButton
            // 
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Location = new System.Drawing.Point(331, 17);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(130, 23);
            this.addButton.TabIndex = 301;
            this.addButton.Text = "Add Cell";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // cellFlatComboBox
            // 
            this.cellFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellFlatComboBox.FormattingEnabled = true;
            this.cellFlatComboBox.Location = new System.Drawing.Point(91, 19);
            this.cellFlatComboBox.Name = "cellFlatComboBox";
            this.cellFlatComboBox.Size = new System.Drawing.Size(217, 21);
            this.cellFlatComboBox.TabIndex = 300;
            this.cellFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.CellFlatComboBox_SelectedIndexChanged);
            // 
            // conditionGroupBox
            // 
            this.conditionGroupBox.Controls.Add(this.button2);
            this.conditionGroupBox.Controls.Add(this.conditionLineNumberRTB);
            this.conditionGroupBox.ForeColor = System.Drawing.Color.White;
            this.conditionGroupBox.Location = new System.Drawing.Point(6, 329);
            this.conditionGroupBox.Name = "conditionGroupBox";
            this.conditionGroupBox.Size = new System.Drawing.Size(620, 191);
            this.conditionGroupBox.TabIndex = 298;
            this.conditionGroupBox.TabStop = false;
            this.conditionGroupBox.Text = "Condition";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(9, 160);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(601, 23);
            this.button2.TabIndex = 274;
            this.button2.Text = "Compile";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // conditionLineNumberRTB
            // 
            this.conditionLineNumberRTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionLineNumberRTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.conditionLineNumberRTB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.conditionLineNumberRTB.Location = new System.Drawing.Point(9, 21);
            this.conditionLineNumberRTB.Name = "conditionLineNumberRTB";
            this.conditionLineNumberRTB.Size = new System.Drawing.Size(601, 133);
            this.conditionLineNumberRTB.TabIndex = 269;
            // 
            // restrictionGroupBox
            // 
            this.restrictionGroupBox.Controls.Add(this.mapTextBox);
            this.restrictionGroupBox.Controls.Add(this.textLockTextBox);
            this.restrictionGroupBox.Controls.Add(this.matchFlatComboBox);
            this.restrictionGroupBox.Controls.Add(this.label15);
            this.restrictionGroupBox.Controls.Add(this.label19);
            this.restrictionGroupBox.Controls.Add(this.label20);
            this.restrictionGroupBox.ForeColor = System.Drawing.Color.White;
            this.restrictionGroupBox.Location = new System.Drawing.Point(6, 237);
            this.restrictionGroupBox.Name = "restrictionGroupBox";
            this.restrictionGroupBox.Size = new System.Drawing.Size(620, 86);
            this.restrictionGroupBox.TabIndex = 297;
            this.restrictionGroupBox.TabStop = false;
            this.restrictionGroupBox.Text = "Restriction";
            // 
            // mapTextBox
            // 
            this.mapTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.mapTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mapTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.mapTextBox.Location = new System.Drawing.Point(85, 53);
            this.mapTextBox.Name = "mapTextBox";
            this.mapTextBox.Size = new System.Drawing.Size(217, 13);
            this.mapTextBox.TabIndex = 301;
            this.mapTextBox.TextChanged += new System.EventHandler(this.MapTextBox_TextChanged);
            // 
            // textLockTextBox
            // 
            this.textLockTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.textLockTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textLockTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.textLockTextBox.Location = new System.Drawing.Point(390, 26);
            this.textLockTextBox.Name = "textLockTextBox";
            this.textLockTextBox.Size = new System.Drawing.Size(217, 13);
            this.textLockTextBox.TabIndex = 300;
            this.textLockTextBox.TextChanged += new System.EventHandler(this.TextLockTextBox_TextChanged);
            // 
            // matchFlatComboBox
            // 
            this.matchFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.matchFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.matchFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.matchFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.matchFlatComboBox.FormattingEnabled = true;
            this.matchFlatComboBox.Location = new System.Drawing.Point(85, 26);
            this.matchFlatComboBox.Name = "matchFlatComboBox";
            this.matchFlatComboBox.Size = new System.Drawing.Size(217, 21);
            this.matchFlatComboBox.TabIndex = 298;
            this.matchFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.MatchFlatComboBox_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(17, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(37, 13);
            this.label15.TabIndex = 68;
            this.label15.Text = "Match";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(17, 52);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(28, 13);
            this.label19.TabIndex = 290;
            this.label19.Text = "Map";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(333, 26);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(51, 13);
            this.label20.TabIndex = 291;
            this.label20.Text = "Text lock";
            // 
            // linkGroupBox
            // 
            this.linkGroupBox.Controls.Add(this.unlinkButton2);
            this.linkGroupBox.Controls.Add(this.unlinkButton3);
            this.linkGroupBox.Controls.Add(this.unlinButton1);
            this.linkGroupBox.Controls.Add(this.cellLinkFlatComboBox2);
            this.linkGroupBox.Controls.Add(this.cellLinkFlatComboBox1);
            this.linkGroupBox.Controls.Add(this.cellLinkFlatComboBox3);
            this.linkGroupBox.Controls.Add(this.label12);
            this.linkGroupBox.Controls.Add(this.label16);
            this.linkGroupBox.Controls.Add(this.label17);
            this.linkGroupBox.ForeColor = System.Drawing.Color.White;
            this.linkGroupBox.Location = new System.Drawing.Point(6, 145);
            this.linkGroupBox.Name = "linkGroupBox";
            this.linkGroupBox.Size = new System.Drawing.Size(620, 86);
            this.linkGroupBox.TabIndex = 296;
            this.linkGroupBox.TabStop = false;
            this.linkGroupBox.Text = "Link";
            // 
            // unlinkButton2
            // 
            this.unlinkButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.unlinkButton2.ForeColor = System.Drawing.Color.White;
            this.unlinkButton2.Location = new System.Drawing.Point(583, 21);
            this.unlinkButton2.Name = "unlinkButton2";
            this.unlinkButton2.Size = new System.Drawing.Size(24, 23);
            this.unlinkButton2.TabIndex = 304;
            this.unlinkButton2.Text = "X";
            this.unlinkButton2.UseVisualStyleBackColor = true;
            this.unlinkButton2.Click += new System.EventHandler(this.UnlinkButton2_Click);
            // 
            // unlinkButton3
            // 
            this.unlinkButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.unlinkButton3.ForeColor = System.Drawing.Color.White;
            this.unlinkButton3.Location = new System.Drawing.Point(278, 50);
            this.unlinkButton3.Name = "unlinkButton3";
            this.unlinkButton3.Size = new System.Drawing.Size(24, 23);
            this.unlinkButton3.TabIndex = 303;
            this.unlinkButton3.Text = "X";
            this.unlinkButton3.UseVisualStyleBackColor = true;
            this.unlinkButton3.Click += new System.EventHandler(this.UnlinkButton3_Click);
            // 
            // unlinButton1
            // 
            this.unlinButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.unlinButton1.ForeColor = System.Drawing.Color.White;
            this.unlinButton1.Location = new System.Drawing.Point(278, 21);
            this.unlinButton1.Name = "unlinButton1";
            this.unlinButton1.Size = new System.Drawing.Size(24, 23);
            this.unlinButton1.TabIndex = 302;
            this.unlinButton1.Text = "X";
            this.unlinButton1.UseVisualStyleBackColor = true;
            this.unlinButton1.Click += new System.EventHandler(this.UnlinButton1_Click);
            // 
            // cellLinkFlatComboBox2
            // 
            this.cellLinkFlatComboBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellLinkFlatComboBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellLinkFlatComboBox2.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellLinkFlatComboBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellLinkFlatComboBox2.FormattingEnabled = true;
            this.cellLinkFlatComboBox2.Location = new System.Drawing.Point(390, 23);
            this.cellLinkFlatComboBox2.Name = "cellLinkFlatComboBox2";
            this.cellLinkFlatComboBox2.Size = new System.Drawing.Size(187, 21);
            this.cellLinkFlatComboBox2.TabIndex = 299;
            this.cellLinkFlatComboBox2.SelectedIndexChanged += new System.EventHandler(this.CellLinkFlatComboBox2_SelectedIndexChanged);
            // 
            // cellLinkFlatComboBox1
            // 
            this.cellLinkFlatComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellLinkFlatComboBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellLinkFlatComboBox1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellLinkFlatComboBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellLinkFlatComboBox1.FormattingEnabled = true;
            this.cellLinkFlatComboBox1.Location = new System.Drawing.Point(85, 23);
            this.cellLinkFlatComboBox1.Name = "cellLinkFlatComboBox1";
            this.cellLinkFlatComboBox1.Size = new System.Drawing.Size(187, 21);
            this.cellLinkFlatComboBox1.TabIndex = 298;
            this.cellLinkFlatComboBox1.SelectedIndexChanged += new System.EventHandler(this.CellLinkFlatComboBox1_SelectedIndexChanged);
            // 
            // cellLinkFlatComboBox3
            // 
            this.cellLinkFlatComboBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellLinkFlatComboBox3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellLinkFlatComboBox3.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellLinkFlatComboBox3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellLinkFlatComboBox3.FormattingEnabled = true;
            this.cellLinkFlatComboBox3.Location = new System.Drawing.Point(85, 50);
            this.cellLinkFlatComboBox3.Name = "cellLinkFlatComboBox3";
            this.cellLinkFlatComboBox3.Size = new System.Drawing.Size(187, 21);
            this.cellLinkFlatComboBox3.TabIndex = 296;
            this.cellLinkFlatComboBox3.SelectedIndexChanged += new System.EventHandler(this.cellLinkFlatComboBox3_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(17, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 68;
            this.label12.Text = "Cell Link 1";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(17, 52);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 290;
            this.label16.Text = "Cell Link 3";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(322, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 13);
            this.label17.TabIndex = 291;
            this.label17.Text = "Cell Link 2";
            // 
            // informationGroupBox
            // 
            this.informationGroupBox.Controls.Add(this.cellContentFlatComboBox);
            this.informationGroupBox.Controls.Add(this.cellTypeFlatComboBox);
            this.informationGroupBox.Controls.Add(this.label18);
            this.informationGroupBox.Controls.Add(this.label6);
            this.informationGroupBox.Controls.Add(this.cellNumberFlatNumericUpDown);
            this.informationGroupBox.Controls.Add(this.cellFlagFlatNumericUpDown);
            this.informationGroupBox.Controls.Add(this.label2);
            this.informationGroupBox.Controls.Add(this.label7);
            this.informationGroupBox.ForeColor = System.Drawing.Color.White;
            this.informationGroupBox.Location = new System.Drawing.Point(6, 53);
            this.informationGroupBox.Name = "informationGroupBox";
            this.informationGroupBox.Size = new System.Drawing.Size(620, 86);
            this.informationGroupBox.TabIndex = 295;
            this.informationGroupBox.TabStop = false;
            this.informationGroupBox.Text = "Information";
            // 
            // cellContentFlatComboBox
            // 
            this.cellContentFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellContentFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellContentFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellContentFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellContentFlatComboBox.FormattingEnabled = true;
            this.cellContentFlatComboBox.Location = new System.Drawing.Point(390, 52);
            this.cellContentFlatComboBox.Name = "cellContentFlatComboBox";
            this.cellContentFlatComboBox.Size = new System.Drawing.Size(217, 21);
            this.cellContentFlatComboBox.TabIndex = 297;
            this.cellContentFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.CellContentFlatComboBox_SelectedIndexChanged);
            // 
            // cellTypeFlatComboBox
            // 
            this.cellTypeFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellTypeFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellTypeFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellTypeFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellTypeFlatComboBox.FormattingEnabled = true;
            this.cellTypeFlatComboBox.Items.AddRange(new object[] {
            "None",
            "Start",
            "Match",
            "Blue Chest",
            "Gold Chest"});
            this.cellTypeFlatComboBox.Location = new System.Drawing.Point(85, 52);
            this.cellTypeFlatComboBox.Name = "cellTypeFlatComboBox";
            this.cellTypeFlatComboBox.Size = new System.Drawing.Size(217, 21);
            this.cellTypeFlatComboBox.TabIndex = 296;
            this.cellTypeFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.CellTypeFlatComboBox_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(17, 26);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 13);
            this.label18.TabIndex = 68;
            this.label18.Text = "Cell number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(322, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 294;
            this.label6.Text = "Content";
            // 
            // cellNumberFlatNumericUpDown
            // 
            this.cellNumberFlatNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellNumberFlatNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellNumberFlatNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cellNumberFlatNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellNumberFlatNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellNumberFlatNumericUpDown.Location = new System.Drawing.Point(85, 25);
            this.cellNumberFlatNumericUpDown.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.cellNumberFlatNumericUpDown.Name = "cellNumberFlatNumericUpDown";
            this.cellNumberFlatNumericUpDown.Size = new System.Drawing.Size(217, 20);
            this.cellNumberFlatNumericUpDown.TabIndex = 283;
            this.cellNumberFlatNumericUpDown.ValueChanged += new System.EventHandler(this.CellNumberFlatNumericUpDown_ValueChanged);
            // 
            // cellFlagFlatNumericUpDown
            // 
            this.cellFlagFlatNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellFlagFlatNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellFlagFlatNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cellFlagFlatNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellFlagFlatNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellFlagFlatNumericUpDown.Location = new System.Drawing.Point(390, 26);
            this.cellFlagFlatNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.cellFlagFlatNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.cellFlagFlatNumericUpDown.Name = "cellFlagFlatNumericUpDown";
            this.cellFlagFlatNumericUpDown.Size = new System.Drawing.Size(217, 20);
            this.cellFlagFlatNumericUpDown.TabIndex = 292;
            this.cellFlagFlatNumericUpDown.ValueChanged += new System.EventHandler(this.CellFlagFlatNumericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 290;
            this.label2.Text = "Cell Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(322, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 291;
            this.label7.Text = "Flag";
            // 
            // routeListBox
            // 
            this.routeListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.routeListBox.ContextMenuStrip = this.routeContextMenuStrip;
            this.routeListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.routeListBox.FormattingEnabled = true;
            this.routeListBox.Location = new System.Drawing.Point(12, 29);
            this.routeListBox.Name = "routeListBox";
            this.routeListBox.Size = new System.Drawing.Size(445, 368);
            this.routeListBox.TabIndex = 280;
            this.routeListBox.SelectedIndexChanged += new System.EventHandler(this.RouteListBox_SelectedIndexChanged);
            // 
            // routeContextMenuStrip
            // 
            this.routeContextMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.routeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.routeContextMenuStrip.Name = "characterContextMenuStrip";
            this.routeContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.routeContextMenuStrip.Size = new System.Drawing.Size(181, 70);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.insertToolStripMenuItem.Text = "Add";
            this.insertToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.manageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1140, 24);
            this.menuStrip1.TabIndex = 281;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsCfgbinToolStripMenuItem,
            this.exportAscsvToolStripMenuItem});
            this.exportToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // exportAsCfgbinToolStripMenuItem
            // 
            this.exportAsCfgbinToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.exportAsCfgbinToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportAsCfgbinToolStripMenuItem.Name = "exportAsCfgbinToolStripMenuItem";
            this.exportAsCfgbinToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exportAsCfgbinToolStripMenuItem.Text = "Export as .cfg.bin";
            // 
            // exportAscsvToolStripMenuItem
            // 
            this.exportAscsvToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.exportAscsvToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportAscsvToolStripMenuItem.Name = "exportAscsvToolStripMenuItem";
            this.exportAscsvToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exportAscsvToolStripMenuItem.Text = "Export as .xlsx";
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAllCellsToolStripMenuItem,
            this.removeAllCellsToolStripMenuItem});
            this.manageToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.manageToolStripMenuItem.Text = "Manage";
            // 
            // addAllCellsToolStripMenuItem
            // 
            this.addAllCellsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.addAllCellsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.addAllCellsToolStripMenuItem.Name = "addAllCellsToolStripMenuItem";
            this.addAllCellsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.addAllCellsToolStripMenuItem.Text = "Add all unasigned cells";
            this.addAllCellsToolStripMenuItem.Click += new System.EventHandler(this.AddAllCellsToolStripMenuItem_Click);
            // 
            // removeAllCellsToolStripMenuItem
            // 
            this.removeAllCellsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.removeAllCellsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.removeAllCellsToolStripMenuItem.Name = "removeAllCellsToolStripMenuItem";
            this.removeAllCellsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.removeAllCellsToolStripMenuItem.Text = "Delete all cells";
            this.removeAllCellsToolStripMenuItem.Click += new System.EventHandler(this.RemoveAllCellsToolStripMenuItem_Click);
            // 
            // ChallengeRouteWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(1140, 616);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.routeListBox);
            this.Controls.Add(this.challengeRouteGroupBox);
            this.Controls.Add(this.previewPictureBox);
            this.KeyPreview = true;
            this.Name = "ChallengeRouteWindow";
            this.Text = "ChallengeRouteWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChallengeRouteWindow_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChallengeRouteWindow_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
            this.challengeRouteGroupBox.ResumeLayout(false);
            this.challengeRouteGroupBox.PerformLayout();
            this.itemGroupBox.ResumeLayout(false);
            this.itemGroupBox.PerformLayout();
            this.conditionGroupBox.ResumeLayout(false);
            this.restrictionGroupBox.ResumeLayout(false);
            this.restrictionGroupBox.PerformLayout();
            this.linkGroupBox.ResumeLayout(false);
            this.linkGroupBox.PerformLayout();
            this.informationGroupBox.ResumeLayout(false);
            this.informationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cellNumberFlatNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellFlagFlatNumericUpDown)).EndInit();
            this.routeContextMenuStrip.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.GroupBox challengeRouteGroupBox;
        private System.Windows.Forms.GroupBox itemGroupBox;
        private UI.FlatNumericUpDown cellNumberFlatNumericUpDown;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private UI.FlatNumericUpDown cellFlagFlatNumericUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox informationGroupBox;
        private UI.FlatComboBox cellTypeFlatComboBox;
        private System.Windows.Forms.GroupBox restrictionGroupBox;
        private UI.FlatComboBox matchFlatComboBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox linkGroupBox;
        private UI.FlatComboBox cellLinkFlatComboBox2;
        private UI.FlatComboBox cellLinkFlatComboBox1;
        private UI.FlatComboBox cellLinkFlatComboBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private UI.FlatComboBox cellContentFlatComboBox;
        private System.Windows.Forms.TextBox textLockTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox conditionGroupBox;
        private System.Windows.Forms.Button button2;
        private UI.LineNumberRTB conditionLineNumberRTB;
        private System.Windows.Forms.TextBox mapTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private UI.FlatComboBox cellFlatComboBox;
        private System.Windows.Forms.ListBox routeListBox;
        private System.Windows.Forms.TextBox filenameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAsCfgbinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAscsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAllCellsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllCellsToolStripMenuItem;
        private System.Windows.Forms.Button unlinkButton3;
        private System.Windows.Forms.Button unlinButton1;
        private System.Windows.Forms.Button unlinkButton2;
        private System.Windows.Forms.ContextMenuStrip routeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}