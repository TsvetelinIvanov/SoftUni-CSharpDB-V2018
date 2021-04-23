using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data.EntityConfigurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.Name).HasMaxLength(50).IsUnicode().IsRequired();

            builder.Property(p => p.Quantity).IsRequired();

            builder.Property(p => p.Price).IsRequired();

            builder.Property(p => p.Description).HasMaxLength(250).HasDefaultValue("No description");
        }
    }
}