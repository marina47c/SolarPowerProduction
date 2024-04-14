using System.ComponentModel.DataAnnotations;

namespace SolarPowerAPI.Models.DTOs.SolarPlantDTOs
{
    public class AddSolarPlantRequestDto
    {
        public string? Name { get; set; }

        [Required]
        public double InstalledPower { get; set; }

        [Required]
        public DateTime DateOfInstallation { get; set; }

        [Required]
        public float LocationLongitude { get; set; }

        [Required]
        public float LocationLatitude { get; set; }
    }
}
