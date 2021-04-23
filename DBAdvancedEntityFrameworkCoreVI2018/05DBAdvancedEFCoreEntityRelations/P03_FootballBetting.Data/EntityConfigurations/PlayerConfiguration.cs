using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfigurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.PlayerId);

            builder.Property(p => p.Name).IsRequired();

            //builder.Property(p => p.IsInjured).IsRequired().HasDefaultValue(false);

            builder.HasOne(p => p.Position).WithMany(po => po.Players).HasForeignKey(p => p.PositionId);

            builder.HasOne(p => p.Team).WithMany(t => t.Players).HasForeignKey(p => p.TeamId);
        }
    }
}