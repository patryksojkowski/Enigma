using System.Collections.Generic;
using Caliburn.Micro;
using EnigmaLibrary.Models.Enums;
using EnigmaLibrary.Models.Interfaces;
using EnigmaLibrary.Models.Interfaces.Components;

namespace EnigmaLibrary.Tests.Stubs
{
    public class EnigmaSettingsStub : IEnigmaSettings
    {
        private readonly IEnigmaEventAggregator _eventAggregator;
        private readonly IComponentFactory _componentFactory;

        public EnigmaSettingsStub(IEnigmaEventAggregator eventAggregator, IComponentFactory componentFactory)
        {
            _eventAggregator = eventAggregator;
            _componentFactory = componentFactory;
            ComponentList = new List<IEnigmaComponent>();
        }
        public List<IEnigmaComponent> ComponentList { get; set; }
    }

    public class ComponentFactoryStub : IComponentFactory
    {
        public IPlugboard CreatePlugboard()
        {
            return new PlugboardStub();
        }

        public IReflector CreateReflector()
        {
            return new ReflectorStub();
        }

        public IRotor CreateRotor(RotorSlot slot)
        {
            return new RotorStub(slot);
        }
    }

    public class RotorStub : IRotor
    {
        public RotorStub(RotorSlot slot)
        {
            Slot = slot;
        }

        public RotorSlot Slot { get; set; }

        public char Process(char input)
        {
            return input;
        }
    }

    public class ReflectorStub : IReflector
    {
        public char Process(char input)
        {
            return input;
        }
    }

    public class PlugboardStub : IPlugboard
    {
        public char Process(char input)
        {
            return input;
        }
    }
}
