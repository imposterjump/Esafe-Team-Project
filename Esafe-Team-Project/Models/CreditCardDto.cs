using Esafe_Team_Project.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Esafe_Team_Project.Models
{
    public class CreditCardDto
    {
        public int ClientId { get; set; }
        [RegularExpression("^(Silver|Gold|Platinum)$", ErrorMessage = "Valid credit card types are Silver or Gold or Platinum.")]
        public string CardType { get; set; }

        [MaxLength(16, ErrorMessage = "Credit Card number cannot exceed 13 numbers")]
        [MinLength(13, ErrorMessage = "Credit Card must be atleast 13 numbers")]
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double BalanceAvailable { get; set; }
        public double CardLimit { get; set; }

        public bool Accepted { get; set; }
    }
}
