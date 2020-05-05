using System.Threading.Tasks;
using EnigmaLibrary.Models.Classic.Components;

namespace EnigmaLibrary.Models.Interfaces.Components
{
    public interface IEnigmaComponent
    {
        Task<Signal> Process(Signal input);
    }
}