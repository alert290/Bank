using Bank.Domain.Models;
using Bank.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Domain.Repositories
{
    public interface ITransactionRepository : IEntityRepository<Transaction>, IDisposableRepository
    {
        IEnumerable<Transaction> GetTransactionsByCard(SearchFilter searchfilter);

        Task<int> InsertAsync(Transaction entity);
    }
}