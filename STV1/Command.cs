using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public class Command
    {
        public bool useItem;
        public bool timeCrystal;
        public bool healingPotion;
        public bool move;
        public bool retreat;
        public Node node;

        public Command(bool timeCrystal, bool healingPotion, bool move, bool retreat, Node node = null)
        {
            useItem = timeCrystal || healingPotion;
            this.timeCrystal = timeCrystal;
            this.healingPotion = healingPotion;
            this.move = move;
            this.node = node;
            this.retreat = retreat;
        }
    }
}
