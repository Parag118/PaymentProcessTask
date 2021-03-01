using ProcessPaymentTask;
using ProcessPaymentTask.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private ICardInformationRepository _card;
        private IPaymentStatusRepository _paymentStatus;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;

        }
        public ICardInformationRepository CardInformation
        {
            get
            {
                if (_card == null)
                {
                    _card = new CardInformationRepository(_repoContext);
                }
                return _card;
            }
        }

        public IPaymentStatusRepository PaymentStatus
        {
            get
            {
                if (_paymentStatus == null)
                {
                    _paymentStatus = new PaymentStatusRepository(_repoContext);
                }
                return _paymentStatus;
            }
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
