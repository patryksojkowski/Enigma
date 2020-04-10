namespace EnigmaLibrary.Models.Classic.Components
{
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Signal : ISignal
    {
        public Signal(int value, bool step, SignalDirection direction)
        {
            Value = value;
            Step = step;
            Direction = direction;
        }
        public int Value { get; set; }
        public bool Step { get; set; }
        public SignalDirection Direction { get; set; }
    }
}
