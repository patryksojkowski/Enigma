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
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class PlugboardViewModel : Conductor<object>, IViewAware, IHandle<RectangleClickMessage>
    {
        private readonly List<RectangleConnection> _connections = new List<RectangleConnection>();
        private readonly ConnectingViewModelFactory _factory;
        private readonly IEnigmaSettings _enigmaSettings;
        private ConnectingRectangleViewModel _destinationRectangle;
        private Grid _grid;
        private bool _isConnecting;
        private Line _line;
        private ConnectingRectangleViewModel _sourceRectangle;
        private PlugboardView _view;
        private PlugboardController _plugboardController;

        public PlugboardViewModel(ConnectingAlphabetViewModel topAlphabet, ConnectingAlphabetViewModel bottomAlphabet, ConnectingViewModelFactory factory,
            IEventAggregator viewEventAggregator, IEnigmaSettings enigmaSettings)
        {
            TopAlphabet = topAlphabet;
            BottomAlphabet = bottomAlphabet;
            _factory = factory;
            _enigmaSettings = enigmaSettings;
            _plugboardController = new PlugboardController(enigmaSettings);
            viewEventAggregator.Subscribe(this);
        }

        public ConnectingAlphabetViewModel BottomAlphabet { get; }

        public Grid Grid
        {
            get
            {
                return _grid ?? (_grid = View.GetChildOfType<Grid>() as Grid);
            }
        }

        public ConnectingAlphabetViewModel TopAlphabet { get; }

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

                AddConnection(_sourceRectangle, _destinationRectangle, _line);
            }
        }

        private void AddConnection(ConnectingRectangleViewModel source, ConnectingRectangleViewModel destination, Line line)
        {
            FinishDrawingLine(destination);
            AddNewConnection(source, destination, line);
            AddReverseConnection(source, destination);
        }

        private void AddReverseConnection(ConnectingRectangleViewModel source, ConnectingRectangleViewModel destination)
        {
            var sourceLetter = source.Letter;
            var destinationLetter = destination.Letter;
            if (sourceLetter != destinationLetter)
            {
                ConnectingAlphabetViewModel sourceAlphabet, destinationAlphabet;
                if (TopAlphabet.ConnectingRectangles.Contains(source))
                {
                    sourceAlphabet = TopAlphabet;
                    destinationAlphabet = BottomAlphabet;
                }
                else
                {
                    sourceAlphabet = BottomAlphabet;
                    destinationAlphabet = TopAlphabet;
                }

                var reverseSourceLetter = sourceAlphabet.ConnectingRectangles.Single(x => x.Letter == destinationLetter);
                var reverseDestinationLetter = destinationAlphabet.ConnectingRectangles.Single(x => x.Letter == sourceLetter);
                var line = AddLine(reverseSourceLetter, reverseDestinationLetter);
                AddNewConnection(reverseSourceLetter, reverseDestinationLetter, line);
            }
        }

        private Line AddLine(ConnectingRectangleViewModel source, ConnectingRectangleViewModel destination)
        {
            var startPosition = GetAnchorPoint(source);
            var endPosition = GetAnchorPoint(destination);

            var line = DrawerHelper.GetLine(startPosition, endPosition, isHitTestVisible : false);
            AddLineToGrid(line);

            return line;
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

            _plugboardController.AddPlugboardConnection(connection.From, connection.To);
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
            return TopAlphabet.ConnectingRectangles.Contains(_sourceRectangle) && TopAlphabet.ConnectingRectangles.Contains(rectangle) ||
                    BottomAlphabet.ConnectingRectangles.Contains(_sourceRectangle) && BottomAlphabet.ConnectingRectangles.Contains(rectangle);
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
            var letter = rectangle.Letter;
            var connection = _connections.Single(con => con.SourceRectangle.Letter == letter);
            var reverseConnection = _connections.Single(con =>con.DestinationRectangle.Letter == letter);

            RemoveConnection(connection);
            RemoveConnection(reverseConnection);

            void RemoveConnection(RectangleConnection con)
            {
                con.Clear();

                var line = con.ConnectingLine;
                Grid.Children.Remove(line);
                _connections.Remove(con);

                _plugboardController.RemovePlugboardConnection(con.From, con.To);
            }
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

        private class PlugboardController
        {
            private readonly IEnigmaSettings _enigmaSettings;
            private readonly IPlugboard _plugboard;

            public PlugboardController(IEnigmaSettings enigmaSettings)
            {
                _enigmaSettings = enigmaSettings;
                _plugboard = _enigmaSettings.Plugboard;
            }

            public void AddPlugboardConnection(char from, char to)
            {
                _plugboard.AddConnection(from, to);
            }

            public void RemovePlugboardConnection(char from, char to)
            {
                _plugboard.RemoveConnection(from, to);
            }
        }

        private class PlugboardViewController
        {
            public PlugboardViewController()
            {

            }
        }
    }
}