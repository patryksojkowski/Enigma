namespace EnigmaUI.ViewModels.Helpers
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaUI.Views.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;
    using EnigmaUI.Extensions;
    using EnigmaUI.ViewModels.Interfaces;
    using EnigmaUI.Helpers;
    using EnigmaUI.Drawers;

    public class SingleAlphabetViewModel : IViewAware, IHandle<ILetterTranslation>, IAlphabetViewModel
    {
        private IEventAggregator _eventAggregator;
        private object _view;

        public event EventHandler<ViewAttachedEventArgs> ViewAttached;

        public SingleAlphabetViewModel(HelpersViewModelFactory letterViewModelFactory, IEventAggregator eventAggregator)
        {
            foreach (var letter in AlphabetHelper.GetAlphabet())
            {
                var letterViewModel = letterViewModelFactory.CreateLetter(letter);
                LetterViewModels.Add(letterViewModel);
            }

            EventAggregator = eventAggregator;
            ConnectionDrawer = new SingleConnectionDrawer();
        }
        public List<LetterViewModel> LetterViewModels { get; } = new List<LetterViewModel>();

        public IConnectionDrawer ConnectionDrawer { get; set; }

        public IEventAggregator EventAggregator
        {
            get {
                return _eventAggregator;
           }
            set {
                _eventAggregator = value;
                _eventAggregator.Subscribe(this);
            }
        }

        public void AttachView(object view, object context = null)
        {
            _view = view;
        }

        public object GetView(object context = null)
        {
            return _view;
        }

        public void Handle(ILetterTranslation translation)
        {
            var grid = (GetView() as SingleAlphabetView).GetChildOfType<Grid>();
            var fromView = LetterViewModels.First(vm => vm.Letter == translation.Input).GetView() as LetterView;
            var toView = LetterViewModels.First(vm => vm.Letter == translation.Result).GetView() as LetterView;
            ConnectionDrawer.Draw(grid, fromView, toView);
        }
    }
}
