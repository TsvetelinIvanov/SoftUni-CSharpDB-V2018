using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.EntityConfigurations
{
    public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(ut => new { ut.TeamId, ut.UserId });

            builder.HasOne(ut => ut.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(ut => ut.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ut => ut.User)
                .WithMany(u => u.MembersOf)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}