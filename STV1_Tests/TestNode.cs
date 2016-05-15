using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestNode
    {
        [TestMethod]
        public void TestNodeConstructor()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestNodeConnect()
        {
            Node n = new Node(0,0);

            Node o = new Node(0,0);

            Assert.IsTrue(n.Connect(o));
            Assert.AreEqual(1, n.ConnectionsCount);
            Assert.AreEqual(1, o.ConnectionsCount);

            Node a = new Node();
            Node b = new Node();
            Node c = new Node();
            Node d = new Node();

            Assert.IsTrue(n.Connect(a));
            Assert.IsTrue(n.Connect(b));
            Assert.IsTrue(n.Connect(c));
            Assert.IsFalse(n.Connect(d));
        }

        [TestMethod]
        public void TestNodeDisconnect()
        {
            Node n = new Node(0, 0);
            Node o = new Node(0, 0);

            n.Connect(o);
            n.Disconnect(o);
            Assert.AreEqual(0, n.ConnectionsCount);
            Assert.AreEqual(0, o.ConnectionsCount);
        }

        [TestMethod]
        public void TestNodeDestroy()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestNodePackEnters()
        {
            Node n = new Node();

            Pack p = new Pack(1, null);

            n.PackEnters(p);
            Assert.IsTrue(n.PacksInNode.Contains(p));
        }

        [TestMethod]
        public void TestNodePackLeaves()
        {
            Node n = new Node(0, 0);

            Pack p1 = new Pack(1, null);

            n.PackEnters(p1);
            n.PackLeaves(p1);
            Assert.IsFalse(n.PacksInNode.Contains(p1));
        }

        [TestMethod]
        public void TestNodePlayerEnters()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestNodePlayerLeaves()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestNodeUseItem()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestNodeAddItem()
        {
            Node n1 = new Node();

            n1.AddItem(new HealingPotion());
            Assert.AreEqual(1, n1.Items.Count);

            n1.AddItem(new TimeCrystal());
            Assert.AreEqual(2, n1.Items.Count);
        }

        [TestMethod]
        public void TestNodeAdjacent()
        {
            Node n = new Node(0, 0);
            Node o = new Node(0, 0);

            n.Connect(o);

            Assert.IsTrue(n.Adjacent(o));
            Assert.IsTrue(o.Adjacent(n));

            n.Disconnect(o);

            Assert.IsFalse(n.Adjacent(o));
            Assert.IsFalse(o.Adjacent(n));
        }

        [TestMethod]
        public void TestPackFits()
        {
            Node moveTarget = new Node(1, 5, 5);
            Node moveSource = new Node(1, 5, 30);

            moveTarget.Connect(moveSource);

            Pack p3 = new Pack(3, moveSource);
            Pack p4 = new Pack(4, moveSource);
            Pack p5 = new Pack(5, moveSource);
            Pack p6 = new Pack(6, moveSource);

            Assert.IsTrue(moveTarget.PackFits(p5));
            Assert.IsFalse(moveTarget.PackFits(p6));

            p3.Move(moveTarget);

            Assert.IsFalse(moveTarget.PackFits(p4));
        }

        [TestMethod]
        public void TestNodePlayerInNode()
        {
            Node n = new Node(0,0);

            Player p = new Player(new Node(0,0), 10, 5, null, null);

            Assert.IsFalse(n.PlayerInNode());
            n.PlayerEnters(p);
            Assert.IsTrue(n.PlayerInNode());
            n.PlayerLeaves();
            Assert.IsFalse(n.PlayerInNode());
        }

        [TestMethod]
        public void TestNodeConnectionsCount()
        {
            Node n = new Node();
            Node o = new Node();

            Assert.AreEqual(0, n.ConnectionsCount);

            n.ConnectedNodes.Add(o);

            Assert.AreEqual(1, n.ConnectionsCount);
        }

        [TestMethod]
        public void TestNodeConnectionsSetCapacity()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestNodeAddPack()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestNodeCheckForCombat()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestNodeDoCombat()
        {
            Node n1 = new Node(0, 0, 10);
            Node n2 = new Node(0, 0, 10);
            n1.Connect(n2);
            Player player = new Player(n1, 100, 100, null, null);
            Pack pack = new Pack(5, n1, 10, 5);

            n1.doCombat();
            Assert.IsTrue(player.IsDead() || pack.Dead || n2.PacksInNode.Count == 1);
        }

        [TestMethod]
        public void TestNodeDoCombatRound()
        {
            Node n1 = new Node(0, 0, 10);
            Player player = new Player(n1, 100, 10, null, null);
            Pack pack = new Pack(10, n1, 10, 5);

            n1.doCombatRound(player, pack);
            Assert.AreEqual(9, pack.Size);
            Assert.AreEqual(55, player.HitPoints);

        }
    }
}
