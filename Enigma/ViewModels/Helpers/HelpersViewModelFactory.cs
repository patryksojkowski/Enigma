namespace EnigmaUI.ViewModels.Helpers
{
    using Caliburn.Micro;

    public class HelpersViewModelFactory
    {
        public LetterViewModel CreateLetter(char letter)
        {
            return new LetterViewModel(letter);
        }
        public SingleAlphabetViewModel CreateAlphabetViewModel(IEventAggregator eventAggregator)
        {
            return new SingleAlphabetViewModel(this, eventAggregator);
        }
    }
}
