namespace EnigmaLibrary.Models.Classic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;
    using Newtonsoft.Json;

    public partial class EnigmaSettings
    {
        private class SettingsHelper
        {
            private readonly EnigmaSettings _enigmaSettings;

            public SettingsHelper(EnigmaSettings enigmaSettings)
            {
                _enigmaSettings = enigmaSettings;
            }

            public void InitializeSettings()
            {
                try
                {
                    LoadSettings();
                }
                catch (Exception ex)
                {
                    throw new TypeInitializationException(nameof(EnigmaSettings), ex);
                }
            }

            public void SaveSettings()
            {
                var settings = CreateSavedSettings();
                try
                {
                    var settingsJson = JsonConvert.SerializeObject(settings);
                    File.WriteAllText(Directory.GetCurrentDirectory() + @"\Config\SavedSettings.json", settingsJson);
                }
                catch
                {
                    throw;
                }
            }

            private SavedSettings CreateSavedSettings()
            {
                return new SavedSettings()
                {
                    ReflectorType = _enigmaSettings.Reflector.Type,
                    PlugboardConnections = _enigmaSettings.Plugboard.Connections,

                    Slot1 = new SavedSettings.Slot
                    {
                        Position = _enigmaSettings.Rotor1.PositionShift,
                        RotorType = _enigmaSettings.Rotor1.Type
                    },

                    Slot2 = new SavedSettings.Slot
                    {
                        Position = _enigmaSettings.Rotor2.PositionShift,
                        RotorType = _enigmaSettings.Rotor2.Type
                    },

                    Slot3 = new SavedSettings.Slot
                    {
                        Position = _enigmaSettings.Rotor3.PositionShift,
                        RotorType = _enigmaSettings.Rotor3.Type
                    },
                };
            }

            private void InitializeComponentList()
            {
                _enigmaSettings.ComponentList = new List<IEnigmaComponent>
                {
                    _enigmaSettings.Plugboard,
                    _enigmaSettings.Rotor1,
                    _enigmaSettings.Rotor2,
                    _enigmaSettings.Rotor3,
                    _enigmaSettings.Reflector,
                    _enigmaSettings.Rotor3,
                    _enigmaSettings.Rotor2,
                    _enigmaSettings.Rotor1,
                    _enigmaSettings.Plugboard
                };
            }

            private void LoadDefaultSettings()
            {
                _enigmaSettings.Rotor1 = _enigmaSettings.ComponentFactory.CreateRotor(RotorType.I, RotorSlot.One, 0);
                _enigmaSettings.Rotor2 = _enigmaSettings.ComponentFactory.CreateRotor(RotorType.II, RotorSlot.Two, 0);
                _enigmaSettings.Rotor3 = _enigmaSettings.ComponentFactory.CreateRotor(RotorType.III, RotorSlot.Three, 0);

                _enigmaSettings.Reflector = _enigmaSettings.ComponentFactory.CreateReflector(ReflectorType.B);
                _enigmaSettings.Plugboard = _enigmaSettings.ComponentFactory.CreatePlugboard();
            }

            private void LoadFromSettings(SavedSettings settings)
            {
                _enigmaSettings.Rotor1 = _enigmaSettings.ComponentFactory.CreateRotor(settings.Slot1.RotorType, RotorSlot.One, settings.Slot1.Position);
                _enigmaSettings.Rotor2 = _enigmaSettings.ComponentFactory.CreateRotor(settings.Slot2.RotorType, RotorSlot.Two, settings.Slot2.Position);
                _enigmaSettings.Rotor3 = _enigmaSettings.ComponentFactory.CreateRotor(settings.Slot3.RotorType, RotorSlot.Three, settings.Slot3.Position);

                _enigmaSettings.Reflector = _enigmaSettings.ComponentFactory.CreateReflector(settings.ReflectorType);
                _enigmaSettings.Plugboard = _enigmaSettings.ComponentFactory.CreatePlugboard(settings.PlugboardConnections);
            }

            private void LoadSettings()
            {
                var settingsJson = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Config\SavedSettings.json");
                var settings = JsonConvert.DeserializeObject<SavedSettings>(settingsJson);

                if (settings is null)
                {
                    LoadDefaultSettings();
                }
                else
                {
                    LoadFromSettings(settings);
                }

                InitializeComponentList();
            }
        }
    }
}