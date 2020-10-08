using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpreadsheetGUI;
using SpreadsheetUtilities;
using SS;

namespace SpreadsheetGUI
{


    public delegate List<string> ValChanged(string cellName, string cellVal);
    public delegate void FormClosing();

    public partial class spreadWindow : Form
    {
        public event ValChanged cellValChanged;
        public event FormClosing tryCloseForm;



        private Controller control;




        public spreadWindow()
        {
            InitializeComponent();
            control = new Controller();
            cellValChanged += control.ChangeCellVal;
            tryCloseForm += control.SafetyCheck;
        }

        public void CloseWindow()
        {
            this.Close();
        }


        /// <summary>
        /// Takes an input of a row and a column, and returns the cell at that location's name
        /// </summary>
        private string GetCellName(int col, int row)
        {
            //Get the collumn name
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string result = letters.Substring(col, 1);
            row++;
            result += row.ToString();
            return result;
        }

        /// <summary>
        /// Returns the coords of a given cell name in the form of a double. The column is the number before the decimal, the row is the number after the decimal
        /// </summary>

        private double GetCellCoords(string cellName)
        {
            string letters = "abcdefghijklmnopqrstuvwxyz";
            double result = letters.IndexOf(cellName.Substring(0, 1));
            result += (double.Parse(cellName.Substring(1)) - 1) / 100;
            return result;
        }

        private void mainPanel_SelectionChanged(SS.SpreadsheetPanel sender)
        {


            //Set cellName box
            mainPanel.GetSelection(out int col, out int row);
            cellNameBox.Text = GetCellName(col, row);
            //Check for cell contents and value
            contentBox.Text = control.GetCellContents(GetCellName(col, row));
            valBox.Text = control.GetCellValue(GetCellName(col, row));

            //Take away focus from the contentBox, then give it back in order to pre-select all text inside it
            mainPanel.Focus();
            contentBox.Focus();

        }



        /// <summary>
        /// Used to update the values of any cells on a given input list of cells
        /// </summary>
        /// <param name="cells"></param>
        private void UpdateDependentCells(List<string> cells)
        {
            //TODO: Loop through cell names and update all cell vals/content
            foreach (string s in cells)
            {
                int col;
                int row;
                double coords = GetCellCoords(s);
                col = (int)coords;
                row = (int)((coords - col) * 100);
                mainPanel.SetValue(col, row, control.GetCellValue(s));
            }
        }







        private void contentBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            mainPanel.GetSelection(out int col, out int row);
            if (e.KeyChar == '\r')
            {
                //Trigger the cellValChanged event and update dependent cells
                string cellName = GetCellName(col, row);
                UpdateDependentCells(cellValChanged(cellName, contentBox.Text));
                //Display the cell's value
                mainPanel.SetValue(col, row, control.GetCellValue(GetCellName(col, row)));
                valBox.Text = control.GetCellValue(cellName);

            }
        }

        private void contentBox_Enter(object sender, EventArgs e)
        {
            contentBox.SelectionStart = 0;
            contentBox.SelectionLength = contentBox.Text.Length;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tryCloseForm();
        }


        

    }


    public class Controller
    {


    
    //The spreadsheet that will hold the form's data
        public Spreadsheet spreadsheet;
        public Controller()
        {
            //Create a new spreadsheet with the validator method below, and a default noramalizer that changes all cell names to lowercase
            spreadsheet = new Spreadsheet(Validator, s => s.ToLower(), "ps6");
            
        }



        public List<string> ChangeCellVal(string cellName, string cellContent)
        {
            //Add/change the value of the cell specified, and update the cell's value

            return new List<string>(spreadsheet.SetContentsOfCell(cellName, cellContent));
        }

        public string GetCellValue(string cellName)
        {
            object obj = spreadsheet.GetCellValue(cellName);
            if (obj is FormulaError)
                return "Invalid!";
            else return obj.ToString();
        }

        public string GetCellContents(string cellName)
        {
            return spreadsheet.GetCellContents(cellName).ToString();
        }

        /// <summary>
        /// Validator used by the spreadsheet to ensure that all names are a letter followed by numbers
        /// </summary>
        private bool Validator(string name)
        {
            //Check that the first character is a letter
            if (!Char.IsLetter(name.ElementAt(0)))
                return false;

            //Check that the remaining values in the name are numbers
            string digits = name.Substring(1);
            if (!int.TryParse(digits, out int _))
                return false;

            return true;
        }

        public void SafetyCheck()
        {
            if (spreadsheet.Changed == true)
            {
                //Open "Are you sure" window
                closeCheck checkClose = new closeCheck();
                checkClose.Show();

            }

            
        }

    }

}
