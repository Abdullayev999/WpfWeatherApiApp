using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using WpfNavigationApp.Models;

namespace WpfDependencyInjectionApp.Services
{
    class ForecastStorage : IForecastStorage
    {
        private List<Forecast> forecasts;
        private  const string fileName = "forecasts.json";

        public ILogger Logger { get; }

        public ForecastStorage(ILogger logger)
        {
            forecasts = new List<Forecast>(); 
            Load();
            Logger = logger;
        }

        public void AddForecast(Forecast forecast)
        {
            forecasts.Add(forecast);
            Save();
        }

        public IEnumerable<Forecast> GetAllForecasts()
        {
            return forecasts;
        }

        private void Save()
        {
            var json = JsonSerializer.Serialize(forecasts);
            File.WriteAllText(fileName, json);
        }

        private void Load()
        {
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                forecasts = JsonSerializer.Deserialize<List<Forecast>>(json);
            }
        }

        public void RemoveForecast(Forecast forecast)
        {
            forecasts.Remove(forecast);
            Save();
        }
    }
}
