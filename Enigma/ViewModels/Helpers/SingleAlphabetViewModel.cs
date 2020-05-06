namespace EnigmaUI.ViewModels.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;
    using Caliburn.Micro;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaUI.Drawers;
    using EnigmaUI.Extensions;
    using EnigmaUI.ViewModels.Interfaces;
    using EnigmaUI.Views.Helpers;

    public class SingleAlphabetViewModel : AlphabetViewModelBase, IAlphabetViewModel, IHandle<LetterTranslation>
    {
        private Grid _grid;

        public Grid Grid
        {
            get
            {
                return _grid ?? (_grid = (GetView() as SingleAlphabetView).GetChildOfType<Grid>());
            }
        }

        public List<LetterViewModel> LetterViewModels { get; } = new List<LetterViewModel>();

        public override void Handle(LetterTranslation translation)
        {
            var fromView = LetterViewModels.First(vm => vm.Letter == translation.Input).GetView() as LetterView;
            var toView = LetterViewModels.First(vm => vm.Letter == translation.Result).GetView() as LetterView;

            ConnectionDrawer.Draw(fromView, toView, translation.Direction);
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (var letter in CommonHelper.GetAlphabet())
            {
                var letterViewModel = HelpersViewModelFactory.CreateLetter(letter);
                LetterViewModels.Add(letterViewModel);
            }
        }

        protected override void OnViewReady(object view)
        {
            ConnectionDrawer = new SingleConnectionDrawer(Grid);
        }
    }
}