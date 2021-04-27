using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoShare.Models;

namespace PhotoShare.Data.Configuration
{
    class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                   .HasMaxLength(60)
                   .IsRequired();

            builder.Property(t => t.Country)
                   .HasMaxLength(60);
        }
    }
}