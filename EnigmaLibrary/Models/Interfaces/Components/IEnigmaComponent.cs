namespace EnigmaLibrary.Models.Interfaces.Components
{
    public interface IEnigmaComponent
    {
        ISignal Process(ISignal input);
    }
}
