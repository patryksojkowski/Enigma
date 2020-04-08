namespace EnigmaUI.ViewModels.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaUI.ViewModels.Helpers;

    public class ReflectorViewModel : PropertyChangedBase
    {
        private readonly ReflectorController _reflectorController;
        private readonly ReflectorViewController _viewController;
        private SingleAlphabetViewModel _alphabetViewModel;

        public ReflectorViewModel(IEventAggregator enigmaAggregator, IComponentFactory componentFactory, IEnigmaSettings enigmaSettings, HelpersViewModelFactory helpersViewModelFactory)
        {
            Types = Enum.GetValues(typeof(ReflectorType)).Cast<ReflectorType>();

            _reflectorController = new ReflectorController(enigmaSettings, componentFactory, enigmaAggregator);

            var reflectorAggregator = _reflectorController.GetAggregator();

            _viewController = new ReflectorViewController(reflectorAggregator, helpersViewModelFactory);

            AlphabetViewModel = _viewController.GetAlphabetViewModel();
        }

        public IEnumerable<ReflectorType> Types { get; set; }

        public SingleAlphabetViewModel AlphabetViewModel
        {
            get
            {
                return _alphabetViewModel;
            }
            set
            {
                _alphabetViewModel = value;
                NotifyOfPropertyChange(() => AlphabetViewModel);
            }
        }

        public void ChangeReflector(object sender, SelectionChangedEventArgs args)
        {
            var type = (ReflectorType)((ComboBox)sender).SelectedItem;
            _reflectorController.ChangeReflector(type);

            var reflectorAggregator = _reflectorController.GetAggregator();

            _viewController.SetReflectorAggregator(reflectorAggregator);
        }

        private class ReflectorController
        {
            private readonly IEnigmaSettings _enigmaSettings;
            private readonly IComponentFactory _componentFactory;
            private readonly IEventAggregator _settingsAggregator;

            private IReflector _reflector;

            public ReflectorController(IEnigmaSettings enigmaSettings, IComponentFactory componentFactory, IEventAggregator settingsAggregator)
            {
                _enigmaSettings = enigmaSettings;
                _componentFactory = componentFactory;
                _settingsAggregator = settingsAggregator;

                _reflector = _enigmaSettings.Reflector;
            }

            public void ChangeReflector(ReflectorType type)
            {
                _reflector = _componentFactory.CreateReflector(type);
                _settingsAggregator.PublishOnUIThread(_reflector);
            }

            public IEventAggregator GetAggregator()
            {
                return _reflector.ReflectorAggregator;
            }
        }

        private class ReflectorViewController
        {
            private readonly HelpersViewModelFactory _helpersViewModelFactory;
            private readonly SingleAlphabetViewModel _alphabetViewModel;

            public ReflectorViewController(IEventAggregator reflectorAggregator, HelpersViewModelFactory helpersViewModelFactory)
            {
                _helpersViewModelFactory = helpersViewModelFactory;
                _alphabetViewModel = _helpersViewModelFactory.CreateAlphabetViewModel(reflectorAggregator);
            }

            public SingleAlphabetViewModel GetAlphabetViewModel()
            {
                return _alphabetViewModel;
            }

            public void SetReflectorAggregator(IEventAggregator reflectorAggregator)
            {
                _alphabetViewModel.EventAggregator = reflectorAggregator;
            }
        }
    }
}
