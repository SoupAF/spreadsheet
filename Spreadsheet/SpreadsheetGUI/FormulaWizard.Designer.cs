namespace SpreadsheetGUI
{
    partial class FormulaWizard
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
            this.label1 = new System.Windows.Forms.Label();
            this.letterBox = new System.Windows.Forms.ComboBox();
            this.numBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.formulaBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.plusBut = new System.Windows.Forms.Button();
            this.subBut = new System.Windows.Forms.Button();
            this.mulBut = new System.Windows.Forms.Button();
            this.divBut = new System.Windows.Forms.Button();
            this.lParBut = new System.Windows.Forms.Button();
            this.rParBut = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.addVarBut = new System.Windows.Forms.Button();
            this.insertBut = new System.Windows.Forms.Button();
            this.outBox = new System.Windows.Forms.TextBox();
            this.backBut = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.constBox = new System.Windows.Forms.TextBox();
            this.addNumBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Target Cell:";
            // 
            // letterBox
            // 
            this.letterBox.FormattingEnabled = true;
            this.letterBox.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"});
            this.letterBox.Location = new System.Drawing.Point(86, 27);
            this.letterBox.Name = "letterBox";
            this.letterBox.Size = new System.Drawing.Size(47, 21);
            this.letterBox.TabIndex = 3;
            // 
            // numBox
            // 
            this.numBox.FormattingEnabled = true;
            this.numBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99 "});
            this.numBox.Location = new System.Drawing.Point(150, 27);
            this.numBox.Name = "numBox";
            this.numBox.Size = new System.Drawing.Size(47, 21);
            this.numBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Column";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Row";
            // 
            // formulaBox
            // 
            this.formulaBox.Enabled = false;
            this.formulaBox.Location = new System.Drawing.Point(12, 314);
            this.formulaBox.Name = "formulaBox";
            this.formulaBox.Size = new System.Drawing.Size(222, 20);
            this.formulaBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Result:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(82, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Add Operator";
            // 
            // plusBut
            // 
            this.plusBut.Location = new System.Drawing.Point(36, 107);
            this.plusBut.Name = "plusBut";
            this.plusBut.Size = new System.Drawing.Size(75, 23);
            this.plusBut.TabIndex = 10;
            this.plusBut.Text = "+";
            this.plusBut.UseVisualStyleBackColor = true;
            this.plusBut.Click += new System.EventHandler(this.plusBut_Click);
            // 
            // subBut
            // 
            this.subBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subBut.Location = new System.Drawing.Point(125, 107);
            this.subBut.Name = "subBut";
            this.subBut.Size = new System.Drawing.Size(75, 23);
            this.subBut.TabIndex = 11;
            this.subBut.Text = "--";
            this.subBut.UseVisualStyleBackColor = true;
            this.subBut.Click += new System.EventHandler(this.subBut_Click);
            // 
            // mulBut
            // 
            this.mulBut.Location = new System.Drawing.Point(36, 136);
            this.mulBut.Name = "mulBut";
            this.mulBut.Size = new System.Drawing.Size(75, 23);
            this.mulBut.TabIndex = 12;
            this.mulBut.Text = "X";
            this.mulBut.UseVisualStyleBackColor = true;
            this.mulBut.Click += new System.EventHandler(this.mulBut_Click);
            // 
            // divBut
            // 
            this.divBut.Location = new System.Drawing.Point(125, 136);
            this.divBut.Name = "divBut";
            this.divBut.Size = new System.Drawing.Size(75, 23);
            this.divBut.TabIndex = 13;
            this.divBut.Text = "/";
            this.divBut.UseVisualStyleBackColor = true;
            this.divBut.Click += new System.EventHandler(this.divBut_Click);
            // 
            // lParBut
            // 
            this.lParBut.Location = new System.Drawing.Point(36, 165);
            this.lParBut.Name = "lParBut";
            this.lParBut.Size = new System.Drawing.Size(75, 23);
            this.lParBut.TabIndex = 14;
            this.lParBut.Text = "(";
            this.lParBut.UseVisualStyleBackColor = true;
            this.lParBut.Click += new System.EventHandler(this.lParBut_Click);
            // 
            // rParBut
            // 
            this.rParBut.Location = new System.Drawing.Point(125, 165);
            this.rParBut.Name = "rParBut";
            this.rParBut.Size = new System.Drawing.Size(75, 23);
            this.rParBut.TabIndex = 15;
            this.rParBut.Text = ")";
            this.rParBut.UseVisualStyleBackColor = true;
            this.rParBut.Click += new System.EventHandler(this.rParBut_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(82, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Add Variable";
            // 
            // addVarBut
            // 
            this.addVarBut.Location = new System.Drawing.Point(36, 227);
            this.addVarBut.Name = "addVarBut";
            this.addVarBut.Size = new System.Drawing.Size(161, 23);
            this.addVarBut.TabIndex = 17;
            this.addVarBut.Text = "Add Selected Cell as variable";
            this.addVarBut.UseVisualStyleBackColor = true;
            this.addVarBut.Click += new System.EventHandler(this.addVarBut_Click);
            // 
            // insertBut
            // 
            this.insertBut.Enabled = false;
            this.insertBut.Location = new System.Drawing.Point(85, 400);
            this.insertBut.Name = "insertBut";
            this.insertBut.Size = new System.Drawing.Size(75, 23);
            this.insertBut.TabIndex = 18;
            this.insertBut.Text = "Insert Formula";
            this.insertBut.UseVisualStyleBackColor = true;
            this.insertBut.Click += new System.EventHandler(this.insertBut_Click);
            // 
            // outBox
            // 
            this.outBox.Enabled = false;
            this.outBox.Location = new System.Drawing.Point(12, 344);
            this.outBox.Multiline = true;
            this.outBox.Name = "outBox";
            this.outBox.Size = new System.Drawing.Size(222, 50);
            this.outBox.TabIndex = 19;
            // 
            // backBut
            // 
            this.backBut.Location = new System.Drawing.Point(44, 256);
            this.backBut.Name = "backBut";
            this.backBut.Size = new System.Drawing.Size(141, 23);
            this.backBut.TabIndex = 20;
            this.backBut.Text = "Backspace";
            this.backBut.UseVisualStyleBackColor = true;
            this.backBut.Click += new System.EventHandler(this.backBut_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Add Constant";
            // 
            // constBox
            // 
            this.constBox.Location = new System.Drawing.Point(86, 60);
            this.constBox.Name = "constBox";
            this.constBox.Size = new System.Drawing.Size(47, 20);
            this.constBox.TabIndex = 22;
            // 
            // addNumBut
            // 
            this.addNumBut.Location = new System.Drawing.Point(150, 58);
            this.addNumBut.Name = "addNumBut";
            this.addNumBut.Size = new System.Drawing.Size(50, 23);
            this.addNumBut.TabIndex = 23;
            this.addNumBut.Text = "Add";
            this.addNumBut.UseVisualStyleBackColor = true;
            this.addNumBut.Click += new System.EventHandler(this.addNumBut_Click);
            // 
            // FormulaWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 426);
            this.Controls.Add(this.addNumBut);
            this.Controls.Add(this.constBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.backBut);
            this.Controls.Add(this.outBox);
            this.Controls.Add(this.insertBut);
            this.Controls.Add(this.addVarBut);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rParBut);
            this.Controls.Add(this.lParBut);
            this.Controls.Add(this.divBut);
            this.Controls.Add(this.mulBut);
            this.Controls.Add(this.subBut);
            this.Controls.Add(this.plusBut);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.formulaBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numBox);
            this.Controls.Add(this.letterBox);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(0, 779);
            this.Name = "FormulaWizard";
            this.Text = "Formula Wizard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox letterBox;
        public System.Windows.Forms.ComboBox numBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox formulaBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button plusBut;
        private System.Windows.Forms.Button subBut;
        private System.Windows.Forms.Button mulBut;
        private System.Windows.Forms.Button divBut;
        private System.Windows.Forms.Button lParBut;
        private System.Windows.Forms.Button rParBut;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button addVarBut;
        private System.Windows.Forms.Button insertBut;
        private System.Windows.Forms.TextBox outBox;
        private System.Windows.Forms.Button backBut;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox constBox;
        private System.Windows.Forms.Button addNumBut;
    }
}