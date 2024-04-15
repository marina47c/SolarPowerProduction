using SolarPowerAPI.Models.DTOs.ForecastDTOs;
using SolarPowerAPI.Models.DTOs.ProductionDTOs;
using System.ComponentModel.DataAnnotations;

namespace SolarPowerAPI.Models.Validation
{
    public class EndDateNotBeforeStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime endDate = (DateTime)value;
                DateTime startDate = DateTime.MinValue;

                if (validationContext.ObjectInstance is GetForecastRequestDto)
                {
                    GetForecastRequestDto request = (GetForecastRequestDto)validationContext.ObjectInstance;
                    startDate = request.StartDateTime;
                }
                else if (validationContext.ObjectInstance is GetProductionRequestDto)
                {
                    GetProductionRequestDto request = (GetProductionRequestDto)validationContext.ObjectInstance;
                    startDate = request.StartDateTime;
                }

                if (endDate < startDate)
                {
                    return new ValidationResult("End date cannot be before the start date.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
