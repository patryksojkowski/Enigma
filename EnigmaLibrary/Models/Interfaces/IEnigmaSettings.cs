namespace EnigmaLibrary.Models.Interfaces
{
    using System.Collections.Generic;
    using EnigmaLibrary.Models.Interfaces.Components;

    public interface IEnigmaSettings
    {
        List<IEnigmaComponent> ComponentList { get; set; }
    }
}
