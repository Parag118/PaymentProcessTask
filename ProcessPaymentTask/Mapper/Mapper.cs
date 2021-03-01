using AutoMapper;
using ProcessPaymentTask.DTO;
using ProcessPaymentTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentTask.Mapper
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<CardInformationDTO, CardInformation>();
            CreateMap<PaymentStatusDTO, PaymentStatus>();
        }
    }
}
