namespace EnigmaUI.ViewModels
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces;

    public class EncryptionViewModel : Screen
    {
        private string _input = string.Empty;
        private string _output;

        public EncryptionViewModel(IEnigma enigma)
        {
            Enigma = enigma;
        }

        public IEnigma Enigma { get; set; }

        public string Input
        {
            get
            {
                return _input;
            }
            set
            {
                HandleInputChange(_input, value);
                _input = value;
            }
        }

        public string Output
        {
            get
            {
                return _output;
            }
            set
            {
                _output = value;
                NotifyOfPropertyChange(() => Output);
            }
        }

        private void HandleInputChange(string oldValue, string newValue)
        {
            if (oldValue.Length < newValue.Length)
            {
                var addedSubstring = newValue.Substring(oldValue.Length);
                foreach (var c in addedSubstring)
                {
                    var x = c;
                    if (char.IsLetter(c))
                    {
                        x = Enigma.Encrypt(c);
                    }
                    Output += x;
                }
            }
            else if (oldValue.Length > newValue.Length)
            {
                Output = Output.Remove(newValue.Length);
            }
        }
    }
}