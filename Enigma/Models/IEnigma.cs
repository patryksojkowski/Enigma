using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Models
{
    public interface IEnigma
    {
        string Encrypt(string input);
    }
}
