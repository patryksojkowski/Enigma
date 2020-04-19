namespace DragAndDrop
{
    using System.Windows;
    using System.Windows.Shapes;
    using Caliburn.Micro;
    using DragAndDrop.ViewModels;

    public class ConnectingViewModelFactory
    {
        private readonly IEventAggregator _eventAggregator;

        public ConnectingViewModelFactory(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public RectangleConnection CreateConnection(ConnectingRectangleViewModel source, ConnectingRectangleViewModel destination, Line line)
        {
            return new RectangleConnection(source, destination, line);
        }

        public ConnectingRectangleViewModel CreateRectangleViewModel(char letter, UIElement grid = null)
        {
            return new ConnectingRectangleViewModel(letter, _eventAggregator);
        }
    }
}