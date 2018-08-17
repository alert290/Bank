using AutoMapper;

namespace Bank.Config.AutoMapper
{
    public class CreditCardProfile : Profile
    {
        public CreditCardProfile()
        {
            CreateMap<DAL.Models.CreditCard, Domain.Models.CreditCard>()
                .ReverseMap()
                .ForMember(x => x.Customer, y => y.Ignore())
                .ForMember(x => x.TransactionsFrom, y => y.Ignore())
                .ForMember(x => x.TransactionsTo, y => y.Ignore());
        }
    }
}