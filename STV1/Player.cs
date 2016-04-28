using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public class Player : Creature
    {
        private float maxHitPoints;
        private int killPoints;
        private List<Item> bag;
        Queue<Command> commands;

        public Player(Node location, float hitPoints, float attackRating)
            : base(location, hitPoints, attackRating)
        {
            maxHitPoints = hitPoints;
        }

        public override void Move(Node target)
        {
            Location.PlayerLeaves();
            target.PlayerEnters(this);
            base.Move(target);
        }

        // TODO: finish this method.
        public void GetCommand() {
            Command currentCommand = commands.Dequeue();
        }

        protected override void Die() { }
    }
}
