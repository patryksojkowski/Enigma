namespace EnigmaUI.ViewModels
{
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaUI.Helpers;

    public class EncryptionViewModel : Screen
    {
        private readonly SemaphoreQueue _semaphoreQueue = new SemaphoreQueue(1, 1);
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
                var oldValue = _input;
                var newValue = value;
                Task.Run(() => HandleInputChange(oldValue, newValue));

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

        private async Task HandleInputChange(string oldValue, string newValue)
        {
            await _semaphoreQueue.WaitAsync();
            await Task.Run(async () =>
            {
                if (oldValue.Length < newValue.Length)
                {
                    var addedSubstring = newValue.Substring(oldValue.Length);
                    addedSubstring = addedSubstring.ToUpper();
                    foreach (var c in addedSubstring)
                    {
                        var x = c;
                        if (char.IsLetter(c))
                        {
                            x = await Enigma.Encrypt(c);
                        }
                        Output += x;
                    }
                }
                else if (oldValue.Length > newValue.Length)
                {
                    Output = Output.Remove(newValue.Length);
                }
            });

            _semaphoreQueue.Release();
        }
    }
}