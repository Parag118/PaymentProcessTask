using ProcessPaymentTask.DTO;
using ProcessPaymentTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentTask.Interface
{
   public interface IExpensivePaymentGateway
    {
        //This method pass the card infornatiom to Payment Gateway
        public Task<int> ProcessPayment(CardInformation card);
    }
}
