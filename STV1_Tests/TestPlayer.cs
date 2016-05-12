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

        [TestMethod]
        public void TestPlayerMove()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestPlayerAttack()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestPlayerPickUp()
        {
            Node n = new Node();
            Node m = new Node();
            n.Connect(m);

            m.AddItem(new TimeCrystal());
            m.AddItem(new HealingPotion());

            Player p = new Player(n, 10, 10, null, null);
            Assert.AreEqual(false, p.UseHealingPotion());

            p.Move(m);
            Assert.AreEqual(true, p.UseHealingPotion());
            Assert.AreEqual(true, p.UseTimeCrystal());
        }

        [TestMethod]
        public void TestPlayerGetCommand()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestPlayerHitPoints()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestPlayerUseTimeCrystal()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestPlayerUseHealingPotion()
        {
            Assert.IsTrue(false);
        }
    }
}
