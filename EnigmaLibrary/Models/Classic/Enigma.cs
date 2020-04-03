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

        public string Encrypt(string input)
        {
            var sb = new StringBuilder();
            foreach (var c in input)
            {
                var x = c;
                foreach(var component in _enigmaSettings.ComponentList)
                {
                    x = component.Process(x);
                }

                sb.Append(x);
            }
            return sb.ToString();
        }

        public void Handle(IEnigmaSettings enigmaSettings)
        {
            _enigmaSettings = enigmaSettings;
        }
    }
}
