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
        public bool accepted { get; set; } = false;
        public DateTime ApplicationDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public int? ApprovedById { get; set; }
        public CreditCard()
        {
            ApplicationDate = DateTime.Now;
        }


    }
}
