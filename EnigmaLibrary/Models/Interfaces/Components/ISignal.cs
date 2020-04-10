using EnigmaLibrary.Models.Enums;

namespace EnigmaLibrary.Models.Interfaces.Components
{
    public interface ISignal
    {
        /// <summary>
        /// Value between 0 and 25 correspodning to position in alphabet
        /// </summary>
        int Value { get; set; }
        bool Step { get; set; }
        SignalDirection Direction { get; set; }
    }
}
