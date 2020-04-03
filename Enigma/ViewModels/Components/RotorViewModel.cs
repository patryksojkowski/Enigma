using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Models;
using Caliburn.Micro;
using Enigma.Models.Components;
using Enigma.Models.Enums;

namespace Enigma.ViewModels.Components
{
    public class RotorViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ComponentFactory _componentFactory;
        public RotorSlot _slot;

        public RotorViewModel(IEventAggregator eventAggregator, ComponentFactory componentFactory, RotorSlot slot)
        {
            _eventAggregator = eventAggregator;
            _componentFactory = componentFactory;
            _slot = slot;
        }


        public void ChangeRotor()
        {
            _eventAggregator.PublishOnUIThread(_componentFactory.Create<Rotor>(_slot));
        }
    }
}
