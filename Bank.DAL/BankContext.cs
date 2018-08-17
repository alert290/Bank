using Bank.DAL.Models;
using Bank.DAL.Models.Mapping;
using System.Data.Entity;

namespace Bank.DAL
{
    public class BankContext : DbContext
    {
        public BankContext()
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public BankContext(string connectionStr)
            : base(connectionStr)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CreditCardMap());
            modelBuilder.Configurations.Add(new TransactionMap());
        }
    }
}