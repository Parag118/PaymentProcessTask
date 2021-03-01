using ProcessPaymentTask.DTO;
using ProcessPaymentTask.Interface;
using ProcessPaymentTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentTask.Repository.Common
{
    public class PreminumServiceGateway: IPreminumServiceGateway
    {
        public async Task<int> ProcessPayment(CardInformation card)
        {
            await Task.Delay(10000);
            return 200;
        }
    }
}
