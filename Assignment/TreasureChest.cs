namespace Assignment
{
    public class TreasureChestException : Exception
    {
        public TreasureChestException(string message) : base(message)
        {

        }
    };

    public class TreasureChest
    {
        private State _state = State.Locked;
        private readonly Material _material;
        private readonly LockType _lockType;
        private readonly LootQuality _lootQuality;

        public TreasureChest()
        {
            _material = Material.Oak;
            _lockType = LockType.Novice;
            _lootQuality = LootQuality.Grey;
        }

        public TreasureChest(Material material, LockType lockType, LootQuality lootQuality)
        {
            _material = material;
            _lockType = lockType;
            _lootQuality = lootQuality;
        }

        public State? Manipulate(Action action)
        {
            switch (action)
            {
                case Action.Open:
                    ValidateState(State.Closed);
                    Open();
                    break;
                case Action.Close:
                    ValidateState(State.Open);
                    Close();
                    break;
                case Action.Lock:
                    ValidateState(State.Closed);
                    Lock();
                    break;
                case Action.Unlock:
                    ValidateState(State.Locked);
                    Unlock();
                    break;
                default:
                    throw new TreasureChestException($"Cannot handle this action: {action}");
            }

            return _state;
        }

        public State CurrentState()
        {
            return _state;
        }

        private void Unlock()
        {
            _state = State.Closed;
        }

        private void Lock()
        {
            _state = State.Locked;
        }

        private void Open()
        {
            _state = State.Open;
        }

        private void Close()
        {
            _state = State.Closed;
        }

        public override string ToString()
        {
            return $"A {_state} chest with the following properties:\nMaterial: {_material}\nLock Type: {_lockType}\nLoot Quality: {_lootQuality}";
        }

        private static void ConsoleHelper(string prop1, string prop2, string prop3)
        {
            Console.WriteLine($"Choose from the following properties.\n1.{prop1}\n2.{prop2}\n3.{prop3}");
        }

        private void ValidateState(State beforeChangedState)
        {
            if (_state != beforeChangedState)
            {
                throw new TreasureChestException($"State is not correct. Current state is {_state}.");
            }
        }

        public enum State { Open, Closed, Locked };
        public enum Action { Open, Close, Lock, Unlock };
        public enum Material { Oak, RichMahogany, Iron };
        public enum LockType { Novice, Intermediate, Expert };
        public enum LootQuality { Grey, Green, Purple };
    }
}
