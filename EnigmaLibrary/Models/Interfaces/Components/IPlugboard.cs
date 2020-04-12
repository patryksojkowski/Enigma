namespace EnigmaLibrary.Models.Interfaces.Components
{
    using System.Collections.Generic;

    public interface IPlugboard : IEnigmaComponent
    {
        Dictionary<char, char> Connections { get; }

        void AddConnection(char from, char to);
    }
}