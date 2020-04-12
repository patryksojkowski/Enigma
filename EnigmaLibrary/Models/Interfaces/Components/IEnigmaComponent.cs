using EnigmaLibrary.Models.Classic.Components;

namespace EnigmaLibrary.Models.Interfaces.Components
{
    public interface IEnigmaComponent
    {
        Signal Process(Signal input);
    }
}