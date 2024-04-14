using SolarPowerAPI.Enums;
using SolarPowerAPI.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace SolarPowerAPI.Models.DTOs.ForecastDTOs
{
    public class GetForecastRequestDto
    {
        [Required]
        public Guid SolarPlantId { get; set; }

        [DateNotInPast]
        public DateTime StartDateTime { get; set; } = DateTime.Now;

        [Required]
        [DateNotInPast]
        [DateNotMoreThanSixDaysFromNow]
        [EndDateNotBeforeStartDate]
        public DateTime EndDateTime { get; set; }

        [Required]
        public GranularityLevel GranularityLevel { get; set; }
    }
}
