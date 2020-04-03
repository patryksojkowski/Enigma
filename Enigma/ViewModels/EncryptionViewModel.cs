namespace EnigmaUI.ViewModels
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces;

    public class EncryptionViewModel : Screen
    {
        private string _input;
        private string _output;

        public EncryptionViewModel(IEnigma enigma)
        {
            Enigma = enigma;
        }

        public string Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                NotifyOfPropertyChange(() => Input);
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

        public IEnigma Enigma { get; set; }


        public void Encrypt(string input)
        {
            Output = Enigma.Encrypt(input);
        }

        public bool CanEncrypt(string input)
        {
            return !string.IsNullOrEmpty(input);
        }


    }
}
