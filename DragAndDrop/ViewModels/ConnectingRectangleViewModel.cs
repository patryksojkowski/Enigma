namespace DragAndDrop.ViewModels
{
    using System;
    using System.Windows;
    using Caliburn.Micro;
    using DragAndDrop.Messages;
    using DragAndDrop.Views;

    public class ConnectingRectangleViewModel : ViewAware
    {
        private readonly IEventAggregator _eventAggregator;
        public ConnectingRectangleView _view;

        public ConnectingRectangleViewModel(char letter, IEventAggregator eventAggregator)
        {
            Letter = letter;
            _eventAggregator = eventAggregator;
        }


        public bool IsConnected { get; set; }
        public char Letter { get; private set; }

        public ConnectingRectangleView View
        {
            get
            {
                return _view ?? (_view = GetView() as ConnectingRectangleView);
            }
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