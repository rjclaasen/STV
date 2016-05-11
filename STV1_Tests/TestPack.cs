using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestPack
    {
        [TestMethod]
        public void TestPackConstructor()
        {
            Node n = new Node(0,0);
            Pack p = new Pack(5, n);

            Assert.AreEqual(5, p.Size);
            Assert.AreEqual(n, p.Location);
        }

        [TestMethod]
        public void TestPackMove()
        {
            const int PACKSIZE = 1;

            Node n1 = new Node(0,0,PACKSIZE);
            Node n2 = new Node(0,0,PACKSIZE);

            n1.Connect(n2);
            Pack p = new Pack(PACKSIZE, n1);

            p.Move(n2);

            Assert.AreEqual(n2, p.Location);
        }

        [TestMethod]
        public void TestPackAttack()
        {
            Pack p = new Pack(3, new Node(0,0), 1, 1);
            MockCreature c = new MockCreature(new Node(0,0), 5);

            p.Attack(c);
            Assert.AreEqual(2, c.HitPoints);
        }
    }
}
