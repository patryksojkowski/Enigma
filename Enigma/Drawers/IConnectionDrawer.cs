namespace EnigmaUI.Drawers
{
    using EnigmaLibrary.Models.Enums;
    using EnigmaUI.Views.Helpers;

    public interface IConnectionDrawer
    {
        void Draw(LetterView from, LetterView to, SignalDirection direction);
    }
}