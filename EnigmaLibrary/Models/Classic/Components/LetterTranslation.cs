namespace EnigmaLibrary.Models.Classic.Components
{
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class LetterTranslation : ILetterTranslation
    {
        public LetterTranslation(char input, char result, SignalDirection direction)
        {
            Input = input;
            Result = result;
            Direction = direction;
        }

        public char Input { get; }
        public char Result { get; }
        public SignalDirection Direction { get; }
    }
}
