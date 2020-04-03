using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Enigma.Models.Components;
using Enigma.Models.Enums;
using Enigma.ViewModels.Components;

namespace Enigma
{
    public class RotorViewModelFactory
    {
        IEventAggregator _eventAggregator;
        private readonly ComponentFactory _componentFactory;

        public RotorViewModelFactory(IEventAggregator eventAggregator, ComponentFactory componentFactory)
        {
            _eventAggregator = eventAggregator;
            _componentFactory = componentFactory;
        }

        public RotorViewModel Create(RotorSlot Slot)
        {
            return new RotorViewModel(_eventAggregator, _componentFactory, Slot);
        }


    }
}
