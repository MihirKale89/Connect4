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
    public partial class NewGameHumanVSHuman : Form
    {
        public Slot[,] slot = new Slot[6, 7];
        public Button[,] btn = new Button[6, 7];
        public Player p1 = new Player();
        public Player p2 = new Player();
        public Boolean win = false;
        static Boolean player1move = false;
        public static int movecount = 0;
        public int exploreopportunity = 20;
        public int blockoppurtunity = 40;
        public int winopportunity = 25;
        public NewGameHumanVSHuman()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            win = false;
            callonload();
            clearbuttons();
            callonstart();
        }

        private void NewGameHumanVSHuman_Load(object sender, EventArgs e)
        {
            InstantiateButtons(btn);
            if (Form2.player1firstmove)
            {
                player1move = true;
            }
            label3.Text = Form2.player1_name;
            p1.setPlayername(Form2.player1_name);
            label4.Text = Form2.player2_name;
            p2.setPlayername(Form2.player2_name);
            // button1.Text = "" + Form2.player1_color;
            // button2.Text = "" + Form2.player2_color;
            switch (Form2.player1_color)
            {
                case 0:
                    button1.BackColor = Color.Red;
                    p1.setPlayerColor(Color.Red);
                    break;
                case 1:
                    button1.BackColor = Color.Green;
                    p1.setPlayerColor(Color.Green);
                    break;
                case 2:
                    button1.BackColor = Color.Blue;
                    p1.setPlayerColor(Color.Blue);
                    break;
                case 3:
                    button1.BackColor = Color.Yellow;
                    p1.setPlayerColor(Color.Yellow);
                    break;
            }
            switch (Form2.player2_color)
            {
                case 0:
                    button2.BackColor = Color.Red;
                    p2.setPlayerColor(Color.Red);
                    break;
                case 1:
                    button2.BackColor = Color.Green;
                    p2.setPlayerColor(Color.Green);
                    break;
                case 2:
                    button2.BackColor = Color.Blue;
                    p2.setPlayerColor(Color.Blue);
                    break;
                case 3:
                    button2.BackColor = Color.Yellow;
                    p2.setPlayerColor(Color.Yellow);
                    break;
            }
        }

        private void callonload()
        {
            // Button[,] btn = new Button[6, 7];

            label3.Text = Form2.player1_name;
            p1.setPlayername(Form2.player1_name);
            label4.Text = Form2.player2_name;
            p2.setPlayername(Form2.player2_name);
            if (Form2.player1firstmove)
            {
                lblPlayerMove.Text = p1.getPlayername();
            }
            else
            {
                lblPlayerMove.Text = p2.getPlayername();
            }

            // button1.Text = "" + Form2.player1_color;
            // button2.Text = "" + Form2.player2_color;
            switch (Form2.player1_color)
            {
                case 0:
                    button1.BackColor = Color.Red;
                    p1.setPlayerColor(Color.Red);
                    break;
                case 1:
                    button1.BackColor = Color.Green;
                    p1.setPlayerColor(Color.Green);
                    break;
                case 2:
                    button1.BackColor = Color.Blue;
                    p1.setPlayerColor(Color.Blue);
                    break;
                case 3:
                    button1.BackColor = Color.Yellow;
                    p1.setPlayerColor(Color.Yellow);
                    break;
            }
            switch (Form2.player2_color)
            {
                case 0:
                    button2.BackColor = Color.Red;
                    p2.setPlayerColor(Color.Red);
                    break;
                case 1:
                    button2.BackColor = Color.Green;
                    p2.setPlayerColor(Color.Green);
                    break;
                case 2:
                    button2.BackColor = Color.Blue;
                    p2.setPlayerColor(Color.Blue);
                    break;
                case 3:
                    button2.BackColor = Color.Yellow;
                    p2.setPlayerColor(Color.Yellow);
                    break;
            }
        }

        private void callonstart()
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
                slot[0, i].setSlotColor(Color.White);
                btn[0, i].BackColor = Color.White;
                btn[0, i].Enabled = true;
                //Thread.Sleep(500);
                this.Refresh();
            }
            if (Form2.player1firstmove)
            {
                // computermove();
            }
        }

        private void clearbuttons()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 7; column++)
                {
                    btn[row, column].BackColor = Color.Black;
                    btn[row, column].Enabled = false;
                }
            }
        }

        public void InstantiateButtons(Button[,] buttons)
        {
            int rows = buttons.GetUpperBound(0) + 1; // Getting the number of rows in the array
            int columns = buttons.GetUpperBound(buttons.Rank - 1) + 1; // Getting the number of columns in the array
            int left = 130;
            int top = 200;
            // Instantiating all the buttons in the array
            for (int row = rows - 1; row >= 0; row--)
            {
                for (int column = 0; column < columns; column++)
                {
                    buttons[row, column] = new Button();
                    buttons[row, column].Visible = true;
                    buttons[row, column].Left = left;
                    buttons[row, column].Top = top;
                    buttons[row, column].BackColor = Color.Black;
                    buttons[row, column].Enabled = false;
                    buttons[row, column].Name = "btn" + row + "" + column;
                    buttons[row, column].Click += myEventHandler;
                    buttons[row, column].Height = 25;
                    buttons[row, column].Width = 50;
                    this.Controls.Add(buttons[row, column]);
                    left += 50 + 2;
                    // Button button = new Button();                                        
                }
                top += 25 + 2;
                left = 130;
            }
        }

        private void myEventHandler(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            // movecount++;
            Player p = new Player();
            if (player1move)
            {
                p = p1;
                player1move = false;
            }
            else
            {
                p = p2;
                player1move = true; 
            }

            if (clickedButton == null) // just to be on the safe side
                return;
           
                for (int row = 0; row < 6; row++)
                {
                    for (int column = 0; column < 7; column++)
                    {
                        if (clickedButton.Name.Equals("btn" + row + "" + column))
                        {
                            slot[row, column].setOccupied(true);
                            slot[row, column].setPlayable(false);
                            slot[row, column].setoccupiedPlayer(p.getPlayername());
                            slot[row, column].setSlotColor(p.getPlayerColor());
                            btn[row, column].BackColor = p.getPlayerColor();
                            btn[row, column].Enabled = false;
                            //Console.WriteLine("Slot: "+slot[row, column].getSlotName()+" Score:"+slot[row, column].getScore());
                            if (row < 5)
                            {
                                slot[row + 1, column].setPlayable(true);
                                slot[row + 1, column].setSlotColor(Color.White);
                                btn[row + 1, column].BackColor = Color.White;
                                btn[row + 1, column].Enabled = true;
                            }

                            int p2winscore = winscore(row, column, p);
                            lblPlayerMove.Text = p1.getPlayername();
                            if (win)
                            {
                                lblPlayerMove.Text = p.getPlayername() + " wins!";
                            }
                            else
                            {
                                // computermove();
                            }
                            //Thread.Sleep(1000);
                            this.Refresh();
                        }
                    }
                }

            

        }

        public int winscore(int row, int column, Player player)
        {
            int winscore = 0;
            int count = 0;
            int hlWin, hrWin, vuWin, vdWin, dneWin, dswWin, dnwWin, dseWin;
            hlWin = hrWin = vuWin = vdWin = dneWin = dswWin = dnwWin = dseWin = 0;
            // checking horizontal scope towards left
            // if ((column - 3) >= 0)
            //  {
            for (count = 1; (column - count) >= 0 && count <= 3; count++)
            {

                // Checking must block situation
                if (slot[row, column - count].isOccupied() && String.Equals(slot[row, column - count].getoccupiedPlayer(), player.getPlayername()))
                {
                    hlWin++;
                }
                else
                {
                    break;
                }
            }

            // checking horizontal scope towards right
            // if ((column + 3) < 7)
            // {
            for (count = 1; (column + count) < 7 && count <= 3; count++)
            {

                // Checking must block situation
                if (slot[row, column + count].isOccupied() && String.Equals(slot[row, column + count].getoccupiedPlayer(), player.getPlayername()))
                {
                    hrWin++;
                }
                else
                {
                    break;
                }
            }
            // }


            // checking vertical scope upwards
            // if ((row + 3) < 6)
            // {
            for (count = 1; (row + count) < 6 && count <= 3; count++)
            {
                // Checking must block situation
                if (slot[row + count, column].isOccupied() && String.Equals(slot[row + count, column].getoccupiedPlayer(), player.getPlayername()))
                {
                    vuWin++;
                }
                else
                {
                    break;
                }
            }

            // checking vertical scope downwards
            // if ((row - 3) >= 0)
            // {
            for (count = 1; (row - count) >= 0 && count <= 3; count++)
            {
                // Checking must block situation
                if (slot[row - count, column].isOccupied() && String.Equals(slot[row - count, column].getoccupiedPlayer(), player.getPlayername()))
                {
                    vdWin++;
                }
                else
                {
                    break;
                }
            }


            // checking diagonal scope upwards right
            // if ((row + 3) < 6 && (column + 3) < 7)
            // {
            for (count = 1; (row + count) < 6 && (column + count) < 7 && count <= 3; count++)
            {

                // checking must block situation
                if (slot[row + count, column + count].isOccupied() && String.Equals(slot[row + count, column + count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dneWin++;
                }
                else
                {
                    break;
                }
            }
            // checking diagonal scope upwards left
            for (count = 1; (row + count) < 6 && (column - count) >= 0 && count <= 3; count++)
            {
                // checking must block situation
                if (slot[row + count, column - count].isOccupied() && String.Equals(slot[row + count, column - count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dnwWin++;
                }
                else
                {
                    break;
                }
            }

            // checking diagonal scope downwards left

            for (count = 1; (row - count) >= 0 && (column - count) >= 0 && count <= 3; count++)
            {

                // checking must block situation
                if (slot[row - count, column - count].isOccupied() && String.Equals(slot[row - count, column - count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dswWin++;
                }
                else
                {
                    break;
                }
            }
            // checking diagonal scope downwards right
            for (count = 1; (row - count) >= 0 && (column + count) < 7 && count <= 3; count++)
            {

                // checking must block situation
                if (slot[row - count, column + count].isOccupied() && String.Equals(slot[row - count, column + count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dseWin++;
                }
                else
                {
                    break;
                }
            }
            // Console.Write("\n hlWin + hrWin "+ (hlWin + hrWin));
            // Console.Write("\n dneWin + dswWin "+(vuWin + vdWin));
            // Console.Write("\n dneWin + dswWin "+ (dneWin + dswWin));
            // Console.Write("\n dseWin + dnwWin "+ (dseWin + dnwWin));
            if ((hlWin + hrWin) == 2 ||
                            (vuWin + vdWin) == 2 ||
                            (dneWin + dswWin) == 2 ||
                            (dseWin + dnwWin) == 2)
            {
                winscore = winscore + 20 * winopportunity * (hlWin + hrWin + vuWin + vdWin + dneWin + dswWin + dseWin + dnwWin);
            }

            if ((hlWin + hrWin) > 2 ||
                            (vuWin + vdWin) > 2 ||
                            (dneWin + dswWin) > 2 ||
                            (dseWin + dnwWin) > 2)
            {
                winscore = +100000;
                win = true;
            }
            /* 
            else
            {
                winscore = winscore + 2000 * (hlWin + hrWin + vuWin + vdWin + dneWin + dswWin + dseWin + dnwWin);
            }
            */
            return winscore;
        }
    }
}
