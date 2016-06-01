using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Slot
    {
        private System.Drawing.Color slotColor;
        private String slotname;
        private String occupiedPlayer;
        private Boolean occupied;
        private Boolean playable;
        private int score;

        public Slot()
        {

        }

        public void setSlotColor(System.Drawing.Color color)
        {
            this.slotColor = color;
        }

        public System.Drawing.Color getSlotColor()
        {
            return this.slotColor;       
        }

        public void setSlotname(String slot)
        {
            this.slotname = slot;
        }

        public String getSlotname()
        {
            return this.slotname;
        }

        public void setoccupiedPlayer(String player)
        {
            this.occupiedPlayer = player;
        }

        public String getoccupiedPlayer()
        {
            return this.occupiedPlayer;
        }

        public void setOccupied(Boolean o)
        {
            this.occupied = o;
        }

        public Boolean isOccupied()
        {
            return this.occupied;
        }

        public void setPlayable(Boolean p)
        {
            this.playable = p;
        }

        public Boolean isPlayable()
        {
            return this.playable;
        }

        public void setScore(int score)
        {
            this.score = score;
        }

        public int getScore()
        {
            return this.score;
        }

    }
}
