namespace EnigmaLibrary.Models.Classic
{
    using System;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Enigma : IEnigma
    {
        private readonly Func<char, bool, ISignal> _signalFactory;
        private readonly IEnigmaSettings _enigmaSettings;

        public Enigma(IEnigmaSettings enigmaSettings)
        {
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
    }
}
