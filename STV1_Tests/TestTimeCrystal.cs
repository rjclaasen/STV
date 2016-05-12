using System;
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
            Node n = new Node(1, 1, 10);
            n.Connect(contested);
            n.AddItem(new TimeCrystal());

            Pack pack = new Pack(10, contested, 10, 5);
            Player player = new Player(n, 150, 10, null, null);
            
            Command useTimeCrystal = new Command(true, false, false, false);
            player.SetCommand(useTimeCrystal);
            player.Move(contested);
            

            Assert.AreEqual(0, contested.PacksInNode.Count);

            contested = new Node(1, 1, 10);
            n = new Node(1, 1, 10);
            n.Connect(contested);
            n.AddItem(new TimeCrystal());

            pack = new Pack(10, contested, 10, 5);
            player = new Player(n, 50, 5, null, null);
            
            useTimeCrystal = new Command(true, false, false, false);
            player.SetCommand(useTimeCrystal);
            player.Move(contested);

            Assert.IsTrue(player.IsDead());
            Assert.AreEqual(5, pack.Monster.HitPoints);
            foreach (Monster m in pack.Monsters)
                Assert.AreEqual(5, m.HitPoints);

        }
    }
}
