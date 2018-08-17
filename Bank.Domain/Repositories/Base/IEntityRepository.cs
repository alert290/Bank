using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bank.Domain.Repositories.Base
{
    public interface IEntityRepository<T>
    {
        IEnumerable<T> GetCollection();

        T GetById(int id);

        int Insert(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
