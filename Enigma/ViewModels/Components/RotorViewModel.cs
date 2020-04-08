namespace EnigmaUI.ViewModels.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaLibrary.Models.Enums;

    public class RotorViewModel
    {
        private readonly IEventAggregator _enigmaAggregator;
        private readonly IComponentFactory _componentFactory;
        private RotorSlot _slot;
        private int _position;

        public RotorViewModel(IEventAggregator enigmaAggregator, IComponentFactory componentFactory, RotorSlot slot)
        {
            _enigmaAggregator = enigmaAggregator;
            _componentFactory = componentFactory;
            _slot = slot;
        }

        public RotorType RotorType { get; set; }

        public void ChangeRotor()
        {
            _enigmaAggregator.PublishOnUIThread(_componentFactory.CreateRotor(RotorType, _slot, _position));
        }
    }
}
