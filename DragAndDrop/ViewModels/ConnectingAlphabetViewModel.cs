namespace DragAndDrop.ViewModels
{
    using System.Collections.Generic;

    public class ConnectingAlphabetViewModel
    {
        private readonly ConnectingViewModelFactory _factory;

        public ConnectingAlphabetViewModel(ConnectingViewModelFactory factory)
        {
            _factory = factory;

            foreach (var letter in CommonHelper.GetAlphabet())
            {
                var rectangleViewModel = _factory.CreateRectangleViewModel(letter);
                ConnectingRectangles.Add(rectangleViewModel);
            }
        }

        public List<ConnectingRectangleViewModel> ConnectingRectangles { get; set; } = new List<ConnectingRectangleViewModel>();
    }
}