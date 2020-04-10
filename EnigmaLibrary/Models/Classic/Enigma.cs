namespace EnigmaLibrary.Models.Classic
{
    using System;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Enigma : IEnigma
    {
        private readonly IComponentFactory _componentFactory;
        private readonly IEnigmaSettings _enigmaSettings;

        public Enigma(IEnigmaSettings enigmaSettings)
        {
            _enigmaSettings = enigmaSettings;
            _componentFactory = enigmaSettings.ComponentFactory;
        }

        public char Encrypt(char inputLetter)
        {
            var inputPosition = CommonHelper.LetterToNumber(inputLetter);
            var signal = _componentFactory.CreateSignal(inputPosition, false, SignalDirection.In);

            foreach(var component in _enigmaSettings.ComponentList)
            {
                signal = component.Process(signal);
            }

            var resultLetter = CommonHelper.NumberToLetter(signal.Value);
            return resultLetter;
        }
    }
}
