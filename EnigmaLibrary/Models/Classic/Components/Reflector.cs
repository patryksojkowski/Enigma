namespace EnigmaLibrary.Models.Classic.Components
{
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Reflector : IReflector
    {
        private readonly Dictionary<char, char> _connections;
        private readonly Func<char, bool, ISignal> _signalFactory;
        private readonly Func<char, char, ILetterTranslation> _translationFactory;

        public Reflector(Dictionary<char, char> connections, Func<char, bool, ISignal> signalFactory, ReflectorType type, IEventAggregator eventAggregator,
            Func<char, char, ILetterTranslation> translationFactory)
        {
            _connections = connections;
            _signalFactory = signalFactory;
            _translationFactory = translationFactory;

            Type = type;
            ReflectorAggregator = eventAggregator;
        }

        public ReflectorType Type { get; set; }
        public IEventAggregator ReflectorAggregator { get; }

        public ISignal Process(ISignal incoming)
        {
            var input = incoming.Letter;
            var result = _connections[incoming.Letter];
            var translation = _translationFactory(input, result);

            ReflectorAggregator.PublishOnUIThread(translation);
            return _signalFactory(result, false);
        }
    }
}
