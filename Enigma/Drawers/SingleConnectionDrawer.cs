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
        private Line _firstLine;
        private Line _mainLineIn;
        private Line _mainLineOut;
        private Line _outFirstLine;
        private Line _outSecondLine;
        private Line _secondLine;

        public void Draw(Grid grid, LetterView from, LetterView to, SignalDirection direction)
        {
            ClearLines();

            Point start = DrawerHelper.GetLocation(from, grid, _xOffset, _yOffset);
            Point end = DrawerHelper.GetLocation(to, grid, _xOffset, _yOffset);
            Point middlePoint = DrawerHelper.GetMiddlePoint(start, end);

            _mainLineIn = DrawerHelper.GetLine(start, middlePoint);
            _mainLineOut = DrawerHelper.GetLine(middlePoint, end, DrawerHelper.BlueBrush);

            _firstLine = DrawerHelper.GetHorizontalLine(start, 5);
            _secondLine = DrawerHelper.GetHorizontalLine(end, 5, DrawerHelper.BlueBrush);

            var v = new Vector(from.Width + 4, 0);

            _outFirstLine = DrawerHelper.GetHorizontalLine(start + v, 50);
            _outSecondLine = DrawerHelper.GetHorizontalLine(end + v, 50, DrawerHelper.BlueBrush);

            AddLines();

            void ClearLines()
            {
                grid.Children.Remove(_mainLineIn);
                grid.Children.Remove(_mainLineOut);
                grid.Children.Remove(_firstLine);
                grid.Children.Remove(_secondLine);
                grid.Children.Remove(_outFirstLine);
                grid.Children.Remove(_outSecondLine);
            }

            void AddLines()
            {
                grid.Children.Add(_mainLineIn);
                grid.Children.Add(_mainLineOut);
                grid.Children.Add(_firstLine);
                grid.Children.Add(_secondLine);
                grid.Children.Add(_outFirstLine);
                grid.Children.Add(_outSecondLine);
            }
        }
    }
}