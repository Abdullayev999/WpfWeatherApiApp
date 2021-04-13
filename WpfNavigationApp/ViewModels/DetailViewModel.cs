using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using WpfNavigationApp.Messages;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Maps.MapControl.WPF;

namespace WpfNavigationApp.ViewModels
{
    public class DetailViewModel : ViewModelBase
    {
        public DetailViewModel(IMessenger messanger)
        {
            this.messanger = messanger;
                messanger.Register<ForecastDetailsMessage>(this,
                message =>
                {
                CityName = message.Forecast.Name;
                Temperature = message.Forecast.Main.Temperature.ToString();
                CityName = message.Forecast.Name;
                var icon = message.Forecast.Weather[0].Icon;
                Image = $"http://openweathermap.org/img/wn/{icon}@2x.png";
                Location = new Location(message.Forecast.Coord.Lat, message.Forecast.Coord.Lon);
                });
        }
        private RelayCommand backCommand = null;
        private readonly IMessenger messanger;

        public RelayCommand BackCommand => backCommand ??= new RelayCommand(
        () =>
        {
            
            messanger.Send(new NavigationMessage { viewModelType = typeof(HomeViewModel) });
        });


        private Location location;

        public Location Location { get => location; set => Set(ref location, value); }


        private string cityName;

        public string CityName { get => cityName; set => Set(ref cityName, value); }
        
        private string image;

        public string Image { get => image; set => Set(ref image, value); }

        private string temperature;

        public string Temperature { get => temperature; set => Set(ref temperature, value); }

    }
}
