namespace EnigmaLibrary.Models.Classic
{
    using System;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Enigma : IEnigma, IHandle<IEnigmaSettings>
    {
        private readonly IEnigmaEventAggregator _eventAggregator;
        private readonly Func<char, bool, ISignal> _signalFactory;

        private IEnigmaSettings _enigmaSettings;

        public Enigma(IEnigmaEventAggregator enigmaAggregator, IEnigmaSettings enigmaSettings)
        {
            _eventAggregator = enigmaAggregator;
            _eventAggregator.Subscribe(this);

            _enigmaSettings = enigmaSettings;
            _signalFactory = enigmaSettings.ComponentFactory.SignalFactory;
        }

        public char Encrypt(char input)
        {
            var signal = _signalFactory(char.ToUpper(input), false);
            foreach(var component in _enigmaSettings.ComponentList)
            {
                signal = component.Process(signal);
            }

            return signal.Letter;
        }

        public void Handle(IEnigmaSettings enigmaSettings)
        {
            _enigmaSettings = enigmaSettings;
        }
    }
}
