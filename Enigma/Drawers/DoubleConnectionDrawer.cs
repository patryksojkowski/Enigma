using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using EnigmaLibrary.Models.Enums;
using EnigmaUI.Views.Helpers;

namespace EnigmaUI.Drawers
{
    internal class DoubleConnectionDrawer : IConnectionDrawer
    {
        private readonly double _xOffset = -10;
        private readonly double _yOffset = 7;
        private Line _endOuterLineIn;
        private Line _endOuterLineOut;
        private Line _firstLineIn;
        private Line _firstLineOut;
        private Line _mainLineIn;
        private Line _mainLineOut;
        private Line _secondLineIn;
        private Line _secondLineOut;

        private Line _startOuterLineIn;
        private Line _startOuterLineOut;

        public void Draw(Grid grid, LetterView from, LetterView to, SignalDirection direction)
        {
            switch (direction)
            {
                case SignalDirection.In:
                    DrawFirst(grid, from, to);
                    break;

                case SignalDirection.Out:
                    DrawSecond(grid, from, to);
                    break;

                default:
                    break;
            }
        }

        public void DrawFirst(Grid grid, LetterView from, LetterView to)
        {
            ClearLines();

            Point start = DrawerHelper.GetLocation(from, grid, _xOffset, _yOffset);
            Point end = DrawerHelper.GetLocation(to, grid, to.Width + 2 + _xOffset + 7, _yOffset);

            _mainLineIn = DrawerHelper.GetLine(start, end);
            Grid.SetColumnSpan(_mainLineIn, 2);

            _firstLineIn = DrawerHelper.GetHorizontalLine(start, 5);
            Grid.SetColumnSpan(_firstLineIn, 2);

            _secondLineIn = DrawerHelper.GetHorizontalLine(end, -5);

            var v = new Vector(from.Width + 4, 0);

            Point outerStart = Point.Add(start, v);

            _startOuterLineIn = DrawerHelper.GetHorizontalLine(outerStart, 50);
            Grid.SetColumnSpan(_startOuterLineIn, 2);

            Point outerEnd = Point.Add(end, -v);

            _endOuterLineIn = DrawerHelper.GetHorizontalLine(outerEnd, -50);
            Grid.SetColumnSpan(_endOuterLineIn, 2);

            AddLines();

            void ClearLines()
            {
                grid.Children.Remove(_mainLineIn);
                grid.Children.Remove(_firstLineIn);
                grid.Children.Remove(_secondLineIn);
                grid.Children.Remove(_mainLineOut);
                grid.Children.Remove(_firstLineOut);
                grid.Children.Remove(_secondLineOut);
                grid.Children.Remove(_startOuterLineIn);
                grid.Children.Remove(_endOuterLineIn);
                grid.Children.Remove(_startOuterLineOut);
                grid.Children.Remove(_endOuterLineOut);
            }

            void AddLines()
            {
                grid.Children.Add(_mainLineIn);
                grid.Children.Add(_firstLineIn);
                grid.Children.Add(_secondLineIn);
                grid.Children.Add(_startOuterLineIn);
                grid.Children.Add(_endOuterLineIn);
            }
        }

        public void DrawSecond(Grid grid, LetterView from, LetterView to)
        {
            var brush = DrawerHelper.BlueBrush;
            Point start = DrawerHelper.GetLocation(from, grid, from.Width + 2 + _xOffset + 7, _yOffset);
            Point end = DrawerHelper.GetLocation(to, grid, _xOffset, _yOffset);

            _mainLineOut = DrawerHelper.GetLine(start, end, brush);
            Grid.SetColumnSpan(_mainLineOut, 2);

            _firstLineOut = DrawerHelper.GetHorizontalLine(start, -5, brush);
            Grid.SetColumnSpan(_firstLineOut, 2);

            _secondLineOut = DrawerHelper.GetHorizontalLine(end, 5, brush);
            Grid.SetColumnSpan(_secondLineOut, 2);

            var v = new Vector(from.Width + 4, 0);

            Point outerStart = Point.Add(start, -v);

            _startOuterLineOut = DrawerHelper.GetHorizontalLine(outerStart, -50, brush);
            Grid.SetColumnSpan(_startOuterLineOut, 2);

            Point outerEnd = Point.Add(end, v);

            _endOuterLineOut = DrawerHelper.GetHorizontalLine(outerEnd, 50, brush);
            Grid.SetColumnSpan(_endOuterLineOut, 2);

            AddLines();

            void AddLines()
            {
                grid.Children.Add(_mainLineOut);
                grid.Children.Add(_firstLineOut);
                grid.Children.Add(_secondLineOut);
                grid.Children.Add(_startOuterLineOut);
                grid.Children.Add(_endOuterLineOut);
            }
        }
    }
}