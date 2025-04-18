namespace Lynx.Forms.Shops
{
    partial class ShopWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.shopDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.shopTreeView = new System.Windows.Forms.TreeView();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.areaTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.infoGroupBox = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsCfgbinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAscsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.shopDataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.infoGroupBox.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shopDataGridView
            // 
            this.shopDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.shopDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.shopDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.shopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.shopDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.shopDataGridView.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.shopDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.shopDataGridView.Enabled = false;
            this.shopDataGridView.EnableHeadersVisualStyles = false;
            this.shopDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.shopDataGridView.Location = new System.Drawing.Point(312, 71);
            this.shopDataGridView.Name = "shopDataGridView";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.shopDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.shopDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.shopDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle13;
            this.shopDataGridView.Size = new System.Drawing.Size(625, 544);
            this.shopDataGridView.TabIndex = 54;
            this.shopDataGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ShopDataGridView_CellMouseUp);
            this.shopDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ShopDataGridView_CellValueChanged);
            this.shopDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.ShopDataGridView_CurrentCellDirtyStateChanged);
            // 
            // Column1
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Sorted = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 300;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Condition";
            this.Column2.Name = "Column2";
            this.Column2.Width = 300;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Index";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteItemToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 26);
            // 
            // deleteItemToolStripMenuItem
            // 
            this.deleteItemToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.deleteItemToolStripMenuItem.Name = "deleteItemToolStripMenuItem";
            this.deleteItemToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.deleteItemToolStripMenuItem.Text = "Delete Item";
            this.deleteItemToolStripMenuItem.Click += new System.EventHandler(this.DeleteItemToolStripMenuItem_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchTextBox.Location = new System.Drawing.Point(63, 5);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(243, 13);
            this.searchTextBox.TabIndex = 55;
            this.searchTextBox.Text = "Search...";
            this.searchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            this.searchTextBox.MouseEnter += new System.EventHandler(this.SearchTextBox_MouseEnter);
            this.searchTextBox.MouseLeave += new System.EventHandler(this.SearchTextBox_MouseLeave);
            // 
            // shopTreeView
            // 
            this.shopTreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.shopTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.shopTreeView.ForeColor = System.Drawing.Color.White;
            this.shopTreeView.LineColor = System.Drawing.Color.White;
            this.shopTreeView.Location = new System.Drawing.Point(12, 27);
            this.shopTreeView.Name = "shopTreeView";
            this.shopTreeView.Size = new System.Drawing.Size(294, 588);
            this.shopTreeView.TabIndex = 56;
            this.shopTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ShopTreeView_AfterSelect);
            this.shopTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShopTreeView_MouseDown);
            // 
            // fileTextBox
            // 
            this.fileTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.fileTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fileTextBox.Enabled = false;
            this.fileTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.fileTextBox.Location = new System.Drawing.Point(41, 19);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.ReadOnly = true;
            this.fileTextBox.Size = new System.Drawing.Size(152, 13);
            this.fileTextBox.TabIndex = 58;
            this.fileTextBox.TextChanged += new System.EventHandler(this.FileTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 57;
            this.label5.Text = "File";
            // 
            // nameTextBox
            // 
            this.nameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nameTextBox.Location = new System.Drawing.Point(255, 19);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(152, 13);
            this.nameTextBox.TabIndex = 60;
            this.nameTextBox.Click += new System.EventHandler(this.NameTextBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(214, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Name";
            // 
            // areaTextBox
            // 
            this.areaTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.areaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.areaTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.areaTextBox.Location = new System.Drawing.Point(459, 19);
            this.areaTextBox.Name = "areaTextBox";
            this.areaTextBox.ReadOnly = true;
            this.areaTextBox.Size = new System.Drawing.Size(152, 13);
            this.areaTextBox.TabIndex = 62;
            this.areaTextBox.Click += new System.EventHandler(this.AreaTextBox_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(424, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "Area";
            // 
            // infoGroupBox
            // 
            this.infoGroupBox.Controls.Add(this.areaTextBox);
            this.infoGroupBox.Controls.Add(this.label5);
            this.infoGroupBox.Controls.Add(this.label2);
            this.infoGroupBox.Controls.Add(this.fileTextBox);
            this.infoGroupBox.Controls.Add(this.nameTextBox);
            this.infoGroupBox.Controls.Add(this.label1);
            this.infoGroupBox.Enabled = false;
            this.infoGroupBox.ForeColor = System.Drawing.Color.White;
            this.infoGroupBox.Location = new System.Drawing.Point(312, 21);
            this.infoGroupBox.Name = "infoGroupBox";
            this.infoGroupBox.Size = new System.Drawing.Size(625, 45);
            this.infoGroupBox.TabIndex = 265;
            this.infoGroupBox.TabStop = false;
            this.infoGroupBox.Text = "Info";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip2.Size = new System.Drawing.Size(108, 48);
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
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(950, 24);
            this.menuStrip1.TabIndex = 276;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsCfgbinToolStripMenuItem,
            this.exportAscsvToolStripMenuItem});
            this.exportToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // exportAsCfgbinToolStripMenuItem
            // 
            this.exportAsCfgbinToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.exportAsCfgbinToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportAsCfgbinToolStripMenuItem.Name = "exportAsCfgbinToolStripMenuItem";
            this.exportAsCfgbinToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportAsCfgbinToolStripMenuItem.Text = "Export as .cfg.bin";
            this.exportAsCfgbinToolStripMenuItem.Click += new System.EventHandler(this.ExportAsCfgbinToolStripMenuItem_Click);
            // 
            // exportAscsvToolStripMenuItem
            // 
            this.exportAscsvToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.exportAscsvToolStripMenuItem.Enabled = false;
            this.exportAscsvToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportAscsvToolStripMenuItem.Name = "exportAscsvToolStripMenuItem";
            this.exportAscsvToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportAscsvToolStripMenuItem.Text = "Export as .xlsx";
            this.exportAscsvToolStripMenuItem.Click += new System.EventHandler(this.ExportAscsvToolStripMenuItem_Click);
            // 
            // ShopWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(950, 627);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.infoGroupBox);
            this.Controls.Add(this.shopTreeView);
            this.Controls.Add(this.shopDataGridView);
            this.MaximumSize = new System.Drawing.Size(966, 666);
            this.MinimumSize = new System.Drawing.Size(966, 666);
            this.Name = "ShopWindow";
            this.Text = "ShopWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShopWindow_FormClosed);
            this.Shown += new System.EventHandler(this.ShopWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.shopDataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.infoGroupBox.ResumeLayout(false);
            this.infoGroupBox.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView shopDataGridView;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.TreeView shopTreeView;
        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox areaTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox infoGroupBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteItemToolStripMenuItem;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAsCfgbinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAscsvToolStripMenuItem;
    }
}