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
            this.previewPictureBox = new System.Windows.Forms.PictureBox();
            this.challengeRouteGroupBox = new System.Windows.Forms.GroupBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.itemGroupBox = new System.Windows.Forms.GroupBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.cellFlatComboBox = new Lynx.UI.FlatComboBox();
            this.conditionGroupBox = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.positionCondLineNumberRTB = new Lynx.UI.LineNumberRTB();
            this.restrictionGroupBox = new System.Windows.Forms.GroupBox();
            this.mapTextBox = new System.Windows.Forms.TextBox();
            this.textLockTextBox = new System.Windows.Forms.TextBox();
            this.matchFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.linkGroupBox = new System.Windows.Forms.GroupBox();
            this.cellLinkFlatComboBox2 = new Lynx.UI.FlatComboBox();
            this.cellLinkFlatComboBox1 = new Lynx.UI.FlatComboBox();
            this.cellLinkFlatComboBo3 = new Lynx.UI.FlatComboBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            this.challengeRouteGroupBox.SuspendLayout();
            this.itemGroupBox.SuspendLayout();
            this.conditionGroupBox.SuspendLayout();
            this.restrictionGroupBox.SuspendLayout();
            this.linkGroupBox.SuspendLayout();
            this.informationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cellNumberFlatNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellFlagFlatNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // previewPictureBox
            // 
            this.previewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPictureBox.Location = new System.Drawing.Point(12, 393);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.Size = new System.Drawing.Size(496, 200);
            this.previewPictureBox.TabIndex = 0;
            this.previewPictureBox.TabStop = false;
            this.previewPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PreviewPictureBox_Paint);
            // 
            // challengeRouteGroupBox
            // 
            this.challengeRouteGroupBox.Controls.Add(this.nameTextBox);
            this.challengeRouteGroupBox.Controls.Add(this.label1);
            this.challengeRouteGroupBox.Controls.Add(this.itemGroupBox);
            this.challengeRouteGroupBox.Enabled = false;
            this.challengeRouteGroupBox.ForeColor = System.Drawing.Color.White;
            this.challengeRouteGroupBox.Location = new System.Drawing.Point(514, 7);
            this.challengeRouteGroupBox.Name = "challengeRouteGroupBox";
            this.challengeRouteGroupBox.Size = new System.Drawing.Size(565, 586);
            this.challengeRouteGroupBox.TabIndex = 277;
            this.challengeRouteGroupBox.TabStop = false;
            this.challengeRouteGroupBox.Text = "Challenge Route";
            // 
            // nameTextBox
            // 
            this.nameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nameTextBox.Location = new System.Drawing.Point(53, 25);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(499, 13);
            this.nameTextBox.TabIndex = 301;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 284;
            this.label1.Text = "Name";
            // 
            // itemGroupBox
            // 
            this.itemGroupBox.Controls.Add(this.deleteButton);
            this.itemGroupBox.Controls.Add(this.addButton);
            this.itemGroupBox.Controls.Add(this.cellFlatComboBox);
            this.itemGroupBox.Controls.Add(this.conditionGroupBox);
            this.itemGroupBox.Controls.Add(this.restrictionGroupBox);
            this.itemGroupBox.Controls.Add(this.linkGroupBox);
            this.itemGroupBox.Controls.Add(this.informationGroupBox);
            this.itemGroupBox.ForeColor = System.Drawing.Color.White;
            this.itemGroupBox.Location = new System.Drawing.Point(15, 50);
            this.itemGroupBox.Name = "itemGroupBox";
            this.itemGroupBox.Size = new System.Drawing.Size(537, 528);
            this.itemGroupBox.TabIndex = 272;
            this.itemGroupBox.TabStop = false;
            this.itemGroupBox.Text = "Cell";
            // 
            // deleteButton
            // 
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(413, 17);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(115, 23);
            this.deleteButton.TabIndex = 302;
            this.deleteButton.Text = "Delete Cell";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // addButton
            // 
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Location = new System.Drawing.Point(284, 17);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(115, 23);
            this.addButton.TabIndex = 301;
            this.addButton.Text = "Add Cell";
            this.addButton.UseVisualStyleBackColor = true;
            // 
            // cellFlatComboBox
            // 
            this.cellFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellFlatComboBox.FormattingEnabled = true;
            this.cellFlatComboBox.Location = new System.Drawing.Point(6, 19);
            this.cellFlatComboBox.Name = "cellFlatComboBox";
            this.cellFlatComboBox.Size = new System.Drawing.Size(254, 21);
            this.cellFlatComboBox.TabIndex = 300;
            this.cellFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.CellFlatComboBox_SelectedIndexChanged);
            // 
            // conditionGroupBox
            // 
            this.conditionGroupBox.Controls.Add(this.button2);
            this.conditionGroupBox.Controls.Add(this.positionCondLineNumberRTB);
            this.conditionGroupBox.ForeColor = System.Drawing.Color.White;
            this.conditionGroupBox.Location = new System.Drawing.Point(6, 329);
            this.conditionGroupBox.Name = "conditionGroupBox";
            this.conditionGroupBox.Size = new System.Drawing.Size(522, 191);
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
            this.button2.Size = new System.Drawing.Size(504, 23);
            this.button2.TabIndex = 274;
            this.button2.Text = "Compile";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // positionCondLineNumberRTB
            // 
            this.positionCondLineNumberRTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.positionCondLineNumberRTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.positionCondLineNumberRTB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.positionCondLineNumberRTB.Location = new System.Drawing.Point(9, 21);
            this.positionCondLineNumberRTB.Name = "positionCondLineNumberRTB";
            this.positionCondLineNumberRTB.Size = new System.Drawing.Size(503, 133);
            this.positionCondLineNumberRTB.TabIndex = 269;
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
            this.restrictionGroupBox.Size = new System.Drawing.Size(522, 86);
            this.restrictionGroupBox.TabIndex = 297;
            this.restrictionGroupBox.TabStop = false;
            this.restrictionGroupBox.Text = "Restriction";
            // 
            // mapTextBox
            // 
            this.mapTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.mapTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mapTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.mapTextBox.Location = new System.Drawing.Point(102, 52);
            this.mapTextBox.Name = "mapTextBox";
            this.mapTextBox.Size = new System.Drawing.Size(152, 13);
            this.mapTextBox.TabIndex = 301;
            // 
            // textLockTextBox
            // 
            this.textLockTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.textLockTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textLockTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.textLockTextBox.Location = new System.Drawing.Point(360, 26);
            this.textLockTextBox.Name = "textLockTextBox";
            this.textLockTextBox.Size = new System.Drawing.Size(152, 13);
            this.textLockTextBox.TabIndex = 300;
            // 
            // matchFlatComboBox
            // 
            this.matchFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.matchFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.matchFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.matchFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.matchFlatComboBox.FormattingEnabled = true;
            this.matchFlatComboBox.Location = new System.Drawing.Point(102, 24);
            this.matchFlatComboBox.Name = "matchFlatComboBox";
            this.matchFlatComboBox.Size = new System.Drawing.Size(152, 21);
            this.matchFlatComboBox.TabIndex = 298;
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
            this.label20.Location = new System.Drawing.Point(275, 26);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(51, 13);
            this.label20.TabIndex = 291;
            this.label20.Text = "Text lock";
            // 
            // linkGroupBox
            // 
            this.linkGroupBox.Controls.Add(this.cellLinkFlatComboBox2);
            this.linkGroupBox.Controls.Add(this.cellLinkFlatComboBox1);
            this.linkGroupBox.Controls.Add(this.cellLinkFlatComboBo3);
            this.linkGroupBox.Controls.Add(this.label12);
            this.linkGroupBox.Controls.Add(this.label16);
            this.linkGroupBox.Controls.Add(this.label17);
            this.linkGroupBox.ForeColor = System.Drawing.Color.White;
            this.linkGroupBox.Location = new System.Drawing.Point(6, 145);
            this.linkGroupBox.Name = "linkGroupBox";
            this.linkGroupBox.Size = new System.Drawing.Size(522, 86);
            this.linkGroupBox.TabIndex = 296;
            this.linkGroupBox.TabStop = false;
            this.linkGroupBox.Text = "Link";
            // 
            // cellLinkFlatComboBox2
            // 
            this.cellLinkFlatComboBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellLinkFlatComboBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellLinkFlatComboBox2.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellLinkFlatComboBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellLinkFlatComboBox2.FormattingEnabled = true;
            this.cellLinkFlatComboBox2.Location = new System.Drawing.Point(360, 24);
            this.cellLinkFlatComboBox2.Name = "cellLinkFlatComboBox2";
            this.cellLinkFlatComboBox2.Size = new System.Drawing.Size(152, 21);
            this.cellLinkFlatComboBox2.TabIndex = 299;
            // 
            // cellLinkFlatComboBox1
            // 
            this.cellLinkFlatComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellLinkFlatComboBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellLinkFlatComboBox1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellLinkFlatComboBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellLinkFlatComboBox1.FormattingEnabled = true;
            this.cellLinkFlatComboBox1.Location = new System.Drawing.Point(102, 24);
            this.cellLinkFlatComboBox1.Name = "cellLinkFlatComboBox1";
            this.cellLinkFlatComboBox1.Size = new System.Drawing.Size(152, 21);
            this.cellLinkFlatComboBox1.TabIndex = 298;
            // 
            // cellLinkFlatComboBo3
            // 
            this.cellLinkFlatComboBo3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellLinkFlatComboBo3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellLinkFlatComboBo3.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellLinkFlatComboBo3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellLinkFlatComboBo3.FormattingEnabled = true;
            this.cellLinkFlatComboBo3.Location = new System.Drawing.Point(102, 51);
            this.cellLinkFlatComboBo3.Name = "cellLinkFlatComboBo3";
            this.cellLinkFlatComboBo3.Size = new System.Drawing.Size(152, 21);
            this.cellLinkFlatComboBo3.TabIndex = 296;
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
            this.label17.Location = new System.Drawing.Point(275, 26);
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
            this.informationGroupBox.Size = new System.Drawing.Size(522, 86);
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
            this.cellContentFlatComboBox.Location = new System.Drawing.Point(360, 52);
            this.cellContentFlatComboBox.Name = "cellContentFlatComboBox";
            this.cellContentFlatComboBox.Size = new System.Drawing.Size(152, 21);
            this.cellContentFlatComboBox.TabIndex = 297;
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
            this.cellTypeFlatComboBox.Location = new System.Drawing.Point(102, 51);
            this.cellTypeFlatComboBox.Name = "cellTypeFlatComboBox";
            this.cellTypeFlatComboBox.Size = new System.Drawing.Size(152, 21);
            this.cellTypeFlatComboBox.TabIndex = 296;
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
            this.label6.Location = new System.Drawing.Point(275, 54);
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
            this.cellNumberFlatNumericUpDown.Location = new System.Drawing.Point(102, 24);
            this.cellNumberFlatNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.cellNumberFlatNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.cellNumberFlatNumericUpDown.Name = "cellNumberFlatNumericUpDown";
            this.cellNumberFlatNumericUpDown.Size = new System.Drawing.Size(152, 20);
            this.cellNumberFlatNumericUpDown.TabIndex = 283;
            // 
            // cellFlagFlatNumericUpDown
            // 
            this.cellFlagFlatNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cellFlagFlatNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.cellFlagFlatNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cellFlagFlatNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.cellFlagFlatNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cellFlagFlatNumericUpDown.Location = new System.Drawing.Point(360, 26);
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
            this.cellFlagFlatNumericUpDown.Size = new System.Drawing.Size(152, 20);
            this.cellFlagFlatNumericUpDown.TabIndex = 292;
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
            this.label7.Location = new System.Drawing.Point(275, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 291;
            this.label7.Text = "Flag";
            // 
            // routeListBox
            // 
            this.routeListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.routeListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.routeListBox.FormattingEnabled = true;
            this.routeListBox.Location = new System.Drawing.Point(12, 12);
            this.routeListBox.Name = "routeListBox";
            this.routeListBox.Size = new System.Drawing.Size(496, 368);
            this.routeListBox.TabIndex = 280;
            this.routeListBox.SelectedIndexChanged += new System.EventHandler(this.RouteListBox_SelectedIndexChanged);
            // 
            // ChallengeRouteWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(1094, 604);
            this.Controls.Add(this.routeListBox);
            this.Controls.Add(this.challengeRouteGroupBox);
            this.Controls.Add(this.previewPictureBox);
            this.Name = "ChallengeRouteWindow";
            this.Text = "ChallengeRouteWindow";
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
            this.challengeRouteGroupBox.ResumeLayout(false);
            this.challengeRouteGroupBox.PerformLayout();
            this.itemGroupBox.ResumeLayout(false);
            this.conditionGroupBox.ResumeLayout(false);
            this.restrictionGroupBox.ResumeLayout(false);
            this.restrictionGroupBox.PerformLayout();
            this.linkGroupBox.ResumeLayout(false);
            this.linkGroupBox.PerformLayout();
            this.informationGroupBox.ResumeLayout(false);
            this.informationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cellNumberFlatNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellFlagFlatNumericUpDown)).EndInit();
            this.ResumeLayout(false);

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
        private UI.FlatComboBox cellLinkFlatComboBo3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private UI.FlatComboBox cellContentFlatComboBox;
        private System.Windows.Forms.TextBox textLockTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox conditionGroupBox;
        private System.Windows.Forms.Button button2;
        private UI.LineNumberRTB positionCondLineNumberRTB;
        private System.Windows.Forms.TextBox mapTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private UI.FlatComboBox cellFlatComboBox;
        private System.Windows.Forms.ListBox routeListBox;
    }
}