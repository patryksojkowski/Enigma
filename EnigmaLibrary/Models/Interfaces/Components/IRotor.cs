namespace EnigmaLibrary.Models.Interfaces.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Enums;

    public interface IRotor : IEnigmaComponent
    {
        char[] Connections { get; }
        int PositionShift { get; }
        IEventAggregator RotorAggregator { get; }
        RotorSlot Slot { get; }
        RotorType Type { get; }

        bool Move(int steps);
    }
}