namespace EnigmaUI.ViewModels.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaUI.ViewModels.Helpers;

    public class RotorViewModelFactory
    {
        private readonly IEventAggregator _enigmaAggregator;
        private readonly IComponentFactory _componentFactory;
        private readonly IEnigmaSettings _enigmaSettings;
        private readonly HelpersViewModelFactory _helpersViewModelFactory;

        public RotorViewModelFactory(IEventAggregator enigmaAggregator, IComponentFactory componentFactory, IEnigmaSettings enigmaSettings, HelpersViewModelFactory helpersViewModelFactory)
        {
            _enigmaAggregator = enigmaAggregator;
            _componentFactory = componentFactory;
            _enigmaSettings = enigmaSettings;
            _helpersViewModelFactory = helpersViewModelFactory;
        }

        public RotorViewModel Create(RotorSlot Slot)
        {
            return new RotorViewModel(_enigmaAggregator, _componentFactory, Slot, _enigmaSettings, _helpersViewModelFactory);
        }
    }
}
