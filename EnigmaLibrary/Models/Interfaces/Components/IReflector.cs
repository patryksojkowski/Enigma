namespace EnigmaLibrary.Models.Interfaces.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Enums;

    public interface IReflector : IEnigmaComponent
    {
        IEventAggregator ReflectorAggregator { get; }
        ReflectorType Type { get; }
    }
}