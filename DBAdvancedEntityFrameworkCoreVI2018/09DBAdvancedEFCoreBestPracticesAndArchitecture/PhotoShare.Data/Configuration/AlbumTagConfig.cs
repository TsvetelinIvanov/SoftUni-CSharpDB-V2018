using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoShare.Models;

namespace PhotoShare.Data.Configuration
{
    internal class AlbumTagConfig : IEntityTypeConfiguration<AlbumTag>
    {
        public void Configure(EntityTypeBuilder<AlbumTag> builder)
        {
            builder.HasKey(at => new { at.AlbumId, at.TagId });

            builder.HasOne(at => at.Album)
                   .WithMany(a => a.AlbumTags)
                   .HasForeignKey(at => at.AlbumId);

            builder.HasOne(at => at.Tag)
                   .WithMany(t => t.AlbumTags)
                   .HasForeignKey(at => at.TagId);
        }
    }
}