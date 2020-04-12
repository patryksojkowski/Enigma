namespace EnigmaUI.ViewModels.Helpers
{
    using System;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaUI.Drawers;
    using EnigmaUI.ViewModels.Interfaces;

    public abstract class AlphabetViewModelBase : IAlphabetViewModel, IHandle<LetterTranslation>, IViewAware
    {
        private IEventAggregator _eventAggregator;
        private object _view;

        public AlphabetViewModelBase()
        {
        }

        public event EventHandler<ViewAttachedEventArgs> ViewAttached;

        public IConnectionDrawer ConnectionDrawer { get; set; }
        public char[] Connections { get; set; }

        public IEventAggregator EventAggregator
        {
            get
            {
                return _eventAggregator;
            }
            set
            {
                _eventAggregator = value;
                _eventAggregator.Subscribe(this);
            }
        }

        public HelpersViewModelFactory HelpersViewModelFactory { get; set; }
        public int PositionShift { get; set; }

        public void AttachView(object view, object context = null)
        {
            _view = view;
        }

        public object GetView(object context = null)
        {
            return _view;
        }

        public abstract void Handle(LetterTranslation message);

        public virtual void Initialize()
        {
            if (HelpersViewModelFactory is null || EventAggregator is null)
            {
                throw new InvalidOperationException("BaseAlphabetViewModel is not initialized properly");
            }
        }
    }
}