﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestHealingPotion
    {
        [TestMethod]
        public void TestHealingPotionUse()
        {
            Player p = new Player(new Node(0,0), 15, 5, null, null);
            HealingPotion hp1 = new HealingPotion();
            HealingPotion hp2 = new HealingPotion();

            p.HitPoints -= 13;

            hp1.Use(p);
            Assert.AreEqual(12, p.HitPoints);
            hp2.Use(p);
            Assert.AreEqual(15, p.HitPoints);
        }
    }
}
