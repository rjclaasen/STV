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
        private int M;

        // Temporary constructor. Doesn't actually create a proper dungeon.
        // TODO: Make a proper dungeon in this constructor.
        public Dungeon(int difficulty)
        {
            start = new Node();
            exit = new Node();
            start.Connect(exit);
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
            return path;
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

        public int Level
        {
            get { return 0; }
        }
    }
}
