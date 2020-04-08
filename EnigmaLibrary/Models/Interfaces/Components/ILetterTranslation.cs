using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaLibrary.Models.Interfaces.Components
{
    public interface ILetterTranslation
    {
        char Input { get; }
        char Result { get; }
    }
}
