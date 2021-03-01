using AutoMapper;
using ProcessPayment;
using ProcessPaymentTask.DTO;
using ProcessPaymentTask.Interface;
using ProcessPaymentTask.Model;
using ProcessPaymentTask.Repository.Common;
using ProcessPaymentTask.Repository.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProcessPaymentTask.BusinessAccess
{
    public class PaymentProcessService
    {
        private readonly IMapper _mapper;
        private IRepositoryWrapper _repoWrapper;
        private IUnitOfWork _unitOfWork;
        private ICheapPaymentGateway _cheapPaymentGateway;
        private IExpensivePaymentGateway _expensivePaymentGateway;
        private IPreminumServiceGateway _preminumServiceGateway;
        public PaymentProcessService(IRepositoryWrapper repoWrapper, IMapper mapper, IUnitOfWork unitOfWork,
            ICheapPaymentGateway cheapPaymentGateway, IExpensivePaymentGateway expensivePaymentGateway, IPreminumServiceGateway preminumServiceGateway)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _preminumServiceGateway = preminumServiceGateway;
        }

        private void Save(CardInformation card, PaymentStatus payment)
        {

            var result = _repoWrapper.CardInformation.FindByCondition(a => a.CreditCardNumber == card.CreditCardNumber).FirstOrDefault();

            int cardid = result == null ? 0 : result.CardId;
            if (cardid > 0)
            {
                try
                {

                    _unitOfWork.CardInformation.Update(result);
                    var result1 = _repoWrapper.PaymentStatus.FindByCondition(a => a.CardId == cardid).FirstOrDefault();
                    result1.Status = payment.Status;
                    _unitOfWork.PaymentStatus.Update(result1);
                    _unitOfWork.Commit();

                }
                catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                    throw;
                }


            }
            else
            {
                try
                {
                    _unitOfWork.CardInformation.Create(card);
                    payment.CardId = card.CardId;
                    payment.CardInformation = card;
                    _unitOfWork.PaymentStatus.Create(payment);
                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                    throw;
                }

            }

        }

        public int PaymentProcessBusinessLogic(CardInformationDTO cardModel)
        {
            try
            {

                PaymentStatusDTO payment = new PaymentStatusDTO();
                var cardTable = _mapper.Map<CardInformation>(cardModel);
                var paymentTable = _mapper.Map<PaymentStatus>(payment);
                if (Common.isValid((long.Parse(cardTable.CreditCardNumber))))
                {
                    if (cardTable.ExpirationDate.Year >= DateTime.Now.Year && cardTable.ExpirationDate.Month >= DateTime.Now.Month)
                    {

                        Task<int> finishedTask = null;

                        if (cardModel.Amount <= 20)
                        {

                            Task<int> finishedTask1 = Task.Run(() => _cheapPaymentGateway.ProcessPayment(cardTable));

                            if (!finishedTask1.IsCompleted)
                            {
                                int result = finishedTask1.Result;
                                paymentTable.Status = "Processed";
                                Save(cardTable, paymentTable);
                                return 200;
                            }

                        }
                        else if (cardModel.Amount >= 21 && cardModel.Amount <= 500)
                        {

                            finishedTask = Task.Run(() => _expensivePaymentGateway.ProcessPayment(cardTable));

                            if (!finishedTask.IsCompleted)
                            {
                                Task<int> finishedTask2 = Task.Run(() => _cheapPaymentGateway.ProcessPayment(cardTable));
                                int result = finishedTask2.Result;
                                paymentTable.Status = "Pending";
                                Save(cardTable, paymentTable);
                                return 400;
                            }
                            else
                            {
                                paymentTable.Status = "Processed";
                                Save(cardTable, paymentTable);
                                return 200;
                            }
                        }
                        else if (cardModel.Amount > 500)
                        {

                            var attempts = 3;
                            do
                            {
                                try
                                {
                                    attempts++;
                                    finishedTask = Task.Run(() => _preminumServiceGateway.ProcessPayment(cardTable));
                                    if (finishedTask.IsCompleted)
                                    {
                                        paymentTable.Status = "Processed";
                                        Save(cardTable, paymentTable);
                                        break;

                                    }

                                }
                                catch (Exception ex)
                                {
                                    paymentTable.Status = "Failed";
                                    Save(cardTable, paymentTable);
                                    if (attempts == 3)
                                        throw;
                                    return 500;
                                }
                            } while (true);
                            return 200;
                        }


                    }
                    else
                    {
                        paymentTable.Status = "Pending";
                        Save(cardTable, paymentTable);
                        return 400;
                    }

                }
                else
                {
                    paymentTable.Status = "Pending";
                    Save(cardTable, paymentTable);
                    return 400;

                }

                return 200;
            }
            catch (Exception)
            {
                return 500;
            }

        }

    }
}
