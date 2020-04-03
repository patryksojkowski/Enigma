using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Models.Enums;
using Enigma.ViewModels.Components;

namespace Enigma.ViewModels
{
    public class SettingsViewModel
    {
        public RotorViewModel Rotor1ViewModel { get; set; }

        public RotorViewModel Rotor2ViewModel { get; set; }

        public RotorViewModel Rotor3ViewModel { get; set; }

        public ReflectorViewModel ReflectorViewModel { get; }

        public PlugboardViewModel PlugboardViewModel { get; }

        public SettingsViewModel(RotorViewModelFactory rotorViewModelFactory, ReflectorViewModel reflectorViewModel, PlugboardViewModel plugboardViewModel)
        {
            Rotor1ViewModel = rotorViewModelFactory.Create(RotorSlot.One);
            Rotor2ViewModel = rotorViewModelFactory.Create(RotorSlot.Two);
            Rotor3ViewModel = rotorViewModelFactory.Create(RotorSlot.Three);
            PlugboardViewModel = plugboardViewModel;
            ReflectorViewModel = reflectorViewModel;
        }
    }
}
