using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentTask.Interface
{
    public interface IUnitOfWork
    {
        ICardInformationRepository CardInformation { get; }
        IPaymentStatusRepository PaymentStatus { get; }
        void Commit();
        void Rollback();
    }
}
