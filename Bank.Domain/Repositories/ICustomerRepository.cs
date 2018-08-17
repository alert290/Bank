using Bank.Domain.Models;
using Bank.Domain.Repositories.Base;

namespace Bank.Domain.Repositories
{
    public interface ICustomerRepository : IEntityRepository<Customer>, IDisposableRepository
    {
    }
}