namespace EnigmaUI.ViewModels.Interfaces
{
    using Caliburn.Micro;
    using EnigmaUI.Drawers;
    using EnigmaUI.ViewModels.Helpers;

    public interface IAlphabetViewModel
    {
        void Initialize();
        IEventAggregator EventAggregator { get; set; }
        IConnectionDrawer ConnectionDrawer { get; set; }
        HelpersViewModelFactory HelpersViewModelFactory { get; set; }
        char[] Connections { get; set; }
    }
}
