namespace EnigmaLibrary.Tests.Stubs
{
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class EnigmaSettingsStub : IEnigmaSettings
    {
        private readonly IEventAggregator _eventAggregator;

        public EnigmaSettingsStub(IEventAggregator eventAggregator, IComponentFactory componentFactory)
        {
            _eventAggregator = eventAggregator;
            ComponentFactory = componentFactory;
            ComponentList = new List<IEnigmaComponent>();
        }
        public List<IEnigmaComponent> ComponentList { get; set; }
        public IComponentFactory ComponentFactory { get; set; }
    }

    public class ComponentFactoryStub : IComponentFactory
    {
        public Func<char, bool, ISignal> SignalFactory => (c, b) => new Signal(c, b);

        public IPlugboard CreatePlugboard()
        {
            return new PlugboardStub(null, SignalFactory);
        }

        public IPlugboard CreatePlugboard(Dictionary<char, char> connections)
        {
            return new PlugboardStub(connections, SignalFactory);
        }

        public IReflector CreateReflector(ReflectorType type)
        {
            return new ReflectorStub(type, SignalFactory);
        }

        public IRotor CreateRotor(RotorType type, RotorSlot slot, int position)
        {
            return new RotorStub(slot, position, SignalFactory, type);
        }
    }

    public class RotorStub : IRotor
    {
        private readonly Func<char, bool, ISignal> _signalFactory;

        public RotorStub(RotorSlot slot, int position, Func<char, bool, ISignal> signalFactory, RotorType type)
        {
            Slot = slot;
            Position = position;
            _signalFactory = signalFactory;
            Type = type;
        }

        public RotorSlot Slot { get; set; }
        public int Position { get; set; }
        public RotorType Type { get; set; }

        public void Move(int steps)
        {
            Position += steps;
            Position %= 26;
            Position += 26;
            Position %= 26;
        }

        public ISignal Process(ISignal input)
        {
            return input;
        }
    }

    public class ReflectorStub : IReflector
    {
        private readonly Func<char, bool, ISignal> _signalFactory;

        public ReflectorStub(ReflectorType type, Func<char, bool, ISignal> signalFactory)
        {
            Type = type;
            _signalFactory = signalFactory;
        }

        public ReflectorType Type { get; set; }

        public ISignal Process(ISignal input)
        {
            return input;
        }
    }

    public class PlugboardStub : IPlugboard
    {
        private readonly Func<char, bool, ISignal> _signalFactory;

        public PlugboardStub(Dictionary<char,char> connections, Func<char, bool, ISignal> signalFactory)
        {
            Connections = connections;
            _signalFactory = signalFactory;
        }

        public Dictionary<char, char> Connections { get; set; }

        public void AddConnection(char from, char to)
        {
            Connections.Add(from, to);
            Connections.Add(to, from);
        }

        public ISignal Process(ISignal input)
        {
            return input;
        }
    }
}
