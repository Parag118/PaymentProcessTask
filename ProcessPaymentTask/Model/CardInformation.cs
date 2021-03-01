using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPaymentTask.Model
{
    [Table("CardInformation")]
    public class CardInformation
    {
        [Key]
        public int CardId { get; set; }
        [Required]
        [MaxLength(20)]
        public string CreditCardNumber { get; set; }
        [Required]
        public string CardHolder { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [StringLength(3)]
        [MinLength(3)]
        public string SecurityCode { get; set; }
        [Required]
        public float Amount { get; set; }
    }
}
