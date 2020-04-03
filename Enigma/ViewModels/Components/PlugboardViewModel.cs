using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Enigma.Models.Components;

namespace Enigma.ViewModels.Components
{
    public class PlugboardViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ComponentFactory _componentFactory;

        public PlugboardViewModel(IEventAggregator eventAggregator, ComponentFactory componentFactory)
        {
            _eventAggregator = eventAggregator;
            _componentFactory = componentFactory;
        }

        public void ChangePlugboard()
        {
            _eventAggregator.PublishOnUIThread(_componentFactory.Create<Plugboard>());
        }
    }
}
