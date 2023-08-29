using Esafe_Team_Project.Entities;
using Esafe_Team_Project.Models.Address;

namespace Esafe_Team_Project.Models.Client.Response
{
    public class ClientDisplayDto
    {
        public string NationalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AccountNo { get; set; }
        public List<AddressDto>? ClientAddresses { get; set; } = new List<AddressDto>();
        
    }
}
