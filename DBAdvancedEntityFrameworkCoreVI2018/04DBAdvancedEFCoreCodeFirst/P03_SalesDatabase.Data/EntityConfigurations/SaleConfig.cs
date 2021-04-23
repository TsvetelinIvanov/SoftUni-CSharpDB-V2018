using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data.EntityConfigurations
{
    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.SaleId);

            builder.Property(s => s.Date).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.ProductId).IsRequired();
            builder.HasOne(s => s.Product).WithMany(p => p.Sales).HasForeignKey(s => s.ProductId);

            builder.Property(s => s.CustomerId).IsRequired();
            builder.HasOne(s => s.Customer).WithMany(c => c.Sales).HasForeignKey(s => s.CustomerId);

            builder.Property(s => s.StoreId).IsRequired();
            builder.HasOne(s => s.Store).WithMany(st => st.Sales).HasForeignKey(s => s.StoreId);
        }
    }
}