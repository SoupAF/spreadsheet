using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpreadsheetGUI;
using SpreadsheetUtilities;
using SS;

namespace SpreadsheetGUI
{


    //public delegate List<string> ValChanged(string cellName, string cellVal);
    public delegate bool FormClosing(FormClosingEventArgs e);
    public delegate void SelectionChanged();
    public delegate void KeyPressed(KeyPressEventArgs e);
    public delegate void ArrowPressed(PreviewKeyDownEventArgs e);
    public delegate void NewSpreadsheet();
    public delegate void SaveSpreadsheet(string filename);
    public delegate void OpenSpreadsheet(string filename);
    public delegate void OpenFormulaWizard();

    public partial class spreadWindow : Form
    {
        //Events for the program
        public event FormClosing tryCloseForm;
        public event SelectionChanged selectionchanged;
        public event KeyPressed keyPressed;
        public event ArrowPressed arrowPressed;
        public event NewSpreadsheet newSpreadsheet;
        public event SaveSpreadsheet saveSpreadsheet;
        public event OpenSpreadsheet openSpreadsheet;
        public event OpenFormulaWizard openWizard;

        public Controller control;

        public spreadWindow()
        {
            InitializeComponent();
            this.Show();
            control = new Controller();
            SaveSpreadsheet = new SaveFileDialog();

            //Set the default selcted cell
            mainPanel.SetSelection(0, 0);
            //Give focus to the content box
            contentBox.Focus();

            //Register all events to their handlers
            selectionchanged += control.SelectionChangedHandler;
            keyPressed += control.KeyPressedHandler;
            tryCloseForm += control.ClosePressedHandler;
            arrowPressed += control.ArrowPressedHandler;
            newSpreadsheet += control.NewSpreadsheetHandler;
            saveSpreadsheet += control.SaveSpreadsheetHandler;
            openSpreadsheet += control.OpenSpreadsheetHandler;
            openWizard += control.OpenWizard;

            

        }


        private void mainPanel_SelectionChanged(SS.SpreadsheetPanel sender)
        {
            //Trigger the SeletionChnaged event
            selectionchanged();
        }

        private void contentBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Trigger the KeyPressed Event
            keyPressed(e);
        }

        private void contentBox_Enter(object sender, EventArgs e)
        {
            //This code is for convenience; it selects all text in the contentBox textbox so that the use can immediately begin typing
            contentBox.SelectionStart = 0;
            contentBox.SelectionLength = contentBox.Text.Length;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Close the window. This will trigger the FormClosing event below
            this.Close();
        }

