using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Models.Components;

namespace Enigma.ViewModels
{
    public class SettingsViewModel
    {
        public RotorViewModel Rotor1 { get; set; }
        public RotorViewModel Rotor2 { get; set; }
        public RotorViewModel Rotor3 { get; set; }
        public SettingsViewModel(RotorViewModelFactory rotorFactory)
        {
            Rotor1 = rotorFactory.Create(RotorSlot.One);
            Rotor2 = rotorFactory.Create(RotorSlot.Two);
            Rotor3 = rotorFactory.Create(RotorSlot.Three);
        }
    }
}
