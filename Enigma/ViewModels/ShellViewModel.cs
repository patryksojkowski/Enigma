namespace EnigmaUI.ViewModels
{
    using Caliburn.Micro;

    public class ShellViewModel : Conductor<object>
    {
        public ShellViewModel(EncryptionViewModel encryptionViewModel, SettingsViewModel settingsViewModel)
        {
            EncryptionViewModel = encryptionViewModel;
            SettingsViewModel = settingsViewModel;
        }

        public EncryptionViewModel EncryptionViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }
    }
}