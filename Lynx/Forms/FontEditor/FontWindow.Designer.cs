namespace Lynx.Forms.FontEditor
{
    partial class FontWindow
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
            this.nameGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // nameGroupBox
            // 
            this.nameGroupBox.ForeColor = System.Drawing.Color.White;
            this.nameGroupBox.Location = new System.Drawing.Point(12, 12);
            this.nameGroupBox.Name = "nameGroupBox";
            this.nameGroupBox.Size = new System.Drawing.Size(512, 512);
            this.nameGroupBox.TabIndex = 267;
            this.nameGroupBox.TabStop = false;
            this.nameGroupBox.Text = "Font";
            // 
            // groupBox1
            // 
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(530, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 86);
            this.groupBox1.TabIndex = 268;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Config";
            // 
            // FontWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(1008, 533);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nameGroupBox);
            this.Name = "FontWindow";
            this.Text = "FontWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox nameGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}