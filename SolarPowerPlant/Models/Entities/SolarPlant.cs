using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolarPowerAPI.Models.Entities
{
    public class SolarPlant
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        [Required]
        public int InstalledPower { get; set; }

        [Required]
        public DateTime DateOfInstallation { get; set; }

        [Required]
        public float LocationLongitude { get; set; }

        [Required]
        public float LocationLatitude { get; set; }
    }
}
