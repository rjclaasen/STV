using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STV1;

namespace STV1_Tests
{
    /// <summary>
    /// Summary description for TestDungeon
    /// </summary>
    [TestClass]
    public class TestDungeon
    {
        

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestShortestPath()
        {
            Dungeon d = new Dungeon(1);
            List<Node> path = Dungeon.ShortestPath(d.Start, d.Exit);

            Assert.AreNotEqual(0, path.Count);
            
        }

        [TestMethod]
        public void TestPathExists()
        {
            Node n1 = new Node();
            Node n2 = new Node();

            n1.Connect(n2);

            Assert.IsTrue(Dungeon.PathExists(n1, n2));

            n1.Disconnect(n2);

            Assert.IsFalse(Dungeon.PathExists(n1, n2));
        }

        [TestMethod]
        public void TestDestroy()
        {
            Dungeon d = new Dungeon(1);


        }

        [TestMethod]
        public void TestLevel()
        {
            Assert.IsTrue(false);
        }
    }
}
