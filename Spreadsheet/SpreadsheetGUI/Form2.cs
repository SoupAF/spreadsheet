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
    public delegate void DontCloseForm();
        

    public partial class closeCheck : Form
    {
        public event CloseForm closeForm;
        public event DontCloseForm dontCloseForm;


        public closeCheck()
        {
            InitializeComponent();
            
        }

        private void closeCheck_Load(object sender, EventArgs e)
        {

        }
    }
}
