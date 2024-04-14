using System.ComponentModel.DataAnnotations;

namespace SolarPowerAPI.Models.Validation
{
    public class DateNotInPastAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dateValue = (DateTime)value;
                if (DateTime.Now > dateValue)
                {
                    return new ValidationResult("Date can not be in the past.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
