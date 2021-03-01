using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentTask.Interface
{
    public interface IRepositoryWrapper
    {
        ICardInformationRepository CardInformation { get; }
        IPaymentStatusRepository PaymentStatus { get; }
        void Save();
    }
}
