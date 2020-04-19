using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using DragAndDrop.ViewModels;
using EnigmaLibrary.Models.Classic;
using EnigmaLibrary.Models.Classic.Components;
using EnigmaLibrary.Models.Interfaces;
using EnigmaLibrary.Models.Interfaces.Components;

namespace DragAndDrop
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            // main
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            // view factories
            _container.Singleton<ConnectingViewModelFactory>();

            // view models
            _container.PerRequest<ConnectingAlphabetViewModel>();
            _container.Singleton<PlugboardViewModel>();

            // models
            _container.Singleton<IComponentFactory, ComponentFactory>();
            _container.Singleton<IEnigmaSettings, EnigmaSettings>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<PlugboardViewModel>();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }
    }
}