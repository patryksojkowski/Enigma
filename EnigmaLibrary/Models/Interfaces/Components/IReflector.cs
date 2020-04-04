namespace EnigmaLibrary.Models.Interfaces.Components
{
    using EnigmaLibrary.Models.Enums;

    public interface IReflector : IEnigmaComponent
    {
        ReflectorType Type { get; set; }
    }
}
