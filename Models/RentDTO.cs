using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class RentDTO
    {
        [Required]
        public int CarId { get; set; }
        [Required]
        [Range(0, int.MaxValue,ErrorMessage ="Rent Days must be greater than 0")]
        public int days { get; set; }
    }
}
