using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public class TimeCrystal : Item
    {
        // TODO: Implement method.
        public override void Use(Player player)
        {
            Dungeon dungeon = player.Dungeon;
            Node bridge = player.Location;

            bool onbridge = dungeon.IsBridge(bridge);
            bool combat = false;

            // Destroy the bridge and nodes behind it.
            if (onbridge)
            {
                // Get the nodes that possibly have a path to the exit after the destruction.
                List<Node> possibleSafeNodes = new List<Node>();
                foreach (Node n in bridge.ConnectedNodes)
                {
                    possibleSafeNodes.Add(n);
                }

                dungeon.Destroy(bridge);

                // Go through all exit-path-possible nodes, and move the player to the first one that has an exit.
                foreach (Node n in possibleSafeNodes)
                {
                    if (Dungeon.PathExists(n, dungeon.Exit))
                    {
                        player.Location = n;
                        break;
                    }
                }

                return;
            }

            // Apply damage to all creatures in enemy pack, instead of to just one.
            if (combat)
            {
                
            }
        }
    }
}
