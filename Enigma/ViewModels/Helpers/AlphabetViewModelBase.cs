namespace EnigmaUI.ViewModels.Helpers
{
    using System;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaUI.Drawers;
    using EnigmaUI.ViewModels.Interfaces;

    public abstract class AlphabetViewModelBase : IAlphabetViewModel, IHandle<ILetterTranslation>, IViewAware
    {
        private IEventAggregator _eventAggregator;
        private object _view;

        public event EventHandler<ViewAttachedEventArgs> ViewAttached;

        public AlphabetViewModelBase()
        {
        }

        public char[] Connections { get; set; }

        public IConnectionDrawer ConnectionDrawer { get; set; }
        public HelpersViewModelFactory HelpersViewModelFactory { get; set; }

        public IEventAggregator EventAggregator {
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

        public int PositionShift { get; set; }

        public void AttachView(object view, object context = null)
        {
            _view = view;
        }

        public object GetView(object context = null)
        {
            return _view;
        }

        public abstract void Handle(ILetterTranslation message);

        public virtual void Initialize()
        {
            if (HelpersViewModelFactory is null || EventAggregator is null)
            {
                throw new InvalidOperationException("BaseAlphabetViewModel is not initialized properly");
            }
        }
    }
}
