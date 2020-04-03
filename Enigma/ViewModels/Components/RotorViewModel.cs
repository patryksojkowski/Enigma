﻿namespace EnigmaUI.ViewModels.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaLibrary.Models.Enums;

    public class RotorViewModel
    {
        private readonly IEnigmaEventAggregator _enigmaAggregator;
        private readonly IComponentFactory _componentFactory;
        public RotorSlot _slot;

        public RotorViewModel(IEnigmaEventAggregator enigmaAggregator, IComponentFactory componentFactory, RotorSlot slot)
        {
            _enigmaAggregator = enigmaAggregator;
            _componentFactory = componentFactory;
            _slot = slot;
        }


        public void ChangeRotor()
        {
            _enigmaAggregator.Publish(_componentFactory.CreateRotor(_slot));
        }
    }
}
