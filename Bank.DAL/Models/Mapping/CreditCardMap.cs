using System.Data.Entity.ModelConfiguration;

namespace Bank.DAL.Models.Mapping
{
    public class CreditCardMap : EntityTypeConfiguration<CreditCard>
    {
        public CreditCardMap()
        {
            this.HasKey(x => x.CreditCardID);
            this.Property(x => x.CardNumber).HasMaxLength(16).IsRequired();
            this.Property(x => x.PIN).HasMaxLength(60).IsRequired();
            this.HasRequired(x => x.Customer)
                .WithMany(y => y.CreditCards)
                .HasForeignKey(x => x.CustomerID)
                .WillCascadeOnDelete(false);
        }
    }
}