using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public delegate void AddOperator(char op);
    public delegate void AddNum(double num);
    public delegate void Backspace();
    public delegate void AddVar();
    public delegate void InsertButton(string cellname);
    public delegate void TargetChanged();

    public partial class FormulaWizard : Form
    {

        public WizardController control;

        public event AddOperator addOp;
        public event AddNum addNum;
        public event Backspace backspace;
        public event AddVar addVar;
        public event InsertButton insertButton;
        public event TargetChanged TargChanged;

        public FormulaWizard()
        {
            InitializeComponent();
            control = new WizardController();

            

            //Register event handlers
            formulaBox.Text = control.Formula;
            addOp += control.AddOperator;
            addNum += control.AddNum;
            backspace += control.Backspace;
            addVar += control.AddVariable;
            insertButton += control.InsertResultFormula;
            TargChanged += control.TargetChanged;
        }

        private void plusBut_Click(object sender, EventArgs e)
        {
            //Trigger the addOp event with the correct operator
            addOp('+');
            //Update the contents of the formula box
            formulaBox.Text = control.Formula;
            //Check the validity of the current formula and display its status
            outBox.Text = control.CheckValidty();
            if (outBox.Text == "Formula is valid")
                insertBut.Enabled = true;
            else insertBut.Enabled = false;
        }

        private void subBut_Click(object sender, EventArgs e)
        {
            //Trigger the addOp event with the correct operator
            addOp('-');
            //Update the contents of the formula box
            formulaBox.Text = control.Formula;
            //Check the validity of the current formula and display its status
            outBox.Text = control.CheckValidty();
            if (outBox.Text == "Formula is valid")
                insertBut.Enabled = true;
            else insertBut.Enabled = false;
        }

        private void mulBut_Click(object sender, EventArgs e)
        {
            //Trigger the addOp event with the correct operator
            addOp('*');
            //Update the contents of the formula box
            formulaBox.Text = control.Formula;
            //Check the validity of the current formula and display its status
            outBox.Text = control.CheckValidty();
            if (outBox.Text == "Formula is valid")
                insertBut.Enabled = true;
            else insertBut.Enabled = false;
        }

        private void divBut_Click(object sender, EventArgs e)
        {
            //Trigger the addOp event with the correct operator
            addOp('/');
            //Update the contents of the formula box
            formulaBox.Text = control.Formula;
            //Check the validity of the current formula and display its status
            outBox.Text = control.CheckValidty();
            if (outBox.Text == "Formula is valid")
                insertBut.Enabled = true;
            else insertBut.Enabled = false;
        }

        private void lParBut_Click(object sender, EventArgs e)
        {
            //Trigger the addOp event with the correct operator
            addOp('(');
            //Update the contents of the formula box
            formulaBox.Text = control.Formula;
            //Check the validity of the current formula and display its status
            outBox.Text = control.CheckValidty();
            if (outBox.Text == "Formula is valid")
                insertBut.Enabled = true;
            else insertBut.Enabled = false;
        }

        private void rParBut_Click(object sender, EventArgs e)
        {
            //Trigger the addOp event with the correct operator
            addOp(')');
            //Update the contents of the formula box
            formulaBox.Text = control.Formula;
            //Check the validity of the current formula and display its status
            outBox.Text = control.CheckValidty();
            if (outBox.Text == "Formula is valid")
                insertBut.Enabled = true;
            else insertBut.Enabled = false;
        }

        private void addVarBut_Click(object sender, EventArgs e)
        {
            //Trigger the addVar event
            addVar();
            //Update the contents of the formula box
            formulaBox.Text = control.Formula;
            //Check the validity of the current formula and display its status
            outBox.Text = control.CheckValidty();
            if (outBox.Text == "Formula is valid")
                insertBut.Enabled = true;
            else insertBut.Enabled = false;
        }

        private void backBut_Click(object sender, EventArgs e)
        {
            //Trigger the backspace event
            backspace();
            //Update the contents of the formula box
            formulaBox.Text = control.Formula;
            //Check the validity of the current formula and display its status
            outBox.Text = control.CheckValidty();
            if (outBox.Text == "Formula is valid")
                insertBut.Enabled = true;
            else insertBut.Enabled = false;
        }

        private void insertBut_Click(object sender, EventArgs e)
        {
            //Get the selected target cell, and trigger the insertButton event
            string cellname = letterBox.Text;
            cellname += numBox.Text;
            insertButton(cellname);
        }

        private void addNumBut_Click(object sender, EventArgs e)
        {
            //If the value in the number box is a double, add it to the formula by triggering the addNum event
            //Then update the display to reflect the addition of the new value
            if (double.TryParse(constBox.Text, out double num))
            {
                addNum(num);
                formulaBox.Text = control.Formula;
                constBox.Clear();
                outBox.Text = control.CheckValidty();
                if (outBox.Text == "Formula is valid")
                    insertBut.Enabled = true;
                else insertBut.Enabled = false;
            }
        }

        
        
        private void numBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check for circular dependencies again by triggering the TargChanged event

            TargChanged();
        }

        private void FormulaWizard_Load(object sender, EventArgs e)
        {
            //Set default selections for the combo boxes when the form loads
            letterBox.SelectedIndex = 0;
            numBox.SelectedIndex = 0;
        }

        private void letterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check for circular dependencies again by triggering the TargChanged event
            TargChanged();
        }
    }

    public delegate void InsertFormula(string cellName, string contents);


    /// <summary>
    /// Controller class used to control the FormulaWizard form. 
    /// </summary>
    public class WizardController
    {

        public event InsertFormula insertFormula;
        public spreadWindow mainWindow;

        private string formula;

        public WizardController()
        {
            formula = "=";
            mainWindow = (spreadWindow)spreadWindow.ActiveForm;
            insertFormula += mainWindow.control.FormulaInsertHander;
        }

        public void AddOperator(char op)
        {
            //Add the selected operator into the formula string
            formula += op;
        }

        public void AddNum(double num)
        {
            //Add the specified number to the formula string
            formula += num.ToString();
        }

        public void Backspace()
        {
            //Remove the last character of the formula string
            if (formula.Length > 1)
                formula = formula.Substring(0, formula.Length - 1);
        }

        public void AddVariable()
        {
            //Get the name of the selected cell, and add it to the formula string
            mainWindow.mainPanel.GetSelection(out int col, out int row);
            formula += mainWindow.control.GetCellName(col, row);
        }

        public void InsertResultFormula(string cellname)
        {
            //Trigger the insertFormula event (notifying the controller of the main window) and close this current window
            insertFormula(cellname, formula);
            mainWindow.control.wizard.Close();
        }

        public void TargetChanged()
        {
            //Check the validity of the current formula and display its status
            mainWindow.control.wizard.outBox.Text = CheckValidty();
            if (mainWindow.control.wizard.outBox.Text == "Formula is valid")
                mainWindow.control.wizard.insertBut.Enabled = true;
            else mainWindow.control.wizard.insertBut.Enabled = false;
        }

        //getter method for the formula string
        public string Formula
        {
            get { return formula; }
        }

        /// <summary>
        /// Takes the current formula and attempts to make a Formula object with it.
        /// If the formula is invalid, this returns the error message. Otherwise it returns "Formula is valid"
        /// </summary>
        public string CheckValidty()
        {
            Formula f1;
            try
            {
                f1 = new Formula(formula.Substring(1));
            }
            catch (FormulaFormatException e)
            {
                return e.Message;
            }

            string target = mainWindow.control.wizard.letterBox.Text.ToLower();
            target += mainWindow.control.wizard.numBox.Text;

            if (formula.Contains(target))
            {
                return "Your formula cannot contain its target cell as a reference. Please remove the variable " + target + " and try again";
            }

            if (formula.Contains("/0"))
                return "Your formula cannot contain division by zero";

            return "Formula is valid";
           
        }
    }

}

