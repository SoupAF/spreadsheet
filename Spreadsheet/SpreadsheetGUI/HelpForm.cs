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

        private void layoutButton_Click(object sender, EventArgs e)
        {
            string data = "Along the top of the window is the options for saving and opening files, creating new spreadsheets, and closing the window \n \nUnderneath that are text boxes that display the name, value, and contents of the selcted cell. Below all of these is a fourth box that will display error messages for formulas you enter \n\nBelow this is the spreadsheet display panel. This is where the data for your spreadsheet is stored and can be interacted with";
            infoBox.Text = data;
        }

        private void fileButton_Click(object sender, EventArgs e)
        {
            string data = "Saving: \nWhen you select the \"Save\" option, a save window will open up. From there you can select a location to save to, and enter a file name.\nNote: You can save a spreadsheet with any file extension, but this program can only open spreadsheet files with the extension \".sprd\"\n\nOpening a spreadsheet:\n To open a saed spreadsheet, select \"Open\". Upon selcting this option, a window will open promtiong you to select a file to open. This program can only open files with a .sprd extension";
            infoBox.Text = data;
        }

        private void navButton_Click(object sender, EventArgs e)
        {
            string data = "To navigate the spreadsheet, you can select cells using the mouse, or use the arrow keys while the spreadsheet panel is selected.\nUpon selecting a cell, you may begin to enter data into the Cell Contents box immediately";
            infoBox.Text = data;
        }
    }
}
