using Esafe_Team_Project.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Esafe_Team_Project.Entities
{
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        public CreditCardType CardType { get; set; }

        [MaxLength(16, ErrorMessage = "Credit Card number cannot exceed 13 digits")]
        [MinLength(13, ErrorMessage = "Credit Card must be atleast 13 digits")]

        public string CardNumber { get; set; }

        public string CVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double BalanceAvailable { get; set; }
        public double CardLimit { get; set; }
        public bool Accepted { get; set; } = false;
        public DateTime? ApplicationDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public int? ApprovedById { get; set; }

    }
}
