using Bank.Domain.Handlers.Abstract;
using Bank.Domain.Models;
using Bank.Domain.Providers;
using Bank.Domain.Repositories;
using Bank.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Domain.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        public Result CreateNewCustomer(Customer newCustomer, IRepositoriesHandler repositoriesHandler, IPasswordService passwordService, IResourcesProvider resourcesProvider)
        {
            var result = new Result();
            var isLoginExist = repositoriesHandler.СustomerRepository.GetCollection().Any(x => x.Login == newCustomer.Login);
            if (isLoginExist)
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("LogExist");
                return result;
            }

            if (!IsCutomerDataValid(newCustomer.Login, newCustomer.Password))
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("NoLogPass");
                return result;
            }

            newCustomer.Password = passwordService.HashPassword(newCustomer.Password);
            newCustomer.DateOfRegistration = DateTime.Now;
            repositoriesHandler.СustomerRepository.Insert(newCustomer);
            result.Success = true;
            result.SuccessMessage = resourcesProvider.GetGeneralResource("CusCreat");
            return result;
        }

        public bool IsCutomerDataValid(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            return true;
        }

        public Result UpdateCustomer(EditedCustomer editedCustomer, IRepositoriesHandler repositoriesHandler, IPasswordService passwordService, IResourcesProvider resourcesProvider)
        {
            var result = new Result();
            var isLoginExist = repositoriesHandler.СustomerRepository.GetCollection().Any(x => x.Login == editedCustomer.Login && x.CustomerID != editedCustomer.CustomerID);
            if (isLoginExist)
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("LogExist");
                return result;
            }

            if (editedCustomer.CustomerID == 0)
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("CusIdNull");
                return result;
            }

            if (!IsCutomerDataValid(editedCustomer.Login, editedCustomer.CurrentPassword))
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("NoLogPass");
                return result;
            }

            var storedCustomer = repositoriesHandler.СustomerRepository.GetById(editedCustomer.CustomerID);
            if (passwordService.ComparePasswords(editedCustomer.CurrentPassword, storedCustomer.Password))
            {
                storedCustomer.CustomerID = editedCustomer.CustomerID;
                storedCustomer.FirstName = editedCustomer.FirstName;
                storedCustomer.LastName = editedCustomer.LastName;
                storedCustomer.Login = editedCustomer.Login;
                storedCustomer.Password = string.IsNullOrEmpty(editedCustomer.NewPassword) ? storedCustomer.Password : passwordService.HashPassword(editedCustomer.NewPassword);
                repositoriesHandler.СustomerRepository.Update(storedCustomer);
                result.Success = true;
                result.SuccessMessage = resourcesProvider.GetGeneralResource("CusUpdt");
                return result;
            }
            else
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("PassDontMatch");
                return result;
            }
        }
    }
}