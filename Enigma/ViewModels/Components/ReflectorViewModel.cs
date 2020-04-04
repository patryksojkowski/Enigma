namespace EnigmaUI.ViewModels.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class ReflectorViewModel
    {
        private readonly IEnigmaEventAggregator _enigmaAggregator;
        private readonly IComponentFactory _componentFactory;

        public ReflectorViewModel(IEnigmaEventAggregator enigmaAggregator, IComponentFactory componentFactory)
        {
            _enigmaAggregator = enigmaAggregator;
            _componentFactory = componentFactory;
        }

        public void ChangeReflector()
        {
            _enigmaAggregator.Publish(_componentFactory.CreateReflector(ReflectorType.B));
        }
    }
}
