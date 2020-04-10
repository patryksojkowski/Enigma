using Caliburn.Micro;
using EnigmaUI.Drawers;
using EnigmaUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnigmaUI.Helpers;
using EnigmaLibrary.Models.Interfaces.Components;
using EnigmaLibrary.Helpers;

namespace EnigmaUI.ViewModels.Helpers
{
    public class DoubleAlphabetViewModel : AlphabetViewModelBase, IAlphabetViewModel, IViewAware, IHandle<ILetterTranslation>
    {
        public List<LetterViewModel> InnerLetterViews { get; } = new List<LetterViewModel>();
        public List<LetterViewModel> OuterLetterViews { get; } = new List<LetterViewModel>();

        public override void Initialize()
        {
            base.Initialize();
            foreach (var letter in AlphabetHelper.GetAlphabet())
            {
                InnerLetterViews.Add(HelpersViewModelFactory.CreateLetter(letter));
                var value = CommonHelper.LetterToNumber(letter);
                OuterLetterViews.Add(HelpersViewModelFactory.CreateLetter(Connections[value]));
            }
        }

        public override void Handle(ILetterTranslation message)
        {
            
        }
    }
}
