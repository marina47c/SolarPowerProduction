using Microsoft.Extensions.Logging;
using SolarPowerAPI.Enums;
using SolarPowerAPI.Models;
using SolarPowerAPI.Models.DTOs.ForecastDTOs;
using SolarPowerAPI.Models.Entities;
using SolarPowerAPI.PredictProductionHelpers;
using System.Text.Json;

namespace SolarPowerAPI.Repositories
{
    public class ForecastRepository : IForecastRepository
    {
        private readonly ILogger<ForecastRepository> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ISolarPlantRepo _solarPlantRepo;
        private readonly PredictProductionHelper _predictorHelper;

        public ForecastRepository(ILogger<ForecastRepository> logger, HttpClient httpClient, IConfiguration configuration, 
            ISolarPlantRepo solarPlantRepo, PredictProductionHelper predictProductionHelper)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
            _solarPlantRepo = solarPlantRepo;
            _predictorHelper = predictProductionHelper;
        }

        public async Task<List<Forecast>?> GetForecastedDataAsync(GetForecastRequestDto getForecastRequest)
        {
            SolarPlant? solarPlant = await _solarPlantRepo.GetByIdAsync(getForecastRequest.SolarPlantId);
            if (solarPlant == null)
            {
                return null;
            }

            Settings? settings = _configuration.GetSection("Settings").Get<Settings>();
            string forecastRequestUrl = BuildForecastRequestUrl(getForecastRequest.EndDateTime, solarPlant, settings);
            
            WeatherApiResponseDto? response = await FetchWeatherDataAsync(forecastRequestUrl);
            if (response == null)
            {
                return null;
            }

            List<Forecast>? forecasts = CreateForecasts(getForecastRequest, response, solarPlant);

            return forecasts;
        }

        private string BuildForecastRequestUrl(DateTime endDate, SolarPlant solarPlant, Settings? settings)
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


        private List<Forecast> CreateForecasts(GetForecastRequestDto request, WeatherApiResponseDto response, SolarPlant solarPlant)
        {
            var forecasts = response.Hourly.Time
                .Where(time => IncludeResultIntoForecast(request.StartDateTime, request.EndDateTime, time))
                .Select((time, index) => CreateForecast(response.Hourly, index, time, solarPlant.Id, solarPlant.InstalledPower, request.GranularityLevel))
                .ToList();

            return forecasts;
        }

        private Forecast CreateForecast(HourlyData hourlyData, int index, DateTime dateTime, Guid solarPlantId, double installedPower, GranularityLevel level)
        {
            return new Forecast
            {
                Id = index + 1,
                ForcastedPower = GetForcastedPower(hourlyData.Time[index], hourlyData.Weather_Code[index], hourlyData.Temperature_2m[index], installedPower, level),
                ProductionDateTime = dateTime,
                SolarPowerPlantId = solarPlantId
            };
        }

        private bool IncludeResultIntoForecast(DateTime startDateTimeFromRequest, DateTime endDateTimeFromRequest, DateTime responseDateTime)
        {
            return responseDateTime >= startDateTimeFromRequest && responseDateTime <= endDateTimeFromRequest;
        }

        private async Task<WeatherApiResponseDto?> FetchWeatherDataAsync(string forecastRequestUrl)
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

        private double GetForcastedPower(DateTime currentDateTime, WeatherCode weatherCode, double temperature, double installedPower, GranularityLevel level)
        {
            double timeEffectOnProduction = _predictorHelper.GetTimeEffectOnProduction(TimeOnly.FromDateTime(currentDateTime));
            double weatherEffectOnProduction = _predictorHelper.GetWatherEffectOnProduction(weatherCode);
            double temperatureEffectOnProduction = _predictorHelper.GetTemperatureEffectOnProduction(temperature);
            double maxProduction = level == GranularityLevel.FifteenMinutes ? installedPower / 4 : installedPower;

            double production = timeEffectOnProduction * weatherEffectOnProduction * temperatureEffectOnProduction * maxProduction;

            return production;
        }
    }
}
