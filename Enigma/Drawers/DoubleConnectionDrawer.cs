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

        private readonly Grid _grid;

        private readonly Line _endOuterLineIn;
        private readonly Line _endOuterLineOut;
        private readonly Line _firstLineIn;
        private readonly Line _firstLineOut;
        private readonly Line _mainLineIn;
        private readonly Line _mainLineOut;
        private readonly Line _secondLineIn;
        private readonly Line _secondLineOut;
        private readonly Line _startOuterLineIn;
        private readonly Line _startOuterLineOut;

        public DoubleConnectionDrawer(Grid grid)
        {
            _grid = grid;

            _mainLineIn = DrawerHelper.GetLine();

            _firstLineIn = DrawerHelper.GetLine();

            _secondLineIn = DrawerHelper.GetLine();

            _startOuterLineIn = DrawerHelper.GetLine();

            _endOuterLineIn = DrawerHelper.GetLine();


            var brush = DrawerHelper.BlueBrush;

            _mainLineOut = DrawerHelper.GetLine(brush);

            _firstLineOut = DrawerHelper.GetLine(brush);

            _secondLineOut = DrawerHelper.GetLine(brush);

            _startOuterLineOut = DrawerHelper.GetLine(brush);

            _endOuterLineOut = DrawerHelper.GetLine(brush);

            AddLines();

            void AddLines ()
            {
                _grid.Children.Add(_mainLineIn);
                Grid.SetColumnSpan(_mainLineIn, 2);

                _grid.Children.Add(_firstLineIn);
                Grid.SetColumnSpan(_firstLineIn, 2);

                _grid.Children.Add(_secondLineIn);
                Grid.SetColumnSpan(_secondLineIn, 2);

                _grid.Children.Add(_startOuterLineIn);
                Grid.SetColumnSpan(_startOuterLineIn, 2);

                _grid.Children.Add(_endOuterLineIn);
                Grid.SetColumnSpan(_endOuterLineIn, 2);



                _grid.Children.Add(_mainLineOut);
                Grid.SetColumnSpan(_mainLineOut, 2);

                _grid.Children.Add(_firstLineOut);
                Grid.SetColumnSpan(_firstLineOut, 2);

                _grid.Children.Add(_secondLineOut);
                Grid.SetColumnSpan(_secondLineOut, 2);

                _grid.Children.Add(_startOuterLineOut);
                Grid.SetColumnSpan(_startOuterLineOut, 2);

                _grid.Children.Add(_endOuterLineOut);
                Grid.SetColumnSpan(_endOuterLineOut, 2);
            }
        }


        public void Draw(LetterView from, LetterView to, SignalDirection direction)
        {
            switch (direction)
            {
                case SignalDirection.In:
                    DrawFirst(from, to);
                    break;

                case SignalDirection.Out:
                    DrawSecond(from, to);
                    break;

                default:
                    break;
            }
        }

        public void DrawFirst(LetterView from, LetterView to)
        {

            Point start = DrawerHelper.GetLocation(from, _grid, _xOffset, _yOffset);
            Point end = DrawerHelper.GetLocation(to, _grid, to.Width + 2 + _xOffset + 7, _yOffset);

            DrawerHelper.SetLine(_mainLineIn, start, end);

            DrawerHelper.SetHorizontalLine(_firstLineIn, start, 5);

            DrawerHelper.SetHorizontalLine(_secondLineIn, end, -5);


            var v = new Vector(from.Width + 4, 0);

            Point outerStart = Point.Add(start, v);
            DrawerHelper.SetHorizontalLine(_startOuterLineIn, outerStart, 50);


            Point outerEnd = Point.Add(end, -v);
            DrawerHelper.SetHorizontalLine(_endOuterLineIn, outerEnd, -50);
        }

        public void DrawSecond(LetterView from, LetterView to)
        {
            var brush = DrawerHelper.BlueBrush;
            Point start = DrawerHelper.GetLocation(from, _grid, from.Width + 2 + _xOffset + 7, _yOffset);
            Point end = DrawerHelper.GetLocation(to, _grid, _xOffset, _yOffset);

            DrawerHelper.SetLine(_mainLineOut, start, end);

            DrawerHelper.SetHorizontalLine(_firstLineOut, start, -5);


            DrawerHelper.SetHorizontalLine(_secondLineOut, end, 5);

            var v = new Vector(from.Width + 4, 0);

            Point outerStart = Point.Add(start, -v);

            DrawerHelper.SetHorizontalLine(_startOuterLineOut, outerStart, -50);

            Point outerEnd = Point.Add(end, v);

            DrawerHelper.SetHorizontalLine(_endOuterLineOut, outerEnd, 50);
        }
    }
}