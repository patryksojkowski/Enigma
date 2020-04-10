namespace EnigmaUI.ViewModels.Components
{
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaLibrary.Models.Enums;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Windows.Controls;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaUI.ViewModels.Interfaces;
    using EnigmaUI.ViewModels.Helpers;

    public class RotorViewModel : PropertyChangedBase
    {
        private readonly RotorSettingsController _componentController;
        private readonly RotorViewController _viewController;


        public RotorViewModel(IEventAggregator settingsAggregator, IComponentFactory componentFactory, RotorSlot slot, IEnigmaSettings enigmaSettings, HelpersViewModelFactory helpersViewModelFactory)
        {
            RotorTypes = Enum.GetValues(typeof(RotorType)).Cast<RotorType>();
            _componentController = new RotorSettingsController(settingsAggregator, componentFactory, enigmaSettings, slot);

            var componentAggregator = _componentController.GetAggregator();
            var connections = _componentController.GetConnections();
            _viewController = new RotorViewController(componentAggregator, helpersViewModelFactory, connections);
            AlphabetViewModel = _viewController.GetAlphabetView();
        }

        public IEnumerable<RotorType> RotorTypes { get; set; }
        public IAlphabetViewModel AlphabetViewModel { get; set; }

        public void ChangeRotor(object sender, SelectionChangedEventArgs args)
        {
            var type = (RotorType)((ComboBox)sender).SelectedItem;

            _componentController.ChangeRotor(type);
            var rotorAggregator = _componentController.GetAggregator();
            var connections = _componentController.GetConnections();

            // handle view
            _viewController.SetAggregator(rotorAggregator);
            _viewController.SetConnections(connections);

        }

        public bool CanMoveUp()
        {
            return false;
        }
        public bool CanMoveDown()
        {
            return false;
        }

        public void MoveUp()
        {
            // handle rotor
            _componentController.MoveRotor(1);

            // handle view
        }

        public void MoveDown()
        {
            // handle rotor
            _componentController.MoveRotor(-1);

            // handle view
        }

        private class RotorSettingsController
        {
            private readonly IEventAggregator _settingsAggregator;
            private readonly IComponentFactory _componentFactory;
            private readonly IEnigmaSettings _enigmaSettings;
            private readonly RotorSlot _slot;
            private IRotor _rotor;

            public RotorSettingsController(IEventAggregator settingsAggregator, IComponentFactory componentFactory, IEnigmaSettings enigmaSettings, RotorSlot slot)
            {
                _settingsAggregator = settingsAggregator;
                _componentFactory = componentFactory;
                _enigmaSettings = enigmaSettings;
                _slot = slot;
                _rotor = _enigmaSettings.GetRotor(slot);
            }

            public void ChangeRotor(RotorType type)
            {
                var position = _rotor.PositionShift;
                _rotor = _componentFactory.CreateRotor(type, _slot, position);
                _settingsAggregator.PublishOnUIThread(_rotor);
            }

            public void MoveRotor(int steps)
            {
                _rotor.Move(steps);
            }

            public IEventAggregator GetAggregator()
            {
                return _rotor.RotorAggregator;
            }
            public char[] GetConnections()
            {
                return _rotor.Connections;
            }
        }

        private class RotorViewController
        {
            private readonly HelpersViewModelFactory _helpersViewModelFactory;
            private readonly IAlphabetViewModel _alphabetViewModel;
            private char[] _connections;

            public RotorViewController(IEventAggregator componentAggregator, HelpersViewModelFactory helpersViewModelFactory, char[] connections)
            {
                _helpersViewModelFactory = helpersViewModelFactory;
                _connections = connections;
                _alphabetViewModel = _helpersViewModelFactory.CreateAlphabetViewModel<DoubleAlphabetViewModel>(componentAggregator, connections);
            }

            public  IAlphabetViewModel GetAlphabetView()
            {
                return _alphabetViewModel;
            }

            public void SetAggregator(IEventAggregator eventAggregator)
            {
                _alphabetViewModel.EventAggregator = eventAggregator;
            }

            public void SetConnections(char[] connections)
            {
                _connections = connections;
                _alphabetViewModel.Connections = connections;
            }
        }
    }
}
