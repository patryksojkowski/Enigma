namespace EnigmaLibrary.Models.Interfaces.Components
{
    using System.Collections.Generic;
    using EnigmaLibrary.Models.Enums;

    public interface IComponentFactory
    {
        IPlugboard CreatePlugboard(Dictionary<char, char> connections = null);

        IReflector CreateReflector(ReflectorType type);

        IRotor CreateRotor(RotorType type, RotorSlot slot, int position);
    }
}