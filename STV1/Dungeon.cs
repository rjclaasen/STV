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

        public Dungeon(int difficulty)
        {

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
            queue.Enqueue(u);
            seen.Add(u);
            bool found = false;
            while (!found)
            {
                Node p = queue.Dequeue();
                foreach (Node q in p.ConnectedNodes)
                {
                    if(q == v)
                    {
                        Node x = q;
                        while(x != u)
                        {
                            path.Add(x);
                            x = x.PrecedingNode;
                        }
                        path.Add(x);
                        found = true;
                    }
                    if (!seen.Contains(q))
                    {
                        seen.Add(q);
                        queue.Enqueue(q);
                        q.PrecedingNode = p;
                    }
                }
            }

            foreach (Node n in seen)
                n.PrecedingNode = null;
            // TODO: Test method
            return path;
        }

        public bool PathExists(Node u, Node v)
        {
            return ShortestPath(u, v).Count != 0;
        }

        public void Destroy(Node bridge)
        {
            if(bridge != exit)
            {
                
            }
        }

        public Node Start
        {
            get { return start; }
        }

        public Node Exit
        {
            get { return exit; }
        }
        public List<Node> OtherNodes
        {
            get
            {
                return otherNodes;
            }
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
