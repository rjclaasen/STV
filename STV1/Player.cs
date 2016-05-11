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
        private Dungeon dungeon;

        /// <summary>
        /// Creates a Player instance.
        /// </summary>
        /// <param name="location">The location the Player starts at.</param>
        /// <param name="hitPoints">The amount of hit points the Player has.</param>
        /// <param name="attackRating">The attack rating of the Player.</param>
        /// <param name="dungeon">The Dungeon the player is in. Can be null during testing sometimes.</param>
        public Player(Node location, float hitPoints, float attackRating, Dungeon dungeon)
            : base(location, hitPoints, attackRating)
        {
            maxHitPoints = hitPoints;
            HitPoints = hitPoints;
            this.dungeon = dungeon;

            location.PlayerEnters(this);
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

        public Dungeon Dungeon
        {
            get { return dungeon; }
        }
    }
}