        private void contentBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //If an arrow key is pressed, trigger the arrowPresed event and the selectionChanged event
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                arrowPressed(e);
                selectionchanged();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Trigger the newSpreadsheet event when the New buton is clicked
            newSpreadsheet();
        }

        private void spreadWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If the user attempts to close the window, trigger the tryCloseForm event to see if changes need to be saved
            e.Cancel = tryCloseForm(e);
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Set settings for the save file window
            SaveSpreadsheet.Filter = "Spreadsheet Files \".sprd\"|*.sprd|All Files|*.*";
            SaveSpreadsheet.Title = "Save a Spreadsheet";
            SaveSpreadsheet.AddExtension = true;
            SaveSpreadsheet.OverwritePrompt = true;
            //If the user picked a valid file, trigger the saveSpreadsheet event
            if (SaveSpreadsheet.ShowDialog() == DialogResult.OK)
                saveSpreadsheet(SaveSpreadsheet.FileName);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set settings for the open file window
            OpenSpreadsheet.Filter = "Spreadsheed Files \".sprd\"|*.sprd";
            //If the user picked a valid file, trigger the openSpreadsheet event
            if (OpenSpreadsheet.ShowDialog() == DialogResult.OK)
                openSpreadsheet(OpenSpreadsheet.FileName);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open the help window
            HelpForm help = new HelpForm();
            help.Show();
        }

        private void formulaWizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open the formula wizard window
            openWizard();
        }
    }



    /// <summary>
    /// Class responsible for interfacing with the model (spreadsheet) and the GUI
    /// </summary>
    public class Controller
    {
        //The spreadsheet that will hold the form's data
        public Spreadsheet spreadsheet;

        //Used for keeping track of the various windows that may open
        private spreadWindow mainWindow;
        private closeCheck closeCheckWindow;
        public FormulaWizard wizard;


        public Controller()
        {
            //Create a new spreadsheet with the validator method below, and a default noramalizer that changes all cell names to lowercase
            spreadsheet = new Spreadsheet(Validator, s => s.ToLower(), "ps6");
            //Get the current spreadsheet window
            mainWindow = (spreadWindow)spreadWindow.ActiveForm;


        }

        public void SelectionChangedHandler()
        {
            //Set cellName box
            mainWindow.mainPanel.GetSelection(out int col, out int row);
            mainWindow.cellNameBox.Text = GetCellName(col, row);
            //Check for cell contents and value
            mainWindow.contentBox.Text = GetCellContents(GetCellName(col, row));
            mainWindow.valBox.Text = GetCellValue(GetCellName(col, row));
            mainWindow.outputBox.Clear();

            //Take away focus from the contentBox, then give it back in order to pre-select all text inside it
            mainWindow.Focus();
            mainWindow.contentBox.Focus();
        }


        public void KeyPressedHandler(KeyPressEventArgs e)
        {
           mainWindow.mainPanel.GetSelection(out int col, out int row);
            //If an enter key was pressed, update the value of the selected cell
            if (e.KeyChar == '\r')
            {
                //Change the value of the cell and update dependent cells
                string cellName = GetCellName(col, row);
                try { UpdateDependentCells(ChangeCellVal(cellName, mainWindow.contentBox.Text)); }

                catch (CircularException)
                {
                    mainWindow.outputBox.Text = "Your formula cannot be dependent on its own value, even indirectly. Please try again";
                    //SystemSounds.Hand.Play();
                    //spreadsheet.SetContentsOfCell(cellName, "");
                    e.Handled = true;
                    return;
                }

                //Display the cell's value
                mainWindow.mainPanel.SetValue(col, row, GetCellValue(GetCellName(col, row)));
                mainWindow.valBox.Text = GetCellValue(cellName);
                e.Handled = true;
            }

        }

        public void FormulaInsertHander(string cellname, string content)
        {
            //Update cells dependent on the inserted formula
            UpdateDependentCells(ChangeCellVal(cellname, content));

            //Display the cell's new value
            int col;
            int row;
            string coords = GetCellCoords(cellname);
            string[] coordsArray = coords.Split(',');
            col = int.Parse(coordsArray[0]);
            row = int.Parse(coordsArray[1]);
            mainWindow.mainPanel.SetValue(col, row, GetCellValue(GetCellName(col, row)));
            mainWindow.valBox.Text = GetCellValue(cellname);
        }

        public bool ClosePressedHandler(FormClosingEventArgs e)
        {
            //If the spreadsheet has no unsaved changes, close the form
            if (spreadsheet.Changed == false)
            {
                return false;
            }
            //If there are unsaved changes, open a window to ask the user if they want to save first
            else
            {
                closeCheckWindow = new closeCheck();
                closeCheckWindow.StartPosition = FormStartPosition.Manual;
                Point closeCheckLoc = mainWindow.PointToScreen(new Point(0, 0));
                closeCheckWindow.Location = closeCheckLoc;
                closeCheckWindow.Show();
                return true;
            }
        }

        public void CloseProgram()
        {
            //Used to handle the response from the CloseCheck form. Only triggers if "yes" is pressed
            //Ends the current program
            System.Windows.Forms.Application.ExitThread();
            closeCheckWindow.Close();
        }



        public void ArrowPressedHandler(PreviewKeyDownEventArgs e)
        {
            //Determine which arrow key was pressed, and shift the current selected cell in that direction
            mainWindow.mainPanel.GetSelection(out int col, out int row);
            switch (e.KeyCode)
            {
                case Keys.Up:
                    mainWindow.mainPanel.SetSelection(col, row - 1);
                    break;

                case Keys.Down:
                    mainWindow.mainPanel.SetSelection(col, row + 1);
                    break;

                case Keys.Left:
                    mainWindow.mainPanel.SetSelection(col - 1, row);
                    break;

                case Keys.Right:
                    mainWindow.mainPanel.SetSelection(col + 1, row);
                    break;
            }
        }

        public void NewSpreadsheetHandler()
        {
            //If the user clicked "New" in the program, open a new instance of the program
            var spread = new System.Diagnostics.ProcessStartInfo(Application.ExecutablePath);
            System.Diagnostics.Process.Start(spread);
        }

        public void SaveSpreadsheetHandler(string fileName)
        {
            //If the user selected a valid filepath, save the spreadsheet
            spreadsheet.Save(fileName);
        }

        public void OpenSpreadsheetHandler(string filename)
        {
            //If the user selected a valid file to open, replace the current spreadsheet with the one loaded from file
            spreadsheet = new Spreadsheet(filename, Validator, s => s.ToLower(), "ps6");
            //Update the spreadsheet panel to display the values of the new cells
            UpdateDependentCells(new List<string>(spreadsheet.GetNamesOfAllNonemptyCells()));
        }

        public void OpenWizard()
        {
            //Create a new Formula wizard window
            wizard = new FormulaWizard();
            wizard.Show();
        }






        //Helper methods used by this and other controller classes

        /// <summary>
        /// Change the value of a cell in the main spreadsheet window
        /// </summary>
        private List<string> ChangeCellVal(string cellName, string cellContent)
        {
            //Attempt to change the value of the cell specified, and update the cell's value
            try
            {
                return new List<string>(spreadsheet.SetContentsOfCell(cellName, cellContent));
            }
            //If the formula is an invalid formula, catch the exception and display the error message to the user
            //NOTE: If the user enters an invalid formula, the formula will not be added into the cell 
            catch (FormulaFormatException e)
            {
                mainWindow.outputBox.Text = e.Message;
                SystemSounds.Hand.Play();
                return new List<string>(spreadsheet.GetNamesOfAllNonemptyCells());
            }
            catch (CircularException)
            {
                mainWindow.outputBox.Text = "Your formula is dependent on its own value. Please try again";
                SystemSounds.Hand.Play();
                spreadsheet.SetContentsOfCell(cellName, "");
                return new List<string>(spreadsheet.GetNamesOfAllNonemptyCells());
            }
        }

        /// <summary>
        /// Returns the value of the cell named cellName. Returns "Invalid!" if the value is a FormulaError
        /// </summary>
        public string GetCellValue(string cellName)
        {
            //If there is a formula error, display Invalid! Otherwise, display the cells value
            object obj = spreadsheet.GetCellValue(cellName);
            if (obj is FormulaError)
                return "Invalid!";
            else return obj.ToString();
        }

        /// <summary>
        /// Returns the contents of a cell in string form
        /// </summary>
        private string GetCellContents(string cellName)
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

        /// <summary>
        /// Takes an input of a row and a column, and returns the cell at that location's name
        /// </summary>
        public string GetCellName(int col, int row)
        {
            //Get the collumn name
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string result = letters.Substring(col, 1);
            row++;
            result += row.ToString();
            return result;
        }


        /// <summary>
        /// Used to update the values of any cells on a given input list of cells
        /// </summary>
        /// <param name="cells"></param>
        private void UpdateDependentCells(List<string> cells)
        {
            //Loop through all cell names and update their values
            foreach (string s in cells)
            {
                int col;
                int row;
                string coords = GetCellCoords(s);
                string[] coordsArray = coords.Split(',');
                col = int.Parse(coordsArray[0]);
                row = int.Parse(coordsArray[1]);
                mainWindow.mainPanel.SetValue(col, row, GetCellValue(s));
            }
        }



        /// <summary>
        /// Returns the coords of a given cell name in the form of a double. The column is the number before the decimal, the row is the number after the decimal
        /// </summary>
        private string GetCellCoords(string cellName)
        {
            cellName = cellName.ToLower();
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string result = letters.IndexOf(cellName.Substring(0, 1)).ToString();
            result += ",";
            result += (int.Parse(cellName.Substring(1))-1).ToString();
            return result;
        }


    }

}
