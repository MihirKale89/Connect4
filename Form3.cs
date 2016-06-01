using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Form3 : Form
    {
        public Slot[,] slot = new Slot[6,7];
        public Player p1 = new Player();
        public Player p2 = new Player();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label3.Text = Form2.player1_name;
            label4.Text = Form2.player2_name;
            // button1.Text = "" + Form2.player1_color;
            // button2.Text = "" + Form2.player2_color;
            switch (Form2.player1_color)
            {
                case 0:
                    button1.BackColor = Color.Red;
                    break;
                case 1:
                    button1.BackColor = Color.Green;
                    break;
                case 2:
                    button1.BackColor = Color.Blue;
                    break;
                case 3:
                    button1.BackColor = Color.Yellow;
                    break;
            }
            switch (Form2.player2_color)
            {
                case 0:
                    button2.BackColor = Color.Red;
                    break;
                case 1:
                    button2.BackColor = Color.Green;
                    break;
                case 2:
                    button2.BackColor = Color.Blue;
                    break;
                case 3:
                    button2.BackColor = Color.Yellow;
                    break;
            }
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 7; column++)
                {
                    slot[row, column] = new Slot();
                    slot[row, column].setOccupied(false);
                    slot[row, column].setPlayable(false);
                    slot[row, column].setSlotColor(System.Drawing.Color.Black);
                    slot[row, column].setSlotname("btn" + row + "" + column);
                }
            }

            for (int i = 0; i < 7; i++)
            {
                slot[0, i].setPlayable(true);
                slot[0, i].setSlotColor(System.Drawing.Color.White);                
            }

            btn00.BackColor = Color.White;
            Thread.Sleep(500);
            this.Refresh();
            btn01.BackColor = Color.White;
            Thread.Sleep(500);
            this.Refresh();
            btn02.BackColor = Color.White;
            Thread.Sleep(500);
            this.Refresh();
            btn03.BackColor = Color.White;
            Thread.Sleep(500);
            this.Refresh();
            btn04.BackColor = Color.White;
            Thread.Sleep(500);
            this.Refresh();
            btn05.BackColor = Color.White;
            Thread.Sleep(500);
            this.Refresh();
            btn06.BackColor = Color.White;
            Thread.Sleep(500);
            this.Refresh();

        }
    }
}
