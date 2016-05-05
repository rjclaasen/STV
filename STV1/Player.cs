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
            HitPoints = hitPoints;
        }

        public override void Move(Node target)
        {
            Location.PlayerLeaves();
            target.PlayerEnters(this);
            base.Move(target);
        }

        public void PickUp(Item item)
        {
            bag.Add(item);
        }

        // TODO: finish this method.
        public virtual void GetCommand()
        {
            Command currentCommand = commands.Dequeue();
        }

        protected override void Die() { }

        public override float HitPoints
        {
            get { return base.HitPoints; }
            set
            {
                if (value > maxHitPoints)
                    base.HitPoints = maxHitPoints;
                else
                    base.HitPoints = value;
            }
        }
    }
}
