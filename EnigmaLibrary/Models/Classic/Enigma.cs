namespace EnigmaLibrary.Models.Classic
{
    using System.Threading.Tasks;
    using EnigmaLibrary.Helpers;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class Enigma : IEnigma
    {
        private readonly IEnigmaSettings _enigmaSettings;
        private readonly IUtilityFactory _utilityFactory;

        public Enigma(IEnigmaSettings enigmaSettings, IUtilityFactory utilityFactory)
        {
            _enigmaSettings = enigmaSettings;
            _utilityFactory = utilityFactory;
        }

        public async Task<char> Encrypt(char inputLetter)
        {
            var inputPosition = CommonHelper.LetterToNumber(inputLetter);
            var signal = await _utilityFactory.CreateSignal(inputPosition, false, SignalDirection.In);

            foreach (var component in _enigmaSettings.ComponentList)
            {
                signal = await component.Process(signal);
            }

            var resultLetter = CommonHelper.NumberToLetter(signal.Value);
            return resultLetter;
        }
    }
}