using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public class Node
    {
        private List<Pack> packsInNode;
        private Player playerInNode;

        public Node()
        {
            packsInNode = new List<Pack>();
        }

        public void PackEnters(Pack enteringPack)
        {
            packsInNode.Add(enteringPack);
        }

        public void PackLeaves(Pack leavingPack)
        {
            packsInNode.Remove(leavingPack);
        }

        public void PlayerEnters(Player player)
        {
            playerInNode = player;
        }

        public void PlayerLeaves()
        {
            playerInNode = null;
        }

        // TODO: 
        public bool Adjacent(Node other)
        {
            return true;
        }

        // TODO: Check capacity
        public bool PackFits(Pack pack)
        {
            return true;
        }

        public bool PlayerInNode()
        {
            return playerInNode != null;
        }

        public List<Pack> PacksInNode
        {
            get { return packsInNode; }
        }
    }
}
