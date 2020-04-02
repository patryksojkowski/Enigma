using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Models
{
    interface IEnigmaComponent
    {
        string Process(string input);
    }
}
