using SolarPowerAPI.Models.Entities;

namespace SolarPowerAPI.Repositories
{
    public interface ISolarPlantRepo
    {
        Task<List<SolarPlant>> GetAllAsync();
        Task<SolarPlant?> GetByIdAsync(Guid id);
        Task<SolarPlant> CreateAsync(SolarPlant entity);
        Task<SolarPlant?> UpdateAsync(Guid id, SolarPlant entity);
        Task<SolarPlant?> DeleteAsync(Guid id);
    }
}
