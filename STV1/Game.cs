using System;
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
        private int difficulty;

        public Game()
        {
            difficulty = 1;
            dungeon = new Dungeon(difficulty);
            player = new Player(dungeon.Start, 25, 5, dungeon);
        }
        public bool Save(string filename)
        {
            return true;
        }

        public void Load(string filename)
        {

        }

        public Dungeon NextDungeon()
        {
            if (dungeon == null)
            {
                dungeon = new Dungeon(1);
                difficulty = 1;
                player.Location = dungeon.Start;
                
            }
            else
            {
                difficulty++;
                dungeon = new Dungeon(difficulty);
                player.Location = dungeon.Start;
            }
            return dungeon;
        }
    }
}
