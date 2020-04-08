namespace EnigmaUI.ViewModels.Interfaces
{
    using Caliburn.Micro;
    using EnigmaUI.Drawers;

    public interface IAlphabetViewModel
    {
        IEventAggregator EventAggregator { get; set; }
        IConnectionDrawer ConnectionDrawer { get; set; }
    }
}
