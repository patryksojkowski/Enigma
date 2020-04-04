using Caliburn.Micro;
using Xunit;
using EnigmaLibrary.Models.Classic;
using EnigmaLibrary.Tests.Stubs;

namespace EnigmaLibrary.Tests
{
    public class EnigmaTests
    {
        [Fact]
        public void PassingTest()
        {
            // Arrange
            var eventAggregator = new EnigmaEventAggregator();
            var componentFactory = new ComponentFactoryStub();
            var settings = new EnigmaSettings(eventAggregator, componentFactory);
            var enigma = new Enigma(eventAggregator, settings);

            // Act
            var result = enigma is Enigma;

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData('a', 'a')]
        [InlineData('b', 'b')]
        [InlineData('c', 'c')]
        [InlineData('d', 'd')]
        public void Encrypt_ShouldReturnCharacter(char input, char expected)
        {
            // Arrange
            var eventAggregator = new EnigmaEventAggregator();
            var componentFactory = new ComponentFactoryStub();
            var settings = new EnigmaSettingsStub(eventAggregator, componentFactory);
            var enigma = new Enigma(eventAggregator, settings);

            // Act
            var result = enigma.Encrypt(input);

            // Assert
            Assert.Equal(result, expected);
        }
    }
}
