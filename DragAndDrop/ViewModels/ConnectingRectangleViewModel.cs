namespace DragAndDrop.ViewModels
{
    using System;
    using System.Windows;
    using Caliburn.Micro;
    using DragAndDrop.Messages;
    using DragAndDrop.Views;

    public class ConnectingRectangleViewModel : IViewAware
    {
        private readonly IEventAggregator _eventAggregator;
        private object _view;

        public ConnectingRectangleViewModel(char letter, IEventAggregator eventAggregator)
        {
            Letter = letter;
            _eventAggregator = eventAggregator;
        }

        public event EventHandler<ViewAttachedEventArgs> ViewAttached;

        public bool IsConnected { get; set; }
        public char Letter { get; private set; }

        public ConnectingRectangleView View
        {
            get
            {
                return _view as ConnectingRectangleView;
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

        public void Rectangle_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var message = new RectangleClickMessage()
            {
                Rectangle = this,
            };
            _eventAggregator.PublishOnUIThread(message);
        }
    }
}