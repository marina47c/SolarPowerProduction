using SolarPowerAPI.Models.Entities;
using System.Collections.Generic;

namespace SolarPowerAPI.Mock
{
    public class SolarPlantsMock
    {
        public List<SolarPlant> CreateSolarPlantsMock()
        {
            List<SolarPlant> solarPlants = new()
            {
                new SolarPlant()
                {
                    Id = Guid.Parse("E5EEC96F-4970-43E8-A365-73A81277DB8A"),
                    Name = "pw1",
                    InstalledPower = 10,
                    DateOfInstallation = new DateTime(2022, 04, 01),
                    LocationLatitude = 45.815210F,
                    LocationLongitude = 15.890098F
                },
                new SolarPlant()
                {
                    Id = Guid.Parse("DB496C9B-6288-4625-A266-86D8E7C1ECD2"),
                    Name = "pw2",
                    InstalledPower = 4,
                    DateOfInstallation = new DateTime(2022, 04, 01),
                    LocationLatitude = 45.815210F,
                    LocationLongitude = 15.890098F
                }
            };

            return solarPlants;
        }
    }
}
