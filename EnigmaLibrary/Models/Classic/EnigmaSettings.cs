namespace EnigmaLibrary.Models.Classic
{
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;

    /// <summary>
    /// EnigmaSettings with one Plugboard attached to sequence of three Rotors and Reflector.
    /// Signal carrying goes from keyboard through all Components back and forth.
    /// </summary>
    public partial class EnigmaSettings : IEnigmaSettings, IHandle<IRotor>, IHandle<IReflector>, IHandle<IPlugboard>
    {
        private SettingsHelper _settingsHelper;

        public EnigmaSettings(IEventAggregator enigmaAggregator, IComponentFactory componentFactory)
        {
            enigmaAggregator.Subscribe(this);
            ComponentFactory = componentFactory;

            _settingsHelper = new SettingsHelper(this);

            _settingsHelper.InitializeSettings();
        }

        ~EnigmaSettings()
        {
            _settingsHelper.SaveSettings();
        }

        public IComponentFactory ComponentFactory { get; }

        public List<IEnigmaComponent> ComponentList { get; private set; }

        public IPlugboard Plugboard { get; private set; }

        public IReflector Reflector { get; private set; }

        public IRotor Rotor1 { get; private set; }

        public IRotor Rotor2 { get; private set; }

        public IRotor Rotor3 { get; private set; }

        public IRotor GetRotor(RotorSlot slot) => slot switch
        {
            RotorSlot.One => Rotor1,
            RotorSlot.Two => Rotor2,
            RotorSlot.Three => Rotor3,
            _ => throw new ArgumentOutOfRangeException()
        };

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
        }

        public void Handle(IReflector reflector)
        {
            Reflector = reflector;
        }

        public void Handle(IPlugboard plugboard)
        {
            Plugboard = plugboard;
        }
    }
}