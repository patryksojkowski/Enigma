using System.Threading.Tasks;
using EnigmaLibrary.Models.Enums;
using EnigmaLibrary.Models.Interfaces.Components;

namespace EnigmaLibrary.Models.Classic.Components
{
    public class UtilityFactory : IUtilityFactory
    {
        public UtilityFactory()
        {
        }

        public RotorStepMessage CreateRotorStepMessage(int step)
        {
            return new RotorStepMessage(step);
        }

        public LetterTranslation CreateTranslation(char from, char to, SignalDirection direction)
        {
            return new LetterTranslation(from, to, direction);
        }

        Task<Signal> IUtilityFactory.CreateSignal(int value, bool step, SignalDirection direction)
        {
            return Task.Run(() => new Signal(value, step, direction));
        }
    }
}