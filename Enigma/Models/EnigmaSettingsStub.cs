using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Enigma.Models.Components;
using Enigma.Models.Enums;

namespace Enigma.Models
{
    public class EnigmaSettingsStub : IEnigmaSettings, IHandle<Rotor>, IHandle<Reflector>, IHandle<Plugboard>
    {
        private readonly IEventAggregator _eventAggregator;

        public EnigmaSettingsStub(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);
        }
        public Rotor Rotor1 { get; set; }
        public Rotor Rotor2 { get; set; }
        public Rotor Rotor3 { get; set; }
        public Reflector Reflector { get; set; }
        public Plugboard Plugboard { get; set; }

        public void Handle(Rotor rotor)
        {
            ClearComponents();
            if (rotor.Slot == RotorSlot.One)
            {
                Rotor1 = rotor;
            }
            else if (rotor.Slot == RotorSlot.Two)
            {
                Rotor2 = rotor;
            }
            else if (rotor.Slot == RotorSlot.Three)
            {
                Rotor3 = rotor;
            }
            _eventAggregator.PublishOnUIThread(this);
        }

        public void Handle(Reflector reflector)
        {
            ClearComponents();
            Reflector = reflector;
            _eventAggregator.PublishOnUIThread(this);
        }

        public void Handle(Plugboard plugboard)
        {
            ClearComponents();
            Plugboard = plugboard;
            _eventAggregator.PublishOnUIThread(this);
        }

        private void ClearComponents()
        {
            Rotor1 = null;
            Rotor2 = null;
            Rotor3 = null;
            Reflector = null;
            Plugboard = null;
        }
    }
}
