using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Models.Components
{
    public class Rotor : IEnigmaComponent
    {
        char _letter;

        public RotorSlot Slot { get; set; }
        public Rotor(char letter, RotorSlot slot)
        {
            _letter = letter;
            Slot = slot;
        }

        public string Process(string input)
        {
            return input.Replace('a', _letter);
        }

        public char Process(char input)
        {
            throw new NotImplementedException();
        }
    }

    public enum RotorSlot
    {
        One = 1,
        Two,
        Three
    }
}
