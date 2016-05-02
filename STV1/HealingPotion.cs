using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public class HealingPotion : Item
    {
        private float healValue = 10f;

        public override void Use(Player player)
        {
            player.HitPoints += healValue;
        }
    }
}
