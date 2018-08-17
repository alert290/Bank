using Bank.Domain.Handlers.Abstract;
using Bank.Domain.Models;
using Bank.Domain.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Services.Abstract
{
    public interface ICreditCardService
    {
        Result CreateNewCreditCard(CreditCard newCreditCard, IRepositoriesHandler repositoriesHandler, IPasswordService passwordService, IResourcesProvider resourcesProvider);

        bool IsCreditCardDataValid(string cardNumber, string pin);

        Result UpdateCreditCard(EditedCreditCard editedCreditCard, IRepositoriesHandler repositoriesHandler, IPasswordService passwordService, IResourcesProvider resourcesProvider);
    }
}
