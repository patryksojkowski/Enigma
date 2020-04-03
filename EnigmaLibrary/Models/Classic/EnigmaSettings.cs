namespace EnigmaLibrary.Models.Classic
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaLibrary.Models.Enums;

    /// <summary>
    /// Classic EnigmaSettings with one Plugboard attached to sequence of three Rotors and Reflector.
    /// Signal carrying goes from keyboard through all Components back and forth.
    /// </summary>
    public class EnigmaSettings : IEnigmaSettings, IHandle<IRotor>, IHandle<IReflector>, IHandle<IPlugboard>
    {
        private readonly IEnigmaEventAggregator _eventAggregator;
        private readonly IComponentFactory _componentFactory;

        public EnigmaSettings(IEnigmaEventAggregator enigmaAggregator, IComponentFactory componentFactory)
        {
            _eventAggregator = enigmaAggregator;
            _eventAggregator.Subscribe(this);
            _componentFactory = componentFactory;

            AssignProperties();
            InitializeComponentList();

            void AssignProperties()
            {
                Rotor1 = _componentFactory.CreateRotor(RotorSlot.One);
                Rotor2 = _componentFactory.CreateRotor(RotorSlot.Two);
                Rotor3 = _componentFactory.CreateRotor(RotorSlot.Three);

                Reflector = _componentFactory.CreateReflector();
                Plugboard = _componentFactory.CreatePlugboard();
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

        public IRotor Rotor1 { get; set; }
        public IRotor Rotor2 { get; set; }
        public IRotor Rotor3 { get; set; }
        public IReflector Reflector { get; set; }
        public IPlugboard Plugboard { get; set; }
        public List<IEnigmaComponent> ComponentList { get; set; }

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
    }
}
