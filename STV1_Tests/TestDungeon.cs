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


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestShortestPath()
        {
            Dungeon dun = new Dungeon(1);
            List<Node> path = Dungeon.ShortestPath(dun.Start, dun.Exit);

            Assert.AreNotEqual(0, path.Count);
            Assert.AreEqual(dun.Exit, path[path.Count - 1]);

            Node[] nodes = new Node[5];
            for (int i = 0; i < 5; i++)
                nodes[i] = new Node(0, 0);
            for (int i = 1; i < 5; i++) //Make a cross
                nodes[0].Connect(nodes[i]);
            for (int i = 1; i < 5; i++)
            {
                path = Dungeon.ShortestPath(nodes[0], nodes[i]);
                Assert.IsTrue(path[0] == nodes[0]);
                Assert.IsTrue(path.Count == 2);
            }
            for (int i = 2; i < 5; i++)
                for (int j = i + 1; j < 5; j++)
                    Assert.IsTrue(Dungeon.ShortestPath(nodes[i], nodes[j]).Count == 3);

            nodes = new Node[4];
            for (int i = 0; i < 4; i++)
                nodes[i] = new Node(0, 0);
            for (int i = 1; i < 3; i++) // Make a diamond
            {
                nodes[0].Connect(nodes[i]);
                nodes[3].Connect(nodes[i]);
            }
            path = Dungeon.ShortestPath(nodes[0], nodes[3]);
            Assert.IsTrue(path.Count == 3);
            Assert.IsTrue(path[0] == nodes[0]);
            Assert.IsTrue(path[1] == nodes[1] || path[1] == nodes[2]);
            Assert.IsTrue(path[2] == nodes[3]);
        }

        [TestMethod]
        public void TestDungeonConstructor()
        {
            Dungeon d = new Dungeon(1);
            Assert.AreNotEqual(d.Start, d.Exit);
            Assert.IsTrue(Dungeon.PathExists(d.Start, d.Exit));
            Assert.IsTrue(d.AllNodesReachable());
            //Make test regarding connectivity
            
        }

        [TestMethod]
        public void TestDungeonConstructor2()
        {
            Dungeon d = new Dungeon(2);
            Assert.AreNotEqual(d.Start, d.Exit);
            Assert.IsTrue(Dungeon.PathExists(d.Start, d.Exit));
            Assert.IsTrue(d.AllNodesReachable());
        }

        [TestMethod]
        public void TestPathExists()
        {
            Node n1 = new Node(0,0);
            Node n2 = new Node(0,0);

            n1.Connect(n2);

            Assert.IsTrue(Dungeon.PathExists(n1, n2));

            n1.Disconnect(n2);

            Assert.IsFalse(Dungeon.PathExists(n1, n2));
        }

        [TestMethod]
        public void TestDestroy()
        {
            Dungeon d = new Dungeon(1);


        }

        [TestMethod]
        public void TestLevel()
        {
            Assert.IsTrue(false);
        }
    }
}
