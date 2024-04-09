using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolarPowerAPI.Models.Entities
{
    public class Production
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public double ProducedPower { get; set; }
        public DateTime ProductionDateTime { get; set; }

        [ForeignKey("SolarPowerPlant")]
        public Guid SolarPowerPlantId { get; set; }
        public SolarPlant? SolarPowerPlant { get; set; }
    }
}
