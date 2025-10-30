namespace Lynx.Forms.TranslationHelper
{
    partial class TranslationHelperWindow
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
            this.importTranslationButton = new System.Windows.Forms.Button();
            this.exportTranslationButton = new System.Windows.Forms.Button();
            this.buttonExportCfgBin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // importTranslationButton
            // 
            this.importTranslationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.importTranslationButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.importTranslationButton.Location = new System.Drawing.Point(21, 12);
            this.importTranslationButton.Name = "importTranslationButton";
            this.importTranslationButton.Size = new System.Drawing.Size(200, 23);
            this.importTranslationButton.TabIndex = 1;
            this.importTranslationButton.Text = "Import Translation Table";
            this.importTranslationButton.UseVisualStyleBackColor = true;
            this.importTranslationButton.Click += new System.EventHandler(this.ImportTranslationButton_Click);
            // 
            // exportTranslationButton
            // 
            this.exportTranslationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportTranslationButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.exportTranslationButton.Location = new System.Drawing.Point(21, 41);
            this.exportTranslationButton.Name = "exportTranslationButton";
            this.exportTranslationButton.Size = new System.Drawing.Size(200, 23);
            this.exportTranslationButton.TabIndex = 2;
            this.exportTranslationButton.Text = "Export Translation Table";
            this.exportTranslationButton.UseVisualStyleBackColor = true;
            this.exportTranslationButton.Click += new System.EventHandler(this.ExportTranslationButton_Click);
            // 
            // buttonExportCfgBin
            // 
            this.buttonExportCfgBin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportCfgBin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonExportCfgBin.Location = new System.Drawing.Point(21, 70);
            this.buttonExportCfgBin.Name = "buttonExportCfgBin";
            this.buttonExportCfgBin.Size = new System.Drawing.Size(200, 23);
            this.buttonExportCfgBin.TabIndex = 3;
            this.buttonExportCfgBin.Text = "Export as .cfg.bin";
            this.buttonExportCfgBin.UseVisualStyleBackColor = true;
            this.buttonExportCfgBin.Click += new System.EventHandler(this.ButtonExportCfgBin_Click);
            // 
            // TranslationHelperWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(238, 106);
            this.Controls.Add(this.buttonExportCfgBin);
            this.Controls.Add(this.exportTranslationButton);
            this.Controls.Add(this.importTranslationButton);
            this.Name = "TranslationHelperWindow";
            this.Text = "TranslationHelper";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TranslationHelperWindow_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button importTranslationButton;
        private System.Windows.Forms.Button exportTranslationButton;
        private System.Windows.Forms.Button buttonExportCfgBin;
    }
}