using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STV1;

namespace STV1_Tests
{
    /// <summary>
    /// A Dungeon, but with some more variables exposed to make testing less of a hassle.
    /// </summary>
    class MockDungeon : Dungeon
    {
        public MockDungeon(int difficulty)
            : base(difficulty)
        {

        }

        public List<Node> OtherNodes
        {
            get { return otherNodes; }
        }
    }
}
