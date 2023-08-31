using Esafe_Team_Project.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Esafe_Team_Project.Models
{
    public class CreditCardReqDto
    {
        [RegularExpression("^(Silver|Gold|Platinum)$", ErrorMessage = "Valid credit card types are Silver or Gold or Platinum.")]
        public string CardType { get; set; }
    }
}