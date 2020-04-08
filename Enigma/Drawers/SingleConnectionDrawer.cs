namespace EnigmaUI.Drawers
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;
    using EnigmaUI.Views.Helpers;

    public class SingleConnectionDrawer : IConnectionDrawer
    {
        private readonly double _xOffset = -10;
        private readonly double _yOffset = 7;
        private Line _mainLine;
        private Line _firstLine;
        private Line _secondLine;

        public void Draw(Grid grid, LetterView from, LetterView to)
        {
            ClearLines();

            Point start = DrawerHelper.GetLocation(from, grid, _xOffset, _yOffset);
            Point end = DrawerHelper.GetLocation(to, grid, _xOffset, _yOffset);

            _mainLine = DrawerHelper.GetLine(start, end);

            _firstLine = DrawerHelper.GetHorizontalLine(start, 5);
            _secondLine = DrawerHelper.GetHorizontalLine(end, 5);

            AddLines();

            void ClearLines()
            {
                grid.Children.Remove(_mainLine);
                grid.Children.Remove(_firstLine);
                grid.Children.Remove(_secondLine);
            }

            void AddLines()
            {
                grid.Children.Add(_mainLine);
                grid.Children.Add(_firstLine);
                grid.Children.Add(_secondLine);
            }
        }
    }
}
