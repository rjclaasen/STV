using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public abstract class Item
    {
        // In which Player instance the item is. Null if none.
        protected Player player;

        public abstract void Use();

        public void PickUp(Player p)
        {
            player = p;
        }
    }
}
