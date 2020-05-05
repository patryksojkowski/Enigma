using System.Threading.Tasks;

namespace EnigmaLibrary.Models.Interfaces
{
    public interface IEnigma
    {
        Task<char> Encrypt(char input);
    }
}