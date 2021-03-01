using ProcessPaymentTask;
using ProcessPaymentTask.Interface;
using ProcessPaymentTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment
{
    public class CardInformationRepository : RepositoryBase<CardInformation>, ICardInformationRepository
    {
        public CardInformationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
