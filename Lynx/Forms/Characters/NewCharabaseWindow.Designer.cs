
namespace Lynx.Forms.Characters
{
    partial class NewCharabaseWindow
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
            this.confirmButton = new System.Windows.Forms.Button();
            this.charaTypeVSTabControl = new Lynx.UI.VSTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.playerListBox = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.npcListBox = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.npcOtherListBox = new System.Windows.Forms.ListBox();
            this.charaTypeVSTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // confirmButton
            // 
            this.confirmButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirmButton.ForeColor = System.Drawing.Color.White;
            this.confirmButton.Location = new System.Drawing.Point(68, 449);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(130, 23);
            this.confirmButton.TabIndex = 3;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // charaTypeVSTabControl
            // 
            this.charaTypeVSTabControl.ActiveIndicator = System.Drawing.Color.White;
            this.charaTypeVSTabControl.ActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.charaTypeVSTabControl.ActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.charaTypeVSTabControl.Background = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.charaTypeVSTabControl.BackgroundTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.charaTypeVSTabControl.Border = System.Drawing.Color.White;
            this.charaTypeVSTabControl.Controls.Add(this.tabPage1);
            this.charaTypeVSTabControl.Controls.Add(this.tabPage2);
            this.charaTypeVSTabControl.Controls.Add(this.tabPage3);
            this.charaTypeVSTabControl.Divider = System.Drawing.Color.White;
            this.charaTypeVSTabControl.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
            this.charaTypeVSTabControl.InActiveIndicator = System.Drawing.Color.White;
            this.charaTypeVSTabControl.InActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.charaTypeVSTabControl.InActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.charaTypeVSTabControl.Location = new System.Drawing.Point(9, 9);
            this.charaTypeVSTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.charaTypeVSTabControl.Name = "charaTypeVSTabControl";
            this.charaTypeVSTabControl.Padding = new System.Drawing.Point(0, 0);
            this.charaTypeVSTabControl.SelectedIndex = 0;
            this.charaTypeVSTabControl.Size = new System.Drawing.Size(263, 432);
            this.charaTypeVSTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tabPage1.Controls.Add(this.playerListBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(255, 403);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Player";
            // 
            // playerListBox
            // 
            this.playerListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.playerListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.playerListBox.FormattingEnabled = true;
            this.playerListBox.Location = new System.Drawing.Point(0, 4);
            this.playerListBox.Name = "playerListBox";
            this.playerListBox.Size = new System.Drawing.Size(255, 394);
            this.playerListBox.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tabPage2.Controls.Add(this.npcListBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(255, 403);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "NPC";
            // 
            // npcListBox
            // 
            this.npcListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.npcListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.npcListBox.FormattingEnabled = true;
            this.npcListBox.Location = new System.Drawing.Point(0, 4);
            this.npcListBox.Name = "npcListBox";
            this.npcListBox.Size = new System.Drawing.Size(255, 394);
            this.npcListBox.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tabPage3.Controls.Add(this.npcOtherListBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(255, 403);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "NPCOther";
            // 
            // npcOtherListBox
            // 
            this.npcOtherListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.npcOtherListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.npcOtherListBox.FormattingEnabled = true;
            this.npcOtherListBox.Location = new System.Drawing.Point(0, 4);
            this.npcOtherListBox.Name = "npcOtherListBox";
            this.npcOtherListBox.Size = new System.Drawing.Size(255, 394);
            this.npcOtherListBox.TabIndex = 5;
            // 
            // NewCharabaseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(284, 484);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.charaTypeVSTabControl);
            this.Name = "NewCharabaseWindow";
            this.Text = "NewCharabaseWindow";
            this.Load += new System.EventHandler(this.NewCharabaseWindow_Load);
            this.charaTypeVSTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.VSTabControl charaTypeVSTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox playerListBox;
        private System.Windows.Forms.ListBox npcListBox;
        private System.Windows.Forms.ListBox npcOtherListBox;
        private System.Windows.Forms.Button confirmButton;
    }
}