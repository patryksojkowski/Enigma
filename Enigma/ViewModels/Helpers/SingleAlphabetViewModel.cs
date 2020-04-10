namespace EnigmaUI.ViewModels.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaUI.Views.Helpers;
    using EnigmaUI.Extensions;
    using EnigmaUI.ViewModels.Interfaces;
    using EnigmaUI.Helpers;
    using EnigmaUI.Drawers;

    public class SingleAlphabetViewModel : AlphabetViewModelBase, IAlphabetViewModel, IViewAware, IHandle<ILetterTranslation>
    {
        public List<LetterViewModel> LetterViewModels { get; } = new List<LetterViewModel>();

        public override void Initialize()
        {
            base.Initialize();
            
            foreach (var letter in AlphabetHelper.GetAlphabet())
            {
                var letterViewModel = HelpersViewModelFactory.CreateLetter(letter);
                LetterViewModels.Add(letterViewModel);
            }

            ConnectionDrawer = new SingleConnectionDrawer();
        }

        public override void Handle(ILetterTranslation translation)
        {
            var grid = (GetView() as SingleAlphabetView).GetChildOfType<Grid>();

            var fromView = LetterViewModels.First(vm => vm.Letter == translation.Input).GetView() as LetterView;
            var toView = LetterViewModels.First(vm => vm.Letter == translation.Result).GetView() as LetterView;

            ConnectionDrawer.Draw(grid, fromView, toView);
        }
    }
}
