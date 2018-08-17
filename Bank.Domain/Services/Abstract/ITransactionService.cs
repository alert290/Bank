using Bank.Domain.Handlers.Abstract;
using Bank.Domain.Models;
using Bank.Domain.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Domain.Services.Abstract
{
    public interface ITransactionService
    {
        Task<Result> GenerateCardRandomTransactionsAsync(int toCardID, IRepositoriesHandler repositoriesHandler, IResourcesProvider resourcesProvider);
        Task<Result> PerformTransactionAsync(Transaction newTransaction, IRepositoriesHandler repositoriesHandler, IResourcesProvider resourcesProvider);
    }
}
