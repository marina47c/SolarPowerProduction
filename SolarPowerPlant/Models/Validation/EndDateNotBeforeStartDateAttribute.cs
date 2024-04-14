using SolarPowerAPI.Models.DTOs.ForecastDTOs;
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
                GetForecastRequestDto request = (GetForecastRequestDto)validationContext.ObjectInstance;
                DateTime startDate = request.StartDateTime;

                if (endDate < startDate)
                {
                    return new ValidationResult("End date cannot be before the start date.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
