using System.ComponentModel.DataAnnotations;

namespace Esafe_Team_Project.Models.Address
{
    public class AddressDto
    {
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        public string? Street { get; set; }

        public AddressDto(string country, string city, string street)
        {
            if (street != null)
            {
                Street = street;
            }
            Country = country;
            City = city;
        }
    }
}
