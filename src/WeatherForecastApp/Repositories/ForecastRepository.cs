using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastApp.Config;
using WeatherForecastApp.OpenWeatherMapModels;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace WeatherForecastApp.Repositories
{
    public class ForecastRepository : IForecastRepository
    {
        WeatherResponse IForecastRepository.GetForecast(string city)
        {
            string IDOWeather = Constants.OPEN_WEATHER_APPID;
            // Connection String
            var client = new RestClient($"http://api.openweathermap.org/data/3.0/weather?q={city}&units=metric&APPID={IDOWeather}");
            var request = new RestRequest();
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                // Deserialize the string content into JToken object
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                // Deserialize the JToken object into our WeatherResponse Class
                return content.ToObject<WeatherResponse>();
            }

            return null;
        }
    }
}