namespace EnigmaLibrary.Models.Classic.Components
{
    using System;
    using Caliburn.Micro;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Rotor : IRotor
    {
        private readonly IComponentFactory _componentFactory;

        public Rotor(RotorSlot slot, int position, char[] connections, RotorType type, IEventAggregator eventAggregator, IComponentFactory componentFactory)
        {
            Connections = connections;
            Slot = slot;
            PositionShift = position;
            Type = type;
            RotorAggregator = eventAggregator;
            _componentFactory = componentFactory;
        }

        public RotorSlot Slot { get; set; }
        public int PositionShift { get; private set; }
        public RotorType Type { get; set; }
        public IEventAggregator RotorAggregator { get; private set; }

        public char[] Connections { get; private set; }

        public bool Move(int steps)
        {
            var nextStep = false;

            PositionShift += steps;

            if (PositionShift >= 26)
            {
                nextStep = true;
            }

            PositionShift = CommonHelper.To0_25Range(PositionShift);

            return nextStep;
        }

        public ISignal Process(ISignal signal) // S = 18
        {
            bool nextStep = false;

            if (signal.Step)
            {
                nextStep = Move(1);
            }

            var inputValue = signal.Value; // 18
            var shiftedInput = AddShift(inputValue); // 18 + 7 = 25

            // translation
            char inputLetter, outputLetter;

            if  (signal.Direction == SignalDirection.In)
            {
                inputLetter = CommonHelper.NumberToLetter(shiftedInput); // 25 = Z
                outputLetter = Connections[shiftedInput]; // Conn[25] = E
            }
            else
            {
                inputLetter = CommonHelper.NumberToLetter(shiftedInput);
                var outputPosition = Array.IndexOf(Connections, inputLetter);
                outputLetter = CommonHelper.NumberToLetter(outputPosition);
            }

            var translation = _componentFactory.CreateTranslation(inputLetter, outputLetter, signal.Direction); // Z -> E
            RotorAggregator.PublishOnUIThread(translation);

            // - remove position shift
            var shiftedOutput = CommonHelper.LetterToNumber(outputLetter); // 4
            var resultValue = RemoveShift(shiftedOutput); // 4 - 7 = -3 = 22

            return _componentFactory.CreateSignal(resultValue, nextStep, signal.Direction);
        }

        private int AddShift(int inputPosition)
        {
            return CommonHelper.To0_25Range(inputPosition + PositionShift);
        }

        private int RemoveShift(int outputPosition)
        {
            return CommonHelper.To0_25Range(outputPosition - PositionShift);
        }
    }
}
