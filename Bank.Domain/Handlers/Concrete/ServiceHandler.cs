using Bank.Domain.Handlers.Abstract;
using Bank.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Domain.Handlers.Concrete
{
    public class ServiceHandler : IServiceHandler
    {
        public ServiceHandler(IPasswordService passwordService,
            ICustomerService customerService,
            ICreditCardService creditCardService,
            ITransactionService transactionService)
        {
            this.PasswordService = passwordService;
            this.CustomerService = customerService;
            this.CreditCardService = creditCardService;
            this.TransactionService = transactionService;
        }

        public IPasswordService PasswordService { get; private set; }

        public ICustomerService CustomerService { get; private set; }

        public ICreditCardService CreditCardService { get; private set; }

        public ITransactionService TransactionService { get; private set; }
    }
}