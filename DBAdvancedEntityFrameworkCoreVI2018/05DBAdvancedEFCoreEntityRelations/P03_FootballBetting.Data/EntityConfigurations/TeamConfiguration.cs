using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfigurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.TeamId);

            builder.Property(t => t.Name).IsUnicode().IsRequired();
            //builder.Property(t => t.Name).IsRequired();

            builder.Property(t => t.LogoUrl).IsUnicode().IsRequired();
            //builder.Property(t => t.LogoUrl).IsUnicode(false).IsRequired();

            builder.Property(t => t.Initials).HasMaxLength(3).IsUnicode().IsRequired();
            //builder.Property(t => t.Initials).HasColumnType("NCHAR(3)").IsRequired();

            builder.HasOne(t => t.PrimaryKitColor).WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(c => c.PrimaryKitColorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.SecondaryKitColor).WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Town).WithMany(t => t.Teams).HasForeignKey(t => t.TownId);
        }
    }
}