
namespace Lynx.Forms.Characters
{
    partial class GenerateStatsWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.targetGroupBox = new System.Windows.Forms.GroupBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.resultGroupBox = new System.Windows.Forms.GroupBox();
            this.previousButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.profileLabel = new System.Windows.Forms.Label();
            this.resultDataGridView = new System.Windows.Forms.DataGridView();
            this.deviationLabel = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.resultGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).BeginInit();
            this.SuspendLayout();
            //
            // targetGroupBox
            //
            this.targetGroupBox.ForeColor = System.Drawing.Color.White;
            this.targetGroupBox.Location = new System.Drawing.Point(12, 12);
            this.targetGroupBox.Name = "targetGroupBox";
            this.targetGroupBox.Size = new System.Drawing.Size(250, 310);
            this.targetGroupBox.TabIndex = 0;
            this.targetGroupBox.TabStop = false;
            this.targetGroupBox.Text = "Wanted stats at level 99";
            //
            // searchButton
            //
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(12, 332);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(250, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search profiles";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            //
            // resultGroupBox
            //
            this.resultGroupBox.Controls.Add(this.deviationLabel);
            this.resultGroupBox.Controls.Add(this.resultDataGridView);
            this.resultGroupBox.Controls.Add(this.profileLabel);
            this.resultGroupBox.Controls.Add(this.nextButton);
            this.resultGroupBox.Controls.Add(this.previousButton);
            this.resultGroupBox.ForeColor = System.Drawing.Color.White;
            this.resultGroupBox.Location = new System.Drawing.Point(274, 12);
            this.resultGroupBox.Name = "resultGroupBox";
            this.resultGroupBox.Size = new System.Drawing.Size(414, 380);
            this.resultGroupBox.TabIndex = 2;
            this.resultGroupBox.TabStop = false;
            this.resultGroupBox.Text = "Result";
            //
            // previousButton
            //
            this.previousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousButton.ForeColor = System.Drawing.Color.White;
            this.previousButton.Location = new System.Drawing.Point(10, 22);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(30, 23);
            this.previousButton.TabIndex = 0;
            this.previousButton.Text = "<";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            //
            // nextButton
            //
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextButton.ForeColor = System.Drawing.Color.White;
            this.nextButton.Location = new System.Drawing.Point(374, 22);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(30, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = ">";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButton_Click);
            //
            // profileLabel
            //
            this.profileLabel.ForeColor = System.Drawing.Color.White;
            this.profileLabel.Location = new System.Drawing.Point(46, 27);
            this.profileLabel.Name = "profileLabel";
            this.profileLabel.Size = new System.Drawing.Size(322, 15);
            this.profileLabel.TabIndex = 2;
            this.profileLabel.Text = "No search yet";
            this.profileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // resultDataGridView
            //
            this.resultDataGridView.AllowUserToAddRows = false;
            this.resultDataGridView.AllowUserToDeleteRows = false;
            this.resultDataGridView.AllowUserToResizeRows = false;
            this.resultDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.resultDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.resultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.resultDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.resultDataGridView.EnableHeadersVisualStyles = false;
            this.resultDataGridView.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.resultDataGridView.Location = new System.Drawing.Point(10, 52);
            this.resultDataGridView.Name = "resultDataGridView";
            this.resultDataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.resultDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.resultDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.resultDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.resultDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultDataGridView.Size = new System.Drawing.Size(394, 282);
            this.resultDataGridView.TabIndex = 3;
            //
            // deviationLabel
            //
            this.deviationLabel.ForeColor = System.Drawing.Color.White;
            this.deviationLabel.Location = new System.Drawing.Point(10, 342);
            this.deviationLabel.Name = "deviationLabel";
            this.deviationLabel.Size = new System.Drawing.Size(394, 15);
            this.deviationLabel.TabIndex = 4;
            //
            // applyButton
            //
            this.applyButton.Enabled = false;
            this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyButton.ForeColor = System.Drawing.Color.White;
            this.applyButton.Location = new System.Drawing.Point(558, 402);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(130, 23);
            this.applyButton.TabIndex = 3;
            this.applyButton.Text = "Apply this profile";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            //
            // cancelButton
            //
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(420, 402);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(130, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            //
            // GenerateStatsWindow
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(700, 437);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.resultGroupBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.targetGroupBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerateStatsWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate level 99 stats";
            this.resultGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox targetGroupBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.GroupBox resultGroupBox;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label profileLabel;
        private System.Windows.Forms.DataGridView resultDataGridView;
        private System.Windows.Forms.Label deviationLabel;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
