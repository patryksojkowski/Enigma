namespace EnigmaLibrary.Models.Classic.Components
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Reflector : IReflector
    {
        private readonly Dictionary<char, char> _connections;
        private readonly IComponentFactory _componentFactory;

        public Reflector(Dictionary<char, char> connections, ReflectorType type, IEventAggregator eventAggregator, IComponentFactory componentFactory)
        {
            _connections = connections;
            _componentFactory = componentFactory;

            Type = type;
            ReflectorAggregator = eventAggregator;
        }

        public ReflectorType Type { get; set; }
        public IEventAggregator ReflectorAggregator { get; }

        public ISignal Process(ISignal signal)
        {
            var inputLetter = CommonHelper.NumberToLetter(signal.Value);

            var outputLetter = _connections[inputLetter];
            var translation = _componentFactory.CreateTranslation(inputLetter, outputLetter, SignalDirection.Out);

            ReflectorAggregator.PublishOnUIThread(translation);

            var resultValue = CommonHelper.LetterToNumber(outputLetter);

            return _componentFactory.CreateSignal(resultValue, false, SignalDirection.Out);
        }
    }
}
