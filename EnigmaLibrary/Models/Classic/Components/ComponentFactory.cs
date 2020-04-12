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
        private Dictionary<ReflectorType, Dictionary<char, char>> _reflectorConnections = new Dictionary<ReflectorType, Dictionary<char, char>>();
        private Dictionary<RotorType, char[]> _rotorConnections = new Dictionary<RotorType, char[]>();

        public ComponentFactory(IUtilityFactory utilityFactory)
        {
            InitializeDictionaries();
            UtilityFactory = utilityFactory;
        }

        public IUtilityFactory UtilityFactory { get; }

        public IPlugboard CreatePlugboard(Dictionary<char, char> plugboardConnections = null)
        {
            if (plugboardConnections is null)
            {
                plugboardConnections = new Dictionary<char, char>();
            }
            return new Plugboard(plugboardConnections, UtilityFactory);
        }

        public IReflector CreateReflector(ReflectorType type)
        {
            var aggregator = CreateEventAggregator();
            return new Reflector(_reflectorConnections[type], type, aggregator, UtilityFactory);
        }

        public IRotor CreateRotor(RotorType type, RotorSlot slot, int position)
        {
            var aggregator = CreateEventAggregator();
            return new Rotor(slot, position, _rotorConnections[type], type, aggregator, UtilityFactory);
        }

        private IEventAggregator CreateEventAggregator()
        {
            return new EventAggregator();
        }

        private void InitializeDictionaries()
        {
            try
            {
                var reflectorJson = JsonHelper.GetJsonContent(Directory.GetCurrentDirectory() + @"\Config\Reflector.json");
                _reflectorConnections = JsonConvert.DeserializeObject<Dictionary<ReflectorType, Dictionary<char, char>>>(reflectorJson);

                var rotorJson = JsonHelper.GetJsonContent(Directory.GetCurrentDirectory() + @"\Config\Rotor.json");
                _rotorConnections = JsonConvert.DeserializeObject<Dictionary<RotorType, char[]>>(rotorJson);
            }
            catch (Exception ex)
            {
                throw new TypeInitializationException(nameof(ComponentFactory), ex);
            }
        }
    }
}