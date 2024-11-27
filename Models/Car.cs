using System.ComponentModel.DataAnnotations;
using CarRentalSystem.Filters;

namespace CarRentalSystem.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Make is required")]
        [StringLength(50,ErrorMessage ="Make must be between 1 to 50 characters")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [StringLength(50, ErrorMessage = "Model must be between 1 to 50 characters")]
        public string Model {  get; set; }

        [ValidYear]
        public int Year {  get; set; }

        [Required(ErrorMessage ="price per day is Required")]
        [Range(1,double.MaxValue,ErrorMessage ="Price per day must be greater than 0")]
        public decimal PricePerDay {  get; set; }

        public bool isAvailable { get; set; } = true;
    }
}
