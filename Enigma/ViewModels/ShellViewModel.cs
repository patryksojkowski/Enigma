namespace EnigmaUI.ViewModels
{
    using Caliburn.Micro;

    public class ShellViewModel : Conductor<object>
    {
        public EncryptionViewModel EncryptionViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }

        public ShellViewModel(EncryptionViewModel encryptionViewModel, SettingsViewModel settingsViewModel)
        {
            EncryptionViewModel = encryptionViewModel;
            SettingsViewModel = settingsViewModel;
        }

    }
}
