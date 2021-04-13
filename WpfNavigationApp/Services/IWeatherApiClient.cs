using System;
using System.Collections.Generic;
using System.Text;
using WpfNavigationApp.Models;

namespace WpfDependencyInjectionApp.Services
{
    public interface IWeatherApiClient
    {
        Forecast GetForecastByCityName(string cityName);
        Forecast GetForecastByCityGeolocation(double latitude, double longitude);
    }
}
