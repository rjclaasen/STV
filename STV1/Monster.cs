using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public class Monster : Creature
    {
        private const float DEFAULTHITPOINTS = 10;
        private const float DEFAULTATTACKRATING = 2;

        public Monster(Node location, float hitPoints = DEFAULTHITPOINTS, float attackRating = DEFAULTATTACKRATING)
            : base(location, hitPoints, attackRating) { }

        protected override void Die() { }
    }
}
