namespace EnigmaLibrary.Tests
{
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaLibrary.Models.Enums;
    using Xunit;

    public class ReflectorTests
    {
        [Theory]
        [InlineData('A', 'Y', false, false, ReflectorType.B)]
        [InlineData('P', 'C', true, false, ReflectorType.C)]
        [InlineData('Z', 'S', false, false, ReflectorType.BD)]
        [InlineData('H', 'K', false, false, ReflectorType.CD)]
        public async void Process_GetCorrectOutput(char inputLetter, char expectedLetter, bool step, bool expectedStep, ReflectorType type)
        {
            // Arrange
            var utilityFactory = new UtilityFactory();
            var factory = new ComponentFactory(utilityFactory);
            var reflector = factory.CreateReflector(type);
            var inputValue = CommonHelper.LetterToNumber(inputLetter);
            var signal = await utilityFactory.CreateSignal(inputValue, step, SignalDirection.In);

            // Act
            var resultSignal = await reflector.Process(signal);
            var resultValue = resultSignal.Value;
            var resultLetter = CommonHelper.NumberToLetter(resultValue);
            var resultStep = resultSignal.Step;

            // Assert
            Assert.Equal(expectedLetter, resultLetter);
            Assert.Equal(expectedStep, resultStep);
        }
    }
}