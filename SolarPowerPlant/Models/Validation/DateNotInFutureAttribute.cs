using System.ComponentModel.DataAnnotations;

namespace SolarPowerAPI.Models.Validation
{
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dateValue = (DateTime)value;
                if (dateValue > DateTime.Now)
                {
                    return new ValidationResult("Date cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
