using Realms.Sync;
using System.ComponentModel.DataAnnotations;

namespace Esafe_Team_Project.Entities
{
    public class Client : User
    {



        [MaxLength(14, ErrorMessage = "National ID must be 14 numbers.")]
        [Required(ErrorMessage = "National ID is required.")]
        public string NationalId { get; set; }

        [Phone(ErrorMessage = "Phone Number is invalid.")]
        public string PhoneNumber { get; set; }
        public string? AccountNo { get; set; }
        public DateTime Birthdate { get; set; }
        public double balance { get; set; } = 0;
        public bool Blocked { get; set; } = false;
        public List<Address>? ClientAddresses { get; set; } = new List<Address>();
        public List<Certificate >? ClientCertificates { get; set; } = new List<Certificate>();
        public List<CreditCard>? ClientCreditCards { get; set; } = new List<CreditCard>();
        public List<Transfer>? ClientTransfers { get; set; } = new List<Transfer>();
    }
}
