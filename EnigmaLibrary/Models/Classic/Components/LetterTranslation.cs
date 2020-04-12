namespace EnigmaLibrary.Models.Classic.Components
{
    using EnigmaLibrary.Models.Enums;

    public class LetterTranslation
    {
        public LetterTranslation(char input, char result, SignalDirection direction)
        {
            Input = input;
            Result = result;
            Direction = direction;
        }

        public SignalDirection Direction { get; }
        public char Input { get; }
        public char Result { get; }
    }
}