using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STV1
{
    public class Dungeon
    {
        private Node start;
        private List<Node> otherNodes;
        private List<List<Node>> zones;
        private Node exit;
        private const int M = 5;
        private const int NODES_PER_ZONE = 10;
        private const int MAX_CONNECTIVITY = 3;
        public const int TOTAL_MONSTERS = 50;
        private const int TOTAL_PACKS = 5;
        private const int TIME_CRYSTALS_PER_ZONE = 5;
        private int packSize;

        
        public Dungeon(int difficulty)
        {
            Random rnd = new Random();
            start = new Node(0,M,0);
            otherNodes = new List<Node>();
            zones = new List<List<Node>>();
                        
            /* Start = 0
            * Zone k = 1 + (k-1) * NODESPERZONE tot (k * NODESPERZONE)
            * Bridge of bridge level j = j * NODESPERZONE
            */

            Node last = start;

            //create the zones
            for (int k = 1; k < difficulty + 2; k++)
            {
                List<Node> zone = new List<Node>();
                
                //Connect the first two nodes with the last node of previous zone

                Node x = new Node(0, M);
                zone.Add(x);
                Node y = new Node(0, M);
                zone.Add(y);
                x.Connect(last);
                y.Connect(last);
                x.Connect(y);

                //Create the nodes in the zone
                for (int i = 2; i < NODES_PER_ZONE; i++)
                {
                    //Create new node z
                    Node z = new Node(0, M);

                    //Pick two different nodes that have less than 3 connections.
                    Node rndNode1;
                    do
                    {
                        rndNode1 = zone[rnd.Next(zone.Count)];
                    } while (rndNode1.ConnectionsCount > MAX_CONNECTIVITY);
                    Node rndNode2;
                    do
                    {
                        rndNode2 = zone[rnd.Next(zone.Count)];
                    } while (rndNode2 == rndNode1 || rndNode2.ConnectionsCount > MAX_CONNECTIVITY);

                    //Remove connection between the nodes if it exists
                    if (rndNode1.Adjacent(rndNode2))
                        rndNode1.Disconnect(rndNode2);

                    //Connect z to the chosen nodes
                    z.Connect(rndNode1);
                    z.Connect(rndNode2);

                    //Add z to the list of nodes in this zone
                    zone.Add(z);
                    
                }
                //Create the last node in this zone, which will be the bridge, and connect it to two preceding nodes
                Node bridge = new Node(k, M);
                bridge.Connect(zone[zone.Count - 1]);
                bridge.Connect(zone[zone.Count - 2]);
                zone.Add(bridge);
                last = bridge;
                //This zone is done, add it to the list of zones
                zones.Add(zone);
            }

            //The exit needs some special treatment, it has capacity 0
            exit = zones.Last().Last();
            zones.Last().Remove(exit);
            exit.setCapacity(M, -1);

            //Now put all the lists together as one
            foreach (List<Node> zone in zones)
                otherNodes.AddRange(zone);


            FixConnectivity();

            // Add monsters
            DistributeMonsters(difficulty);
        }

        /// <summary>
        /// Calculates the shortes path from u to v.
        /// </summary>
        /// <param name="u">The entry node of the path.</param>
        /// <param name="v">The exit node of the path.</param>
        /// <returns>The shortest path from u to v.</returns>
        public List<Node> ShortestPath(Node u, Node v)
        {
            // Implement BFS with queue
            Queue<Node> queue = new Queue<Node>();
            List<Node> seen = new List<Node>();
            List<Node> path = new List<Node>();
            Dictionary<Node,Node> preceding = new Dictionary<Node, Node>();
            queue.Enqueue(u);
            seen.Add(u);
            bool found = false;
            if (u == v)
            {
                path.Add(u);
                found = true;

            }
            while (!found && queue.Count > 0)
            {
                Node p = queue.Dequeue();
                foreach (Node q in p.ConnectedNodes)
                {
                    if (!seen.Contains(q))
                    {
                        seen.Add(q);
                        queue.Enqueue(q);
                        preceding.Add(q, p);
                    }
                    if (q == v)
                    {   
                        Node x = q;
                        while(x != u)
                        {
                            path.Add(x);
                            preceding.TryGetValue(x, out x);
                        }
                        path.Add(x);
                        found = true;
                        break;
                    }
                    
                }
            }

            if (!found) // Pad bestaat niet
                return null;

            path.Reverse();
            return path;
        }

        public bool PathExists(Node u, Node v)
        {
            return ShortestPath(u, v) != null;
        }

        /// <summary>
        /// Returns a Node that is a bridge with the specified level. Returns null if none is found.
        /// </summary>
        /// <param name="level">The level that the bridge should be.</param>
        /// <returns>The bridge with the specified level. Null if not found.</returns>
        public Node GetBridge(int level)
        {
            foreach(Node n in otherNodes)
            {
                if (Level(n) == level)
                    return n;
            }

            return null;
        }

        /// <summary>
        /// Checks whether all nodes in otherNodes are reachable from the start.
        /// Only used for testing purposes.
        /// </summary>
        /// <returns>Whether all otherNodes are reachable from the start.</returns>
        public bool AllNodesReachable()
        {
            foreach (Node n in otherNodes)
                if (!PathExists(start, n))
                    return false;
            return true;
        }

        public void Destroy(Node bridge)
        {
            if(bridge != exit)
            {
                RemoveNode(bridge);

                // Any bridge destruction is going to make the start unusable, so check if it exists, and if so, destroy it.
                if (start != null)
                    RemoveNode(start);

                // Remove nodes that can't reach the exit after the bridge is gone.
                List<Node> toBeRemoved = new List<Node>();

                foreach (Node node in otherNodes)
                    if (!PathExists(node, exit))
                        toBeRemoved.Add(node);

                foreach (Node n in toBeRemoved)
                    RemoveNode(n);
            }
        }

        /// <summary>
        /// Removes a single node from the dungeon: Removes it from otherNodes if it's in there, and calls its Destroy() method.
        /// </summary>
        /// <param name="node">The node to be removed from the dungeon.</param>
        private void RemoveNode(Node node)
        {
            otherNodes.Remove(node);
            node.Destroy();
            if (node == start)
                start = null;
        }

        public int Level(Node u)
        {
            int level = u.Capacity / M - 1;
            if (level >= 0)
                return level;
            else
                return 0;
        }

        public bool IsBridge(Node u)
        {
            return Level(u) >= 1;
        }

        public Node Start
        {
            get { return start; }
        }

        public Node Exit
        {
            get { return exit; }
        }

        public int Size
        {
            get { return 2 + otherNodes.Count; }
        }

        public double ConnectivityDegree
        {
            get
            {
                int connections = 0;
                foreach(List<Node> zone in zones)
                    foreach (Node n in zone)
                    connections += n.ConnectionsCount;
                connections += start.ConnectionsCount;
                connections += exit.ConnectionsCount;
                return (double)connections / Size;
            }
        }

        private void FixConnectivity()
        {
            while (ConnectivityDegree > 3)
            {
                Node n = new Node(0, M);
                List<Node> z = new List<Node>();
                bool breakLoop = false;
                foreach (List<Node> zone in zones)
                {
                    z = zone;
                    foreach (Node m in zone)
                        if (m.ConnectionsCount < 3)
                        {
                            m.Connect(n);
                            breakLoop = true;
                            break;
                        }
                    if (breakLoop)
                        break;
                }
                z.Add(n);
                otherNodes.Add(n);
            }
        }

        private void DistributeMonsters(int difficulty)
        {
            Random rnd = new Random();
            packSize = TOTAL_MONSTERS / TOTAL_PACKS;

            foreach (List<Node> zone in zones)
            {
                int k = zones.IndexOf(zone) + 1;
                int mobs = 2 * k * TOTAL_MONSTERS / ((difficulty + 2) * (difficulty + 1));
                int zoneCapacity = (zone.Count - 1) * M + zone.Last().Capacity;
                if (mobs > zoneCapacity)
                    mobs = zoneCapacity;
                int actualmobs = 0;
                while (actualmobs < mobs) // As long as we haven't placed enough mobs in the zone, continue (doesn't finish if there's too many monsters to place)
                {
                    Node x = zone[rnd.Next(zone.Count)];
                    int mobsInNode = 0;
                    foreach (Pack p in x.PacksInNode)
                        mobsInNode += p.Size;

                    if (mobsInNode < x.Capacity)
                    {
                        Pack p;
                        if (packSize <= x.Capacity - mobsInNode && packSize <= mobs - actualmobs)
                            p = new Pack(packSize, x);
                        else if (x.Capacity - mobsInNode <= mobs - actualmobs)
                            p = new Pack(x.Capacity - mobsInNode, x);
                        else
                            p = new Pack(mobs - actualmobs, x);
                        actualmobs += p.Size;
                    }
                }
            }
        }

        public void DistributeItems(Player player)
        {
            Random rnd = new Random();

            //Calculate how many health pots we need
            float healValue = new HealingPotion().HealValue;
            float totalMonsterHealth = 0;
            foreach (List<Node> zone in zones)
            {
                foreach (Node n in zone)
                {
                    foreach (Pack p in n.PacksInNode)
                    {
                        totalMonsterHealth += p.Size * p.Monster.HitPoints;
                    }
                }
            }
            int healthPots = (int)((totalMonsterHealth - player.HitPoints) / healValue);
            int healthPotsPerZone = healthPots / zones.Count;
            
            //Distribute the healthpots and timecrystals evenly over the zones
            foreach (List<Node> zone in zones)
            {
                //Put every health pot in a random node
                for (int i = 0; i < healthPotsPerZone; i++)
                {
                    Node x = zone[rnd.Next(zone.Count)];
                    x.AddItem(new HealingPotion());
                }
                //Put every timecrystal in a random node
                for(int i = 0; i < TIME_CRYSTALS_PER_ZONE; i++)
                {
                    Node x = zone[rnd.Next(zone.Count)];
                    x.AddItem(new TimeCrystal());
                }
            }



        }

        public List<List<Node>> Zones
        {
            get { return zones; }
        }
    }
}
