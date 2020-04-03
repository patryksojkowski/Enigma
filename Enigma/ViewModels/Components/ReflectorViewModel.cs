using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Enigma.Models.Components;

namespace Enigma.ViewModels.Components
{
    public class ReflectorViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ComponentFactory _componentFactory;

        public ReflectorViewModel(IEventAggregator eventAggregator, ComponentFactory componentFactory)
        {
            _eventAggregator = eventAggregator;
            _componentFactory = componentFactory;
        }

        public void ChangeReflector()
        {
            _eventAggregator.PublishOnUIThread(_componentFactory.Create<Reflector>());
        }
    }
}
