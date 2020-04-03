using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Models.Enums;

namespace Enigma.Models.Components
{
    public class ComponentFactory
    {
        public T Create<T>() where T : IEnigmaComponent, new()
        {
            return new T();
        }
        
        public Rotor Create<T>(RotorSlot slot) where T : Rotor
        {
            return new Rotor(slot);
        }
    }
}
