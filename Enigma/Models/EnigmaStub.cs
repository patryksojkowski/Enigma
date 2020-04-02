using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Enigma.Models.Components;

namespace Enigma.Models
{
    public class EnigmaStub : IEnigma, IEnigmaSettings, IHandle<IEnigmaSettings>
    {
        private readonly IEventAggregator _eventAggregator;

        public EnigmaStub(IEventAggregator eventAggregator, IEnigmaSettings enigmaSettings)
        {
            Rotor1 = new Rotor('Z', RotorSlot.One);
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public Rotor Rotor1 { get; set; }
        public Rotor Rotor2 { get; set; }
        public Rotor Rotor3 { get; set; }
        public Reflector Reflector { get; set; }
        public Plugboard Plugboard { get; set; }

        public string Encrypt(string input)
        {
            return Rotor1.Process(input);
        }

        public void Handle(IEnigmaSettings enigmaSettings)
        {
            if (enigmaSettings.Rotor1 != null)
            {
                Rotor1 = enigmaSettings.Rotor1;
            }

            if (enigmaSettings.Rotor2 != null)
            {
                Rotor2 = enigmaSettings.Rotor2;
            }

            if (enigmaSettings.Rotor3 != null)
            {
                Rotor3 = enigmaSettings.Rotor3;
            }

            if (enigmaSettings.Reflector != null)
            {
                Reflector = enigmaSettings.Reflector;
            }

            if (enigmaSettings.Plugboard != null)
            {
                Plugboard = enigmaSettings.Plugboard;
            }
        }
    }
}
