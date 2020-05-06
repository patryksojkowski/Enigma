namespace EnigmaUI.ViewModels.Helpers
{
    using Caliburn.Micro;

    public class LetterViewModel : ViewAware
    {
        public LetterViewModel(char letter)
        {
            Letter = letter;
        }

        public char Letter { get; set; }
    }
}