namespace EnigmaLibrary.Models.Classic.Components
{
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Signal : ISignal
    {
        public Signal(char input, bool step)
        {
            Letter = input;
            Step = step;
        }
        public char Letter { get; set; }
        public bool Step { get; set; }
    }
}
