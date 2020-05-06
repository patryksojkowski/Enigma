namespace EnigmaLibrary.Tests
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Classic;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Tests.Stubs;
    using Xunit;

    public class EnigmaTests
    {
        [Theory]
        [InlineData('S', 'M')]
        [InlineData('M', 'S')]
        public async void Encrypt_ShouldReturnCharacter(char input, char expected)
        {
            // Arrange
            var eventAggregator = new EventAggregator();
            var utilityFactory = new UtilityFactory();
            var componentFactory = new ComponentFactory(utilityFactory);
            var settings = new EnigmaSettingsStub(eventAggregator, componentFactory);
            var savedSettings = new EnigmaSettings.SavedSettings()
            {
                ReflectorType = ReflectorType.B,
                PlugboardConnections = new Dictionary<char, char>(),
                Slot1 = new EnigmaSettings.SavedSettings.Slot()
                {
                    Position = 6,
                    RotorType = RotorType.II,
                },
                Slot2 = new EnigmaSettings.SavedSettings.Slot()
                {
                    Position = 11,
                    RotorType = RotorType.I,
                },
                Slot3 = new EnigmaSettings.SavedSettings.Slot()
                {
                    Position = 5,
                    RotorType = RotorType.III,
                },
            };
            settings.LoadSettings(savedSettings);
            var enigma = new Enigma(settings, utilityFactory);

            // Act
            var result = await enigma.Encrypt(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}