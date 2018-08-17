using AutoMapper;

namespace Bank.Config.AutoMapper
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<DAL.Models.Transaction, Domain.Models.Transaction>()
                .ForMember(x => x.FromCreditCardNumber, opt => opt.MapFrom(y => y.FromCreditCard.CardNumber))
                .ForMember(x => x.ToCreditCardNumber, opt => opt.MapFrom(y => y.ToCreditCard.CardNumber))
                .ReverseMap()
                .ForMember(x => x.FromCreditCard, y => y.Ignore())
                .ForMember(x => x.ToCreditCard, y => y.Ignore());
        }
    }
}