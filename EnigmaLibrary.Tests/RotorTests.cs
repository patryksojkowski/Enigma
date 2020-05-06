namespace EnigmaLibrary.Tests
{
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaLibrary.Models.Enums;
    using Xunit;

    public class RotorTests
    {
        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(1, 2, 3)]
        [InlineData(25, 1, 0)]
        [InlineData(24, 2, 0)]
        [InlineData(25, 2, 1)]
        [InlineData(0, -1, 25)]
        [InlineData(0, -5, 21)]
        [InlineData(25, -1, 24)]
        [InlineData(25, -26, 25)]
        public void Move_GetCorrectPosition(int position, int steps, int expectedPosition)
        {
            // Arrange
            var componentFactory = new ComponentFactory(new UtilityFactory());
            var rotor = componentFactory.CreateRotor(RotorType.I, RotorSlot.One, position);

            // Act
            rotor.Move(steps);
            var resultPosition = rotor.PositionShift;

            //Assert
            Assert.Equal(resultPosition, expectedPosition);
        }

        [Theory]
        [InlineData('A', 'E', false, false, 0, 0)]
        [InlineData('A', 'I', false, false, 11, 11)]
        [InlineData('A', 'C', true, false, 11, 12)]
        [InlineData('Z', 'C', false, false, 11, 11)]
        [InlineData('Z', 'H', true, false, 11, 12)]
        [InlineData('A', 'E', true, true, 25, 0)]
        public async void Process_GetCorrectOutput(char inputLetter, char expectedLetter, bool step, bool expectedStep, int position, int expectedPosition,
            RotorType type = RotorType.I, RotorSlot slot = RotorSlot.One)
        {
            // Arrange
            var utilityFactory = new UtilityFactory();
            var componentFactory = new ComponentFactory(utilityFactory);
            var rotor = componentFactory.CreateRotor(type, slot, position);
            var inputValue = CommonHelper.LetterToNumber(inputLetter);
            var signal = await utilityFactory.CreateSignal(inputValue, step, SignalDirection.In);

            // Act
            var resultSignal = await rotor.Process(signal);
            var resultValue = resultSignal.Value;
            var resultLetter = CommonHelper.NumberToLetter(resultValue);
            var resultStep = resultSignal.Step;

            // Assert
            Assert.Equal(expectedLetter, resultLetter);
            Assert.Equal(expectedStep, resultStep);
            Assert.Equal(expectedPosition, rotor.PositionShift);
        }
    }
}