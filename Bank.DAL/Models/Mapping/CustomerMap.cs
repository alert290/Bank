using System.Data.Entity.ModelConfiguration;

namespace Bank.DAL.Models.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.HasKey(x => x.CustomerID);
            this.Property(x => x.FirstName).HasMaxLength(256);
            this.Property(x => x.LastName).HasMaxLength(256);
            this.Property(x => x.Login).HasMaxLength(256).IsRequired();
            this.Property(x => x.Password).HasMaxLength(60).IsRequired();
        }
    }
}