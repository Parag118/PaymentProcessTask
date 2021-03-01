using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPaymentTask.Model
{
    [Table("PaymentStatus")]
    public class PaymentStatus
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        [ForeignKey(nameof(CardInformation))]
        public int CardId { get; set; }
        public virtual CardInformation CardInformation { get; set; }
    }
}
