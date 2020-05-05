namespace EnigmaUI.ViewModels.Helpers
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using Caliburn.Micro;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaLibrary.Models.Enums;
    using EnigmaUI.Drawers;
    using EnigmaUI.Extensions;
    using EnigmaUI.ViewModels.Interfaces;
    using EnigmaUI.Views.Helpers;

    public class DoubleAlphabetViewModel : AlphabetViewModelBase, IAlphabetViewModel, IViewAware,
        IHandle<LetterTranslation>, IHandle<RotorStepMessage>
    {
        public ObservableCollection<LetterViewModel> InnerLetterViews { get; } = new ObservableCollection<LetterViewModel>();
        public ObservableCollection<LetterViewModel> OuterLetterViews { get; } = new ObservableCollection<LetterViewModel>();

        public override void Handle(LetterTranslation translation)
        {
            var grid = (GetView() as DoubleAlphabetView).GetChildOfType<Grid>();
            grid.UpdateLayout();

            LetterView fromView, toView;

            if (translation.Direction == SignalDirection.In)
            {
                fromView = InnerLetterViews.First(vm => vm.Letter == translation.Input).GetView() as LetterView;
                toView = OuterLetterViews.First(vm => vm.Letter == translation.Result).GetView() as LetterView;
            }
            else
            {
                fromView = OuterLetterViews.First(vm => vm.Letter == translation.Input).GetView() as LetterView;
                toView = InnerLetterViews.First(vm => vm.Letter == translation.Result).GetView() as LetterView;
            }

            ConnectionDrawer.Draw(grid, fromView, toView, translation.Direction);
        }

        public void Handle(RotorStepMessage message)
        {
            MoveAlphabetBy(message.Steps);
        }

        public override void Initialize()
        {
            base.Initialize();
            foreach (var letter in CommonHelper.GetShiftedAlphabet(PositionShift))
            {
                InnerLetterViews.Add(HelpersViewModelFactory.CreateLetter(letter));
                OuterLetterViews.Add(HelpersViewModelFactory.CreateLetter(letter));
            }

            ConnectionDrawer = new DoubleConnectionDrawer();
        }

        private void MoveAlphabetBy(int steps)
        {
            if (steps > 0)
            {
                for (var i = 0; i < steps; i++)
                {
                    var firstLetterInner = InnerLetterViews.First();
                    var firstLetterOuter = OuterLetterViews.First();
                    InnerLetterViews.Remove(firstLetterInner);
                    OuterLetterViews.Remove(firstLetterOuter);
                    InnerLetterViews.Add(firstLetterInner);
                    OuterLetterViews.Add(firstLetterOuter);
                }
            }
            else
            {
                for (var i = 0; i > steps; i--)
                {
                    var lastLetterInner = InnerLetterViews.Last();
                    var lastLetterOuter = OuterLetterViews.Last();
                    InnerLetterViews.Remove(lastLetterInner);
                    OuterLetterViews.Remove(lastLetterOuter);
                    InnerLetterViews.Insert(0, lastLetterInner);
                    OuterLetterViews.Insert(0, lastLetterOuter);
                }
            }
        }
    }
}