using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastApp.ForecastAppModels;
using WeatherForecastApp.OpenWeatherMapModels;
using WeatherForecastApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForecastApp.Controllers
{
    public class ForecastAppController : Controller
    {

        private readonly IForecastRepository _forecastRepository;

        // GET: ForecastApp/SearchCity
        public IActionResult SearchCity()
        {
            var viewModel = new SearchCity();
            return View(viewModel);
        }

        // POST: ForecastApp/SearchCity
        [HttpPost]
        public IActionResult SearchCity(SearchCity model)
        {
            // If the model is valid, consume the Weather API to bring the data of the city
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "ForecastApp", new { city = model.CityName });
            }
            return View(model);
        }

        // GET: ForecastApp/City
        public IActionResult City(string city)
        {
            // Consume the OpenWeatherAPI in order to bring Forecast data in our page.
            WeatherResponse weatherResponse = _forecastRepository.GetForecast(city);
            City viewModel = new City();

            if (weatherResponse != null)
            {
                viewModel.Name = weatherResponse.Name;
                viewModel.Humidity = weatherResponse.Main.Humidity;
                viewModel.Pressure = weatherResponse.Main.Pressure;
                viewModel.Temp = weatherResponse.Main.Temp;
                viewModel.Weather = weatherResponse.Weather[0].Main;
                viewModel.Wind = weatherResponse.Wind.Speed;
            }
            return View(viewModel);
        }

        // Dependency Injection
        public ForecastAppController(IForecastRepository forecastAppRepo)
        {
            _forecastRepository = forecastAppRepo;
        }
    }
}