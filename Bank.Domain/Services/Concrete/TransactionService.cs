using Bank.Domain.Handlers.Abstract;
using Bank.Domain.Models;
using Bank.Domain.Providers;
using Bank.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Bank.Domain.Services.Concrete
{
    public class TransactionService : ITransactionService
    {
        public async Task<Result> GenerateCardRandomTransactionsAsync(int toCardID, IRepositoriesHandler repositoriesHandler, IResourcesProvider resourcesProvider)
        {
            var result = new Result();
            if (toCardID == 0)
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("CCIdNull");
                return result;
            }

            var cards = await repositoriesHandler.СreditCardRepository.GetCollectionForRandomTranAsync(toCardID);
            if (cards.Count == 0)
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("NotEnCards");
                return result;
            }

            int counter = 0;
            var transactionResult = new Result();
            foreach (var card in cards)
            {
                Random rnd = new Random();
                var transaction = new Domain.Models.Transaction();
                transaction.Amount = rnd.Next(1, (int)card.Amount);
                transaction.Comment = resourcesProvider.GetGeneralResource("RndTran");
                transaction.Date = DateTime.Now;
                transaction.FromCreditCardID = card.CreditCardID;
                transaction.ToCreditCardID = toCardID;
                transactionResult = await this.PerformTransactionAsync(transaction, repositoriesHandler, resourcesProvider);
                if (transactionResult.Success)
                {
                    counter++;
                }
            }

            if (counter > 0)
            {
                result.Success = true;
                result.SuccessMessage = resourcesProvider.GetGeneralResource("TransGenDone");
                return result;
            }
            else
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("TranAllCCOutOfFunds");
                return result;
            }
        }

        public async Task<Result> PerformTransactionAsync(Domain.Models.Transaction newTransaction, IRepositoriesHandler repositoriesHandler, IResourcesProvider resourcesProvider)
        {
            var result = new Result();
            result.Success = newTransaction.Amount > 0; 
            if (!result.Success)
            {
                result.Error = resourcesProvider.GetGeneralResource("IncAmount");
                return result;
            }

            var fromCreditCard = await repositoriesHandler.СreditCardRepository.GetByIdAsync(newTransaction.FromCreditCardID);
            result.Success = fromCreditCard.Amount > newTransaction.Amount; 
            if (!result.Success)
            {
                result.Error = resourcesProvider.GetGeneralResource("TranOutOfFunds");
                return result;
            }

            var toCreditCard = await repositoriesHandler.СreditCardRepository.GetByIdAsync(newTransaction.ToCreditCardID);
            if (fromCreditCard.CreditCardID == toCreditCard.CreditCardID)
            {
                result.Success = false;
                result.Error = resourcesProvider.GetGeneralResource("SelectDifCards");
                return result;
            }

            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    fromCreditCard.Amount -= newTransaction.Amount;
                    await repositoriesHandler.СreditCardRepository.UpdateAsync(fromCreditCard);
                    toCreditCard.Amount += newTransaction.Amount;
                    await repositoriesHandler.СreditCardRepository.UpdateAsync(toCreditCard);
                    newTransaction.Date = DateTime.Now;
                    await repositoriesHandler.TransactionRepository.InsertAsync(newTransaction);
                    scope.Complete();
                }

                result.Success = true;
                result.SuccessMessage = resourcesProvider.GetGeneralResource("TranOk");
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = $"{resourcesProvider.GetGeneralResource("TranFail")} {ex.Message}.";
                return result;
            }
        }
    }
}