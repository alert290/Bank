using Bank.DAL.Repositories;
using Bank.Domain.Repositories;
using Ninject.Modules;

namespace Bank.Config
{
    public class RepositoriesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITransactionRepository>().To<TransactionRepository>();
            Bind<ICreditCardRepository>().To<CreditCardRepository>();
            Bind<ICustomerRepository>().To<CustomerRepository>();
        }
    }
}