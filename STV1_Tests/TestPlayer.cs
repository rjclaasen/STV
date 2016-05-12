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
            Game game = new Game();
            Node node = new Node();
            Dungeon dungeon = game.Dungeon;

            game.Player.Move(node);

            Assert.IsFalse(dungeon.Start.PlayerInNode());
            Assert.IsTrue(node.PlayerInNode());

            game.Player.Move(dungeon.Exit);

            Assert.AreNotEqual(dungeon, game.Player.Dungeon);
        }

        [TestMethod]
        public void TestPlayerAttack()
        {
            MockCreature c = new MockCreature();
            Player p = new Player(null, 10, 5, null, null);

            p.Attack(c);

            Assert.AreEqual(1, p.KillPoints);
        }

        [TestMethod]
        public void TestPlayerPickUp()
        {
            Assert.IsTrue(false);
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
