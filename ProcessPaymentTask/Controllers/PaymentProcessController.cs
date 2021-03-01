using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessPaymentTask.BusinessAccess;
using ProcessPaymentTask.DTO;
using ProcessPaymentTask.Interface;
using ProcessPaymentTask.Model;

namespace ProcessPaymentTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentProcessController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private ICheapPaymentGateway _cheapPaymentGateway=null;
        private IExpensivePaymentGateway _expensivePaymentGateway = null;
        private IPreminumServiceGateway _preminumServiceGateway = null;
        public PaymentProcessController(IRepositoryWrapper repoWrapper, IMapper mapper, IUnitOfWork unitOfWork,
            ICheapPaymentGateway cheapPaymentGateway,IExpensivePaymentGateway expensivePaymentGateway,IPreminumServiceGateway preminumServiceGateway)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _preminumServiceGateway = preminumServiceGateway;

        }

        [HttpPost]
        public HttpResponseMessage Post(CardInformationDTO card)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    PaymentProcessService payment = new PaymentProcessService(_repoWrapper, _mapper, _unitOfWork,_cheapPaymentGateway,_expensivePaymentGateway,_preminumServiceGateway);
                    int status = payment.PaymentProcessBusinessLogic(card);
                    if (status == 200)
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    else if (status == 400)
                        return new HttpResponseMessage(HttpStatusCode.BadRequest);
                    else
                        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

        }
    }
}