
namespace EnigmaUI.Drawers
{
    using System.Windows.Controls;
    using EnigmaUI.Views.Helpers;

    public interface IConnectionDrawer
    {
        void Draw(Grid parrent, LetterView from, LetterView to);
    }
}
