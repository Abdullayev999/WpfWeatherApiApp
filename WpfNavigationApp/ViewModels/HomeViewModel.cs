using System.Collections.ObjectModel;
using WpfNavigationApp.Models;
using System.Net;
using WpfDependencyInjectionApp.Services;
using WpfNavigationApp.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace WpfNavigationApp.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {        
        private ObservableCollection<Forecast> forecasts;
        public ObservableCollection<Forecast> Forecasts { get => forecasts; set => Set(ref forecasts, value); }
        
        public readonly IForecastStorage forecastStorage;
        private readonly IMessenger messanger;

        public WebClient WebClient { get; set; }
        
        
        // Dependency injection(DI)
        public HomeViewModel(IForecastStorage forecastStorage , IMessenger messanger)
        {
            this.forecastStorage = forecastStorage;
            this.messanger = messanger;
            Forecasts = new ObservableCollection<Forecast>(forecastStorage.GetAllForecasts());
         }

        private RelayCommand addCommand = null;
        public RelayCommand AddCommand => 
            addCommand ??= new RelayCommand(
        () =>
        {
            messanger.Send(new NavigationMessage { viewModelType = typeof(AddViewModel)});
        });

        private Forecast selectedForecast;

        public Forecast SelectedForecast
        { 
            get => selectedForecast;
            set { Set(ref selectedForecast, value); 
                RemoveCommand.RaiseCanExecuteChanged(); 
            } 
        }


        private RelayCommand<Forecast> removeCommand = null;
        public RelayCommand<Forecast> RemoveCommand =>
            removeCommand ??= new RelayCommand<Forecast>(
        param =>
        {
            forecastStorage.RemoveForecast(param);
            forecasts.Remove(param);
        }, param => selectedForecast != null);



        private RelayCommand<Forecast> detailsCommand = null;
        public RelayCommand<Forecast> DetailsCommand => detailsCommand ??= new RelayCommand<Forecast>(
        param =>
        {
            if (param != null)
            {
                messanger.Send(new NavigationMessage { viewModelType = typeof(DetailViewModel) });
                messanger.Send(new ForecastDetailsMessage { Forecast = param });
            }
            else MessageBox.Show("Please choose the city!");
            
        });

    }
}
