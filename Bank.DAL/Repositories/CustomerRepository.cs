using AutoMapper;
using Bank.DAL.Models;
using Bank.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bank.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankContext context;
        private readonly IMapper mapper;
        private bool disposed = false;

        public CustomerRepository(IMapper mapper)
        {
            this.context = new BankContext();
            this.mapper = mapper;
        }

        public void Delete(int id)
        {
            Customer entityToDelete = this.context.Customers.First(x => x.CustomerID == id);
            this.context.Customers.Remove(entityToDelete);
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

        public IEnumerable<Domain.Models.Customer> Get(Expression<Func<Domain.Models.Customer, bool>> filter = null)
        {
            IEnumerable<Customer> collection;
            if (filter != null)
            {
                var convertedFilter = this.mapper.Map<Expression<Func<Customer, bool>>>(filter);
                collection = this.context.Customers.Where(convertedFilter);
            }
            else
            {
                collection = this.context.Customers;
            }

            return this.mapper.Map<IEnumerable<Domain.Models.Customer>>(collection);
        }

        public Domain.Models.Customer GetById(int id)
        {
            return this.mapper.Map<Domain.Models.Customer>(this.context.Customers.First(x => x.CustomerID == id));
        }

        public IEnumerable<Domain.Models.Customer> GetCollection()
        {
            return this.mapper.Map<IEnumerable<Customer>, IEnumerable<Domain.Models.Customer>>(this.context.Customers);
        }

        public int Insert(Domain.Models.Customer entity)
        {
            Customer entityToInsert = this.context.Customers.Create();
            this.mapper.Map(entity, entityToInsert);
            this.context.Customers.Add(entityToInsert);
            this.context.SaveChanges();
            return entityToInsert.CustomerID;
        }

        public void Update(Domain.Models.Customer entity)
        {
            Customer entityToUpdate = this.context.Customers.First(x => x.CustomerID == entity.CustomerID);
            this.mapper.Map(entity, entityToUpdate);
            this.context.SaveChanges();
        }
    }
}