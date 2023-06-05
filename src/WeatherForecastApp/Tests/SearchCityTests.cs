using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using WeatherForecastApp.ForecastAppModels;

namespace WeatherForecastApp.Tests
{
    [TestFixture]
    public class SearchCityTests
    {
        [Test]
        public void CityName_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var searchCity = new SearchCity();
            string cityName = "London";

            // Act
            searchCity.CityName = cityName;
            var result = searchCity.CityName;

            // Assert
            Assert.That(result, Is.EqualTo(cityName));
        }

        [TestCase(null)]
        [TestCase("")]
        public void CityName_NullOrEmpty_ValidationError(string cityName)
        {
            // Arrange
            var searchCity = new SearchCity();
            searchCity.CityName = cityName;

            // Act
            var validationContext = new ValidationContext(searchCity);
            var validationResults = searchCity.Validate(validationContext);

            // Assert
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("You must enter a city name!"));
        }

        [TestCase("123")]
        [TestCase("London123")]
        public void CityName_InvalidCharacters_ValidationError(string cityName)
        {
            // Arrange
            var searchCity = new SearchCity();
            searchCity.CityName = cityName;

            // Act
            var validationContext = new ValidationContext(searchCity);
            var validationResults = searchCity.Validate(validationContext);

            // Assert
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("Only text allowed"));
        }

        [TestCase("A")]
        [TestCase("abvjfdhsdagasgasgsgsg")]
        [TestCase("1234567890")]
        public void CityName_ValidLength_NoValidationError(string cityName)
        {
            // Arrange
            var searchCity = new SearchCity();
            searchCity.CityName = cityName;

            // Act
            var validationContext = new ValidationContext(searchCity);
            var validationResults = searchCity.Validate(validationContext);

            // Assert
            Assert.That(validationResults, Is.Empty);
        }

        [TestCase("A")]
        [TestCase("gfhdhsdgsfgsfsgashjdshdhjdj")]
        public void CityName_LengthLessThan2_ValidationError(string cityName)
        {
            // Arrange
            var searchCity = new SearchCity();
            searchCity.CityName = cityName;

            // Act
            var validationContext = new ValidationContext(searchCity);
            var validationResults = searchCity.Validate(validationContext);

            // Assert
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("Enter a city name greater than 2 and lesser than 20 characters!"));
        }

        [TestCase("sahsdhgsdhndsvmnvjkdfjdnsdbsdgjsdjdshdsdgbdb")]
        public void CityName_LengthGreaterThan20_ValidationError(string cityName)
        {
            // Arrange
            var searchCity = new SearchCity();
            searchCity.CityName = cityName;

            // Act
            var validationContext = new ValidationContext(searchCity);
            var validationResults = searchCity.Validate(validationContext);

            // Assert
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("Enter a city name greater than 2 and lesser than 20 characters!"));
        }
    }
}
