namespace EnigmaUI.ViewModels.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaLibrary.Models.Enums;

    public class RotorViewModel
    {
        private readonly IEnigmaEventAggregator _enigmaAggregator;
        private readonly IComponentFactory _componentFactory;
        public RotorSlot _slot;
        public int Positon { get; set; }

        public RotorViewModel(IEnigmaEventAggregator enigmaAggregator, IComponentFactory componentFactory, RotorSlot slot)
        {
            _enigmaAggregator = enigmaAggregator;
            _componentFactory = componentFactory;
            _slot = slot;
        }

        public RotorType RotorType { get; set; }

        public void ChangeRotor()
        {
            _enigmaAggregator.Publish(_componentFactory.CreateRotor(RotorType, _slot, Positon));
        }
    }
}
