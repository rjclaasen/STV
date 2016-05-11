using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STV1;

namespace STV1_Tests
{
    class MockCreature : Creature
    {
        public MockCreature(Node location, float hitPoints = 2, float attackRating = 1)
            : base(location, hitPoints, attackRating) { }

        protected override void Die()
        {
            base.Die();
        }
    }
}
