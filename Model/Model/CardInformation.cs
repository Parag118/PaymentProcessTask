using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    [Table("CardInformation")]
    public class CardInformation
    {
        [Key]
        public int CardId { get; set; }
        [Required]
        [StringLength(12)]
        public int CreditCardNumber { get; set; }
        [Required]
        public string CardHolder { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [StringLength(3)]
        public int SecurityCode { get; set; }
        [Required]
        public float Amount { get; set; }
    }
}
