using SolarPowerAPI.Models.DTOs.ProductionDTOs;
using SolarPowerAPI.Models.Entities;

namespace SolarPowerAPI.Repositories
{
    public interface IProductionRepo
    {
        Task<List<Production>?> GetProductionAsync(GetProductionRequestDto getProductionRequest);
    }
}
