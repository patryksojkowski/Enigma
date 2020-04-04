namespace EnigmaLibrary.Models.Classic.Components
{
    using System;
    using System.Collections.Generic;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Reflector : IReflector
    {
        private readonly Dictionary<char, char> _connections;
        private readonly Func<char, bool, ISignal> _signalFactory;

        public Reflector(Dictionary<char, char> connections, Func<char, bool, ISignal> signalFactory, ReflectorType type)
        {
            _connections = connections;
            _signalFactory = signalFactory;
            Type = type;
        }

        public ReflectorType Type { get; set; }

        public ISignal Process(ISignal input)
        {
            return _signalFactory(_connections[input.Letter], false);
        }
    }
}
