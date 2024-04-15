namespace SolarPowerAPI.Models.DTOs.ForecastDTOs
{
    public class GetForecastDto
    {
        public int Id { get; set; }
        public double ForcastedPower { get; set; }
        public DateTime ProductionDateTime { get; set; }
        public Guid SolarPowerPlantId { get; set; }
    }
}
