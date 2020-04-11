namespace EnigmaLibrary.Models.Interfaces.Components
{
    using System;
    using System.Collections.Generic;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaLibrary.Models.Enums;

    public interface IComponentFactory
    {
        IRotor CreateRotor(RotorType type, RotorSlot slot, int position);
        IReflector CreateReflector(ReflectorType type);
        IPlugboard CreatePlugboard(Dictionary<char, char> connections);
        ILetterTranslation CreateTranslation(char from, char to, SignalDirection direction);
        ISignal CreateSignal(int value, bool step, SignalDirection direction);

        RotorStepMessage CreateRotorStepMessage(int step);
    }
}
