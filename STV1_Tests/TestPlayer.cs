﻿using System;
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
            Node n1 = new Node(0, 0, 10);
            Player player = new Player(n1, 10, 10, null, null);
            Assert.AreEqual(10, player.HitPoints);
            player.HitPoints = 100;
            Assert.AreEqual(10, player.HitPoints);

        }

        [TestMethod]
        public void TestPlayerUseTimeCrystal()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestPlayerUseHealingPotion()
        {
            Node n1 = new Node();
            Node n2 = new Node();
            n1.Connect(n2);

            Player player = new Player(n1, 100, 10, null, null);
            player.HitPoints = 85;

            n2.AddItem(new HealingPotion());
            player.Move(n2);
            player.UseHealingPotion();

            Assert.AreEqual(95, player.HitPoints);

            n1.AddItem(new HealingPotion());
            player.Move(n1);
            player.UseHealingPotion();
            Assert.AreEqual(100, player.HitPoints);
        }
    }
}
