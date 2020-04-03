namespace EnigmaLibrary.Models.Classic.Components
{
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Rotor : IRotor
    {
        public Rotor(RotorSlot slot)
        {
            Slot = slot;
        }

        public RotorSlot Slot { get; set; }

        public char Process(char input)
        {
            return 'a';
        }
    }
}
