using Bank.Domain.Handlers.Abstract;
using Bank.Domain.Repositories;

namespace Bank.Domain.Handlers.Concrete
{
    public class RepositoriesHandler : IRepositoriesHandler
    {
        public RepositoriesHandler(ICustomerRepository customerRepository,
            ICreditCardRepository creditCardRepository,
            ITransactionRepository transactionRepository)
        {
            this.СustomerRepository = customerRepository;
            this.СreditCardRepository = creditCardRepository;
            this.TransactionRepository = transactionRepository;
        }

        public ICustomerRepository СustomerRepository { get; private set; }

        public ICreditCardRepository СreditCardRepository { get; private set; }

        public ITransactionRepository TransactionRepository { get; private set; }

        public void Dispose()
        {
            this.СustomerRepository.Dispose();
            this.СreditCardRepository.Dispose();
            this.TransactionRepository.Dispose();
        }
    }
}