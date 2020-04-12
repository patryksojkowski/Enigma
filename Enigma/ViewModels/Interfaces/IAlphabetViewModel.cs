namespace EnigmaUI.ViewModels.Interfaces
{
    using Caliburn.Micro;
    using EnigmaUI.Drawers;
    using EnigmaUI.ViewModels.Helpers;

    public interface IAlphabetViewModel
    {
        IConnectionDrawer ConnectionDrawer { get; }

        char[] Connections { get; set; }

        IEventAggregator EventAggregator { get; set; }

        HelpersViewModelFactory HelpersViewModelFactory { get; set; }

        int PositionShift { get; set; }

        void Initialize();
    }
}