using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        
        private void percentageBox_TextChanged(object sender, EventArgs e)
        {
            //If the contents to the box are not valid, open the error window (Form2)
            if (!double.TryParse(percentageBox.Text, out double _) && percentageBox.Text != "")
            {
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }

            //If both boxes are filled and valid, compute the  bill and put it into totalBox
            if(percentageBox.Text != "" && billBox.Text != "")
            {
                double.TryParse(billBox.Text, out double bill);
                double.TryParse(percentageBox.Text, out double percent);
                //The percent is currently just a number, so it needs to be divided by 100 in order to make it a accurate percentage
                percent = percent / 100;
                double tip = bill * percent;
                bill += tip;
                totalBox.Text = bill.ToString();
            }
        }

        private void billBox_TextChanged(object sender, EventArgs e)
        {
            //If the contents to the box are not valid, open the error window (Form2)
            if (!double.TryParse(billBox.Text, out double _) && billBox.Text != "")
            {
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }

            if (percentageBox.Text != "" && billBox.Text != "")
            {
                double.TryParse(billBox.Text, out double bill);
                double.TryParse(percentageBox.Text, out double percent);
                //The percent is currently just a number, so it needs to be divided by 100 in order to make it a accurate percentage
                percent = percent / 100;
                double tip = bill * percent;
                bill += tip;
                totalBox.Text = bill.ToString();
            }
        }
    }
}
