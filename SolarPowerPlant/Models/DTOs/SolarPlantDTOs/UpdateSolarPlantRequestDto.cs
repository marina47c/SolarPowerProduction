namespace SolarPowerAPI.Models.DTOs.SolarPlantDTOs
{
    public class UpdateSolarPlantRequestDto
    {
        public string? Name { get; set; }

        public double InstalledPower { get; set; }

        public DateTime DateOfInstallation { get; set; }

        public float LocationLongitude { get; set; }

        public float LocationLatitude { get; set; }
    }
}
