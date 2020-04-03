using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Models.Enums;

namespace Enigma.Models.Components
{
    public class Rotor : IEnigmaComponent
    {
        public RotorSlot Slot { get; set; }
        public Rotor(RotorSlot slot)
        {
            Slot = slot;
        }

        public string Process(string input)
        {
            return input.Replace('a', 'z');
        }

        public char Process(char input)
        {
            throw new NotImplementedException();
        }
    }
}
