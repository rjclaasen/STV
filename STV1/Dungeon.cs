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
        protected List<Node> otherNodes;
        private Node exit;
        private const int M = 5;
        private const int NODESPERZONE = 10;
        private const int MAXCONNECTIVITY = 3;

        // Temporary constructor. Doesn't actually create a proper dungeon.
        // TODO: Make a proper dungeon in this constructor.
        public Dungeon(int difficulty)
        {
            Random rnd = new Random();
            start = new Node(0,M,0);
            otherNodes = new List<Node>();
            Node[] dungeon = new Node[(difficulty + 1) * NODESPERZONE + 2];
            dungeon[0] = start;
            dungeon[(difficulty + 1) * NODESPERZONE + 1] = exit;
            /* Start = 0
            * Zone k = 1 + (k-1) * NODESPERZONE tot (k * NODESPERZONE)
            * Bridge of bridge level j = j * NODESPERZONE
            */


            //create the zones
            for (int k = 1; k < difficulty + 2; k++)
            {
                int zoneStart = (k - 1) * NODESPERZONE + 1;
                
                //Connect the first two nodes with the last node of previous zone
                dungeon[zoneStart] = new Node(0,M);
                dungeon[zoneStart + 1] = new Node(0,M);
                dungeon[zoneStart].Connect(dungeon[zoneStart - 1]);
                dungeon[zoneStart + 1].Connect(dungeon[zoneStart - 1]);
                //Create the nodes in the zone
                for (int i = 2; i < NODESPERZONE; i++)
                {
                    Node x = new Node(0,M);
                    dungeon[zoneStart + i] = x; 
                    
                    //Choose two nodes to connect to
                    int a = rnd.Next(0,i);
                    while(dungeon[zoneStart + a].ConnectionsCount > MAXCONNECTIVITY)
                        a = rnd.Next(0, i);
                    x.Connect(dungeon[zoneStart + a]);
                    int b = a;
                    while (b == a || dungeon[zoneStart + b].ConnectionsCount > MAXCONNECTIVITY)
                        b = rnd.Next(0, i);
                    x.Connect(dungeon[zoneStart + b]);
                }
                //Connect the last node of the zone (the bridge) to two earlier nodes
                Node bridge = new Node(k,M);
                dungeon[zoneStart + NODESPERZONE - 1] = bridge;
                bridge.Connect(dungeon[zoneStart + NODESPERZONE - 2]);
                bridge.Connect(dungeon[zoneStart + NODESPERZONE - 3]);
            }
            exit = dungeon[(difficulty + 1) * NODESPERZONE];
            exit.setCapacity(M, -1);
            for (int i = 1; i < (difficulty + 1) * NODESPERZONE-1; i++)
                otherNodes.Add(dungeon[i]);


        }

        /// <summary>
        /// Calculates the shortes path from u to v.
        /// </summary>
        /// <param name="u">The entry node of the path.</param>
        /// <param name="v">The exit node of the path.</param>
        /// <returns>The shortest path from u to v.</returns>
        public static List<Node> ShortestPath(Node u, Node v)
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

        public bool AllNodesReachable()
        {
            foreach (Node n in otherNodes)
                if (!PathExists(start, n))
                    return false;
            return true;
        }

        public static bool PathExists(Node u, Node v)
        {
            return ShortestPath(u, v) != null;
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
                foreach(Node node in otherNodes)
                    if (!PathExists(node, exit))
                        RemoveNode(node);
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

        public int Level(Node u)
        {
            return u.Capacity / M;
        }

        public bool IsBridge(Node u)
        {
            return Level(u) >= 1;
        }
    }
}
