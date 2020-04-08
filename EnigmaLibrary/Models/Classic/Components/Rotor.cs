namespace EnigmaLibrary.Models.Classic.Components
{
    using System;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Rotor : IRotor
    {
        private readonly char[] _connections;
        private readonly Func<char, bool, ISignal> _signalFactory;

        public Rotor(RotorSlot slot, int position, char[] connections, Func<char, bool, ISignal> signalFactory, RotorType type)
        {
            _connections = connections;
            _signalFactory = signalFactory;
            Slot = slot;
            Position = position;
            Type = type;
        }

        public RotorSlot Slot { get; set; }
        public int Position { get; set; }
        public RotorType Type { get; set; }

        public void Move(int steps)
        {
            Position += steps;
            Position %= 26;
            Position += 26;
            Position %= 26;
        }

        public ISignal Process(ISignal signal)
        {
            bool nextStep = false;

            if (signal.Step)
            {
                Position++;
            }
            if (Position == 26)
            {
                Position = 0;
                nextStep = true;
            }

            var readPosition = (Position + signal.Letter - 65) % 26;
            var output = _connections[readPosition];
            return _signalFactory(output, nextStep);
        }
    }
}
