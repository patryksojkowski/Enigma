namespace EnigmaLibrary.Models.Interfaces.Components
{
    using EnigmaLibrary.Models.Enums;

    public interface IComponentFactory
    {
        IRotor CreateRotor(RotorSlot slot);
        IReflector CreateReflector();
        IPlugboard CreatePlugboard();
    }
}
