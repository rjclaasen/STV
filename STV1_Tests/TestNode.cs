using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestNode
    {
        [TestMethod]
        public void TestPlayerInNode()
        {
            Node n = new Node();

            Player p = new Player(new Node(), 10, 5);

            Assert.AreEqual(false, n.PlayerInNode());
            n.PlayerEnters(p);
            Assert.AreEqual(true, n.PlayerInNode());
            n.PlayerLeaves();
            Assert.AreEqual(false, n.PlayerInNode());
        }

        [TestMethod]
        public void TestPackEnterAndLeave()
        {
            Node n = new Node();

            Pack p1 = new Pack(1, new Node());
            Pack p2 = new Pack(2, new Node());

            n.PackEnters(p1);
            Assert.IsTrue(n.PacksInNode.Contains(p1));
            n.PackEnters(p2);
            n.PackLeaves(p1);
            Assert.IsFalse(n.PacksInNode.Contains(p1));
            n.PackLeaves(p2);
            Assert.AreEqual(0, n.PacksInNode.Count);
        }
    }
}
