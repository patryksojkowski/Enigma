namespace EnigmaUI.Drawers
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;
    using EnigmaLibrary.Models.Enums;
    using EnigmaUI.Views.Helpers;

    public class SingleConnectionDrawer : IConnectionDrawer
    {
        private readonly double _xOffset = -10;
        private readonly double _yOffset = 7;
        private readonly Grid _grid;

        private readonly Line _firstLine;
        private readonly Line _mainLineIn;
        private readonly Line _mainLineOut;
        private readonly Line _outFirstLine;
        private readonly Line _outSecondLine;
        private readonly Line _secondLine;

        public SingleConnectionDrawer(Grid grid)
        {
            _grid = grid;

            _mainLineIn = DrawerHelper.GetLine();

            _mainLineOut = DrawerHelper.GetLine(DrawerHelper.BlueBrush);

            _firstLine = DrawerHelper.GetLine();

            _secondLine = DrawerHelper.GetLine(DrawerHelper.BlueBrush);

            _outFirstLine = DrawerHelper.GetLine();

            _outSecondLine = DrawerHelper.GetLine(DrawerHelper.BlueBrush);

            AddLines();

            void AddLines()
            {
                _grid.Children.Add(_mainLineIn);
                _grid.Children.Add(_mainLineOut);
                _grid.Children.Add(_firstLine);
                _grid.Children.Add(_secondLine);
                _grid.Children.Add(_outFirstLine);
                _grid.Children.Add(_outSecondLine);
            }

        }

        public void Draw(Grid grid, LetterView from, LetterView to, SignalDirection direction)
        {
            Point start = DrawerHelper.GetLocation(from, grid, _xOffset, _yOffset);
            Point end = DrawerHelper.GetLocation(to, grid, _xOffset, _yOffset);
            Point middlePoint = DrawerHelper.GetMiddlePoint(start, end);

            DrawerHelper.SetLineBetweenPoints(_mainLineIn, start, middlePoint);

            DrawerHelper.SetLineBetweenPoints(_mainLineOut, middlePoint, end);

            DrawerHelper.SetHorizontalLine(_firstLine, start, 5);

            DrawerHelper.SetHorizontalLine(_secondLine, end, 5);

            var v = new Vector(from.Width + 4, 0);

            DrawerHelper.SetHorizontalLine(_outFirstLine, start + v, 50);

            DrawerHelper.SetHorizontalLine(_outSecondLine, end + v, 50);
        }
    }
}