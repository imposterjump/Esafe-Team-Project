using System.ComponentModel.DataAnnotations;

namespace Esafe_Team_Project.Entities
{
    public class Transfer
    {
        [Key] public int Id { get; set; }
        [Required] public int SenderId { get; set; }
        [Required] public int RecieverId { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Only numbers are allowed.")]
        public double Amount { get; set; }

        public DateTime Date { get; set; }
        public Transfer()
        {
            Date = DateTime.Now;
        }
    }
}
