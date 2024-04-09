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
                    Id = Guid.NewGuid(),
                    Name = "pw1",
                    InstalledPower = 10,
                    DateOfInstallation = new DateTime(2022, 04, 01),
                    LocationLatitude = 45.815210F,
                    LocationLongitude = 15.890098F
                },
                new SolarPlant()
                {
                    Id = Guid.NewGuid(),
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
