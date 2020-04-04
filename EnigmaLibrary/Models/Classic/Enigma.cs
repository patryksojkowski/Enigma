namespace EnigmaLibrary.Models.Classic
{
    using System.Text;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces;

    public class Enigma : IEnigma, IHandle<IEnigmaSettings>
    {
        private readonly IEnigmaEventAggregator _eventAggregator;
        private IEnigmaSettings _enigmaSettings;

        public Enigma(IEnigmaEventAggregator enigmaAggregator, IEnigmaSettings enigmaSettings)
        {
            _eventAggregator = enigmaAggregator;
            _eventAggregator.Subscribe(this);

            _enigmaSettings = enigmaSettings;
        }

        public char Encrypt(char input)
        {
            var output = input;
            foreach(var component in _enigmaSettings.ComponentList)
            {
                output = component.Process(output);
            }
            return output;
        }

        public void Handle(IEnigmaSettings enigmaSettings)
        {
            _enigmaSettings = enigmaSettings;
        }
    }
}
