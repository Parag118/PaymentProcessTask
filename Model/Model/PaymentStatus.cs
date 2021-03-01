using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    [Table("PaymentStatus")]
    public class PaymentStatus
    {
        public string Status { get; set; }
        [ForeignKey(nameof(CardInformation))]
        public int CardId { get; set; }
        public CardInformation CardInformation { get; set; }
    }
}
