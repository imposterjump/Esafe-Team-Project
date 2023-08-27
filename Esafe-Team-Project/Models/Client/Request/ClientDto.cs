using Esafe_Team_Project.Models.Address;
using System.ComponentModel.DataAnnotations;

namespace Esafe_Team_Project.Models.Client.Request
{
    public class ClientDto
    {
        [MaxLength(14, ErrorMessage = "National ID must be 14 numbers.")]
        [Required(ErrorMessage = "National ID is required.")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email address is invalid.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Phone Number is invalid.")]
        public string PhoneNumber { get; set; }

        [MaxLength(15, ErrorMessage = "Username should not exceed 15 characters")]
        [MinLength(6, ErrorMessage = "Username should be atleat 6 characters")]
        public string Username { get; set; }

        [MinLength(8, ErrorMessage = "Password should be atleast 8 characters.")]

        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Birthdate is required.")]
        public DateTime Birthdate { get; set; }
        public List<AddressDto>? ClientAddresses { get; set; } = new List<AddressDto>();

    }
}
