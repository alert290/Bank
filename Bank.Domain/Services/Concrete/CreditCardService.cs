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
    public class CreditCardService : ICreditCardService
    {
        public Result CreateNewCreditCard(CreditCard newCreditCard, IRepositoriesHandler repositoriesHandler, IPasswordService passwordService, IResourcesProvider resourcesProvider)
        {
            var result = new Result();
            var isCardNumExist = repositoriesHandler.СreditCardRepository.GetCollection().Any(x => x.CardNumber == newCreditCard.CardNumber);
            if (isCardNumExist)
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("CCNumExist");
                return result;
            }

            if (!IsCreditCardDataValid(newCreditCard.CardNumber, newCreditCard.PIN))
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("NoNumPin");
                return result;
            }

            newCreditCard.PIN = passwordService.HashPassword(newCreditCard.PIN);
            newCreditCard.DateOfCreation = DateTime.Now;
            repositoriesHandler.СreditCardRepository.Insert(newCreditCard);
            result.Success = true;
            result.SuccessMessage = resourcesProvider.GetGeneralResource("CCCreated");
            return result;
        }

        public bool IsCreditCardDataValid(string cardNumber, string pin)
        {
            if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(pin))
            {
                return false;
            }

            return true;
        }

        public Result UpdateCreditCard(EditedCreditCard editedCreditCard, IRepositoriesHandler repositoriesHandler, IPasswordService passwordService, IResourcesProvider resourcesProvider)
        {
            var result = new Result();
            var isCardNumExist = repositoriesHandler.СreditCardRepository.GetCollection().Any(x => x.CardNumber == editedCreditCard.CardNumber && x.CreditCardID != editedCreditCard.CreditCardID);
            if (isCardNumExist)
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("CCNumExist");
                return result;
            }

            if (!IsCreditCardDataValid(editedCreditCard.CardNumber, editedCreditCard.CurrentPIN))
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("NoNumPin");
                return result;
            }

            var storedCreditCard = repositoriesHandler.СreditCardRepository.GetById(editedCreditCard.CreditCardID);
            if (passwordService.ComparePasswords(editedCreditCard.CurrentPIN, storedCreditCard.PIN))
            {
                storedCreditCard.CardNumber = editedCreditCard.CardNumber;
                storedCreditCard.Amount = editedCreditCard.Amount;
                storedCreditCard.PIN = string.IsNullOrEmpty(editedCreditCard.NewPIN) ? storedCreditCard.PIN : passwordService.HashPassword(editedCreditCard.NewPIN);
                repositoriesHandler.СreditCardRepository.Update(storedCreditCard);
                result.Success = true;
                result.SuccessMessage = resourcesProvider.GetGeneralResource("CCUpdt");
                return result;
            }
            else
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("PinsDontMatch");
                return result;
            }
            
        }
    }
}