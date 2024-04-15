namespace SolarPowerAPI.Models.DTOs.ProductionDTOs
{
    public class GetProductionDto
    {
        public long Id { get; set; }
        public double ProducedPower { get; set; }
        public DateTime ProductionDateTime { get; set; }
        public Guid SolarPowerPlantId { get; set; }
    }
}
