namespace EnigmaLibrary.Tests
{
    using Xunit;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Classic.Components;

    public class RotorTests
    {
        [Theory]
        [InlineData('A', 'E', false, false, 0, 0)]
        [InlineData('A', 'T', false, false, 11, 11)]
        [InlineData('A', 'O', true, false, 11, 12)]
        [InlineData('Z', 'N', false, false, 11, 11)]
        [InlineData('Z', 'T', true, false, 11, 12)]
        [InlineData('A', 'E', true, true, 25, 0)]
        public void Process_GetCorrectOutput(char inputLetter, char expectedLetter, bool step, bool expectedStep, int position, int expectedPosition,
            RotorType type = RotorType.I, RotorSlot slot = RotorSlot.One)
        {
            // Arrange
            var componentFactory = new ComponentFactory();
            var rotor = componentFactory.CreateRotor(type, slot, position);
            var signal = componentFactory.SignalFactory(inputLetter, step);

            // Act
            var resultSignal = rotor.Process(signal);
            var resultLetter = resultSignal.Letter;
            var resultStep = resultSignal.Step;

            // Assert
            Assert.Equal(expectedLetter, resultLetter);
            Assert.Equal(expectedStep, resultStep);
            Assert.Equal(expectedPosition, rotor.Position);
        }

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
            var componentFactory = new ComponentFactory();
            var rotor = componentFactory.CreateRotor(RotorType.I, RotorSlot.One, position);

            // Act
            rotor.Move(steps);
            var resultPosition = rotor.Position;

            //Assert
            Assert.Equal(resultPosition, expectedPosition);
        }
    }
}
