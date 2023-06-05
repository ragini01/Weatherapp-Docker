using NUnit.Framework;
using WeatherForecastApp.ForecastAppModels;

namespace WeatherForecastApp.Tests
{
    [TestFixture]
    public class CityTests
    {
        [Test]
        public void Name_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var city = new City();
            string cityName = "London";

            // Act
            city.Name = cityName;
            var result = city.Name;

            // Assert
            Assert.That(result, Is.EqualTo(cityName));
        }

        [Test]
        public void Temp_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var city = new City();
            float temperature = 20.5f;

            // Act
            city.Temp = temperature;
            var result = city.Temp;

            // Assert
            Assert.That(result, Is.EqualTo(temperature));
        }

        [Test]
        public void Humidity_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var city = new City();
            int humidity = 70;

            // Act
            city.Humidity = humidity;
            var result = city.Humidity;

            // Assert
            Assert.That(result, Is.EqualTo(humidity));
        }

        [Test]
        public void Pressure_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var city = new City();
            int pressure = 1015;

            // Act
            city.Pressure = pressure;
            var result = city.Pressure;

            // Assert
            Assert.That(result, Is.EqualTo(pressure));
        }

        [Test]
        public void Wind_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var city = new City();
            float windSpeed = 5.5f;

            // Act
            city.Wind = windSpeed;
            var result = city.Wind;

            // Assert
            Assert.That(result, Is.EqualTo(windSpeed));
        }

        [Test]
        public void Weather_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var city = new City();
            string weatherCondition = "Cloudy";

            // Act
            city.Weather = weatherCondition;
            var result = city.Weather;

            // Assert
            Assert.That(result, Is.EqualTo(weatherCondition));
        }
    }
}
