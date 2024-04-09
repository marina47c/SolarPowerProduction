using SolarPowerAPI.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolarPowerAPI.Models.DTOs.ProductionDTOs
{
    public class GetProductionDto
    {
        public double ProducedPower { get; set; }
        public DateTime ProductionDateTime { get; set; }
        public Guid SolarPowerPlantId { get; set; }
    }
}
