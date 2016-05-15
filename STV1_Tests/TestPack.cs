using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestPack
    {
        [TestMethod]
        public void TestPackConstructor()
        {
            Node n = new Node(0,0);
            Pack p = new Pack(5, n);
            Pack p2 = new Pack(5, n, 10);

            Assert.AreEqual(5, p.Size);
            Assert.AreEqual(n, p.Location);
            Assert.AreEqual(10, p2.Monster.HitPoints);
        }

        [TestMethod]
        public void TestPackMove()
        {
            const int PACKSIZE = 1;

            Node n1 = new Node(0,0,PACKSIZE);
            Node n2 = new Node(0,0,PACKSIZE);

            n1.Connect(n2);
            Pack p = new Pack(PACKSIZE, n1);

            p.Move(n2);

            Assert.AreEqual(n2, p.Location);

            Assert.IsFalse(p.Move(new Node()));
        }

        [TestMethod]
        public void TestPackRetreat()
        {
            Node n1 = new Node(0, 0, 10);
            Node n2 = new Node(0, 0, 10);
            n1.Connect(n2);
            Player player = new Player(n1, 100, 10, null, null);
            Pack pack = new Pack(5, n1, 10, 10);
            pack.Retreat(player);
            Assert.IsTrue(n1.PacksInNode.Count == 0);
            Assert.IsTrue(n2.PacksInNode.Count == 1);
            Assert.IsTrue(pack.Location == n2);

            Node n3 = new Node(0, 0, 10);
            Node n4 = new Node(0, 0, 10);
            n3.Connect(n4);
            player = new Player(n3, 10, 10, null, null);
            pack = new Pack(5, n3, 10, 10);
            pack.Retreat(player);
            Assert.IsTrue(n3.PacksInNode.Count == 1);
            Assert.IsTrue(n4.PacksInNode.Count == 0);
            Assert.IsTrue(pack.Location == n3);
        }

        [TestMethod]
        public void TestPackAttack()
        {
            Pack p = new Pack(3, new Node(0,0), 1, 1);
            MockCreature c = new MockCreature(new Node(0,0), 5);

            p.Attack(c);
            Assert.AreEqual(2, c.HitPoints);
        }

        [TestMethod]
        public void TestPackUpdate()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestPackLocation()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestPackSize()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestPackDead()
        {
            Assert.IsTrue(false);
        }
    }
}
