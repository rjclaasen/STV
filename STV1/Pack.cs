using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public class Pack
    {
        List<Monster> monsters;

        /// <summary>
        /// Creates a pack of the specified size at the specified location. Optional parameters allow you to set the monster health and attack rating.
        /// </summary>
        /// <param name="size">The size of the pack, in amount of monsters.</param>
        /// <param name="location">The location of the pack.</param>
        /// <param name="monsterHealth">The health that the monsters will have. If not used, or set to -1, the default value specified in the Monster class will be used.</param>
        /// <param name="attackRating">The attack rating that the monster will have. If not used, or set to -1, the default value specified in the Monster class will be used.</param>
        public Pack(int size, Node location, int monsterHealth = -1, int attackRating = -1)
        {
            monsters = new List<Monster>();
            for (int i = 0; i < size; i++)
            {
                Monster m;
                if (monsterHealth != -1)
                {
                    if (attackRating != -1)
                        m = new Monster(location, monsterHealth, attackRating);
                    else
                        m = new Monster(location, monsterHealth);
                }
                else
                    m = new Monster(location);
                monsters.Add(m);
            }

            location.AddPack(this);
        }

        public bool Move(Node target)
        {
            if (Location.Adjacent(target) && target.PackFits(this))
            {
                target.PackEnters(this);
                Location.PackLeaves(this);
                foreach (Monster m in monsters)
                    m.Move(target);
                return true;
            }
            return false;
        }

        public Node Location
        {
            get { return monsters[0].Location; }
        }

        public int Size
        {
            get { return monsters.Count; }
        }

        public void Attack(Creature c)
        {
            foreach (Monster m in monsters)
                m.Attack(c);
        }
    }
}
