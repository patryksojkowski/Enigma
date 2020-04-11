namespace EnigmaUI.ViewModels.Helpers
{
    using Caliburn.Micro;
    using EnigmaUI.ViewModels.Interfaces;
    using System;

    public class HelpersViewModelFactory
    {
        public LetterViewModel CreateLetter(char letter)
        {
            return new LetterViewModel(letter);
        }

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
    }
}
