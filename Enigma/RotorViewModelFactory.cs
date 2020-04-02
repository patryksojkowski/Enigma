using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Enigma.Models.Components;
using Enigma.ViewModels;

namespace Enigma
{
    public class RotorViewModelFactory
    {
        IEventAggregator _eventAggregator;
        public RotorViewModelFactory(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public RotorViewModel Create(RotorSlot Slot)
        {
            return new RotorViewModel(_eventAggregator, Slot);
        }


    }
}
