using SolarPowerAPI.Models.DTOs.ForecastDTOs;
using SolarPowerAPI.Models.DTOs.ProductionDTOs;
using SolarPowerAPI.Models.Entities;

namespace SolarPowerAPI.Repositories
{
    public interface IForecastRepository
    {
        Task<List<Forecast>?> GetForecastedDataAsync(GetForecastRequestDto getForecastRequest);
    }
}
