using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Player
    {
        private String playername;
        private System.Drawing.Color playerColor;
        public Player()
        {

        }

        public void setPlayername(String pname)
        {
            this.playername = pname;
        }

        public String getPlayername()
        {
            return this.playername;
        }

        public void setPlayerColor(System.Drawing.Color c)
        {
            this.playerColor = c;
        }

        public System.Drawing.Color getPlayerColor()
        {
            return this.playerColor;
        }

    }
}
