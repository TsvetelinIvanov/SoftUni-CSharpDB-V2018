using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfigurations
{
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(t => t.TownId);

            //builder.HasAlternateKey(t => new { t.Name, t.CountryId });

            builder.Property(t => t.Name).IsRequired();

            builder.Property(t => t.CountryId).IsRequired();
            builder.HasOne(t => t.Country).WithMany(c => c.Towns).HasForeignKey(t => t.CountryId);
        }
    }
}