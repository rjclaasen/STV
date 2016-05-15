using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace STV1
{
    public class Node
    {
        private static int idCounter = 0;
        private int id;

        private const int MAXCONNECTIONS = 4;

        private List<Pack> packsInNode;
        private Player playerInNode;
        private List<Node> connectedNodes;
        private int capacity;
        private int level;
        private bool destroyed = false;
        private List<Item> items;
        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="level">TODO: Add documentation.</param>
        /// <param name="M">TODO: Add documentation.</param>
        /// <param name="capacity">Optional parameter to set the capacity directly, instead of using the formula.</param>
        public Node(int level = 0, int M = 0, int capacity = -1)
        {
            this.level = level;
            this.capacity = M * (level + 1);
            packsInNode = new List<Pack>();
            connectedNodes = new List<Node>();
            items = new List<Item>();
            if (capacity != -1)
                this.capacity = capacity;

            id = idCounter;
            idCounter++;
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
            while (connectedNodes.Count > 0)
                Disconnect(connectedNodes[0]);
            packsInNode = null;
            destroyed = true;
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
            foreach (Item item in items)
                player.PickUp(item);
            items = new List<Item>();
        }

        public void PlayerLeaves()
        {
            playerInNode = null;
        }

        public void UseItem(Item item)
        {
            item.Use(playerInNode);
        }

        public void AddItem(Item item)
        {
            items.Add(item);
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
            get { return capacity; }
        }

        public bool AddPack(Pack p)
        {
            if (PackFits(p))
            {
                packsInNode.Add(p);
                return true;
            }
            else
                return false;
        }

        public bool Destroyed
        {
            get { return destroyed; }
        }

        public void CheckForCombat()
        {
            while (playerInNode != null && !playerInNode.IsDead() && packsInNode.Count > 0)
                doCombat();

        }

        public void doCombat()
        {
            bool inCombat = true;
            while(inCombat)
            {
                inCombat = doCombatRound(playerInNode, packsInNode[0]);
            }
            return;

        }

        public bool doCombatRound(Player player, Pack pack)
        {
            bool timeCrystal = false;
            Command c = player.GetCommand();
            if (c != null)
            {
                if (c.useItem)
                {

                    if (c.timeCrystal) { timeCrystal = player.UseTimeCrystal(); }
                    else if (c.healingPotion) { player.UseHealingPotion(); }

                }
                if (timeCrystal)
                    foreach (Monster m in pack.Monsters)
                    {
                        player.Attack(m);
                    }
                else
                    player.Attack(pack.Monster);
            }
            else
                player.Attack(pack.Monster);
            pack.Update();
            if (pack.Dead)
                return false;
            pack.Attack(player);

            
            if (player.IsDead())
                return false;
            Command g = player.GetCommand();
            if (g!= null && g.retreat)
                player.Move(g.node);
            if (pack.Retreat(player))
                return false;
            return true;
        }
        
        public List<Item> Items
        {
            get { return items; }
        }
    }
}
