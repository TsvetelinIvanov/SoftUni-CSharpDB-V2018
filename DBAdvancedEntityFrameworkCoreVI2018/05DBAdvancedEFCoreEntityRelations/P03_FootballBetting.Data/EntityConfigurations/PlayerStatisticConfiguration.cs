﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfigurations
{
    public class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder.HasKey(ps => new { ps.GameId, ps.PlayerId });

            builder.HasOne(ps => ps.Game).WithMany(g => g.PlayerStatistics).HasForeignKey(ps => ps.GameId);

            builder.HasOne(ps => ps.Player).WithMany(p => p.PlayerStatistics).HasForeignKey(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}