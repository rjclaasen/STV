﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestTimeCrystal
    {
        // Tests the usage of a time crystal on a bridge.
        [TestMethod]
        public void TestUseBridge()
        {
            Dungeon dungeon = new Dungeon(1);

            Node targetBridge = new Node(0, 0);

            // Find any bridge in the dungeon.
            targetBridge = dungeon.GetBridge(1);

            Player player = new Player(targetBridge, 10, 10, dungeon);

            targetBridge.UseItem(new TimeCrystal());

            Assert.IsTrue(targetBridge.Destroyed);
            Assert.IsTrue(Dungeon.PathExists(player.Location, dungeon.Exit));
        }

        // Tests the usage of a time crystal in combat.
        [TestMethod]
        public void TestUseCombat()
        {
            Assert.IsFalse(true);
        }
    }
}
