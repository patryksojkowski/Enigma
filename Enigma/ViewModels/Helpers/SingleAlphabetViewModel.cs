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

    public class SingleAlphabetViewModel : AlphabetViewModelBase, IAlphabetViewModel, IViewAware, IHandle<LetterTranslation>
    {
        public List<LetterViewModel> LetterViewModels { get; } = new List<LetterViewModel>();

        public override void Handle(LetterTranslation translation)
        {
            var grid = (GetView() as SingleAlphabetView).GetChildOfType<Grid>();

            var fromView = LetterViewModels.First(vm => vm.Letter == translation.Input).GetView() as LetterView;
            var toView = LetterViewModels.First(vm => vm.Letter == translation.Result).GetView() as LetterView;

            ConnectionDrawer.Draw(grid, fromView, toView, translation.Direction);
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (var letter in CommonHelper.GetAlphabet())
            {
                var letterViewModel = HelpersViewModelFactory.CreateLetter(letter);
                LetterViewModels.Add(letterViewModel);
            }

            ConnectionDrawer = new SingleConnectionDrawer();
        }
    }
}