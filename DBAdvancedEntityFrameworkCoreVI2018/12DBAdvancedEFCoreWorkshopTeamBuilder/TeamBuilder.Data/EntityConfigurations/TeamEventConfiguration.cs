using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.EntityConfigurations
{
    public class TeamEventConfiguration : IEntityTypeConfiguration<TeamEvent>
    {
        public void Configure(EntityTypeBuilder<TeamEvent> builder)
        {
            builder.ToTable("EventTeams");

            builder.HasKey(te => new { te.TeamId, te.EventId });

            builder.HasOne(te => te.Team)
                .WithMany(t => t.Events)
                .HasForeignKey(te => te.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(te => te.Event)
                .WithMany(e => e.ParticipatingEventTeams)
                .HasForeignKey(te => te.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}