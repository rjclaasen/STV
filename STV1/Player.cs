﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public class Player : Creature
    {
        private float maxHitPoints = float.MaxValue;
        private int killPoints;
        private List<Item> bag = new List<Item>();
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
            commands = new Queue<Command>();
            this.dungeon = dungeon;
            this.game = game;
        }

        public override void Move(Node target)
        {
            if (dungeon != null && target == dungeon.Exit)
            {
                dungeon = game.NextDungeon();
                return;
            }
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

        public Command GetCommand()
        {
            if (commands.Count > 0)
            {
            Command currentCommand = commands.Dequeue();
            return currentCommand;
        }
            return null;
        }

        public void SetCommand(Command command)
        {
            commands.Enqueue(command);
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

        public override Node Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                if(Location != null)
                    Location.PlayerLeaves();
                base.Location = value;
                if(value != null)
                    Location.PlayerEnters(this);
            }
        }

        public Dungeon Dungeon
        {
            get { return dungeon; }
        }

        public int KillPoints
        {
            get { return killPoints; }
        }
    }
}
