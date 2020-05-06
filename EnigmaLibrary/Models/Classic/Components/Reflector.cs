namespace EnigmaLibrary.Models.Classic.Components
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Reflector : IReflector
    {
        private readonly Dictionary<char, char> _connections;
        private readonly IUtilityFactory _utilityFactory;

        public Reflector(Dictionary<char, char> connections, ReflectorType type, IEventAggregator eventAggregator, IUtilityFactory utilityFactory)
        {
            _connections = connections;
            _utilityFactory = utilityFactory;

            Type = type;
            ReflectorAggregator = eventAggregator;
        }

        public IEventAggregator ReflectorAggregator { get; }
        public ReflectorType Type { get; }

        public async Task<Signal> Process(Signal signal)
        {
            var inputLetter = CommonHelper.NumberToLetter(signal.Value);

            var outputLetter = _connections[inputLetter];

            SendTranslationToUI(inputLetter, outputLetter);

            var resultValue = CommonHelper.LetterToNumber(outputLetter);

            return await _utilityFactory.CreateSignal(resultValue, false, SignalDirection.Out);
        }

        private void SendTranslationToUI(char from, char to)
        {
            var translation = _utilityFactory.CreateTranslation(from, to, SignalDirection.Out);

            ReflectorAggregator.PublishOnUIThread(translation);
        }
    }
}