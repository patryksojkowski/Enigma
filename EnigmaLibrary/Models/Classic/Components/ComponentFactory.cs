namespace EnigmaLibrary.Models.Classic.Components
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;
    using Newtonsoft.Json;

    public class ComponentFactory : IComponentFactry
    {
        private readonly Dictionary<ReflectorType, Dictionary<char, char>> _reflectorConnections = new Dictionary<ReflectorType, Dictionary<char, char>>();
        private readonly Dictionary<RotorType, char[]> _rotorConnections = new Dictionary<RotorType, char[]>();

        public Func<char, bool, ISignal> SignalFactory { get; }

        public ComponentFactory()
        {
            SignalFactory = (c, b) => CreateSignal(c, b);

            var reflectorJson = GetJsonContent(Directory.GetCurrentDirectory() + @"\Config\Reflector.json");
            _reflectorConnections = JsonConvert.DeserializeObject<Dictionary<ReflectorType, Dictionary<char, char>>>(reflectorJson);

            var rotorJson = GetJsonContent(Directory.GetCurrentDirectory() + @"\Config\Rotor.json");
            _rotorConnections = JsonConvert.DeserializeObject<Dictionary<RotorType, char[]>>(rotorJson);

            string GetJsonContent(string path)
            {
                return File.ReadAllText(path);
            }
        }

        public IPlugboard CreatePlugboard(Dictionary<char, char> plugboardConnections)
        {
            if (plugboardConnections is null)
            {
                plugboardConnections = new Dictionary<char, char>();
            }
            return new Plugboard(plugboardConnections, SignalFactory);
        }

        public IReflector CreateReflector(ReflectorType type)
        {
            return new Reflector(_reflectorConnections[type], SignalFactory, type);
        }

        public IRotor CreateRotor(RotorType type, RotorSlot slot, int position)
        {
            return new Rotor(slot, position, _rotorConnections[type], SignalFactory, type);
        }

        private ISignal CreateSignal(char input, bool step)
        {
            return new Signal(input, step);
        }
    }
}
