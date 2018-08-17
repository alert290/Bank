using Bank.Domain.Services.Abstract;
using Bank.Domain.Services.Concrete;
using Ninject.Modules;

namespace Bank.Config
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPasswordService>().To<PasswordService>();
            Bind<ICustomerService>().To<CustomerService>();
            Bind<ICreditCardService>().To<CreditCardService>();
            Bind<ITransactionService>().To<TransactionService>();
        }
    }
}