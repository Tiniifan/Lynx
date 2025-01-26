namespace Lynx.Forms.Maps
{
    partial class MapPreview
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
            ((System.ComponentModel.ISupportInitialize)(this.selectedNpcMapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // selectedNpcMapPictureBox
            // 
            this.selectedNpcMapPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectedNpcMapPictureBox.Location = new System.Drawing.Point(1, 1);
            this.selectedNpcMapPictureBox.Name = "selectedNpcMapPictureBox";
            this.selectedNpcMapPictureBox.Size = new System.Drawing.Size(400, 400);
            this.selectedNpcMapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.selectedNpcMapPictureBox.TabIndex = 53;
            this.selectedNpcMapPictureBox.TabStop = false;
            this.selectedNpcMapPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectedNpcMapPictureBox_MouseDown);
            this.selectedNpcMapPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SelectedNpcMapPictureBox_MouseMove);
            this.selectedNpcMapPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectedNpcMapPictureBox_MouseUp);
            // 
            // MapPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(402, 402);
            this.Controls.Add(this.selectedNpcMapPictureBox);
            this.Name = "MapPreview";
            this.Text = "MapPreview";
            ((System.ComponentModel.ISupportInitialize)(this.selectedNpcMapPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox selectedNpcMapPictureBox;
    }
}