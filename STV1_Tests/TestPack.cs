using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    [TestClass]
    public class TestPack
    {
        [TestMethod]
        public void TestConstructor()
        {
            Node n = new Node();
            Pack p = new Pack(5, n);

            Assert.AreEqual(5, p.Size);
            Assert.AreEqual(n, p.Location);
        }

        [TestMethod]
        public void TestMove()
        {
            Node n1 = new Node();
            Node n2 = new Node();

            n1.AddConnection(n2);
            Pack p = new Pack(1, n1);

            p.Move(n2);

            Assert.AreEqual(n2, p.Location);
        }
    }
}
