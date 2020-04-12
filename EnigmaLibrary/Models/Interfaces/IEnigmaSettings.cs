namespace EnigmaLibrary.Models.Interfaces
{
    using System.Collections.Generic;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public interface IEnigmaSettings
    {
        IComponentFactory ComponentFactory { get; }
        List<IEnigmaComponent> ComponentList { get; }
        IPlugboard Plugboard { get; }
        IReflector Reflector { get; }
        IRotor Rotor1 { get; }
        IRotor Rotor2 { get; }
        IRotor Rotor3 { get; }

        IRotor GetRotor(RotorSlot slot);
    }
}