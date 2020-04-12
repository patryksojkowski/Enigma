namespace EnigmaLibrary.Models.Classic
{
    using System.Collections.Generic;
    using EnigmaLibrary.Models.Enums;

    public partial class EnigmaSettings
    {
        public class SavedSettings
        {
            public Dictionary<char, char> PlugboardConnections { get; set; }
            public ReflectorType ReflectorType { get; set; }
            public Slot Slot1 { get; set; }
            public Slot Slot2 { get; set; }
            public Slot Slot3 { get; set; }

            public struct Slot
            {
                public int Position { get; set; }
                public RotorType RotorType { get; set; }
            }
        }
    }
}