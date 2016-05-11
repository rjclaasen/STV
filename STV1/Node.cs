using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public class Node
    {
        private const int MAXCONNECTIONS = 4;

        private List<Pack> packsInNode;
        private Player playerInNode;
        private List<Node> connectedNodes;
        private int capacity;
        private int level;
        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="capacity">Optional parameter to set the capacity directly, instead of using the formula.</param>
        public Node(int level, int M, int capacity = -1)
        {
            this.level = level;
            this.capacity = M * (level + 1);
            packsInNode = new List<Pack>();
            connectedNodes = new List<Node>();
            if (capacity != -1)
                this.capacity = capacity;
        }

        public bool Connect(Node n)
        {
            if (ConnectionsCount >= MAXCONNECTIONS || n.ConnectionsCount >= MAXCONNECTIONS 
                || Adjacent(n) || n.Adjacent(this))
                return false;
            else
            {
                connectedNodes.Add(n);
                n.connectedNodes.Add(this);
                return true;
            }
        }

        public void Disconnect(Node n)
        {
            if (connectedNodes.Contains(n))
            {
                connectedNodes.Remove(n);
                n.Disconnect(this);
            }
        }

        /// <summary>
        /// Removes connections to and from this node, and the pack(s) inside.
        /// </summary>
        public void Destroy()
        {
            foreach (Node n in connectedNodes)
                Disconnect(n);
            packsInNode = null;
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

        public void UseItem(Item item)
        {
            item.Use(playerInNode);
        }

        public bool Adjacent(Node other)
        {
            return connectedNodes.Contains(other);
        }

        
        public bool PackFits(Pack pack)
        {
            int size = 0;
            foreach (Pack p in packsInNode)
                size += p.Size;
            return capacity - (size + pack.Size) >= 0;
        }

        public bool PlayerInNode()
        {
            return playerInNode != null;
        }

        public List<Pack> PacksInNode
        {
            get { return packsInNode; }
        }

        public int ConnectionsCount
        {
            get { return connectedNodes.Count; }
        }

        public List<Node> ConnectedNodes
        {
            get { return connectedNodes; }
        }     
        
        //TODO: Set capacity when we know which nodes are bridges
        /// <summary>
        /// Sets a node's capacity, based on the dungeon's constant and the level of the node.
        /// </summary>
        /// <param name="M">The dungeon's capacity constant.</param>
        /// <param name="L">The level of the node.</param>
        public void setCapacity(int M, int L)
        {
            capacity = M * (L + 1);
        }

        public int Capacity
        {
            get { return capacity;}
        }
    }
}
