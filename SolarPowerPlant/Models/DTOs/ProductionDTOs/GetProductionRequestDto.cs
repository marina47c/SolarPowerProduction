﻿using SolarPowerAPI.Enums;
using SolarPowerAPI.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace SolarPowerAPI.Models.DTOs.ProductionDTOs
{
    public class GetProductionRequestDto
    {
        [Required]
        public Guid SolarPlantId { get; set; }

        [Required]
        [DateNotInFuture]
        public DateTime StartDateTime { get; set; }

        [Required]
        [DateNotInFuture]
        [EndDateNotBeforeStartDate]
        public DateTime EndDateTime { get; set; }

        [Required]
        public GranularityLevel GranularityLevel { get; set; }
    }
}
