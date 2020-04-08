namespace EnigmaLibrary.Models.Interfaces.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Enums;

    public interface IReflector : IEnigmaComponent
    {
        ReflectorType Type { get; set; }
        IEventAggregator ReflectorAggregator { get; }
    }
}
