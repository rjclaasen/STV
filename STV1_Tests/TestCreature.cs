using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestCreature
    {
        [TestMethod]
        public void TestConstructor()
        {
            float creatureHealth = 10;
            Node node = new Node();
            float attackRating = 2;
            MockCreature m = new MockCreature(node, creatureHealth, attackRating);

            Assert.AreEqual(creatureHealth, m.HitPoints);
            Assert.AreEqual(node, m.Location);
        }

        [TestMethod]
        public void TestDead()
        {
            MockCreature m = new MockCreature(new Node());

            m.HitPoints = 0;

            Assert.AreEqual(true, m.IsDead());
        }

        [TestMethod]
        public void TestMove()
        {
            Node oldNode = new Node();
            MockCreature m = new MockCreature(oldNode);

            Node newNode = new Node();

            m.Move(newNode);

            Assert.AreNotEqual(oldNode, m.Location);
            Assert.AreEqual(newNode, m.Location);
        }

        [TestMethod]
        public void TestAttack()
        {
            MockCreature m1 = new MockCreature(new Node());
            MockCreature m2 = new MockCreature(new Node());

            float startHealth = m2.HitPoints;
            float targetHealth = startHealth - m1.AttackRating;

            m1.Attack(m2);

            Assert.AreEqual(targetHealth, m2.HitPoints);
        }
    }
}
