namespace EnigmaUI.ViewModels
{
    using EnigmaLibrary.Models.Enums;
    using EnigmaUI.ViewModels.Components;

    public class SettingsViewModel
    {
        public SettingsViewModel(RotorViewModelFactory rotorViewModelFactory, ReflectorViewModel reflectorViewModel, PlugboardViewModel plugboardViewModel)
        {
            Rotor1ViewModel = rotorViewModelFactory.Create(RotorSlot.One);
            Rotor2ViewModel = rotorViewModelFactory.Create(RotorSlot.Two);
            Rotor3ViewModel = rotorViewModelFactory.Create(RotorSlot.Three);
            PlugboardViewModel = plugboardViewModel;
            ReflectorViewModel = reflectorViewModel;
        }

        public PlugboardViewModel PlugboardViewModel { get; }
        public ReflectorViewModel ReflectorViewModel { get; }
        public RotorViewModel Rotor1ViewModel { get; set; }

        public RotorViewModel Rotor2ViewModel { get; set; }

        public RotorViewModel Rotor3ViewModel { get; set; }
    }
}