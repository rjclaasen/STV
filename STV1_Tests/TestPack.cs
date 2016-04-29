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
            Node n = new Node();
            Pack p = new Pack(1, new Node());

            p.Move(n);

            Assert.AreEqual(n, p.Location);
        }
    }
}
