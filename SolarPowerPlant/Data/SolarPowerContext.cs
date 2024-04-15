using Microsoft.EntityFrameworkCore;
using SolarPowerAPI.Mock;
using SolarPowerAPI.Models.Entities;

namespace SolarPowerAPI.Data
{
    public class SolarPowerContext: DbContext
    {
        private readonly SolarPlantsMock _solarPlantsMock;
        private readonly ProductionMock _productionMock;

        public SolarPowerContext(DbContextOptions<SolarPowerContext> options, SolarPlantsMock solarPlantsMock, ProductionMock productionMock)
            : base(options)
        {
            _solarPlantsMock = solarPlantsMock;
            _productionMock = productionMock;
        }

        public DbSet<SolarPlant> SolarPlants { get; set; }
        public DbSet<Production> Production { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DateTime productionStartDate = new DateTime(2024, 3, 1);
            DateTime productionEndDate = DateTime.Now;

            // Seed data for solar power plants
            List<SolarPlant> solarPlants = _solarPlantsMock.CreateSolarPlantsMock();
            modelBuilder.Entity<SolarPlant>().HasData(solarPlants);

            // Seed data for power production
            List<Production> mockProductionForAllPlants = new();
            foreach (SolarPlant plantMock in solarPlants)
            {
                List<Production> mockProductionsForOnePlant =
                    _productionMock.CreateMockPowerProduction(plantMock.Id, plantMock.InstalledPower, productionStartDate, productionEndDate);
                mockProductionForAllPlants.AddRange(mockProductionsForOnePlant);
            }

            modelBuilder.Entity<Production>().HasData(mockProductionForAllPlants);
        }
    }
}
