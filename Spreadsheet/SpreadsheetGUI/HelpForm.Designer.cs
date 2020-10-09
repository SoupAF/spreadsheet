namespace SpreadsheetGUI
{
    partial class HelpForm
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
            this.infoBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.layoutButton = new System.Windows.Forms.Button();
            this.fileButton = new System.Windows.Forms.Button();
            this.navButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // infoBox
            // 
            this.infoBox.Location = new System.Drawing.Point(1, 97);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(597, 356);
            this.infoBox.TabIndex = 0;
            this.infoBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Click a button to display information on that topic\r\n";
            // 
            // layoutButton
            // 
            this.layoutButton.Location = new System.Drawing.Point(79, 43);
            this.layoutButton.Name = "layoutButton";
            this.layoutButton.Size = new System.Drawing.Size(75, 23);
            this.layoutButton.TabIndex = 2;
            this.layoutButton.Text = "Layout";
            this.layoutButton.UseVisualStyleBackColor = true;
            this.layoutButton.Click += new System.EventHandler(this.layoutButton_Click);
            // 
            // fileButton
            // 
            this.fileButton.Location = new System.Drawing.Point(234, 43);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(123, 23);
            this.fileButton.TabIndex = 3;
            this.fileButton.Text = "Saving/Loading";
            this.fileButton.UseVisualStyleBackColor = true;
            this.fileButton.Click += new System.EventHandler(this.fileButton_Click);
            // 
            // navButton
            // 
            this.navButton.Location = new System.Drawing.Point(444, 43);
            this.navButton.Name = "navButton";
            this.navButton.Size = new System.Drawing.Size(75, 23);
            this.navButton.TabIndex = 4;
            this.navButton.Text = "Navigation";
            this.navButton.UseVisualStyleBackColor = true;
            this.navButton.Click += new System.EventHandler(this.navButton_Click);
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 450);
            this.Controls.Add(this.navButton);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(this.layoutButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.infoBox);
            this.Name = "HelpForm";
            this.Text = "HelpForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox infoBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button layoutButton;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.Button navButton;
    }
}