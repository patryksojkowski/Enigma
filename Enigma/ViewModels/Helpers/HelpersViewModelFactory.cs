namespace EnigmaUI.ViewModels.Helpers
{
    using Caliburn.Micro;
    using EnigmaUI.ViewModels.Interfaces;

    public class HelpersViewModelFactory
    {
        public IAlphabetViewModel CreateAlphabetViewModel<T>(IEventAggregator eventAggregator, int positionShift, char[] connections = null) where T : AlphabetViewModelBase, new()
        {
            var viewModel = new T()
            {
                EventAggregator = eventAggregator,
                HelpersViewModelFactory = this,
                Connections = connections,
                PositionShift = positionShift,
            };
            viewModel.Initialize();
            return viewModel;
        }

        public LetterViewModel CreateLetter(char letter)
        {
            return new LetterViewModel(letter);
        }
    }
}