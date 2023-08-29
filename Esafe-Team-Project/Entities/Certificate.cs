using Esafe_Team_Project.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Esafe_Team_Project.Entities
{
    public class Certificate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        public Client?  client{ get; set; }
        public CertificateType CertificateType { get; set; }
        public int InterestPercentage { get; set; }
        public bool Accepted { get; set; } = false;
        public DateTime ApplicationDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public int? ApprovedById { get; set; }
    }
}
