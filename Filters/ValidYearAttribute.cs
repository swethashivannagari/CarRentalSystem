using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Filters
{
    public class ValidYearAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,ValidationContext context)
        {
           if(value is int year)
            {
                int currentYear = DateTime.Now.Year;
                if(year>=1900 && year <= currentYear)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"Year must be between 1900 and {currentYear}.");
                }
            }
            return new ValidationResult("Invalid year value");
        }
    }
}
