namespace EnigmaUI
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Caliburn.Micro;
    using EnigmaLibrary.Models.Classic;
    using EnigmaLibrary.Models.Classic.Components;
    using EnigmaLibrary.Models.Interfaces;
    using EnigmaLibrary.Models.Interfaces.Components;
    using EnigmaUI.ViewModels;
    using EnigmaUI.ViewModels.Components;
    using EnigmaUI.ViewModels.Helpers;

    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void Configure()
        {
            // main components
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();

            // factories
            container.Singleton<IComponentFactory, ComponentFactory>();
            container.Singleton<IUtilityFactory, UtilityFactory>();

            // views
            container.Singleton<ShellViewModel>();
            container.Singleton<EncryptionViewModel>();
            container.Singleton<SettingsViewModel>();

            // component views
            container.Singleton<PlugboardViewModel>();
            container.Singleton<ReflectorViewModel>();
            container.Singleton<RotorViewModelFactory>();

            // helper views
            container.Singleton<HelpersViewModelFactory>();

            // single models
            container.Singleton<IEnigma, Enigma>();
            container.Singleton<IEnigmaSettings, EnigmaSettings>();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}