using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Bank.DAL.Models;
using Bank.Domain.Repositories;

namespace Bank.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankContext context;
        private readonly IMapper mapper;
        private bool disposed = false;

        public TransactionRepository(IMapper mapper)
        {
            this.context = new BankContext();
            this.mapper = mapper;
        }

        public void Delete(int id)
        {
            Transaction entityToDelete = this.context.Transactions.First(x => x.TransactionID == id);
            this.context.Transactions.Remove(entityToDelete);
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }

        public Domain.Models.Transaction GetById(int id)
        {
            return this.mapper.Map<Domain.Models.Transaction>(this.context.Transactions.First(x => x.TransactionID == id));
        }

        public IEnumerable<Domain.Models.Transaction> GetCollection()
        {
            return this.mapper.Map<IEnumerable<Transaction>, IEnumerable<Domain.Models.Transaction>>(this.context.Transactions);
        }

        public IEnumerable<Domain.Models.Transaction> GetTransactionsByCard(Domain.Models.SearchFilter searchfilter)
        {
            IEnumerable<Transaction> collection = collection = this.context.Transactions.Where(x =>
                (searchfilter.AmountFrom == 0 || x.Amount >= searchfilter.AmountFrom) &&
                (searchfilter.AmountTo == 0 || x.Amount <= searchfilter.AmountTo) &&
                (!searchfilter.DateFrom.HasValue || x.Date >= searchfilter.DateFrom) &&
                (!searchfilter.DateTo.HasValue || x.Date <= searchfilter.DateTo) &&
                (x.FromCreditCardID == searchfilter.CreditCardID || x.ToCreditCardID == searchfilter.CreditCardID));
            return this.mapper.Map<IEnumerable<Domain.Models.Transaction>>(collection);
        }

        public int Insert(Domain.Models.Transaction entity)
        {
            Transaction entityToInsert = this.context.Transactions.Create();
            this.mapper.Map(entity, entityToInsert);
            this.context.Transactions.Add(entityToInsert);
            this.context.SaveChanges();
            return entityToInsert.TransactionID;
        }

        public async Task<int> InsertAsync(Domain.Models.Transaction entity)
        {
            Transaction entityToInsert = this.context.Transactions.Create();
            this.mapper.Map(entity, entityToInsert);
            this.context.Transactions.Add(entityToInsert);
            await this.context.SaveChangesAsync();
            return entityToInsert.TransactionID;
        }

        public void Update(Domain.Models.Transaction entity)
        {
            Transaction entityToUpdate = this.context.Transactions.First(x => x.TransactionID == entity.TransactionID);
            this.mapper.Map(entity, entityToUpdate);
            this.context.SaveChanges();
        }
    }
}