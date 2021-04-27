using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoShare.Models;

namespace PhotoShare.Data.Configuration
{
    internal class AlbumRoleConfig : IEntityTypeConfiguration<AlbumRole>
    {
        public void Configure(EntityTypeBuilder<AlbumRole> builder)
        {
            builder.HasKey(ar => new { ar.AlbumId, ar.UserId });

            builder.HasOne(ar => ar.User)
                   .WithMany(u => u.AlbumRoles)
                   .HasForeignKey(ar => ar.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ar => ar.Album)
                   .WithMany(a => a.AlbumRoles)
                   .HasForeignKey(ar => ar.AlbumId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}