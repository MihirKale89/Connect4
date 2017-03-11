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
    public partial class NewGameComputerVSHuman : Form
    {
        public Slot[,] slot = new Slot[6, 7];
        public Button[,] btn = new Button[6, 7];
        public Player p1 = new Player();
        public Player p2 = new Player();
        public Boolean win = false;
        public int movecount = 0;
        public int exploreopportunity = 10;
        public int blockoppurtunity = 500;
        public int winopportunity = 30;

        public NewGameComputerVSHuman()
        {
            InitializeComponent();
        }

        

        public void computermove()
        {
                Thread.Sleep(1000);
                this.Refresh();
                if (lblPlayerMove.Text.Equals(p1.getPlayername()))
                {
                    Console.WriteLine("Next move: player 1");
                    makemove(p1);
                    lblPlayerMove.Text = p2.getPlayername();
                }
                if (win)
                {
                    lblPlayerMove.Text = p1.getPlayername() + " won!";
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

            if (clickedButton == null) // just to be on the safe side
                return;
            if (String.Equals(lblPlayerMove.Text , p2.getPlayername()))
            {
                for (int row = 0; row < 6; row++)
                {
                    for(int column = 0; column < 7; column++)
                    {
                        if (clickedButton.Name.Equals("btn" + row + "" + column))
                        {
                            slot[row, column].setOccupied(true);
                            slot[row, column].setPlayable(false);
                            slot[row, column].setoccupiedPlayer(p2.getPlayername());
                            slot[row, column].setSlotColor(p2.getPlayerColor());
                            btn[row, column].BackColor = p2.getPlayerColor();
                            btn[row, column].Enabled = false;
                            //Console.WriteLine("Slot: "+slot[row, column].getSlotName()+" Score:"+slot[row, column].getScore());
                            if (row < 5)
                            {
                                slot[row + 1, column].setPlayable(true);
                                slot[row + 1, column].setSlotColor(Color.White);
                                btn[row + 1, column].BackColor = Color.White;
                                btn[row + 1, column].Enabled = true;
                            }

                            int p2winscore = winscore(row, column, p2);
                            lblPlayerMove.Text = p1.getPlayername();
                            if (win)
                            {
                                lblPlayerMove.Text = p2.getPlayername() + " wins!";
                            }
                            else
                            {
                                computermove();
                            }
                            //Thread.Sleep(1000);
                            this.Refresh();
                        }
                    }
                }
                
            }

        }


        private void NewGameComputerVSComputer_Load(object sender, EventArgs e)
        {
            // Button[,] btn = new Button[6, 7];
            InstantiateButtons(btn);
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

        public void clearScore()
        {
            for (int row = 5; row >= 0; row--)
            {
                for (int column = 6; column >= 0; column--)
                {
                    slot[row, column].setScore(0);
                }
            }
        }

        public void makemove(Player player)
        {
            int score = 0;
            int count = 0;
            int hlCount, hrCount, vuCount, vdCount, dneCount, dswCount, dnwCount, dseCount, hlbCount, hrbCount, vubCount, vdbCount, dnebCount, dswbCount, dnwbCount, dsebCount;
            int hleCount, hreCount, vueCount, vdeCount, dneeCount, dsweCount, dnweCount, dseeCount;
            hlCount = hrCount = vuCount = vdCount = dneCount = dswCount = dnwCount = dseCount = hlbCount = hrbCount = vubCount = vdbCount = dnebCount = dswbCount = dnwbCount = dsebCount = 0;
            hleCount = hreCount = vueCount = vdeCount = dneeCount = dsweCount = dnweCount = dseeCount = 0;
            Boolean stopWinCheck, stopBlockCheck;
            stopWinCheck = stopBlockCheck = false;
            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 7; column++)
                {

                    if (slot[row, column].isPlayable())
                    {
                        // checking horizontal scope towards left
                        // if ((column - 3) >= 0)
                        //  {
                        for (count = 1; (column - count) >= 0 && count <= 3; count++)
                        {
                            // Checking exploring opportunity
                            if (!slot[row, column - count].isOccupied())
                            {
                                score = score + exploreopportunity;
                                hleCount++;
                                // stopWinCheck = true;
                            }
                            // Checking winning opportunity
                            if (slot[row, column - count].isOccupied() && String.Equals(slot[row, column - count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + winopportunity;
                                stopBlockCheck = true;
                                if (!stopWinCheck)
                                {
                                    hlCount++;
                                }
                            }
                            // Checking must block situation
                            if (slot[row, column - count].isOccupied() && !String.Equals(slot[row, column - count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + blockoppurtunity;
                                stopWinCheck = true;
                                if (!stopBlockCheck)
                                {
                                    hlbCount++;
                                }
                            }
                        }
                        // }
                        stopBlockCheck = stopWinCheck = false;
                        // checking horizontal scope towards right
                        // if ((column + 3) < 7)
                        // {
                        for (count = 1; (column + count) < 7 && count <= 3; count++)
                        {
                            // Checking exploring opportunity
                            if (!slot[row, column + count].isOccupied())
                            {
                                score = score + exploreopportunity;
                                // stopWinCheck = true;
                                hreCount++;
                            }

                            // Checking winning opportunity
                            if (slot[row, column + count].isOccupied() && String.Equals(slot[row, column + count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + winopportunity;
                                stopBlockCheck = true;
                                if (!stopWinCheck)
                                {
                                    hrCount++;
                                }
                            }

                            // Checking must block situation
                            if (slot[row, column + count].isOccupied() && !String.Equals(slot[row, column + count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + blockoppurtunity;
                                stopWinCheck = true;
                                if (!stopBlockCheck)
                                {
                                    hrbCount++;
                                }
                            }
                        }
                        // }

                        stopBlockCheck = stopWinCheck = false;
                        // checking vertical scope upwards
                        // if ((row + 3) < 6)
                        // {
                        for (count = 1; (row + count) < 6 && count <= 3; count++)
                        {
                            // Checking exploring opportunity
                            if (!slot[row + count, column].isOccupied())
                            {
                                score = score + exploreopportunity;
                                vueCount++;
                                // stopWinCheck = true;
                            }
                            // Checking winning opportunity
                            if (slot[row + count, column].isOccupied() && String.Equals(slot[row + count, column].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + winopportunity;
                                stopBlockCheck = true;
                                if (!stopWinCheck)
                                {
                                    vuCount++;
                                }
                            }
                            // Checking must block situation
                            if (slot[row + count, column].isOccupied() && !String.Equals(slot[row + count, column].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + blockoppurtunity;
                                stopWinCheck = true;
                                if (!stopWinCheck)
                                {
                                    vubCount++;
                                }
                            }
                        }
                        // }
                        stopBlockCheck = stopWinCheck = false;
                        // checking vertical scope downwards
                        // if ((row - 3) >= 0)
                        // {
                        for (count = 1; (row - count) >= 0 && count <= 3; count++)
                        {
                            // Checking exploring opportunity
                            if (!slot[row - count, column].isOccupied())
                            {
                                score = score + exploreopportunity;
                                vdeCount++;
                                // stopWinCheck = true;
                            }
                            // Checking winning opportunity
                            if (slot[row - count, column].isOccupied() && String.Equals(slot[row - count, column].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + winopportunity;
                                stopBlockCheck = true;
                                if (!stopWinCheck)
                                {
                                    vdCount++;
                                }
                            }

                            // Checking must block situation
                            if (slot[row - count, column].isOccupied() && !String.Equals(slot[row - count, column].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + blockoppurtunity;
                                stopWinCheck = true;
                                if (!stopBlockCheck)
                                {
                                    vdbCount++;
                                }
                                // Console.WriteLine("---- I am executed for slot " + slot[row, column].getSlotname());
                            }
                        }
                        // }
                        stopBlockCheck = stopWinCheck = false;
                        // checking diagonal scope upwards right
                        // if ((row + 3) < 6 && (column + 3) < 7)
                        // {
                        for (count = 1; (row + count) < 6 && (column + count) < 7 && count <= 3; count++)
                        {
                            // checking exploring opportunity
                            if (!slot[row + count, column + count].isOccupied())
                            {
                                score = score + exploreopportunity;
                                dneeCount++;
                                // stopWinCheck = true;
                            }

                            // checking winning opportunity
                            if (slot[row + count, column + count].isOccupied() && String.Equals(slot[row + count, column + count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + winopportunity;
                                stopBlockCheck = true;
                                if (!stopWinCheck)
                                {
                                    dneCount++;
                                }
                            }

                            // checking must block situation
                            if (slot[row + count, column + count].isOccupied() && !String.Equals(slot[row + count, column + count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + blockoppurtunity;
                                stopWinCheck = true;
                                if (!stopBlockCheck)
                                {
                                    dnebCount++;
                                }
                            }
                        }
                        // }
                        stopBlockCheck = stopWinCheck = false;
                        // checking diagonal scope upwards left
                        // if ((row + 3) < 6 && (column - 3) >= 0)
                        // {
                        for (count = 1; (row + count) < 6 && (column - count) >= 0 && count <= 3; count++)
                        {
                            // checking exploring opportunity
                            if (!slot[row + count, column - count].isOccupied())
                            {
                                score = score + exploreopportunity;
                                dnweCount++;
                                // stopWinCheck = true;
                            }
                            // checking winning opportunity
                            if (slot[row + count, column - count].isOccupied() && String.Equals(slot[row + count, column - count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + winopportunity;
                                stopBlockCheck = true;
                                if (!stopWinCheck)
                                {
                                    dnwCount++;
                                }
                            }
                            // checking must block situation
                            if (slot[row + count, column - count].isOccupied() && !String.Equals(slot[row + count, column - count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + blockoppurtunity;
                                stopWinCheck = true;
                                if (!stopBlockCheck)
                                {
                                    dnwbCount++;
                                }
                            }
                        }
                        // }
                        // checking diagonal scope downwards left
                        stopBlockCheck = stopWinCheck = false;
                        // if ((row - 3) >= 0 && (column - 3) >= 0)
                        // {
                        for (count = 1; (row - count) >= 0 && (column - count) >= 0 && count <= 3; count++)
                        {
                            // checking exploring opportunity
                            if (!slot[row - count, column - count].isOccupied())
                            {
                                score = score + exploreopportunity;
                                dsweCount++;
                                // stopWinCheck = true;
                            }

                            // checking winning opportunity
                            if (slot[row - count, column - count].isOccupied() && String.Equals(slot[row - count, column - count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + winopportunity;
                                stopBlockCheck = true;
                                if (!stopWinCheck)
                                {
                                    dswCount++;
                                }
                            }

                            // checking must block situation
                            if (slot[row - count, column - count].isOccupied() && !String.Equals(slot[row - count, column - count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + blockoppurtunity;
                                stopWinCheck = true;
                                if (!stopBlockCheck)
                                {
                                    dswbCount++;
                                }
                            }
                        }
                        // }
                        stopBlockCheck = stopWinCheck = false;
                        // checking diagonal scope downwards right
                        // if ((row - 3) >= 0 && (column + 3) < 7)
                        // {
                        for (count = 1; (row - count) >= 0 && (column + count) < 7 && count <= 3; count++)
                        {
                            // checking exploring opportunity
                            if (!slot[row - count, column + count].isOccupied())
                            {
                                score = score + exploreopportunity;
                                dseeCount++;
                                // stopWinCheck = true;
                            }

                            // checking winning opportunity                            
                            if (slot[row - count, column + count].isOccupied() && String.Equals(slot[row - count, column + count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + winopportunity;
                                stopBlockCheck = true;
                                if (!stopWinCheck)
                                {
                                    dseCount++;
                                }
                            }

                            // checking must block situation
                            if (slot[row - count, column + count].isOccupied() && !String.Equals(slot[row - count, column + count].getoccupiedPlayer(), player.getPlayername()))
                            {
                                score = score + blockoppurtunity;
                                stopWinCheck = true;
                                if (!stopBlockCheck)
                                {
                                    dsebCount++;
                                }
                            }
                        }
                        // }
                       
                        
                        // score = score - loseforkscore(row + 1, column, player) + winscore(row, column, player);
                        if (row < 5)
                        {
                            if(threatscore(row, column, player) >= 50000 || winscore(row, column, player) >= 100000)
                            {
                                score = 2000000;
                            }
                            else
                            {
                                score = score - threatscore(row + 1, column, player) / 5 - winthreatscore(row + 1, column, player) / 5 + threatscore(row, column, player) * 2 + winscore(row, column, player) * 2 + losingfork(row, column, player) * 2 + winningfork(row, column, player) * 2;
                            }                          
                        }
                        else
                        {
                            score = score + threatscore(row, column, player) + winscore(row, column, player) + losingfork(row, column, player) + winningfork(row, column, player);
                        }
                        slot[row, column].setScore(score);

                        Console.WriteLine(" The score for the slot " + slot[row, column].getSlotname() + " is: " + score);
                        score = hlCount = hrCount = vuCount = vdCount = dneCount = dswCount = dnwCount = dseCount = hlbCount = hrbCount = vubCount = vdbCount = dnebCount = dswbCount = dnwbCount = dsebCount = 0;
                        hleCount = hreCount = vueCount = vdeCount = dneeCount = dsweCount = dnweCount = dseeCount = 0;
                    }
                }
            }

            Slot s = new Slot();
            s.setScore(-99999);
            // finding top score
            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 7; column++)
                {
                    if (slot[row, column].isPlayable())
                    {
                        if (s.getScore() < slot[row, column].getScore())
                        {
                            s = slot[row, column];
                            //Console.WriteLine("Slot: "+slot[row, column].getSlotName()+" Score:"+slot[row, column].getScore());
                        }
                    }
                }
            }

            // making move and making new slot playable
            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 7; column++)
                {
                    if (slot[row, column].isPlayable())
                    {
                        if (s.getScore() == slot[row, column].getScore())
                        {

                            slot[row, column].setOccupied(true);
                            slot[row, column].setPlayable(false);
                            slot[row, column].setoccupiedPlayer(player.getPlayername());
                            slot[row, column].setSlotColor(player.getPlayerColor());
                            btn[row, column].BackColor = player.getPlayerColor();
                            btn[row, column].Enabled = false;
                            btn[row, column].Enabled = false; 
                            //Console.WriteLine("Slot: "+slot[row, column].getSlotName()+" Score:"+slot[row, column].getScore());
                            if (row < 5)
                            {
                                slot[row + 1, column].setPlayable(true);
                                slot[row + 1, column].setSlotColor(Color.White);
                                btn[row + 1, column].BackColor = Color.White;
                                btn[row + 1, column].Enabled = true;
                            }
                            //Thread.Sleep(1000);
                            // this.Refresh();
                            return;
                        }
                    }
                }
            }

            // displayGrid();
            clearScore();
            score = hlCount = hrCount = vuCount = vdCount = dneCount = dswCount = dnwCount = dseCount = hlbCount = hrbCount = vubCount = vdbCount = dnebCount = dswbCount = dnwbCount = dsebCount = 0;
            hleCount = hreCount = vueCount = vdeCount = dneeCount = dsweCount = dnweCount = dseeCount = 0;
        }

        public int threatscore(int row, int column, Player player)
        {
            int threatscore = 0;
            int count = 0;
            int hlThreat, hrThreat, vuThreat, vdThreat, dneThreat, dswThreat, dnwThreat, dseThreat;
            hlThreat = hrThreat = vuThreat = vdThreat = dneThreat = dswThreat = dnwThreat = dseThreat = 0;
            // checking horizontal scope towards left
            // if ((column - 3) >= 0)
            //  {
            for (count = 1; (column - count) >= 0 && count <= 3; count++)
            {

                // Checking must block situation
                if (slot[row, column - count].isOccupied() && !String.Equals(slot[row, column - count].getoccupiedPlayer(), player.getPlayername()))
                {
                    hlThreat++;
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
                if (slot[row, column + count].isOccupied() && !String.Equals(slot[row, column + count].getoccupiedPlayer(), player.getPlayername()))
                {
                    hrThreat++;
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
                if (slot[row + count, column].isOccupied() && !String.Equals(slot[row + count, column].getoccupiedPlayer(), player.getPlayername()))
                {
                    vuThreat++;
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
                if (slot[row - count, column].isOccupied() && !String.Equals(slot[row - count, column].getoccupiedPlayer(), player.getPlayername()))
                {
                    vdThreat++;
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
                if (slot[row + count, column + count].isOccupied() && !String.Equals(slot[row + count, column + count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dneThreat++;
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
                if (slot[row + count, column - count].isOccupied() && !String.Equals(slot[row + count, column - count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dnwThreat++;
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
                if (slot[row - count, column - count].isOccupied() && !String.Equals(slot[row - count, column - count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dswThreat++;
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
                if (slot[row - count, column + count].isOccupied() && !String.Equals(slot[row - count, column + count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dseThreat++;
                }
                else
                {
                    break;
                }
            }

            /*
            Console.Write("\n hlThreat + hrThreat " + (hlThreat + hrThreat));
            Console.Write("\n vuThreat + vdThreat " + (vuThreat + vdThreat));
            Console.Write("\n dneThreat + dswThreat " + (dneThreat + dswThreat));
            Console.Write("\n dseThreat + dnwThreat " + (dseThreat + dnwThreat));
            */

            if ((hlThreat + hrThreat) <= 2 ||
                            (vuThreat + vdThreat) <= 2 ||
                            (dneThreat + dswThreat) <= 2 ||
                            (dseThreat + dnwThreat) <= 2)
            {
                 threatscore = threatscore + 15 * blockoppurtunity * (hlThreat + hrThreat + vuThreat + vdThreat + dneThreat + dswThreat + dseThreat + dnwThreat);
                // loseforkscore = loseforkscore + 1000;
            }

            if ((hlThreat + hrThreat) > 2 ||
                            (vuThreat + vdThreat) > 2 ||
                            (dneThreat + dswThreat) > 2 ||
                            (dseThreat + dnwThreat) > 2)
            {
                threatscore = threatscore + 55000;
            }

            return threatscore;
        }

        public int winthreatscore(int row, int column, Player player)
        {
            int winthreatscore = 0;
            int count = 0;
            int hlThreat, hrThreat, vuThreat, vdThreat, dneThreat, dswThreat, dnwThreat, dseThreat;
            hlThreat = hrThreat = vuThreat = vdThreat = dneThreat = dswThreat = dnwThreat = dseThreat = 0;
            // checking horizontal scope towards left
            // if ((column - 3) >= 0)
            //  {
            for (count = 1; (column - count) >= 0 && count <= 3; count++)
            {

                // Checking must block situation
                if (slot[row, column - count].isOccupied() && String.Equals(slot[row, column - count].getoccupiedPlayer(), player.getPlayername()))
                {
                    hlThreat++;
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
                    hrThreat++;
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
                    vuThreat++;
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
                    vdThreat++;
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
                    dneThreat++;
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
                    dnwThreat++;
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
                    dswThreat++;
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
                    dseThreat++;
                }
                else
                {
                    break;
                }
            }

            /*
            Console.Write("\n hlThreat + hrThreat " + (hlThreat + hrThreat));
            Console.Write("\n vuThreat + vdThreat " + (vuThreat + vdThreat));
            Console.Write("\n dneThreat + dswThreat " + (dneThreat + dswThreat));
            Console.Write("\n dseThreat + dnwThreat " + (dseThreat + dnwThreat));
            */

            if ((hlThreat + hrThreat) == 2 ||
                            (vuThreat + vdThreat) == 2 ||
                            (dneThreat + dswThreat) == 2 ||
                            (dseThreat + dnwThreat) == 2)
            {
                winthreatscore = winthreatscore + 15 * blockoppurtunity * (hlThreat + hrThreat + vuThreat + vdThreat + dneThreat + dswThreat + dseThreat + dnwThreat);
                // loseforkscore = loseforkscore + 1000;
            }

            if ((hlThreat + hrThreat) > 2 ||
                            (vuThreat + vdThreat) > 2 ||
                            (dneThreat + dswThreat) > 2 ||
                            (dseThreat + dnwThreat) > 2)
            {
                winthreatscore = +30000;
            }

            return winthreatscore;
        }

        public int losingfork(int row, int column, Player player)
        {
            int loseforkscore = 0;
            int count = 0;
            int hlThreat, hrThreat, vuThreat, vdThreat, dneThreat, dswThreat, dnwThreat, dseThreat;
            Boolean hlOpen, hrOpen, vuOpen, vdOpen, dneOpen, dswOpen, dnwOpen, dseOpen;
            hlThreat = hrThreat = vuThreat = vdThreat = dneThreat = dswThreat = dnwThreat = dseThreat = 0;
            hlOpen = hrOpen = vuOpen = vdOpen = dneOpen = dswOpen = dnwOpen = dseOpen = false;
            // checking horizontal scope towards left
            // if ((column - 3) >= 0)
            //  {
            for (count = 1; (column - count) >= 0 && count <= 3; count++)
            {

                // Checking must block situation
                if (slot[row, column - count].isOccupied() && !String.Equals(slot[row, column - count].getoccupiedPlayer(), player.getPlayername()))
                {
                    hlThreat++;
                }
                else
                {
                    if(!slot[row, column - count].isOccupied() && slot[row, column - count].isPlayable())
                    {
                        hlOpen = true;
                    }
                    break;
                }
            }

            // checking horizontal scope towards right
            // if ((column + 3) < 7)
            // {
            for (count = 1; (column + count) < 7 && count <= 3; count++)
            {

                // Checking must block situation
                if (slot[row, column + count].isOccupied() && !String.Equals(slot[row, column + count].getoccupiedPlayer(), player.getPlayername()))
                {
                    hrThreat++;
                }
                else
                {
                    if(!slot[row, column + count].isOccupied() && slot[row, column + count].isPlayable())
                    {
                        hrOpen = true;
                    }
                    break;
                }
            }
            // }

            // vertical fork impossible
            /*
            // checking vertical scope upwards
            // if ((row + 3) < 6)
            // {
            for (count = 1; (row + count) < 6 && count <= 3; count++)
            {
                // Checking must block situation
                if (slot[row + count, column].isOccupied() && !String.Equals(slot[row + count, column].getoccupiedPlayer(), player.getPlayername()))
                {
                    vuThreat++;
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
                if (slot[row - count, column].isOccupied() && !String.Equals(slot[row - count, column].getoccupiedPlayer(), player.getPlayername()))
                {
                    vdThreat++;
                }
                else
                {
                    break;
                }
            }
            */

            // checking diagonal scope upwards right
            // if ((row + 3) < 6 && (column + 3) < 7)
            // {
            for (count = 1; (row + count) < 6 && (column + count) < 7 && count <= 3; count++)
            {

                // checking must block situation
                if (slot[row + count, column + count].isOccupied() && !String.Equals(slot[row + count, column + count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dneThreat++;
                }
                else
                {
                    if(!slot[row + count, column + count].isOccupied() && slot[row + count, column + count].isPlayable())
                    {
                        dneOpen = true;
                    }
                    break;
                }
            }
            // checking diagonal scope upwards left
            for (count = 1; (row + count) < 6 && (column - count) >= 0 && count <= 3; count++)
            {
                // checking must block situation
                if (slot[row + count, column - count].isOccupied() && !String.Equals(slot[row + count, column - count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dnwThreat++;
                }
                else
                {
                    if(!slot[row + count, column - count].isOccupied()  && slot[row + count, column - count].isPlayable())
                    {
                        dnwOpen = true;
                    }
                    break;
                }
            }

            // checking diagonal scope downwards left

            for (count = 1; (row - count) >= 0 && (column - count) >= 0 && count <= 3; count++)
            {

                // checking must block situation
                if (slot[row - count, column - count].isOccupied() && !String.Equals(slot[row - count, column - count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dswThreat++;
                }
                else
                {
                    if(!slot[row - count, column - count].isOccupied() && slot[row - count, column - count].isPlayable())
                    {
                        dswOpen = true;
                    }
                    break;
                }
            }
            // checking diagonal scope downwards right
            for (count = 1; (row - count) >= 0 && (column + count) < 7 && count <= 3; count++)
            {

                // checking must block situation
                if (slot[row - count, column + count].isOccupied() && !String.Equals(slot[row - count, column + count].getoccupiedPlayer(), player.getPlayername()))
                {
                    dseThreat++;
                }
                else
                {
                    if(!slot[row - count, column + count].isOccupied() && slot[row - count, column + count].isPlayable())
                    {
                        dseOpen = true;
                    }
                    break;
                }
            }           

            if (            ((hlThreat + hrThreat) == 2 && hlOpen && hrOpen) ||                            
                            ((dneThreat + dswThreat) == 2 && dneOpen && dswOpen) ||
                            ((dseThreat + dnwThreat) == 2) && dseOpen && dnwOpen)
            {
                loseforkscore = 20000;
                // loseforkscore = loseforkscore + 1000;
            }

            return loseforkscore;
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

        public int winningfork(int row, int column, Player player)
        {
            int winforkscore = 0;
            int count = 0;
            int hlWin, hrWin, vuWin, vdWin, dneWin, dswWin, dnwWin, dseWin;
            Boolean hlOpen, hrOpen, vuOpen, vdOpen, dneOpen, dswOpen, dnwOpen, dseOpen;
            hlWin = hrWin = vuWin = vdWin = dneWin = dswWin = dnwWin = dseWin = 0;
            hlOpen = hrOpen = vuOpen = vdOpen = dneOpen = dswOpen = dnwOpen = dseOpen = false;
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
                    if (!slot[row, column - count].isOccupied() && slot[row, column - count].isPlayable())
                    {
                        hlOpen = true;
                    }
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
                    if (!slot[row, column + count].isOccupied() && slot[row, column + count].isPlayable())
                    {
                        hrOpen = true;
                    }
                    break;
                }
            }
            // }

            // vertical fork impossible
            /*
            // checking vertical scope upwards
            // if ((row + 3) < 6)
            // {
            for (count = 1; (row + count) < 6 && count <= 3; count++)
            {
                // Checking must block situation
                if (slot[row + count, column].isOccupied() && !String.Equals(slot[row + count, column].getoccupiedPlayer(), player.getPlayername()))
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
                if (slot[row - count, column].isOccupied() && !String.Equals(slot[row - count, column].getoccupiedPlayer(), player.getPlayername()))
                {
                    vdWin++;
                }
                else
                {
                    break;
                }
            }
            */

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
                    if (!slot[row + count, column + count].isOccupied()  && slot[row + count, column + count].isPlayable())
                    {
                        dneOpen = true;
                    }
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
                    if (!slot[row + count, column - count].isOccupied()  && slot[row + count, column - count].isPlayable())
                    {
                        dnwOpen = true;
                    }
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
                    if (!slot[row - count, column - count].isOccupied() && slot[row - count, column - count].isPlayable())
                    {
                        dswOpen = true;
                    }
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
                    if (!slot[row - count, column + count].isOccupied() && slot[row - count, column + count].isPlayable())
                    {
                        dseOpen = true;
                    }
                    break;
                }
            }

            if (((hlWin + hrWin) >= 2 && !(hlOpen || hrOpen)) ||
                            ((dneWin + dswWin) >= 2 && !(dneOpen || dswOpen)) ||
                            ((dseWin + dnwWin) >= 2) && !(dseOpen || dnwOpen))
            {
                winforkscore -= 5000;
                // winforkscore = winforkscore + 1000;
            }

            if (((hlWin + hrWin) >= 2 && (hlOpen || hrOpen)) ||
                            ((dneWin + dswWin) >= 2 && (dneOpen || dswOpen)) ||
                            ((dseWin + dnwWin) >= 2) && (dseOpen || dnwOpen))
            {
                winforkscore += 5000;
                // winforkscore = winforkscore + 1000;
            }

            if (((hlWin + hrWin) == 2 && hlOpen && hrOpen) ||
                            ((dneWin + dswWin) == 2 && dneOpen && dswOpen) ||
                            ((dseWin + dnwWin) == 2) && dseOpen && dnwOpen)
            {
                winforkscore += 24000;
                // winforkscore = winforkscore + 1000;
            }

            return winforkscore;
        }

        private void NewGameComputerVSHuman_Load(object sender, EventArgs e)
        {
            InstantiateButtons(btn);
            callonload();
            callonstart();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            win = false;
            callonload();
            clearbuttons();
            callonstart();
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
                computermove();
            }            
        }

        private void clearbuttons()
        {
            for(int row = 0; row < 6; row++)
            {
                for(int column = 0; column < 7; column++)
                {
                    btn[row, column].BackColor = Color.Black;
                    btn[row, column].Enabled = false;
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }
    }
}
