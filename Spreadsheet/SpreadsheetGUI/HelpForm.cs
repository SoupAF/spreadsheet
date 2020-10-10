using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }


        //Each of the methods below displays a message in the windows textbox when the window is clicked
        private void layoutButton_Click(object sender, EventArgs e)
        {
            string data = "Along the top of the window is the options for saving and opening files, creating new spreadsheets, and closing the window \n \nUnderneath that are text boxes that display the name, value, and contents of the selcted cell. Below all of these is a fourth box that will display error messages for formulas you enter \n\nBelow this is the spreadsheet display panel. This is where the data for your spreadsheet is stored and can be interacted with";
            infoBox.Text = data;
        }

        private void fileButton_Click(object sender, EventArgs e)
        {
            string data = "Saving: \nWhen you select the \"Save\" option, a save window will open up. From there you can select a location to save to, and enter a file name.\nNote: You can save a spreadsheet with any file extension, but this program can only open spreadsheet files with the extension \".sprd\"\n\nOpening a spreadsheet:\nTo open a saved spreadsheet, select \"Open\". Upon selcting this option, a window will open promtiong you to select a file to open. This program can only open files with a .sprd extension";
            infoBox.Text = data;
        }

        private void navButton_Click(object sender, EventArgs e)
        {
            string data = "To navigate the spreadsheet, you can select cells using the mouse, or use the arrow keys while the spreadsheet panel is selected.\nVarious menu options can be accessed through the selections along the top of the window";
            infoBox.Text = data;
        }

        private void datButton_Click(object sender, EventArgs e)
        {
            string data = "To add data to your spreadsheet, first select a cell. As mentioned in the Navigation section, this can be achieved with the mouse or the arrow keys. Once a cell is selected, you can begin to type your data immediately.\nWhen your data is typed, hit enter. If you do not hit enter before selecting another cell, your data will not be saved in that cell";
            infoBox.Text = data;
        }

        private void wizardButon_Click(object sender, EventArgs e)
        {
            string data = "The special functionality in this program is a Formula Wizard tool. This tool can be used to check a formula for format issues and to insert variables by clicking on the desired cell.\n\nTo use the wizard, click the Formula Wizard menu option. Use the Target Cell boxes to select the cell you want to put your formula in.\n\nUse the Add Operator buttons to add operators, and use the Add Constant button to add the value of the adjacent box to your formula.\n\nTo insert a variable, select the cell you want in the main spreadsheet window and click Add Selected Cell as Variable.\n\nThe formula in progress will be displayed at the bottom, and below that is a box that will display if your formula is valid as written.\n\nOnce your formula is complete and valid, click Insert to add your formula to the spreadsheet";
            infoBox.Text = data;
        }
    }
}
