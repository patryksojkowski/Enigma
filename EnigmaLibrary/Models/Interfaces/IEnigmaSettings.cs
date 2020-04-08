namespace EnigmaLibrary.Models.Interfaces
{
    using System.Collections.Generic;
    using EnigmaLibrary.Models.Interfaces.Components;

    public interface IEnigmaSettings
    {
        List<IEnigmaComponent> ComponentList { get; set; }
        IComponentFactory ComponentFactory { get; set; }

        IRotor Rotor1 { get; set; }
        IRotor Rotor2 { get; set; }
        IRotor Rotor3 { get; set; }
        IReflector Reflector { get; set; }
        IPlugboard Plugboard { get; set; }
    }
}
