﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public class Game
    {
        private Dungeon dungeon;
        private Player player;
        private List<Pack> packs;
        private List<Item> items;

        public bool Save(string filename)
        {
            return true;
        }

        public void Load(string filename)
        {

        }

        public void NextDungeon()
        {
            if (dungeon == null)
            {

            }
            else
            {

            }
        }
    }
}
