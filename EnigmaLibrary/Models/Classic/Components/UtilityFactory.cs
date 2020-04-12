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

        public Signal CreateSignal(int value, bool step, SignalDirection direction)
        {
            return new Signal(value, step, direction);
        }

        public LetterTranslation CreateTranslation(char from, char to, SignalDirection direction)
        {
            return new LetterTranslation(from, to, direction);
        }
    }
}