using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Config.AutoMapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<DAL.Models.Customer, Domain.Models.Customer>()
                .ReverseMap()
                .ForMember(x => x.CreditCards, y => y.Ignore());
        }
    }
}