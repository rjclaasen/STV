using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestCreature
    {
        [TestMethod]
        public void TestCreatureConstructor()
        {
            float creatureHealth = 10;
            Node node = new Node(0, 0, 0);
            float attackRating = 2;
            MockCreature m = new MockCreature(node, creatureHealth, attackRating);

            Assert.AreEqual(node, m.Location);
            Assert.AreEqual(creatureHealth, m.HitPoints);
            Assert.AreEqual(attackRating, m.AttackRating);
        }

        [TestMethod]
        public void TestCreatureMove()
        {
            Node oldNode = new Node(0, 0, 0);
            MockCreature m = new MockCreature(oldNode);

            Node newNode = new Node(0, 0, 0);

            m.Move(newNode);

            Assert.AreNotEqual(oldNode, m.Location);
            Assert.AreEqual(newNode, m.Location);
        }

        [TestMethod]
        public void TestCreatureAttack()
        {
            MockCreature m1 = new MockCreature(null);
            MockCreature m2 = new MockCreature(null);

            float startHealth = m2.HitPoints;
            float targetHealth = startHealth - m1.AttackRating;

            m1.Attack(m2);

            Assert.AreEqual(targetHealth, m2.HitPoints);
        }



        [TestMethod]
        public void TestCreatureHitPoints()
        {
            MockCreature m = new MockCreature(null);
            const float TARGETHP = 50f;

            m.HitPoints = TARGETHP;
            Assert.AreEqual(TARGETHP, m.HitPoints);

            m.HitPoints = 0;
            Assert.AreEqual(true, m.IsDead());
        }

        [TestMethod]
        public void TestCreatureLocation()
        {
            Node n = new Node();
            MockCreature c = new MockCreature(null);

            c.Location = n;
            Assert.AreEqual(n, c.Location);
        }
    }
}
