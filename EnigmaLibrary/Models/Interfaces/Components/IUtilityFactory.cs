namespace EnigmaLibrary.Models.Interfaces.Components
{
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaLibrary.Models.Enums;

    public interface IUtilityFactory
    {
        RotorStepMessage CreateRotorStepMessage(int step);

        Signal CreateSignal(int value, bool step, SignalDirection direction);

        LetterTranslation CreateTranslation(char from, char to, SignalDirection direction);
    }
}