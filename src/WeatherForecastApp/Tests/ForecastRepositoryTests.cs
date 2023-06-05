using System;
using Moq;
using NUnit.Framework;
using RestSharp;
using WeatherForecastApp.Config;
using WeatherForecastApp.OpenWeatherMapModels;
using WeatherForecastApp.Repositories;

namespace WeatherForecastApp.Tests
{
    [TestFixture]
    public class ForecastRepositoryTests
    {
        private ForecastRepository _repository;
        private Mock<IRestClient> _mockRestClient;

        [SetUp]
        public void Setup()
        {
            _mockRestClient = new Mock<RestClient>();
            _repository = new ForecastRepository(_mockRestClient.Object);
        }

        [Test]
        public void GetForecast_ValidCity_ReturnsWeatherResponse()
        {
            // Arrange
            string city = "London";
            string appId = Constants.OPEN_WEATHER_APPID;
            var expectedUrl = $"http://api.openweathermap.org/data/3.0/weather?q={city}&units=metric&APPID={appId}";

            var response = new RestResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = @"{""Name"":""London"",""Main"":{""Humidity"":50,""Pressure"":1015,""Temp"":20},""Weather"":[{""Main"":""Clouds""}],""Wind"":{""Speed"":5}}"
            };
            _mockRestClient.Setup(c => c.Execute(It.IsAny<RestRequest>())).Returns(response);

            // Act
            var result = _repository.GetForecast(city);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("London"));
            Assert.That(result.Main.Humidity, Is.EqualTo(50));
            Assert.That(result.Main.Pressure, Is.EqualTo(1015));
            Assert.That(result.Main.Temp, Is.EqualTo(20));
            Assert.That(result.Weather[0].Main, Is.EqualTo("Clouds"));
            Assert.That(result.Wind.Speed, Is.EqualTo(5));

            _mockRestClient.Verify(c => c.Execute(It.Is<RestRequest>(r => r.Resource == expectedUrl)), Times.Once);
        }

        [Test]
        public void GetForecast_ApiRequestFails_ReturnsNull()
        {
            // Arrange
            string city = "London";
            string appId = Constants.OPEN_WEATHER_APPID;
            var expectedUrl = $"http://api.openweathermap.org/data/3.0/weather?q={city}&units=metric&APPID={appId}";

            var response = new RestResponse
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Content = @"{""message"":""Invalid API key. Please see http://openweathermap.org/faq#error401 for more info.""}"
            };
            _mockRestClient.Setup(c => c.Execute(It.IsAny<RestRequest>())).Returns(response);

            // Act
            var result = _repository.GetForecast(city);

            // Assert
            Assert.That(result, Is.Null);

            _mockRestClient.Verify(c => c.Execute(It.Is<RestRequest>(r => r.Resource == expectedUrl)), Times.Once);
        }
    }
}
