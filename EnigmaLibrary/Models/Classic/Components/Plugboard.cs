namespace EnigmaLibrary.Models.Classic.Components
{
    using System;
    using System.Collections.Generic;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Plugboard : IPlugboard
    {
        private readonly IComponentFactory _componentFactory;
        public Dictionary<char, char> Connections { get; set; }

        public Plugboard(Dictionary<char, char> connections, IComponentFactory componentFactory)
        {
            _componentFactory = componentFactory;

            Connections = connections;
        }

        public ISignal Process(ISignal signal)
        {
            var inputValue = signal.Value;

            var inputLetter = CommonHelper.NumberToLetter(inputValue);
            var outputLetter = inputLetter;
            if (Connections.ContainsKey(inputLetter))
            {
                outputLetter = Connections[inputLetter];
            }

            var resultValue = CommonHelper.LetterToNumber(outputLetter);

            return _componentFactory.CreateSignal(resultValue, true, signal.Direction);
        }

        public void AddConnection(char from, char to)
        {
            if (from == to)
            {
                return;
            }
            if (Connections.ContainsKey(from) || Connections.ContainsKey(to))
            {
                return;
            }
            Connections.Add(from, to);
            Connections.Add(to, from);
        }
    }
}
