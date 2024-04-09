using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SolarPowerAPI.Data;
using SolarPowerAPI.Models.Entities;

namespace SolarPowerAPI.Repositories
{
    public class SolarPlantRepo : ISolarPlantRepo
    {
        readonly SolarPowerContext _context;

        public SolarPlantRepo(SolarPowerContext context)
        {
            _context = context;
        }

        public async Task<List<SolarPlant>> GetAllAsync()
        {
            return await _context.SolarPlants.ToListAsync();
        }

        public async Task<SolarPlant?> GetByIdAsync(Guid id)
        {
            return await _context.SolarPlants.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SolarPlant> CreateAsync(SolarPlant entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SolarPlant?> UpdateAsync(Guid id, SolarPlant entity)
        {
            SolarPlant? solarPowerPlantToUpdate = await _context.SolarPlants.FirstOrDefaultAsync(x => x.Id == id);
            if (solarPowerPlantToUpdate == null)
            {
                return null;
            }

            solarPowerPlantToUpdate.Name = entity.Name;
            solarPowerPlantToUpdate.InstalledPower = entity.InstalledPower;
            solarPowerPlantToUpdate.DateOfInstallation = entity.DateOfInstallation;
            solarPowerPlantToUpdate.LocationLatitude = entity.LocationLatitude;
            solarPowerPlantToUpdate.LocationLongitude = entity.LocationLongitude;

            await _context.SaveChangesAsync();

            return solarPowerPlantToUpdate;
        }

        public async Task<SolarPlant?> DeleteAsync(Guid id)
        {
            SolarPlant? targetEntity = await _context.SolarPlants.FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (targetEntity == null)
            {
                return null;
            }
            _context.SolarPlants.Remove(targetEntity);
            await _context.SaveChangesAsync();

            return targetEntity;
        }
    }
}
