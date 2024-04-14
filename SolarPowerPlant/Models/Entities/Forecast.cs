using System.ComponentModel.DataAnnotations.Schema;

namespace SolarPowerAPI.Models.Entities
{
    public class Forecast
    {
        public int Id { get; set; }
        public double ForcastedPower { get; set; }
        public DateTime ProductionDateTime { get; set; }
        public Guid SolarPowerPlantId { get; set; }
    }
}
