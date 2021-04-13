using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows;
using WpfDependencyInjectionApp.Services;
using WpfNavigationApp.Atrtribuites;
using WpfNavigationApp.Messages;
using WpfNavigationApp.Models;

namespace WpfNavigationApp.ViewModels
{
    public class AddViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly IMessenger message;
        private readonly IWeatherApiClient weatherApiClient;
        private readonly IForecastStorage forecastStorage;
        private string cityName;

        [Required(ErrorMessage ="Please fill city name")]
        public string CityName
        {
            get => cityName;
            set
            {
                Set(ref cityName, value);
                OkCommand.RaiseCanExecuteChanged();
            }
        }


        private string longitude;
        [GeoCoordinate]
        public string Longitude {
            get => longitude;
            set
            {
                Set(ref longitude, value);
                /*if (!string.IsNullOrWhiteSpace(Latitude))*/
                OkCommand.RaiseCanExecuteChanged();
            }
        }


        private string latitude;
        [GeoCoordinate]
         public string Latitude
        {
            get => latitude;
            set
            {
                Set(ref latitude, value);
                /*if (!string.IsNullOrWhiteSpace(Longitude)) */
                OkCommand.RaiseCanExecuteChanged();
            }
        }


        private bool fingByName = true;
        public bool FingByName
        {
            get => fingByName;
            set
            {
                Set(ref fingByName, value);
                if (FingByGeo)
                {
                    Latitude = string.Empty;
                    Longitude = string.Empty;
                }
            }
        }


        private bool fingByGeo;
        public bool FingByGeo
        {
            get => fingByGeo;
            set
            {
                Set(ref fingByGeo, value);
                if (FingByName) CityName = string.Empty;

            }
        }





        public AddViewModel(IMessenger message, IWeatherApiClient weatherApiClient, IForecastStorage forecastStorage)
        {
            this.message = message;
            this.weatherApiClient = weatherApiClient;
            this.forecastStorage = forecastStorage;


            message.Register<LocationMessage>(this, message =>
            {
                Longitude = message.Longitude.ToString();
                Latitude = message.Latitude.ToString();
            });
        }


        private RelayCommand okCommand = null;

        public RelayCommand OkCommand => okCommand ??= new RelayCommand(
        () =>
        {
            if (FingByName)
            {
                var forecast = weatherApiClient.GetForecastByCityName(CityName);
                forecastStorage.AddForecast(forecast);

            }
            else if (FingByGeo)
            {
                var latitude = double.Parse(Latitude);
                var longitude = double.Parse(Longitude);

                var forecast = weatherApiClient.GetForecastByCityGeolocation(latitude, longitude);
                forecastStorage.AddForecast(forecast);
            }


            message.Send(new NavigationMessage { viewModelType = typeof(HomeViewModel) });
        }
        , () =>
        {
            return (FingByName && !string.IsNullOrWhiteSpace(CityName)) || ((FingByGeo && !string.IsNullOrWhiteSpace(Longitude)) && (FingByGeo && !string.IsNullOrWhiteSpace(Latitude)));
            //return (!string.IsNullOrWhiteSpace(CityName) && !string.IsNullOrWhiteSpace(longitude) && !string.IsNullOrWhiteSpace(longitude));
        });


        private RelayCommand cancelCommand = null;

        public RelayCommand CancelCommand => cancelCommand ??= new RelayCommand(
        () =>
        {
            message.Send(new NavigationMessage { viewModelType = typeof(HomeViewModel) });
        });

        public string Error { get; }

        public string this[string columnName] 
        {
            get
            {
                var validationContext = new ValidationContext(this);
                List<ValidationResult> results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(this,validationContext , results,true);
               
                if (isValid)
                    return string.Empty;

                var result = results.FirstOrDefault(x=>x.MemberNames.Contains(columnName));

                if (result is null)
                {
                    return string.Empty;
                }

                return result.ErrorMessage;
            }
        }
    }
}
