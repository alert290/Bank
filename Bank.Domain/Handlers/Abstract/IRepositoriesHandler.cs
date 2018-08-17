using Bank.Domain.Repositories;
using System;

namespace Bank.Domain.Handlers.Abstract
{
    public interface IRepositoriesHandler : IDisposable
    {
        ICustomerRepository СustomerRepository { get; }

        ICreditCardRepository СreditCardRepository { get; }

        ITransactionRepository TransactionRepository { get; }
    }
}
