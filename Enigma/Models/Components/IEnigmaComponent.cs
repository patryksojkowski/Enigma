using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Models.Components
{
    public interface IEnigmaComponent
    {
        char Process(char input);
    }
}
