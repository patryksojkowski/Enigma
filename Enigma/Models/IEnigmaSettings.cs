using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Models.Components;

namespace Enigma.Models
{
    public interface IEnigmaSettings
    {
        Rotor Rotor1 { get; set; }
        Rotor Rotor2 { get; set; }
        Rotor Rotor3 { get; set; }
        Reflector Reflector { get; set; }
        Plugboard Plugboard { get; set; }
    }
}
