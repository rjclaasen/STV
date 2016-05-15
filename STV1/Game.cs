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
            dungeon = NextDungeon();
            player = new Player(dungeon.Start, 25, 5, dungeon, this);
            dungeon.DistributeItems(player);
        }

        public Dungeon NextDungeon()
        {
            if (dungeon == null)
            {
                dungeon = new Dungeon(1);
                difficulty = 1;
                if (player != null)
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

        public Player Player
        {
            get { return player; }
        }

        public Dungeon Dungeon
        {
            get { return dungeon; }
        }
    }
}
