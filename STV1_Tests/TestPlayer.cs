﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestPlayer
    {
        [TestMethod]
        public void TestConstructor()
        {
            Node n = new Node();
            float hp = 10;
            float ar = 2;
            Player p = new Player(n, hp, ar);

            Assert.AreEqual(n, p.Location);
            Assert.AreEqual(hp,p.HitPoints);
            Assert.AreEqual(ar,p.AttackRating);
        }
    }
}