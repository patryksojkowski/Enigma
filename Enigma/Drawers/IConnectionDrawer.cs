
namespace EnigmaUI.Drawers
{
    using System.Windows.Controls;
    using EnigmaLibrary.Models.Enums;
    using EnigmaUI.Views.Helpers;

    public interface IConnectionDrawer
    {
        void Draw(Grid parrent, LetterView from, LetterView to, SignalDirection direction);
    }
}
