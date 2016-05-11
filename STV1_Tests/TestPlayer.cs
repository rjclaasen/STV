using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestPlayer
    {
        [TestMethod]
        public void TestPlayerConstructor()
        {
            Node n = new Node(0,0);
            float hp = 10;
            float ar = 2;
            Player p = new Player(n, hp, ar, null, null);

            Assert.AreEqual(n, p.Location);
            Assert.AreEqual(hp,p.HitPoints);
            Assert.AreEqual(ar,p.AttackRating);
        }
    }
}
