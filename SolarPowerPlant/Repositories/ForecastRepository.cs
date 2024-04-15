using Microsoft.Extensions.Logging;
using SolarPowerAPI.Enums;
using SolarPowerAPI.Models;
using SolarPowerAPI.Models.DTOs.ForecastDTOs;
using SolarPowerAPI.Models.Entities;
using SolarPowerAPI.PredictProductionHelpers;
using SolarPowerAPI.Services;
using System.Text.Json;

namespace SolarPowerAPI.Repositories
{
    public class ForecastRepository : IForecastRepository
    {
        private readonly WeatherService _weatherService;
        private readonly IConfiguration _configuration;
        private readonly ISolarPlantRepo _solarPlantRepo;
        private readonly PredictProductionHelper _predictorHelper;
        private readonly ForecastFactory _forecastFactory;

        public ForecastRepository(WeatherService weatherService, IConfiguration configuration, ISolarPlantRepo solarPlantRepo,
            ForecastFactory forecastFactory, PredictProductionHelper predictProductionHelper)
        {
            _weatherService = weatherService;
            _configuration = configuration;
            _solarPlantRepo = solarPlantRepo;
            _forecastFactory = forecastFactory;
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
            string forecastRequestUrl = _weatherService.BuildForecastRequestUrl(getForecastRequest.EndDateTime, solarPlant, settings);
            
            WeatherApiResponseDto? response = await _weatherService.FetchWeatherDataAsync(forecastRequestUrl);
            if (response == null)
            {
                return null;
            }

            return _forecastFactory.CreateForecasts(getForecastRequest, response, solarPlant);
        }
    }
}
