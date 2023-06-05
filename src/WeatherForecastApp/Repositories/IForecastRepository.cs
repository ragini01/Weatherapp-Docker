using WeatherForecastApp.OpenWeatherMapModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecastApp.Repositories
{
    public interface IForecastRepository
    {
        WeatherResponse GetForecast(string city);
    }
}