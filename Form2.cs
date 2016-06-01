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
    public partial class Form2 : Form
    {
        public static String player1_name;
        public static int player1_color;
        public static String player2_name;
        public static int player2_color;
        public static Boolean player1firstmove = true;
        public Form2()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Boolean valid = true;
            String errormessage="";
            if (textBox1.Text.Equals(textBox2.Text))
            {
                valid = false;
                errormessage = errormessage + "\n Player 1 and Player 2 should have unique names.";
            }
            if (comboBox1.SelectedIndex == comboBox2.SelectedIndex)
            {
                valid = false;
                errormessage = errormessage + "\n Player 1 and Player 2 should have unique colors.";
            }
            if (valid)
            {
                this.Hide();
                player1_name = textBox1.Text;
                player1_color = comboBox1.SelectedIndex;
                player2_name = textBox2.Text;
                player2_color = comboBox2.SelectedIndex;
                if (comboBox3.SelectedIndex == 0)
                {
                    player1firstmove = true;
                }
                else
                {
                    player1firstmove = false;
                }
                if (Form1.gameType == 1)
                {
                    NewGameComputerVSComputer frm1 = new NewGameComputerVSComputer();
                    frm1.Show();
                }
                if (Form1.gameType == 2)
                {
                    NewGameComputerVSHuman frm2 = new NewGameComputerVSHuman();
                    frm2.Show();
                }
                if (Form1.gameType == 3)
                {
                    NewGameHumanVSHuman frm3 = new NewGameHumanVSHuman();
                    frm3.Show();
                }
            }
            else
            {
                MessageBox.Show(""+errormessage);
            }
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 2;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 1;
            if(Form1.gameType == 2)
            {
                label1.Text = label1.Text + "(Computer)";
                textBox1.Text = textBox1.Text + "Computer";
                label2.Text = label2.Text + "(Human)";
            }
            if(Form1.gameType == 1)
            {
                comboBox3.Hide();
                label7.Hide();
            }
            if (Form1.gameType == 3)
            {
                label1.Text = label1.Text + "(Human)";
                textBox1.Text = textBox1.Text + "Human";
                label2.Text = label2.Text + "(Human)";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }
    }
}
