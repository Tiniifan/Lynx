namespace Lynx.Forms.Skills
{
    partial class SkillWindow
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
            this.skillTreeView = new System.Windows.Forms.TreeView();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.infoGroupBox = new System.Windows.Forms.GroupBox();
            this.numberFlatNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameWazaTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.growFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.typeFlatComboBox = new Lynx.UI.FlatComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.moveTypeRadioButton2 = new System.Windows.Forms.RadioButton();
            this.partnerFlatNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.moveTypeRadioButton1 = new System.Windows.Forms.RadioButton();
            this.effectFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.elementFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.positonFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.skillEffectFlatComboBox = new Lynx.UI.FlatComboBox();
            this.effectFlatNumericUpDown1 = new Lynx.UI.FlatNumericUpDown();
            this.boostPanel = new System.Windows.Forms.Panel();
            this.boostActiveRadioButton2 = new System.Windows.Forms.RadioButton();
            this.boostActiveRadioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.techniqueFlatNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.faultRateFlatNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.powerFlatNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.tpCostFlatNumericUpDown = new Lynx.UI.FlatNumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.skillGroupBox = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberFlatNumericUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.partnerFlatNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectFlatNumericUpDown1)).BeginInit();
            this.boostPanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.techniqueFlatNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faultRateFlatNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerFlatNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpCostFlatNumericUpDown)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.skillGroupBox.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // skillTreeView
            // 
            this.skillTreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.skillTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.skillTreeView.ForeColor = System.Drawing.Color.White;
            this.skillTreeView.LineColor = System.Drawing.Color.White;
            this.skillTreeView.Location = new System.Drawing.Point(12, 39);
            this.skillTreeView.Name = "skillTreeView";
            this.skillTreeView.Size = new System.Drawing.Size(243, 370);
            this.skillTreeView.TabIndex = 58;
            this.skillTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SkillTreeView_AfterSelect);
            this.skillTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SkillTreeView_MouseDown);
            // 
            // searchTextBox
            // 
            this.searchTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchTextBox.Location = new System.Drawing.Point(12, 12);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(243, 13);
            this.searchTextBox.TabIndex = 57;
            this.searchTextBox.Text = "Search...";
            this.searchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            this.searchTextBox.MouseEnter += new System.EventHandler(this.SearchTextBox_MouseEnter);
            this.searchTextBox.MouseLeave += new System.EventHandler(this.SearchTextBox_MouseLeave);
            // 
            // infoGroupBox
            // 
            this.infoGroupBox.Controls.Add(this.numberFlatNumericUpDown);
            this.infoGroupBox.Controls.Add(this.label14);
            this.infoGroupBox.Controls.Add(this.label13);
            this.infoGroupBox.Controls.Add(this.idTextBox);
            this.infoGroupBox.Controls.Add(this.label5);
            this.infoGroupBox.Controls.Add(this.nameTextBox);
            this.infoGroupBox.Controls.Add(this.nameWazaTextBox);
            this.infoGroupBox.Controls.Add(this.label1);
            this.infoGroupBox.ForeColor = System.Drawing.Color.White;
            this.infoGroupBox.Location = new System.Drawing.Point(15, 19);
            this.infoGroupBox.Name = "infoGroupBox";
            this.infoGroupBox.Size = new System.Drawing.Size(468, 70);
            this.infoGroupBox.TabIndex = 266;
            this.infoGroupBox.TabStop = false;
            this.infoGroupBox.Text = "Name";
            // 
            // numberFlatNumericUpDown
            // 
            this.numberFlatNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.numberFlatNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.numberFlatNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numberFlatNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.numberFlatNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numberFlatNumericUpDown.Location = new System.Drawing.Point(307, 38);
            this.numberFlatNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numberFlatNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.numberFlatNumericUpDown.Name = "numberFlatNumericUpDown";
            this.numberFlatNumericUpDown.Size = new System.Drawing.Size(152, 20);
            this.numberFlatNumericUpDown.TabIndex = 273;
            this.numberFlatNumericUpDown.ValueChanged += new System.EventHandler(this.NumberFlatNumericUpDown_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(232, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 13);
            this.label14.TabIndex = 272;
            this.label14.Text = "Number";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(12, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 13);
            this.label13.TabIndex = 61;
            this.label13.Text = "ID";
            // 
            // idTextBox
            // 
            this.idTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.idTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.idTextBox.Enabled = false;
            this.idTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.idTextBox.Location = new System.Drawing.Point(62, 45);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(152, 13);
            this.idTextBox.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 57;
            this.label5.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nameTextBox.Location = new System.Drawing.Point(62, 19);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(152, 13);
            this.nameTextBox.TabIndex = 58;
            this.nameTextBox.Click += new System.EventHandler(this.NameTextBox_Click);
            // 
            // nameWazaTextBox
            // 
            this.nameWazaTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.nameWazaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameWazaTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nameWazaTextBox.Location = new System.Drawing.Point(307, 19);
            this.nameWazaTextBox.Name = "nameWazaTextBox";
            this.nameWazaTextBox.ReadOnly = true;
            this.nameWazaTextBox.Size = new System.Drawing.Size(152, 13);
            this.nameWazaTextBox.TabIndex = 60;
            this.nameWazaTextBox.Click += new System.EventHandler(this.NameWazaTextBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(232, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Name (waza)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(18, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "Type";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.growFlatComboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.typeFlatComboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(301, 206);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 80);
            this.groupBox1.TabIndex = 267;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Evolution";
            // 
            // growFlatComboBox
            // 
            this.growFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.growFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.growFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.growFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.growFlatComboBox.FormattingEnabled = true;
            this.growFlatComboBox.Items.AddRange(new object[] {
            "No Grow",
            "Fast",
            "Medium",
            "Long"});
            this.growFlatComboBox.Location = new System.Drawing.Point(68, 46);
            this.growFlatComboBox.Name = "growFlatComboBox";
            this.growFlatComboBox.Size = new System.Drawing.Size(97, 21);
            this.growFlatComboBox.TabIndex = 67;
            this.growFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.GrowFlatComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(18, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "Grow";
            // 
            // typeFlatComboBox
            // 
            this.typeFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.typeFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.typeFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.typeFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.typeFlatComboBox.FormattingEnabled = true;
            this.typeFlatComboBox.Items.AddRange(new object[] {
            "No evolution",
            "3X",
            "3Z",
            "3Y",
            "A",
            "S",
            "Z",
            "NX"});
            this.typeFlatComboBox.Location = new System.Drawing.Point(68, 19);
            this.typeFlatComboBox.Name = "typeFlatComboBox";
            this.typeFlatComboBox.Size = new System.Drawing.Size(97, 21);
            this.typeFlatComboBox.TabIndex = 65;
            this.typeFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.TypeFlatComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.moveTypeRadioButton2);
            this.groupBox2.Controls.Add(this.partnerFlatNumericUpDown);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.moveTypeRadioButton1);
            this.groupBox2.Controls.Add(this.effectFlatComboBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.elementFlatComboBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.positonFlatComboBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.skillEffectFlatComboBox);
            this.groupBox2.Controls.Add(this.effectFlatNumericUpDown1);
            this.groupBox2.Controls.Add(this.boostPanel);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(15, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(468, 105);
            this.groupBox2.TabIndex = 268;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(25, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 274;
            this.label15.Text = "Type";
            // 
            // moveTypeRadioButton2
            // 
            this.moveTypeRadioButton2.AutoSize = true;
            this.moveTypeRadioButton2.Location = new System.Drawing.Point(133, 19);
            this.moveTypeRadioButton2.Name = "moveTypeRadioButton2";
            this.moveTypeRadioButton2.Size = new System.Drawing.Size(88, 17);
            this.moveTypeRadioButton2.TabIndex = 273;
            this.moveTypeRadioButton2.TabStop = true;
            this.moveTypeRadioButton2.Text = "Fighting Spirit";
            this.moveTypeRadioButton2.UseVisualStyleBackColor = true;
            this.moveTypeRadioButton2.CheckedChanged += new System.EventHandler(this.MoveTypeRadioButton2_CheckedChanged);
            // 
            // partnerFlatNumericUpDown
            // 
            this.partnerFlatNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.partnerFlatNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.partnerFlatNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.partnerFlatNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.partnerFlatNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.partnerFlatNumericUpDown.Location = new System.Drawing.Point(314, 73);
            this.partnerFlatNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.partnerFlatNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.partnerFlatNumericUpDown.Name = "partnerFlatNumericUpDown";
            this.partnerFlatNumericUpDown.Size = new System.Drawing.Size(130, 20);
            this.partnerFlatNumericUpDown.TabIndex = 271;
            this.partnerFlatNumericUpDown.ValueChanged += new System.EventHandler(this.PartnerFlatNumericUpDown_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(264, 76);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 270;
            this.label12.Text = "Partner";
            // 
            // moveTypeRadioButton1
            // 
            this.moveTypeRadioButton1.AutoSize = true;
            this.moveTypeRadioButton1.Location = new System.Drawing.Point(75, 19);
            this.moveTypeRadioButton1.Name = "moveTypeRadioButton1";
            this.moveTypeRadioButton1.Size = new System.Drawing.Size(52, 17);
            this.moveTypeRadioButton1.TabIndex = 272;
            this.moveTypeRadioButton1.TabStop = true;
            this.moveTypeRadioButton1.Text = "Move";
            this.moveTypeRadioButton1.UseVisualStyleBackColor = true;
            this.moveTypeRadioButton1.CheckedChanged += new System.EventHandler(this.MoveTypeRadioButton1_CheckedChanged);
            // 
            // effectFlatComboBox
            // 
            this.effectFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.effectFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.effectFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.effectFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.effectFlatComboBox.FormattingEnabled = true;
            this.effectFlatComboBox.Items.AddRange(new object[] {
            "No Effect",
            "Save",
            "Punch",
            "Punch +",
            "Long Shoot",
            "Shoot Chain",
            "Shoot Block",
            "Unused",
            "Punch ++"});
            this.effectFlatComboBox.Location = new System.Drawing.Point(314, 40);
            this.effectFlatComboBox.Name = "effectFlatComboBox";
            this.effectFlatComboBox.Size = new System.Drawing.Size(130, 21);
            this.effectFlatComboBox.TabIndex = 69;
            this.effectFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.EffectFlatComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(264, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Effect";
            // 
            // elementFlatComboBox
            // 
            this.elementFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.elementFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.elementFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.elementFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.elementFlatComboBox.FormattingEnabled = true;
            this.elementFlatComboBox.Items.AddRange(new object[] {
            "Wind",
            "Wood",
            "Fire",
            "Earth",
            "Void"});
            this.elementFlatComboBox.Location = new System.Drawing.Point(75, 73);
            this.elementFlatComboBox.Name = "elementFlatComboBox";
            this.elementFlatComboBox.Size = new System.Drawing.Size(139, 21);
            this.elementFlatComboBox.TabIndex = 67;
            this.elementFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.ElementFlatComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(25, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "Element";
            // 
            // positonFlatComboBox
            // 
            this.positonFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.positonFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.positonFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.positonFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.positonFlatComboBox.FormattingEnabled = true;
            this.positonFlatComboBox.Items.AddRange(new object[] {
            "Shoot",
            "Dribble",
            "Block",
            "Save",
            "Skill"});
            this.positonFlatComboBox.Location = new System.Drawing.Point(75, 46);
            this.positonFlatComboBox.Name = "positonFlatComboBox";
            this.positonFlatComboBox.Size = new System.Drawing.Size(139, 21);
            this.positonFlatComboBox.TabIndex = 65;
            this.positonFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.PositonFlatComboBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(25, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "Position";
            // 
            // skillEffectFlatComboBox
            // 
            this.skillEffectFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.skillEffectFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.skillEffectFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.skillEffectFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.skillEffectFlatComboBox.FormattingEnabled = true;
            this.skillEffectFlatComboBox.Items.AddRange(new object[] {
            "Catch Boost",
            "Block Boost",
            "Dribble Boost",
            "Kick Boost",
            "Speed Boost",
            "Luck Boost",
            "Technique Boost",
            "Effect 8",
            "TP Boost",
            "FP Boost",
            "Economy!",
            "Big Move!",
            "Put Your Back Into It!",
            "Critical",
            "Jinx",
            "Recovery",
            "Study",
            "Slack Off",
            "Friendship",
            "Prestige",
            "Power Element",
            "Null Element",
            "Charm Up!",
            "Cool Up!",
            "Trickery!",
            "Fair Play",
            "Helping Hand!",
            "Burning Force",
            "Destructive Aura",
            "Veil of Protection",
            "Goddess\'s Grace",
            "Warrior\'s Cry",
            "Feet Be Nimble",
            "Stage Presence",
            "Never Give Up",
            "Everyone Move It!",
            "Cheerleader Backup",
            "Anti-Slip",
            "Anti-Cyclone",
            "Effect 40",
            "Technician",
            "Swiftest Wings",
            "Sacred Blade",
            "Close Them Down!",
            "Effect 45",
            "Effect 46",
            "Effect 47",
            "Block Force",
            "Dribble Force",
            "Kick Force",
            "Speed Force",
            "Luck Force",
            "Technique Force",
            "Stamina Force",
            "Effect 55",
            "Burning Force (2)",
            "Effect 57",
            "Wind\'s Call",
            "Wood\'s Call",
            "Fire\'s Call",
            "Earth\'s Call",
            "Nobody\'s Call"});
            this.skillEffectFlatComboBox.Location = new System.Drawing.Point(75, 72);
            this.skillEffectFlatComboBox.Name = "skillEffectFlatComboBox";
            this.skillEffectFlatComboBox.Size = new System.Drawing.Size(139, 21);
            this.skillEffectFlatComboBox.TabIndex = 275;
            this.skillEffectFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.SkillEffectFlatComboBox_SelectedIndexChanged);
            // 
            // effectFlatNumericUpDown1
            // 
            this.effectFlatNumericUpDown1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.effectFlatNumericUpDown1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.effectFlatNumericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.effectFlatNumericUpDown1.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.effectFlatNumericUpDown1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.effectFlatNumericUpDown1.Location = new System.Drawing.Point(314, 41);
            this.effectFlatNumericUpDown1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.effectFlatNumericUpDown1.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.effectFlatNumericUpDown1.Name = "effectFlatNumericUpDown1";
            this.effectFlatNumericUpDown1.Size = new System.Drawing.Size(130, 20);
            this.effectFlatNumericUpDown1.TabIndex = 276;
            this.effectFlatNumericUpDown1.ValueChanged += new System.EventHandler(this.EffectFlatNumericUpDown1_ValueChanged);
            // 
            // boostPanel
            // 
            this.boostPanel.Controls.Add(this.boostActiveRadioButton2);
            this.boostPanel.Controls.Add(this.boostActiveRadioButton1);
            this.boostPanel.Location = new System.Drawing.Point(314, 71);
            this.boostPanel.Name = "boostPanel";
            this.boostPanel.Size = new System.Drawing.Size(130, 29);
            this.boostPanel.TabIndex = 279;
            // 
            // boostActiveRadioButton2
            // 
            this.boostActiveRadioButton2.AutoSize = true;
            this.boostActiveRadioButton2.Location = new System.Drawing.Point(64, 3);
            this.boostActiveRadioButton2.Name = "boostActiveRadioButton2";
            this.boostActiveRadioButton2.Size = new System.Drawing.Size(52, 17);
            this.boostActiveRadioButton2.TabIndex = 278;
            this.boostActiveRadioButton2.TabStop = true;
            this.boostActiveRadioButton2.Text = "Team";
            this.boostActiveRadioButton2.UseVisualStyleBackColor = true;
            this.boostActiveRadioButton2.CheckedChanged += new System.EventHandler(this.BoostActiveRadioButton2_CheckedChanged);
            // 
            // boostActiveRadioButton1
            // 
            this.boostActiveRadioButton1.AutoSize = true;
            this.boostActiveRadioButton1.Location = new System.Drawing.Point(6, 3);
            this.boostActiveRadioButton1.Name = "boostActiveRadioButton1";
            this.boostActiveRadioButton1.Size = new System.Drawing.Size(43, 17);
            this.boostActiveRadioButton1.TabIndex = 277;
            this.boostActiveRadioButton1.TabStop = true;
            this.boostActiveRadioButton1.Text = "Self";
            this.boostActiveRadioButton1.UseVisualStyleBackColor = true;
            this.boostActiveRadioButton1.CheckedChanged += new System.EventHandler(this.BoostActiveRadioButton1_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.techniqueFlatNumericUpDown);
            this.groupBox3.Controls.Add(this.faultRateFlatNumericUpDown);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.powerFlatNumericUpDown);
            this.groupBox3.Controls.Add(this.tpCostFlatNumericUpDown);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(15, 206);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 80);
            this.groupBox3.TabIndex = 269;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Stats";
            // 
            // techniqueFlatNumericUpDown
            // 
            this.techniqueFlatNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.techniqueFlatNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.techniqueFlatNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.techniqueFlatNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.techniqueFlatNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.techniqueFlatNumericUpDown.Location = new System.Drawing.Point(202, 47);
            this.techniqueFlatNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.techniqueFlatNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.techniqueFlatNumericUpDown.Name = "techniqueFlatNumericUpDown";
            this.techniqueFlatNumericUpDown.Size = new System.Drawing.Size(66, 20);
            this.techniqueFlatNumericUpDown.TabIndex = 270;
            this.techniqueFlatNumericUpDown.ValueChanged += new System.EventHandler(this.TechniqueFlatNumericUpDown_ValueChanged);
            // 
            // faultRateFlatNumericUpDown
            // 
            this.faultRateFlatNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.faultRateFlatNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.faultRateFlatNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.faultRateFlatNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.faultRateFlatNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.faultRateFlatNumericUpDown.Location = new System.Drawing.Point(202, 19);
            this.faultRateFlatNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.faultRateFlatNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.faultRateFlatNumericUpDown.Name = "faultRateFlatNumericUpDown";
            this.faultRateFlatNumericUpDown.Size = new System.Drawing.Size(66, 20);
            this.faultRateFlatNumericUpDown.TabIndex = 269;
            this.faultRateFlatNumericUpDown.ValueChanged += new System.EventHandler(this.FaultRateFlatNumericUpDown_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(139, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 268;
            this.label10.Text = "Technique";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(139, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 267;
            this.label11.Text = "Fault";
            // 
            // powerFlatNumericUpDown
            // 
            this.powerFlatNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.powerFlatNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.powerFlatNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.powerFlatNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.powerFlatNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.powerFlatNumericUpDown.Location = new System.Drawing.Point(61, 47);
            this.powerFlatNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.powerFlatNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.powerFlatNumericUpDown.Name = "powerFlatNumericUpDown";
            this.powerFlatNumericUpDown.Size = new System.Drawing.Size(66, 20);
            this.powerFlatNumericUpDown.TabIndex = 266;
            this.powerFlatNumericUpDown.ValueChanged += new System.EventHandler(this.PowerFlatNumericUpDown_ValueChanged);
            // 
            // tpCostFlatNumericUpDown
            // 
            this.tpCostFlatNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.tpCostFlatNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tpCostFlatNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpCostFlatNumericUpDown.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.tpCostFlatNumericUpDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.tpCostFlatNumericUpDown.Location = new System.Drawing.Point(61, 20);
            this.tpCostFlatNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.tpCostFlatNumericUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.tpCostFlatNumericUpDown.Name = "tpCostFlatNumericUpDown";
            this.tpCostFlatNumericUpDown.Size = new System.Drawing.Size(66, 20);
            this.tpCostFlatNumericUpDown.TabIndex = 265;
            this.tpCostFlatNumericUpDown.ValueChanged += new System.EventHandler(this.TpCostFlatNumericUpDown_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(11, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 66;
            this.label8.Text = "Power";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(11, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 64;
            this.label9.Text = "TP";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.descriptionTextBox.Location = new System.Drawing.Point(22, 19);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.Size = new System.Drawing.Size(429, 34);
            this.descriptionTextBox.TabIndex = 270;
            this.descriptionTextBox.Click += new System.EventHandler(this.DescriptionTextBox_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.descriptionTextBox);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(15, 292);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(468, 67);
            this.groupBox4.TabIndex = 271;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Description";
            // 
            // skillGroupBox
            // 
            this.skillGroupBox.Controls.Add(this.infoGroupBox);
            this.skillGroupBox.Controls.Add(this.groupBox4);
            this.skillGroupBox.Controls.Add(this.groupBox2);
            this.skillGroupBox.Controls.Add(this.groupBox1);
            this.skillGroupBox.Controls.Add(this.groupBox3);
            this.skillGroupBox.Enabled = false;
            this.skillGroupBox.ForeColor = System.Drawing.Color.White;
            this.skillGroupBox.Location = new System.Drawing.Point(261, 34);
            this.skillGroupBox.Name = "skillGroupBox";
            this.skillGroupBox.Size = new System.Drawing.Size(496, 375);
            this.skillGroupBox.TabIndex = 272;
            this.skillGroupBox.TabStop = false;
            this.skillGroupBox.Text = "Skill";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 48);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // SkillWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(770, 424);
            this.Controls.Add(this.skillGroupBox);
            this.Controls.Add(this.skillTreeView);
            this.Controls.Add(this.searchTextBox);
            this.Name = "SkillWindow";
            this.Text = "SkillWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SkillWindow_FormClosed);
            this.Shown += new System.EventHandler(this.SkillWindow_Shown);
            this.infoGroupBox.ResumeLayout(false);
            this.infoGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberFlatNumericUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.partnerFlatNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectFlatNumericUpDown1)).EndInit();
            this.boostPanel.ResumeLayout(false);
            this.boostPanel.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.techniqueFlatNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faultRateFlatNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerFlatNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpCostFlatNumericUpDown)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.skillGroupBox.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView skillTreeView;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.GroupBox infoGroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox nameWazaTextBox;
        private System.Windows.Forms.Label label1;
        private UI.FlatComboBox typeFlatComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private UI.FlatComboBox growFlatComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private UI.FlatComboBox elementFlatComboBox;
        private System.Windows.Forms.Label label6;
        private UI.FlatComboBox positonFlatComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private UI.FlatNumericUpDown techniqueFlatNumericUpDown;
        private UI.FlatNumericUpDown faultRateFlatNumericUpDown;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private UI.FlatNumericUpDown powerFlatNumericUpDown;
        private UI.FlatNumericUpDown tpCostFlatNumericUpDown;
        private UI.FlatNumericUpDown partnerFlatNumericUpDown;
        private System.Windows.Forms.Label label12;
        private UI.FlatComboBox effectFlatComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox skillGroupBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox idTextBox;
        private UI.FlatNumericUpDown numberFlatNumericUpDown;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton moveTypeRadioButton2;
        private System.Windows.Forms.RadioButton moveTypeRadioButton1;
        private UI.FlatComboBox skillEffectFlatComboBox;
        private UI.FlatNumericUpDown effectFlatNumericUpDown1;
        private System.Windows.Forms.RadioButton boostActiveRadioButton2;
        private System.Windows.Forms.RadioButton boostActiveRadioButton1;
        private System.Windows.Forms.Panel boostPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}