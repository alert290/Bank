using Bank.Domain.Services.Abstract;
using System;

namespace Bank.Domain.Handlers.Abstract
{
    public interface IServiceHandler
    {
        IPasswordService PasswordService { get; }

        ICustomerService CustomerService { get; }

        ICreditCardService CreditCardService { get; }

        ITransactionService TransactionService { get; }
    }
}
