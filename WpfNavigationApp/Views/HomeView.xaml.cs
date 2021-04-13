using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfDependencyInjectionApp.Services;
using WpfNavigationApp;
using WpfNavigationApp.ViewModels;

namespace WpfNavigationApp.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
           
            
           //DataContext = App.ServicesSimpleInjector.GetInstance<HomeViewModel>();

           // DataContext = new HomeViewModel(new WeatherApiClient(),new ForecastStorage());
        }

    }
}
