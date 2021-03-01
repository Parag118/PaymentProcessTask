using ProcessPaymentTask;
using ProcessPaymentTask.Interface;
using ProcessPaymentTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment
{
    public class PaymentStatusRepository:RepositoryBase<PaymentStatus>,IPaymentStatusRepository
    {
        public PaymentStatusRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {

        }
    }
}
