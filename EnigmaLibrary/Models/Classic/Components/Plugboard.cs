namespace EnigmaLibrary.Models.Classic.Components
{
    using System;
    using System.Collections.Generic;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Plugboard : IPlugboard
    {
        private readonly Func<char, bool, ISignal> _signalFactory;
        public Dictionary<char, char> Connections { get; set; }

        public Plugboard(Dictionary<char, char> connections, Func<char, bool, ISignal> signalFactory)
        {
            _signalFactory = signalFactory;

            Connections = connections;
        }

        public ISignal Process(ISignal input)
        {
            var letter = input.Letter;
            var output = letter;
            if (Connections.ContainsKey(letter))
            {
                output = Connections[letter];
            }
            return _signalFactory(output, true);
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
