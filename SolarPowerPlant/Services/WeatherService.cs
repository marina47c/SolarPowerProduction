using SolarPowerAPI.Models;
using SolarPowerAPI.Models.DTOs.ForecastDTOs;
using SolarPowerAPI.Models.Entities;

namespace SolarPowerAPI.Services
{
    public class WeatherService
    {
        private readonly ILogger<WeatherService> _logger;
        private readonly HttpClient _httpClient;
        public WeatherService(ILogger<WeatherService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<WeatherApiResponseDto?> FetchWeatherDataAsync(string forecastRequestUrl)
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.GetAsync(forecastRequestUrl);
                httpResponse.EnsureSuccessStatusCode();
                return await httpResponse.Content.ReadFromJsonAsync<WeatherApiResponseDto?>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while trying to fetch data: {ex.Message}");
                return null;
            }
        }
        public string BuildForecastRequestUrl(DateTime endDate, SolarPlant solarPlant, Settings? settings)
        {
            string forecastServiceEndpoint = settings?.OpenMeteo?.OpenMeteoForecastEndpoint ?? string.Empty;
            double forecastDays = (endDate - DateTime.Now).TotalDays + 1;
            double roundedForcastDays = Math.Ceiling(forecastDays);

            string forecastQuery = settings?.OpenMeteo?.OpenMeteoForecastQuery ?? String.Empty;
            forecastQuery = forecastQuery.Replace("{latitude}", solarPlant.LocationLatitude.ToString())
                                         .Replace("{longitude}", solarPlant.LocationLongitude.ToString())
                                         .Replace("{forecastDays}", roundedForcastDays.ToString());

            return forecastServiceEndpoint + forecastQuery;
        }
    }
}
