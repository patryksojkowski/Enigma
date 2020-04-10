namespace EnigmaLibrary.Tests
{
    using Caliburn.Micro;
    using Xunit;
    using EnigmaLibrary.Models.Classic;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaLibrary.Models.Enums;
    using System.Collections.Generic;

    public class EnigmaTests
    {
        [Theory]
        [InlineData('S', 'M')]
        [InlineData('M', 'S')]
        public void Encrypt_ShouldReturnCharacter(char input, char expected)
        {
            // Arrange
            var eventAggregator = new EventAggregator();
            var componentFactory = new ComponentFactory();
            var settings = new EnigmaSettings(eventAggregator, componentFactory);
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
            var enigma = new Enigma(settings);

            // Act
            var result = enigma.Encrypt(input);

            // Assert
            Assert.Equal(expected, result);
        }

    }
}
