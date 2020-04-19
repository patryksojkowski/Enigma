namespace DragAndDrop.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Shapes;
    using Caliburn.Micro;
    using DragAndDrop.Messages;
    using DragAndDrop.Views;

    public class PlugboardViewModel : Conductor<object>, IViewAware, IHandle<RectangleClickMessage>
    {
        private readonly List<RectangleConnection> _connections = new List<RectangleConnection>();
        private readonly ConnectingViewModelFactory _factory;
        private ConnectingRectangleViewModel _destinationRectangle;
        private Grid _grid;
        private bool _isConnecting;
        private Line _line;
        private ConnectingRectangleViewModel _sourceRectangle;
        private PlugboardView _view;

        public PlugboardViewModel(ConnectingAlphabetViewModel sourceAlphabet, ConnectingAlphabetViewModel destinationAlphabet, ConnectingViewModelFactory factory, IEventAggregator viewEventAggregator)
        {
            SourceAlphabet = sourceAlphabet;
            DestinationAlphabet = destinationAlphabet;
            _factory = factory;
            viewEventAggregator.Subscribe(this);
        }

        public ConnectingAlphabetViewModel DestinationAlphabet { get; }

        public Grid Grid
        {
            get
            {
                return _grid ?? (_grid = View.GetChildOfType<Grid>() as Grid);
            }
        }

        public ConnectingAlphabetViewModel SourceAlphabet { get; }

        public PlugboardView View
        {
            get
            {
                return _view ?? (_view = GetView() as PlugboardView);
            }
        }

        public void Handle(RectangleClickMessage message)
        {
            var rectangle = message.Rectangle;

            if (!_isConnecting)
            {
                _isConnecting = true;
                _sourceRectangle = rectangle;

                if (_sourceRectangle.IsConnected)
                {
                    RemoveExistingConnection(_sourceRectangle);
                }

                StartDrawingLine(_sourceRectangle);
            }
            else
            {
                _isConnecting = false;
                _destinationRectangle = rectangle;

                if (ConnectionNotPossible(rectangle))
                {
                    CancelDrawingLine();
                    return;
                }

                if (_destinationRectangle.IsConnected)
                {
                    RemoveExistingConnection(_destinationRectangle);
                }

                FinishDrawingLine(_destinationRectangle);

                AddNewConnection(_sourceRectangle, _destinationRectangle, _line);
            }
        }

        private void AddLineToGrid(Line line)
        {
            Grid.SetRowSpan(line, 2);
            Grid.Children.Add(line);
        }

        private void AddNewConnection(ConnectingRectangleViewModel source, ConnectingRectangleViewModel destination, Line line)
        {
            var connection = _factory.CreateConnection(source, destination, line);
            _connections.Add(connection);
        }

        private void CancelDrawingLine()
        {
            Grid.Children.Remove(_line);

            View.MouseMove -= View_MouseMove;
        }

        /// <summary>
        /// Checks if destination rectangle is from different collection than source rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        private bool ConnectionNotPossible(ConnectingRectangleViewModel rectangle)
        {
            return SourceAlphabet.ConnectingRectangles.Contains(_sourceRectangle) && SourceAlphabet.ConnectingRectangles.Contains(rectangle) ||
                    DestinationAlphabet.ConnectingRectangles.Contains(_sourceRectangle) && DestinationAlphabet.ConnectingRectangles.Contains(rectangle);
        }

        private void FinishDrawingLine(ConnectingRectangleViewModel rectangle)
        {
            var endPosition = GetAnchorPoint(rectangle);
            _line.SetEndPoint(endPosition);

            View.MouseMove -= View_MouseMove;
        }

        private Point GetAnchorPoint(ConnectingRectangleViewModel rectangle)
        {
            var view = rectangle.View;
            var width = view.Width;
            var height = view.Height;
            var middlePoint = new Point(width / 2, height / 2);

            return view.TranslatePoint(middlePoint, Grid);
        }

        private void RemoveExistingConnection(ConnectingRectangleViewModel rectangle)
        {
            var connection = _connections.Single(con => con.SourceRectangle == rectangle || con.DestinationRectangle == rectangle);
            connection.Clear();

            var line = connection.ConnectingLine;
            Grid.Children.Remove(line);
            _connections.Remove(connection);
        }

        private void StartDrawingLine(ConnectingRectangleViewModel rectangle)
        {
            var startPosition = GetAnchorPoint(rectangle);
            _line = DrawerHelper.GetLine(startPosition, startPosition, isHitTestVisible: false);

            AddLineToGrid(_line);

            View.MouseMove += View_MouseMove;
        }

        private void View_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePosition = e.GetPosition(null);
            _line.X2 = mousePosition.X;
            _line.Y2 = mousePosition.Y;
        }
    }
}