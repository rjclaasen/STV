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
        private Node precedingNode;
        public Node()
        {
            packsInNode = new List<Pack>();
            connectedNodes = new List<Node>();
        }

        public bool AddConnection(Node n)
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

        public void RemoveConnection(Node n)
        {
            if (connectedNodes.Contains(n))
            {
                connectedNodes.Remove(n);
                n.RemoveConnection(this);
            }
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
