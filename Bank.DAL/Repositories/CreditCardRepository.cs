using AutoMapper;
using Bank.DAL.Models;
using Bank.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bank.DAL.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly BankContext context;
        private readonly IMapper mapper;
        private bool disposed = false;

        public CreditCardRepository(IMapper mapper)
        {
            this.context = new BankContext();
            this.mapper = mapper;
        }

        public void Delete(int id)
        {
            CreditCard entityToDelete = this.context.CreditCards.First(x => x.CreditCardID == id);
            this.context.CreditCards.Remove(entityToDelete);
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

        public Domain.Models.CreditCard GetById(int id)
        {
            return this.mapper.Map<Domain.Models.CreditCard>(this.context.CreditCards.First(x => x.CreditCardID == id));
        }

        public async Task<Domain.Models.CreditCard> GetByIdAsync(int id)
        {
            return this.mapper.Map<Domain.Models.CreditCard>(await this.context.CreditCards.FindAsync(id));
        }

        public IEnumerable<Domain.Models.CreditCard> GetByCustomerId(int id)
        {
            return this.mapper.Map<IEnumerable<Domain.Models.CreditCard>>(this.context.CreditCards.Where(x => x.CustomerID == id));
        }

        public IEnumerable<Domain.Models.CreditCard> GetCollection()
        {
            return this.mapper.Map<IEnumerable<CreditCard>, IEnumerable<Domain.Models.CreditCard>>(this.context.CreditCards);
        }

        public async Task<List<Domain.Models.CreditCard>> GetCollectionForRandomTranAsync(int id)
        {
            return this.mapper.Map<List<CreditCard>, List<Domain.Models.CreditCard>>(await Task.Run(() => this.context.CreditCards.Where(x => x.CreditCardID != id).ToList()));
        }

        public int Insert(Domain.Models.CreditCard entity)
        {
            CreditCard entityToInsert = this.context.CreditCards.Create();
            this.mapper.Map(entity, entityToInsert);
            this.context.CreditCards.Add(entityToInsert);
            this.context.SaveChanges();
            return entityToInsert.CreditCardID;
        }

        public void Update(Domain.Models.CreditCard entity)
        {
            CreditCard entityToUpdate = this.context.CreditCards.First(x => x.CreditCardID == entity.CreditCardID);
            this.mapper.Map(entity, entityToUpdate);
            this.context.SaveChanges();
        }

        public async Task UpdateAsync(Domain.Models.CreditCard entity)
        {
            CreditCard entityToUpdate = await this.context.CreditCards.FindAsync(entity.CreditCardID);
            this.mapper.Map(entity, entityToUpdate);
            await this.context.SaveChangesAsync();
        }
    }
}