using Bank.Domain.Models;
using Bank.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Domain.Repositories
{
    public interface ICreditCardRepository : IEntityRepository<CreditCard>, IDisposableRepository
    {
        IEnumerable<CreditCard> GetByCustomerId(int id);

        Task<CreditCard> GetByIdAsync(int id);

        Task<List<CreditCard>> GetCollectionForRandomTranAsync(int id);

        Task UpdateAsync(CreditCard entity);
    }
}