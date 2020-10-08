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
            this.openHelpWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formulaWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Location = new System.Drawing.Point(2, 155);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(973, 617);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.SelectionChanged += new SS.SelectionChangedHandler(this.mainPanel_SelectionChanged);
            // 
            // contentBox
            // 
            this.contentBox.AcceptsReturn = true;
            this.contentBox.Location = new System.Drawing.Point(2, 129);
            this.contentBox.Name = "contentBox";
            this.contentBox.Size = new System.Drawing.Size(973, 20);
            this.contentBox.TabIndex = 1;
            this.contentBox.Enter += new System.EventHandler(this.contentBox_Enter);
            this.contentBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.contentBox_KeyPress);
            // 
            // cellNameBox
            // 
            this.cellNameBox.Enabled = false;
            this.cellNameBox.Location = new System.Drawing.Point(2, 74);
            this.cellNameBox.Name = "cellNameBox";
            this.cellNameBox.Size = new System.Drawing.Size(100, 20);
            this.cellNameBox.TabIndex = 2;
            // 
            // valBox
            // 
            this.valBox.Enabled = false;
            this.valBox.Location = new System.Drawing.Point(125, 74);
            this.valBox.Name = "valBox";
            this.valBox.Size = new System.Drawing.Size(100, 20);
            this.valBox.TabIndex = 3;
            // 
            // contentLabel
            // 
            this.contentLabel.AutoSize = true;
            this.contentLabel.Location = new System.Drawing.Point(-1, 113);
            this.contentLabel.Name = "contentLabel";
            this.contentLabel.Size = new System.Drawing.Size(72, 13);
            this.contentLabel.TabIndex = 4;
            this.contentLabel.Text = "Cell Contents:";
            // 
            // cellValLabel
            // 
            this.cellValLabel.AutoSize = true;
            this.cellValLabel.Location = new System.Drawing.Point(25, 55);
            this.cellValLabel.Name = "cellValLabel";
            this.cellValLabel.Size = new System.Drawing.Size(58, 13);
            this.cellValLabel.TabIndex = 5;
            this.cellValLabel.Text = "Cell Name:";
            // 
            // valLabel
            // 
            this.valLabel.AutoSize = true;
            this.valLabel.Location = new System.Drawing.Point(146, 55);
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
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem1.Text = "Save";
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
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openHelpWindowToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // openHelpWindowToolStripMenuItem
            // 
            this.openHelpWindowToolStripMenuItem.Name = "openHelpWindowToolStripMenuItem";
            this.openHelpWindowToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.openHelpWindowToolStripMenuItem.Text = "Open Help Window";
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
            // spreadWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 806);
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
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal SS.SpreadsheetPanel mainPanel;
        private System.Windows.Forms.TextBox contentBox;
        private System.Windows.Forms.TextBox cellNameBox;
        private System.Windows.Forms.TextBox valBox;
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
        private System.Windows.Forms.ToolStripMenuItem openHelpWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formulaWizardToolStripMenuItem;
    }
}

