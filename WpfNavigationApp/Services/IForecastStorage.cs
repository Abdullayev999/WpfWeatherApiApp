using System;
using System.Collections.Generic;
using System.Text;
using WpfNavigationApp.Models;

namespace WpfDependencyInjectionApp.Services
{
    public interface IForecastStorage
    {
        void AddForecast(Forecast forecast);
        void RemoveForecast(Forecast forecast);

        IEnumerable<Forecast> GetAllForecasts();
    }
}
