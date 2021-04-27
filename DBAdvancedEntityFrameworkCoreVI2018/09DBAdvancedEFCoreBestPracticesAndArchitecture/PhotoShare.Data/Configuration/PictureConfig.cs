using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoShare.Models;

namespace PhotoShare.Data.Configuration
{
    class PictureConfig : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.Caption)
                   .HasMaxLength(250);

            builder.Property(p => p.Path)
                   .IsRequired(true);

            builder.HasOne(p => p.Album)
                   .WithMany(a => a.Pictures)
                   .HasForeignKey(p => p.AlbumId);

            builder.Ignore(p => p.UserProfileId);
        }
    }
}