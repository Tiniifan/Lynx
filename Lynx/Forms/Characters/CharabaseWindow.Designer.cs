
namespace Lynx.Forms.Characters
{
    partial class CharabaseWindow
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
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.characterListBox = new System.Windows.Forms.ListBox();
            this.characterContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.characterGroupBox = new System.Windows.Forms.GroupBox();
            this.hashTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.modelTypeflatComboBox = new Lynx.UI.FlatComboBox();
            this.colorPictureBox = new System.Windows.Forms.PictureBox();
            this.genderFlatComboBox = new Lynx.UI.FlatComboBox();
            this.skinFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.yearFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bodyFlatComboBox = new Lynx.UI.FlatComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.nicknameTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.fullNameTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.facePictureBox = new System.Windows.Forms.PictureBox();
            this.modelNPCFlatComboBox = new Lynx.UI.FlatComboBox();
            this.modelPlayerFlatComboBox = new Lynx.UI.FlatComboBox();
            this.modelNPCOtherFlatComboBox = new Lynx.UI.FlatComboBox();
            this.characterContextMenuStrip.SuspendLayout();
            this.characterGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.facePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // searchTextBox
            // 
            this.searchTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchTextBox.Location = new System.Drawing.Point(12, 12);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(185, 13);
            this.searchTextBox.TabIndex = 2;
            this.searchTextBox.Text = "Search...";
            this.searchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            this.searchTextBox.MouseEnter += new System.EventHandler(this.SearchTextBox_MouseEnter);
            this.searchTextBox.MouseLeave += new System.EventHandler(this.SearchTextBox_MouseLeave);
            // 
            // characterListBox
            // 
            this.characterListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.characterListBox.ContextMenuStrip = this.characterContextMenuStrip;
            this.characterListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.characterListBox.FormattingEnabled = true;
            this.characterListBox.Location = new System.Drawing.Point(12, 38);
            this.characterListBox.Name = "characterListBox";
            this.characterListBox.Size = new System.Drawing.Size(185, 277);
            this.characterListBox.TabIndex = 3;
            this.characterListBox.SelectedIndexChanged += new System.EventHandler(this.CharacterListBox_SelectedIndexChanged);
            this.characterListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CharacterListBox_MouseUp);
            // 
            // characterContextMenuStrip
            // 
            this.characterContextMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.characterContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.characterContextMenuStrip.Name = "characterContextMenuStrip";
            this.characterContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.characterContextMenuStrip.Size = new System.Drawing.Size(108, 48);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.insertToolStripMenuItem.Text = "Insert";
            this.insertToolStripMenuItem.Click += new System.EventHandler(this.InsertToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // characterGroupBox
            // 
            this.characterGroupBox.Controls.Add(this.hashTextBox);
            this.characterGroupBox.Controls.Add(this.label5);
            this.characterGroupBox.Controls.Add(this.modelTypeflatComboBox);
            this.characterGroupBox.Controls.Add(this.colorPictureBox);
            this.characterGroupBox.Controls.Add(this.genderFlatComboBox);
            this.characterGroupBox.Controls.Add(this.skinFlatComboBox);
            this.characterGroupBox.Controls.Add(this.label4);
            this.characterGroupBox.Controls.Add(this.yearFlatComboBox);
            this.characterGroupBox.Controls.Add(this.label3);
            this.characterGroupBox.Controls.Add(this.bodyFlatComboBox);
            this.characterGroupBox.Controls.Add(this.label2);
            this.characterGroupBox.Controls.Add(this.label1);
            this.characterGroupBox.Controls.Add(this.descriptionTextBox);
            this.characterGroupBox.Controls.Add(this.nicknameTextBox);
            this.characterGroupBox.Controls.Add(this.label14);
            this.characterGroupBox.Controls.Add(this.label13);
            this.characterGroupBox.Controls.Add(this.fullNameTextBox);
            this.characterGroupBox.Controls.Add(this.label12);
            this.characterGroupBox.Controls.Add(this.facePictureBox);
            this.characterGroupBox.Controls.Add(this.modelNPCFlatComboBox);
            this.characterGroupBox.Controls.Add(this.modelPlayerFlatComboBox);
            this.characterGroupBox.Controls.Add(this.modelNPCOtherFlatComboBox);
            this.characterGroupBox.Enabled = false;
            this.characterGroupBox.ForeColor = System.Drawing.Color.White;
            this.characterGroupBox.Location = new System.Drawing.Point(203, 32);
            this.characterGroupBox.Name = "characterGroupBox";
            this.characterGroupBox.Size = new System.Drawing.Size(341, 285);
            this.characterGroupBox.TabIndex = 4;
            this.characterGroupBox.TabStop = false;
            this.characterGroupBox.Text = "Character";
            // 
            // hashTextBox
            // 
            this.hashTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.hashTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hashTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.hashTextBox.Location = new System.Drawing.Point(162, 40);
            this.hashTextBox.Name = "hashTextBox";
            this.hashTextBox.ReadOnly = true;
            this.hashTextBox.Size = new System.Drawing.Size(152, 13);
            this.hashTextBox.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Hash";
            // 
            // modelTypeflatComboBox
            // 
            this.modelTypeflatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.modelTypeflatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.modelTypeflatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.modelTypeflatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.modelTypeflatComboBox.FormattingEnabled = true;
            this.modelTypeflatComboBox.Location = new System.Drawing.Point(210, 144);
            this.modelTypeflatComboBox.Name = "modelTypeflatComboBox";
            this.modelTypeflatComboBox.Size = new System.Drawing.Size(104, 21);
            this.modelTypeflatComboBox.TabIndex = 45;
            this.modelTypeflatComboBox.SelectedIndexChanged += new System.EventHandler(this.ModelTypeflatComboBox_SelectedIndexChanged);
            // 
            // colorPictureBox
            // 
            this.colorPictureBox.Location = new System.Drawing.Point(319, 177);
            this.colorPictureBox.Name = "colorPictureBox";
            this.colorPictureBox.Size = new System.Drawing.Size(16, 16);
            this.colorPictureBox.TabIndex = 44;
            this.colorPictureBox.TabStop = false;
            // 
            // genderFlatComboBox
            // 
            this.genderFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.genderFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.genderFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.genderFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.genderFlatComboBox.FormattingEnabled = true;
            this.genderFlatComboBox.Location = new System.Drawing.Point(210, 203);
            this.genderFlatComboBox.Name = "genderFlatComboBox";
            this.genderFlatComboBox.Size = new System.Drawing.Size(104, 21);
            this.genderFlatComboBox.TabIndex = 43;
            this.genderFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.GenderFlatComboBox_SelectedIndexChanged);
            // 
            // skinFlatComboBox
            // 
            this.skinFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.skinFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.skinFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.skinFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.skinFlatComboBox.FormattingEnabled = true;
            this.skinFlatComboBox.Location = new System.Drawing.Point(93, 174);
            this.skinFlatComboBox.Name = "skinFlatComboBox";
            this.skinFlatComboBox.Size = new System.Drawing.Size(221, 21);
            this.skinFlatComboBox.TabIndex = 42;
            this.skinFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.SkinFlatComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Skin";
            // 
            // yearFlatComboBox
            // 
            this.yearFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.yearFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.yearFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.yearFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.yearFlatComboBox.FormattingEnabled = true;
            this.yearFlatComboBox.Location = new System.Drawing.Point(93, 115);
            this.yearFlatComboBox.Name = "yearFlatComboBox";
            this.yearFlatComboBox.Size = new System.Drawing.Size(221, 21);
            this.yearFlatComboBox.TabIndex = 40;
            this.yearFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.YearFlatComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Year";
            // 
            // bodyFlatComboBox
            // 
            this.bodyFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.bodyFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.bodyFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.bodyFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.bodyFlatComboBox.FormattingEnabled = true;
            this.bodyFlatComboBox.Location = new System.Drawing.Point(93, 203);
            this.bodyFlatComboBox.Name = "bodyFlatComboBox";
            this.bodyFlatComboBox.Size = new System.Drawing.Size(104, 21);
            this.bodyFlatComboBox.TabIndex = 38;
            this.bodyFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.BodyFlatComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Body Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Model";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.descriptionTextBox.Location = new System.Drawing.Point(93, 232);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.Size = new System.Drawing.Size(221, 34);
            this.descriptionTextBox.TabIndex = 34;
            this.descriptionTextBox.Click += new System.EventHandler(this.DescriptionTextBox_Click);
            // 
            // nicknameTextBox
            // 
            this.nicknameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.nicknameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nicknameTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nicknameTextBox.Location = new System.Drawing.Point(162, 78);
            this.nicknameTextBox.Name = "nicknameTextBox";
            this.nicknameTextBox.ReadOnly = true;
            this.nicknameTextBox.Size = new System.Drawing.Size(152, 13);
            this.nicknameTextBox.TabIndex = 33;
            this.nicknameTextBox.Click += new System.EventHandler(this.NicknameTextBox_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(96, 78);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "Nickname";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 242);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "Palpack Des.";
            // 
            // fullNameTextBox
            // 
            this.fullNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.fullNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fullNameTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.fullNameTextBox.Location = new System.Drawing.Point(162, 59);
            this.fullNameTextBox.Name = "fullNameTextBox";
            this.fullNameTextBox.ReadOnly = true;
            this.fullNameTextBox.Size = new System.Drawing.Size(152, 13);
            this.fullNameTextBox.TabIndex = 30;
            this.fullNameTextBox.Click += new System.EventHandler(this.FullNameTextBox_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(96, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Fullname";
            // 
            // facePictureBox
            // 
            this.facePictureBox.Location = new System.Drawing.Point(20, 31);
            this.facePictureBox.Name = "facePictureBox";
            this.facePictureBox.Size = new System.Drawing.Size(64, 64);
            this.facePictureBox.TabIndex = 28;
            this.facePictureBox.TabStop = false;
            // 
            // modelNPCFlatComboBox
            // 
            this.modelNPCFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.modelNPCFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.modelNPCFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.modelNPCFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.modelNPCFlatComboBox.FormattingEnabled = true;
            this.modelNPCFlatComboBox.Location = new System.Drawing.Point(93, 144);
            this.modelNPCFlatComboBox.Name = "modelNPCFlatComboBox";
            this.modelNPCFlatComboBox.Size = new System.Drawing.Size(104, 21);
            this.modelNPCFlatComboBox.TabIndex = 36;
            this.modelNPCFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.ModelNPCFlatComboBox_SelectedIndexChanged);
            // 
            // modelPlayerFlatComboBox
            // 
            this.modelPlayerFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.modelPlayerFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.modelPlayerFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.modelPlayerFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.modelPlayerFlatComboBox.FormattingEnabled = true;
            this.modelPlayerFlatComboBox.Location = new System.Drawing.Point(93, 144);
            this.modelPlayerFlatComboBox.Name = "modelPlayerFlatComboBox";
            this.modelPlayerFlatComboBox.Size = new System.Drawing.Size(104, 21);
            this.modelPlayerFlatComboBox.TabIndex = 46;
            this.modelPlayerFlatComboBox.Visible = false;
            this.modelPlayerFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.ModelPlayerFlatComboBox_SelectedIndexChanged);
            // 
            // modelNPCOtherFlatComboBox
            // 
            this.modelNPCOtherFlatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.modelNPCOtherFlatComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.modelNPCOtherFlatComboBox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.modelNPCOtherFlatComboBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.modelNPCOtherFlatComboBox.FormattingEnabled = true;
            this.modelNPCOtherFlatComboBox.Location = new System.Drawing.Point(93, 144);
            this.modelNPCOtherFlatComboBox.Name = "modelNPCOtherFlatComboBox";
            this.modelNPCOtherFlatComboBox.Size = new System.Drawing.Size(104, 21);
            this.modelNPCOtherFlatComboBox.TabIndex = 37;
            this.modelNPCOtherFlatComboBox.Visible = false;
            this.modelNPCOtherFlatComboBox.SelectedIndexChanged += new System.EventHandler(this.ModelNPCOtherFlatComboBox_SelectedIndexChanged);
            // 
            // CharabaseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(558, 328);
            this.Controls.Add(this.characterGroupBox);
            this.Controls.Add(this.characterListBox);
            this.Controls.Add(this.searchTextBox);
            this.Name = "CharabaseWindow";
            this.Text = "CharabaseWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CharabaseWindow_FormClosed);
            this.Load += new System.EventHandler(this.CharabaseWindow_Load);
            this.Shown += new System.EventHandler(this.CharabaseWindow_Shown);
            this.characterContextMenuStrip.ResumeLayout(false);
            this.characterGroupBox.ResumeLayout(false);
            this.characterGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.facePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.ListBox characterListBox;
        private System.Windows.Forms.GroupBox characterGroupBox;
        private System.Windows.Forms.TextBox nicknameTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox fullNameTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox facePictureBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private UI.FlatComboBox modelNPCFlatComboBox;
        private System.Windows.Forms.Label label1;
        private UI.FlatComboBox bodyFlatComboBox;
        private System.Windows.Forms.Label label2;
        private UI.FlatComboBox yearFlatComboBox;
        private System.Windows.Forms.Label label3;
        private UI.FlatComboBox skinFlatComboBox;
        private System.Windows.Forms.Label label4;
        private UI.FlatComboBox genderFlatComboBox;
        private System.Windows.Forms.PictureBox colorPictureBox;
        private UI.FlatComboBox modelTypeflatComboBox;
        private UI.FlatComboBox modelPlayerFlatComboBox;
        private UI.FlatComboBox modelNPCOtherFlatComboBox;
        private System.Windows.Forms.TextBox hashTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip characterContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}