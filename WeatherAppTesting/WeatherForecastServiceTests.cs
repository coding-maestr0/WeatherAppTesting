using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppTesting
{
    public class WeatherForecastServiceTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public WeatherForecastServiceTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5256/") // Set your API base URL
            };
        }

       [Fact]
        public async Task GetWeatherForecast_ReturnsSuccess_CorrectLocation()
        {
            // Arrange
            var location = "M3C";
            var requestUri = $"api/WeatherForecast/getWeatherData?loc={location}";

            // Act
            var response = await _client.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeatherEntity>(content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(result);
            Assert.True(result.status);
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsFailure_IncorrectLocation()
        {
            // Arrange
            var location = "lkhsaas";
            var requestUri = $"api/WeatherForecast/getWeatherData?loc={location}";

            // Act
            var response = await _client.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeatherEntity>(content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(result);
            Assert.False(result.status);
        }

    }
}

