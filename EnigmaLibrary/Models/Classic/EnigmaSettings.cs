namespace EnigmaLibrary.Models.Classic
{
    using System.Collections.Generic;
    using System.IO;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaLibrary.Models.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// Classic EnigmaSettings with one Plugboard attached to sequence of three Rotors and Reflector.
    /// Signal carrying goes from keyboard through all Components back and forth.
    /// </summary>
    public class EnigmaSettings : IEnigmaSettings, IHandle<IRotor>, IHandle<IReflector>, IHandle<IPlugboard>
    {
        private readonly IEnigmaEventAggregator _eventAggregator;

        public EnigmaSettings(IEnigmaEventAggregator enigmaAggregator, IComponentFactry componentFactory)
        {
            _eventAggregator = enigmaAggregator;
            _eventAggregator.Subscribe(this);
            ComponentFactory = componentFactory;

            var settingsJson = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Config\SavedSettings.json");
            var savedSettings = JsonConvert.DeserializeObject<SavedSettings>(settingsJson);

            AssignProperties(savedSettings);
            InitializeComponentList();

            void AssignProperties(SavedSettings settings)
            {
                if (settings != null)
                {
                    Rotor1 = ComponentFactory.CreateRotor(settings.Slot1.RotorType, RotorSlot.One, settings.Slot1.Position);
                    Rotor2 = ComponentFactory.CreateRotor(settings.Slot2.RotorType, RotorSlot.Two, settings.Slot2.Position);
                    Rotor3 = ComponentFactory.CreateRotor(settings.Slot3.RotorType, RotorSlot.Three, settings.Slot3.Position);
                    Reflector = ComponentFactory.CreateReflector(settings.ReflectorType);
                    Plugboard = ComponentFactory.CreatePlugboard(settings.PlugboardConnections);
                    return;
                }

                Rotor1 = ComponentFactory.CreateRotor(RotorType.I, RotorSlot.One, 0);
                Rotor2 = ComponentFactory.CreateRotor(RotorType.II, RotorSlot.Two, 0);
                Rotor3 = ComponentFactory.CreateRotor(RotorType.III, RotorSlot.Three, 0);

                Reflector = ComponentFactory.CreateReflector(ReflectorType.B);
                Plugboard = ComponentFactory.CreatePlugboard(null);

            }

            void InitializeComponentList()
            {
                ComponentList = new List<IEnigmaComponent>
                {
                    Plugboard,
                    Rotor1,
                    Rotor2,
                    Rotor3,
                    Reflector,
                    Rotor3,
                    Rotor2,
                    Rotor1,
                    Plugboard
                };
            }
        }

        ~EnigmaSettings()
        {
            var settings = new SavedSettings()
            {
                ReflectorType = Reflector.Type,
                PlugboardConnections = Plugboard.Connections,

                Slot1 = new SavedSettings.Slot
                {
                    Position = Rotor1.Position,
                    RotorType = Rotor1.Type
                },

                Slot2 = new SavedSettings.Slot
                {
                    Position = Rotor2.Position,
                    RotorType = Rotor2.Type
                },

                Slot3 = new SavedSettings.Slot
                {
                    Position = Rotor3.Position,
                    RotorType = Rotor3.Type
                },
            };
            var settingsJson = JsonConvert.SerializeObject(settings);
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\Config\SavedSettings.json", settingsJson);
        }

        public IRotor Rotor1 { get; set; }
        public IRotor Rotor2 { get; set; }
        public IRotor Rotor3 { get; set; }
        public IReflector Reflector { get; set; }
        public IPlugboard Plugboard { get; set; }
        public List<IEnigmaComponent> ComponentList { get; set; }

        public IComponentFactry ComponentFactory { get; set; }

        public void Handle(IRotor rotor)
        {
            switch (rotor.Slot)
            {
                case RotorSlot.One:
                    Rotor1 = rotor;
                    break;
                case RotorSlot.Two:
                    Rotor2 = rotor;
                    break;
                case RotorSlot.Three:
                    Rotor3 = rotor;
                    break;
                default:
                    break;
            }
            _eventAggregator.Publish(this);
        }

        public void Handle(IReflector reflector)
        {
            Reflector = reflector;
            _eventAggregator.Publish(this);
        }

        public void Handle(IPlugboard plugboard)
        {
            Plugboard = plugboard;
            _eventAggregator.Publish(this);
        }

        private class SavedSettings
        {
            public ReflectorType ReflectorType { get; set; }
            public Slot Slot1 { get; set; }
            public Slot Slot2 { get; set; }
            public Slot Slot3 { get; set; }
            public Dictionary<char, char> PlugboardConnections { get; set; }

            public struct Slot
            {
                public RotorType RotorType { get; set; }
                public int Position { get; set; }
            }
        }
    }
}
