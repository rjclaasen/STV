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
        private Game game;

        /// <summary>
        /// Creates a Player instance.
        /// </summary>
        /// <param name="location">The location the Player starts at.</param>
        /// <param name="hitPoints">The amount of hit points the Player has.</param>
        /// <param name="attackRating">The attack rating of the Player.</param>
        /// <param name="dungeon">The Dungeon the player is in. Can be null during testing sometimes.</param>
        public Player(Node location, float hitPoints, float attackRating, Dungeon dungeon, Game game)
            : base(location, hitPoints, attackRating)
        {
            maxHitPoints = hitPoints;
            HitPoints = hitPoints;
            this.dungeon = dungeon;
            this.game = game;
            location.PlayerEnters(this);
        }

        public override void Move(Node target)
        {
            if (target == dungeon.Exit)
            { game.NextDungeon(); }
            Location.PlayerLeaves();
            target.PlayerEnters(this);
            base.Move(target);
        }

        public override void Attack(Creature target)
        {
            base.Attack(target);
            if (target.IsDead())
                killPoints++;
        }

        public void PickUp(Item item)
        {
            bag.Add(item);
        }

        // TODO: finish this method.
        public Command GetCommand()
        {
            Command currentCommand = commands.Dequeue();
            return currentCommand;
        }

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

        public bool UseTimeCrystal()
        {
            foreach (Item item in bag)
                if (item.GetType() == typeof(TimeCrystal))
                {
                    bag.Remove(item);
                    item.Use(this);
                    return true;
                }
            return false;
        }

        public bool UseHealingPotion()
        {
            foreach (Item item in bag)
                if (item.GetType() == typeof(HealingPotion))
                {
                    bag.Remove(item);
                    item.Use(this);
                    return true;
                }
            return false;
        }

        public Dungeon Dungeon
        {
            get { return dungeon; }
        }
    }
}
