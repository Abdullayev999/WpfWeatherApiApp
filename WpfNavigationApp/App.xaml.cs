using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfDependencyInjectionApp.Services;
using WpfNavigationApp.ViewModels;

namespace WpfNavigationApp
{
    public partial class App : Application
    {
        public static Container ServicesSimpleInjector { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterServices();
            Start<HomeViewModel>(); 


        }

        private void RegisterServices()
        {
            ServicesSimpleInjector = new Container();

            ServicesSimpleInjector.RegisterSingleton<IMessenger,Messenger>();
            ServicesSimpleInjector.RegisterSingleton<MainViewModel>();


            ServicesSimpleInjector.Register<ILogger, FileLogger>();
            ServicesSimpleInjector.Register<IWeatherApiClient, WeatherApiClient>();
            ServicesSimpleInjector.Register<IForecastStorage, ForecastStorage>();
            ServicesSimpleInjector.Register<HomeViewModel>();
            ServicesSimpleInjector.Register<DetailViewModel>();
            ServicesSimpleInjector.Register<AddViewModel>();
            ServicesSimpleInjector.Verify();
        }


        private void Start<T>() where T : ViewModelBase
        {
            var windowViewModel = ServicesSimpleInjector.GetInstance<MainViewModel>();
            windowViewModel.CurrentViewModel = ServicesSimpleInjector.GetInstance<T>();

            var window = new MainWindow() { DataContext = windowViewModel };
            window.ShowDialog();
        }
    }
}
