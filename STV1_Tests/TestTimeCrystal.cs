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
        public void TestTimeCrystalUseBridge()
        {
            Dungeon dungeon = new Dungeon(1);

            Node targetBridge = new Node(0, 0);

            // Find any bridge in the dungeon.
            targetBridge = dungeon.GetBridge(1);

            Player player = new Player(targetBridge, 10, 10, dungeon, null);

            targetBridge.UseItem(new TimeCrystal());

            Assert.IsTrue(targetBridge.Destroyed);
            Assert.IsTrue(dungeon.PathExists(player.Location, dungeon.Exit));
        }

        // Tests the usage of a time crystal in combat.
        [TestMethod]
        public void TestTimeCrystalUseCombat()
        {
            Node contested = new Node(1, 1, 10);
            Pack pack = new Pack(10, contested, 10, 5);
            Player player = new Player(contested, 150, 10, null, null);
            Assert.IsFalse(true);
        }
    }
}
