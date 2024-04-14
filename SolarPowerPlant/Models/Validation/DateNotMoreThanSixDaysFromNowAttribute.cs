using System.ComponentModel.DataAnnotations;

namespace SolarPowerAPI.Models.Validation
{
    public class DateNotMoreThanSixDaysFromNowAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dateValue = (DateTime)value;
                if (dateValue > DateTime.Now.AddDays(6))
                {
                    return new ValidationResult("The date can not be more than 6 days in the future.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
