using SolarPowerAPI.Enums;
using SolarPowerAPI.Models.DTOs.ForecastDTOs;
using SolarPowerAPI.Models.Entities;
using SolarPowerAPI.PredictProductionHelpers;

namespace SolarPowerAPI.Services
{
    public class ForecastFactory
    {
        private readonly PredictProductionHelper _predictorHelper;
        public ForecastFactory(PredictProductionHelper predictProductionHelper)
        {
            _predictorHelper = predictProductionHelper;
        }

        public List<Forecast> CreateForecasts(GetForecastRequestDto request, WeatherApiResponseDto response, SolarPlant solarPlant)
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

        private double GetForcastedPower(DateTime currentDateTime, WeatherCode weatherCode, double temperature, double installedPower, GranularityLevel level)
        {
            double timeEffectOnProduction = _predictorHelper.GetTimeEffectOnProduction(TimeOnly.FromDateTime(currentDateTime));
            double weatherEffectOnProduction = _predictorHelper.GetWatherEffectOnProduction(weatherCode);
            double temperatureEffectOnProduction = _predictorHelper.GetTemperatureEffectOnProduction(temperature);
            double maxProduction = level == GranularityLevel.FifteenMinutes ? installedPower / 4 : installedPower;

            return timeEffectOnProduction * weatherEffectOnProduction * temperatureEffectOnProduction * maxProduction;
        }
    }
}
