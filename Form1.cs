using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Form1 : Form
    {
        public static int gameType;
        public Form1()
        {
            InitializeComponent();
        }
       

       
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (radioButton1.Checked) gameType = 1;
            if (radioButton2.Checked) gameType = 2;
            if (radioButton3.Checked) gameType = 3;
            Form2 frm = new Form2();
            frm.Show();
            // this.SetVisibleCore(false);
            // this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
