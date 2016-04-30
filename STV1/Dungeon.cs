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
            // TODO: Implement method.
            return new List<Node>();
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
