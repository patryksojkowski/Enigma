namespace EnigmaLibrary.Models.Classic.Components
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Caliburn.Micro;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;
    using Newtonsoft.Json;

    public class ComponentFactory : IComponentFactory
    {
        private readonly Dictionary<ReflectorType, Dictionary<char, char>> _reflectorConnections = new Dictionary<ReflectorType, Dictionary<char, char>>();
        private readonly Dictionary<RotorType, char[]> _rotorConnections = new Dictionary<RotorType, char[]>();

        public ComponentFactory()
        {

            var reflectorJson = JsonHelper.GetJsonContent(Directory.GetCurrentDirectory() + @"\Config\Reflector.json");
            _reflectorConnections = JsonConvert.DeserializeObject<Dictionary<ReflectorType, Dictionary<char, char>>>(reflectorJson);

            var rotorJson = JsonHelper.GetJsonContent(Directory.GetCurrentDirectory() + @"\Config\Rotor.json");
            _rotorConnections = JsonConvert.DeserializeObject<Dictionary<RotorType, char[]>>(rotorJson);
        }

        public IPlugboard CreatePlugboard(Dictionary<char, char> plugboardConnections)
        {
            if (plugboardConnections is null)
            {
                plugboardConnections = new Dictionary<char, char>();
            }
            return new Plugboard(plugboardConnections, this);
        }

        public IReflector CreateReflector(ReflectorType type)
        {
            var aggregator = CreateEventAggregator();
            return new Reflector(_reflectorConnections[type], type, aggregator, this);
        }

        public IRotor CreateRotor(RotorType type, RotorSlot slot, int position)
        {
            var aggregator = CreateEventAggregator();
            return new Rotor(slot, position, _rotorConnections[type], type, aggregator, this);
        }

        public ISignal CreateSignal(int input, bool step, SignalDirection direction)
        {
            return new Signal(input, step, direction);
        }

        private IEventAggregator CreateEventAggregator()
        {
            return new EventAggregator();
        }

        public ILetterTranslation CreateTranslation(char from, char to, SignalDirection direction)
        {
            return new LetterTranslation(from, to, direction);
        }
    }
}
