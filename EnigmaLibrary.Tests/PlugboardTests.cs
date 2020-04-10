namespace EnigmaLibrary.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaLibrary.Models.Enums;
    using Xunit;

    public class PlugboardTests
    {
        public class ProcessTestData : IEnumerable<object[]>
        {
            private readonly List<object[]> data = new List<object[]>
            {
                new object[]
                {
                    new Dictionary<char, char>
                    {
                        { 'A', 'B' }, { 'D', 'C' }, { 'S', 'Z' }, { 'B', 'A' }, { 'C', 'D' }, { 'Z', 'S' }
                    },
                    'A',
                    'B',
                    true,
                    true
                },
                new object[]
                {
                    new Dictionary<char, char>
                    {
                        { 'A', 'B' }, { 'D', 'C' }, { 'S', 'Z' }, { 'B', 'A' }, { 'C', 'D' }, { 'Z', 'S' }
                    },
                    'Z',
                    'S',
                    true,
                    true
                },
                new object[]
                {
                    new Dictionary<char, char>
                    {
                        { 'A', 'B' }, { 'D', 'C' }, { 'S', 'Z' }, { 'B', 'A' }, { 'C', 'D' }, { 'Z', 'S' }
                    },
                    'F',
                    'F',
                    true,
                    true
                },
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                return data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Theory]
        [ClassData(typeof(ProcessTestData))]
        public void Process_GetCorrectOutput(Dictionary<char, char> connections, char inputLetter, char expectedLetter, bool step, bool expectedStep)
        {
            // Arrange
            var factory = new ComponentFactory();
            var plugboard = factory.CreatePlugboard(connections);
            var inputValue = CommonHelper.LetterToNumber(inputLetter);
            var signal = factory.CreateSignal(inputValue, step, SignalDirection.In);

            // Act
            var resultSignal = plugboard.Process(signal);
            var resultValue = resultSignal.Value;
            var resultLetter = CommonHelper.NumberToLetter(resultValue);
            var resultStep = resultSignal.Step;

            // Assert
            Assert.Equal(expectedLetter, resultLetter);
            Assert.Equal(expectedStep, resultStep);
        }

        [Theory]
        [InlineData('A','B')]
        [InlineData('C','B')]
        [InlineData('Z','A')]
        public void AddConnection_EmptyDictionaryAndCorrectConnections(char from, char to)
        {
            // Arrange
            var factory = new ComponentFactory();
            var plugboard = factory.CreatePlugboard(new Dictionary<char, char>());

            // Act
            plugboard.AddConnection(from, to);
            var connections = plugboard.Connections;

            // Assert
            Assert.True(connections.ContainsKey(from));
            Assert.True(connections[from] == to);
            Assert.True(connections.ContainsKey(to));
            Assert.True(connections[to] == from);
        }

        [Fact]
        public void AddConnection_NotAllowRecursiveConnection()
        {
            // Arrange
            var factory = new ComponentFactory();
            var plugboard = factory.CreatePlugboard(new Dictionary<char, char>());

            // Act
            plugboard.AddConnection('A', 'A');

            // Assert
            Assert.Empty(plugboard.Connections);
        }

        public class AddConnectionTestData : IEnumerable<object[]>
        {

            private readonly List<object[]> data = new List<object[]>
            {
                new object[]
                {
                    'A', 'B',
                    new Dictionary<char, char>
                    {
                        { 'A', 'B' }, { 'D', 'C' }, { 'S', 'Z' }, { 'B', 'A' }, { 'C', 'D' }, { 'Z', 'S' }
                    },
                },
                new object[]
                {
                    'Z', 'B',
                    new Dictionary<char, char>
                    {
                        { 'A', 'B' }, { 'D', 'C' }, { 'S', 'Z' }, { 'B', 'A' }, { 'C', 'D' }, { 'Z', 'S' }
                    },
                },
                new object[]
                {
                    'E', 'S',
                    new Dictionary<char, char>
                    {
                        { 'A', 'B' }, { 'D', 'C' }, { 'S', 'Z' }, { 'B', 'A' }, { 'C', 'D' }, { 'Z', 'S' }
                    },
                },
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                return data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Theory]
        [ClassData(typeof(AddConnectionTestData))]
        public void AddConnection_NotAllowSameConnections(char from, char to, Dictionary<char, char> connections)
        {
            // Arrange
            var factory = new ComponentFactory();
            var plugboard = factory.CreatePlugboard(connections);
            var connectionsCount = plugboard.Connections.Count;

            // Act
            plugboard.AddConnection(from, to);

            // Assert
            Assert.True(plugboard.Connections.Count == connectionsCount);
        }
    }
}
