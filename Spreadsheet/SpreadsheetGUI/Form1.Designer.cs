namespace SpreadsheetGUI
{
    partial class spreadWindow
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
            this.mainPanel = new SS.SpreadsheetPanel();
            this.contentBox = new System.Windows.Forms.TextBox();
            this.cellNameBox = new System.Windows.Forms.TextBox();
            this.valBox = new System.Windows.Forms.TextBox();
            this.contentLabel = new System.Windows.Forms.Label();
            this.cellValLabel = new System.Windows.Forms.Label();
            this.valLabel = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formulaWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveSpreadsheet = new System.Windows.Forms.SaveFileDialog();
            this.OpenSpreadsheet = new System.Windows.Forms.OpenFileDialog();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Location = new System.Drawing.Point(5, 151);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(973, 581);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.SelectionChanged += new SS.SelectionChangedHandler(this.mainPanel_SelectionChanged);
            // 
            // contentBox
            // 
            this.contentBox.AcceptsReturn = true;
            this.contentBox.Location = new System.Drawing.Point(5, 99);
            this.contentBox.Multiline = true;
            this.contentBox.Name = "contentBox";
            this.contentBox.Size = new System.Drawing.Size(961, 20);
            this.contentBox.TabIndex = 1;
            this.contentBox.Enter += new System.EventHandler(this.contentBox_Enter);
            this.contentBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.contentBox_KeyPress);
            this.contentBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.contentBox_PreviewKeyDown);
            // 
            // cellNameBox
            // 
            this.cellNameBox.Enabled = false;
            this.cellNameBox.Location = new System.Drawing.Point(5, 44);
            this.cellNameBox.Name = "cellNameBox";
            this.cellNameBox.Size = new System.Drawing.Size(88, 20);
            this.cellNameBox.TabIndex = 2;
            // 
            // valBox
            // 
            this.valBox.Enabled = false;
            this.valBox.Location = new System.Drawing.Point(128, 44);
            this.valBox.Name = "valBox";
            this.valBox.Size = new System.Drawing.Size(88, 20);
            this.valBox.TabIndex = 3;
            // 
            // contentLabel
            // 
            this.contentLabel.AutoSize = true;
            this.contentLabel.Location = new System.Drawing.Point(2, 83);
            this.contentLabel.Name = "contentLabel";
            this.contentLabel.Size = new System.Drawing.Size(72, 13);
            this.contentLabel.TabIndex = 4;
            this.contentLabel.Text = "Cell Contents:";
            // 
            // cellValLabel
            // 
            this.cellValLabel.AutoSize = true;
            this.cellValLabel.Location = new System.Drawing.Point(16, 24);
            this.cellValLabel.Name = "cellValLabel";
            this.cellValLabel.Size = new System.Drawing.Size(58, 13);
            this.cellValLabel.TabIndex = 5;
            this.cellValLabel.Text = "Cell Name:";
            // 
            // valLabel
            // 
            this.valLabel.AutoSize = true;
            this.valLabel.Location = new System.Drawing.Point(149, 25);
            this.valLabel.Name = "valLabel";
            this.valLabel.Size = new System.Drawing.Size(57, 13);
            this.valLabel.TabIndex = 6;
            this.valLabel.Text = "Cell Value:";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(978, 24);
            this.menuStrip.TabIndex = 7;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem1,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "New";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formulaWizardToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // formulaWizardToolStripMenuItem
            // 
            this.formulaWizardToolStripMenuItem.Name = "formulaWizardToolStripMenuItem";
            this.formulaWizardToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.formulaWizardToolStripMenuItem.Text = "Formula Wizard";
            // 
            // outputBox
            // 
            this.outputBox.Enabled = false;
            this.outputBox.Location = new System.Drawing.Point(5, 125);
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(961, 20);
            this.outputBox.TabIndex = 8;
            // 
            // spreadWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 739);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.valLabel);
            this.Controls.Add(this.cellValLabel);
            this.Controls.Add(this.contentLabel);
            this.Controls.Add(this.valBox);
            this.Controls.Add(this.cellNameBox);
            this.Controls.Add(this.contentBox);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "spreadWindow";
            this.Text = "Spreadsheet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.spreadWindow_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal SS.SpreadsheetPanel mainPanel;
        public System.Windows.Forms.TextBox contentBox;
        public System.Windows.Forms.TextBox cellNameBox;
        public System.Windows.Forms.TextBox valBox;
        private System.Windows.Forms.Label contentLabel;
        private System.Windows.Forms.Label cellValLabel;
        private System.Windows.Forms.Label valLabel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formulaWizardToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog SaveSpreadsheet;
        private System.Windows.Forms.OpenFileDialog OpenSpreadsheet;
        public System.Windows.Forms.TextBox outputBox;
    }
}

