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


    public delegate void CloseForm();
   
        

    public partial class closeCheck : Form
    {
        public event CloseForm closeForm;

        public closeCheck()
        {
            InitializeComponent();
            spreadWindow mainWindow = (spreadWindow)spreadWindow.ActiveForm;
            closeForm += mainWindow.control.CloseProgram;
            
        }


        private void yes_Click(object sender, EventArgs e)
        {
            //If the "yes" button was clicked, close the entire program by triggering the colseForm event
            closeForm();
        }

        private void no_Click(object sender, EventArgs e)
        {
            //If "no" was clicked, close this form
            this.Close();
        }
    }
}
