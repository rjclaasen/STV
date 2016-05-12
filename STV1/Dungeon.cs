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
        private Node exit;
        private const int M = 5;
        private const int NODES_PER_ZONE = 10;
        private const int MAX_CONNECTIVITY = 3;
        private const int TOTAL_MONSTERS = 50;
        private const int TOTAL_PACKS = 5;
        private const int ITEMS_PER_ZONE = 10;
        private int packSize;

        // Temporary constructor. Doesn't actually create a proper dungeon.
        // TODO: Make a proper dungeon in this constructor.
        public Dungeon(int difficulty)
        {
            Random rnd = new Random();
            start = new Node(0,M,0);
            otherNodes = new List<Node>();
            Node[] dungeon = new Node[(difficulty + 1) * NODES_PER_ZONE + difficulty + 2];
            dungeon[0] = start;
            dungeon[(difficulty + 1) * NODES_PER_ZONE + 1] = exit;
            /* Start = 0
            * Zone k = 1 + (k-1) * NODESPERZONE tot (k * NODESPERZONE)
            * Bridge of bridge level j = j * NODESPERZONE
            */


            //create the zones
            for (int k = 1; k < difficulty + 2; k++)
            {
                int zoneStart = (k - 1) * NODES_PER_ZONE + k;
                
                //Connect the first two nodes with the last node of previous zone
                dungeon[zoneStart] = new Node(0,M);
                dungeon[zoneStart + 1] = new Node(0,M);
                dungeon[zoneStart].Connect(dungeon[zoneStart - 1]);
                dungeon[zoneStart + 1].Connect(dungeon[zoneStart - 1]);

                //Create the nodes in the zone
                for (int i = 2; i < NODES_PER_ZONE; i++)
                {
                    Node x = new Node(0,M);
                    dungeon[zoneStart + i] = x;
                    
                    //Choose two nodes to connect to
                    int a = zoneStart + rnd.Next(0,i);
                    while(dungeon[a].ConnectionsCount > MAX_CONNECTIVITY)
                        a = zoneStart + rnd.Next(0, i);
                    x.Connect(dungeon[a]);
                    int b = a;
                    while (b == a || dungeon[b].ConnectionsCount > MAX_CONNECTIVITY)
                        b = zoneStart + rnd.Next(0, i);
                    x.Connect(dungeon[b]);
                    if(dungeon[b].Adjacent(dungeon[a]))
                    {
                        dungeon[b].Disconnect(dungeon[a]);
                    }
                }
                //Connect the last node of the zone (the bridge) to two earlier nodes
                Node bridge = new Node(k,M);
                dungeon[zoneStart + NODES_PER_ZONE] = bridge;
                bridge.Connect(dungeon[zoneStart + NODES_PER_ZONE - 1]);
                bridge.Connect(dungeon[zoneStart + NODES_PER_ZONE - 2]);
            }

            exit = dungeon[dungeon.Length - 1];
            exit.setCapacity(M, -1);
            for (int i = 1; i <= dungeon.Length - 2; i++)
                otherNodes.Add(dungeon[i]);

            while (ConnectivityDegree > 3)
            {
                Node n = new Node(0, M);
                foreach (Node m in otherNodes)
                    if (m.ConnectionsCount < 3)
                    {
                        m.Connect(n);
                        break;
                    }
                otherNodes.Add(n);
            }

            // Add monsters
            packSize = TOTAL_MONSTERS / TOTAL_PACKS;
            for(int k = 1; k < difficulty + 2; k++)
            {
                int zoneStart = (k - 1) * NODES_PER_ZONE + 1;
                int zoneEnd = k * NODES_PER_ZONE;
                int mobs = 2 * k * TOTAL_MONSTERS / ((difficulty + 2) * (difficulty + 1));
                int actualmobs = 0;
                while(actualmobs < mobs)
                {
                    int x = rnd.Next(zoneStart,zoneEnd + 1);
                    Node n = dungeon[x];
                    int mobsInNode = 0;
                    foreach (Pack p in n.PacksInNode)
                        mobsInNode += p.Size;

                    if (mobsInNode < n.Capacity)
                    {
                        Pack p;
                        if (packSize <= n.Capacity - mobsInNode && packSize <= mobs - actualmobs)
                            p = new Pack(packSize, n);
                        else if (n.Capacity - mobsInNode <= mobs - actualmobs)
                            p = new Pack(n.Capacity - mobsInNode, n);
                        else
                            p = new Pack(mobs - actualmobs, n); 
                        actualmobs += p.Size;
                    }
                }
            }

            // Add items
            for(int k = 1; k < difficulty + 2; k++)
            {
                int zoneStart = (k - 1) * NODES_PER_ZONE + 1;
                int zoneEnd = k * NODES_PER_ZONE;

                for (int i = 0; i < ITEMS_PER_ZONE; i++)
                {
                    int x = rnd.Next(zoneStart + i, zoneEnd + 1);
                    float j = rnd.Next();
                    if (j < 0.1f)
                        dungeon[x].AddItem(new TimeCrystal());
                    else
                        dungeon[x].AddItem(new HealingPotion());
                }
            }

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

            // TODO: Test method
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
                foreach (Node n in otherNodes)
                    connections += n.ConnectionsCount;
                connections += start.ConnectionsCount;
                connections += exit.ConnectionsCount;
                return (double)connections / Size;
            }
        }
    }
}
