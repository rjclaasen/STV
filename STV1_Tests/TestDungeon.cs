using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    /// <summary>
    /// Summary description for TestDungeon
    /// </summary>
    [TestClass]
    public class TestDungeon
    {
        [TestMethod]
        public void TestDungeonConstructor()
        {
            Dungeon d = new Dungeon(1);
            Assert.AreNotEqual(d.Start, d.Exit);
            Assert.IsTrue(d.PathExists(d.Start, d.Exit));
            Assert.IsTrue(d.AllNodesReachable());
            Assert.IsTrue(d.ConnectivityDegree <= 3.0f);
        }

        [TestMethod]
        public void TestDungeonConstructor2()
        {
            Dungeon d = new Dungeon(2);
            Assert.AreNotEqual(d.Start, d.Exit);
            Assert.IsTrue(d.PathExists(d.Start, d.Exit));
            Assert.IsTrue(d.AllNodesReachable());
            Assert.IsTrue(d.ConnectivityDegree <= 3.0f);
        }

        [TestMethod]
        public void TestDungeonConstraints()
        {
            int level = 3;
            Dungeon d = new Dungeon(level);
            Player player = new Player(d.Start, 100, 10, d, null);
            d.DistributeItems(player);
            List<Node> path = d.ShortestPath(d.Start, d.Exit);
            bool pathContainsAllBridges = true;
            List<Node> bridges = new List<Node>();
            foreach (Node n in GetAllNodesFromDungeon(d))
                if (d.IsBridge(n))
                    bridges.Add(n);
            foreach (Node n in bridges)
                if (!path.Contains(n))
                        pathContainsAllBridges = false;

            Assert.IsTrue(pathContainsAllBridges);

            List<List<Node>> zones = d.Zones;
            foreach(List<Node> zone in zones)
            {
                int expectedMobs = 2 * (1 + zones.IndexOf(zone)) * Dungeon.TOTAL_MONSTERS / ((level + 1) * (level + 2));
                int actualMobs = 0;
                foreach (Node n in zone)
                    foreach (Pack p in n.PacksInNode)
                    {
                        actualMobs += p.Size;
                    }
                Assert.AreEqual(expectedMobs, actualMobs);
            }

            float totalMonsterHealth = 0;
            float totalPotionHealth = 0;
            foreach(List<Node> zone in zones)
            {
                foreach (Node n in zone)
                {
                    foreach (Pack p in n.PacksInNode)
                    {
                        totalMonsterHealth += p.Size * p.Monster.HitPoints;
                    }
                    foreach (Item pot in n.Items)
                            if(pot is HealingPotion)
                                totalPotionHealth += ((HealingPotion)pot).HealValue;
                }
            }

            float totalPlayerHealth = player.HitPoints;
            Assert.IsTrue(totalPotionHealth + totalPlayerHealth <= totalMonsterHealth);


        }
        [TestMethod]
        public void TestDungeonShortestPath()
        {
            Dungeon dun = new Dungeon(1);
            List<Node> path = dun.ShortestPath(dun.Start, dun.Exit);

            Assert.AreNotEqual(0, path.Count);
            Assert.AreEqual(dun.Exit, path[path.Count - 1]);

            Node[] nodes = new Node[5];
            for (int i = 0; i < 5; i++)
                nodes[i] = new Node(0, 0);
            for (int i = 1; i < 5; i++) //Make a cross
                nodes[0].Connect(nodes[i]);
            for (int i = 1; i < 5; i++)
            {
                path = dun.ShortestPath(nodes[0], nodes[i]);
                Assert.IsTrue(path[0] == nodes[0]);
                Assert.IsTrue(path.Count == 2);
            }
            for (int i = 2; i < 5; i++)
                for (int j = i + 1; j < 5; j++)
                    Assert.IsTrue(dun.ShortestPath(nodes[i], nodes[j]).Count == 3);

            nodes = new Node[4];
            for (int i = 0; i < 4; i++)
                nodes[i] = new Node(0, 0);
            for (int i = 1; i < 3; i++) // Make a diamond
            {
                nodes[0].Connect(nodes[i]);
                nodes[3].Connect(nodes[i]);
            }
            path = dun.ShortestPath(nodes[0], nodes[3]);
            Assert.IsTrue(path.Count == 3);
            Assert.IsTrue(path[0] == nodes[0]);
            Assert.IsTrue(path[1] == nodes[1] || path[1] == nodes[2]);
            Assert.IsTrue(path[2] == nodes[3]);
        }

        [TestMethod]
        public void TestDungeonPathExists()
        {
            Dungeon d = new Dungeon(1);
            Node n1 = new Node(0, 0);
            Node n2 = new Node(0, 0);

            n1.Connect(n2);

            Assert.IsTrue(d.PathExists(n1, n2));

            n1.Disconnect(n2);

            Assert.IsFalse(d.PathExists(n1, n2));
        }

        [TestMethod]
        public void TestDungeonGetBridge()
        {
            Dungeon d = new Dungeon(2);

            Node bridge = d.GetBridge(1);
            Assert.AreEqual(1, d.Level(bridge));

            bridge = d.GetBridge(2);
            Assert.AreEqual(2, d.Level(bridge));

            bridge = d.GetBridge(3);
            Assert.IsNull(bridge);
        }

        [TestMethod]
        public void TestDungeonAllNodesReachable()
        {
            Dungeon d = new Dungeon(1);
            bool allNodesReachable = true;

            List<Node> nodes = GetAllNodesFromDungeon(d);

            foreach (Node n in nodes)
            {
                if (!d.PathExists(d.Start, n))
                {
                    allNodesReachable = false;
                    break;
                }
            }

            Assert.IsTrue(allNodesReachable);
        }

        [TestMethod]
        public void TestDungeonDestroy()
        {
            Dungeon d = new Dungeon(1);

            Node bridge = d.GetBridge(1);
            Node start = d.Start;

            d.Destroy(bridge);

            Assert.IsTrue(start.Destroyed);
            Assert.IsNull(d.Start);
            Assert.IsTrue(bridge.Destroyed);
        }

        [TestMethod]
        public void TestDungeonLevel()
        {
            Dungeon d = new Dungeon(1);

            Assert.AreEqual(0, d.Level(d.Start));
            Assert.AreEqual(0, d.Level(d.Exit));

            Node n = new Node(1, 5);
            Assert.AreEqual(1, d.Level(n));
            n = new Node(2, 5);
            Assert.AreEqual(2, d.Level(n));
        }

        [TestMethod]
        public void TestDungeonIsBridge()
        {
            Dungeon d = new Dungeon(1);

            Assert.IsFalse(d.IsBridge(d.Start));
            Assert.IsFalse(d.IsBridge(d.Exit));

            Node n = new Node(1, 5);
            Assert.IsTrue(d.IsBridge(n));
        }

        [TestMethod]
        public void TestDungeonSize()
        {
            Dungeon d = new Dungeon(1);

            Assert.AreEqual(GetAllNodesFromDungeon(d).Count, d.Size);

            d = new Dungeon(2);

            Assert.AreEqual(GetAllNodesFromDungeon(d).Count, d.Size);

            d = new Dungeon(3);

            Assert.AreEqual(GetAllNodesFromDungeon(d).Count, d.Size);
        }

        [TestMethod]
        public void TestDungeonConnectivityDegree()
        {
            Dungeon d = new Dungeon(1);

            Assert.IsTrue(d.ConnectivityDegree <= 3.0);

            List<Node> allNodes = GetAllNodesFromDungeon(d);

            int connections = 0;
            foreach (Node n in allNodes)
            {
                connections += n.ConnectionsCount;
            }
            double connectivity = (double)connections / d.Size;

            Assert.AreEqual(connectivity, d.ConnectivityDegree);
        }


        /// <summary>
        /// Searches through the dungeon recursively and creates a list of all the nodes inside.
        /// </summary>
        /// <param name="dungeon">The dungeon to be searched for nodes.</param>
        /// <returns>A list of all the nodes in the dungeon.</returns>
        private List<Node> GetAllNodesFromDungeon(Dungeon dungeon)
        {
            HashSet<Node> AllNodes = new HashSet<Node>();
            Queue<Node> toBeSearched = new Queue<Node>();
            Node next = dungeon.Start;

            do
            {
                AllNodes.Add(next);

                foreach (Node n in next.ConnectedNodes)
                    if (!AllNodes.Contains(n))
                        toBeSearched.Enqueue(n);

                next = toBeSearched.Dequeue();
            }
            while (toBeSearched.Count > 0);

            List<Node> result = new List<Node>();
            foreach (Node n in AllNodes)
                result.Add(n);

            return result;
        }

        
    }
}
