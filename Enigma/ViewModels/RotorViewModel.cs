using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Models;
using Caliburn.Micro;
using Enigma.Models.Components;

namespace Enigma.ViewModels
{
    public class RotorViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        public RotorSlot _slot;

        public RotorViewModel(IEventAggregator eventAggregator, RotorSlot slot)
        {
            _eventAggregator = eventAggregator;
            _slot = slot;
        }


        public void ChangeRotor()
        {
            _eventAggregator.PublishOnUIThread(new Rotor('B', _slot));
        }
    }
}
