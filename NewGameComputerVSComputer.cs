using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class NewGameComputerVSComputer : Form
    {
        public Slot[,] slot = new Slot[6, 7];
        public Button[,] btn = new Button[6, 7];
        public Player p1 = new Player();
        public Player p2 = new Player();
        public Boolean win = false;
        public int movecount = 0;
        public int exploreopportunity = 10;
        public int blockoppurtunity = 30;
        public int winopportunity = 50;

        public NewGameComputerVSComputer()
        {
            InitializeComponent();
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
                slot[0, i].setSlotColor(Color.White);
                btn[0, i].BackColor = Color.White;
                Thread.Sleep(500);
                this.Refresh();
            }

            // Boolean player1move = true;

            while (!win)
            {
                Thread.Sleep(1000);
                this.Refresh();
                if (movecount % 2 == 0)
                {
                    Console.WriteLine("Next move: player 1");
                    makemove(p1);
                }
                else
                {
                    Console.WriteLine("Next move: player 2");
                    makemove(p2);
                }
                movecount++;

            }
        }

        public void InstantiateButtons(Button[,] buttons)
        {
            int rows = buttons.GetUpperBound(0) + 1; // Getting the number of rows in the array
            int columns = buttons.GetUpperBound(buttons.Rank - 1) + 1; // Getting the number of columns in the array
            int left = 130;
            int top = 200;
            // Instantiating all the buttons in the array
            for (int i = rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < columns; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Visible = true;
                    buttons[i, j].Left = left;
                    buttons[i, j].Top = top;
                    buttons[i, j].BackColor = Color.Black;
                    buttons[i, j].Height = 25;
                    buttons[i, j].Width = 50;
                    this.Controls.Add(buttons[i, j]);
                    left += 50 + 2;
                    // Button button = new Button();                                        
                }
                top += 25 + 2;
                left = 130;
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
                                Console.WriteLine("---- I am executed for slot " + slot[row, column].getSlotname());
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
                        // checking winning move 
                        /*
                        Console.WriteLine("Checking hlCount " + hlCount);
                        Console.WriteLine("Checking vuCount " + vuCount);
                        Console.WriteLine("Checking dneCount + dswCount " + dneCount + dswCount);
                        Console.WriteLine("Checking dseCount + dnwCount " + dseCount + dnwCount);
                        
                        if ((hlCount+hrCount >= 3  ) 
                            || (vuCount + vdCount >= 3 ) 
                            || ((dneCount + dswCount) >= 3 )
                            || ((dseCount + dnwCount) >= 3 ))

                        {
                            // win = true;
                            // Console.WriteLine(" This is the winning move");                           
                            // Console.WriteLine(" The winning player is " + player.getPlayername());
                            score = score + 1000 *(hlCount + hrCount + vuCount + vdCount + dneCount + dswCount + dseCount + dnwCount) ;
                        }

                        // checking must block move
                        if ( (hlbCount+hrbCount) >= 2 || 
                            (vubCount+vdbCount) >= 2  || 
                            (dnebCount + dswbCount) >= 2  ||
                            (dsebCount + dnwbCount) >= 2 )
                        {
                            score = score + 2000*(hlbCount + hrbCount + vubCount + vdbCount + dnebCount + dswbCount + dsebCount + dnwbCount);
                        }
                        */
                        // score = score - threatscore(row + 1, column, player) + winscore(row, column, player);
                        if(row < 5)
                        {
                            score = score - threatscore(row + 1, column, player) + threatscore(row, column, player) + winscore(row, column, player);
                        }
                        else
                        {
                            score = score + threatscore(row, column, player) + winscore(row, column, player);
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
                            //Console.WriteLine("Slot: "+slot[row, column].getSlotName()+" Score:"+slot[row, column].getScore());
                            if (row < 5)
                            {
                                slot[row + 1, column].setPlayable(true);
                                slot[row + 1, column].setSlotColor(Color.White);
                                btn[row + 1, column].BackColor = Color.White;
                            }
                            Thread.Sleep(1000);
                            this.Refresh();
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

            if ((hlThreat + hrThreat) <= 2 ||
                            (vuThreat + vdThreat) <= 2 ||
                            (dneThreat + dswThreat) <= 2 ||
                            (dseThreat + dnwThreat) <= 2)
            {
                threatscore = threatscore + 2*blockoppurtunity*(hlThreat + hrThreat + dneThreat + dswThreat + dseThreat + dnwThreat);
            }

            if ((hlThreat + hrThreat) > 2 ||
                            (vuThreat + vdThreat) > 2 ||
                            (dneThreat + dswThreat) > 2 ||
                            (dseThreat + dnwThreat) > 2)
            {
                threatscore = +30000;
            }

            return threatscore;
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

            if ((hlWin + hrWin) <= 2 ||
                            (vuWin + vdWin) <= 2 ||
                            (dneWin + dswWin) <= 2 ||
                            (dseWin + dnwWin) <= 2)
            {
                winscore = winscore + 2 * winopportunity * (vuWin + vdWin + dneWin + dswWin + dseWin + dnwWin);                
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

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }
    }
}
