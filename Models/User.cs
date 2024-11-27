using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [MaxLength(100,ErrorMessage ="Name must be between 1 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Email is required.")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required.")]
        [StringLength(100,MinimumLength =6,ErrorMessage ="Password must have atleast 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Phone NUmber is required.")]
        [StringLength(10, ErrorMessage = "Phone Number should have 10 digits.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone Number must be exactly 10 digits.")]
        public string PhoneNumber {  get; set; }

        [Required(ErrorMessage ="Role is required.")]
        [RegularExpression("Admin|User" ,ErrorMessage ="Role must be either User or Admin.")]
        public string Role { get; set; }

    }
}
