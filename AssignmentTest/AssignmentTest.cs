using System.Reflection;
using Assignment;

namespace AssignmentTest
{
    [TestClass]
    public class AssignmentTests
    {
        [TestMethod]
        public void DummyTest()
        {
            Assert.AreNotSame(1, 2);
        }

        [TestMethod]
        public void TestOpen()
        {
            TreasureChest chest = new TreasureChest(); // default state is Locked.
            var method = GetMethodByReflection("Open");
            method.Invoke(chest, null);
            Assert.AreEqual(TreasureChest.State.Open, chest.CurrentState());
        }

        [TestMethod]
        public void TestClose()
        {
            TreasureChest chest = new TreasureChest(); // default state is Locked.
            var method = GetMethodByReflection("Close");
            method.Invoke(chest, null);
            Assert.AreEqual(TreasureChest.State.Closed, chest.CurrentState());
        }

        [TestMethod]
        public void TestLock()
        {
            TreasureChest chest = new TreasureChest(); // default state is Locked.
            var method = GetMethodByReflection("Lock");
            method.Invoke(chest, null);
            Assert.AreEqual(TreasureChest.State.Locked, chest.CurrentState());
        }

        [TestMethod]
        public void TestUnLock()
        {
            TreasureChest chest = new TreasureChest(); // default state is Locked.
            var method = GetMethodByReflection("Unlock");
            method.Invoke(chest, null);
            Assert.AreEqual(TreasureChest.State.Closed, chest.CurrentState());
        }

        [TestMethod]
        public void TestManipulate_UnlockAction_StateChangesToClosed()
        {
            TreasureChest chest = new TreasureChest(); // default state is Locked.
            chest.Manipulate(TreasureChest.Action.Unlock);

            Assert.AreEqual(TreasureChest.State.Closed, chest.CurrentState());
        }

        [TestMethod]
        public void TestManipulate_OpenAction_StateChangesToOpen()
        {
            TreasureChest chest = new TreasureChest();

            SetChestStateByReflection(chest, TreasureChest.State.Closed);

            chest.Manipulate(TreasureChest.Action.Open);
            Assert.AreEqual(TreasureChest.State.Open, chest.CurrentState());
        }

        [TestMethod]
        public void TestManipulate_CloseAction_StateChangesToClosed()
        {
            TreasureChest chest = new TreasureChest();

            SetChestStateByReflection(chest, TreasureChest.State.Open);

            chest.Manipulate(TreasureChest.Action.Close);
            Assert.AreEqual(TreasureChest.State.Closed, chest.CurrentState());
        }

        [TestMethod]
        public void TestManipulate_LockAction_StateChangesToLocked()
        {
            TreasureChest chest = new TreasureChest();

            SetChestStateByReflection(chest, TreasureChest.State.Closed);

            chest.Manipulate(TreasureChest.Action.Lock);
            Assert.AreEqual(TreasureChest.State.Locked, chest.CurrentState());
        }

        [TestMethod]
        public void TestManipulate_UnLockAction_StateChangesToClosed()
        {
            TreasureChest chest = new TreasureChest();

            SetChestStateByReflection(chest, TreasureChest.State.Locked);

            chest.Manipulate(TreasureChest.Action.Unlock);
            Assert.AreEqual(TreasureChest.State.Closed, chest.CurrentState());
        }

        [TestMethod]
        public void TestManipulate_InvalidAction_ThrowsTreasureChestException()
        {
            TreasureChest chest = new TreasureChest();

            Assert.ThrowsException<TreasureChestException>(() => chest.Manipulate((TreasureChest.Action)100));
        }

        [TestMethod]
        public void TestManipulate_InvalidState_OpenAction_WhenStateIsLocked()
        {
            TreasureChest chest = new TreasureChest();
            SetChestStateByReflection(chest, TreasureChest.State.Locked);

            Assert.ThrowsException<TreasureChestException>(() => chest.Manipulate(TreasureChest.Action.Open));
        }

        [TestMethod]
        public void TestManipulate_InvalidState_LockAction_WhenStateIsOpen()
        {
            TreasureChest chest = new TreasureChest();
            SetChestStateByReflection(chest, TreasureChest.State.Open);

            Assert.ThrowsException<TreasureChestException>(() => chest.Manipulate(TreasureChest.Action.Lock));
        }

        private void SetChestStateByReflection(TreasureChest chest, TreasureChest.State state)
        {
            // Use reflection to access and set the private variable "_state"
            Type chestType = typeof(TreasureChest);
            FieldInfo stateField = chestType.GetField("_state", BindingFlags.NonPublic | BindingFlags.Instance);
            stateField.SetValue(chest, state);
        }

        private MethodInfo GetMethodByReflection(string methodName)
        {
            Type chestType = typeof(TreasureChest);
            MethodInfo privateMethod = chestType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            return privateMethod;
        }
    }
}
