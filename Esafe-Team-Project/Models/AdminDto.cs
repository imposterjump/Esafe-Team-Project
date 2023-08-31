using System.ComponentModel.DataAnnotations;

namespace Esafe_Team_Project.Models
{
    public class AdminDto
    {
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email address is invalid.")]
        public string Email { get; set; }

        [MaxLength(15, ErrorMessage = "Username should not exceed 10 characters")]
        [MinLength(6, ErrorMessage = "Username should be atleat 6 characters")]
        public string Username { get; set; }

        [MinLength(8, ErrorMessage = "Password should be atleast 8 characters.")]
        public string Password { get; set; }
    }
}
