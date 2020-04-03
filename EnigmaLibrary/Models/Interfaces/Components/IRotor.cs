namespace EnigmaLibrary.Models.Interfaces.Components
{
    using EnigmaLibrary.Models.Enums;

    public interface IRotor : IEnigmaComponent
    {
        RotorSlot Slot { get; set; }
    }
}
