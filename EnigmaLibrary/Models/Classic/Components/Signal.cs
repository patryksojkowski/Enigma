namespace EnigmaLibrary.Models.Classic.Components
{
    using EnigmaLibrary.Models.Enums;

    public class Signal
    {
        public Signal(int value, bool step, SignalDirection direction)
        {
            Value = value;
            Step = step;
            Direction = direction;
        }

        public SignalDirection Direction { get; }
        public bool Step { get; }
        public int Value { get; }
    }
}