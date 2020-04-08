namespace EnigmaUI.ViewModels.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaLibrary.Models.Enums;

    public class RotorViewModelFactory
    {
        private readonly IEventAggregator _enigmaAggregator;
        private readonly IComponentFactory _componentFactory;

        public RotorViewModelFactory(IEventAggregator enigmaAggregator, IComponentFactory componentFactory)
        {
            _enigmaAggregator = enigmaAggregator;
            _componentFactory = componentFactory;
        }

        public RotorViewModel Create(RotorSlot Slot)
        {
            return new RotorViewModel(_enigmaAggregator, _componentFactory, Slot);
        }
    }
}
