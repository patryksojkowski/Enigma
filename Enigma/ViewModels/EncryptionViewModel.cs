using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Models;

namespace Enigma.ViewModels
{
    public class EncryptionViewModel : Screen
    {
        private string _input;
        private string _output;
        private IEnigma _enigma;

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

        public IEnigma Enigma
        {
            get
            {
                return _enigma;
            }
            set
            {
                _enigma = value;
            }
        }


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
