namespace EnigmaLibrary.Tests.Stubs
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;
    using static EnigmaLibrary.Models.Classic.EnigmaSettings;

    public class EnigmaSettingsStub : IEnigmaSettings
    {
        private readonly IEventAggregator _eventAggregator;

        public EnigmaSettingsStub(IEventAggregator eventAggregator, IComponentFactory componentFactory)
        {
            _eventAggregator = eventAggregator;
            ComponentFactory = componentFactory;
            ComponentList = new List<IEnigmaComponent>();
        }

        public IComponentFactory ComponentFactory { get; set; }
        public List<IEnigmaComponent> ComponentList { get; set; }
        public IPlugboard Plugboard { get; private set; }

        public IReflector Reflector { get; private set; }

        public IRotor Rotor1 { get; private set; }

        public IRotor Rotor2 { get; private set; }

        public IRotor Rotor3 { get; private set; }

        public IRotor GetRotor(RotorSlot slot)
        {
            return null;
        }

        public void LoadSettings(SavedSettings settings)
        {
            Rotor1 = ComponentFactory.CreateRotor(settings.Slot1.RotorType, RotorSlot.One, settings.Slot1.Position);
            Rotor2 = ComponentFactory.CreateRotor(settings.Slot2.RotorType, RotorSlot.Two, settings.Slot2.Position);
            Rotor3 = ComponentFactory.CreateRotor(settings.Slot3.RotorType, RotorSlot.Three, settings.Slot3.Position);

            Reflector = ComponentFactory.CreateReflector(settings.ReflectorType);
            Plugboard = ComponentFactory.CreatePlugboard(settings.PlugboardConnections);

            InitializeComponentList();
        }

        private void InitializeComponentList()
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
}