namespace EnigmaLibrary.Models.Classic.Components
{
    using EnigmaLibrary.Models.Interfaces.Components;

    public class LetterTranslation : ILetterTranslation
    {
        public LetterTranslation(char input, char result)
        {
            Input = input;
            Result = result;
        }

        public char Input { get; }
        public char Result { get; }
    }
}
