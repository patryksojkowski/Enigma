namespace EnigmaLibrary.Models.Interfaces.Components
{
    using System;
    using System.Collections.Generic;
    using EnigmaLibrary.Models.Enums;

    public interface IComponentFactory
    {
        IRotor CreateRotor(RotorType type, RotorSlot slot, int position);
        IReflector CreateReflector(ReflectorType type);
        IPlugboard CreatePlugboard(Dictionary<char, char> connections);
        Func<char, bool, ISignal> SignalFactory { get; }
    }
}
