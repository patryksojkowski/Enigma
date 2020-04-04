namespace EnigmaUI.ViewModels.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class PlugboardViewModel
    {
        private readonly IEnigmaEventAggregator _enigmaAggregator;
        private readonly IComponentFactory _componentFactory;

        public PlugboardViewModel(IEnigmaEventAggregator enigmaAggregator, IComponentFactory componentFactory)
        {
            _enigmaAggregator = enigmaAggregator;
            _componentFactory = componentFactory;
        }

        public void ChangePlugboard()
        {
            _enigmaAggregator.Publish(_componentFactory.CreatePlugboard(null));
        }
    }
}
