﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfigurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.GameId);

            builder.HasOne(g => g.HomeTeam).WithMany(t => t.HomeGames).HasForeignKey(g => g.HomeTeamId)            
              .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(g => g.AwayTeam).WithMany(t => t.AwayGames).HasForeignKey(g => g.AwayTeamId);
        }
    }
}