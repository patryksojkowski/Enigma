namespace EnigmaUI.ViewModels.Helpers
{
    using System;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaUI.Drawers;
    using EnigmaUI.ViewModels.Interfaces;

    public abstract class AlphabetViewModelBase : ViewAware, IAlphabetViewModel, IHandle<LetterTranslation>
    {
        private IEventAggregator _eventAggregator;

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