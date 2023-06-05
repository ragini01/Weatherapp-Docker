using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WeatherForecastApp.Controllers;
using WeatherForecastApp.ForecastAppModels;
using WeatherForecastApp.OpenWeatherMapModels;
using WeatherForecastApp.Repositories;

namespace WeatherForecastApp.Tests
{
    [TestFixture]
    public class ForecastAppControllerTests
    {
        private ForecastAppController _controller;
        private Mock<IForecastRepository> _mockForecastRepository;

        [SetUp]
        public void Setup()
        {
            _mockForecastRepository = new Mock<IForecastRepository>();
            _controller = new ForecastAppController(_mockForecastRepository.Object);
        }

        [Test]
        public void SearchCity_Get_ReturnsView()
        {
            // Arrange

            // Act
            var result = _controller.SearchCity();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void SearchCity_Post_ValidModel_RedirectsToCityAction()
        {
            // Arrange
            var model = new SearchCity { CityName = "London" };

            // Act
            var result = _controller.SearchCity(model);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            var redirectResult = (RedirectToActionResult)result;
            Assert.That(redirectResult.ActionName, Is.EqualTo("City"));
            Assert.That(redirectResult.ControllerName, Is.EqualTo("ForecastApp"));
            Assert.That(redirectResult.RouteValues["city"], Is.EqualTo(model.CityName));
        }

        [Test]
        public void SearchCity_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            var model = new SearchCity { CityName = "" };
            _controller.ModelState.AddModelError("CityName", "The CityName field is required.");

            // Act
            var result = _controller.SearchCity(model);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = (ViewResult)result;
            Assert.That(viewResult.Model, Is.EqualTo(model));
        }

        [Test]
        public void City_ReturnsViewWithViewModel()
        {
            // Arrange
            var cityName = "London";
            var weatherResponse = new WeatherResponse
            {
                Name = cityName,
                Main = new Main { Humidity = 50, Pressure = 1015, Temp = 20 },
                Weather = new[] { new Weather { Main = "Clouds" } },
                Wind = new Wind { Speed = 5 }
            };
            _mockForecastRepository.Setup(r => r.GetForecast(cityName)).Returns(weatherResponse);

            // Act
            var result = _controller.City(cityName);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = (ViewResult)result;
            Assert.That(viewResult.Model, Is.InstanceOf<City>());
            var viewModel = (City)viewResult.Model;
            Assert.That(viewModel.Name, Is.EqualTo(weatherResponse.Name));
            Assert.That(viewModel.Humidity, Is.EqualTo(weatherResponse.Main.Humidity));
            Assert.That(viewModel.Pressure, Is.EqualTo(weatherResponse.Main.Pressure));
            Assert.That(viewModel.Temp, Is.EqualTo(weatherResponse.Main.Temp));
            Assert.That(viewModel.Weather, Is.EqualTo(weatherResponse.Weather[0].Main));
            Assert.That(viewModel.Wind, Is.EqualTo(weatherResponse.Wind.Speed));
        }
    }
}
