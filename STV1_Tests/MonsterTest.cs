using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class MonsterTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            float creatureHealth = 10;
            Node node = new Node();
            float attackRating = 2;
            Monster m = new Monster(node, creatureHealth, attackRating);

            Assert.AreEqual(creatureHealth, m.HitPoints);
            Assert.AreEqual(node, m.Location);
        }
    }
}
