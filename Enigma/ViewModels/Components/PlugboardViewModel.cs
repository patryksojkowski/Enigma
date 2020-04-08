namespace EnigmaUI.ViewModels.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class PlugboardViewModel
    {
        private readonly IEventAggregator _enigmaAggregator;
        private readonly IComponentFactory _componentFactory;

        public PlugboardViewModel(IEventAggregator enigmaAggregator, IComponentFactory componentFactory)
        {
            _enigmaAggregator = enigmaAggregator;
            _componentFactory = componentFactory;
        }

        public void ChangePlugboard()
        {
            _enigmaAggregator.PublishOnUIThread(_componentFactory.CreatePlugboard(null));
        }
    }
}
