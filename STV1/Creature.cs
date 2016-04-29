using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public abstract class Creature
    {
        private float hitPoints;
        private Node location;
        private float attackRating;
        protected bool dead = false;

        public Creature(Node location, float hitPoints, float attackRating)
        {
            HitPoints = hitPoints;
            Location = location;
            this.attackRating = attackRating;
        }

        public virtual void Move(Node target) 
        {
            Location = target;
        }

        public void Attack(Creature target)
        {
            target.HitPoints -= attackRating;
        }

        public bool IsDead()
        {
            return dead;
        }

        protected virtual void Die()
        {
            dead = true;
        }

        public float HitPoints
        {
            get { return hitPoints; }
            set
            {
                hitPoints = value;
                if (hitPoints <= 0)
                    Die();
            }
        }

        public Node Location
        {
            get { return location; }
            set { location = value; }
        }

        public float AttackRating
        {
            get { return attackRating; }
        }
    }
}
