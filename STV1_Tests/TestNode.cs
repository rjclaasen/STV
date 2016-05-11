using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestNode
    {
        [TestMethod]
        public void TestAddConnection()
        {
            Node n = new Node(0,0);

            Node o = new Node(0,0);

            Assert.IsTrue(n.Connect(o));
            Assert.AreEqual(1, n.ConnectionsCount);
            Assert.AreEqual(1, o.ConnectionsCount);
        }

        [TestMethod]
        public void TestRemoveConnection()
        {
            Node n = new Node(0,0);
            Node o = new Node(0,0);

            n.Connect(o);
            n.Disconnect(o);
            Assert.AreEqual(0, n.ConnectionsCount);
            Assert.AreEqual(0, o.ConnectionsCount);
        }

        [TestMethod]
        public void TestPlayerInNode()
        {
            Node n = new Node(0,0);

            Player p = new Player(new Node(0,0), 10, 5, null, null);

            Assert.AreEqual(false, n.PlayerInNode());
            n.PlayerEnters(p);
            Assert.AreEqual(true, n.PlayerInNode());
            n.PlayerLeaves();
            Assert.AreEqual(false, n.PlayerInNode());
        }

        [TestMethod]
        public void TestPackEnterAndLeave()
        {
            Node n = new Node(0,0);

            Pack p1 = new Pack(1, new Node(0,0));
            Pack p2 = new Pack(2, new Node(0,0));

            n.PackEnters(p1);
            Assert.IsTrue(n.PacksInNode.Contains(p1));
            n.PackEnters(p2);
            n.PackLeaves(p1);
            Assert.IsFalse(n.PacksInNode.Contains(p1));
            n.PackLeaves(p2);
            Assert.AreEqual(0, n.PacksInNode.Count);
        }

        [TestMethod]
        public void TestAdjacent()
        {
            Node n = new Node(0,0);
            Node o = new Node(0,0);

            n.Connect(o);

            Assert.IsTrue(n.Adjacent(o));
            Assert.IsTrue(o.Adjacent(n));

            n.Disconnect(o);

            Assert.IsFalse(n.Adjacent(o));
            Assert.IsFalse(o.Adjacent(n));
        }
    }
}
