using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.EntityConfiguration
{
    class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.AuthorId);

            builder.Property(a => a.FirstName)
                .IsRequired(false)
                .IsUnicode(true)
                .HasMaxLength(50);

            builder.Property(a => a.LastName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);
        }
    }
}