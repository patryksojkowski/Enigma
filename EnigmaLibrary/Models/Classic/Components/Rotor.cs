namespace EnigmaLibrary.Models.Classic.Components
{
    using System;
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Rotor : IRotor
    {
        private readonly IUtilityFactory _utilityFactory;

        public Rotor(RotorSlot slot, int position, char[] connections, RotorType type, IEventAggregator eventAggregator, IUtilityFactory utilityFactory)
        {
            _utilityFactory = utilityFactory;

            Connections = connections;
            Slot = slot;
            PositionShift = position;
            Type = type;
            RotorAggregator = eventAggregator;
        }

        public char[] Connections { get; private set; }
        public int PositionShift { get; private set; }
        public IEventAggregator RotorAggregator { get; private set; }
        public RotorSlot Slot { get; private set; }
        public RotorType Type { get; private set; }

        public async Task<bool> Move(int steps)
        {
            var nextStep = false;

            PositionShift += steps;

            if (PositionShift >= 26)
            {
                nextStep = true;
            }

            PositionShift = CommonHelper.To0_25Range(PositionShift);

            await SendStepsToUI(steps);

            return nextStep;
        }

        public async Task<Signal> Process(Signal signal)
        {
            bool nextStep = false;

            if (signal.Step)
            {
                nextStep = await Move(1);
            }

            var inputValue = signal.Value;
            var shiftedInput = AddShift(inputValue);

            char inputLetter, outputLetter;

            if (signal.Direction == SignalDirection.In)
            {
                inputLetter = CommonHelper.NumberToLetter(shiftedInput);
                outputLetter = Connections[shiftedInput];
            }
            else
            {
                inputLetter = CommonHelper.NumberToLetter(shiftedInput);
                var outputPosition = Array.IndexOf(Connections, inputLetter);
                outputLetter = CommonHelper.NumberToLetter(outputPosition);
            }

            var shiftedOutput = CommonHelper.LetterToNumber(outputLetter);
            var resultValue = RemoveShift(shiftedOutput);

            SendTranslationToUI(inputLetter, outputLetter, signal.Direction);

            return await _utilityFactory.CreateSignal(resultValue, nextStep, signal.Direction);
        }

        private int AddShift(int inputPosition)
        {
            return CommonHelper.To0_25Range(inputPosition + PositionShift);
        }

        private int RemoveShift(int outputPosition)
        {
            return CommonHelper.To0_25Range(outputPosition - PositionShift);
        }

        private async Task SendStepsToUI(int steps)
        {
            var stepMessage = _utilityFactory.CreateRotorStepMessage(steps);
            await RotorAggregator.PublishOnUIThreadAsync(stepMessage);
        }

        private void SendTranslationToUI(char from, char to, SignalDirection direction)
        {
            var translation = _utilityFactory.CreateTranslation(from, to, direction);
            RotorAggregator.PublishOnUIThread(translation);
        }
    }
}