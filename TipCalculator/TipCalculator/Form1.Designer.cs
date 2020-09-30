namespace TipCalculator
{
    partial class Form1
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
            this.billLabel = new System.Windows.Forms.Label();
            this.billBox = new System.Windows.Forms.TextBox();
            this.totalBox = new System.Windows.Forms.TextBox();
            this.percentLabel = new System.Windows.Forms.Label();
            this.percentageBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // billLabel
            // 
            this.billLabel.AutoSize = true;
            this.billLabel.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.billLabel.Location = new System.Drawing.Point(12, 12);
            this.billLabel.Name = "billLabel";
            this.billLabel.Size = new System.Drawing.Size(84, 16);
            this.billLabel.TabIndex = 0;
            this.billLabel.Text = "Enter Total Bill:";
            // 
            // billBox
            // 
            this.billBox.Location = new System.Drawing.Point(148, 9);
            this.billBox.Name = "billBox";
            this.billBox.Size = new System.Drawing.Size(100, 20);
            this.billBox.TabIndex = 1;
            this.billBox.TextChanged += new System.EventHandler(this.billBox_TextChanged);
            // 
            // totalBox
            // 
            this.totalBox.Location = new System.Drawing.Point(148, 83);
            this.totalBox.Name = "totalBox";
            this.totalBox.ReadOnly = true;
            this.totalBox.Size = new System.Drawing.Size(100, 20);
            this.totalBox.TabIndex = 2;
            // 
            // percentLabel
            // 
            this.percentLabel.AutoSize = true;
            this.percentLabel.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percentLabel.Location = new System.Drawing.Point(9, 46);
            this.percentLabel.Name = "percentLabel";
            this.percentLabel.Size = new System.Drawing.Size(115, 16);
            this.percentLabel.TabIndex = 4;
            this.percentLabel.Text = "Enter Tip Percentage:";
            // 
            // percentageBox
            // 
            this.percentageBox.Location = new System.Drawing.Point(148, 46);
            this.percentageBox.Name = "percentageBox";
            this.percentageBox.Size = new System.Drawing.Size(100, 20);
            this.percentageBox.TabIndex = 5;
            this.percentageBox.TextChanged += new System.EventHandler(this.percentageBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Total Bill:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(279, 133);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.percentageBox);
            this.Controls.Add(this.percentLabel);
            this.Controls.Add(this.totalBox);
            this.Controls.Add(this.billBox);
            this.Controls.Add(this.billLabel);
            this.Name = "Form1";
            this.Text = "Tip Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label billLabel;
        private System.Windows.Forms.TextBox billBox;
        private System.Windows.Forms.TextBox totalBox;
        private System.Windows.Forms.Label percentLabel;
        private System.Windows.Forms.TextBox percentageBox;
        private System.Windows.Forms.Label label1;
    }
}

