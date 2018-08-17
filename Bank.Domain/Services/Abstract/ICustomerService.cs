using Bank.Domain.Handlers.Abstract;
using Bank.Domain.Models;
using Bank.Domain.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Domain.Services.Abstract
{
    public interface ICustomerService
    {
        Result CreateNewCustomer(Customer newCustomer, IRepositoriesHandler repositoriesHandler, IPasswordService passwordService, IResourcesProvider resourcesProvider);

        Result UpdateCustomer(EditedCustomer editedCustomer, IRepositoriesHandler repositoriesHandler, IPasswordService passwordService, IResourcesProvider resourcesProvider);

        bool IsCutomerDataValid(string login, string password);
    }
}