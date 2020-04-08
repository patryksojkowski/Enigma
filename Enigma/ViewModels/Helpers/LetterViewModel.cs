namespace EnigmaUI.ViewModels.Helpers
{
    using System;
    using Caliburn.Micro;

    public class LetterViewModel : IViewAware
    {
        private object _view;

        public event EventHandler<ViewAttachedEventArgs> ViewAttached;

        public LetterViewModel(char letter)
        {
            Letter = letter;
        }

        public char Letter { get; set; }

        public void AttachView(object view, object context = null)
        {
            _view = view;
        }

        public object GetView(object context = null)
        {
            return _view;
        }
    }
}
