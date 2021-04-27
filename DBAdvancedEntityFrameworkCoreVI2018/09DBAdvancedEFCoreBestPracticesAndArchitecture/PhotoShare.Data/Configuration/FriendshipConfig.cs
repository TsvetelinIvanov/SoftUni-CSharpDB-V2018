using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoShare.Models;

namespace PhotoShare.Data.Configuration
{
    class FriendshipConfig : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(f => new { f.UserId, f.FriendId });

            builder.HasOne(f => f.User)
                   .WithMany(u => u.FriendsAdded)
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}