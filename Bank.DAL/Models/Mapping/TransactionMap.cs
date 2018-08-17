using System.Data.Entity.ModelConfiguration;

namespace Bank.DAL.Models.Mapping
{
    public class TransactionMap : EntityTypeConfiguration<Transaction>
    {
        public TransactionMap()
        {
            this.HasKey(x => x.TransactionID);
            this.Property(x => x.Amount).IsRequired();
            this.Property(x => x.Comment).HasMaxLength(512);
            this.HasRequired(x => x.FromCreditCard)
                .WithMany(y => y.TransactionsFrom)
                .HasForeignKey(x => x.FromCreditCardID)
                .WillCascadeOnDelete(false);
            this.HasRequired(x => x.ToCreditCard)
                .WithMany(y => y.TransactionsTo)
                .HasForeignKey(x => x.ToCreditCardID)
                .WillCascadeOnDelete(false);
        }
    }
}