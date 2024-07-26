
namespace Lynx.Forms.Nyanko
{
    partial class SearchWindow
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
            this.searchButton = new System.Windows.Forms.Button();
            this.searchedTextBox = new System.Windows.Forms.TextBox();
            this.foundListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.searchVSTabControl = new Lynx.UI.VSTabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.searchVSTabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(372, 8);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            // 
            // searchedTextBox
            // 
            this.searchedTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.searchedTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchedTextBox.ForeColor = System.Drawing.Color.White;
            this.searchedTextBox.Location = new System.Drawing.Point(39, 13);
            this.searchedTextBox.Name = "searchedTextBox";
            this.searchedTextBox.Size = new System.Drawing.Size(326, 15);
            this.searchedTextBox.TabIndex = 1;
            // 
            // foundListBox
            // 
            this.foundListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.foundListBox.ForeColor = System.Drawing.Color.White;
            this.foundListBox.FormattingEnabled = true;
            this.foundListBox.Location = new System.Drawing.Point(9, 41);
            this.foundListBox.Name = "foundListBox";
            this.foundListBox.Size = new System.Drawing.Size(438, 290);
            this.foundListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text";
            // 
            // searchVSTabControl
            // 
            this.searchVSTabControl.ActiveIndicator = System.Drawing.Color.White;
            this.searchVSTabControl.ActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.searchVSTabControl.ActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.searchVSTabControl.Background = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.searchVSTabControl.BackgroundTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.searchVSTabControl.Border = System.Drawing.Color.White;
            this.searchVSTabControl.Controls.Add(this.tabPage2);
            this.searchVSTabControl.Divider = System.Drawing.Color.White;
            this.searchVSTabControl.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
            this.searchVSTabControl.InActiveIndicator = System.Drawing.Color.White;
            this.searchVSTabControl.InActiveTab = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.searchVSTabControl.InActiveText = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.searchVSTabControl.Location = new System.Drawing.Point(9, 9);
            this.searchVSTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.searchVSTabControl.Name = "searchVSTabControl";
            this.searchVSTabControl.Padding = new System.Drawing.Point(0, 0);
            this.searchVSTabControl.SelectedIndex = 0;
            this.searchVSTabControl.Size = new System.Drawing.Size(464, 366);
            this.searchVSTabControl.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.tabPage2.Controls.Add(this.searchButton);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.searchedTextBox);
            this.tabPage2.Controls.Add(this.foundListBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(456, 337);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Search";
            // 
            // SearchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(482, 384);
            this.Controls.Add(this.searchVSTabControl);
            this.Name = "SearchWindow";
            this.Text = "SearchWindow";
            this.searchVSTabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchedTextBox;
        private System.Windows.Forms.ListBox foundListBox;
        private System.Windows.Forms.Label label1;
        private UI.VSTabControl searchVSTabControl;
        private System.Windows.Forms.TabPage tabPage2;
    }
}