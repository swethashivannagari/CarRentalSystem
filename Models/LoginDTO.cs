using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must have atleast 6 characters.")]
        public string Password { get; set; }
    }
}
