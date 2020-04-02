using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public EncryptionViewModel EncryptionViewModel { get; set; }
        public SettingsViewModel SettingsViewModel { get; }

        public ShellViewModel(EncryptionViewModel encryptionViewModel, SettingsViewModel settingsViewModel)
        {
            EncryptionViewModel = encryptionViewModel;
            SettingsViewModel = settingsViewModel;
        }

    }
}
