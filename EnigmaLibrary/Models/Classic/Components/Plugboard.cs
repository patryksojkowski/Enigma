namespace EnigmaLibrary.Models.Classic.Components
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Plugboard : IPlugboard
    {
        private readonly IUtilityFactory _utilityFactory;

        public Plugboard(Dictionary<char, char> connections, IUtilityFactory utilityFactory)
        {
            _utilityFactory = utilityFactory;

            Connections = connections;
        }

        public Dictionary<char, char> Connections { get; }

        public void AddConnection(char from, char to)
        {
            if (from == to)
            {
                return;
            }
            if (Connections.ContainsKey(from) || Connections.ContainsKey(to))
            {
                return;
            }
            Connections.Add(from, to);
            Connections.Add(to, from);
        }

        public async Task<Signal> Process(Signal signal)
        {
            var inputValue = signal.Value;

            var inputLetter = CommonHelper.NumberToLetter(inputValue);

            var outputLetter = inputLetter;

            if (Connections.ContainsKey(inputLetter))
            {
                outputLetter = Connections[inputLetter];
            }

            var resultValue = CommonHelper.LetterToNumber(outputLetter);

            return await _utilityFactory.CreateSignal(resultValue, true, signal.Direction);
        }

        public void RemoveConnection(char from, char to)
        {
            Connections.Remove(from);
            Connections.Remove(to);
        }
    }
}