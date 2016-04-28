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

        public Pack(int size, Node location)
        {
            for (int i = 0; i < size; i++)
            {
                Monster m = new Monster(location);
                monsters.Add(m);
            }
        }

        public void Move(Node target)
        {
            if (Location.Adjacent(target) && Location.PackFits(this))
            {
                target.PackEnters(this);
                Location.PackLeaves(this);
                foreach (Monster m in monsters)
                    m.Move(target);
            }
        }

        public Node Location
        {
            get { return monsters[0].Location; }
        }
    }
}
