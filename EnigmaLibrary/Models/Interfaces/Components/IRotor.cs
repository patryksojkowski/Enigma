namespace EnigmaLibrary.Models.Interfaces.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Enums;

    public interface IRotor : IEnigmaComponent
    {
        RotorSlot Slot { get; set; }
        int PositionShift { get; }
        RotorType Type { get; set; }
        bool Move(int steps);
        IEventAggregator RotorAggregator { get; }
        char [] Connections { get; }

    }
}
