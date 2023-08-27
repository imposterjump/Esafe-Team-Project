using Esafe_Team_Project.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Esafe_Team_Project.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
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

        public Role Role { get; set; }
    }
}
