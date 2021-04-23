using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;
using System;

namespace P03_FootballBetting.Data.EntityConfigurations
{
    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(b => b.BetId);

            builder.Property(b => b.Prediction).IsRequired();

            builder.Property(b => b.DateTime).HasDefaultValue(DateTime.Now);

            builder.Property(b => b.UserId).IsRequired();
            builder.HasOne(b => b.User).WithMany(u => u.Bets).HasForeignKey(b => b.UserId);

            builder.Property(b => b.GameId).IsRequired();
            builder.HasOne(b => b.Game).WithMany(g => g.Bets).HasForeignKey(b => b.GameId);
        }
    }
}