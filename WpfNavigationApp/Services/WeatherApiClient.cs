using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Windows;
using WpfNavigationApp.Models;

namespace WpfDependencyInjectionApp.Services
{
    class WeatherApiClient : IWeatherApiClient
    {
        private WebClient WebClient;
        private string appId = "835492c0eab300994ec658dfb16ad305";
        private string units = "metric";
        private string apiUri = "http://api.openweathermap.org/data/2.5";


        public WeatherApiClient(ILogger logger)
        {
            WebClient = new WebClient();
            Logger = logger;
        }

        public ILogger Logger { get; }

        public Forecast GetForecastByCityName(string cityName)
        {
            try
            {
                var json = WebClient.DownloadString($"{apiUri}/weather?q={cityName}&appid={appId}&units={units}");
                var forecast = JsonSerializer.Deserialize<Forecast>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); 
                return forecast;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error!!!",ex);
                throw;
            }
            
        }


        public Forecast GetForecastByCityGeolocation(double latitude , double longitude)
        {
            try
            {
                var json = WebClient.DownloadString($"{apiUri}/weather?lat={latitude}&lon={longitude}&appid={appId}&units={units}");
                var forecast = JsonSerializer.Deserialize<Forecast>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return forecast;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error!!!", ex);
                throw;
            }

        }
    }
}
