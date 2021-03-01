using ProcessPayment;
using ProcessPaymentTask.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentTask.Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private RepositoryContext _repoContext;
        private UnitOfWork _unitOfWork;
        private ICardInformationRepository _card;
        private IPaymentStatusRepository _paymentStatus;
        public RepositoryContext RepositoryContext { get; set; }
        public UnitOfWork(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public ICardInformationRepository CardInformation
        {
            get { return _card = _card ?? new CardInformationRepository(_repoContext); }
        }

        public IPaymentStatusRepository PaymentStatus
        {
            get { return _paymentStatus = _paymentStatus ?? new PaymentStatusRepository(_repoContext); }
        }

        public void Commit()
        {
            _repoContext.SaveChanges();
        }
        public void Rollback()
        {
            _repoContext.Dispose();
        }
    }
}
