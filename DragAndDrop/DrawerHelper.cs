﻿namespace DragAndDrop
{
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public static class DrawerHelper
    {
        public static Brush BlueBrush = new SolidColorBrush
        {
            Color = Colors.Blue
        };

        public static Brush DefaultBrush = new SolidColorBrush
        {
            Color = Colors.Red
        };

        public static double DefaultThickness = 2d;

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

        public static Line GetLine(Point start, Point end, Brush stroke = null, double? strokeThickness = null, bool isHitTestVisible = true)
        {
            return new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = stroke ?? DefaultBrush,
                StrokeThickness = strokeThickness ?? DefaultThickness,
                IsHitTestVisible = isHitTestVisible,
            };
        }

        public static Point GetLocation(UIElement element, UIElement relativeTo, double xOffset = 0, double yOffset = 0)
        {
            var refPoint = new Point(xOffset, yOffset);
            return element.TranslatePoint(refPoint, relativeTo);
        }

        public static Point GetMiddlePoint(Point start, Point end)
        {
            var x = (start.X + end.X) / 2;
            var y = (start.Y + end.Y) / 2;
            return new Point(x, y);
        }

        public static Point MovePoint(Point start, double x, double y)
        {
            return new Point()
            {
                X = start.X + x,
                Y = start.Y + y,
            };
        }

        public static void SetEndPoint(this Line line, Point endPoint)
        {
            line.X2 = endPoint.X;
            line.Y2 = endPoint.Y;
        }
    }
}