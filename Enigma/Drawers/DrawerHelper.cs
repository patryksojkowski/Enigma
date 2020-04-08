namespace EnigmaUI.Drawers
{
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public static class DrawerHelper
    {
        public static Brush DefaultBrush = new SolidColorBrush
        {
            Color = Colors.Red
        };

        public static double DefaultThickness = 2d;

        public static Line GetLine(Point start, Point end, Brush stroke = null, double? strokeThickness = null)
        {
            return new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = stroke ?? DefaultBrush,
                StrokeThickness = strokeThickness ?? DefaultThickness,
            };
        }

        public static Line GetHorizontalLine(Point start, double lenght, Brush stroke = null, double? strokeThickness = null)
        {
            return new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = start.X + lenght,
                Y2 = start.Y,
                Stroke = stroke ?? DefaultBrush,
                StrokeThickness = strokeThickness ?? DefaultThickness,
            };
        }

        public static Point GetLocation(UIElement element, UIElement relativeTo, double xOffset = 0, double yOffset = 0)
        {
            var refPoint = new Point(xOffset, yOffset);
            return element.TranslatePoint(refPoint, relativeTo);
        }
    }
}
