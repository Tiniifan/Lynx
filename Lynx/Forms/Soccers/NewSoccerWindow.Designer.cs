namespace Lynx.Forms.Soccers
{
    partial class NewSoccerWindow
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
            this.configTabPage = new System.Windows.Forms.TabPage();
            this.configVsTabControl = new Lynx.UI.VSTabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.storyConfigListBox = new System.Windows.Forms.ListBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.encounterConfigListBox = new System.Windows.Forms.ListBox();
            this.paramTabPage = new System.Windows.Forms.TabPage();
            this.paramVsTabControl = new Lynx.UI.VSTabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.storyParamListBox = new System.Windows.Forms.ListBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.encounterParamListBox = new System.Windows.Forms.ListBox();
            this.soccerTabPage = new System.Windows.Forms.TabPage();
            this.soccerListBox = new System.Windows.Forms.ListBox();
            this.charaTypeVSTabControl.SuspendLayout();
            this.configTabPage.SuspendLayout();
            this.configVsTabControl.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.paramTabPage.SuspendLayout();
            this.paramVsTabControl.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.soccerTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // confirmButton
            // 
            this.confirmButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirmButton.ForeColor = System.Drawing.Color.White;
            this.confirmButton.Location = new System.Drawing.Point(68, 486);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(130, 23);
            this.confirmButton.TabIndex = 5;
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
            this.charaTypeVSTabControl.Controls.Add(this.configTabPage);
            this.charaTypeVSTabControl.Controls.Add(this.paramTabPage);
            this.charaTypeVSTabControl.Controls.Add(this.soccerTabPage);
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
            this.charaTypeVSTabControl.Size = new System.Drawing.Size(279, 471);
            this.charaTypeVSTabControl.TabIndex = 4;
            // 
            // configTabPage
            // 
            this.configTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.configTabPage.Controls.Add(this.configVsTabControl);
            this.configTabPage.Location = new System.Drawing.Point(4, 25);
            this.configTabPage.Name = "configTabPage";
            this.configTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.configTabPage.Size = new System.Drawing.Size(271, 442);
            this.configTabPage.TabIndex = 2;
            this.configTabPage.Text = "Config";
            // 
            // configVsTabControl
            // 
            this.configVsTabControl.ActiveIndicator = System.Drawing.Color.White;
            this.configVsTabControl.ActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.configVsTabControl.ActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.configVsTabControl.Background = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.configVsTabControl.BackgroundTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.configVsTabControl.Border = System.Drawing.Color.White;
            this.configVsTabControl.Controls.Add(this.tabPage6);
            this.configVsTabControl.Controls.Add(this.tabPage7);
            this.configVsTabControl.Divider = System.Drawing.Color.White;
            this.configVsTabControl.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
            this.configVsTabControl.InActiveIndicator = System.Drawing.Color.White;
            this.configVsTabControl.InActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.configVsTabControl.InActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.configVsTabControl.Location = new System.Drawing.Point(3, 3);
            this.configVsTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.configVsTabControl.Name = "configVsTabControl";
            this.configVsTabControl.Padding = new System.Drawing.Point(0, 0);
            this.configVsTabControl.SelectedIndex = 0;
            this.configVsTabControl.Size = new System.Drawing.Size(263, 432);
            this.configVsTabControl.TabIndex = 6;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tabPage6.Controls.Add(this.storyConfigListBox);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(255, 403);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "Story";
            // 
            // storyConfigListBox
            // 
            this.storyConfigListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.storyConfigListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.storyConfigListBox.FormattingEnabled = true;
            this.storyConfigListBox.Location = new System.Drawing.Point(0, 4);
            this.storyConfigListBox.Name = "storyConfigListBox";
            this.storyConfigListBox.Size = new System.Drawing.Size(255, 394);
            this.storyConfigListBox.TabIndex = 5;
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tabPage7.Controls.Add(this.encounterConfigListBox);
            this.tabPage7.Location = new System.Drawing.Point(4, 25);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(255, 403);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "Encounter";
            // 
            // encounterConfigListBox
            // 
            this.encounterConfigListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.encounterConfigListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.encounterConfigListBox.FormattingEnabled = true;
            this.encounterConfigListBox.Location = new System.Drawing.Point(0, 4);
            this.encounterConfigListBox.Name = "encounterConfigListBox";
            this.encounterConfigListBox.Size = new System.Drawing.Size(255, 394);
            this.encounterConfigListBox.TabIndex = 5;
            // 
            // paramTabPage
            // 
            this.paramTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.paramTabPage.Controls.Add(this.paramVsTabControl);
            this.paramTabPage.Location = new System.Drawing.Point(4, 25);
            this.paramTabPage.Name = "paramTabPage";
            this.paramTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.paramTabPage.Size = new System.Drawing.Size(271, 442);
            this.paramTabPage.TabIndex = 1;
            this.paramTabPage.Text = "Param";
            // 
            // paramVsTabControl
            // 
            this.paramVsTabControl.ActiveIndicator = System.Drawing.Color.White;
            this.paramVsTabControl.ActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.paramVsTabControl.ActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.paramVsTabControl.Background = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.paramVsTabControl.BackgroundTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.paramVsTabControl.Border = System.Drawing.Color.White;
            this.paramVsTabControl.Controls.Add(this.tabPage4);
            this.paramVsTabControl.Controls.Add(this.tabPage5);
            this.paramVsTabControl.Divider = System.Drawing.Color.White;
            this.paramVsTabControl.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
            this.paramVsTabControl.InActiveIndicator = System.Drawing.Color.White;
            this.paramVsTabControl.InActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.paramVsTabControl.InActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.paramVsTabControl.Location = new System.Drawing.Point(3, 3);
            this.paramVsTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.paramVsTabControl.Name = "paramVsTabControl";
            this.paramVsTabControl.Padding = new System.Drawing.Point(0, 0);
            this.paramVsTabControl.SelectedIndex = 0;
            this.paramVsTabControl.Size = new System.Drawing.Size(263, 432);
            this.paramVsTabControl.TabIndex = 5;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tabPage4.Controls.Add(this.storyParamListBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(255, 403);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Story";
            // 
            // storyParamListBox
            // 
            this.storyParamListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.storyParamListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.storyParamListBox.FormattingEnabled = true;
            this.storyParamListBox.Location = new System.Drawing.Point(0, 4);
            this.storyParamListBox.Name = "storyParamListBox";
            this.storyParamListBox.Size = new System.Drawing.Size(255, 394);
            this.storyParamListBox.TabIndex = 5;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tabPage5.Controls.Add(this.encounterParamListBox);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(255, 403);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Encounter";
            // 
            // encounterParamListBox
            // 
            this.encounterParamListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.encounterParamListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.encounterParamListBox.FormattingEnabled = true;
            this.encounterParamListBox.Location = new System.Drawing.Point(0, 4);
            this.encounterParamListBox.Name = "encounterParamListBox";
            this.encounterParamListBox.Size = new System.Drawing.Size(255, 394);
            this.encounterParamListBox.TabIndex = 5;
            // 
            // soccerTabPage
            // 
            this.soccerTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.soccerTabPage.Controls.Add(this.soccerListBox);
            this.soccerTabPage.Location = new System.Drawing.Point(4, 25);
            this.soccerTabPage.Name = "soccerTabPage";
            this.soccerTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.soccerTabPage.Size = new System.Drawing.Size(271, 442);
            this.soccerTabPage.TabIndex = 0;
            this.soccerTabPage.Text = "Soccer";
            // 
            // soccerListBox
            // 
            this.soccerListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.soccerListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.soccerListBox.FormattingEnabled = true;
            this.soccerListBox.Location = new System.Drawing.Point(0, 4);
            this.soccerListBox.Name = "soccerListBox";
            this.soccerListBox.Size = new System.Drawing.Size(268, 433);
            this.soccerListBox.TabIndex = 5;
            // 
            // NewSoccerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(295, 517);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.charaTypeVSTabControl);
            this.Name = "NewSoccerWindow";
            this.Text = "NewSoccerWindow";
            this.charaTypeVSTabControl.ResumeLayout(false);
            this.configTabPage.ResumeLayout(false);
            this.configVsTabControl.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.paramTabPage.ResumeLayout(false);
            this.paramVsTabControl.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.soccerTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button confirmButton;
        private UI.VSTabControl charaTypeVSTabControl;
        private System.Windows.Forms.TabPage soccerTabPage;
        private System.Windows.Forms.ListBox soccerListBox;
        private System.Windows.Forms.TabPage paramTabPage;
        private System.Windows.Forms.TabPage configTabPage;
        private UI.VSTabControl paramVsTabControl;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListBox storyParamListBox;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListBox encounterParamListBox;
        private UI.VSTabControl configVsTabControl;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ListBox storyConfigListBox;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.ListBox encounterConfigListBox;
    }
}