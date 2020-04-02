using Caliburn.Micro;
using Enigma.Models;
using Enigma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Enigma
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {
            // main components
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();

            // factories
            container.Singleton<RotorViewModelFactory>();

            // views
            container.Singleton<ShellViewModel>();
            container.Singleton<EncryptionViewModel>();
            container.Singleton<SettingsViewModel>();

            // single models
            container.Singleton<IEnigma, EnigmaStub>();
            container.Singleton<IEnigmaSettings, EnigmaSettingsStub>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
