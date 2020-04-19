using System.Windows.Shapes;
using DragAndDrop.ViewModels;

namespace DragAndDrop
{
    public class RectangleConnection
    {
        public RectangleConnection(ConnectingRectangleViewModel sourceRectangle, ConnectingRectangleViewModel destinationRectangle, Line connectingLine)
        {
            SourceRectangle = sourceRectangle;
            DestinationRectangle = destinationRectangle;
            ConnectingLine = connectingLine;

            SourceRectangle.IsConnected = true;
            DestinationRectangle.IsConnected = true;

            From = SourceRectangle.Letter;
            To = DestinationRectangle.Letter;
        }

        public Line ConnectingLine { get; }
        public ConnectingRectangleViewModel DestinationRectangle { get; }
        public ConnectingRectangleViewModel SourceRectangle { get; }

        public void Clear()
        {
            SourceRectangle.IsConnected = false;
            DestinationRectangle.IsConnected = false;
        }

        public char From { get; }
        public char To { get; }
    }
}